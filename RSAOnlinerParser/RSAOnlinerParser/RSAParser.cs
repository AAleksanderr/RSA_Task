using System;
using System.Collections.Generic;
using CreateExcelDocumentService;
using EmailSenderService;
using NLog;
using Preferences;
using Preferences.Interfaces;
using Preferences.Models;
using WebParseService;

namespace RSAOnlinerParser
{
    public class RsaParser
    {
        private static readonly Logger AppLogger = LogManager.GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            if (args.Length > 0) ParsePreferences.DriverUrl = args[0];

            var items = GetSoughtItems(new WebParser());

            CreateExcelDocument(items, new ExcelDocumentCreator());

            SendDocumentViaEmail(new EmailSender());

            AppLogger.Info("Work complete.");
        }

        private static void SendDocumentViaEmail(IEmailSenderService senderService)
        {
            AppLogger.Info($"Sending document to email address {EmailSenderPreferences.EmailAddress}.");
            try
            {
                senderService.Send();
            }
            catch (Exception)
            {
                AppLogger.Info(
                    $"Сonfirmation of sending is required or can't send document to email address {EmailSenderPreferences.EmailAddress}.");
                return;
            }

            AppLogger.Info("Sending complete.");
        }

        private static void CreateExcelDocument(IEnumerable<SoughtItem> items,
            ICreateExcelDocumentService createExcelDocumentService)
        {
            var path = CreateExcelDocPreferences.CreatedDocumentDirectory;
            var name = CreateExcelDocPreferences.CreatedDocumentName;
            AppLogger.Info($"Creating file {path}{name}");
            try
            {
                createExcelDocumentService.Create(items);
                AppLogger.Info("Creating file complete.");
            }
            catch (Exception)
            {
                AppLogger.Error($"Can't create file {path}{name}");
            }
        }

        private static IEnumerable<SoughtItem> GetSoughtItems(IWebParseService parserService)
        {
            AppLogger.Info("Getting items from parser service.");
            try
            {
                var items = parserService.GetItems();
                AppLogger.Info("Getting items completed.");
                return items;
            }
            catch (Exception)
            {
                AppLogger.Error("Can't get items from parser service.");
                return null;
            }
        }
    }
}