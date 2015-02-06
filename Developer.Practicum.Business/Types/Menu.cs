using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grosvenor.Developer.Practicum.Types
{
    public class Menu<T> where T : MenuItem
    {
        public List<T> Items { get; set; }
    }
}
