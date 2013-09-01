using System;
using System.Collections.Generic;

namespace OwinIoCContainerSample
{
    public interface IRepository : IDisposable
    {
        string GetRandomText();
        IEnumerable<string> GetTexts();
    }
}
