using SpreadsheetGear;
using SpreadsheetGear.Windows.Controls;
using System;
using System.Reflection;
using System.Windows.Input;

namespace TMTech.Shared.SpreadSheetGear
{

    /// <summary>
    /// Hide unused ranges ( both entire rows and columns )
    /// </summary>
    public static class ExcelHelper
    {
        public const int MAX_EXCEL_ROWS = 1048575;
        public const int MAX_EXCEL_COLUMNS = 16383;


        public static void HideUnusedArea(IWorksheet sheet)
        {
            var usedRange = sheet.UsedRange;

            sheet.Cells[0, 0, 0, Math.Max(usedRange.ColumnCount - 1, 0)].EntireColumn.Hidden = false;
            sheet.Cells[0, usedRange.ColumnCount, 0, MAX_EXCEL_COLUMNS].EntireColumn.Hidden = true;

            sheet.Cells[0, 0, Math.Max(usedRange.RowCount - 1, 0), 0].EntireRow.Hidden = false;
            sheet.Cells[usedRange.RowCount, 0, MAX_EXCEL_ROWS, 0].EntireRow.Hidden = true;

            //sheet.Cells[0, 0].Activate();

        }



        /// <summary>
        /// Hide unsed columns on the right side.
        /// </summary>
        /// <param name="sheet"></param>
        public static void HideRightUnusedColumns(IWorksheet sheet, int columnIndex)
        {
            //var usedRange = sheet.UsedRange;
            sheet.Cells[0, columnIndex, 0, MAX_EXCEL_COLUMNS].EntireColumn.Hidden = true;
        }


        /// <summary>
        /// Set the stripe ackground color for used area
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="stripeColor"></param>
        public static void SetRangeStrikeBack(IRange rng, System.Windows.Media.Color stripeColor)
        {
            if (rng.RowCount == 0 || rng.ColumnCount == 0) return;

            // get a reference to the range
            var conditions = rng.FormatConditions;

            // delete any existing conditional fomattings.
            conditions.Delete();

            // add a condition
            var condition = conditions.Add(SpreadsheetGear.FormatConditionType.Expression, FormatConditionOperator.None, "=MOD(Row(), 2)=0", null);
            //condition.Interior.Color = Color.FromArgb(0xe5, 0xff, 0xff);
            condition.Interior.Color = Color.FromArgb(stripeColor.R, stripeColor.G, stripeColor.B);  // Color.FromArgb(0xac, 0xf0, 0xf2);

            //condition.Interior.ThemeColor = SpreadsheetGear.Themes.ColorSchemeIndex.
            //condition.Interior.TintAndShade = 0.9;
        }

        /// <summary>
        /// Set the stripe ackground color for used area
        /// </summary>
        /// <param name="rng"></param>
        /// <param name="seedColor">theme schema index</param>
        /// <param name="bright">-0.9 t0 0.9</param>
        public static void SetRangeStrikeBack(IRange rng, SpreadsheetGear.Themes.ColorSchemeIndex seedColor, double tintAndShade, bool deleteExisting = true)
        {
            if (rng.RowCount == 0 || rng.ColumnCount == 0) return;

            rng.Interior.ThemeColor = seedColor;
            rng.Interior.TintAndShade = tintAndShade;

            // get a reference to the range
            var conditions = rng.FormatConditions;

            // delete any existing conditional fomattings.
            if (deleteExisting) conditions.Delete();

            // add a condition
            var condition = conditions.Add(SpreadsheetGear.FormatConditionType.Expression, FormatConditionOperator.None, "=MOD(Row(), 2)=1", null);
            //condition.Interior.Color = Color.FromArgb(0xe5, 0xff, 0xff);
            condition.Interior.Color = Colors.White;


            //condition.Interior.ThemeColor = seedColor;  
            //condition.Interior.TintAndShade = tintAndShade;
        }


        public static void SetUsedRangeVerticalBorder(IWorksheet sheet)
        {
            IRange rng = sheet.UsedRange;
            if (rng.RowCount == 0 || rng.ColumnCount == 0) return;

            var border = rng.Borders[BordersIndex.InsideVertical];
            border.LineStyle = LineStyle.Continous;
            border.Weight = BorderWeight.Thin;
            border.Color = Colors.LightGray;

            //for (int i = 0; i < rng.ColumnCount; i++)
            //{
            //    var border = sheet.Cells[0, i, rng.RowCount, i].Borders[BordersIndex.EdgeRight];
            //    border.LineStyle = LineStyle.Continous;
            //    border.Weight = BorderWeight.Thin;
            //    border.Color = Colors.LightGray;
            //}
        }

        public static void SetUsedRangeHorizonalBorder(IWorksheet sheet)
        {
            IRange rng = sheet.UsedRange;
            if (rng.RowCount == 0 || rng.ColumnCount == 0) return;

            var border = rng.Borders[BordersIndex.InsideHorizontal];
            border.LineStyle = LineStyle.Continous;
            border.Weight = BorderWeight.Thin;
            border.Color = Colors.LightGray;
        }


        public static void SetRangeHorizonalBorder(IRange rng)
        {
            if (rng.RowCount == 0 || rng.ColumnCount == 0) return;
            var border = rng.Borders[BordersIndex.InsideHorizontal];
            SetBorder(border);
        }

