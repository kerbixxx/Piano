using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Workspace;
using static piano.InputSender;

namespace piano
{
    // Declare the INPUT struct
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        internal uint type;
        internal InputUnion U;
        internal static int Size
        {
            get { return Marshal.SizeOf(typeof(INPUT)); }
        }
    }
    [Flags]
    internal enum KEYEVENTF : uint
    {
        EXTENDEDKEY = 0x0001,
        KEYUP = 0x0002,
        SCANCODE = 0x0008,
        UNICODE = 0x0004
    }
    public class Player
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
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int showCmds);
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

        void HoldShift()
        {
            var Inputs = new INPUT[1];
            var Input = new INPUT();
            Input.type = 1;
            Input.U.ki.wScan = 42;
            Input.U.ki.dwFlags = (uint)KeyEventF.Scancode;
            Inputs[0] = Input;
            SendInput(1, Inputs, INPUT.Size);
        }
        void ReleaseShift()
        {
            var Inputs = new INPUT[1];
            var Input = new INPUT();
            Input.type = 1;
            Input.U.ki.wScan = 42;
            Input.U.ki.dwFlags = (uint)KeyEventF.KeyUp | (uint)KeyEventF.Scancode;
            Inputs[0] = Input;
            SendInput(1, Inputs, INPUT.Size);
        }

        void PressHighKey(Keys key)
        {
            HoldShift();
            PressKey(key);
            ReleaseShift();
        }

        void ConvertCharToVirtualKey(char ch)
        {
            if (ch == ' ') return;
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
    
        public async Task PlaySong(CancellationToken cancelToken, PauseToken pauseToken)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1400), cancelToken);
                for (int i = 0; i < text.Length; i++)
                {
                    if (cancelToken.IsCancellationRequested)
                    {
                        return;
                    }

                    ;

                    await pauseToken.WaitWhilePausedAsync();

                    if (text[i] == '-')
                    {
                        await Task.Delay(tact * 4, cancelToken);
                        continue;
                    }

                    if (text[i] == '+')
                    {
                        await Task.Delay(tact / 8, cancelToken);
                        continue;
                    }

                    if (text[i] == '=')
                    {
                        await Task.Delay(tact / 6, cancelToken);
                        continue;
                    }

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
                                if (buffer[j] == ' ') await Task.Delay(tact / 2, cancelToken);
                                ConvertCharToVirtualKey(buffer[j]);
                            }

                            await Task.Delay(tact, cancelToken);
                            continue;
                        }
                    }

                    if (text[i] == ' ')
                    {
                        await Task.Delay(tact * 2, cancelToken);
                        continue;
                    }

                    if (text[i] == '|')
                    {
                        await Task.Delay(tact * 4, cancelToken);
                        continue;
                    }

                    CancellationToken token;
                    if (text[i] == '\n' || text[i] == '\r')
                    {
                        continue;
                    }

                    ConvertCharToVirtualKey(text[i]);
                    await Task.Delay(tact, cancelToken);
                    var kek = Task.CurrentId;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
