using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Earth
{
    public static class  WarningMessage
    {
        public static bool ShowWaningMessage(string Title, string Message, int Height, int Width, Brush TitleColor)
        {
            DoubleAnimation animFadeIn = new DoubleAnimation();
            animFadeIn.From = 0;
            animFadeIn.To = 1;
            animFadeIn.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            WarningMessageUserControl MsgBox = new WarningMessageUserControl();
            MsgBox.Title = Title;
            MsgBox.Message = Message;
            MsgBox.Height = Height;
            MsgBox.Width = Width;
            MsgBox.TitleColor = TitleColor;
            Window window = new Window
            {
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Normal,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                AllowsTransparency = true,
                Background = Brushes.Transparent,
                Content = MsgBox,

            };
            window.BeginAnimation(Window.OpacityProperty, animFadeIn);
            window.ShowDialog();
            bool DilogResult = MsgBox.DialogResult;
            return DilogResult;

        }
    }
}
