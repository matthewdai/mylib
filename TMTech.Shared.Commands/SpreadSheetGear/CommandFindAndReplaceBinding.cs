using SpreadsheetGear.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TMTech.Shared.Commands.SpreadSheetGear
{
 
    /// <summary>
    /// Command binding for AutoFilter command
    /// </summary>
    public sealed class CommandFindAndReplaceBinding : CommandBinding
    {

        public CommandFindAndReplaceBinding()
            : base(SpreadSheetCommands.FindAndReplace, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbview = Helper.FindChild<WorkbookView>(sender as DependencyObject);
            if (wbview != null)
            {
                wbview.Focus();
                System.Windows.Forms.SendKeys.Send("^f");  // does not work, we need to implement my own dialog.
            }

             
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            WorkbookView wbview = Helper.FindChild<WorkbookView>(sender as DependencyObject);
            e.CanExecute = wbview != null;
        }

    }
}
