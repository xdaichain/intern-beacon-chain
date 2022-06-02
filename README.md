# In both cases, a private network of 3 nodes is built.

## GETH:
First of all, installing GETH is needed.

```sh
brew tap ethereum/ethereum 
brew install ethereum
```

Creating 3 folders (names gethLocalTestnet, gethLocalTestnet2, gethLocalTestnet3 were used) on the same level.
```sh
mkdir gethLocalTestnet gethLocalTestnet2 gethLocalTestnet3
```

Creating folder (name genesisBlock was used).
```sh
mkdir genesisBlock
```

Creating an account in each folder.
```sh
geth --datadir path/gethLocalTestnet account new
geth --datadir path/gethLocalTestnet2 account new
geth --datadir path/gethLocalTestnet3 account new
```

Creating genesisBlock/[genesisBlock.json](genesisBlock/genesisBlock.json) file. Consensus algorithm is "clique". In my case, a single initial signer address stored in extraData is the account from gethLocalTestnet2.

Initializing the GETH database in each node.
```sh
geth --datadir path/gethLocalTestnet init path/genesisBlock/genesisBlock.json
geth --datadir path/gethLocalTestnet2 init path/genesisBlock/genesisBlock.json
geth --datadir path/gethLocalTestnet3 init path/genesisBlock/genesisBlock.json
```

Network consists only of 3 nodes, so using bootnode isn't really needed. Let's just add peers manually.

Launching all nodes in different terminal windows

First node:
```sh
geth --datadir path/gethLocalTestnet --networkid 158342 --port 1111 --nodiscover console 2> /dev/null
```

Third node:
```sh
geth --datadir path/gethLocalTestnet3 --networkid 158342 --port 1112 --nodiscover console 2> /dev/null
```

Second node is a signer, so --unlock and --mine keys are needed:
```sh
geth --datadir path/gethLocalTestnet2 --port 1113 --networkid 158342 --unlock d7240B779899033EeffdeD2eB52283360FdAB7D9 --mine console 2> /dev/null
```

Each node has a console now. Let's get enode info of each node.
```sh
admin.nodeInfo.enode
```

Connecting nodes is needed, so let's execute
```sh
admin.addPeer("enode id")
```

with addresses of 2 other nodes. When I was testing this, executing this command with 2 nodes addresses in a non-signing node's console and with 1 node address in a signing node's console was enough to connect all nodes.

We can check if the nodes are connected with
```sh
eth.blockNumber
```
command



## Deploying contract with GETH
We have a super simple smart-contract path/smart_contracts/[firstContract.sol](smart_contracts/firstContract.sol)

Creating folder for compiled data.
```sh
mkdir path/smart_contracts/compiled
```

Compiling contract
```sh
solc -o compiled --bin --abi firstContract.sol
```

Now we have files collectingEthers.abi,	collectingEthers.bin, killable.abi, killable.bin

In any node's console 
```sh
var collectingEthersContract = "0x..."
```
Pasting contents of collectingEthers.bin instead of ...

```sh
var collectingEthersContractAbi = ...
```
Pasting contents of collectingEthers.abi instead of ...

```sh
var collectingEthersInterface = eth.contract(collectingEthersContractAbi)
personal.unlockAccount(eth.accounts[0])
var execution = collectingEthersInterface.new(
  {
    from: eth.accounts[0],
    data: collectingEthersContract,
    gas: 1000000
  }
)
var executionHash = execution.transactionHash
eth.getTransactionReceipt(executionHash)
var addressOfContract = eth.getTransactionReceipt(executionHash).contractAddress
eth.defaultAccount = eth.accounts[0]
```


Testing 

Sending ethers to the contract's "Trove"
```sh
eth.sendTransaction({
  from: eth.accounts[0],
  to: addressOfContract,
  value: 1000000000000000000000,
  gas: 120000,
  gasPrice: 80000000000
})
```

Withdrawing ethers
```sh
collectingEthersInterface.at(addressOfContract).withdraw(1000000)
```

Checking "Trove"'s balance
```sh
collectingEthersInterface.at(addressOfContract).ethersIsInTheTrove.call()
```

Impossibility of withdrawing ethers from the other account can be checked from another nodes.


## Nethermind:
Everything was done according to the Manual Setup section from this article: https://docs.nethermind.io/nethermind/ethereum-client/private-networks/how-to-setup-a-nethermind-only-clique-based-chain


## Snap Sync:
According to this article: https://docs.nethermind.io/nethermind/ethereum-client/sync-modes#snap-sync
"As of v1.13.0, Snap Sync on the Nethermind client can only download the Ethereum state but not serve it to other clients implementing Snap Sync. Since the only Ethereum client that supports serving Snap Sync requests is Geth, only networks supported by Geth can be synced: Mainnet, Goerli, Ropsten and Rinkeby. ..."
