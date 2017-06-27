using SpreadsheetGear;
using SpreadsheetGear.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMTech.Shared.Commands.ViewModel
{
    public class RangeStatus : BaseViewModel
    {
        private WorkbookView _WorkbookView;


        #region properties
        public bool Bold { get; private set; }

        public bool Italic { get; private set; }

        public bool Underline { get; private set; }

        public bool Locked { get; private set; }

        public HAlign HorizontalAlignment { get; private set; }

        public bool HorizontalAlignmentLeft { get; private set; }

        public bool HorizontalAlignmentCenter { get; private set; }

        public bool HorizontalAlignmentRight { get; private set; }

        public VAlign VerticalAlignment { get; private set; }
        public bool VerticalAlignmentTop { get; private set; }
        public bool VerticalAlignmentCenter { get; private set; }
        public bool VerticalAlignmentBottom { get; private set; }


        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="view"></param>
        public RangeStatus(WorkbookView view)
        {
            _WorkbookView = view;

            _WorkbookView.RangeSelectionChanged += _WorkbookView_RangeSelectionChanged;
            _WorkbookView.RangeChanged += _WorkbookView_RangeChanged;
        }

        private void _WorkbookView_RangeChanged(object sender, RangeChangedEventArgs e)
        {
            _WorkbookView_RangeSelectionChanged(null, null);
        }

        private void _WorkbookView_RangeSelectionChanged(object sender, RangeSelectionChangedEventArgs e)
        {
            Bold = _WorkbookView.RangeSelection.Font.Bold;
            Italic = _WorkbookView.RangeSelection.Font.Italic;
            Underline = _WorkbookView.RangeSelection.Font.Underline != SpreadsheetGear.UnderlineStyle.None;
            Locked = _WorkbookView.RangeSelection.Locked;
            HorizontalAlignment = _WorkbookView.RangeSelection.HorizontalAlignment;
            HorizontalAlignmentLeft = _WorkbookView.RangeSelection.HorizontalAlignment == HAlign.Left;
            HorizontalAlignmentCenter = _WorkbookView.RangeSelection.HorizontalAlignment == HAlign.Center;
            HorizontalAlignmentRight = _WorkbookView.RangeSelection.HorizontalAlignment == HAlign.Right;
            VerticalAlignment = _WorkbookView.RangeSelection.VerticalAlignment;
            VerticalAlignmentTop = _WorkbookView.RangeSelection.VerticalAlignment == VAlign.Top;
            VerticalAlignmentCenter = _WorkbookView.RangeSelection.VerticalAlignment == VAlign.Center;
            VerticalAlignmentBottom = _WorkbookView.RangeSelection.VerticalAlignment == VAlign.Bottom;

            OnPropertyChanged(nameof(Bold));
            OnPropertyChanged(nameof(Italic));
            OnPropertyChanged(nameof(Underline));
            OnPropertyChanged(nameof(Locked));

            OnPropertyChanged(nameof(HorizontalAlignment));
            OnPropertyChanged(nameof(HorizontalAlignmentLeft));
            OnPropertyChanged(nameof(HorizontalAlignmentCenter));
            OnPropertyChanged(nameof(HorizontalAlignmentRight));

            OnPropertyChanged(nameof(VerticalAlignment));
            OnPropertyChanged(nameof(VerticalAlignmentTop));
            OnPropertyChanged(nameof(VerticalAlignmentCenter));
            OnPropertyChanged(nameof(VerticalAlignmentBottom));
        }
    }
}
