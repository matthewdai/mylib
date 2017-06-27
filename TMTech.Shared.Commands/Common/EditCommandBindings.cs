using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TMTech.Shared.Commands.Interfaces;

namespace TMTech.Shared.Commands.Common
{



    /// <summary>
    /// Defines the command binding for application command COPY
    /// </summary>
    public sealed class CommandCopyBinding : CommandBinding
    {
        public static RoutedCommand Format = new RoutedCommand();



        public CommandCopyBinding()
            : base(ApplicationCommands.Copy, OnExecute, OnCanExecute)
        { }


        static void OnExecute(object sender, ExecutedRoutedEventArgs e)
        {
            IEditable control = sender as IEditable;

            if (control != null)
            {
                control.Copy();
                return;
            }
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            IEditable control = sender as IEditable;

            if (control != null)
            {
                e.CanExecute = control.CanCopy();
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
            IEditable control = sender as IEditable;

            if (control != null)
            {
                control.Paste();
                return;
            }
            
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            IEditable control = sender as IEditable;

            if (control != null)
            {
                e.CanExecute = control.CanPaste();
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
            var editableControl = sender as IEditable;

            if (editableControl != null)
            {
                // remove selected items
                editableControl.Delete();

            }
        }


        static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var editableControl = sender as IEditable;

            if (editableControl != null)
            {
                e.CanExecute = editableControl.CanDelete();
            }

        }

    }



}
