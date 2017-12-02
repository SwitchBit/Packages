using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwitchBit.Blockchain.Rpc
{
    public class BitcoinRpcClient : BlockchainRpcClient
    {
        public async Task<string> GetBlockHash(int index = 0)
        {
            var hash = await InvokeMethod("getblockhash", index);
            return hash.GetValue("result").ToString();
        }

        public async Task<string> GetBestBlockHash()
        {
            var hash = await InvokeMethod("getbestblockhash");
            return hash.ToString();
        }

        public async Task<BitcoinBlock> GetBlock(string blockHash)
        {
            var block = await InvokeMethod("getblock", blockHash);
            var retVal = block.GetValue("result").ToObject<BitcoinBlock>();
            retVal.Height = int.Parse(JObject.Parse(block.GetValue("result").ToString()).GetValue("height").ToString());
            return retVal;
        }

        public async Task<JObject> GetInfo()
        {
            var info = await InvokeMethod("getinfo");
            return info;
        }
        public async Task<JObject> GetTransaction(string transactionId)
        {
            var tx = await InvokeMethod("gettransaction", transactionId);
            return tx;
        }
        public async Task<BitcoinTransaction> GetRawTransaction(string transactionId, string verbose = "1")
        {
            var obj = await InvokeMethod("getrawtransaction", transactionId, 1); //Invalid type provided. Verbose parameter must be a boolean. - error code -3 if not, "1" works, true doesnt....
            var tx = obj.GetValue("result").ToObject<BitcoinTransaction>();
            return tx;
        }

        public async Task<int> GetBlockCount()
        {
            var retVal = await InvokeMethod("getblockcount");
            return retVal.GetValue("result").ToObject<int>();
        }

        public async Task<double> GetDifficulty()
        {
            var retVal = await InvokeMethod("getdifficulty");
            return retVal.GetValue("result").ToObject<double>();
        }

        public async Task<JObject> GetRawMempool()
        {
            var mempoolInfo = await InvokeMethod("getrawmempool");

            return mempoolInfo;
        }

        public async Task<BitcoinAccount> GetAccount()
        {
            var retVal = await InvokeMethod("getaccount");
            Console.WriteLine($@"Account {retVal}");
            return retVal.GetValue("result").ToObject<BitcoinAccount>();
        }
    }
}
