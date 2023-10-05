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
    public partial class SaveForm : Form
    {
        Song song;
        public SaveForm(Song song)
        {
            InitializeComponent();
            this.song = song;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            song.SongName = textBoxSongName.Text;
            using(var db = new SongDb())
            {
                if (song.SongName != null && song.tact != null && song.text != null)
                {
                    db.Songs.Add(song);
                    db.SaveChanges();
                }
            }
            Close();
        }
    }
}
