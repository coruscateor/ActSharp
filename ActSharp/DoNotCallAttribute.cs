using System;
using System.Collections.Generic;
using System.Text;

namespace ActSharp
{

    [AttributeUsageAttribute(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Delegate | AttributeTargets.Property)]
    public class DoNotCallAttribute : Attribute
    {
    }

}
