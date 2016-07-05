using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMTech.Shared.Commands.Interfaces
{

    /// <summary>
    /// To use the commands binding, the control must implement the IEditable interface.
    /// </summary>
    public interface IEditable
    {
        void Copy();

        bool CanCopy();

        bool CanPaste();

        void Paste();

        bool CanDelete();

        void Delete();

    }
}
