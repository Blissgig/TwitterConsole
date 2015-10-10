using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TwitterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TwitterSearch.TwitterMain();

            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                var frame = st.GetFrame(0);
                var line = st.GetFrame(0).GetFileLineNumber();

                MethodBase site = ex.TargetSite;
                string sMethodName = site == null ? null : site.Name;
                    
                Console.WriteLine("------------------");
                Console.WriteLine(DateTime.Now.ToString());
                Console.WriteLine("Line: " + line.ToString());
                Console.WriteLine("Method: " + sMethodName);
                Console.WriteLine("Exception: " + ex.Message);
                Console.Write(ex.StackTrace.ToString());
                Console.WriteLine("");
            }
            catch { }
        }
    }
}
