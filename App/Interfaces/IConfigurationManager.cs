using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Interfaces
{

    public interface IConfigurationManager
    {
        /// <summary>
        /// Gets an instance of the specified configuration.
        /// </summary>
        T Get<T>() where T : class, new();
    }
}
