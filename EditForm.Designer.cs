namespace piano
{
    partial class EditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            textBoxTact = new TextBox();
            labelTact = new Label();
            comboBoxSongs = new ComboBox();
            labelSelectSong = new Label();
            buttonEdit = new Button();
            buttonDelete = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(0, 0);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(576, 338);
            textBox1.TabIndex = 0;
            // 
            // textBoxTact
            // 
            textBoxTact.Location = new Point(476, 373);
            textBoxTact.Name = "textBoxTact";
            textBoxTact.Size = new Size(100, 23);
            textBoxTact.TabIndex = 1;
            // 
            // labelTact
            // 
            labelTact.AutoSize = true;
            labelTact.Location = new Point(438, 376);
            labelTact.Name = "labelTact";
            labelTact.Size = new Size(32, 15);
            labelTact.TabIndex = 2;
            labelTact.Text = "BPM";
            // 
            // comboBoxSongs
            // 
            comboBoxSongs.FormattingEnabled = true;
            comboBoxSongs.Location = new Point(582, 29);
            comboBoxSongs.Name = "comboBoxSongs";
            comboBoxSongs.Size = new Size(206, 23);
            comboBoxSongs.TabIndex = 3;
            comboBoxSongs.SelectedIndexChanged += comboBoxSongs_SelectedIndexChanged;
            // 
            // labelSelectSong
            // 
            labelSelectSong.AutoSize = true;
            labelSelectSong.Location = new Point(582, 9);
            labelSelectSong.Name = "labelSelectSong";
            labelSelectSong.Size = new Size(194, 15);
            labelSelectSong.TabIndex = 4;
            labelSelectSong.Text = "Выбор песни для редактирования";
            // 
            // buttonEdit
            // 
            buttonEdit.Location = new Point(12, 344);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(207, 68);
            buttonEdit.TabIndex = 5;
            buttonEdit.Text = "Сохранить изменения";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(238, 344);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(194, 68);
            buttonDelete.TabIndex = 6;
            buttonDelete.Text = "Удалить песню";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // EditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(labelSelectSong);
            Controls.Add(comboBoxSongs);
            Controls.Add(labelTact);
            Controls.Add(textBoxTact);
            Controls.Add(textBox1);
            Name = "EditForm";
            Text = "EditForm";
            Load += EditForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBoxTact;
        private Label labelTact;
        private ComboBox comboBoxSongs;
        private Label labelSelectSong;
        private Button buttonEdit;
        private Button buttonDelete;
    }
}