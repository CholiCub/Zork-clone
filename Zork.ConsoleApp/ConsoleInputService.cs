using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
     class ConsoleInputService : InputService
    {
        public event EventHandler<string> InputReceived;

        public void ProcessInput()
        {
            string inputString = Console.ReadLine().Trim();
            InputReceived?.Invoke(this, inputString);
        }
    }
}
