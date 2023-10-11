namespace piano
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            buttonStart = new Button();
            buttonStop = new Button();
            textBoxTact = new TextBox();
            label1 = new Label();
            comboBoxSongs = new ComboBox();
            label3 = new Label();
            buttonSave = new Button();
            button1 = new Button();
            comboBoxSelectWindows = new ComboBox();
            labelSelectWindow = new Label();
            buttonUpdateWindowsList = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(0, 0);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(788, 343);
            textBox1.TabIndex = 0;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(12, 361);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(190, 67);
            buttonStart.TabIndex = 1;
            buttonStart.Text = "Начать (F6)";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(208, 361);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(190, 67);
            buttonStop.TabIndex = 2;
            buttonStop.Text = "Стоп (F7)";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;
            // 
            // textBoxTact
            // 
            textBoxTact.Location = new Point(707, 384);
            textBoxTact.Name = "textBoxTact";
            textBoxTact.Size = new Size(81, 23);
            textBoxTact.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(600, 387);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 5;
            label1.Text = "Скорость такта";
            // 
            // comboBoxSongs
            // 
            comboBoxSongs.FormattingEnabled = true;
            comboBoxSongs.Location = new Point(817, 33);
            comboBoxSongs.Name = "comboBoxSongs";
            comboBoxSongs.Size = new Size(237, 23);
            comboBoxSongs.TabIndex = 7;
            comboBoxSongs.SelectedIndexChanged += comboBoxSongs_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(817, 9);
            label3.Name = "label3";
            label3.Size = new Size(134, 15);
            label3.TabIndex = 8;
            label3.Text = "Загруженные мелодии";
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(404, 362);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(190, 65);
            buttonSave.TabIndex = 9;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // button1
            // 
            button1.Location = new Point(817, 62);
            button1.Name = "button1";
            button1.Size = new Size(237, 23);
            button1.TabIndex = 10;
            button1.Text = "Обновить список (после сохранения)";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBoxSelectWindows
            // 
            comboBoxSelectWindows.FormattingEnabled = true;
            comboBoxSelectWindows.Location = new Point(817, 121);
            comboBoxSelectWindows.Name = "comboBoxSelectWindows";
            comboBoxSelectWindows.Size = new Size(237, 23);
            comboBoxSelectWindows.TabIndex = 11;
            comboBoxSelectWindows.SelectedIndexChanged += comboBoxSelectWindows_SelectedIndexChanged;
            // 
            // labelSelectWindow
            // 
            labelSelectWindow.AutoSize = true;
            labelSelectWindow.Location = new Point(817, 103);
            labelSelectWindow.Name = "labelSelectWindow";
            labelSelectWindow.Size = new Size(179, 15);
            labelSelectWindow.TabIndex = 12;
            labelSelectWindow.Text = "Выбор окна для проигрывания";
            // 
            // buttonUpdateWindowsList
            // 
            buttonUpdateWindowsList.Location = new Point(817, 150);
            buttonUpdateWindowsList.Name = "buttonUpdateWindowsList";
            buttonUpdateWindowsList.Size = new Size(237, 23);
            buttonUpdateWindowsList.TabIndex = 13;
            buttonUpdateWindowsList.Text = "Обновить список открытых окон";
            buttonUpdateWindowsList.UseVisualStyleBackColor = true;
            buttonUpdateWindowsList.Click += buttonUpdateWindowsList_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1075, 529);
            Controls.Add(buttonUpdateWindowsList);
            Controls.Add(labelSelectWindow);
            Controls.Add(comboBoxSelectWindows);
            Controls.Add(button1);
            Controls.Add(buttonSave);
            Controls.Add(label3);
            Controls.Add(comboBoxSongs);
            Controls.Add(label1);
            Controls.Add(textBoxTact);
            Controls.Add(buttonStop);
            Controls.Add(buttonStart);
            Controls.Add(textBox1);
            KeyPreview = true;
            Name = "Form1";
            Text = "Piano Simulator";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button buttonStart;
        private Button buttonStop;
        private TextBox textBoxTact;
        private Label label1;
        private ComboBox comboBoxSongs;
        private Label label3;
        private Button buttonSave;
        private Button button1;
        private ComboBox comboBoxSelectWindows;
        private Label labelSelectWindow;
        private Button buttonUpdateWindowsList;
    }
}