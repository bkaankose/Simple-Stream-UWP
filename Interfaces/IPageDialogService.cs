using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Stream_UWP.Interfaces
{
    public interface IPageDialogService
    {
        Task DisplayAlertAsync(string message, string title = "");
        Task<bool> DisplayConfirmationDialogAsync(string question,string title = "");
    }
}
