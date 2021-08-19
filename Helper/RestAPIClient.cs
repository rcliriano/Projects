using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Collections;

namespace Projects.Helper
{
    public enum AcceptType
    {
        JSON
    }

    

    internal class RestAPIClient
    {
        //private readonly IHttpClientFactory _clientFactory;



        ///<summary>
        ///Calls the specified API and returns the result
        ///</summary>
        ///<typeparam name="T">Generic class identifier</typeparam>
        ///<param name="apiAddress">URI of the API to be called</param>
        ///<param name="parameters">API parameters</param>
        ///<returns>API result cast as the model that the calling function specified</returns>

        public List<T> GetRestAPIClient<T>(string apiAddress, string parameters)
        {
            List<T> result;

            apiAddress += parameters;
            int timeOutSeconds = 600000;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiAddress);



            request.UserAgent = "StreamClient";
            request.AllowReadStreamBuffering = false;
            request.AllowWriteStreamBuffering = false;
            request.Timeout = timeOutSeconds;
            string jsonArray = null;


            WebResponse response = request.GetResponse();
            using (Stream resultStreams = response.GetResponseStream())

            using (StreamReader readers = new StreamReader(resultStreams))

            using (Stream resultStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(resultStream)) {

                jsonArray = reader.ReadToEnd();

            }

                

             result = JsonConvert.DeserializeObject<List<T>>(jsonArray);


            return result;
        }

        ///<summary>
        ///Calls the specified API and returns the result
        ///</summary>
        ///<typeparam name="T">Generic class identifier</typeparam>
        ///<param name="apiAddress">URI of the API to be called</param>
        ///<param name="parameters">API parameters</param>
        ///<returns>API result cast as the model that the calling function specified</returns>

        public T GetSingleRestAPIClient<T>(string apiAddress, string parameters)
        {
            T result;

            apiAddress += parameters;
            int timeOutSeconds = 600000;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiAddress);



            request.UserAgent = "StreamClient";
            request.AllowReadStreamBuffering = false;
            request.AllowWriteStreamBuffering = false;
            request.Timeout = timeOutSeconds;
            string jsonArray = null;


            WebResponse response = request.GetResponse();
            using (Stream resultStreams = response.GetResponseStream())

            using (StreamReader readers = new StreamReader(resultStreams))

            using (Stream resultStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(resultStream))
            {

                jsonArray = reader.ReadToEnd();

            }



            result = JsonConvert.DeserializeObject<T>(jsonArray);


            return result;
        }


    }

    
}
