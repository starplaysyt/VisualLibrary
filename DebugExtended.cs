using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualFrameworkLibrary
{
    public class DebugExtended
    {
        public string productName;

        public DebugExtended(string productName)
        {
            this.productName = productName;
        }

        public void WriteLine(string info)
        {
            Debug.WriteLine("[" + DateTime.Now.TimeOfDay + "] " + productName + " - " + info);
        }
    }
}
