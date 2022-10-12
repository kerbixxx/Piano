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

        Thread myThread1 = new Thread(PlaySong);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            IntPtr calcWindow = FindWindow(null, "Form1");
            if (SetForegroundWindow(calcWindow))
            {
                TextTact tecttact = new TextTact(textBox1.Text, Convert.ToInt32(textBox2.Text));
                myThread1.Start();
            }
        }
        void PlaySong(object texttact)
        {
            TextTact textTact = (TextTact)texttact;
            int tact = textTact.tact;
            Thread.Sleep(4000);
            string text = textTact.text;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '[')
                {
                    i++;
                    string buffer = text[i].ToString();
                    i++;
                    while (text[i] != ']')
                    {
                        buffer = buffer + text[i];
                        i++;
                    }
                    if (text[i] == ']')
                    {
                        for (int j = 0; j < buffer.Length; j++)
                        {
                            ConvertCharToVirtualKey(buffer[j]);
                        }
                        Thread.Sleep(tact*10);
                        continue;
                    }
                }
                if (text[i] == ' ')
                {
                    //PresslowKey(Keys.Space); Прикол для гонок на клавиатуре
                    Thread.Sleep(tact*10);
                    continue;
                }
                if (text[i] == '|')
                {
                    Thread.Sleep(tact*20);
                    continue;
                }
                ConvertCharToVirtualKey(text[i]);
                Thread.Sleep(Convert.ToInt32(tact)); //20
            }
        }

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
                    byte bVk,
                    byte bScan,
                                int dwFlags, // Здесь целочисленный тип нажимается 0, отпускается 2
                                int dwExtraInfo // Это целочисленный тип. Обычно устанавливается в 0
                );
        void PressKey(Keys key)
        {
            keybd_event((byte)key, 0, 0, 0);
            keybd_event((byte)key, 0, 2, 0);
        }

        void PressHighKey(Keys key)
        {
            keybd_event((byte)Keys.ShiftKey, 0, 0, 0);
            PressKey(key);
            keybd_event((byte)Keys.ShiftKey, 0, 2, 0);
        }

        void ConvertCharToVirtualKey(char ch)
        {
            short vkey = VkKeyScan(ch);
            Keys retval = (Keys)(vkey & 0xff);
            int modifiers = vkey >> 8;
            if ((modifiers & 1) != 0) { PressHighKey((Keys)retval); return; }
            if ((modifiers & 2) != 0) retval |= Keys.Control;
            if ((modifiers & 4) != 0) retval |= Keys.Alt;

            PressKey(retval);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        private void buttonStop_Click(object sender, EventArgs e)
        {
            myThread1.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxTact.Text = "20";
        }
    }
}