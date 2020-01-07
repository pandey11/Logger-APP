using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Interfaces
{
    public interface ILogger
    {
        public void LogInformation(string message);

        public void LogWarning(string message);

        public void LogError(string message);
    }
}
