using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Keystroke.API
{
    /// <summary>
    /// Class to wrap the dependencies for the User32.dll
    /// </summary>
    internal static class User32
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr SetWindowsHookEx(int idHook, LowLevelHook lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		internal static extern short GetKeyState(KeyCode virtualKeyCode);

		[DllImport("User32.dll")]
		internal static extern short GetAsyncKeyState(KeyCode vKey);

		[DllImport("User32.dll")]
		internal static extern int GetWindowText(int hwnd, StringBuilder s, int nMaxCount);

		[DllImport("User32.dll")]
		internal static extern int GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetKeyboardLayout(uint thread);

        internal delegate IntPtr LowLevelHook(int nCode, IntPtr wParam, IntPtr lParam);        
    }
}
