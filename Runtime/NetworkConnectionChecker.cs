// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using System;
using System.IO;
using System.Net;

namespace IKhom.UtilitiesLibrary.Runtime
{
    public static class NetworkConnectionChecker
    {
        public static void CheckConnection(Action<bool> onCheckCompleted)
        {
            const string HTTP_GOOGLE = "http://google.com";
            const string SCHEMA_ORG_WEBPAGE = "schema.org/WebPage";

            var result = GetHtmlFromUrl(HTTP_GOOGLE);

            var hasConnection = result != "" && result.Contains(SCHEMA_ORG_WEBPAGE);

            onCheckCompleted.Invoke(hasConnection);
        }

        private static string GetHtmlFromUrl(string resource)
        {
            var html = string.Empty;
            var req = (HttpWebRequest)WebRequest.Create(resource);

            try
            {
                using var resp = (HttpWebResponse)req.GetResponse();

                var isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;

                if (isSuccess)
                {
                    using var responseStream = resp.GetResponseStream();
                    if (responseStream != null)
                    {
                        using var reader = new StreamReader(responseStream);
                        var cs = new char[80];

                        int readChars;
                        while ((readChars = reader.Read(cs, 0, cs.Length)) > 0)
                        {
                            html += new string(cs, 0, readChars);
                        }
                    }
                }
            }
            catch
            {
                return string.Empty;
            }

            return html;
        }
    }
}