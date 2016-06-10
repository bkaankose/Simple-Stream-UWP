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
        /// 
        private bool isContentDisplaying = false;
        public async Task DisplayAlertAsync(string message, string title = "")
        {
            if (isContentDisplaying)
                return;

            isContentDisplaying = true;
            await new MessageDialog(message, title).ShowAsync();
            isContentDisplaying = false;
        }

        /// <summary>
        /// Returns the boolean result depending on the choice that user choose.
        /// </summary>
        /// <param name="question">Question that will be asked to user in the MessageDialog.</param>
        /// <param name="title">Simple MessageDialog title. (Optional)</param>
        /// <returns></returns>
        public async Task<bool> DisplayConfirmationDialogAsync(string question, string title = "")
        {
            if (isContentDisplaying)
                return false;                

            isContentDisplaying = true;
            var questionDialog = new MessageDialog(question,title);
            questionDialog.Commands.Add(new UICommand() { Id = 0, Label = "Yes" });
            questionDialog.Commands.Add(new UICommand() { Id = 1, Label = "No" });
            
            var result = await questionDialog.ShowAsync();
            isContentDisplaying = false;
            return result != null ? (Int32.Parse(result.Id.ToString()) == 0 ? true : false) : false;
        }
    }
}
