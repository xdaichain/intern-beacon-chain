# Implementing feature

All the code, that is involved in this task, is stored [in this directory](https://github.com/xdaichain/intern-beacon-chain/tree/main/myCode/nethermind). 
The hierarchy of folders is the same, as in the project.

The main file is [retrieveStorage.cs](https://github.com/xdaichain/intern-beacon-chain/blob/main/myCode/nethermind/src/Nethermind/Nethermind.State/RetrieveStorage/retrieveStorage.cs).\
It provides the tree visiting with ITreeVisitor interface functions. It was made according to AccountProofCollector example.

Some other files needed to be updated due to the new feature appearing.\
First of all, the [IEthRpcModule.cs](https://github.com/xdaichain/intern-beacon-chain/blob/main/myCode/nethermind/src/Nethermind/Nethermind.JsonRpc/Modules/Eth/IEthRpcModule.cs) file.
It is the interface for RPC functions.

Then [IEthRpcModule.cs](https://github.com/xdaichain/intern-beacon-chain/blob/main/myCode/nethermind/src/Nethermind/Nethermind.JsonRpc/Modules/Eth/IEthRpcModule.cs) and [IEthRpcModuleProxy.cs](https://github.com/xdaichain/intern-beacon-chain/blob/main/myCode/nethermind/src/Nethermind/Nethermind.JsonRpc/Modules/Eth/EthRpcModuleProxy.cs), which implement functions of the interface.

The next file is [EthCliModule.cs](https://github.com/xdaichain/intern-beacon-chain/blob/main/myCode/nethermind/src/Nethermind/Nethermind.Cli/Modules/EthCliModule.cs). It provides console interface calls for the RPC functions.

There were a lot of warnings during compilation of the project and the solution to get rid of them was found in adding information about our new function to the files in [docs folder](https://github.com/xdaichain/intern-beacon-chain/tree/main/myCode/nethermind/docs/source).

Currently I'm struggling with creating docker image to run Nethermind test setup of 3 nodes with the above code included. 
