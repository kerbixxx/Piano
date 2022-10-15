using System.Text.RegularExpressions;
using System.Globalization;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace piano
{
    public partial class Form1 : Form
    {
        CancellationTokenSource _tokenSource = null;

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            IntPtr calcWindow = FindWindow(null, "Form1");
            if (SetForegroundWindow(calcWindow))
            {

                Player player = new(textBox1.Text,Convert.ToInt16(textBoxTact.Text));
                _tokenSource = new CancellationTokenSource();
                var token = _tokenSource.Token;

                try
                {
                    await Task.Run(() => player.PlaySong(token));
                }
                catch (OperationCanceledException)
                {
                    textBox2.Text = "Cancelled";
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
            _tokenSource.Cancel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxTact.Text = "20";
        }
    }
}