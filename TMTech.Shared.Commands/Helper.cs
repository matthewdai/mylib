using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TMTech.Shared.Commands
{
    public class Helper
    {
        public static T FindChild<T>(DependencyObject parent)
            where T : DependencyObject
        {
            if (parent == null) return null;

            // first check parent itself
            T found = parent as T;
            if (found != null) return found;
            
            // check children    
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                found = child as T;
                if (found == null)
                {
                    // recursively drill down the tree
                    found = FindChild<T>(child);
                    if (found != null) break;
                }
                else
                {
                    break;
                }
            }

            return found;
        }
    }
}
