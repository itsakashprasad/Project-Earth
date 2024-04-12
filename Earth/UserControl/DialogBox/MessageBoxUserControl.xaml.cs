using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Earth
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBoxUserControl : System.Windows.Controls.UserControl
    {
        public string Title;
        public string Message;
        public int MsgHeight;
        public int MsgWidth;
        public Brush TitleColor;


        public MessageBoxUserControl()
        {
            InitializeComponent();
            this.Height = MsgHeight;
            this.Width = MsgWidth;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
            TitleBackground.Background = TitleColor;
            MessageTitle.Text = Title;
            lblMessage.Text = Message;

        }

        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var uiElement = sender as UIElement;
            if (uiElement != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DependencyObject parent = uiElement;
                    int avoidInfiniteLoop = 0;
                    // Search up the visual tree to find the first parent window.
                    while ((parent is Window) == false)
                    {
                        parent = VisualTreeHelper.GetParent(parent);
                        avoidInfiniteLoop++;
                        if (avoidInfiniteLoop == 1000)
                        {
                            // Something is wrong - we could not find the parent window.
                            return;
                        }
                    }
                    var window = parent as Window;
                    window.DragMove();
                }
            }
        }
    }
}
