using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Hurricane
{
    public static class Parser
    {

        private static string regexForOnlineCount = @"\b\d*";
        private static string regexForDetectionChance = @"[a-z]{8,10}";
        private static Tuple<int, string> requestOutput;

        public static Tuple<int,string> ParseRequest(string callBack)
        {

            if (callBack != null && callBack != String.Empty)
            {
                Match onlineCount = Regex.Match(callBack, regexForOnlineCount);
                Match detectionChance = Regex.Match(callBack, regexForDetectionChance);

                int onlinePlayers = Int32.Parse(onlineCount.ToString());

                requestOutput = new Tuple<int, string>(onlinePlayers, detectionChance.ToString().ToUpper());
            }
            else
                requestOutput = new Tuple<int, string>(0, "Connection error");           

            return requestOutput;
        }
    }
}
