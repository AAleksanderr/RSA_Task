using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Preferences;
using Preferences.Interfaces;
using Preferences.Models;

namespace WebParseService
{
    public class WebParser : IWebParseService
    {
        private readonly Logger _logger;
        public WebParser()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }
        public IEnumerable<SoughtItem> GetItems()
        {
            try
            {
                _logger.Info("CreateDriver");
                var driver = CreateDriver();

                _logger.Info("MoveToDestinationPage");
                MoveToDestinationPage(driver);

                var paginationCount = 1;

                _logger.Info("Check PaginationExist");
                if (PaginationExist(driver)) paginationCount = GetPaginationPagesCount(driver);

                _logger.Info("GetDataFromPages");
                return GetDataFromPages(driver, paginationCount);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                throw new Exception("Parser Error");
            }
        }

        private static IEnumerable<SoughtItem> GetDataFromPages(ChromeDriver driver, int paginationCount)
        {
            var result = new List<SoughtItem>();
            for (var paginationPage = 0; paginationPage < paginationCount; paginationPage++)
            {
                MoveToCurrentPaginationPage(driver, paginationPage);
                var pageResult = GetItemsFromPage(driver).ToArray();
                if (pageResult.Length == 0) return result;
                result.AddRange(pageResult);
            }
            return result;
        }

        private static bool PaginationExist(ChromeDriver driver)
        {
            try
            {
                driver.FindElement(By.XPath(ParsePreferences.PaginationElementXPath));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private static int GetPaginationPagesCount(ChromeDriver driver)
        {
            return driver.FindElements(By.XPath(ParsePreferences.PaginationPagesXPath)).Count;
        }

        private static void MoveToCurrentPaginationPage(ChromeDriver driver, int paginationPage)
        {
            if (paginationPage > 0)
            {
                var paginationElement = driver.FindElement(By.XPath(ParsePreferences.PaginationElementXPath));
                MoveToElement(driver, paginationElement);
                paginationElement.Click();
            }

            if (paginationPage == 0) return;
            var element = driver.FindElements(By.XPath(ParsePreferences.PaginationPagesXPath))[paginationPage];
            MoveToElement(driver, element);
            element.Click();
        }

        private static void MoveToDestinationPage(IWebDriver driver)
        {
            foreach (var path in ParsePreferences.XPaths)
            {
                var element = driver.FindElement(By.XPath(path));
                MoveToElement(driver, element);
                element.Click();
            }
        }

        private static ChromeDriver CreateDriver()
        {
            var option = new ChromeOptions();
            option.AddArgument("--start-maximized");
            var driver = new ChromeDriver(Environment.CurrentDirectory, option)
                { Url = ParsePreferences.DriverUrl };
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(ParsePreferences.ParseRequestTimeout);
            return driver;
        }

        private static void MoveToElement(IWebDriver driver, IWebElement element)
        {
            var actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
            Thread.Sleep(1000);
        }

        private static IEnumerable<SoughtItem> GetItemsFromPage(ChromeDriver driver)
        {
            var result = new List<SoughtItem>();
            var counter = 0;
            while (counter < 10)
                try
                {
                    var items = driver.FindElements(By.XPath(ParsePreferences.ElementsXPath));
                    foreach (var item in items)
                    {
                        var soughtItem = new SoughtItem();
                        var name = item.FindElement(By.XPath(ParsePreferences.NameXPath));
                        soughtItem.Name = name.Text.Remove(0, 19);
                        IWebElement price;
                        try
                        {
                            price = item.FindElement(By.XPath(ParsePreferences.PriceXPath));
                        }
                        catch (NoSuchElementException)
                        {
                            return result;
                        }

                        soughtItem.Price = $"от {price.Text}";
                        var link = item.FindElement(By.XPath(ParsePreferences.LinkXPath));
                        soughtItem.Link = link.GetAttribute("href");
                        result.Add(soughtItem);
                    }
                    return result;
                }
                catch(Exception) 
                {
                    result.Clear();
                    Thread.Sleep(1000);
                    counter++;
                }
            return result;
        }
    }
}