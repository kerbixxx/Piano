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
using static piano.InputSender;

namespace piano
{
    class Player
    {
        public string text { get; set; }
        public int tact { get; set; }

        public Player(string text, int tact)
        {
            this.text = text;
            this.tact = tact;
        }

        #region Imports
        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        [DllImport("user32.dll")]
        static extern int MapVirtualKey(int uCode, uint uMapType);

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
                    byte bVk,
                    byte bScan,
                                int dwFlags, // Здесь целочисленный тип нажимается 0, отпускается 2
                                int dwExtraInfo // Это целочисленный тип. Обычно устанавливается в 0
                );
        #endregion

        #region KeyPress
        void PressKey(Keys key)
        {
            Input[] inputs = new Input[]
            {
                new Input
                {
                    type = (int)InputType.Keyboard,
                    u = new InputUnion
                    {
                        ki = new KeyboardInput
                        {
                            wVk = 0,
                            wScan = (ushort)MapVirtualKey((char)key,0),
                            dwFlags = (uint)(KeyEventF.KeyDown | KeyEventF.Scancode),
                            dwExtraInfo=GetMessageExtraInfo()
                        }
                    }
                },
                new Input
                {
                    type = (int)InputType.Keyboard,
                    u = new InputUnion
                    {
                        ki = new KeyboardInput
                        {
                            wVk = 0,
                            wScan = (ushort)MapVirtualKey((char)key,0),
                            dwFlags = (uint)(KeyEventF.KeyUp | KeyEventF.Scancode),
                            dwExtraInfo =GetMessageExtraInfo()
                        }
                    }
                }
            };
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
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
            int W = MapVirtualKey((char)retval, 0);
            int modifiers = vkey >> 8;
            if ((modifiers & 1) != 0) { PressHighKey(retval); return; }
            if ((modifiers & 2) != 0) retval |= Keys.Control;
            if ((modifiers & 4) != 0) retval |= Keys.Alt;

            PressKey(retval);
        }
        #endregion

        public void PlaySong(CancellationToken cancelToken)
        {
            Thread.Sleep(1400);

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
                        Thread.Sleep(tact * 10);
                        continue;
                    }
                }
                if (text[i] == ' ')
                {
                    //PresslowKey(Keys.Space); Прикол для гонок на клавиатуре08
                    Thread.Sleep(tact * 10);
                    continue;
                }
                if (text[i] == '|')
                {
                    Thread.Sleep(tact * 20);
                    continue;
                }
                ConvertCharToVirtualKey(text[i]);
                Thread.Sleep(Convert.ToInt32(tact)); //20
                if (cancelToken.IsCancellationRequested)
                {
                    cancelToken.ThrowIfCancellationRequested();
                };
            }
        }
    }
}
