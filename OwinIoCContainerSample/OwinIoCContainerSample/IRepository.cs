using System;

namespace OwinIoCContainerSample
{
    public interface IRepository : IDisposable
    {
        string GetRandomText();
    }
}
