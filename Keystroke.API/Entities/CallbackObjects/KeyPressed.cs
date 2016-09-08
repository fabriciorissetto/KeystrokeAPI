namespace Keystroke.API.CallbackObjects
{
    public class KeyPressed
    {
        public KeyCode KeyCode { get; set; }
        public bool CapsLockOn { get; set; }
        public bool ShiftPressed { get; set; }
        public string CurrentWindow { get; set; }
        public string KeyboardLayout { get; set; }

        internal KeyPressed(KeyCode keyCode, bool shiftPressed, bool capsLockOn, string currentWindow, string keyboardLayout)
        {
            this.KeyCode = keyCode;
            this.ShiftPressed = shiftPressed;
            this.CapsLockOn = capsLockOn;
            this.CurrentWindow = currentWindow;
            this.KeyboardLayout = keyboardLayout;
        }        

        public override string ToString()
        {
            var character = ConvertKeyCodeToString();

            if (IsAlphabeticKey())
            {
                //If both (shift and caps) are active then the string remains lowercase
                if (ShiftPressed == CapsLockOn)
                    return character;
                else
                    return character.ToUpper();
            }
            else if (ShiftPressed)
            {
                return ShiftCharacterIfItIsShiftable(character);
            }

            return character;
        }

        private bool IsAlphabeticKey()
        {
            return (int)KeyCode > 64 && (int)KeyCode < 91;
        }

        private string ConvertKeyCodeToString()
        {
            if (KeyCode == KeyCode.F1) return "<F1>";
            if (KeyCode == KeyCode.F2) return "<F2>";
            if (KeyCode == KeyCode.F3) return "<F3>";
            if (KeyCode == KeyCode.F4) return "<F4>";
            if (KeyCode == KeyCode.F5) return "<F5>";
            if (KeyCode == KeyCode.F6) return "<F6>";
            if (KeyCode == KeyCode.F7) return "<F7>";
            if (KeyCode == KeyCode.F8) return "<F8>";
            if (KeyCode == KeyCode.F9) return "<F9>";
            if (KeyCode == KeyCode.F10) return "<F10>";
            if (KeyCode == KeyCode.F11) return "<F11>";
            if (KeyCode == KeyCode.F12) return "<F12>";
            if (KeyCode == KeyCode.Snapshot) return "<print screen>";
            if (KeyCode == KeyCode.Scroll) return "<scroll>";
            if (KeyCode == KeyCode.Pause) return "<pause>";
            if (KeyCode == KeyCode.Insert) return "<insert>";
            if (KeyCode == KeyCode.Home) return "<home>";
            if (KeyCode == KeyCode.Delete) return "<delete>";
            if (KeyCode == KeyCode.End) return "<end>";
            if (KeyCode == KeyCode.Prior) return "<page up>";
            if (KeyCode == KeyCode.Next) return "<page down>";
            if (KeyCode == KeyCode.Escape) return "<esc>";
            if (KeyCode == KeyCode.NumLock) return "<numlock>";
            if (KeyCode == KeyCode.Tab) return "<tab>";
            if (KeyCode == KeyCode.Back) return "<backspace>";
            if (KeyCode == KeyCode.Return) return "<enter>";
            if (KeyCode == KeyCode.Space) return " ";
            if (KeyCode == KeyCode.Left) return "<left>";
            if (KeyCode == KeyCode.Up) return "<up>";
            if (KeyCode == KeyCode.Right) return "<right>";
            if (KeyCode == KeyCode.Down) return "<down>";

            if (KeyCode == KeyCode.LMenu || KeyCode == KeyCode.RMenu) return "<alt>";
            if (KeyCode == KeyCode.LWin || KeyCode == KeyCode.RWin) return "<win>";
            if (KeyCode == KeyCode.Capital) return "<capsLock>";
            if (KeyCode == KeyCode.LControlKey || KeyCode == KeyCode.RControlKey) return "<ctrl>";
            if (KeyCode == KeyCode.LShiftKey || KeyCode == KeyCode.RShiftKey) return "<shift>";

            if (KeyCode == KeyCode.VolumeDown) return "<volumeDown>";
            if (KeyCode == KeyCode.VolumeUp) return "<volumeUp>";
            if (KeyCode == KeyCode.VolumeMute) return "<volumeMute>";

            if (KeyCode == KeyCode.Multiply) return "*";
            if (KeyCode == KeyCode.Add) return "+";
            if (KeyCode == KeyCode.Separator) return "|";
            if (KeyCode == KeyCode.Subtract) return "-";            
            if (KeyCode == KeyCode.Divide) return "/";            
            if (KeyCode == KeyCode.Oemplus) return "=";
            if (KeyCode == KeyCode.Oemcomma) return ",";
            if (KeyCode == KeyCode.OemMinus) return "-";
            if (KeyCode == KeyCode.OemPeriod) return ".";
            if (KeyCode == KeyCode.NumPadDot) return ".";

            if (KeyboardLayout == "pt-BR")
            {
                if (KeyCode == KeyCode.LatinKeyboardBar) return "/";
                if (KeyCode == KeyCode.Decimal) return ",";
                if (KeyCode == KeyCode.Oem1) return "ç";
                if (KeyCode == KeyCode.Oem2) return ";";
                if (KeyCode == KeyCode.Oem3) return "`";
                if (KeyCode == KeyCode.Oem4) return "´";
                if (KeyCode == KeyCode.Oem5) return @"]";
                if (KeyCode == KeyCode.Oem6) return "[";
                if (KeyCode == KeyCode.Oem7) return "~";
            }

            //EN-US
            if (KeyCode == KeyCode.Decimal) return ".";
            if (KeyCode == KeyCode.Oem1) return ";";
            if (KeyCode == KeyCode.Oem2) return "/";
            if (KeyCode == KeyCode.Oem3) return "`";
            if (KeyCode == KeyCode.Oem4) return "[";
            if (KeyCode == KeyCode.Oem5) return "\\";
            if (KeyCode == KeyCode.Oem6) return "]";
            if (KeyCode == KeyCode.Oem7) return "'";

            if (KeyCode == KeyCode.NumPad0) return "0";
            if (KeyCode == KeyCode.NumPad1) return "1";
            if (KeyCode == KeyCode.NumPad2) return "2";
            if (KeyCode == KeyCode.NumPad3) return "3";
            if (KeyCode == KeyCode.NumPad4) return "4";
            if (KeyCode == KeyCode.NumPad5) return "5";
            if (KeyCode == KeyCode.NumPad6) return "6";
            if (KeyCode == KeyCode.NumPad7) return "7";
            if (KeyCode == KeyCode.NumPad8) return "8";
            if (KeyCode == KeyCode.NumPad9) return "9";
            if (KeyCode == KeyCode.Q) return "q";
            if (KeyCode == KeyCode.W) return "w";
            if (KeyCode == KeyCode.E) return "e";
            if (KeyCode == KeyCode.R) return "r";
            if (KeyCode == KeyCode.T) return "t";
            if (KeyCode == KeyCode.Y) return "y";
            if (KeyCode == KeyCode.U) return "u";
            if (KeyCode == KeyCode.I) return "i";
            if (KeyCode == KeyCode.O) return "o";
            if (KeyCode == KeyCode.P) return "p";
            if (KeyCode == KeyCode.A) return "a";
            if (KeyCode == KeyCode.S) return "s";
            if (KeyCode == KeyCode.D) return "d";
            if (KeyCode == KeyCode.F) return "f";
            if (KeyCode == KeyCode.G) return "g";
            if (KeyCode == KeyCode.H) return "h";
            if (KeyCode == KeyCode.J) return "j";
            if (KeyCode == KeyCode.K) return "k";
            if (KeyCode == KeyCode.L) return "l";
            if (KeyCode == KeyCode.Z) return "z";
            if (KeyCode == KeyCode.X) return "x";
            if (KeyCode == KeyCode.C) return "c";
            if (KeyCode == KeyCode.V) return "v";
            if (KeyCode == KeyCode.B) return "b";
            if (KeyCode == KeyCode.N) return "n";
            if (KeyCode == KeyCode.M) return "m";
            if (KeyCode == KeyCode.D0) return "0";
            if (KeyCode == KeyCode.D1) return "1";
            if (KeyCode == KeyCode.D2) return "2";
            if (KeyCode == KeyCode.D3) return "3";
            if (KeyCode == KeyCode.D4) return "4";
            if (KeyCode == KeyCode.D5) return "5";
            if (KeyCode == KeyCode.D6) return "6";
            if (KeyCode == KeyCode.D7) return "7";
            if (KeyCode == KeyCode.D8) return "8";
            if (KeyCode == KeyCode.D9) return "9";
            return string.Empty;
        }

        private string ShiftCharacterIfItIsShiftable(string character)
        {
            if (KeyCode == KeyCode.D1) return "!";
            if (KeyCode == KeyCode.D2) return "@";
            if (KeyCode == KeyCode.D3) return "#";
            if (KeyCode == KeyCode.D4) return "$";
            if (KeyCode == KeyCode.D5) return "%";
            if (KeyCode == KeyCode.D6) return "^";
            if (KeyCode == KeyCode.D7) return "&";
            if (KeyCode == KeyCode.D8) return "*";
            if (KeyCode == KeyCode.D9) return "(";
            if (KeyCode == KeyCode.D0) return ")";
            if (KeyCode == KeyCode.Oemcomma) return "<";
            if (KeyCode == KeyCode.OemMinus) return "_";
            if (KeyCode == KeyCode.OemPeriod) return ">";
            if (KeyCode == KeyCode.Oemplus) return "+";
            if ((int)KeyCode == 193) return "?";

            if (KeyboardLayout == "pt-BR")
            {
                if (KeyCode == KeyCode.Oem1) return "Ç";
                if (KeyCode == KeyCode.Oem2) return ":";
                if (KeyCode == KeyCode.Oem3) return "\"";
                if (KeyCode == KeyCode.Oem4) return "`";
                if (KeyCode == KeyCode.Oem5) return "}";
                if (KeyCode == KeyCode.Oem6) return "{";
                if (KeyCode == KeyCode.Oem7) return "^";
            }

            //EN-US
            if (KeyCode == KeyCode.Oem1) return ":";
            if (KeyCode == KeyCode.Oem2) return "?";
            if (KeyCode == KeyCode.Oem3) return "~";
            if (KeyCode == KeyCode.Oem4) return "{";
            if (KeyCode == KeyCode.Oem5) return "|";
            if (KeyCode == KeyCode.Oem6) return "}";
            if (KeyCode == KeyCode.Oem7) return "\"";

            //Character not "shiftable"
            return character;
        }
    }
}
