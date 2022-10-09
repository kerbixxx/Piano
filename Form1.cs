using System.Text.RegularExpressions;
using System.Globalization;
using System.Runtime.InteropServices;
using System;

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
                        for(int j = 0; j < buffer.Length; j++)
                        {
                            PressKey((byte)buffer[j]);
                        }
                        continue;
                    }
                }
                if (text[i] == ' ')
                {
                    Thread.Sleep(100);
                    continue;
                }
                if (text[i] == '|')
                {
                    Thread.Sleep(400);
                    continue;
                }
                string buffPlay = text[i].ToString();
                PressKey((byte)text[i]);
                Thread.Sleep(50);
                //textBox2.Text = textBox2.Text + text[i].ToString();
            }
        }

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
                    byte bVk,
                    byte bScan,
                                int dwFlags, // Здесь целочисленный тип нажимается 0, отпускается 2
                                int dwExtraInfo // Это целочисленный тип. Обычно устанавливается в 0
                );
        void PressKey(byte input)
        {
            keybd_event(input, 0, 0, 0);
            keybd_event(input, 0, 2, 0);
        }
    }
}