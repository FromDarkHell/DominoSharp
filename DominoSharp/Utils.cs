using System;
using System.Linq;
using System.Net.Http;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DominoSharp
{
    /// <summary>
    /// General utilites for gathering out stuff
    /// </summary>
    public class Utils
    {
        #region Functions
        /// <summary>
        /// Returns an HTTP GET Request on url as a JObject
        /// </summary>
        /// <param name="url">The URL to GET </param>
        /// <returns>an HTTP GET Request on url as a JObject</returns>
        public static JObject request_JSON(string url)
        {
            return JObject.FromObject(JsonConvert.DeserializeObject(requestData(url)));
        }

        /// <summary>
        /// Returns an HTTP GET Request on URL as a JObject but the URL is for XML
        /// </summary>
        /// <param name="URL">An XML page</param>
        /// <returns>an HTTP GET Request on XML URL as a JObject</returns>
        public static JObject request_XML(string URL)
        {
            // Get our XML data
            string XML = requestData(URL);
            // Remove some of the junk lines of XML.
            XML = string.Join(Environment.NewLine, XML.Split(Environment.NewLine.ToCharArray()).Skip(4).ToArray().Reverse().Skip(3).Reverse().ToArray());

            // Create a new XML doc to fill with our XML.
            XmlDocument doc = new XmlDocument();
            // Fill our XML
            doc.LoadXml(XML);
            // Convert the XML to JSON
            string JSON = JsonConvert.SerializeXmlNode(doc);
            // Parse it as a JObject
            return JObject.Parse(JSON);
        }

        /// <summary>
        /// A general helper function to return a string of all data returned from an HTTP GET Request
        /// </summary>
        /// <param name="URL">The URL to get</param>
        /// <returns>A string of all data returned from an HTTP GET Request</returns>
        private static string requestData(string URL)
        {
            string responseString = null;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(URL).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    responseString = responseContent.ReadAsStringAsync().Result;
                }
            }
            return responseString;
        }
        #endregion
    }
}
