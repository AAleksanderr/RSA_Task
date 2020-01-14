using System.Collections.Generic;
using Preferences.Interfaces;
using Preferences.Models;

namespace MockParser
{
    public class MockParserService : IWebParseService
    {
        public IEnumerable<SoughtItem> GetItems()
        {
            return new List<SoughtItem>
            {
                new SoughtItem
                {
                    Name = "Samsung MC28H5135CK",
                    Price = "от 547,00 р.",
                    Link = "https://catalog.onliner.by/microvawe/samsung/mc28h5135ck"
                },
                new SoughtItem
                {
                    Name = "Panasonic NN-CS894BZPE",
                    Price = "от 337,00 р.",
                    Link = "https://catalog.onliner.by/microvawe/panasonic/nncs894bzpe"
                },
                new SoughtItem
                {
                    Name = "Daewoo KOR-5A67W",
                    Price = "от 517,00 р.",
                    Link = "https://catalog.onliner.by/microvawe/samsung/me88suw"
                },
                new SoughtItem
                {
                    Name = "BBK 20MWS-726S/W",
                    Price = "от 567,00 р.",
                    Link = "https://catalog.onliner.by/microvawe/samsung/me88sut"
                },
                new SoughtItem
                {
                    Name = "Samsung MS23H3115FK",
                    Price = "от 435,00 р.",
                    Link = "https://catalog.onliner.by/microvawe/samsung/ms23f302tak"
                }
            };
        }
    }
}