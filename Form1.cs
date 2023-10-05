using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
namespace piano
{
    public partial class Form1 : Form
    {
        CancellationTokenSource _tokenSource = null;

        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            using (var db = new SongDb())
            {
                db.Database.Migrate();
            }
            InitializeComponent();
            this.KeyPreview = true;
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            IntPtr calcWindow = FindWindow(null, "Roblox");
            if (SetForegroundWindow(calcWindow))
            {
                Player player = new Player(textBox1.Text, Convert.ToInt16(textBoxTact.Text));
                _tokenSource = new CancellationTokenSource();
                var token = _tokenSource.Token;
                try
                {
                    await Task.Run(() => player.PlaySong(token));
                }
                finally
                {
                    _tokenSource.Dispose();
                }
            }
            textBox1.Focus();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Stop(_tokenSource);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxTact.Text = "200";
            HotKey hotKeyStop = new HotKey();
            hotKeyStop.Key = Keys.F7;
            hotKeyStop.HotKeyPressed += HotkeyStop_HotKeyPressed;

            HotKey hotKeyStart = new HotKey();
            hotKeyStart.Key = Keys.F6;
            hotKeyStart.HotKeyPressed += HotKeyStart_HotKeyPressed;

            using (var db = new SongDb())
            {
                List<Song> list = db.Songs.ToList();
                comboBoxSongs.DataSource = list;
                comboBoxSongs.DisplayMember = "SongName";
            }
        }
        private void HotKeyStart_HotKeyPressed(object? sender, KeyEventArgs e)
        {
            buttonStart_Click(sender, e);
        }

        private void HotkeyStop_HotKeyPressed(object? sender, KeyEventArgs e)
        {
            Stop(_tokenSource);
        }

        public void Stop(CancellationTokenSource _tokenSource)
        {
            _tokenSource?.Cancel();
        }

        public void Pause(CancellationTokenSource _tokenSource)
        {
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Song song = new Song(textBox1.Text, Convert.ToInt16(textBoxTact.Text));
            Form SaveForm = new SaveForm(song);
            SaveForm.ShowDialog();
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

        private void button1_Click(object sender, EventArgs e)
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