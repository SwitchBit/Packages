using Newtonsoft.Json.Linq;

namespace SwitchBit.Blockchain.Rpc
{
    public interface IBlockchainRpcClient
    {
        JObject InvokeMethod(string methodName, params object[] parameters);
    }
}