using System.Collections.Generic;
using Preferences.Models;

namespace Preferences.Interfaces
{
    public interface ICreateExcelDocumentService
    {
        void Create(IEnumerable<SoughtItem> items);
    }
}