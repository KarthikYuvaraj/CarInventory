using System;

namespace CarInventory.Core.Logging
{
    public interface ILogger
    {
        void Log(string message);
        void Log(Exception ex);
    }
}
