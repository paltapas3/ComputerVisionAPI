using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;

namespace ComputerVisionAPI
{
    class ComputerVisionAPI
    {
        
        static string Subscriptionkey()
        {
            return System.Configuration.ConfigurationManager.AppSettings["Subscription-Key"];
            
        }
        static string RequestParameters()
        {
            return System.Configuration.ConfigurationManager.AppSettings["RequestParameters"];

        }
        static string ReadImagePath()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ImagePath"];

        }
        static string ReadURI()
        {
            return System.Configuration.ConfigurationManager.AppSettings["APIuri"];

        }
        static string Contenttypes()
        {
            return System.Configuration.ConfigurationManager.AppSettings["Contenttypes"];

        }

        static void Main(string[] args)
        {
            GetImgeDetails(ReadImagePath());
          
        }
       
        static byte[] GetImageAsByteArray(string ImagePath)
        {
            FileStream ImagefileStream = new FileStream(ImagePath, FileMode.Open, FileAccess.Read);
            BinaryReader ImagebinaryReader = new BinaryReader(ImagefileStream);
            return ImagebinaryReader.ReadBytes((int)ImagefileStream.Length);
        }
        /// <summary>
        /// Following function to fetch all image related details
        /// </summary>
        /// <param name="ImagePath"></param>
        static  void GetImgeDetails(string ImagePath)
        {
            var ComputerVisionAPIclient = new HttpClient();

            // Request headers - replace this example key with your valid subscription key. I have added that in App.config
            ComputerVisionAPIclient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Subscriptionkey());

            // Request parameters.
            string requestParameters = RequestParameters();
            string APIuri = ReadURI() + requestParameters;
          
            // Request body. 
            byte[] ImagebyteData = GetImageAsByteArray(ImagePath);

            ImgeAnalysis(ImagebyteData, APIuri, ComputerVisionAPIclient);

            
        }
        /// <summary>
        /// Following function calls the computer vision API & display the response in Console
        /// </summary>
        /// <param name="ImagebyteData"></param>
        /// <param name="uri"></param>
        /// <param name="ComputerVisionAPIclient"></param>
        static async void ImgeAnalysis(byte[] ImagebyteData, string APIuri, HttpClient ComputerVisionAPIclient)

        {
            HttpResponseMessage APIresponse;
            var Imagecontent = new ByteArrayContent(ImagebyteData);
            Imagecontent.Headers.ContentType = new MediaTypeHeaderValue(Contenttypes());
            APIresponse = await ComputerVisionAPIclient.PostAsync(APIuri, Imagecontent);
            Console.WriteLine(APIresponse);
            Console.Read();
            

        }

    }
}
