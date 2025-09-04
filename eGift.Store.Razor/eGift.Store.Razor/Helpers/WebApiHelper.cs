
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System;

namespace eGift.Store.Razor.Helpers
{
    public class WebApiHelper
    {
        #region Variables
        public IConfiguration _configuration;
        #endregion

        #region Constructors

        public WebApiHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        #endregion

        #region Web Client Methods

        public string WebApiClientGet(string apiUrl)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                WebClient webClient = new WebClient();
                webClient.Headers.Add("apiseckey", "FKVJL7MPe983Z0pnSW7DdIJGxnBvWucw");
                var url = _configuration.GetSection("Urls").GetSection("ApiUrl").Value + apiUrl;
                string json = webClient.DownloadString(url);
                return (json);
            }
            catch (WebException ex)
            {
                string pageContent = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().ToString();
                throw new Exception(ex.Message);
            }
        }

        public string WebApiClientPost(string apiUrl, string parameters)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                WebClient webClient = new WebClient();
                webClient.Headers.Add("apiseckey", "FKVJL7MPe983Z0pnSW7DdIJGxnBvWucw");
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                string json = webClient.UploadString(_configuration.GetSection("Urls").GetSection("ApiUrl").Value + apiUrl, parameters);
                return (json);
            }
            catch (WebException ex)
            {
                string pageContent = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().ToString();
                throw new Exception(ex.Message);
            }
        }

        public string WebApiClientDelete(string apiUrl)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                WebClient webClient = new WebClient();
                webClient.Headers.Add("apiseckey", "FKVJL7MPe983Z0pnSW7DdIJGxnBvWucw");
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";

                string json = webClient.UploadString(_configuration.GetSection("Urls").GetSection("ApiUrl").Value + apiUrl, "DELETE", "{}");
                return (json);
            }
            catch (WebException ex)
            {
                string pageContent = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd().ToString();
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
