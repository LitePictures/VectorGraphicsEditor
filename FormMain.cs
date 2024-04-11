using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleVectorGraphicsEditor
{
    public partial class FormMain : Form
    {
        private readonly PictureEditor _editor;
        private const string Title = @"Простой векторный графический редактор (демо)";

        public FormMain()
        {
            InitializeComponent();
            // создаём хранилище созданных фигур, которое также и рисует их
            _editor = new PictureEditor(pbCanvas)
            {
                BackgoundContextMenu = cmsBkgPopup,
                FigureContextMenu = cmsFigPopup
            };
            _editor.FigureSelected += EditorFigureSelected;
            _editor.EditorFarConnerUpdated += EditorFarConnerUpdated;
            cbWidth.Items.Clear();
            for (var i = 1; i < 61; i++) cbWidth.Items.Add(i.ToString("0"));
        }

        void EditorFarConnerUpdated(object sender, EventArgs e)
        {
            UpdateCanvasSize();
        }

        private void cmsFigPopup_Opening(object sender, CancelEventArgs e)
        {
            tsmiNodeSeparator.Visible = miBeginChangeNodes.Visible = _editor.CanStartNodeChanging;
            miEndChangeNodes.Visible = _editor.CanStopNodeChanging;
            miAddFigureNode.Visible = _editor.CanNodeAdding;
            miDeleteFigureNode.Visible = _editor.CanNodeDeleting;
        }

        private void miBeginChangeNodes_Click(object sender, EventArgs e)
        {
            _editor.NodeChanging = true;
        }

        private void miEndChangeNodes_Click(object sender, EventArgs e)
        {
            _editor.NodeChanging = false;
        }

        private void miAddFigureNode_Click(object sender, EventArgs e)
        {
            _editor.AddNode();
        }

        private void miDeleteFigureNode_Click(object sender, EventArgs e)
        {
            _editor.RemoveNode();
        }

        private void miBringToFront_Click(object sender, EventArgs e)
        {
            _editor.BringToFront();
        }

        private void miSendToBack_Click(object sender, EventArgs e)
        {
            _editor.SendToBack();
        }

        private void miTurnLeft90_Click(object sender, EventArgs e)
        {
            _editor.TurnLeftAt90();
        }

        private void miTurnRight90_Click(object sender, EventArgs e)
        {
            _editor.TurnRightAt90();
        }

        private void miFlipVertical_Click(object sender, EventArgs e)
        {
            _editor.FlipVertical();
        }

        private void miFlipHorizontal_Click(object sender, EventArgs e)
        {
            _editor.FlipHorizontal();
        }

        private void cmsBkgPopup_Opening(object sender, CancelEventArgs e)
        {
            miPasteFromBuffer.Enabled = _editor.CanPasteFromClipboard;
        }

        private void miPasteFromBuffer_Click(object sender, EventArgs e)
        {
            _editor.PasteFromClipboardAndSelected();
        }

        private void miCutPopup_Click(object sender, EventArgs e)
        {
            _editor.CutSelectedToClipboard();
        }

        private void miCopyPopup_Click(object sender, EventArgs e)
        {
            _editor.CopySelectedToClipboard();
        }

        private void tsbColors_MouseDown(object sender, MouseEventArgs e)
        {
            Color selcolor;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    selcolor = MousePosToColor(e.X, e.Y);
                    if (selcolor != Color.Transparent)
                    {
                        _editor.DefaultForeColor = selcolor;
                        tsColors.Refresh();
                        _editor.SetForeColor(selcolor);
                    }
                    break;
                case MouseButtons.Right:
                    selcolor = MousePosToColor(e.X, e.Y);
                    if (selcolor != Color.Transparent)
                    {
                        _editor.DefaultBackColor = selcolor;
                        tsColors.Refresh();
                        _editor.SetBackColor(selcolor);
                    }
                    break;
            }
        }

        /// <summary>
        /// Обработчик смены выбора фигур
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void EditorFigureSelected(object sender, FigureSelectedEventArgs e)
        {
            var figure = e.FigureSelected;
            if (figure == null) return;
            cbWidth.SelectedIndex = (int)figure.Stroke.Width - 1;
            _editor.DefaultForeColor = figure.Stroke.Color;
            _editor.DefaultPenWidth = figure.Stroke.Width;
            if (figure.Kind == DrawingKind.Polygon)
                _editor.DefaultBackColor = figure.Fill.Color;
            tsColors.Refresh();
        }

        private Color MousePosToColor(int x, int y)
        {
            var result = Color.Transparent;
            var rects = new Rectangle[28];
            var xh = 35;
            for (var i = 0; i < 14; i++)
            {
                rects[i].X = xh;
                rects[i].Y = 4;
                rects[i].Width = 10;
                rects[i].Height = 10;
                xh += 16;
            }
            xh = 35;
            for (var i = 14; i < 28; i++)
            {
                rects[i].X = xh;
                rects[i].Y = 20;
                rects[i].Width = 10;
                rects[i].Height = 10;
                xh += 16;
            }
            using (var image = new Bitmap(tsbColors.Image))
            {
                for (var i = 0; i < 28; i++)
                {
                    if (!rects[i].Contains(x, y)) continue;
                    result = i == 14 ? Color.White : image.GetPixel(x, y);
                    break;
                }
            }
            return result;
        }

        private void tsbColors_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            g.DrawImage(tsbColors.Image, 2, 2);
            var forerect = new Rectangle(8, 9, 11, 11);
            using (var brush = new SolidBrush(_editor.DefaultForeColor))
                g.FillRectangle(brush, forerect);
            var pts = new Point[6];
            pts[0].X = 21; pts[0].Y = 16;
            pts[1].X = 26; pts[1].Y = 16;
            pts[2].X = 26; pts[2].Y = 27;
            pts[3].X = 15; pts[3].Y = 27;
            pts[4].X = 15; pts[4].Y = 22;
            pts[5].X = 21; pts[5].Y = 22;
            using (var brush = new SolidBrush(_editor.DefaultBackColor))
                g.FillPolygon(brush, pts);
        }

        private void tsmEditMenu_DropDownOpening(object sender, EventArgs e)
        {
            tsmSelectAll.Enabled = _editor.CanSelectFigures;
            tsmCut.Enabled = tsmCopy.Enabled = _editor.CanOneFigureOperation ||
                                               _editor.CanGroupFigureOperation;
            tsmPaste.Enabled = _editor.CanPasteFromClipboard;
            tsmUndo.Enabled = _editor.CanUndoChanges;
            tsmRedo.Enabled = _editor.CanRedoChanges;
        }

        private void tsmSelectAll_Click(object sender, EventArgs e)
        {
            _editor.SelectAllFigures();
        }

        private void timerFormUpdate_Tick(object sender, EventArgs e)
        {
            tsbCut.Enabled = tsbCopy.Enabled = _editor.CanOneFigureOperation ||
                                               _editor.CanGroupFigureOperation;
            tsbPaste.Enabled = _editor.CanPasteFromClipboard;
            tsbUndo.Enabled = _editor.CanUndoChanges;
            tsbRedo.Enabled = _editor.CanRedoChanges;
            tsmSave.Enabled = tsbSave.Enabled = _editor.FileChanged;
            if (!tsbArrow.Checked && _editor.EditorMode == EditorMode.Selection)
            {
                foreach (ToolStripButton btn in tsFigures.Items) btn.Checked = false;
                tsbArrow.Checked = true;
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            UpdateCanvasSize();
        }

        private void UpdateCanvasSize()
        {
            if (_editor != null)
            {
                var editrect = _editor.ClientRectangle;
                pbCanvas.Size = Size.Ceiling(editrect.Size);
                var rect = panelForScroll.ClientRectangle;
                if (pbCanvas.Width < rect.Width) pbCanvas.Width = rect.Width;
                if (pbCanvas.Height < rect.Height) pbCanvas.Height = rect.Height;
            }
        }

        private void tsmUndo_Click(object sender, EventArgs e)
        {
            _editor.UndoChanges();
        }

        private void tsmRedo_Click(object sender, EventArgs e)
        {
            _editor.RedoChanges();
        }

        private void tsbSelectMode_Click(object sender, EventArgs e)
        {
            foreach (ToolStripButton btn in tsFigures.Items) btn.Checked = false;
            ((ToolStripButton)sender).Checked = true;
            if (tsbPolyline.Checked)
                _editor.EditorMode = EditorMode.AddLine;
            else if (tsbPolygon.Checked)
                _editor.EditorMode = EditorMode.AddPolygon;
            else
                _editor.EditorMode = EditorMode.Selection;
        }

        private void tsmExit_Click(object sender, EventArgs e)
        {
            if (_editor.FileChanged &&
                MessageBox.Show(@"Есть не сохранённые данные! Закрыть программу?",
                                @"Редактор фигур",
                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button3) == DialogResult.Yes)
                Close();
        }


        private void tsmOpen_Click(object sender, EventArgs e)
        {
            openFiguresFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|All files (*.*)|*.*"; // Добавляем фильтр для файлов BMP
            if (_editor.FileChanged &&
                (!_editor.FileChanged || MessageBox.Show(@"Есть не сохранённые данные! Открыть новый файл?",
                                                         @"Редактор фигур",
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                                         MessageBoxDefaultButton.Button3) != DialogResult.Yes)) return;
            if (openFiguresFileDialog.ShowDialog(this) != DialogResult.OK) return;
            _editor.LoadFromFile(openFiguresFileDialog.FileName);
            Text = Title + @" - " + _editor.FileName;
        }

        private void tsmCreate_Click(object sender, EventArgs e)
        {
            if (_editor.FileChanged &&
                (!_editor.FileChanged || MessageBox.Show(@"Есть не сохранённые данные! Открыть новый файл?",
                                                         @"Редактор фигур",
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                                                         MessageBoxDefaultButton.Button3) != DialogResult.Yes)) return;
            _editor.Reset();
            cbWidth.SelectedIndex = 0;
            Text = Title;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = Title;
        }

        private void cbWidth_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            var cb = (ComboBox)sender;
            if (e.Index < cb.Height - 8)
                e.ItemHeight = cb.Height;
            else
                e.ItemHeight = e.Index + 8;
        }

        private void cbWidth_DrawItem(object sender, DrawItemEventArgs e)
        {
            var cb = (ComboBox)sender;
            var g = e.Graphics;
            // Draw the background of the item.
            e.DrawBackground();
            var rect = new Rectangle(e.Bounds.X, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
            try
            {
                rect.Inflate(-4, 0);
                using (var p = new Pen(e.ForeColor))
                {
                    p.Width = e.Index + 1;
                    g.DrawLine(p, new Point(rect.Left, rect.Top + rect.Height / 2),
                                  new Point(rect.Right, rect.Top + rect.Height / 2));
                    if (e.Index >= 9)
                    {
                        using (var textColor = new SolidBrush(e.BackColor))
                        {
                            rect.Offset(0, 2);
                            var showing = String.Format("{0} точек", cb.Items[e.Index]);
                            g.DrawString(showing, cb.Font, textColor, rect);
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        private void cbWidth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var cb = (ComboBox)sender;
            _editor.DefaultPenWidth = cb.SelectedIndex + 1;
            _editor.SetPenWidth(cb.SelectedIndex + 1);

        }

        private void tsmSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_editor.FileName))
            {
                if (saveFiguresFileDialog.ShowDialog(this) != DialogResult.OK) return;
                _editor.SaveToFile(saveFiguresFileDialog.FileName);
                Text = Title + @" - " + _editor.FileName;
            }
            else
                _editor.SaveToFile(_editor.FileName);
        }

        private void tsmSaveAs_Click(object sender, EventArgs e)
        {
            saveFiguresFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|All files (*.*)|*.*"; 
            if (saveFiguresFileDialog.ShowDialog(this) != DialogResult.OK) return;
            _editor.SaveToFile(saveFiguresFileDialog.FileName);
            Text = Title + @" - " + _editor.FileName;
        }

        private void tsmFileMenu_Click(object sender, EventArgs e)
        {

        }

        private void tsmPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
