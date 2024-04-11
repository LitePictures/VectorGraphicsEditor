using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace SimpleVectorGraphicsEditor
{
    public class FigureSelectedEventArgs : EventArgs
    {
        public Figure FigureSelected { get; set; }
    }

    /// <summary>
    /// Режимы редактора
    /// </summary>
    public enum EditorMode
    {
        Selection,
        Dragging,
        AddLine,
        AddPolygon
    }

    /// <summary>
    /// Описание класса хранилища фигур
    /// </summary>
    [Serializable]
    public class PictureEditor
    {
        public Color DefaultForeColor = Color.Black;
        public Color DefaultBackColor = Color.White;
        public float DefaultPenWidth = 1f;

        /// <summary>
        /// Сброс редактора
        /// </summary>
        public void Reset()
        {
            _fileName = string.Empty;
            _selected.Clear();
            _figures.Clear();
            DefaultForeColor = Color.Black;
            DefaultBackColor = Color.White;
            DefaultPenWidth = 1f;
            _container.Invalidate();
            OnEditorFarConnerUpdated();
        }
        
        public Point MouseDownLocation { get; private set; }

        private PointF _pasteOffset = Point.Empty;

        private Point _mouseOffset = Point.Empty;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        [NonSerialized]
        private readonly RectangleRibbonSelector _ribbonSelector;

        /// <summary>
        /// Внешнее контекстное меню для фона редактора
        /// </summary>
        public ContextMenuStrip BackgoundContextMenu { get; set; }

        /// <summary>
        /// Внешнее контекстное меню фигуры
        /// </summary>
        public ContextMenuStrip FigureContextMenu { get; set; }

        private bool _controlPressed;
        private bool _altPressed;

        // список созданных фигур
        private readonly List<Figure> _figures = new List<Figure>();

        // список выбранных фигур
        private readonly ObservableCollection<Figure> _selected = new ObservableCollection<Figure>();

        // контейнер для рисования фигур
        private readonly Control _container;

        // контейнер для нажатия клавиш
        [NonSerialized]
        private readonly Form _form;

        private EditorMode _editorMode;
        private bool _nodeChanging;

        /// <summary>
        /// Режим работы редактора
        /// </summary>
        public EditorMode EditorMode
        {
            get { return _editorMode; }
            set
            {
                _editorMode = value;
                // выбор рамкой запрещён в режиме EditorMode.Dragging
                _ribbonSelector.Disabled = _editorMode == EditorMode.Dragging;
            }
        }

        /// <summary>
        /// Текущий индекс маркера
        /// </summary>
        public int MarkerIndex { get; private set; }

        /// <summary>
        /// Режим выбора и перетаскивания узловых точек (изменения узлов)
        /// </summary>
        public bool NodeChanging
        {
            get { return _nodeChanging; }
            set
            {
                _nodeChanging = value;
                _container.Invalidate();
            }
        }

        /// <summary>
        /// Размерный прямоугольник, охватывающий все фигуры
        /// </summary>
        public RectangleF GetBounds
        {
            get
            {
                using (var gp = new GraphicsPath())
                {
                    foreach (var fig in _figures)
                        gp.AddRectangle(fig.GetBounds);
                    return gp.GetBounds();
                }
            }
        }

        /// <summary>
        /// Размер всех фигур в совокупности, с учетом смещения от верхнего левого угла
        /// </summary>
        public RectangleF ClientRectangle
        {
            get
            {
                var rect = GetBounds;
                rect.Width += rect.Left;
                rect.Height += rect.Top;
                rect.Location = PointF.Empty;
                return rect;
            }
        }

        /// <summary>
        /// Точка подключения обработчика события выбора
        /// </summary>
        public event EventHandler<FigureSelectedEventArgs> FigureSelected;

        /// <summary>
        /// Точка подключения обработчика события изменения размера
        /// </summary>
        public event EventHandler EditorFarConnerUpdated;

        protected virtual void OnEditorFarConnerUpdated()
        {
            var handler = EditorFarConnerUpdated;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="container">контейнер для рисования</param>
        public PictureEditor(Control container)
        {
            _container = container;
            // подключаемся к необходимым событиям на контейнере
            _container.MouseDown += ContainerMouseDown;
            _container.MouseMove += ContainerMouseMove;
            _container.MouseUp += ContainerMouseUp;
            _container.Paint += ContainerPaint;
            // подключаем объект выбора рамкой
            _ribbonSelector = new RectangleRibbonSelector(_container);
            _ribbonSelector.OnSelected += RibbonSelectorOnSelected;
            // а здесь пробуем найти ссылку на форму, на которой расположен PaintBox
            var parent = _container.Parent;
            // пока не найдём форму или пустой Parent
            while (!(parent is Form))
            {
                if (parent == null) break;
                parent = parent.Parent;
            }
            _form = parent as Form;
            // если найдена форма
            if (_form != null)
            {
                // то подключим к ней обработчик нажатия клавиш
                _form.KeyDown += FormKeyDown;
                _form.KeyUp += FormKeyUp;
                // включим признак предварительного просмотра нажатия клавиш
                _form.KeyPreview = true;
            }
            // при изменении выбора выключаем режим изменения узлов
            _selected.CollectionChanged += (sender, args) =>
                {
                    NodeChanging = false;
                };
            FileChanged = false;
        }

        /// <summary>
        /// Отражение всех выбранных слева-направо
        /// </summary>
        public void FlipHorizontal()
        {
            if (_selected.Count > 0) FileChanged = true;
            foreach (var fig in _selected) fig.FlipHorizontal();
            _container.Invalidate();
        }

        /// <summary>
        /// Отражение всех выбранных сверху-вниз
        /// </summary>
        public void FlipVertical()
        {
            if (_selected.Count > 0) FileChanged = true;
            foreach (var fig in _selected) fig.FlipVertical();
            _container.Invalidate();
        }

        /// <summary>
        /// Установка цвета карандаша
        /// </summary>
        /// <param name="selcolor">цвет</param>
        public void SetForeColor(Color selcolor)
        {
            if (_selected.Count > 0) FileChanged = true;
            foreach (var fig in _selected) fig.Stroke.Color = selcolor;
            _container.Invalidate();
        }

        /// <summary>
        /// Установка цвета фона
        /// </summary>
        /// <param name="selcolor">цвет</param>
        public void SetBackColor(Color selcolor)
        {
            if (_selected.Count > 0) FileChanged = true;
            foreach (var fig in _selected) fig.Fill.Color = selcolor;
            _container.Invalidate();
        }

        /// <summary>
        /// Установка толщины линии
        /// </summary>
        /// <param name="selwidth">толщина</param>
        public void SetPenWidth(int selwidth)
        {
            if (_selected.Count > 0) FileChanged = true;
            foreach (var fig in _selected) fig.Stroke.Width = selwidth;
            _container.Invalidate();
        }

        /// <summary>
        /// Поворот всех выбранных налево на четверть
        /// </summary>
        public void TurnRightAt90()
        {
            if (_selected.Count > 0) FileChanged = true;
            foreach (var fig in _selected) fig.Rotate(90F);
            _container.Invalidate();
        }

        /// <summary>
        /// Поворот всех выбранных направо на четверть
        /// </summary>
        public void TurnLeftAt90()
        {
            if (_selected.Count > 0) FileChanged = true;
            foreach (var fig in _selected) fig.Rotate(-90F);
            _container.Invalidate();
        }

        /// <summary>
        /// Переместить фигуру ниже всех фигур
        /// </summary>
        public void SendToBack()
        {
            if (_selected.Count > 0) FileChanged = true;
            foreach (var fig in _selected)
            {
                _figures.Remove(fig);
                _figures.Insert(0, fig);
            }
            _container.Invalidate();
        }

        /// <summary>
        /// Переместить фигуру выше всех фигур
        /// </summary>
        public void BringToFront()
        {
            if (_selected.Count > 0) FileChanged = true;
            foreach (var fig in _selected)
            {
                _figures.Remove(fig);
                _figures.Add(fig);
            }
            _container.Invalidate();
        }

        // определение типа формата работы с буфером обмена Windows
        readonly DataFormats.Format _drawsFormat = DataFormats.GetFormat("clipboardVectorFiguresFormat");
        
        /// <summary>
        /// Вырезать выделенные в буфер обмена
        /// </summary>
        public void CutSelectedToClipboard()
        {
            FileChanged = true;
            _pasteOffset = Point.Empty;
            var forcopy = _selected.ToList();
            var clipboardDataObject = new DataObject(_drawsFormat.Name, forcopy);
            Clipboard.SetDataObject(clipboardDataObject, false);
            foreach (var fig in _selected) _figures.Remove(fig);
            _selected.Clear();
            GC.Collect();
            _container.Invalidate();
        }

        /// <summary>
        /// Копировать выделенные в буфер обмена
        /// </summary>
        public void CopySelectedToClipboard()
        {
            _pasteOffset = Point.Empty;
            var forcopy = _selected.ToList();
            var clipboardDataObject = new DataObject(_drawsFormat.Name, forcopy);
            Clipboard.SetDataObject(clipboardDataObject, false);
        }

        /// <summary>
        /// Признак возможности вставки данных из буфера обмена
        /// </summary>
        public bool CanPasteFromClipboard
        {
            get { return Clipboard.ContainsData(_drawsFormat.Name); }
        }

        /// <summary>
        /// Вставка ранее скопированных фигур из буфера обмена
        /// </summary>
        public void PasteFromClipboardAndSelected()
        {
            if (!Clipboard.ContainsData(_drawsFormat.Name)) return;
            FileChanged = true;
            var clipboardRetrievedObject = Clipboard.GetDataObject();
            if (clipboardRetrievedObject == null) return;
            var pastedObject = (List<Figure>) clipboardRetrievedObject.GetData(_drawsFormat.Name);
            _selected.Clear();
            _pasteOffset = PointF.Add(_pasteOffset, new SizeF(5, 5));
            foreach (var fig in pastedObject)
            {
                fig.Offset(_pasteOffset);
                _figures.Add(fig);
                _selected.Add(fig);
            }
            _container.Invalidate();
        }

        /// <summary>
        /// Выбрать все фигуры
        /// </summary>
        public void SelectAllFigures()
        {
            _selected.Clear();
            foreach (var fig in _figures)
                _selected.Add(fig);
            _container.Invalidate();
        }

        /// <summary>
        /// Признак изменения данных.
        /// ВНИМАНИЕ! Для правильной работы логики Undo|Redo
        /// изменение этого свойства производить ДО изменения данных!
        /// </summary>
        public bool FileChanged
        {
            get
            {
                return (_fileChanged);
            }
            set
            {
                _fileChanged = value;
                PrepareToUndo(_fileChanged);
                PrepareToRedo(false);
            }
        }

        readonly StackMemory _undoStack = new StackMemory(100);
        readonly StackMemory _redoStack = new StackMemory(100);
        
        private bool _fileChanged;
        private string _fileName = string.Empty;

        /// <summary>
        /// Подготовка к отмене (сохранения состояния)
        /// </summary>
        /// <param name="changed">True - cохранить состояние</param>
        private void PrepareToUndo(bool changed)
        {
            if (changed)
            {
                using (var stream = new MemoryStream())
                {
                    SaveToStream(stream);
                    _undoStack.Push(stream);
                }
            }
            else
                _undoStack.Clear();
        }

        /// <summary>
        /// Подготовка к возврату (сохранения состояния)
        /// </summary>
        /// <param name="changed">True - cохранить состояние</param>
        private void PrepareToRedo(bool changed)
        {
            if (changed)
            {
                using (var stream = new MemoryStream())
                {
                    SaveToStream(stream);
                    _redoStack.Push(stream);
                }
            }
            else
                _redoStack.Clear();
        }

        /// <summary>
        /// Возможность возврата после отмены
        /// </summary>
        /// <returns>True - возврат возможен</returns>
        public bool CanRedoChanges
        {
            get { return (_redoStack.Count > 0); }
        }

        /// <summary>
        /// Возврат после отмены
        /// </summary>
        public void RedoChanges()
        {
            if (!CanRedoChanges) return;
            PrepareToUndo(true);
            _selected.Clear();
            _figures.Clear();
            GC.Collect();
            using (var stream = new MemoryStream())
            {
                _redoStack.Pop(stream);
                var list = LoadFromStream(stream);
                foreach (var fig in list) _figures.Add(fig);
            }
            _container.Invalidate();
        }

        /// <summary>
        /// Возможность отмены действий, изменений
        /// </summary>
        /// <returns>Отмена возможна</returns>
        public bool CanUndoChanges
        {
            get { return (_undoStack.Count > 0); }
        }

        /// <summary>
        /// Отмена действий, изменений
        /// </summary>
        public void UndoChanges()
        {
            if (!CanUndoChanges) return;
            PrepareToRedo(true);
            _selected.Clear();
            _figures.Clear();
            GC.Collect();
            using (var stream = new MemoryStream())
            {
                _undoStack.Pop(stream);
                var list = LoadFromStream(stream);
                foreach (var fig in list) _figures.Add(fig);
            }
            _container.Invalidate();
        }

        /// <summary>
        /// Сохранить все фигуры в поток
        /// </summary>
        /// <param name="stream">поток в памяти</param>
        /// <param name="listToSave">список для сохранения</param>
        private void SaveToStream(Stream stream, List<Figure> listToSave = null)
        {
            var formatter = new BinaryFormatter();
            var list = (listToSave ?? _figures).ToList();
            formatter.Serialize(stream, list);
            stream.Position = 0;
        }

        /// <summary>
        /// Восстановить фигуры из потока в памяти
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static IEnumerable<Figure> LoadFromStream(Stream stream)
        {
            try
            {
                var formatter = new BinaryFormatter();
                stream.Position = 0;
                return (List<Figure>) formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {
                Console.WriteLine(@"Failed to deserialize. Reason: " + e.Message);
                throw;
            }
        }

        /// <summary>
        /// Во внешней форме нажата клавиша
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            // проверяем нажатие Ctrl
            if (e.Control) _controlPressed = true;
            // проверяем нажатие Alt
            if (e.Alt) _altPressed = true;
            float step = 0;
            if (_controlPressed) step = 1;  // точное позиционирование
            if (_altPressed) step = 10;     // быстрое позиционирование
            switch (e.KeyCode)
            {
                case Keys.Up:
                    FileChanged = true;
                    foreach (var fig in _selected)
                        fig.Offset(new PointF(0, -step));
                    _container.Invalidate();
                    break;
                case Keys.Down:
                    FileChanged = true;
                    foreach (var fig in _selected)
                        fig.Offset(new PointF(0, step));
                    _container.Invalidate();
                    break;
                case Keys.Left:
                    FileChanged = true;
                    foreach (var fig in _selected)
                        fig.Offset(new PointF(-step, 0));
                    _container.Invalidate();
                    break;
                case Keys.Right:
                    FileChanged = true;
                    foreach (var fig in _selected)
                        fig.Offset(new PointF(step, 0));
                    _container.Invalidate();
                    break;
                case Keys.Delete:
                    if ((_selected.Count > 0) &&
                        (MessageBox.Show(@"Удалить выделенные объекты?", @"Редактор фигур",
                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, 
                                         MessageBoxDefaultButton.Button3) == DialogResult.Yes))
                    {
                        FileChanged = true;
                        foreach (var fig in _selected) _figures.Remove(fig);
                        _selected.Clear();
                        GC.Collect();
                        _container.Invalidate();
                    }
                    break;
            }
        }

        /// <summary>
        /// Во внешней форме отпущена клавиша
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormKeyUp(object sender, KeyEventArgs e)
        {
            // проверяем отпускание Ctrl
            if (!e.Control) _controlPressed = false;
            // проверяем отпускание Alt
            if (e.Alt) _altPressed = false;
        }

        /// <summary>
        /// Поиск верхней фигуры под курсором
        /// </summary>
        /// <param name="location">точка нажатия</param>
        /// <returns>найденая фигура</returns>
        private Figure PointInFigure(PointF location)
        {
            // просматриваем с конца списка, так как последние нарисованные фигуры вверху
            for (var i = _figures.Count - 1; i >= 0; i--)
            {
                // смотрим на все фигуры, начиная с хвоста списка
                var fig = _figures[i];
                // если точка не попала в фигуру, то берём следующую
                if (!fig.PointInFigure(location)) continue;
                return fig;
            }
            return null;
        }

        /// <summary>
        /// Проверка нажатия на маркер в фигуре
        /// положительные индексы - это маркеры размеров,
        /// отрицательные - маркеры узлов,
        /// ноль - тело фигуры
        /// </summary>
        /// <param name="location">точка выбора</param>
        /// <param name="figure">указатель на фигуру</param>
        /// <returns>индекс маркера</returns>
        private int PointInMarker(PointF location, out Figure figure)
        {
            figure = null;
            // проверка нажатия на маркерах
            for (var i = _selected.Count - 1; i >= 0; i--)
            {
                // смотрим на выбранные фигуры, начиная с хвоста списка
                var fig = _selected[i];
                var found = MarkerSelected(location, fig);
                if (found == null) continue;
                figure = fig;
                return (int)found;
            }
            return 0;
        }

        /// <summary>
        /// Поиск номера маркера в разных режимах редактора
        /// </summary>
        /// <param name="pt">точка "нажатия" мышки</param>
        /// <param name="figure">проверяемая фигура</param>
        /// <returns>индекс маркера</returns>
        public int? MarkerSelected(PointF pt, Figure figure)
        {
            if (NodeChanging)
            {
                // в режиме изменения узлов
                var rects = GetNodeMarkers(figure);
                for (var i = 0; i < rects.Length; i++)
                {
                    if (rects[i].Contains(pt))
                        return -(i + 1);
                }
            }
            else
            {
                // в режиме изменения размеров или перемещения
                var bounds = figure.GetBounds;
                var rects = GetBoundMarkers(bounds).ToArray();
                for (var i = 0; i < rects.Length; i++)
                {
                    if (rects[i].Contains(pt))
                        return i + 1;
                }
            }
            return null;
        }

        /// <summary>
        /// Метод возвращает массив узловых маркеров
        /// </summary>
        /// <param name="figure">фигура для выбора маркеров</param>
        /// <returns>массив прямоугольников вокруг всех маркеров фигуры</returns>
        public RectangleF[] GetNodeMarkers(Figure figure)
        {
            var ps = figure.GetPoints();
            var rects = new RectangleF[ps.Length];
            for (var i = 0; i < ps.Length; i++)
            {
                var pt = ps[i];
                rects[i] = RectangleF.FromLTRB(pt.X - 3, pt.Y - 3, pt.X + 3, pt.Y + 3);
            }
            return rects;
        }

        /// <summary>
        /// Нажатие кнопки "мышки" на PaintBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContainerMouseDown(object sender, MouseEventArgs e)
        {
            // запоминаем точку первую точку для прямоугольника выбора
            MouseDownLocation = e.Location;
            _mouseOffset = Point.Empty;
            // если установлен другой режим, кроме выбора прямоугольником
            if (EditorMode != EditorMode.Selection)
                _selected.Clear();   // очищаем список выбранных
            // ищем маркер в точке нажатия
            Figure fig;
            MarkerIndex = PointInMarker(e.Location, out fig);
            // ищем фигуру в точке нажатия
            if (fig == null) fig = PointInFigure(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                _container.Capture = true;
                switch (EditorMode)
                {
                    case EditorMode.Selection:
                        CheckChangeSelection(e.Location);
                        if (fig != null)
                            EditorMode = EditorMode.Dragging;
                        break;
                    case EditorMode.AddPolygon:
                        _ribbonSelector.Mode = RibbonMode.SolidRectangle;
                        break;
                    case EditorMode.AddLine:
                        _ribbonSelector.Mode = RibbonMode.SolidLine;
                        break;
                }
                return;
            }
            if (e.Button != MouseButtons.Right) return;
            CheckChangeSelection(e.Location);
            // переключаем режим на выбор рамкой
            EditorMode = EditorMode.Selection;
            ShowContextMenu(e.Location);
            // просим перерисовать
            _container.Invalidate();
        }

        /// <summary>
        /// Проверка возможности выбора фигур
        /// </summary>
        public bool CanSelectFigures
        {
            get { return !NodeChanging && _figures.Count > 0; }
        }

        /// <summary>
        /// Проверка возможности работы, когда выбрана только одна фигура
        /// </summary>
        public bool CanOneFigureOperation 
        {
            get { return !NodeChanging && _selected.Count == 1; }
        }

        /// <summary>
        /// Проверка возможности работы, когда выбрана группа
        /// </summary>
        public bool CanGroupFigureOperation
        {
            get { return !NodeChanging && _selected.Count > 1; }
        }

        /// <summary>
        /// Проверка возможности начать изменение узлов
        /// </summary>
        public bool CanStartNodeChanging
        {
            get { return !NodeChanging && _selected.Count == 1; }
        }

        /// <summary>
        /// Проверка возможности закончить изменение узлов
        /// </summary>
        public bool CanStopNodeChanging
        {
            get { return NodeChanging && _selected.Count == 1; }
        }

        /// <summary>
        /// Проверка возможности удаления узла
        /// </summary>
        public bool CanNodeDeleting
        {
            get
            {
                return MarkerIndex < 0 && 
                    _selected.Count == 1 && 
                    _selected[0].GetPoints().Length > 2;
            }
        }

        /// <summary>
        /// Проверка возможности добавления узла
        /// </summary>
        public bool CanNodeAdding
        {
            get
            {
                if (!CanStopNodeChanging) return false;
                var figure = _selected[0];
                var ps = figure.GetPoints();
                var pts = new PointF[ps.Length + 1];
                ps.CopyTo(pts, 0);
                // замыкание контура фигуры
                pts[pts.Length - 1].X = pts[0].X;
                pts[pts.Length - 1].Y = pts[0].Y;
                for (var i = 1; i < pts.Length; i++)
                {
                    // поиск сегмента линии, куда бы можно добавить новый узел
                    if (!PointInRange(figure, MouseDownLocation, pts[i - 1], pts[i])) continue;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Вызов контекстного меню
        /// </summary>
        /// <param name="location">точка вызова контекстного меню</param>
        private void ShowContextMenu(Point location)
        {
            Figure fig;
            PointInMarker(location, out fig);
            // ищем фигуру в точке нажатия
            if (fig == null)
                fig = PointInFigure(location);
            // есть ли фигура под курсором мышки?
            if (fig == null) // это не фигура, показываем общее меню
            {
                if (BackgoundContextMenu != null)
                    BackgoundContextMenu.Show(_container, location);
            }
            else
            {
                if (FigureContextMenu != null)
                    FigureContextMenu.Show(_container, location);
            }
        }

        /// <summary>
        /// Метод удаления выбранного маркера
        /// </summary>
        /// <param name="markerIndex">индекс маркера для удаления</param>
        public void RemoveNode(int markerIndex = 0)
        {
            if (_selected.Count != 1) return;
            if (markerIndex == 0)
                markerIndex = MarkerIndex;
            var figure = _selected[0];
            var ps = figure.GetPoints();
            if ((ps.Length <= (figure.Kind == DrawingKind.Polygon ? 3 : 2)) ||
                (markerIndex >= ps.Length - 1)) return;
            FileChanged = true;
            var points = new List<PointF>(figure.GetPoints());
            points.RemoveAt(Math.Abs(markerIndex) - 1);
            figure.SetPoints(points.ToArray());
            _container.Invalidate();
        }

        /// <summary>
        /// Метод добавления нового узла на выбранный сегмент линии
        /// </summary>
        public void AddNode()
        {
            if (_selected.Count != 1) return;
            var figure = _selected[0];
            var ps = figure.GetPoints();
            var pts = new PointF[ps.Length + 1];
            ps.CopyTo(pts, 0);
            // замыкание контура фигуры
            pts[pts.Length - 1].X = pts[0].X;
            pts[pts.Length - 1].Y = pts[0].Y;
            FileChanged = true;
            for (var i = 1; i < pts.Length; i++)
            {
                // поиск сегмента линии, куда бы можно добавить новый узел
                if (!PointInRange(figure, MouseDownLocation, pts[i - 1], pts[i])) continue;
                var points = new List<PointF>(figure.GetPoints());
                points.Insert(i, MouseDownLocation);
                figure.SetPoints(points.ToArray());
                break;
            }
            _container.Invalidate();
        }

        /// <summary>
        /// Проверка попадания точки на отрезок между двумя другими точками
        /// </summary>
        /// <param name="figure">объект фигуры</param>
        /// <param name="p">текстируемая точка</param>
        /// <param name="p1">первая точка отрезка</param>
        /// <param name="p2">вторая точка отрезка</param>
        /// <returns></returns>
        private static bool PointInRange(Figure figure, PointF p, PointF p1, PointF p2)
        {
            using (var gp = new GraphicsPath())
            {
                gp.AddLine(p1, p2);
                using (var pen = new Pen(Color.Black, figure.Stroke.Width * 5f))
                    return gp.IsOutlineVisible(p, pen);
            }
        }

        private void Focus(Figure figure)
        {
            OnFigureSelected(new FigureSelectedEventArgs
                {
                    FigureSelected = figure
                });
        }

        /// <summary>
        /// Проверка попадания на фигуру и выбор или отмена выбора фигуры
        /// </summary>
        /// <param name="location">позиция выбора "мышкой"</param>
        private void CheckChangeSelection(Point location)
        {
            Figure fig;
            PointInMarker(location, out fig);
            // ищем фигуру в точке нажатия
            if (fig == null)
                fig = PointInFigure(location);
            // если фигура найдена
            if (fig != null)
            {
                // и если нажат Ctrl на клавиатуре
                if (_controlPressed)
                {
                    // если найденная фигура уже была в списке выбранных
                    var foundIndex = _selected.IndexOf(fig);
                    if (foundIndex >= 0)
                    {
                        // удаление из списка уже выделенного элемента
                        if (_selected.Count > 1) // последний элемент при Ctrl не убирается
                        {
                            Focus(foundIndex == 0 ? _selected[foundIndex + 1] : _selected[foundIndex - 1]);
                            _selected.Remove(fig);
                        }
                        else
                            Focus(fig);
                    }
                    else
                    {
                        _selected.Add(fig); // иначе добавление к списку
                        Focus(fig);
                    }
                }
                else // работаем без нажатия Ctrl на клавиатуре
                {
                    // если фигуры не было в списке выбранных, то
                    if (!_selected.Contains(fig))
                    {
                        _selected.Clear(); // очистка списков
                        _selected.Add(fig); // выделение одного элемента
                        Focus(fig);
                    }
                }
                // просим перерисовать контейнер
                _container.Invalidate();
            }
            else // никакая фигура не была найдена 
            {
                _selected.Clear(); // очистка списков     
                Focus(null);
                _container.Invalidate();
            }
        }

        /// <summary>
        /// Перемещение мышки над PaintBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContainerMouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDownLocation == e.Location) return;
            // если удерживается левая кнопка и мышка захвачена
            if (e.Button == MouseButtons.Left && _container.Capture)
            {
                // пересчитываем смещение мышки
                _mouseOffset.X = e.X - MouseDownLocation.X;
                _mouseOffset.Y = e.Y - MouseDownLocation.Y;
                // просим перерисовать
                _container.Invalidate();
            }
            if (e.Button != MouseButtons.None) return;
            Figure fig;
            var marker = PointInMarker(e.Location, out fig);
            if (fig != null)
            {

                switch (marker)
                {
                    case 1:
                    case 5:
                        _container.Cursor = Cursors.SizeNWSE;
                        break;
                    case 3:
                    case 7:
                        _container.Cursor = Cursors.SizeNESW;
                        break;
                    case 2:
                    case 6:
                        _container.Cursor = Cursors.SizeNS;
                        break;
                    case 4:
                    case 8:
                        _container.Cursor = Cursors.SizeWE;
                        break;
                    default:
                        _container.Cursor = Cursors.SizeAll;
                        break;
                }
            }
            else
                _container.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Расчёт номого размерного прямоугольника, с учётом новой точки смещения и индекса маркера
        /// </summary>
        /// <param name="offset">смещение относительно точки нажатия</param>
        /// <param name="figure">фигура, на которой нажали</param>
        /// <param name="marker">индекс маркера</param>
        /// <returns>Новый расчётный прямоугольник</returns>
        public static RectangleF CalcFocusRect(PointF offset, Figure figure, int marker)
        {
            var rect = figure.GetBounds;
            var dx = offset.X;
            var dy = offset.Y;
            var dw = dx;
            var dh = dy;
            switch (marker)
            {
                case 0:  // перемещение фигуры
                    rect.X += dx;
                    rect.Y += dy;
                    break;
                case 1: // влево-вверх
                    if ((rect.Height - dh > 0) && (rect.Width - dw > 0))
                    {
                        rect.Width -= dw;
                        rect.Height -= dh;
                        rect.X += dx;
                        rect.Y += dy;
                    }
                    break;
                case 2: // вверх
                    if (rect.Height - dh > 0)
                    {
                        rect.Height -= dh;
                        rect.Y += dy;
                    }
                    break;
                case 3: // вправо-вверх
                    if ((rect.Height - dh > 0) && (rect.Width + dw > 0))
                    {
                        rect.Width += dw;
                        rect.Height -= dh;
                        rect.Y += dy;
                    }
                    break;
                case 4: // вправо
                    if (rect.Width + dw > 0)
                    {
                        rect.Width += dw;
                    }
                    break;
                case 5: // вправо-вниз
                    if ((rect.Width + dw > 0) && (rect.Height + dh > 0))
                    {
                        rect.Width += dw;
                        rect.Height += dh;
                    }
                    break;
                case 6: // вниз
                    if (rect.Height + dh > 0)
                    {
                        rect.Height += dh;
                    }
                    break;
                case 7: // влево-вниз
                    if ((rect.Height + dh > 0) && (rect.Width - dw > 0))
                    {
                        rect.Width -= dw;
                        rect.Height += dh;
                        rect.X += dx;
                    }
                    break;
                case 8: // влево
                    if (rect.Width - dw > 0)
                    {
                        rect.Width -= dw;
                        rect.X += dx;
                    }
                    break;
            }
            return rect;
        }

        /// <summary>
        /// Обработчик события завершение выбора прямоугольником
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RibbonSelectorOnSelected(object sender, RibbonSelectedEventArgs e)
        {
            if (EditorMode != EditorMode.Selection) return;
            _ribbonSelector.Mode = RibbonMode.DotRectangle;
            // нормализация параметров для прямоугольника выбора
            var rect = e.RectangleSelected;
            // добавляем все фигуры, которые оказались охваченными прямоугольником выбора
            // в список выбранных фигур
            foreach (var fig in _figures.Where(fig => rect.Contains(Rectangle.Ceiling(fig.GetBounds))))
                _selected.Add(fig);
        }

        /// <summary>
        /// Отпускание кнопки мышки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContainerMouseUp(object sender, MouseEventArgs e)
        {
            // если мышь была захвачена
            if (!_container.Capture) return;
            // освобождаем захват мышки
            _container.Capture = false;
            // если нажата левая кнопка
            if (e.Button == MouseButtons.Left)
            {
                Rectangle rect;
                List<PointF> points;
                Figure figure;
                switch (EditorMode)
                {
                        // перетаскивание выбранных фигур "мышкой"
                    case EditorMode.Dragging:
                        if (MarkerIndex == 0)
                        {
                            FileChanged = true;
                            // перебираем все выделенные фигуры и смещаем
                            foreach (var fig in _selected)
                                UpdateLocation(fig, _mouseOffset);
                            _container.Invalidate();
                            OnEditorFarConnerUpdated();
                        }
                        else if (!NodeChanging && (MarkerIndex > 0)) // тянут за размерный маркер
                        {
                            FileChanged = true;
                            // перебираем все выделенные фигуры и меняем размер
                            foreach (var fig in _selected)
                                UpdateSize(fig, _mouseOffset, MarkerIndex);
                            _container.Invalidate();
                            OnEditorFarConnerUpdated();
                        }
                        else if (NodeChanging && (MarkerIndex < 0) && _selected.Count == 1) // тянут за маркер узла
                        {
                            FileChanged = true;
                            var fig = _selected[0];
                            UpdateSize(fig, _mouseOffset, MarkerIndex);
                            _container.Invalidate();
                            OnEditorFarConnerUpdated();
                        }
                        break;
                    case EditorMode.AddLine:
                        rect = new Rectangle(MouseDownLocation, new Size(_mouseOffset.X, _mouseOffset.Y));
                        points = new List<PointF>
                            {
                                new PointF(rect.Left, rect.Top),
                                new PointF(rect.Left + rect.Width, rect.Top + rect.Height)
                            };
                        figure = new Figure(DrawingKind.Polyline, points)
                            {
                                Stroke = { Color = DefaultForeColor, Width = DefaultPenWidth }
                            };
                        AddFigure(figure);
                        OnEditorFarConnerUpdated();
                        break;
                    case EditorMode.AddPolygon:
                        rect = new Rectangle(MouseDownLocation, new Size(_mouseOffset.X, _mouseOffset.Y));
                        points = new List<PointF>
                            {
                                new PointF(rect.Left, rect.Top),
                                new PointF(rect.Left + rect.Width, rect.Top),
                                new PointF(rect.Left + rect.Width, rect.Top + rect.Height),
                                new PointF(rect.Left, rect.Top + rect.Height)
                            };
                        figure = new Figure(DrawingKind.Polygon, points)
                            {
                                Stroke = { Color = DefaultForeColor, Width = DefaultPenWidth },
                                Fill = { Color = DefaultBackColor }
                            };
                        AddFigure(figure);
                        OnEditorFarConnerUpdated();
                        break;
                }
            }
            // возвращаем режим
            EditorMode = EditorMode.Selection;
            // и разрешаем выбор рамкой
            _ribbonSelector.Disabled = false;
        }

        /// <summary>
        /// Перемещение фигуры
        /// </summary>
        /// <param name="fig">объект фигуры</param>
        /// <param name="offset">смещение</param>
        public void UpdateLocation(Figure fig, PointF offset)
        {
            // перемещение фигуры
            var pts = fig.GetPoints();
            var oldrect = CalcFocusRect(PointF.Empty, fig, 0);
            var newrect = CalcFocusRect(offset, fig, 0);
            for (var i = 0; i < pts.Length; i++)
            {
                pts[i].X = newrect.Left + (pts[i].X - oldrect.Left) / oldrect.Width * newrect.Width;
                pts[i].Y = newrect.Top + (pts[i].Y - oldrect.Top) / oldrect.Height * newrect.Height;
            }
            fig.SetPoints(pts);
        }

        /// <summary>
        /// Изменение внутреннего массива точек фигуры при работе с маркерами
        /// </summary>
        /// <param name="fig">объект фигуры</param>
        /// <param name="offset">смещение</param>
        /// <param name="marker">индекс маркера</param>
        public void UpdateSize(Figure fig, PointF offset, int marker)
        {
            PointF[] pts;
            if (!NodeChanging && (marker > 0))
            {
                // перемещение границ
                pts = fig.GetPoints();
                var oldrect = CalcFocusRect(PointF.Empty, fig, marker);
                var newrect = CalcFocusRect(offset, fig, marker);
                for (var i = 0; i < pts.Length; i++)
                {
                    pts[i].X = newrect.Left + (pts[i].X - oldrect.Left) / oldrect.Width * newrect.Width;
                    pts[i].Y = newrect.Top + (pts[i].Y - oldrect.Top) / oldrect.Height * newrect.Height;
                }
                fig.SetPoints(pts);
            }
            else
                if (NodeChanging && (marker < 0))
            {
                // перемещение узлов
                pts = fig.GetPoints();
                var index = Math.Abs(marker) - 1;
                if ((index >= 0) && (index < pts.Length))
                {
                    pts[index].X += offset.X;
                    pts[index].Y += offset.Y;
                    fig.SetPoints(pts);
                }
            }
        }

        /// <summary>
        /// Метод для рисования контуров перетаскиваемых фигур
        /// </summary>
        /// <param name="figure">одна из выбранных фигур</param>
        /// <param name="graphics">канва для рисования</param>
        /// <param name="offset">смещение фигуры относительно её текущего положения</param>
        /// <param name="markerIndex">индекс маркера</param>
        /// <param name="nodeChanging">режим изменения узлов</param>
        private static void DrawFocusFigure(Figure figure, Graphics graphics, PointF offset,
                                            int markerIndex, bool nodeChanging)
        {
            if (markerIndex == 0)
            {
                using (var gp = new GraphicsPath())
                {
                    if (figure.Kind == DrawingKind.Polygon)
                        // добавляем в полигон все точки фигуры
                        gp.AddPolygon(figure.GetPoints());
                    else
                        gp.AddLines(figure.GetPoints());
                    // получаем графический путь
                    var ps = gp.PathPoints;
                    // для всех точек пути
                    for (var i = 0; i < ps.Length; i++)
                    {
                        // делаем смещение
                        ps[i].X += offset.X;
                        ps[i].Y += offset.Y;
                    }
                    DrawCustomFigure(graphics, ps, figure.Kind);
                }
            }
            else
                if (nodeChanging && (markerIndex < 0))
            {
                // тянут мышкой за маркер, изменяющий положение узла
                using (var gp = new GraphicsPath())
                {
                    if (figure.Kind == DrawingKind.Polygon)
                        gp.AddPolygon(figure.GetPoints());
                    else
                        gp.AddLines(figure.GetPoints());
                    var ps = gp.PathPoints;
                    var i = Math.Abs(markerIndex) - 1;
                    if ((i >= 0) && (i < ps.Length))
                    {
                        ps[i].X += offset.X;
                        ps[i].Y += offset.Y;
                        DrawCustomFigure(graphics, ps, figure.Kind);
                    }
                }
            }
            else if (!nodeChanging && (markerIndex > 0))
            {
                // тянут за размерный маркер
                var ps = figure.GetPoints();
                var oldrect = CalcFocusRect(new PointF(), figure, markerIndex);
                var newrect = CalcFocusRect(offset, figure, markerIndex);
                for (var i = 0; i < ps.Length; i++)
                {
                    ps[i].X = newrect.Left + (ps[i].X - oldrect.Left) / oldrect.Width * newrect.Width;
                    ps[i].Y = newrect.Top + (ps[i].Y - oldrect.Top) / oldrect.Height * newrect.Height;
                }
                DrawCustomFigure(graphics, ps, figure.Kind);
            }
        }

        /// <summary>
        /// Построение фигуры или линии для перемещения или изменения размеров
        /// </summary>
        /// <param name="graphics">Канва для рисования</param>
        /// <param name="points">список точек</param>
        /// <param name="kind">Тип фигуры</param>
        private static void DrawCustomFigure(Graphics graphics, PointF[] points, DrawingKind kind)
        {
            // определяем "карандаш" тонкий чёрный пунктирный
            using (var pen = new Pen(Color.Black, 1f))
            {
                pen.DashStyle = DashStyle.Dash;
                if (kind == DrawingKind.Polygon)
                    graphics.DrawPolygon(pen, points); // рисование контура
                else
                    graphics.DrawLines(pen, points);
            }
        }

        /// <summary>
        /// Обработчик события рисования на поверхности контейнера
        /// </summary>
        /// <param name="sender">визуальный компонент с поверхностью для рисования</param>
        /// <param name="e">объект параметров события со свойством Graphics</param>
        private void ContainerPaint(object sender, PaintEventArgs e)
        {
            // рисуем все созданные фигуры
            foreach (var fig in _figures) fig.DrawFigure(e.Graphics);
            if (EditorMode != EditorMode.Dragging)
            {
                if (NodeChanging)
                {
                    // маркеры узлов рисуем круглыми
                    foreach (var fig in _selected)
                    {
                        using (var gp = new GraphicsPath())
                        {
                            var rects = GetNodeMarkers(fig);
                            foreach (var t in rects)
                                gp.AddEllipse(t);
                            e.Graphics.FillPath(Brushes.White, gp);
                            using (var pen = new Pen(Color.Black))
                            {
                                pen.Width = 0;
                                e.Graphics.DrawPath(pen, gp);
                            }
                        }
                    }
                }
                else
                {
                    // рисуем маркеры размеров у выбранных фигур
                    foreach (var fig in _selected)
                    {
                        using (var gp = new GraphicsPath())
                        {
                            var sizeMarkers = GetBoundMarkers(fig.GetBounds);
                            foreach (var t in sizeMarkers.Where(t => t.Width > 2))
                                gp.AddRectangle(t);
                            e.Graphics.FillPath(Brushes.White, gp);
                            using (var pen = new Pen(Color.Black))
                            {
                                pen.Width = 0;
                                e.Graphics.DrawPath(pen, gp);
                            }
                        }
                    }
                }
            }
            // при перетаскивании
            if (EditorMode != EditorMode.Dragging) return;
            foreach (var fig in _selected)
                DrawFocusFigure(fig, e.Graphics, _mouseOffset, MarkerIndex, NodeChanging);
        }

        /// <summary>
        /// Метод строит восемь маркеров на углах и серединах сторон
        /// </summary>
        /// <param name="rect">размерный прямоугольник</param>
        /// <returns>Возвращает перечисление из маркеров для изменения размера</returns>
        private static IEnumerable<RectangleF> GetBoundMarkers(RectangleF rect)
        {
            if (rect.Width <= 10) rect.Inflate(5, 0);
            if (rect.Height <= 10) rect.Inflate(0, 5);
            var pts = new PointF[8];
            pts[0].X = rect.Left; pts[0].Y = rect.Top;
            pts[1].X = rect.Left + rect.Width * 0.5f; pts[1].Y = rect.Top;
            pts[2].X = rect.Right; pts[2].Y = rect.Top;
            pts[3].X = rect.Right; pts[3].Y = rect.Top + rect.Height * 0.5f;
            pts[4].X = rect.Right; pts[4].Y = rect.Bottom;
            pts[5].X = rect.Left + rect.Width * 0.5f; pts[5].Y = rect.Bottom;
            pts[6].X = rect.Left; pts[6].Y = rect.Bottom;
            pts[7].X = rect.Left; pts[7].Y = rect.Top + rect.Height * 0.5F;
            rect.Inflate(-5, -5);
            var rects = new RectangleF[pts.Length];
            for (var i = 0; i < pts.Length; i++)
            {
                float k;
                if ((rect.Width <= 5) && ((i == 1) || (i == 5)))
                    k = 1;
                else if ((rect.Height <= 5) && ((i == 3) || (i == 7)))
                    k = 1;
                else
                    k = 3;
                rects[i] = RectangleF.FromLTRB(pts[i].X - k, pts[i].Y - k,
                                                pts[i].X + k, pts[i].Y + k);
            }
            return rects;
        }

        /// <summary>
        /// Добавить фигуру в список
        /// </summary>
        /// <param name="figure">объект фигуры</param>
        public void AddFigure(Figure figure)
        {
            FileChanged = true;
            _figures.Add(figure);
            _container.Invalidate();
        }

        /// <summary>
        /// Метод инициации события по окончании процесса выбора фигуры
        /// </summary>
        /// <param name="e">объект параметров события со свойством DrawingSelected</param>
        protected virtual void OnFigureSelected(FigureSelectedEventArgs e)
        {
            // если на событие подписались, то вызываем его
            if (FigureSelected != null)
                FigureSelected(this, e);
        }

        /// <summary>
        /// Метод записи фигур в файл
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveToFile(string fileName)
        {
            using (var stream = new MemoryStream())
            {
                SaveToStream(stream);
                File.WriteAllBytes(fileName, stream.GetBuffer());
            }
            _fileName = fileName;
            FileChanged = false;
        }

        /// <summary>
        /// Метод загрузки фигур из файла
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadFromFile(string fileName)
        {
            _fileName = fileName;
            using (var stream = new MemoryStream())
            {
                _selected.Clear();
                var buff = File.ReadAllBytes(fileName);
                stream.Write(buff, 0, buff.Length);
                stream.Position = 0;
                var list = LoadFromStream(stream);
                _figures.Clear();
                foreach (var fig in list) _figures.Add(fig);
            }
            FileChanged = false;
            _container.Invalidate();
            OnEditorFarConnerUpdated();
        }

    }
}
