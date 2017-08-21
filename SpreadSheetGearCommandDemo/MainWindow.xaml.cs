using SpreadsheetGear;
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
                sheet.Range["a5"].AutoFilter(0, "9", AutoFilterOperator.Or, null, true);
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


    }
}
