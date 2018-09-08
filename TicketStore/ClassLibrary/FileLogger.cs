using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.Hosting;

namespace TicketStore.Core
{
    public class FileLogger : ILogger
    {
        public static string logPath = HostingEnvironment.MapPath("~/log.txt");
         public static object LockObject = new object();
 
         public void Write(string category, string message)
         {
             
         }
     }
 }