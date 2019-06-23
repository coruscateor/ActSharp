using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp
{

    public class TaskNotScheduledException : Exception
    {

        public TaskNotScheduledException()
            : base("Task must be scheduled to execute")
        {
        }

        public TaskNotScheduledException(string message)
            : base(message)
        {
        }

    }
}
