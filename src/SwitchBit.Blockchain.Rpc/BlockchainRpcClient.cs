using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwitchBit.Blockchain.Rpc
{
    public abstract class BlockchainRpcClient : IBlockchainRpcClient
    {
        //https://en.bitcoin.it/wiki/Running_Bitcoin

        //NOTES ON TRANSACTION INDEXING
        //To make getrawtransaction work, you have to reindex all the blocks with their transactions, -txindex=1 -reindex=1    
        //"D:\Bitcoin Node\bitcoin-qt.exe" -rpcthreads=16 -rpcallowip=192.168.0.0/24 -rpcuser=test -rpcpassword=1qaz@WSX -txindex=1 -reindex=1 -server -datadir=D:\BlockChains\BTC-MAIN
        //Then when it's complete, remove the -reindex=1 but leave the -txindex=1
        //"D:\Bitcoin Node\bitcoin-qt.exe" -rpcthreads=16 -rpcallowip=192.168.0.0/24 -rpcuser=test -rpcpassword=1qaz@WSX -txindex=1 -reindex=1 -server -datadir=D:\BlockChains\BTC-MAIN

        public string Url { get; set; } = "http://localhost.:8332";

        public async Task<JObject> InvokeMethod(string methodName, params object[] parameters)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(Url);
            webRequest.Credentials = new NetworkCredential("test", "1qaz@WSX"); //TODO: pass this in 
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";

            var postObject = new JObject
            {
                ["jsonrpc"] = "1.0",
                ["id"] = "1",
                ["method"] = methodName
            };
            //Debug.WriteLine($@"[Blockchain.Rpc.BlockchainRpcClient.InvokeMethod] Info: {methodName})");
            if (parameters?.Length > 0) //only add parameters if they exist
            {
                var paramArray = new JArray();
                foreach (var p in parameters)
                {
                    //Debug.WriteLine($@"[Blockchain.Rpc.BlockchainRpcClient.InvokeMethod] Info: {methodName}, param:{p})");

                    paramArray.Add(p);
                }
                postObject.Add(new JProperty("params", paramArray));
            }
            else
            {
                postObject.Add(new JProperty("params", new JArray()));
            }

            var json = JsonConvert.SerializeObject(postObject);
            var bytes = Encoding.UTF8.GetBytes(json);

            webRequest.ContentLength = bytes.Length;

            using (var requestStream = await webRequest.GetRequestStreamAsync())
            {
                await requestStream.WriteAsync(bytes, 0, bytes.Length);
                requestStream.Close();
            }
            
            WebResponse webResponse;
            try
            {
                //Debug.WriteLine($@"[Blockchain.Rpc.BlockchainRpcClient.InvokeMethod] Info: Making request for {methodName}");

                using (webResponse = await webRequest.GetResponseAsync())
                {
                    using (var responseStream = webResponse.GetResponseStream())
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            var returnJson = JsonConvert.DeserializeObject<JObject>(await reader.ReadToEndAsync());
                            //Debug.WriteLine($@"[Blockchain.Rpc.BlockchainRpcClient.InvokeMethod] Response: {methodName}, {postObject.GetValue("params")}), Response: {returnJson}");
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
