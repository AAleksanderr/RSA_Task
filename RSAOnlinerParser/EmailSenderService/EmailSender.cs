using Microsoft.Office.Interop.Outlook;
using Preferences;
using Preferences.Interfaces;

namespace EmailSenderService
{
    public class EmailSender : IEmailSenderService
    {
        public void Send()
        {
            var app = new Application();
            MailItem mailItem = app.CreateItem(OlItemType.olMailItem);
            mailItem.Subject = EmailSenderPreferences.EmailSubject;
            mailItem.To = EmailSenderPreferences.EmailAddress;
            mailItem.Body = EmailSenderPreferences.EmailMessageBody;
            var attachedFilePath = CreateExcelDocPreferences.CreatedDocumentDirectory +
                                   CreateExcelDocPreferences.CreatedDocumentName;
            mailItem.Attachments.Add(attachedFilePath);
            mailItem.Importance = OlImportance.olImportanceHigh;
            mailItem.Send();
            mailItem.Display(false);
        }
    }
}