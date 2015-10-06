using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyloggerAPI.KeyObject
{
	public class Key
	{
		private KeyValue Value { get; set; }
		public bool IsUpperCase { get; set; }
		private bool CapsLockPressed { get; set; }
		private bool ShiftPressed { get; set; }

		internal Key(KeyValue value, bool shiftPressed, bool capsLockPressed)
		{
			this.Value = value;
			this.ShiftPressed = shiftPressed;
			this.CapsLockPressed = capsLockPressed;
		}

		public override string ToString()
		{
			var temp = string.Empty;

			//TODO: Format and put smartness in this code
			switch (Value)
			{
				case KeyValue.F1:
					temp = "<F1>";
					break;
				case KeyValue.F2:
					temp = "<F2>";
					break;
				case KeyValue.F3:
					temp = "<F3>";
					break;
				case KeyValue.F4:
					temp = "<F4>";
					break;
				case KeyValue.F5:
					temp = "<F5>";
					break;
				case KeyValue.F6:
					temp = "<F6>";
					break;
				case KeyValue.F7:
					temp = "<F7>";
					break;
				case KeyValue.F8:
					temp = "<F8>";
					break;
				case KeyValue.F9:
					temp = "<F9>";
					break;
				case KeyValue.F10:
					temp = "<F10>";
					break;
				case KeyValue.F11:
					temp = "<F11>";
					break;
				case KeyValue.F12:
					temp = "<F12>";
					break;
				case KeyValue.Snapshot:
					temp = "<print screen>";
					break;
				case KeyValue.Scroll:
					temp = "<scroll>";
					break;
				case KeyValue.Pause:
					temp = "<pause>";
					break;
				case KeyValue.Insert:
					temp = "<insert>";
					break;
				case KeyValue.Home:
					temp = "<home>";
					break;
				case KeyValue.Delete:
					temp = "<delete>";
					break;
				case KeyValue.End:
					temp = "<end>";
					break;
				case KeyValue.Prior:
					temp = "<page up>";
					break;
				case KeyValue.Next:
					temp = "<page down>";
					break;
				case KeyValue.Escape:
					temp = "<esc>";
					break;
				case KeyValue.NumLock:
					temp = "<numlock>";
					break;
				case KeyValue.Tab:
					temp = "<tab>";
					break;
				case KeyValue.Back:
					temp = "<backspace>";
					break;
				case KeyValue.Return:
					temp = "<enter>";
					break;
				case KeyValue.Space:
					temp = " ";
					break;
				case KeyValue.Left:
					temp = "<left>";
					break;
				case KeyValue.Up:
					temp = "<up>";
					break;
				case KeyValue.Right:
					temp = "<right>";
					break;
				case KeyValue.Down:
					temp = "<down>";
					break;
				case KeyValue.Multiply:
					temp = "*";
					break;
				case KeyValue.Add:
					temp = "+";
					break;
				case KeyValue.Separator:
					temp = "|";
					break;
				case KeyValue.Subtract:
					temp = "-";
					break;
				case KeyValue.Decimal:
					temp = ".";
					break;
				case KeyValue.Divide:
					temp = "/";
					break;
				case KeyValue.Oem1:
					temp = ";";
					break;
				case KeyValue.Oemplus:
					temp = "=";
					break;
				case KeyValue.Oemcomma:
					temp = ",";
					break;
				case KeyValue.OemMinus:
					temp = "-";
					break;
				case KeyValue.OemPeriod:
					temp = ".";
					break;
				case KeyValue.Oem2:
					temp = "/";
					break;
				case KeyValue.Oem3:
					temp = "`";
					break;
				case KeyValue.Oem4:
					temp = "´";
					break;
				case KeyValue.Oem5:
					temp = @"]";
					break;
				case KeyValue.Oem6:
					temp = "[";
					//EN-US
					//case KeyboardKeyValues.Oem4:
					//	temp = "[";
					//	break;
					//case KeyboardKeyValues.Oem5:
					//	temp = @"\";
					//	break;
					//case KeyboardKeyValues.Oem6:
					//	temp = "]";
					break;
				case KeyValue.Oem7:
					temp = "'";
					break;
				case KeyValue.NumPad0:
					temp = "0";
					break;
				case KeyValue.NumPad1:
					temp = "1";
					break;
				case KeyValue.NumPad2:
					temp = "2";
					break;
				case KeyValue.NumPad3:
					temp = "3";
					break;
				case KeyValue.NumPad4:
					temp = "4";
					break;
				case KeyValue.NumPad5:
					temp = "5";
					break;
				case KeyValue.NumPad6:
					temp = "6";
					break;
				case KeyValue.NumPad7:
					temp = "7";
					break;
				case KeyValue.NumPad8:
					temp = "8";
					break;
				case KeyValue.NumPad9:
					temp = "9";
					break;
				case KeyValue.Q:
					temp = "q";
					break;
				case KeyValue.W:
					temp = "w";
					break;
				case KeyValue.E:
					temp = "e";
					break;
				case KeyValue.R:
					temp = "r";
					break;
				case KeyValue.T:
					temp = "t";
					break;
				case KeyValue.Y:
					temp = "y";
					break;
				case KeyValue.U:
					temp = "u";
					break;
				case KeyValue.I:
					temp = "i";
					break;
				case KeyValue.O:
					temp = "o";
					break;
				case KeyValue.P:
					temp = "p";
					break;
				case KeyValue.A:
					temp = "a";
					break;
				case KeyValue.S:
					temp = "s";
					break;
				case KeyValue.D:
					temp = "d";
					break;
				case KeyValue.F:
					temp = "f";
					break;
				case KeyValue.G:
					temp = "g";
					break;
				case KeyValue.H:
					temp = "h";
					break;
				case KeyValue.J:
					temp = "j";
					break;
				case KeyValue.K:
					temp = "k";
					break;
				case KeyValue.L:
					temp = "l";
					break;
				case KeyValue.Z:
					temp = "z";
					break;
				case KeyValue.X:
					temp = "x";
					break;
				case KeyValue.C:
					temp = "c";
					break;
				case KeyValue.V:
					temp = "v";
					break;
				case KeyValue.B:
					temp = "b";
					break;
				case KeyValue.N:
					temp = "n";
					break;
				case KeyValue.M:
					temp = "m";
					break;
				case KeyValue.D0:
					temp = "0";
					break;
				case KeyValue.D1:
					temp = "1";
					break;
				case KeyValue.D2:
					temp = "2";
					break;
				case KeyValue.D3:
					temp = "3";
					break;
				case KeyValue.D4:
					temp = "4";
					break;
				case KeyValue.D5:
					temp = "5";
					break;
				case KeyValue.D6:
					temp = "6";
					break;
				case KeyValue.D7:
					temp = "7";
					break;
				case KeyValue.D8:
					temp = "8";
					break;
				case KeyValue.D9:
					temp = "9";
					break;
				default: break;
			}

			if (ShiftPressed)
			{
				if ((int)Value > 64 && (int)Value < 91 && CapsLockPressed == false)
					return temp.ToUpper();

				if (Value == KeyValue.D1) return "!";
				if (Value == KeyValue.D2) return "@";
				if (Value == KeyValue.D3) return "#";
				if (Value == KeyValue.D4) return "$";
				if (Value == KeyValue.D5) return "%";
				if (Value == KeyValue.D6) return "^";
				if (Value == KeyValue.D7) return "&";
				if (Value == KeyValue.D8) return "*";
				if (Value == KeyValue.D9) return "(";
				if (Value == KeyValue.D0) return ")";
				if (Value == KeyValue.Oem1) return ":";
				if (Value == KeyValue.Oem2) return "?";
				if (Value == KeyValue.Oem3) return "~";
				if (Value == KeyValue.Oemcomma) return "<";
				if (Value == KeyValue.OemMinus) return "_";
				if (Value == KeyValue.OemPeriod) return ">";
				if (Value == KeyValue.Oemplus) return "+";
				if (Value == KeyValue.Oem4) return "`";
				if (Value == KeyValue.Oem5) return "}";
				if (Value == KeyValue.Oem6) return "{";
				//EN-US
				//if (vk == KeyboardKeyValues.Oem4) return "{";
				//if (vk == KeyboardKeyValues.Oem5) return "|";
				//if (vk == KeyboardKeyValues.Oem6) return "}";
				if (Value == KeyValue.Oem7) return "\"";
			}
			else if (CapsLockPressed)
			{
				if ((int)Value > 64 && (int)Value < 91)
					return temp.ToUpper();
			}

			return temp;
		}
	}
}
