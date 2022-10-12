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
                Thread myThread1 = new Thread(PlaySong);
                myThread1.Start();
            }
        }
        void PlaySong()
        {
            int tact = Convert.ToInt32(textBoxTact.Text);
            Thread.Sleep(4000);
            string text = textBox1.Text;

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
                    //PresslowKey(Keys.Space); ������ ��� ����� �� ����������08
                    Thread.Sleep(tact*10);
                    continue;
                }
                if (text[i] == '|')
                {
                    Thread.Sleep(tact*20);
                    continue;
                }
                Thread.Sleep(Convert.ToInt32(tact)); //20
            }
        }

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
                    byte bVk,
                    byte bScan,
                                int dwFlags, // ����� ������������� ��� ���������� 0, ����������� 2
                                int dwExtraInfo // ��� ������������� ���. ������ ��������������� � 0
                );
        void PresslowKey(Keys key)
        {
            keybd_event((byte)key, 0, 0, 0);
            keybd_event((byte)key, 0, 2, 0);
        }

        void PressHighKey(Keys key)
        {
            keybd_event((byte)Keys.ShiftKey, 0, 0, 0);
            keybd_event((byte)key, 0, 0, 0);
            keybd_event((byte)key, 0, 2, 0);
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

            PresslowKey(retval);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Thread.ResetAbort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxTact.Text = "20";
        }
    }
}