using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp
{

    public class TaskScheduledException : Exception
    {

        public TaskScheduledException()
            : base("Task must not be scheduled to execute")
        {
        }

        public TaskScheduledException(string message)
            : base(message)
        {
        }

    }

}
