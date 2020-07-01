using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EdrDataParser
{
    public class EdrDataParserClass
    {
        public static List<string> EdrpouDetailParser(List<string> listEdrpou)
        {
            List<string> resultList = new List<string>();
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                for (int listItemCount = 0; listItemCount < listEdrpou.Count - 1; listItemCount++)
                {
                    var edrpou = listEdrpou[listItemCount];
                    var linkEdrpou = "http://edr.data-gov-ua.org/api/companies?where={\"edrpou\":{\"contains\":\"" + edrpou + "\"}}";
                    string pageEdrpou = client.DownloadString(linkEdrpou);
                    Regex regexOfName = new Regex("\"officialName\":(?<result>.+)\",\"address");
                    Regex regexOccupation = new Regex("\"occupation\":\"(?<result>.+)\",\"status");
                    Regex regexStatus = new Regex("\"status\":\"(?<result>.+)\",\"id");
                    Match matchName = regexOfName.Match(pageEdrpou);
                    Match matchOcc = regexOccupation.Match(pageEdrpou);
                    Match matchStatus = regexStatus.Match(pageEdrpou);
                    string resultName = matchName.Groups[1].Value;
                    string resultOcc = matchOcc.Groups[1].Value;
                    string resultStatus = matchStatus.Groups[1].Value;
                    string result = (listItemCount + 1) + "\t" + edrpou + "\t" + resultName + "\t" + resultOcc + "\t" + resultStatus;
                    resultList.Add(result);
                }
                return resultList;
            }
        }
    }
}