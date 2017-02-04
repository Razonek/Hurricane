﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hurricane
{
    /// <summary>
    /// Everything what we use in game, binds, memory read, key events
    /// </summary>
    public class Game
    {
        
        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, int lpNumberOfBytesWritten);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern int mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

        public static int KEYEVENTF_KEYUP = 0x0002;


        /// <summary>
        /// Reading game memory
        /// </summary>
        /// <param name="Value"> game offset to read </param>
        /// <returns> readed value at this memory point </returns>
        public static int Read(int Value)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(AutoInjector.GameHandle, Value, buffer, 4, 0);
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}
