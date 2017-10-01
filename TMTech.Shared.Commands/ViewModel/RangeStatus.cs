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
        private bool mBold;
        public bool Bold { get { return mBold; }
            private set
            {
                if (mBold != value)
                {
                    mBold = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool mItalic;
        public bool Italic { get { return mItalic; }
            private set
            {
                if (mItalic != value)
                {
                    mItalic = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public bool mUnderline;
        public bool Underline { get { return mUnderline; }
            private set
            {
                if (mUnderline != value)
                {
                    mUnderline = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private bool mLocked;
        public bool Locked { get { return mLocked; }
            private set
            {
                if (mLocked != value )
                {
                    mLocked = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //private double mZoomLevel;
        public double ZoomLevel
        {
            get
            {
                return (double)_WorkbookView.ActiveWorksheet.WindowInfo.Zoom / 10;
            }
            set
            {
                int level = (int)value * 10;
                if (level >= 10 && level <= 400 && level != _WorkbookView.ActiveWorksheet.WindowInfo.Zoom)
                {
                    _WorkbookView.GetLock();
                    try
                    {
                        _WorkbookView.ActiveWorksheet.WindowInfo.Zoom = level;
                        NotifyPropertyChanged();
                    }
                    catch {}

                    finally { _WorkbookView.ReleaseLock(); }
                  
                    
                    
                }
            }
        }


        private HAlign mHorizontalAlignment;
        public bool HorizontalAlignmentLeft { get { return mHorizontalAlignment == HAlign.Left; } }
        public bool HorizontalAlignmentCenter { get { return mHorizontalAlignment == HAlign.Center; } }
        public bool HorizontalAlignmentRight { get { return mHorizontalAlignment == HAlign.Right; } }
        public HAlign HorizontalAlignment { get { return mHorizontalAlignment; }
            private set
            {
                if (mHorizontalAlignment != value)
                {
                    mHorizontalAlignment = value;
                    NotifyPropertyChanged();
                    OnPropertyChanged(nameof(HorizontalAlignmentLeft));
                    OnPropertyChanged(nameof(HorizontalAlignmentCenter));
                    OnPropertyChanged(nameof(HorizontalAlignmentRight));
                }
            }
        }


        private VAlign mVerticalAlignment;
        public bool VerticalAlignmentTop { get { return mVerticalAlignment == VAlign.Top; } }
        public bool VerticalAlignmentCenter { get { return mVerticalAlignment == VAlign.Center; } }
        public bool VerticalAlignmentBottom { get { return mVerticalAlignment == VAlign.Bottom; } }
        public VAlign VerticalAlignment { get { return mVerticalAlignment; }
            private set
            {
                if (mVerticalAlignment != value)
                {
                    mVerticalAlignment = value;
                    NotifyPropertyChanged();
                    OnPropertyChanged(nameof(VerticalAlignmentTop));
                    OnPropertyChanged(nameof(VerticalAlignmentCenter));
                    OnPropertyChanged(nameof(VerticalAlignmentBottom));
                }
            }
        }


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
            _WorkbookView.ActiveTabChanged += _WorkbookView_ActiveTabChanged;
        }


        private void _WorkbookView_ActiveTabChanged(object sender, ActiveTabChangedEventArgs e)
        {
            OnPropertyChanged(nameof(ZoomLevel));
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
            VerticalAlignment = _WorkbookView.RangeSelection.VerticalAlignment;
        }
    }
}
