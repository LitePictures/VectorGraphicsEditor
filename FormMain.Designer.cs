﻿namespace SimpleVectorGraphicsEditor
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tsColors = new System.Windows.Forms.ToolStrip();
            this.tsbColors = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.panelForScroll = new System.Windows.Forms.Panel();
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.tsmFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripFile = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCut = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsFigures = new System.Windows.Forms.ToolStrip();
            this.tsbArrow = new System.Windows.Forms.ToolStripButton();
            this.tsbPolyline = new System.Windows.Forms.ToolStripButton();
            this.tsbPolygon = new System.Windows.Forms.ToolStripButton();
            this.cmsFigPopup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCutPopup = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyPopup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNodeSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.miBeginChangeNodes = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddFigureNode = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteFigureNode = new System.Windows.Forms.ToolStripMenuItem();
            this.miEndChangeNodes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.miStroke = new System.Windows.Forms.ToolStripMenuItem();
            this.miFill = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.miUngroupFigures = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.miBringToFront = new System.Windows.Forms.ToolStripMenuItem();
            this.miSendToBack = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTransforms = new System.Windows.Forms.ToolStripMenuItem();
            this.miTurnLeft90 = new System.Windows.Forms.ToolStripMenuItem();
            this.miTurnRight90 = new System.Windows.Forms.ToolStripMenuItem();
            this.miFlipVertical = new System.Windows.Forms.ToolStripMenuItem();
            this.miFlipHorizontal = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsBkgPopup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miPasteFromBuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.timerFormUpdate = new System.Windows.Forms.Timer(this.components);
            this.saveFiguresFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFiguresFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbWidth = new System.Windows.Forms.ComboBox();
            this.tsColors.SuspendLayout();
            this.panelForScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.menuStripMain.SuspendLayout();
            this.toolStripFile.SuspendLayout();
            this.tsFigures.SuspendLayout();
            this.cmsFigPopup.SuspendLayout();
            this.cmsBkgPopup.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsColors
            // 
            this.tsColors.Dock = System.Windows.Forms.DockStyle.None;
            this.tsColors.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsColors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbColors,
            this.toolStripSeparator5});
            this.tsColors.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tsColors.Location = new System.Drawing.Point(0, 0);
            this.tsColors.Name = "tsColors";
            this.tsColors.Size = new System.Drawing.Size(283, 39);
            this.tsColors.TabIndex = 4;
            // 
            // tsbColors
            // 
            this.tsbColors.BackColor = System.Drawing.Color.White;
            this.tsbColors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbColors.Image = ((System.Drawing.Image)(resources.GetObject("tsbColors.Image")));
            this.tsbColors.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbColors.ImageTransparentColor = System.Drawing.Color.White;
            this.tsbColors.Name = "tsbColors";
            this.tsbColors.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.tsbColors.Size = new System.Drawing.Size(264, 36);
            this.tsbColors.Text = "Выбор цвета";
            this.tsbColors.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbColors_MouseDown);
            this.tsbColors.Paint += new System.Windows.Forms.PaintEventHandler(this.tsbColors_Paint);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // statusStripMain
            // 
            this.statusStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripMain.Location = new System.Drawing.Point(0, 532);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStripMain.Size = new System.Drawing.Size(1067, 22);
            this.statusStripMain.TabIndex = 0;
            // 
            // panelForScroll
            // 
            this.panelForScroll.AutoScroll = true;
            this.panelForScroll.Controls.Add(this.pbCanvas);
            this.panelForScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelForScroll.Location = new System.Drawing.Point(4, 67);
            this.panelForScroll.Margin = new System.Windows.Forms.Padding(4);
            this.panelForScroll.Name = "panelForScroll";
            this.panelForScroll.Size = new System.Drawing.Size(1059, 389);
            this.panelForScroll.TabIndex = 7;
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackColor = System.Drawing.Color.White;
            this.pbCanvas.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbCanvas.BackgroundImage")));
            this.pbCanvas.Location = new System.Drawing.Point(0, 0);
            this.pbCanvas.Margin = new System.Windows.Forms.Padding(4);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(1101, 527);
            this.pbCanvas.TabIndex = 6;
            this.pbCanvas.TabStop = false;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmFileMenu,
            this.tsmEditMenu});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(141, 28);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // tsmFileMenu
            // 
            this.tsmFileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCreate,
            this.tsmOpen,
            this.toolStripSeparator,
            this.tsmSave,
            this.tsmSaveAs,
            this.toolStripSeparator1,
            this.tsmPrint,
            this.toolStripSeparator2,
            this.tsmExit});
            this.tsmFileMenu.Name = "tsmFileMenu";
            this.tsmFileMenu.Size = new System.Drawing.Size(59, 24);
            this.tsmFileMenu.Text = "&Файл";
            this.tsmFileMenu.Click += new System.EventHandler(this.tsmFileMenu_Click);
            // 
            // tsmCreate
            // 
            this.tsmCreate.Image = ((System.Drawing.Image)(resources.GetObject("tsmCreate.Image")));
            this.tsmCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmCreate.Name = "tsmCreate";
            this.tsmCreate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tsmCreate.Size = new System.Drawing.Size(226, 26);
            this.tsmCreate.Text = "&Создать";
            this.tsmCreate.Click += new System.EventHandler(this.tsmCreate_Click);
            // 
            // tsmOpen
            // 
            this.tsmOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsmOpen.Image")));
            this.tsmOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmOpen.Name = "tsmOpen";
            this.tsmOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsmOpen.Size = new System.Drawing.Size(226, 26);
            this.tsmOpen.Text = "&Открыть";
            this.tsmOpen.Click += new System.EventHandler(this.tsmOpen_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmSave
            // 
            this.tsmSave.Enabled = false;
            this.tsmSave.Image = ((System.Drawing.Image)(resources.GetObject("tsmSave.Image")));
            this.tsmSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmSave.Name = "tsmSave";
            this.tsmSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsmSave.Size = new System.Drawing.Size(226, 26);
            this.tsmSave.Text = "&Сохранить";
            this.tsmSave.Click += new System.EventHandler(this.tsmSave_Click);
            // 
            // tsmSaveAs
            // 
            this.tsmSaveAs.Name = "tsmSaveAs";
            this.tsmSaveAs.Size = new System.Drawing.Size(226, 26);
            this.tsmSaveAs.Text = "Сохранить &как";
            this.tsmSaveAs.Click += new System.EventHandler(this.tsmSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmPrint
            // 
            this.tsmPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsmPrint.Image")));
            this.tsmPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmPrint.Name = "tsmPrint";
            this.tsmPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tsmPrint.Size = new System.Drawing.Size(226, 26);
            this.tsmPrint.Text = "&Печать";
            this.tsmPrint.Click += new System.EventHandler(this.tsmPrint_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(223, 6);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(226, 26);
            this.tsmExit.Text = "Вы&ход";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // tsmEditMenu
            // 
            this.tsmEditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmUndo,
            this.tsmRedo,
            this.toolStripSeparator3,
            this.tsmCut,
            this.tsmCopy,
            this.tsmPaste,
            this.toolStripSeparator4,
            this.tsmSelectAll});
            this.tsmEditMenu.Name = "tsmEditMenu";
            this.tsmEditMenu.Size = new System.Drawing.Size(74, 24);
            this.tsmEditMenu.Text = "&Правка";
            this.tsmEditMenu.DropDownOpening += new System.EventHandler(this.tsmEditMenu_DropDownOpening);
            // 
            // tsmUndo
            // 
            this.tsmUndo.Enabled = false;
            this.tsmUndo.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.undo;
            this.tsmUndo.Name = "tsmUndo";
            this.tsmUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsmUndo.Size = new System.Drawing.Size(273, 26);
            this.tsmUndo.Text = "&Отмена действия";
            this.tsmUndo.Click += new System.EventHandler(this.tsmUndo_Click);
            // 
            // tsmRedo
            // 
            this.tsmRedo.Enabled = false;
            this.tsmRedo.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.redo;
            this.tsmRedo.Name = "tsmRedo";
            this.tsmRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.tsmRedo.Size = new System.Drawing.Size(273, 26);
            this.tsmRedo.Text = "&Отмена отмены";
            this.tsmRedo.Click += new System.EventHandler(this.tsmRedo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(270, 6);
            // 
            // tsmCut
            // 
            this.tsmCut.Enabled = false;
            this.tsmCut.Image = ((System.Drawing.Image)(resources.GetObject("tsmCut.Image")));
            this.tsmCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmCut.Name = "tsmCut";
            this.tsmCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmCut.Size = new System.Drawing.Size(273, 26);
            this.tsmCut.Text = "Вырезат&ь";
            this.tsmCut.Click += new System.EventHandler(this.miCutPopup_Click);
            // 
            // tsmCopy
            // 
            this.tsmCopy.Enabled = false;
            this.tsmCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsmCopy.Image")));
            this.tsmCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmCopy.Name = "tsmCopy";
            this.tsmCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmCopy.Size = new System.Drawing.Size(273, 26);
            this.tsmCopy.Text = "&Копировать";
            this.tsmCopy.Click += new System.EventHandler(this.miCopyPopup_Click);
            // 
            // tsmPaste
            // 
            this.tsmPaste.Enabled = false;
            this.tsmPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsmPaste.Image")));
            this.tsmPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmPaste.Name = "tsmPaste";
            this.tsmPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsmPaste.Size = new System.Drawing.Size(273, 26);
            this.tsmPaste.Text = "Вст&авка";
            this.tsmPaste.Click += new System.EventHandler(this.miPasteFromBuffer_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(270, 6);
            // 
            // tsmSelectAll
            // 
            this.tsmSelectAll.Enabled = false;
            this.tsmSelectAll.Name = "tsmSelectAll";
            this.tsmSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsmSelectAll.Size = new System.Drawing.Size(273, 26);
            this.tsmSelectAll.Text = "Выделить &все";
            this.tsmSelectAll.Click += new System.EventHandler(this.tsmSelectAll_Click);
            // 
            // toolStripFile
            // 
            this.toolStripFile.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripFile.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbSave,
            this.tsbPrint,
            this.toolStripSeparator6,
            this.tsbCut,
            this.tsbCopy,
            this.tsbPaste,
            this.toolStripSeparator7,
            this.tsbUndo,
            this.tsbRedo,
            this.toolStripSeparator9});
            this.toolStripFile.Location = new System.Drawing.Point(0, 0);
            this.toolStripFile.Name = "toolStripFile";
            this.toolStripFile.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.toolStripFile.Size = new System.Drawing.Size(298, 27);
            this.toolStripFile.TabIndex = 1;
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(29, 24);
            this.tsbNew.Text = "&Создать";
            this.tsbNew.Click += new System.EventHandler(this.tsmCreate_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(29, 24);
            this.tsbOpen.Text = "&Открыть";
            this.tsbOpen.Click += new System.EventHandler(this.tsmOpen_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Enabled = false;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(29, 24);
            this.tsbSave.Text = "&Сохранить";
            this.tsbSave.Click += new System.EventHandler(this.tsmSave_Click);
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Enabled = false;
            this.tsbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrint.Image")));
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(29, 24);
            this.tsbPrint.Text = "&Печать";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbCut
            // 
            this.tsbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCut.Enabled = false;
            this.tsbCut.Image = ((System.Drawing.Image)(resources.GetObject("tsbCut.Image")));
            this.tsbCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCut.Name = "tsbCut";
            this.tsbCut.Size = new System.Drawing.Size(29, 24);
            this.tsbCut.Text = "В&ырезать";
            this.tsbCut.Click += new System.EventHandler(this.miCutPopup_Click);
            // 
            // tsbCopy
            // 
            this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCopy.Enabled = false;
            this.tsbCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopy.Image")));
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(29, 24);
            this.tsbCopy.Text = "&Копировать";
            this.tsbCopy.Click += new System.EventHandler(this.miCopyPopup_Click);
            // 
            // tsbPaste
            // 
            this.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPaste.Enabled = false;
            this.tsbPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsbPaste.Image")));
            this.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPaste.Name = "tsbPaste";
            this.tsbPaste.Size = new System.Drawing.Size(29, 24);
            this.tsbPaste.Text = "Вст&авка";
            this.tsbPaste.Click += new System.EventHandler(this.miPasteFromBuffer_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUndo.Enabled = false;
            this.tsbUndo.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.undo;
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(29, 24);
            this.tsbUndo.Text = "Отменить";
            this.tsbUndo.Click += new System.EventHandler(this.tsmUndo_Click);
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRedo.Enabled = false;
            this.tsbRedo.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.redo;
            this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new System.Drawing.Size(29, 24);
            this.tsbRedo.Text = "Вернуть";
            this.tsbRedo.Click += new System.EventHandler(this.tsmRedo_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 27);
            // 
            // tsFigures
            // 
            this.tsFigures.Dock = System.Windows.Forms.DockStyle.None;
            this.tsFigures.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tsFigures.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbArrow,
            this.tsbPolyline,
            this.tsbPolygon});
            this.tsFigures.Location = new System.Drawing.Point(298, 0);
            this.tsFigures.Name = "tsFigures";
            this.tsFigures.Size = new System.Drawing.Size(139, 27);
            this.tsFigures.TabIndex = 2;
            // 
            // tsbArrow
            // 
            this.tsbArrow.Checked = true;
            this.tsbArrow.CheckOnClick = true;
            this.tsbArrow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbArrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArrow.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.arrow;
            this.tsbArrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArrow.Name = "tsbArrow";
            this.tsbArrow.Size = new System.Drawing.Size(29, 24);
            this.tsbArrow.Text = "Выбор фигур";
            this.tsbArrow.Click += new System.EventHandler(this.tsbSelectMode_Click);
            // 
            // tsbPolyline
            // 
            this.tsbPolyline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPolyline.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.poliline;
            this.tsbPolyline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPolyline.Name = "tsbPolyline";
            this.tsbPolyline.Size = new System.Drawing.Size(29, 24);
            this.tsbPolyline.Text = "Линия";
            this.tsbPolyline.ToolTipText = "Создать линию";
            this.tsbPolyline.Click += new System.EventHandler(this.tsbSelectMode_Click);
            // 
            // tsbPolygon
            // 
            this.tsbPolygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPolygon.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.poligon;
            this.tsbPolygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPolygon.Name = "tsbPolygon";
            this.tsbPolygon.Size = new System.Drawing.Size(29, 24);
            this.tsbPolygon.Text = "Многоугольник";
            this.tsbPolygon.ToolTipText = "Создать полигон";
            this.tsbPolygon.Click += new System.EventHandler(this.tsbSelectMode_Click);
            // 
            // cmsFigPopup
            // 
            this.cmsFigPopup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmsFigPopup.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsFigPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCutPopup,
            this.miCopyPopup,
            this.tsmiNodeSeparator,
            this.miBeginChangeNodes,
            this.miAddFigureNode,
            this.miDeleteFigureNode,
            this.miEndChangeNodes,
            this.toolStripMenuItem4,
            this.miStroke,
            this.miFill,
            this.toolStripMenuItem3,
            this.toolStripMenuItem6,
            this.miUngroupFigures,
            this.toolStripMenuItem7,
            this.miBringToFront,
            this.miSendToBack,
            this.toolStripMenuItem5,
            this.tsmiTransforms});
            this.cmsFigPopup.Name = "cmsFigPopup";
            this.cmsFigPopup.Size = new System.Drawing.Size(279, 372);
            this.cmsFigPopup.Opening += new System.ComponentModel.CancelEventHandler(this.cmsFigPopup_Opening);
            // 
            // miCutPopup
            // 
            this.miCutPopup.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.cut;
            this.miCutPopup.Name = "miCutPopup";
            this.miCutPopup.Size = new System.Drawing.Size(278, 26);
            this.miCutPopup.Text = "Вырезать";
            this.miCutPopup.Click += new System.EventHandler(this.miCutPopup_Click);
            // 
            // miCopyPopup
            // 
            this.miCopyPopup.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.copy;
            this.miCopyPopup.Name = "miCopyPopup";
            this.miCopyPopup.Size = new System.Drawing.Size(278, 26);
            this.miCopyPopup.Text = "Копировать";
            this.miCopyPopup.Click += new System.EventHandler(this.miCopyPopup_Click);
            // 
            // tsmiNodeSeparator
            // 
            this.tsmiNodeSeparator.Name = "tsmiNodeSeparator";
            this.tsmiNodeSeparator.Size = new System.Drawing.Size(275, 6);
            // 
            // miBeginChangeNodes
            // 
            this.miBeginChangeNodes.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.startnodechanging;
            this.miBeginChangeNodes.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.miBeginChangeNodes.Name = "miBeginChangeNodes";
            this.miBeginChangeNodes.Size = new System.Drawing.Size(278, 26);
            this.miBeginChangeNodes.Text = "Начать изменение узлов";
            this.miBeginChangeNodes.Visible = false;
            this.miBeginChangeNodes.Click += new System.EventHandler(this.miBeginChangeNodes_Click);
            // 
            // miAddFigureNode
            // 
            this.miAddFigureNode.Name = "miAddFigureNode";
            this.miAddFigureNode.Size = new System.Drawing.Size(278, 26);
            this.miAddFigureNode.Text = "Добавить узел";
            this.miAddFigureNode.Click += new System.EventHandler(this.miAddFigureNode_Click);
            // 
            // miDeleteFigureNode
            // 
            this.miDeleteFigureNode.Name = "miDeleteFigureNode";
            this.miDeleteFigureNode.Size = new System.Drawing.Size(278, 26);
            this.miDeleteFigureNode.Text = "Удалить узел";
            this.miDeleteFigureNode.Click += new System.EventHandler(this.miDeleteFigureNode_Click);
            // 
            // miEndChangeNodes
            // 
            this.miEndChangeNodes.Name = "miEndChangeNodes";
            this.miEndChangeNodes.Size = new System.Drawing.Size(278, 26);
            this.miEndChangeNodes.Text = "Завершить изменение узлов";
            this.miEndChangeNodes.Visible = false;
            this.miEndChangeNodes.Click += new System.EventHandler(this.miEndChangeNodes_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(275, 6);
            this.toolStripMenuItem4.Visible = false;
            // 
            // miStroke
            // 
            this.miStroke.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.pen;
            this.miStroke.Name = "miStroke";
            this.miStroke.Size = new System.Drawing.Size(278, 26);
            this.miStroke.Text = "Карандаш...";
            this.miStroke.Visible = false;
            // 
            // miFill
            // 
            this.miFill.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.brush;
            this.miFill.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.miFill.Name = "miFill";
            this.miFill.Size = new System.Drawing.Size(278, 26);
            this.miFill.Text = "Кисть...";
            this.miFill.Visible = false;
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(275, 6);
            this.toolStripMenuItem3.Visible = false;
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Enabled = false;
            this.toolStripMenuItem6.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.grouping;
            this.toolStripMenuItem6.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(278, 26);
            this.toolStripMenuItem6.Text = "Группировать";
            this.toolStripMenuItem6.Visible = false;
            // 
            // miUngroupFigures
            // 
            this.miUngroupFigures.Enabled = false;
            this.miUngroupFigures.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.ungrouping;
            this.miUngroupFigures.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.miUngroupFigures.Name = "miUngroupFigures";
            this.miUngroupFigures.Size = new System.Drawing.Size(278, 26);
            this.miUngroupFigures.Text = "Разруппировать";
            this.miUngroupFigures.Visible = false;
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(275, 6);
            // 
            // miBringToFront
            // 
            this.miBringToFront.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.bringtofront;
            this.miBringToFront.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.miBringToFront.Name = "miBringToFront";
            this.miBringToFront.Size = new System.Drawing.Size(278, 26);
            this.miBringToFront.Text = "Выдвинуть вперёд";
            this.miBringToFront.Click += new System.EventHandler(this.miBringToFront_Click);
            // 
            // miSendToBack
            // 
            this.miSendToBack.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.sendtoback;
            this.miSendToBack.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.miSendToBack.Name = "miSendToBack";
            this.miSendToBack.Size = new System.Drawing.Size(278, 26);
            this.miSendToBack.Text = "Поместить назад";
            this.miSendToBack.Click += new System.EventHandler(this.miSendToBack_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(275, 6);
            // 
            // tsmiTransforms
            // 
            this.tsmiTransforms.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTurnLeft90,
            this.miTurnRight90,
            this.miFlipVertical,
            this.miFlipHorizontal});
            this.tsmiTransforms.Name = "tsmiTransforms";
            this.tsmiTransforms.Size = new System.Drawing.Size(278, 26);
            this.tsmiTransforms.Text = "Трансформации";
            // 
            // miTurnLeft90
            // 
            this.miTurnLeft90.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.rotateleft;
            this.miTurnLeft90.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.miTurnLeft90.Name = "miTurnLeft90";
            this.miTurnLeft90.Size = new System.Drawing.Size(261, 26);
            this.miTurnLeft90.Text = "Повернуть влево";
            this.miTurnLeft90.Click += new System.EventHandler(this.miTurnLeft90_Click);
            // 
            // miTurnRight90
            // 
            this.miTurnRight90.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.rotateright;
            this.miTurnRight90.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.miTurnRight90.Name = "miTurnRight90";
            this.miTurnRight90.Size = new System.Drawing.Size(261, 26);
            this.miTurnRight90.Text = "Повернуть вправо";
            this.miTurnRight90.Click += new System.EventHandler(this.miTurnRight90_Click);
            // 
            // miFlipVertical
            // 
            this.miFlipVertical.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.flipleftright;
            this.miFlipVertical.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.miFlipVertical.Name = "miFlipVertical";
            this.miFlipVertical.Size = new System.Drawing.Size(261, 26);
            this.miFlipVertical.Text = "Отразить слева направо";
            this.miFlipVertical.Click += new System.EventHandler(this.miFlipVertical_Click);
            // 
            // miFlipHorizontal
            // 
            this.miFlipHorizontal.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.flipupdown;
            this.miFlipHorizontal.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.miFlipHorizontal.Name = "miFlipHorizontal";
            this.miFlipHorizontal.Size = new System.Drawing.Size(261, 26);
            this.miFlipHorizontal.Text = "Отразить сверху вниз";
            this.miFlipHorizontal.Click += new System.EventHandler(this.miFlipHorizontal_Click);
            // 
            // cmsBkgPopup
            // 
            this.cmsBkgPopup.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsBkgPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPasteFromBuffer});
            this.cmsBkgPopup.Name = "cmsBkgPopup";
            this.cmsBkgPopup.Size = new System.Drawing.Size(144, 30);
            this.cmsBkgPopup.Opening += new System.ComponentModel.CancelEventHandler(this.cmsBkgPopup_Opening);
            // 
            // miPasteFromBuffer
            // 
            this.miPasteFromBuffer.Enabled = false;
            this.miPasteFromBuffer.Image = global::SimpleVectorGraphicsEditor.Properties.Resources.paste;
            this.miPasteFromBuffer.Name = "miPasteFromBuffer";
            this.miPasteFromBuffer.Size = new System.Drawing.Size(143, 26);
            this.miPasteFromBuffer.Text = "Вставить";
            this.miPasteFromBuffer.Click += new System.EventHandler(this.miPasteFromBuffer_Click);
            // 
            // timerFormUpdate
            // 
            this.timerFormUpdate.Enabled = true;
            this.timerFormUpdate.Interval = 300;
            this.timerFormUpdate.Tick += new System.EventHandler(this.timerFormUpdate_Tick);
            // 
            // saveFiguresFileDialog
            // 
            this.saveFiguresFileDialog.DefaultExt = "pic";
            this.saveFiguresFileDialog.FileName = "example";
            this.saveFiguresFileDialog.Filter = "*.pic|*.pic";
            // 
            // openFiguresFileDialog
            // 
            this.openFiguresFileDialog.DefaultExt = "pic";
            this.openFiguresFileDialog.FileName = "example";
            this.openFiguresFileDialog.Filter = "*.pic|*.pic";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelForScroll, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.menuStripMain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStripMain, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1067, 554);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.toolStripFile);
            this.flowLayoutPanel1.Controls.Add(this.tsFigures);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 32);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(437, 27);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.tsColors);
            this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(4, 464);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1059, 64);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label1);
            this.flowLayoutPanel3.Controls.Add(this.cbWidth);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(287, 4);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(267, 44);
            this.flowLayoutPanel3.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Толщина линии:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbWidth
            // 
            this.cbWidth.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWidth.FormattingEnabled = true;
            this.cbWidth.Location = new System.Drawing.Point(123, 4);
            this.cbWidth.Margin = new System.Windows.Forms.Padding(4);
            this.cbWidth.Name = "cbWidth";
            this.cbWidth.Size = new System.Drawing.Size(115, 23);
            this.cbWidth.TabIndex = 5;
            this.cbWidth.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbWidth_DrawItem);
            this.cbWidth.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cbWidth_MeasureItem);
            this.cbWidth.SelectionChangeCommitted += new System.EventHandler(this.cbWidth_SelectionChangeCommitted);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStripMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Простой векторный графический редактор (демо)";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.tsColors.ResumeLayout(false);
            this.tsColors.PerformLayout();
            this.panelForScroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripFile.ResumeLayout(false);
            this.toolStripFile.PerformLayout();
            this.tsFigures.ResumeLayout(false);
            this.tsFigures.PerformLayout();
            this.cmsFigPopup.ResumeLayout(false);
            this.cmsBkgPopup.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem tsmFileMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmCreate;
        private System.Windows.Forms.ToolStripMenuItem tsmOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem tsmSave;
        private System.Windows.Forms.ToolStripMenuItem tsmSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmEditMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmCut;
        private System.Windows.Forms.ToolStripMenuItem tsmCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmSelectAll;
        private System.Windows.Forms.ToolStrip toolStripFile;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsbCut;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ContextMenuStrip cmsFigPopup;
        private System.Windows.Forms.ToolStripMenuItem miCutPopup;
        private System.Windows.Forms.ToolStripMenuItem miCopyPopup;
        private System.Windows.Forms.ToolStripSeparator tsmiNodeSeparator;
        private System.Windows.Forms.ToolStripMenuItem miBeginChangeNodes;
        private System.Windows.Forms.ToolStripMenuItem miEndChangeNodes;
        private System.Windows.Forms.ToolStripMenuItem miAddFigureNode;
        private System.Windows.Forms.ToolStripMenuItem miDeleteFigureNode;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ContextMenuStrip cmsBkgPopup;
        private System.Windows.Forms.ToolStripMenuItem miPasteFromBuffer;
        private System.Windows.Forms.ToolStrip tsColors;
        private System.Windows.Forms.ToolStripButton tsbColors;
        private System.Windows.Forms.ToolStripButton tsbUndo;
        private System.Windows.Forms.ToolStripButton tsbRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem miStroke;
        private System.Windows.Forms.ToolStripMenuItem miFill;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem miUngroupFigures;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem miBringToFront;
        private System.Windows.Forms.ToolStripMenuItem miSendToBack;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem tsmiTransforms;
        private System.Windows.Forms.ToolStripMenuItem miTurnLeft90;
        private System.Windows.Forms.ToolStripMenuItem miTurnRight90;
        private System.Windows.Forms.ToolStripMenuItem miFlipVertical;
        private System.Windows.Forms.ToolStripMenuItem miFlipHorizontal;
        private System.Windows.Forms.Timer timerFormUpdate;
        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.Panel panelForScroll;
        private System.Windows.Forms.ToolStripButton tsbArrow;
        private System.Windows.Forms.ToolStripButton tsbPolyline;
        private System.Windows.Forms.ToolStripButton tsbPolygon;
        private System.Windows.Forms.ToolStrip tsFigures;
        private System.Windows.Forms.SaveFileDialog saveFiguresFileDialog;
        private System.Windows.Forms.OpenFileDialog openFiguresFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.ComboBox cbWidth;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label1;
    }
}

