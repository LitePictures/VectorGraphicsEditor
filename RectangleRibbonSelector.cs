using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SimpleVectorGraphicsEditor
{
    public enum RibbonMode
    {
        None,
        DotRectangle,
        SolidRectangle,
        SolidLine
    }

    public class RibbonSelectedEventArgs : EventArgs
    {
        public Rectangle RectangleSelected { get; set; }
    }

    /// <summary>
    /// Описание класса выбора рамкой
    /// </summary>
    public class RectangleRibbonSelector
    {
        protected readonly Control Container;
        private Point _mouseDownLocation = Point.Empty;
        private Rectangle _ribbonRect;
        private Point _pt1, _pt2;
        private bool _disabled;

        public event EventHandler<RibbonSelectedEventArgs> OnSelected;

        /// <summary>
        /// Режим запрета рисования
        /// </summary>
        public bool Disabled
        {
            get { return _disabled; }
            set
            {
                _disabled = value;
                Mode = _disabled ? RibbonMode.None : RibbonMode.DotRectangle;
            }
        }

        /// <summary>
        /// Режим рисования
        /// </summary>
        public RibbonMode Mode { get; set; }

        /// <summary>
        /// Конструктор с параметрами для перехвата и обработки событий
        /// </summary>
        /// <param name="container">визуальный компонент с поверхностью для рисования</param>
        public RectangleRibbonSelector(Control container)
        {
            Mode = RibbonMode.DotRectangle;
            // запоминаем ссылку на контейнер для рисования
            Container = container;
            // подключаемся к необходимым событиям на контейнере
            Container.MouseDown += ContainerMouseDown;
            Container.MouseMove += ContainerMouseMove;
            Container.MouseUp += ContainerMouseUp;
            Container.Paint += ContainerPaint;
        }

        /// <summary>
        /// Обработчик события рисования на поверхности контейнера
        /// </summary>
        /// <param name="sender">визуальный компонент с поверхностью для рисования</param>
        /// <param name="e">объект параметров события со свойством Graphics</param>
        protected virtual void ContainerPaint(object sender, PaintEventArgs e)
        {
            if (Disabled || Mode == RibbonMode.None) return;
            // если прямоугольник выбора не пуст
            if (_ribbonRect.IsEmpty) return;
            // рисуем рамку прямоугольника выбора
            using (var pen = new Pen(Color.Black))
            {
                pen.DashStyle = (Mode == RibbonMode.DotRectangle) ? DashStyle.Dot : DashStyle.Solid;
                if (Mode == RibbonMode.SolidLine)
                    e.Graphics.DrawLine(pen, _pt1, _pt2);
                else
                    e.Graphics.DrawRectangle(pen, _ribbonRect);
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки мышки
        /// </summary>
        /// <param name="sender">визуальный компонент с поверхностью для рисования</param>
        /// <param name="e">объект параметров события со свойством Location</param>
        protected virtual void ContainerMouseDown(object sender, MouseEventArgs e)
        {
            // обрабатываем событие, если была нажата левая кнопка мышки
            if (e.Button != MouseButtons.Left) return;
            // обнуление прямоугольника выбора
            _ribbonRect = Rectangle.Empty;
            // запоминаем точку первую точку выбора начала рисования прямоугольника выбора
            _mouseDownLocation = e.Location;
            _pt1 = _mouseDownLocation;
            _pt2 = e.Location;
        }

        /// <summary>
        /// Обработчик события перемещения мышки
        /// </summary>
        /// <param name="sender">визуальный компонент с поверхностью для рисования</param>
        /// <param name="e">объект параметров события со свойством Location</param>
        protected virtual void ContainerMouseMove(object sender, MouseEventArgs e)
        {
            // обрабатываем событие, если была нажата левая кнопка мышки
            if (e.Button != MouseButtons.Left) return;
            // нормализация параметров для прямоугольника выбора
            // в случае, если мы "растягиваем" прямоугольник не только по "главной" диагонали
            _ribbonRect.X = Math.Min(_mouseDownLocation.X, e.Location.X);
            _ribbonRect.Y = Math.Min(_mouseDownLocation.Y, e.Location.Y);
            // размеры должны быть всегда положительные числа
            _ribbonRect.Width = Math.Abs(_mouseDownLocation.X - e.Location.X);
            _ribbonRect.Height = Math.Abs(_mouseDownLocation.Y - e.Location.Y);
            _pt1 = _mouseDownLocation;
            _pt2 = e.Location;
            // запрашиваем контейнер для рисования, чтобы обновился
            Container.Invalidate();
        }

        /// <summary>
        /// Обработчик события отпускания кнопки мышки
        /// </summary>
        /// <param name="sender">визуальный компонент с поверхностью для рисования</param>
        /// <param name="e">объект параметров события</param>
        protected virtual void ContainerMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            // инициация события при окончании выбора прямоугольником
            if (!_ribbonRect.IsEmpty)
            {
                // создаём объект аргумента для возбуждения события
                var args = new RibbonSelectedEventArgs
                    {
                        // и передаём выбранный прямоугольник
                        RectangleSelected = _ribbonRect
                    };
                // возбуждаем событие окончания выбора
                OnRibbonSelected(args);
            }
            // обнуление прямоугольника выбора
            _ribbonRect = Rectangle.Empty;
            _pt1 = _pt2 = Point.Empty;
            // запрашиваем контейнер для рисования, чтобы обновился
            Container.Invalidate();
        }

        /// <summary>
        /// Метод инициации события по окончании процесса выбора прямоугольником
        /// </summary>
        /// <param name="e">объект параметров события со свойством RectangleSelected</param>
        protected virtual void OnRibbonSelected(RibbonSelectedEventArgs e)
        {
            // если на событие подписались, то вызываем его
            if (OnSelected != null)
                OnSelected(this, e);
        }

    }
}
