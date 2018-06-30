using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone
{
    public class Program
    {
        static void Main(string[] args)
        {
			// Forces DateTime to create objects in yyyy/MM/dd format (our SQL db uses this format)
			CultureInfo ci = new CultureInfo(CultureInfo.CurrentCulture.Name);
			ci.DateTimeFormat.ShortDatePattern = "yyyy'/'MM'/'dd";

			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;

			CLI cli = new CLI();

            cli.RunCLI();
        }
    }
}
