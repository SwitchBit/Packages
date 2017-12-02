# BitcoinRpcClient Caveats

There are a couple nuances that need addressed before effectively using the RPC client. One of which is that the `getrawtransaction` method doesn't pull arbitrary transaction data by default anymore. 

## Importing blocks 
The block import process is parallelized now. In the database, the blocks table is prepopulated with all of the empty records for the chain. The importer pulls all of the then incomplete blocks and works on them individually in multiple processes. 

## Enabling `getrawtransaction`
In order for the `getrawtransaction` RPC method to return the indexed transaction info, the blocks' contents need reindexed to include all transaction data. **After v0.8.0.0**, only transactions pertinent to a local wallet are indexed.

* ### Reindex the blocks and transaction data on disk

Start the `bitcoind` or `bitcoin-qt` with the following arguments or add them to your `bitcoin.conf` for the node:

```powershell
 -rpcallowip=192.168.0.0/24 -rpcuser=test -rpcpassword=password -txindex=1 -reindex=1 -server
```

This could take quite a while to finish, but
**when this is complete** stop your `bitcoind` or `bitcoin-qt` client/process.

* ### Remove reindex argument and relaunch
This could take a while to finish. Once it completes, you can remove the following argument from the `command line` and/or `bitcoin.conf`:

~~`-reindex=1`~~

* ### Relaunch bitcoin process

Launch either `bitcoind` or `bitcoin-qt` with the **`-reindex=1`** argument removed or edit your `bitcoin.conf` again to remove the key/value.

Here's the final command line arguments relavent to `getrawtransaction`:

```powershell
-rpcallowip=192.168.0.0/24 -rpcuser=testuser -rpcpassword=password -txindex=1 -server
```

Now you should be able to make an RPC call to `getrawtransaction` and have the results return properly