        public static void SetRangeEdgeBorder(IRange range, bool top = true, bool bottom = true, bool left = true, bool right = true)
        {
            if (range.RowCount == 0 || range.ColumnCount == 0) return;
            if (top) SetBorder(range.Borders[BordersIndex.EdgeTop]);
            if (bottom) SetBorder(range.Borders[BordersIndex.EdgeBottom]);
            if (left) SetBorder(range.Borders[BordersIndex.EdgeLeft]);
            if (right) SetBorder(range.Borders[BordersIndex.EdgeRight]);
        }

        private static void SetBorder(IBorder border)
        {
            border.LineStyle = LineStyle.Continous;
            border.Weight = BorderWeight.Thin;
            border.Color = Colors.LightGray;
        }



        /// <summary>
        /// Check if the property can be edit in worksheet. Only allow to edit simple properties.
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        private static bool IsEditable(PropertyInfo pinfo)
        {
            if (pinfo.CanWrite && pinfo.GetSetMethod() != null)
            {
                Type t = pinfo.PropertyType;
                if (t == typeof(string) || pinfo.PropertyType.IsEnum
                    || t == typeof(double) || t == typeof(double?)
                    || t == typeof(int) || t == typeof(int?)
                    || t == typeof(decimal) || t == typeof(decimal?)
                    || t == typeof(bool) || t == typeof(bool?)
                    || t == typeof(DateTime) || t == typeof(DateTime?))
                {
                    return true;
                }
            }

            return false;
        }


        public delegate void ActionWithLocking();
        
        /// <summary>
        /// Run a function which requires to lock the WorkbookView.
        /// </summary>
        /// <param name="workbookView"></param>
        /// <param name="method"></param>
        public static void RunWithLock(WorkbookView workbookView, ActionWithLocking method)
        {
            workbookView.GetLock();
            try
            {
                method.DynamicInvoke();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                workbookView.ReleaseLock();
            }

        }


        /// <summary>
        /// Get excel column name from index
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        public static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }


        /// <summary>
        /// Get the proeprty value info
        /// </summary>
        /// <param name="pinfo"></param>
        /// <returns></returns>
        public static string GetPropertyValueComment(PropertyInfo pinfo)
        {
            var t = pinfo.PropertyType;

            if (t.IsEnum)
                return "Valid Values:\n" + string.Join("\n", t.GetEnumNames());
            else if (t == typeof(int) || t == typeof(double))
                return "Invalid numeric value.";
            else if (t == typeof(DateTime))
                return "Invalid date value.";

            return "Invalud value.";

        }


        public static void MakeRangeStripeColor(IRange range, Color color)
        {
            // get the range condition collection.
            var conditions = range.FormatConditions;

            // delete any existing formats
            conditions.Delete();

            // add a condition to flag cell is changed
            //var condition = conditions.Add(FormatConditionType.Expression, FormatConditionOperator.Equal, "=AND(MOD(ROW(), 2)=0, sheet3!c2=1)", null);
            //condition.Font.Color = Colors.Salmon;
            //condition.Interior.Color = Colors.PaleTurquoise;
            //condition.Font.Bold = true;

            //condition = conditions.Add(FormatConditionType.Expression, FormatConditionOperator.Equal, "=AND(MOD(ROW(), 2)=1, sheet3!c2=1)", null);
            //condition.Font.Color = Colors.Salmon;
            //condition.Font.Bold = true;

            //add another condiction for stripe row table style.
            var condition = conditions.Add(FormatConditionType.Expression, FormatConditionOperator.Equal, "=MOD(ROW(), 2)=0", null);
            condition.Interior.Color = color; // Colors.PaleTurquoise;
        }


        /// <summary>
        /// Convert windows coloro to spreadsheetcolor space.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color ConvertToExcelColor(System.Windows.Media.Color color)
        {
            return Color.FromArgb(color.R, color.G, color.B);
        }



        /// <summary>
        /// Set workbook view to report mode. will not be able to change.
        /// </summary>
        /// <param name="wbview"></param>
        public static void SetToReportMode(WorkbookView wbview)
        {
            wbview.RangeSelectionBorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);

            wbview.PreviewKeyDown += Wbview_PreviewKeyDown;
        }


        private static void Wbview_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == Key.Tab || e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Return || e.Key == Key.PageDown || e.Key == Key.PageUp || e.Key == Key.Home || e.Key == Key.End)
                return;

            if (e.Key == System.Windows.Input.Key.C && e.KeyboardDevice.Modifiers == System.Windows.Input.ModifierKeys.Control)
                return;

            e.Handled = true;
        }


        /// <summary>
        /// Get double value from specified cell in worksheet.
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static double? GetDouble(IWorksheet sheet, string address)
        {
            object value = sheet.Range[address].Value;

            if (value == null)
                return null;

            string svalue = value.ToString();

            double result;
            if (double.TryParse(svalue, out result))
            {
                return result;
            }


            if (double.TryParse(svalue.Substring(0, svalue.Length - 1), out result))
            {
                if (svalue.EndsWith("m", StringComparison.OrdinalIgnoreCase))
                    return result * 1000000;
                else if (svalue.EndsWith("k", StringComparison.OrdinalIgnoreCase))
                    return result * 1000;
                else if (svalue.EndsWith("%", StringComparison.OrdinalIgnoreCase))
                    return result * 0.01;
                else if (svalue.EndsWith("b", StringComparison.OrdinalIgnoreCase))
                    return result * 1000000000;
            }

            return null;
        }



    }

    
 
}
