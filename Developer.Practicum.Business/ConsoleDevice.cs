using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grosvenor.Developer.Practicum.Interfaces;
using System.ComponentModel.Composition;

namespace Developer.Practicum.Business
{
    [Export(typeof(IDevice))]
    public class ConsoleDevice : IDevice
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string data)
        {
            Console.WriteLine(data);
        }
    }
}
