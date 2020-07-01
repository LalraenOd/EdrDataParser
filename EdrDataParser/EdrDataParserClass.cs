using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace EdrDataParser
{
    public class EdrDataParserClass
    {
        /// <summary>
        /// Gets data from open API edr.data-gov-ua.org/api
        /// </summary>
        /// <param name="listEdrpou">Input list of EDRPOU codes to get info about</param>
        /// <returns>List of strings with basic info like: FullName, Occupation and Status.</returns>
        public static List<string> EdrpouDetailParser(List<string> listEdrpou)
        {
            List<string> resultList = new List<string>();
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                for (int listItemCount = 0; listItemCount < listEdrpou.Count - 1; listItemCount++)
                {
                    string edrpou = listEdrpou[listItemCount];
                    string linkEdrpou = "http://edr.data-gov-ua.org/api/companies?where={\"edrpou\":{\"contains\":\"" + edrpou + "\"}}";
                    string pageEdrpou = client.DownloadString(linkEdrpou);
                    Regex regexOfName = new Regex("\"officialName\":(?<result>.+)\",\"address");
                    Regex regexOccupation = new Regex("\"occupation\":\"(?<result>.+)\",\"status");
                    Regex regexStatus = new Regex("\"status\":\"(?<result>.+)\",\"id");
                    Match matchName = regexOfName.Match(pageEdrpou);
                    Match matchOcc = regexOccupation.Match(pageEdrpou);
                    Match matchStatus = regexStatus.Match(pageEdrpou);
                    string result = 
                        (listItemCount + 1) + "\t" +
                        edrpou + "\t" +
                        matchName.Groups[1].Value.ToString() + "\t" +
                        matchOcc.Groups[1].Value.ToString() + "\t" +
                        matchStatus.Groups[1].Value.ToString();
                    resultList.Add(result);
                }
                return resultList;
            }
        }
    }
}