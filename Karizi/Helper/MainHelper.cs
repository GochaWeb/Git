using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karizi.Helper
{
    public class MainHelper
    {
        public static string Random32()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}