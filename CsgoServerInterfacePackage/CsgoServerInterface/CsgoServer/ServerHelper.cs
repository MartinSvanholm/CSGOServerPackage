using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsgoServerInterface.CsgoServer
{
    public static class ServerHelper
    {
        public static string GetCfg(string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"\Cfg\", fileName);

            string[] commands = File.ReadAllLines(path);
            return string.Join("; ", commands);
        }
    }
}
