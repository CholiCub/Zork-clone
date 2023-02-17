using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    public interface InputService
    {
        event EventHandler<string> InputReceived;
    }
}
