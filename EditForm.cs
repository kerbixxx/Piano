using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace piano
{
    public partial class EditForm : Form
    {
        public EditForm()
        {
            InitializeComponent();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            using (var db = new SongDb())
            {
                List<Song> list = db.Songs.ToList();
                comboBoxSongs.DataSource = list;
                comboBoxSongs.DisplayMember = "SongName";
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Song song = comboBoxSongs.SelectedItem as Song;
            using (var db = new SongDb())
            {
                if (song != null)
                {
                    song.text = textBox1.Text;
                    song.tact = Convert.ToInt32(textBoxTact.Text);
                    db.Update(song);
                    db.SaveChanges();
                }
            }
            UpdateSongs();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Song song = comboBoxSongs.SelectedItem as Song;
            using (var db =new SongDb())
            {
                if(song!= null)
                {
                    db.Remove(song);
                    db.SaveChanges();
                }
            }
            UpdateSongs();
        }

        private void comboBoxSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSongs.SelectedIndex != -1)
            {
                Song song = comboBoxSongs.SelectedItem as Song;
                textBox1.Text = song.text;
                textBoxTact.Text = song.tact.ToString();
            }
        }
        public void UpdateSongs()
        {
            using (var db = new SongDb())
            {
                List<Song> list = db.Songs.ToList();
                comboBoxSongs.DataSource = list;
                comboBoxSongs.DisplayMember = "SongName";
            }
        }
    }
}
