using System.Collections.Generic;
using Preferences.Models;

namespace Preferences.Interfaces
{
    public interface IWebParseService
    {
        IEnumerable<SoughtItem> GetItems();
    }
}