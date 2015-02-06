using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grosvenor.Developer.Practicum.Interfaces
{
    public interface IDevice
    {
        string Read();
        void Write(string data);
    }
}
