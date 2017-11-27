using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace SwitchBit.Blockchain.Rpc
{
    public abstract class BlockchainRpcClient : IBlockchainRpcClient
    {
        public string Url { get; set; } = "http://localhost:8333/";

        public JObject InvokeMethod(string methodName, params object[] parameters)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(Url);
            webRequest.Credentials = new NetworkCredential("username", "password"); //TODO: pass this in 
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";

            var postObject = new JObject
            {
                ["jsonrpc"] = "1.0",
                ["id"] = "1",
                ["method"] = methodName
            };
            Debug.WriteLine($@"[Blockchain.Rpc.BlockchainRpcClient.InvokeMethod] Info: {methodName})");
            if (parameters?.Length > 0) //only add parameters if they exist
            {
                var paramArray = new JArray();
                foreach (var p in parameters)
                {
                    Debug.WriteLine($@"[Blockchain.Rpc.BlockchainRpcClient.InvokeMethod] Info: {methodName}, param:{p})");

                    paramArray.Add(p);
                }
                postObject.Add(new JProperty("params", paramArray));
            }

            var json = JsonConvert.SerializeObject(postObject);
            var bytes = Encoding.UTF8.GetBytes(json);

            webRequest.ContentLength = bytes.Length;

            using (var requestStream = webRequest.GetRequestStream())
                requestStream.Write(bytes, 0, bytes.Length);
            
            WebResponse webResponse;
            try
            {
                Debug.WriteLine($@"[Blockchain.Rpc.BlockchainRpcClient.InvokeMethod] Info: Making request for {methodName}");

                using (webResponse = webRequest.GetResponse())
                {
                    using (var responseStream = webResponse.GetResponseStream())
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            var returnJson = JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd());
                            Debug.WriteLine($@"[Blockchain.Rpc.BlockchainRpcClient.InvokeMethod] Response: {methodName}, {postObject.GetValue("params")}), Response: {returnJson}");
                            return returnJson;
                        }
                    }
                }
            }
            catch (WebException webex)
            {
                using (var responseStream = webex.Response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        var returnJson = JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd());
                        Debug.WriteLine($@"[Blockchain.Rpc.BlockchainRpcClient.InvokeMethod] ERROR: {methodName}, {postObject.GetValue("params")}), Response: {returnJson}");
                        return returnJson;
                    }
                }

            }
            catch (Exception exception)
            {
                Debug.WriteLine($@"[Blockchain.Rpc.BlockchainRpcClient.InvokeMethod] ERROR: {exception.ToString()}");
                throw;
            }
        }
    }
}
