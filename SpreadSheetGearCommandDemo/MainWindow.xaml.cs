﻿using SpreadsheetGear;
using SpreadsheetGear.Shapes;
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
using TMTech.Shared.Commands.SpreadSheetGear;
using TMTech.Shared.Commands.ViewModel;

namespace SpreadSheetGearCommandDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SpreadSheetCommands.BindAllCommands(this);
            this.DataContext = new RangeStatus(wbview);

            var wb = SpreadsheetGear.Factory.GetWorkbook();
            wb.ActiveWorksheet.Range["a5"].Value = "Name";
            for (int i = 6; i <= 20; i++) {
                wb.ActiveWorksheet.Range["a" + i].Value = i;
            }
            wb.ActiveWorksheet.Range.Locked = false;

            wbview.ActiveWorkbook = wb;

        }


        private void Protect_Click(object sender, RoutedEventArgs e)
        {
            wbview.GetLock();
            try
            {
                wbview.ActiveWorksheet.Protect("222222");
            }
            catch { }
            finally { wbview.ReleaseLock(); }
            

        }

        private void Unprotect_Click(object sender, RoutedEventArgs e)
        {
            wbview.GetLock();
            try
            {
                wbview.ActiveWorksheet.Unprotect("222222");
            }
            catch { }
            finally { wbview.ReleaseLock(); }

        }


        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            wbview.GetLock();

            try
            {
                var sheet = wbview.ActiveWorksheet;
                //sheet.Range["a"].AutoFilter(2, "Hedge funds", AutoFilterOperator.Or, null, true);
                sheet.Range["a5"].AutoFilter(0, filterText.Text, AutoFilterOperator.Or, null, true);
            }
            catch { }
            finally { wbview.ReleaseLock(); }



        }


        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            wbview.GetLock();
            try
            {
                var sheet = wbview.ActiveWorksheet;
                //sheet.Range["a"].AutoFilter(2, "Hedge funds", AutoFilterOperator.Or, null, true);
                //sheet.Range["a5"].AutoFilter(0, filterText.Text, AutoFilterOperator.Or, null, true);

                sheet.Range["a5"].AutoFilter();
            }
            catch { }
            finally { wbview.ReleaseLock(); }

        }


        private void FilterSingleItem(SpreadsheetGear.Windows.Controls.WorkbookView workbookView)
        {
            // NOTE: Must acquire a workbook set lock.
            workbookView.GetLock();
            try
            {
                // Clear any existing filters from the active worksheet.
                workbookView.ActiveWorksheet.AutoFilter.ShowAllData();

                // Get a reference to cell A1.
                SpreadsheetGear.IRange cell =
                    workbookView.ActiveWorksheet.Cells["A1"];

                // Initialize the field index within the auto filter range.
                int field = 0;

                // Initialize criteria1 as a single product name.
                string criteria1 = "Thyme";

                // Filter by a single product.
                cell.AutoFilter(field, criteria1,
                    SpreadsheetGear.AutoFilterOperator.Or, null, true);
            }
            finally
            {
                // NOTE: Must release the workbook set lock.
                workbookView.ReleaseLock();
            }
        }



        private void FilterTopItems(SpreadsheetGear.Windows.Controls.WorkbookView workbookView)
        {
            // NOTE: Must acquire a workbook set lock.
            workbookView.GetLock();
            try
            {
                // Clear any existing filters from the active worksheet.
                workbookView.ActiveWorksheet.AutoFilter.ShowAllData();

                // Get a reference to cell A1.
                SpreadsheetGear.IRange cell =
                    workbookView.ActiveWorksheet.Cells["A1"];

                // Initialize the field index within the auto filter range.
                int field = 2;

                // Initialize criteria1 as the number of top items.
                int criteria1 = 5;

                // Filter by Q1 Top Items.
                cell.AutoFilter(field, criteria1,
                    SpreadsheetGear.AutoFilterOperator.Top10Items, null, true);
            }
            finally
            {
                // NOTE: Must release the workbook set lock.
                workbookView.ReleaseLock();
            }
        }

        private void FilterTopPercent(SpreadsheetGear.Windows.Controls.WorkbookView workbookView)
        {
            // NOTE: Must acquire a workbook set lock.
            workbookView.GetLock();
            try
            {
                // Clear any existing filters from the active worksheet.
                workbookView.ActiveWorksheet.AutoFilter.ShowAllData();

                // Get a reference to cell A1.
                SpreadsheetGear.IRange cell =
                    workbookView.ActiveWorksheet.Cells["A1"];

                // Initialize the field index within the auto filter range.
                int field = 3;

                // Initialize criteria1 as a percentage of top items.
                int criteria1 = 10;

                // Filter by Q2 Top Percent.
                cell.AutoFilter(field, criteria1,
                    SpreadsheetGear.AutoFilterOperator.Top10Percent, null, true);
            }
            finally
            {
                // NOTE: Must release the workbook set lock.
                workbookView.ReleaseLock();
            }
        }

        private void FilterBottomItems(SpreadsheetGear.Windows.Controls.WorkbookView workbookView)
        {
            // NOTE: Must acquire a workbook set lock.
            workbookView.GetLock();
            try
            {
                // Clear any existing filters from the active worksheet.
                workbookView.ActiveWorksheet.AutoFilter.ShowAllData();

                // Get a reference to cell A1.
                SpreadsheetGear.IRange cell =
                    workbookView.ActiveWorksheet.Cells["A1"];

                // Initialize the field index within the auto filter range.
                int field = 4;

                // Initialize criteria1 as the number of bottom items.
                int criteria1 = 7;

                // Filter by Q3 Bottom Items.
                cell.AutoFilter(field, criteria1,
                    SpreadsheetGear.AutoFilterOperator.Bottom10Items, null, true);
            }
            finally
            {
                // NOTE: Must release the workbook set lock.
                workbookView.ReleaseLock();
            }
        }

        private void FilterBottomPercent(SpreadsheetGear.Windows.Controls.WorkbookView workbookView)
        {
            // NOTE: Must acquire a workbook set lock.
            workbookView.GetLock();
            try
            {
                // Clear any existing filters from the active worksheet.
                workbookView.ActiveWorksheet.AutoFilter.ShowAllData();

                // Get a reference to cell A1.
                SpreadsheetGear.IRange cell =
                    workbookView.ActiveWorksheet.Cells["A1"];

                // Initialize the field index within the auto filter range.
                int field = 5;

                // Initialize criteria1 as a percentage of bottom items.
                int criteria1 = 10;

                // Filter by Q4 Bottom Percent.
                cell.AutoFilter(field, criteria1,
                    SpreadsheetGear.AutoFilterOperator.Bottom10Percent, null, true);
            }
            finally
            {
                // NOTE: Must release the workbook set lock.
                workbookView.ReleaseLock();
            }
        }

        private void FilterGreaterThan(SpreadsheetGear.Windows.Controls.WorkbookView workbookView)
        {
            // NOTE: Must acquire a workbook set lock.
            workbookView.GetLock();
            try
            {
                // Clear any existing filters from the active worksheet.
                workbookView.ActiveWorksheet.AutoFilter.ShowAllData();

                // Get a reference to cell A1.
                SpreadsheetGear.IRange cell =
                    workbookView.ActiveWorksheet.Cells["A1"];

                // Initialize the field index within the auto filter range.
                int field = 2;

                // Initialize criteria1 using the greater than operator.
                string criteria1 = ">1850";

                // Filter by Q1 Greater Than.
                cell.AutoFilter(field, criteria1,
                    SpreadsheetGear.AutoFilterOperator.Or, null, true);
            }
            finally
            {
                // NOTE: Must release the workbook set lock.
                workbookView.ReleaseLock();
            }
        }

        private void FilterBetween(SpreadsheetGear.Windows.Controls.WorkbookView workbookView)
        {
            // NOTE: Must acquire a workbook set lock.
            workbookView.GetLock();
            try
            {
                // Clear any existing filters from the active worksheet.
                workbookView.ActiveWorksheet.AutoFilter.ShowAllData();

                // Get a reference to cell A1.
                SpreadsheetGear.IRange cell =
                    workbookView.ActiveWorksheet.Cells["A1"];

                // Initialize the field index within the auto filter range.
                int field = 3;

                // Initialize criteria1 using the greater than or equal to operator.
                string criteria1 = ">=1200";

                // Initialize criteria2 using the less than or equal to operator.
                string criteria2 = "<=1400";

                // Filter by Q2 Between.
                cell.AutoFilter(field, criteria1,
                    SpreadsheetGear.AutoFilterOperator.And, criteria2, true);
            }
            finally
            {
                // NOTE: Must release the workbook set lock.
                workbookView.ReleaseLock();
            }
        }

        private void ShowAllData(SpreadsheetGear.Windows.Controls.WorkbookView workbookView)
        {
            // NOTE: Must acquire a workbook set lock.
            workbookView.GetLock();
            try
            {
                // Get a reference to the AutoFilter from the worksheet.
                SpreadsheetGear.IAutoFilter autoFilter =
                    workbookView.ActiveWorksheet.AutoFilter;

                // Clear filters to show all data in the auto filter range.
                autoFilter.ShowAllData();
            }
            finally
            {
                // NOTE: Must release the workbook set lock.
                workbookView.ReleaseLock();
            }
        }


        private void FillColorScheama(SpreadsheetGear.Windows.Controls.WorkbookView workbookView, SpreadsheetGear.Themes.ColorSchemeIndex seedColor)
        {
            // NOTE: Must acquire a workbook set lock.
            workbookView.GetLock();
            try
            {
                var rng = workbookView.RangeSelection;

                double tint = -0.9;
                int offset = 0;

                //seedColor = SpreadsheetGear.Themes.ColorSchemeIndex.Accent2;

                do
                {
                    rng.Offset(offset, 0).Interior.ThemeColor = seedColor; // SpreadsheetGear.Themes.ColorSchemeIndex.Accent1;
                    rng.Offset(offset, 0).Value = (int)seedColor;
                    rng.Offset(offset, 0).Interior.TintAndShade = tint;
                    offset++;
                    tint += 0.1;

                } while (tint <= 0.9);

            }
            finally
            {
                // NOTE: Must release the workbook set lock.
                workbookView.ReleaseLock();
            }

            //workbookView.GetLock();

            //// Use theme colors in combination with "tint and shade" to demonstrate creating a fuller range of colors.
            //SpreadsheetGear.Themes.ColorSchemeIndex[] seedColors = {
            //        SpreadsheetGear.Themes.ColorSchemeIndex.Accent1,
            //        SpreadsheetGear.Themes.ColorSchemeIndex.Accent2,
            //        SpreadsheetGear.Themes.ColorSchemeIndex.Accent3,
            //        SpreadsheetGear.Themes.ColorSchemeIndex.Accent4,
            //        SpreadsheetGear.Themes.ColorSchemeIndex.Accent5,
            //        SpreadsheetGear.Themes.ColorSchemeIndex.Accent6
            //    };

            //// Iterate through each theme color and set some range based off startingRange.
            //SpreadsheetGear.IRange startingRange = workbookView.ActiveWorksheet.Cells["B4:B17"];
            //for (int i = 0; i < seedColors.Length; i++)
            //{
            //    SpreadsheetGear.IRange currentRange = startingRange.Offset(0, i);

            //    // Set the whole range to use the specified theme color.
            //    currentRange.Interior.ThemeColor = seedColors[i];

            //    // TintAndShade values of 1.0 and -1.0 indicate white and black, respectively.  
            //    // Setting limits here to avoid displaying these max B&W values.
            //    double brightnessLimit = 0.9;
            //    double darknessLimit = -0.9;

            //    // Gradually darken the theme color as we iterate down the range
            //    for (int j = 0; j < currentRange.RowCount; j++)
            //    {
            //        currentRange[j, 0].Interior.TintAndShade = brightnessLimit - (((double)j / (double)currentRange.RowCount) * (brightnessLimit - darknessLimit));
            //    }
            //}

            //workbookView.ReleaseLock();
        }

        private void ColorScheama_Click(object sender, RoutedEventArgs e)
        {
            SpreadsheetGear.Themes.ColorSchemeIndex colorSchemaIndex;
            if (Enum.TryParse<SpreadsheetGear.Themes.ColorSchemeIndex>(this.seedcolor.Text, out colorSchemaIndex))
            {
                FillColorScheama(this.wbview, colorSchemaIndex);
            }
            else
            {
                MessageBox.Show("Invalid color schema index");
            }


            
        }

        private void AddCheckbox_Click(object sender, RoutedEventArgs e)
        {
            wbview.GetLock();
            AddCheckBox(4, 3);
            wbview.ReleaseLock();

        }


        protected IShape AddCheckBox(int row, int col, HAlign align = HAlign.Left)
        {

            var sheet = this.wbview.ActiveWorksheet;
            var cell = sheet.Cells[row, col];
            cell.Interior.Color = SpreadsheetGear.Colors.Azure;
            cell.Value = "adsfas";
            //Add check box in sheet
            //Get a reference to the active worksheet and worksheet window info.
            var windowInfo = sheet.WindowInfo;

            double drow = row;
            double dcol = col;

            // Calculate the left, top, width and height of the checkbox by 
            //converting row and column coordinates to points.  Use fractional 
            //  values to get coordinates in between row and column boundaries.
            double left = windowInfo.ColumnToPoints(dcol + 0.0);
            double top = windowInfo.RowToPoints(drow + 0.0);
            double right = windowInfo.ColumnToPoints(dcol + 1);
            double bottom = windowInfo.RowToPoints(drow + 1);
            double width = right - left;
            double height = bottom - top;

            //double left = windowInfo.ColumnToPoints(1.1);
            //double top = windowInfo.RowToPoints(1.1);
            //double right = windowInfo.ColumnToPoints(3.9);
            //double bottom = windowInfo.RowToPoints(2.9);
            //double width = right - left;
            //double height = bottom - top;

            //Add the checkbox using the calculated bounds.
            var shape = sheet.Shapes.AddFormControl(FormControlType.CheckBox, left, top, width, height);
            //shape.TextFrame.


            // get linked cell which will be linked to a visiable cell and a check box
            //var linkedCell = sheet.Cells[row, col + 5];
            //cell.Formula = "=" + linkedCell.Address;
            //cell.Locked = true;     // lock this cell
            cell.Font.Color = SpreadsheetGear.Colors.White;

            // Link the checkbox to linked cell.
            //shape.ControlFormat.LinkedCell = linkedCell.Address;

            //Set the text of the checkbox.
            shape.TextFrame.Characters.Text = null;

            sheet.Cells[0, 0].Value = sheet.Shapes.Count;

            return shape;
        }
    }



    /*
    * Custom control used to replace default row header content.
    */
    public class CustomRowHeaderContent : System.Windows.Controls.ContentControl
    {
        public CustomRowHeaderContent()
        {
            DefaultStyleKey = typeof(CustomRowHeaderContent);
        }

        // RowIndex dependency property (for use from XAML).
        public static readonly System.Windows.DependencyProperty RowIndexProperty =
            System.Windows.DependencyProperty.Register("RowIndex",
            typeof(int),
            typeof(CustomRowHeaderContent),
            new System.Windows.PropertyMetadata(OnRowIndexPropertyChanged));

        // WorkbookView dependency property (for use from XAML).
        public static readonly System.Windows.DependencyProperty WorkbookViewProperty =
            System.Windows.DependencyProperty.Register("WorkbookView",
            typeof(SpreadsheetGear.Windows.Controls.WorkbookView),
            typeof(CustomRowHeaderContent),
            new System.Windows.PropertyMetadata(OnWorkbookViewPropertyChanged));

        // RowIndex property.
        public int RowIndex
        {
            get { return (int)GetValue(RowIndexProperty); }
            set { SetValue(RowIndexProperty, value); }
        }

        // WorkbookView property.
        public SpreadsheetGear.Windows.Controls.WorkbookView WorkbookView
        {
            get { return GetValue(WorkbookViewProperty) as SpreadsheetGear.Windows.Controls.WorkbookView; }
            set { SetValue(WorkbookViewProperty, value); }
        }

        // RowIndex property changed handler.
        private static void OnRowIndexPropertyChanged(
            System.Windows.DependencyObject d, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            var rowHeaderContent = d as CustomRowHeaderContent;
            if (rowHeaderContent != null)
            {
                // Update the data state.
                rowHeaderContent.CheckDataState();
            }
        }

        // WorkbookView property changed handler.
        private static void OnWorkbookViewPropertyChanged(
            System.Windows.DependencyObject d, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            var rowHeaderContent = d as CustomRowHeaderContent;
            if (rowHeaderContent != null)
            {
                var oldView = e.OldValue as SpreadsheetGear.Windows.Controls.WorkbookView;
                var newView = e.NewValue as SpreadsheetGear.Windows.Controls.WorkbookView;

                // If the workbook view has changed...
                if (oldView != newView)
                {
                    // Set up a Calculate event handler to update the data state.
                    if (newView != null)
                    {
                        newView.Calculate +=
                            new SpreadsheetGear.Windows.Controls.CalculateEventHandler(rowHeaderContent.workbookView_Calculate);
                    }

                    // Remove any previous Calculate event handler.
                    if (oldView != null)
                    {
                        newView.Calculate -=
                            new SpreadsheetGear.Windows.Controls.CalculateEventHandler(rowHeaderContent.workbookView_Calculate);
                    }
                }

                // Update the data state.
                rowHeaderContent.CheckDataState();
            }
        }

        // WorkbookView Calculate event handler.
        private void workbookView_Calculate(object sender,
            SpreadsheetGear.Windows.Controls.CalculateEventArgs e)
        {
            // Update the data state.
            CheckDataState();
        }

        private void CheckDataState()
        {
            // Ignore if we don't yet have a workbook view reference.
            if (WorkbookView == null)
                return;

            // NOTE: Must acquire a workbook set lock.
            WorkbookView.GetLock();
            try
            {
                // Get a reference to the advanced IValues interface for the worksheet.
                var values = WorkbookView.ActiveWorksheet as SpreadsheetGear.Advanced.Cells.IValues;

                // DataState defaults to none.
                string dataState = "None";

                // Get various values from the worksheet.
                var rowIndex = RowIndex;
                var store = values[rowIndex, 0];
                var curSales = values[rowIndex, 5];
                var lastSales = values[rowIndex, 6];

                // If we have valid sales entries...
                if (store != null && curSales != null && lastSales != null)
                {
                    // Determine if sales were positive or negative...
                    if (curSales.Number >= lastSales.Number)
                        dataState = "Positive";
                    else
                        dataState = "Negative";
                }

                // Transition the data state.
                System.Windows.VisualStateManager.GoToState(this, dataState, true);
            }
            finally
            {
                // NOTE: Must release the workbook set lock.
                WorkbookView.ReleaseLock();
            }
        }
    }
}

