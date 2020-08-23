using Keystroke.API.CallbackObjects;
using Keystroke.API.Helpers;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Keystroke.API
{
    public class KeystrokeAPI : IDisposable
	{
		private IntPtr globalKeyboardHookId;
		private IntPtr currentModuleId;
		private const int WH_KEYBOARD_LL = 13;
		private const int WH_MOUSE_LL = 14;
		private const int WM_KEYDOWN = 0x100;
		private const int WM_SYSKEYDOWN = 0x104;
        private User32.LowLevelHook HookKeyboardDelegate; //We need to have this delegate as a private field so the GC doesn't collect it
        private Action<KeyPressed> keyPressedCallback;

		public KeystrokeAPI()
		{
			Process currentProcess = Process.GetCurrentProcess();
			ProcessModule currentModudle = currentProcess.MainModule;
			this.currentModuleId = User32.GetModuleHandle(currentModudle.ModuleName);
		}

		public void CreateKeyboardHook(Action<KeyPressed> keyPressedCallback)
		{
			this.keyPressedCallback = keyPressedCallback;
            this.HookKeyboardDelegate = HookKeyboardCallbackImplementation;
            this.globalKeyboardHookId = User32.SetWindowsHookEx(WH_KEYBOARD_LL, this.HookKeyboardDelegate, this.currentModuleId, 0);
		}

		private IntPtr HookKeyboardCallbackImplementation(int nCode, IntPtr wParam, IntPtr lParam)
		{
			int wParamAsInt = wParam.ToInt32();

			if (nCode >= 0 && (wParamAsInt == WM_KEYDOWN || wParamAsInt == WM_SYSKEYDOWN))
			{
				bool shiftPressed = false;
				bool capsLockActive = false;

				var shiftKeyState = User32.GetAsyncKeyState(KeyCode.ShiftKey);
				if (FirstBitIsTurnedOn(shiftKeyState))
					shiftPressed = true;

				//We need to use GetKeyState to verify if CapsLock is "TOGGLED" 
				//because GetAsyncKeyState only verifies if it is "PRESSED" at the moment
				if (User32.GetKeyState(KeyCode.Capital) == 1)
					capsLockActive = true;

				KeyParser(wParam, lParam, shiftPressed, capsLockActive);
			}

			//Chain to the next hook. Otherwise other applications that 
			//are listening to this hook will not get notificied
			return User32.CallNextHookEx(globalKeyboardHookId, nCode, wParam, lParam);
		}		

		private bool FirstBitIsTurnedOn(short value)
		{
			//0x8000 == 1000 0000 0000 0000			
			return Convert.ToBoolean(value & 0x8000);
		}

		private void KeyParser(IntPtr wParam, IntPtr lParam, bool shiftPressed, bool capsLockPressed)
		{
			var keyValue = (KeyCode)Marshal.ReadInt32(lParam);

            var keyboardLayout = new KeyboardLayout().GetCurrentKeyboardLayout();
            var windowTitle = new Window().CurrentWindowTitle();

            var key = new KeyPressed(keyValue, shiftPressed, capsLockPressed, windowTitle, keyboardLayout.ToString());

			keyPressedCallback.Invoke(key);
		}

		public void Dispose()
		{
			if (globalKeyboardHookId != IntPtr.Zero)
				User32.UnhookWindowsHookEx(globalKeyboardHookId);
		}
	}
}
