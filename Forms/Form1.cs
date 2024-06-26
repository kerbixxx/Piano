using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Workspace;
using piano.SongLogic;

namespace piano
{
    public partial class Form1 : Form
    {
        private SaveForm saveFormInstance;
        CancellationTokenSource _tokenSource = null;
        CancellationToken token;
        string selectedWindow = null;
        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public PauseTokenSource pauseTokenSource = new PauseTokenSource();

        private bool isPaused = false;

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
            IntPtr calcWindow = FindWindow(null, selectedWindow);
            if (SetForegroundWindow(calcWindow))
            {
                Player player = new Player(textBox1.Text, Convert.ToInt16(textBoxTact.Text));
                _tokenSource = new CancellationTokenSource();
                pauseTokenSource = new PauseTokenSource();
                isPaused = false;
                token = _tokenSource.Token;
                var pauseToken = pauseTokenSource.Token;
                try
                {
                    await Task.Run(() => player.PlaySong(token, pauseToken), token);
                }
                finally
                {
                    _tokenSource.Dispose();
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (_tokenSource != null)
            {
                Stop(_tokenSource);
            }
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

            HotKey hotKeyPause = new HotKey();
            hotKeyPause.Key = Keys.F8;
            hotKeyPause.HotKeyPressed += HotKeyPause_HotKeyPressed;

            using (var db = new SongDb())
            {
                List<Song> list = db.Songs.ToList();
                comboBoxSongs.DataSource = list;
                comboBoxSongs.DisplayMember = "SongName";
            }
        }

        private void HotKeyPause_HotKeyPressed(object? sender, KeyEventArgs e)
        {
            Pause(_tokenSource);
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
            try
            {
                _tokenSource?.Cancel();
            }
            catch (Exception ex)
            {

            }
        }

        public void Pause(CancellationTokenSource _tokenSource)
        {
            if (isPaused)
            {
                pauseTokenSource.Resume();
                isPaused = false;
            }
            else
            {
                pauseTokenSource.Pause();
                isPaused = true;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Song song = new Song(textBox1.Text, Convert.ToInt16(textBoxTact.Text));
            saveFormInstance = new SaveForm(song);
            saveFormInstance.OnClose += SaveForm_OnClose;
            saveFormInstance.ShowDialog();
        }
        private void SaveForm_OnClose(object sender, EventArgs e)
        {
            UpdateComboBox(sender, e);
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

        private void UpdateComboBox(object sender, EventArgs e)
        {
            using (var db = new SongDb())
            {
                List<Song> list = db.Songs.ToList();
                comboBoxSongs.DataSource = list;
                comboBoxSongs.DisplayMember = "SongName";
            }
        }

        public List<string> GetWindows()
        {

            List<string> windowList = new List<string>();
            foreach (var process in Process.GetProcesses().Where(p => p.MainWindowHandle != IntPtr.Zero && p.ProcessName != "explorer"))
            {
                if (process.MainWindowTitle == "")
                {
                    continue;
                }
                windowList.Add(process.MainWindowTitle);
            }
            return windowList;
        }

        private void comboBoxSelectWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectWindows.SelectedIndex != -1)
            {
                selectedWindow = comboBoxSelectWindows.SelectedItem as string;
            }
        }

        private void buttonUpdateWindowsList_Click(object sender, EventArgs e)
        {
            List<string> objList = GetWindows();
            comboBoxSelectWindows.DataSource = objList;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Form form = new EditForm();
            form.ShowDialog();
        }
    }
}