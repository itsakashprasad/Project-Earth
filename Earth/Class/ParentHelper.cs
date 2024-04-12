using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PrintingSetup.Class
{
    public static class ParentHelper
    {
        public static Window FoundParent(object sender, out string Message)
        {
            Window Parent = null;
            try
            {
                var uiElement = sender as UIElement;
                if (uiElement != null)
                {

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
                                Message = "Error Locating Parent";
                                return Parent;
                            }
                        }
                        var window = parent as Window;
                        Message = "Success";
                        return window;
                    }
                }
                Message = "Success";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                
            }
            return Parent;
        }
    }
}
