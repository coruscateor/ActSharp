using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp
{

    public class TaskNotScheduledException : Exception
    {

        public TaskNotScheduledException()
            : base("Task must be scheduled")
        {
        }

        public TaskNotScheduledException(string message)
            : base(message)
        {
        }

    }
}
