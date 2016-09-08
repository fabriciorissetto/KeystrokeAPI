using Keystroke.API;
using System.Text;

namespace Keystroke.API.Helpers
{
    internal class Window
    {
        internal string CurrentWindowTitle()
        {
            int hwnd = User32.GetForegroundWindow();
            StringBuilder title = new StringBuilder(1024);

            int textLength = User32.GetWindowText(hwnd, title, title.Capacity);
            if ((textLength <= 0) || (textLength > title.Length))
                return "[Unknown]";

            return "[" + title + "]";
        }
    }
}
