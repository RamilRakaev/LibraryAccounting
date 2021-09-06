using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LibraryAccounting.CQRSInfrastructure.Methods
{
    public class MethodsAssembly
    {
        public static Assembly GetAssembly()
        {
            return Assembly.GetAssembly(typeof(MethodsAssembly));
        }
    }
}
