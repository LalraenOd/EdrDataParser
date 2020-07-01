using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace EdrDataParser
{
    public class EdrDataParser
    {
        /// <summary>
        /// Gets data from open API edr.data-gov-ua.org/api
        /// </summary>
        /// <param name="listEdrpou">Input list of EDRPOU codes to get info about</param>
        /// <returns>List of strings with basic info like: FullName, Occupation and Status.</returns>
        public static List<string> EdrpouDetailParser(List<string> listEdrpou)
        {
            List<string> resultList = new List<string>();

            List<Organisation> organisations = new List<Organisation>();

            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                for (int listItemCount = 0; listItemCount < listEdrpou.Count - 1; listItemCount++)
                {
                    string edrpou = listEdrpou[listItemCount];
                    string linkEdrpou = "http://edr.data-gov-ua.org/api/companies?where={\"edrpou\":{\"contains\":\"" + edrpou + "\"}}";
                    string pageEdrpou = client.DownloadString(linkEdrpou);

                    Regex regexOrgInfo = new Regex(
                        @"officialName"":(?<ofName>.+)"",""address""
                        | address"":(?<address>.+)"",""mainPerson""
                        | mainPerson"":(?<mainPerson>.+)"",""occupation""
                        | occupation"":(?<occupation>.+)"",""status""
                        | status"":(?<status>.+)"",""id""
                        ");
                    Match matchInfo = regexOrgInfo.Match(pageEdrpou);
                    string ofName = matchInfo.Groups["ofName"].Value.ToString();
                    string address = matchInfo.Groups["address"].Value.ToString();
                    string mainPerson = matchInfo.Groups["mainPerson"].Value.ToString();
                    string occupation = matchInfo.Groups["occupation"].Value.ToString();
                    string status = matchInfo.Groups["status"].Value.ToString();

                    //Regex regexOfName = new Regex("\"officialName\":(?<result>.+)\",\"address");
                    //Regex regexAddress = new Regex("\"address\":(?<result>.+)\",\"mainPerson");
                    //Regex regexMainPerson = new Regex("\"mainPerson\":(?<result>.+)\",\"occupation");
                    //Regex regexOccupation = new Regex("\"occupation\":\"(?<result>.+)\",\"status");
                    //Regex regexStatus = new Regex("\"status\":\"(?<result>.+)\",\"id");
                    //Match matchName = regexOfName.Match(pageEdrpou);
                    //Match matchAddress = regexAddress.Match(pageEdrpou);
                    //Match matchMainPerson = regexMainPerson.Match(pageEdrpou);
                    //Match matchOcc = regexOccupation.Match(pageEdrpou);
                    //Match matchStatus = regexStatus.Match(pageEdrpou);
                    //string ofName = matchName.Groups[1].Value.ToString();
                    //string address = matchAddress.Groups[1].Value.ToString();
                    //string mainPerson = matchMainPerson.Groups[1].Value.ToString();
                    //string occupation = matchOcc.Groups[1].Value.ToString();
                    //string status = matchStatus.Groups[1].Value.ToString();

                    organisations.Add(new Organisation(edrpou, ofName, address, mainPerson, occupation, status));

                    string result =
                        (listItemCount + 1) + "\t" +
                        edrpou + "\t" +
                        ofName + "\t" +
                        occupation + "\t" +
                        status;
                    resultList.Add(result);
                }
                return resultList;
            }
        }

        /// <summary>
        /// Gets data from open API edr.data-gov-ua.org/api
        /// </summary>
        /// <param name="edrpou">Input EDRPOU code to get info about</param>
        /// <returns>String with basic info like: FullName, Occupation and Status.</returns>
        public static string EdrpouDetailParser(string edrpou)
        {
            string result;
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string linkEdrpou = "http://edr.data-gov-ua.org/api/companies?where={\"edrpou\":{\"contains\":\"" + edrpou + "\"}}";
                string pageEdrpou = client.DownloadString(linkEdrpou);

                Regex regexOrgInfo = new Regex(
                @"officialName"":(?<ofName>.+)"",""address""
                | address"":(?<address>.+)"",""mainPerson""
                | mainPerson"":(?<mainPerson>.+)"",""occupation""
                | occupation"":(?<occupation>.+)"",""status""
                | status"":(?<status>.+)"",""id""
                ");
                Match matchInfo = regexOrgInfo.Match(pageEdrpou);
                string ofName = matchInfo.Groups["ofName"].Value.ToString();
                string address = matchInfo.Groups["address"].Value.ToString();
                string mainPerson = matchInfo.Groups["mainPerson"].Value.ToString();
                string occupation = matchInfo.Groups["occupation"].Value.ToString();
                string status = matchInfo.Groups["status"].Value.ToString();
                result =
                        edrpou + "\t" +
                        ofName + "\t" +
                        occupation + "\t" +
                        status;
            }
            return result;
        }
    }

}