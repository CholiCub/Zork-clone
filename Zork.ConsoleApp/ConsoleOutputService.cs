using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    class ConsoleOutputService : OutputService
    {
        public void Write(object value)
        {
            Console.Write(value);
        }

        public void WriteLine(object value)
        {
            Console.WriteLine(value);
        }
    }
}
