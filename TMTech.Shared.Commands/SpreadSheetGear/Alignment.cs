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
    public sealed class HorizontalAlignmentCommandBinding : CommandBinding
    {

        public HorizontalAlignmentCommandBinding()
            : base(SpreadSheetCommands.HorizontalAlignment, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbView = Helper.FindChild<WorkbookView>(sender as DependencyObject);
            if (wbView == null) return;


            // NOTE: Must acquire a workbook set lock.
            wbView.GetLock();

            try
            {
                var param = e.Parameter as string;
                if (param == "Left")
                    wbView.RangeSelection.HorizontalAlignment = SpreadsheetGear.HAlign.Left;
                else if (param == "Center")
                    wbView.RangeSelection.HorizontalAlignment = SpreadsheetGear.HAlign.Center;
                else if (param == "Right")
                    wbView.RangeSelection.HorizontalAlignment = SpreadsheetGear.HAlign.Right;
                else
                    wbView.RangeSelection.HorizontalAlignment = SpreadsheetGear.HAlign.General;

            }
            catch { }
            finally
            {
                // NOTE: Must release the workbook set lock
                wbView.ReleaseLock();
            }
        }

        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var wbview = Helper.FindChild<WorkbookView>(sender as DependencyObject);
            e.CanExecute = wbview != null;
        }
    }

    public sealed class VerticalAlignmentCommandBinding : CommandBinding
    {

        public VerticalAlignmentCommandBinding()
            : base(SpreadSheetCommands.VerticalAlignment, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbView = Helper.FindChild<WorkbookView>(sender as DependencyObject);
            if (wbView == null) return;


            // NOTE: Must acquire a workbook set lock.
            wbView.GetLock();

            try
            {
                var param = e.Parameter as string;
                if (param == "Top")
                    wbView.RangeSelection.VerticalAlignment = SpreadsheetGear.VAlign.Top;
                else if (param == "Center")
                    wbView.RangeSelection.VerticalAlignment = SpreadsheetGear.VAlign.Center;
                else if (param == "Bottom")
                    wbView.RangeSelection.VerticalAlignment = SpreadsheetGear.VAlign.Bottom;
                else
                    wbView.RangeSelection.VerticalAlignment = SpreadsheetGear.VAlign.Bottom;

            }
            catch { }
            finally
            {
                // NOTE: Must release the workbook set lock
                wbView.ReleaseLock();
            }
        }

        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var wbview = Helper.FindChild<WorkbookView>(sender as DependencyObject);
            e.CanExecute = wbview != null;
        }
    }
}
