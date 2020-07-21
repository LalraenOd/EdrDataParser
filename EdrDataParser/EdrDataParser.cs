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
        /// <param name="edrpouList">Input list of EDRPOU codes to get info about</param>
        /// <returns>List of strings with basic info like: FullName, Occupation and Status.</returns>
        public static List<Organisation> EdrpouDetailParser(List<string> edrpouList)
        {
            List<Organisation> resultOrganisations = new List<Organisation>();

            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                for (int listItemCount = 0; listItemCount < edrpouList.Count - 1; listItemCount++)
                {
                    string edrpou = edrpouList[listItemCount];
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
                    resultOrganisations.Add(new Organisation(edrpou, ofName, address, mainPerson, occupation, status));
                }
                return resultOrganisations;
            }
        }
        
        /// <summary>
        /// Gets data from open API edr.data-gov-ua.org/api
        /// </summary>
        /// <param name="edrpou">Input EDRPOU code to get info about</param>
        /// <returns>String with basic info like: FullName, Occupation and Status.</returns>
        public static Organisation EdrpouDetailParser(string edrpou)
        {
            Organisation resultOrganisation;
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
                resultOrganisation = new Organisation(edrpou, ofName, address, mainPerson, occupation, status);
            }
            return resultOrganisation;
        }
        
        /// <summary>
        /// Gets data about FOP from youcontrol. Be carefull. NOT SO FAST. DO SLEEP
        /// </summary>
        /// <param name="code">INN code</param>
        /// <returns>Organisation</returns>
        public static Organisation FopDataParser(string code)
        {
            Organisation resultOrganisation;
            string link = "https://youcontrol.com.ua/search/?country=1&q=" + code;
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string page = client.DownloadString(link);
                Regex regex = new Regex(" <meta name=\"description\" content=\"➤➤ (?<result>.+) Повне досьє на");
                Match match = regex.Match(page);
                var ofName = match.Groups["result"].Value.ToString();
                resultOrganisation = new Organisation(code, ofName, "", "", "", "");
            }
            return resultOrganisation;
        }
    }
}