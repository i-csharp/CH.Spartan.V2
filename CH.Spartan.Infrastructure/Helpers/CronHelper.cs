using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Spartan.Infrastructure
{
    public static class CronHelper
    {
        public static string Secondly(int second)
        {
            return $"*/{second} * * * *";
        }
    }
}
