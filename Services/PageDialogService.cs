using Simple_Stream_UWP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Simple_Stream_UWP.Services
{
    public class PageDialogService : IPageDialogService
    {
        /// <summary>
        /// Basic MessageDialog displayer.
        /// </summary>
        /// <param name="message">Message parameter of MessageDialog</param>
        /// <param name="title">Title parameter of MessageDialog. (Optional) </param>
        /// <returns></returns>
        public async Task DisplayAlertAsync(string message, string title = "")
        {
            await new MessageDialog(message, title).ShowAsync();
        }

        /// <summary>
        /// Returns the boolean result depending on the choice that user choose.
        /// </summary>
        /// <param name="question">Question that will be asked to user in the MessageDialog.</param>
        /// <param name="title">Simple MessageDialog title. (Optional)</param>
        /// <returns></returns>
        public async Task<bool> DisplayConfirmationDialogAsync(string question, string title = "")
        {
            var questionDialog = new MessageDialog(question,title);
            questionDialog.Commands.Add(new UICommand() { Id = 0, Label = "Evet" });
            questionDialog.Commands.Add(new UICommand() { Id = 1, Label = "Hayır" });
            
            var result = await questionDialog.ShowAsync();
            return result != null ? (Int32.Parse(result.Id.ToString()) == 0 ? true : false) : false;
        }
    }
}
