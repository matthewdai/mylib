using Microsoft.Win32;
using SpreadsheetGear;
using SpreadsheetGear.Windows.Controls;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TMTech.Shared.Commands.SpreadSheetGear
{

    /// <summary>
    /// This class contain some commands related with application.
    /// </summary>
    public static class SpreadSheetCommands
    {
        //public static RoutedCommand ExportToExcel = new RoutedCommand();
        public static RoutedUICommand ZoomIn = new RoutedUICommand("Zoom in", "ZoomIn", typeof(string));
        public static RoutedUICommand ZoomOut = new RoutedUICommand("Zoom out", "ZoomOut", typeof(string));
        public static RoutedUICommand Bold = new RoutedUICommand("Bold", "Bold", typeof(string));
        public static RoutedUICommand Italic = new RoutedUICommand("Italic", "Italic",  typeof(string));

        public static RoutedUICommand Underline = new RoutedUICommand("Underline", "Underline", typeof(string));
        public static RoutedUICommand Percent = new RoutedUICommand("Percent", "Percent", typeof(string));
        public static RoutedUICommand ThousandSeperator = new RoutedUICommand("Thousand Seperator", "ThousandSeperator", typeof(string));
        public static RoutedUICommand IncreaseDecimal = new RoutedUICommand("Increase Decimal", "IncreaseDecimal", typeof(string));
        public static RoutedUICommand DecreaseDecimal = new RoutedUICommand("Decrease Decimal", "DecreaseDecimal", typeof(string));
        public static RoutedUICommand Format = new RoutedUICommand("Format ...", "Format", typeof(string));
        public static RoutedUICommand AutoFilter = new RoutedUICommand("Auto Filter", "AutoFilter", typeof(string));
        internal static RoutedUICommand FindAndReplace = new RoutedUICommand("Find ...", "FindAndReplace", typeof(string), new InputGestureCollection() { new KeyGesture(Key.F, ModifierKeys.Control) });

        public static RoutedUICommand HorizontalAlignment = new RoutedUICommand();
        public static RoutedUICommand VerticalAlignment = new RoutedUICommand();

        /// <summary>
        /// Bind commands to workbookview.
        /// </summary>
        /// <param name="workbookView"></param>
        public static void BindAllCommands(Control control)
        {
            control.CommandBindings.Add(new CommandCopyBinding());
            control.CommandBindings.Add(new CommandPasteBinding());
            //control.CommandBindings.Add(new CommandPasteBinding());

            control.CommandBindings.Add(new CommandUndoBinding());
            control.CommandBindings.Add(new CommandRedoBinding());

            control.CommandBindings.Add(new FormatCommandBinding());

            control.CommandBindings.Add(new BoldCommandBinding());
            control.CommandBindings.Add(new ItalicCommandBinding());
            control.CommandBindings.Add(new UnderlineCommandBinding());

            control.CommandBindings.Add(new CommandZoomInBinding());
            control.CommandBindings.Add(new CommandZoomOutBinding());

            control.CommandBindings.Add(new PercentCommandBinding());
            control.CommandBindings.Add(new ThousandSeperatorCommandBinding());
            control.CommandBindings.Add(new IncreaseDecimalCommandBinding());
            control.CommandBindings.Add(new DecreaseDecimalCommandBinding());

            control.CommandBindings.Add(new CommandSaveAsBinding());
            control.CommandBindings.Add(new PrintCommandBinding());

            control.CommandBindings.Add(new CommandAutoFilterBinding());
            //control.CommandBindings.Add(new CommandFindAndReplaceBinding());

            control.CommandBindings.Add(new HorizontalAlignmentCommandBinding());
            control.CommandBindings.Add(new VerticalAlignmentCommandBinding());
        }
    }


    
    /// <summary>
    /// Command binding for Export to Excel file in spreadwheer viewer.
    /// </summary>
    public sealed class CommandSaveAsBinding : CommandBinding
    {

        public CommandSaveAsBinding()
            : base(ApplicationCommands.SaveAs, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbview = Helper.FindChild<WorkbookView>(sender as DependencyObject);

            if (wbview != null)
            {
                // choise a file name
                var dlg = new SaveFileDialog();
                dlg.Filter = "Excel Workbook (*.xlsx)|*.xlsx";
                var result = dlg.ShowDialog();

                if (result == true)
                {
                    try
                    {
                        wbview.GetLock();
                        wbview.ActiveWorkbook.SaveAs(dlg.FileName, SpreadsheetGear.FileFormat.OpenXMLWorkbook);
                    }
                    catch { 
                    }
                    finally
                    {
                        wbview.ReleaseLock();
                    }
                }
            }
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            WorkbookView wbview = Helper.FindChild<WorkbookView>(sender as DependencyObject);
            e.CanExecute = wbview != null;
        }

    }

    
    /// <summary>
    /// Defines the command binding for application command COPY
    /// </summary>
    public sealed class CommandCopyBinding : CommandBinding
    {

        public CommandCopyBinding()
            : base(ApplicationCommands.Copy, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null)
            {
                control.Copy();
                return;
            }
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null)
            {
                e.CanExecute = true;
                return;
            }

        }

    }

    
    /// <summary>
    /// Defines the command binding for application paste command
    /// </summary>
    public sealed class CommandPasteBinding : CommandBinding
    {

        public CommandPasteBinding()
            : base(ApplicationCommands.Paste, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null)
            {
                try
                {
                    // spreadSheetGear has a bug for pasting over 65536 rows. now using my own code to paste.
                    control.PasteSpecial(PasteType.Values, PasteOperation.None, false, false);
                    //Paste(control);

                    //string v2 = Clipboard.GetText();
                    //string[] lines = v2.Split('\r');
                    //foreach (string line in lines)
                    //{
                    //    string[] cols = line.Split('t');

                    //}
                    //control.Paste();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
                return;
            }

        }
        

        private static void Paste(WorkbookView wbView)
        {

            // check to see if clipboard contains text
            if (!Clipboard.ContainsText())
                return;

            // Confirm the pending changes before going on
            //ConfirmChanges();

            // Get the selected start row and col 
            IRange selectedRng = wbView.RangeSelection;
            int startRow = selectedRng.Row;
            int startCol = selectedRng.Column;

            // Do not paste if the row and column is out of the data area
            // If nRow < LOCATION_ROW_OFFSET OrElse nCol <= ColumnIndex.Order OrElse nCol > ColumnIndex.InspectionRequest Then Return
            //if (startCol > ColumnIndex.InspectionRequest)
            //    return;

            //  Get content from clipboard
            string[] lines = Clipboard.GetText().Split('\r');

            if (lines.Length < 65535)
            {
                wbView.PasteSpecial(PasteType.Values, PasteOperation.Add, false, false);
                return;
            }

            // get lock before pasting.
            wbView.GetLock();


            try
            {
                IRange cells = wbView.ActiveWorksheet.Cells;

                for (int k = 0; k < lines.Length; k++)
                {
                    if (!string.IsNullOrEmpty(lines[k]))
                    {
                        // get current row index
                        int row = startRow + k;

                        // split the line
                        string[] aValue = lines[k].Split('\t');

                        for (int i = 0; i < aValue.Length; i++)
                        {
                            // get current column index
                            int col = startCol + i;
                            cells[row, col].Value = aValue[col];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // If showProgressBar AndAlso frmBar IsNot Nothing Then frmBar.Close()
                wbView.ReleaseLock();
            }

        }

        
        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null)
            {
                e.CanExecute = true;
                return;
            }
        }

    }

    
    /// <summary>
    /// Defines the command binding for Delete command
    /// </summary>
    public sealed class CommandDeleteBinding : CommandBinding
    {

        public CommandDeleteBinding()
            : base(ApplicationCommands.Delete, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null)
            {
                try
                {
                    control.GetLock();
                    // clear the selection
                    if (control.RangeSelection.IsEntireRows)
                        control.RangeSelection.EntireRow.Delete();
                    else
                        control.RangeSelection.Clear();
                }
                catch { }
                finally
                {
                    control.ReleaseLock();
                }

            }
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            var control = sender as WorkbookView;

            if (control != null)
            {
                e.CanExecute = true;
            }
        }

    }

    
    /// <summary>
    /// Defines a command for uodo command in spreadsheet worksheet
    /// </summary>
    public sealed class CommandUndoBinding : CommandBinding
    {

        public CommandUndoBinding()
            : base(ApplicationCommands.Undo, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null && control.ActiveCommandManager.CanUndo)
            {
                try
                {
                    control.GetLock();
                    // clear the selection
                    control.ActiveCommandManager.Undo();
                }
                catch { }
                finally
                {
                    control.ReleaseLock();
                }

            }
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null && control.ActiveCommandManager.CanUndo)
            {
                e.CanExecute = true;
            }

        }

    }

    
    /// <summary>
    /// Defines the command binding for redo on spreadsheetgear.
    /// </summary>
    public sealed class CommandRedoBinding : CommandBinding
    {

        public CommandRedoBinding()
            : base(ApplicationCommands.Redo, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null && control.ActiveCommandManager.CanRedo)
            {
                try
                {
                    control.GetLock();
                    // clear the selection
                    control.ActiveCommandManager.Redo();
                }
                catch { }
                finally
                {
                    control.ReleaseLock();
                }

            }
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null && control.ActiveCommandManager.CanRedo)
            {
                e.CanExecute = true;
            }

        }

    }

    
    /// <summary>
    /// Defines a command binding for zoom in.
    /// </summary>
    public sealed class CommandZoomInBinding : CommandBinding
    {

        public CommandZoomInBinding()
            : base(SpreadSheetCommands.ZoomIn, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null)
            {
                control.GetLock();

                try
                {
                    double currentZoom = control.ActiveWorksheet.WindowInfo.Zoom / 10;
                    int floor = (int)Math.Floor(currentZoom) + 1;

                    if (floor >= 1 && floor <= 40)
                    {
                        control.ActiveWorksheet.WindowInfo.Zoom = floor * 10;
                    }
                }
                catch { }
                finally
                {
                    control.ReleaseLock();
                }

            }
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null)
            {
                e.CanExecute = true;
            }

        }

    }

    
    /// <summary>
    /// Defines the command binding for zoom out.
    /// </summary>
    public sealed class CommandZoomOutBinding : CommandBinding
    {

        public CommandZoomOutBinding()
            : base(SpreadSheetCommands.ZoomOut, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null)
            {
                double currentZoom = control.ActiveWorksheet.WindowInfo.Zoom / 10;
                int floor = (int)Math.Floor(currentZoom) - 1;

                if (floor >= 1 && floor <= 40)
                {
                    try
                    {
                        control.GetLock();
                        control.ActiveWorksheet.WindowInfo.Zoom = floor * 10;

                    }
                    catch { }
                    finally
                    {
                        control.ReleaseLock();
                    }
                }
            }
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var control = sender as WorkbookView;

            if (control != null)
            {
                e.CanExecute = true;
            }

        }

    }

    
    /// <summary>
    /// bold command binding.
    /// </summary>
    public sealed class BoldCommandBinding : CommandBinding
    {

        public BoldCommandBinding()
            : base(SpreadSheetCommands.Bold, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbView = Helper.FindChild<WorkbookView>(sender as DependencyObject);
            if (wbView == null) return;


            // NOTE: Must acquire a workbook set lock.
            wbView.GetLock();

            try
            {
                var selectedCells = wbView.RangeSelection;
                selectedCells.Font.Bold = !selectedCells.Font.Bold;
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

    
    /// <summary>
    /// Defines the command for formating spreadsheetgear
    /// </summary>
    public sealed class ItalicCommandBinding : CommandBinding
    {

        public ItalicCommandBinding()
            : base(SpreadSheetCommands.Italic, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbView = sender as WorkbookView;

            // NOTE: Must acquire a workbook set lock.
            wbView.GetLock();

            try
            {
                var selectedCells = wbView.RangeSelection;
                selectedCells.Font.Italic = !selectedCells.Font.Italic;
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
            var wbView = sender as WorkbookView;
            e.CanExecute = wbView != null;
        }
    }

    
    /// <summary>
    /// Defines the spreadsheet format command binding.
    /// </summary>
    public sealed class UnderlineCommandBinding : CommandBinding
    {

        public UnderlineCommandBinding()
            : base(SpreadSheetCommands.Underline, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbView = sender as WorkbookView;

            // NOTE: Must acquire a workbook set lock.
            wbView.GetLock();

            try
            {
                var selectedCells = wbView.RangeSelection;

                if (selectedCells.Font.Underline == SpreadsheetGear.UnderlineStyle.None)
                    selectedCells.Font.Underline = SpreadsheetGear.UnderlineStyle.Single;
                else
                    selectedCells.Font.Underline = SpreadsheetGear.UnderlineStyle.None;
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
            var wbView = sender as WorkbookView;

            e.CanExecute = wbView != null;
        }
    }

    
    /// <summary>
    /// Format range with percent format.
    /// </summary>
    public sealed class PercentCommandBinding : CommandBinding
    {

        public PercentCommandBinding()
            : base(SpreadSheetCommands.Percent, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbView = sender as WorkbookView;

            // NOTE: Must acquire a workbook set lock.
            wbView.GetLock();

            try
            {
                var selectedCells = wbView.RangeSelection;

                selectedCells.NumberFormat = "0%";
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
            var wbView = sender as WorkbookView;

            e.CanExecute = wbView != null;
        }
    }


    /// <summary>
    /// Format numbers in range as thousand seperator
    /// </summary>
    public sealed class ThousandSeperatorCommandBinding : CommandBinding
    {

        public ThousandSeperatorCommandBinding()
            : base(SpreadSheetCommands.ThousandSeperator, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbView = sender as WorkbookView;

            // NOTE: Must acquire a workbook set lock.
            wbView.GetLock();

            try
            {
                var selectedCells = wbView.RangeSelection;

                selectedCells.NumberFormat = "#,##0.00";
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
            var wbView = sender as WorkbookView;

            e.CanExecute = wbView != null;
        }
    }

    
    /// <summary>
    /// defines the command binding for increasing number decimals
    /// </summary>
    public sealed class IncreaseDecimalCommandBinding : CommandBinding
    {

        public IncreaseDecimalCommandBinding()
            : base(SpreadSheetCommands.IncreaseDecimal, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbView = sender as WorkbookView;

            // NOTE: Must acquire a workbook set lock.
            wbView.GetLock();

            try
            {
                var selectedCells = wbView.RangeSelection;
                selectedCells.NumberFormat = ChangeDecimalPoint(selectedCells.NumberFormat, true);
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
            var wbView = sender as WorkbookView;

            e.CanExecute = wbView != null;
        }



        internal static string ChangeDecimalPoint(string formatString, bool increase)
        {
            string newFormat;
            string percentChar = "";

            if (string.IsNullOrEmpty(formatString)) return "#,##0.00";

            if (formatString.IndexOf("%") >= 0) percentChar = "%";

            int pointPos = formatString.IndexOf(".");

            if (pointPos >= 0)
            {
                // find how many digit after the point
                int numOfDigit = 0;

                for (int i = pointPos + 1; i < formatString.Length; i++)
                {
                    if (formatString[i] == '0')
                    {
                        numOfDigit += 1;
                    }
                    else
                        break;
                }

                if (increase)
                {
                    newFormat = "#,##0." + new string('0', numOfDigit + 1) + percentChar;
                }
                else
                {
                    if (numOfDigit > 1)
                        newFormat = "#,##0." + new string('0', numOfDigit - 1) + percentChar;
                    else
                        newFormat = "#,##0" + percentChar;
                }
            }
            else
            {
                if (increase)
                {
                    newFormat = "#,##0.0" + percentChar;
                }
                else
                {
                    // return the original format string 
                    newFormat = formatString;
                }
            }

            return newFormat;

        }
    }


    /// <summary>
    /// Defines the command binding for decreaseing number decimals
    /// </summary>
    public sealed class DecreaseDecimalCommandBinding : CommandBinding
    {

        public DecreaseDecimalCommandBinding()
            : base(SpreadSheetCommands.DecreaseDecimal, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbView = sender as WorkbookView;

            // NOTE: Must acquire a workbook set lock.
            wbView.GetLock();

            try
            {
                var selectedCells = wbView.RangeSelection;
                selectedCells.NumberFormat = IncreaseDecimalCommandBinding.ChangeDecimalPoint(selectedCells.NumberFormat, false);
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
            var wbView = sender as WorkbookView;

            e.CanExecute = wbView != null;
        }

      
    }
        

    /// <summary>
    /// Defines the command binding for printing worksheet.
    /// </summary>
    public sealed class PrintCommandBinding : CommandBinding
    {

        public PrintCommandBinding()
            : base(ApplicationCommands.Print, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbView = sender as WorkbookView;
            if (wbView == null)
                return;


            // NOTE: Must acquire a workbook set lock.
            wbView.GetLock();

            try
            {
                if (e.Parameter != null && string.Compare((string)e.Parameter, "Preview", true) == 0)
                {
                    wbView.PrintPreview();
                }
                else
                {
                    wbView.Print(SpreadsheetGear.Printing.PrintWhat.Sheet, true);
                }
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
            var wbView = sender as WorkbookView;

            e.CanExecute = wbView != null;
        }


    }

    
    /// <summary>
    /// Defiens the command binding for format ranges in woeksheet
    /// </summary>
    public sealed class FormatCommandBinding : CommandBinding
    {

        public FormatCommandBinding()
            : base(SpreadSheetCommands.Format, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbView = sender as WorkbookView;
            if (wbView == null) return;



            // NOTE: Must acquire a workbook set lock.
            wbView.GetLock();

            try
            {
                // Get the active workbook set.
                var workbookSet = wbView.ActiveWorkbookSet;

                // Set up the categories to display in the Range Explorer.
                // NOTE: This enumeration can be used to limit available categories.
                var categoryFlags = SpreadsheetGear.Windows.Forms.RangeExplorerCategoryFlags.All;

                // Create the Range Explorer which operates on the current range selection.
                var explorer = new SpreadsheetGear.Windows.Forms.RangeExplorer(workbookSet, categoryFlags);

                // Display the Range Explorer to the user.
                explorer.ShowDialog();
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
            var wbView = sender as WorkbookView;

            e.CanExecute = wbView != null;
        }


    }


    /// <summary>
    /// Command binding for AutoFilter command
    /// </summary>
    public sealed class CommandAutoFilterBinding : CommandBinding
    {

        public CommandAutoFilterBinding()
            : base(SpreadSheetCommands.AutoFilter, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var wbview = Helper.FindChild<WorkbookView>(sender as DependencyObject);

            if (wbview != null)
            {
                wbview.GetLock();
                try
                {
                    wbview.RangeSelection.AutoFilter();
                }
                catch {
                    
                }
                finally
                {
                    wbview.ReleaseLock();
                }
            }
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            WorkbookView wbview = Helper.FindChild<WorkbookView>(sender as DependencyObject);
            e.CanExecute = wbview != null;
        }

    }
}
