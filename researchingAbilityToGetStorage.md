# Getting different data types location in contract's storage 

Created [toGetData.sol](https://github.com/xdaichain/intern-beacon-chain/blob/main/smart_contracts/toGetData.sol) contarct with some data types including dynamic arrays and mappings.\
It contains uint, uint[10], uint[], mapping.

Deployed it to local testnet.

Accessing slots of storage and getting stored value:\
uint value:
```sh
> eth.getStorageAt(addressOfContract, 0)
"0x0000000000000000000000000000000000000000000000000000000000000005"
```
uint array element on 0 position
```sh
> eth.getStorageAt(addressOfContract, 1)
"0x0000000000000000000000000000000000000000000000000000000000000000"
```
uint array element on 1 position
```sh
> eth.getStorageAt(addressOfContract, 2)
"0x0000000000000000000000000000000000000000000000000000000000000001"
```
... uint array element on 9 position
```sh
> eth.getStorageAt(addressOfContract, 10)
"0x0000000000000000000000000000000000000000000000000000000000000009"
```
uint dynamic array location. According to solidity's documentation this storage cell contains the length of dynamic array 
```sh
> eth.getStorageAt(addressOfContract, 11)
"0x0000000000000000000000000000000000000000000000000000000000000002"
```
mapping. According to solidity's documentation this storage cell contains nothing.
```sh
> eth.getStorageAt(addressOfContract, 12)
"0x0000000000000000000000000000000000000000000000000000000000000000"
```

Have been trying to get dynamic arrays and mappings values according to [this article]( https://programtheblockchain.com/posts/2018/03/09/understanding-ethereum-smart-contract-storage/) for quite a long time.\
The solution was found [there](https://ethereum.stackexchange.com/questions/41241/how-are-mappings-found-in-storage-in-geth)

## Getting positions of mapping

Our mapping contains 2 elements.
```sh
d[1] = 20;
d[2] = 30;
```

To find position of d[1] value we need to concatenate position of mapping(12) and key(1), hash concatenated string and access storage with this hash.\
So, d[1]:
```sh
> let key = "0000000000000000000000000000000000000000000000000000000000000001"
undefined
> let pos = "000000000000000000000000000000000000000000000000000000000000000c"
undefined
> posHash = web3.sha3(key + pos, {"encoding":"hex"})
"0xd421a5181c571bba3f01190c922c3b2a896fc1d84e86c9f17ac10e67ebef8b5c"
> posHashBN = web3.toBigNumber(posHash)
9.5949769290960679919915568476335582553435826563121580797397853711946803546972e+76
> eth.getStorageAt(contractAddress, posHashBN)
"0x0000000000000000000000000000000000000000000000000000000000000014"
```
d[2]:
```sh
> key = "0000000000000000000000000000000000000000000000000000000000000002"
"0000000000000000000000000000000000000000000000000000000000000002"
> posHash = web3.sha3(key + pos, {"encoding":"hex"})
"0x5d6016397a73f5e079297ac5a36fef17b4d9c3831618e63ab105738020ddd720"
> posHashBN = web3.toBigNumber(posHash)
4.2234865624494891382798413269907358927548514282952878344152767142002251650848e+76
> eth.getStorageAt(contractAddress, posHashBN)
"0x000000000000000000000000000000000000000000000000000000000000001e"
```

## Getting positions of dynamic array

Our dynamic array contains 2 elements.
```sh
c[0] = 11;
c[1] = 12;
```

To find position of c[1] value we need to hash position of dynamic array(11) and add position of element (0) to it.\
So, c[0]:
```sh
> pos = "000000000000000000000000000000000000000000000000000000000000000b"
"000000000000000000000000000000000000000000000000000000000000000b"
> arrPosHash = web3.sha3(pos, {"encoding":"hex"})
"0x0175b7a638427703f0dbe7bb9bbf987a2551717b34e79f33b5b1008d1fa01db9"
> arrPosHashBN = web3.toBigNumber(arrPosHash)
6.60301456019777184113296434797620819555017468543624515662331739614079884729e+74
> eth.getStorageAt(contractAddress, arrPosHashBN)
"0x000000000000000000000000000000000000000000000000000000000000000b"
```
c[1]:
```sh
> eth.getStorageAt(contractAddress, arrPosHashBN.add(1))
"0x000000000000000000000000000000000000000000000000000000000000000c"
```
Now we have data to fill genesisBlock file with!

Creating [empty.sol](https://github.com/xdaichain/intern-beacon-chain/blob/main/smart_contracts/empty.sol), compile it and paste code into [genesisBlock.json](https://github.com/xdaichain/intern-beacon-chain/blob/main/genesisBlock/genesisBlock_2.json)

Finally, starting blockchain with [genesisBlock.json](https://github.com/xdaichain/intern-beacon-chain/blob/main/genesisBlock/genesisBlock_2.json) and requesting to get storage at positions.\
Testing...

```sh
> var contractAddress = "0x0000000000000000000000000000000000000123"
undefined
> eth.getStorageAt(contractAddress, "0x000000000000000000000000000000000000000000000000000000000000000a")
"0x0000000000000000000000000000000000000000000000000000000000000009"
> eth.getStorageAt(contractAddress, "0x5d6016397a73f5e079297ac5a36fef17b4d9c3831618e63ab105738020ddd720")
"0x000000000000000000000000000000000000000000000000000000000000001e"
> eth.getStorageAt(contractAddress, "0x0175b7a638427703f0dbe7bb9bbf987a2551717b34e79f33b5b1008d1fa01dba")
"0x000000000000000000000000000000000000000000000000000000000000000c"
```

### Success!

# Ability to get contract state at the time of certain block

The section with contract state changes list after transaction executing was found on etherscan. [Example](https://etherscan.io/tx/0xa317a9eb061df7702136ecfad5794ecb0d30e8dde781fef6d5c169b801557558#statechange)\
Couldn't find even a small article or documentation about the realization of this feature.

## What information about contract call we can get with web3js module

Creating [researchStateChange.sol contract](https://github.com/xdaichain/intern-beacon-chain/blob/main/smart_contracts/researchStateChange.sol). It contains function to change contract's state.\
Deploying it and calling this function. The state has been changed.

```sh
var pushingHash = contractInterface.at(addressOfContract).pushToArray(10)
eth.getTransaction(pushingHash)

{
  blockHash: "0x38ecdbe22b7c1ffa95c6bf95c6d3a591ec91e8f5e1e84761b5c26f9efa262266",
  blockNumber: 125,
  from: "0xa0307211adba0d010e32f9178d01658fca06b406",
  gas: 62511,
  gasPrice: 1000000000,
  hash: "0x8ea14cad09107e39e88c77a0562339ffcda3169288ade88d67dff4131d3afa29",
  input: "0x2b2654a3000000000000000000000000000000000000000000000000000000000000000a",
  nonce: 1,
  r: "0x7e54cdb7660861a8a346d32d9f6eb963cfbe472ea12beac20809a5878f8d3824",
  s: "0x65555dbf003eb75747f1f8e8aa5c8cf212e71d22c0ba7167bdb52829316da296",
  to: "0x2cf8dd18ee8372ab5483ff3cad272b0e7eaac5ba",
  transactionIndex: 0,
  type: "0x0",
  v: "0x4d532",
  value: 0
}
```
There is an input field, that contains "2b2654a3" - the first 4 bytes of the Keccak hash of the ASCII form of the signature of function name and "000000000000000000000000000000000000000000000000000000000000000a" - the parameter of function call.

## What can be found in Nethermind code

[File](https://github.com/NethermindEth/nethermind/blob/1d2f21ba04d4fd14cada95479d57b8d368b70af7/src/Nethermind/Nethermind.Abi.Contracts/Contract.cs)\
Provides contract operations. Generates transactions and calls.

[File](https://github.com/NethermindEth/nethermind/blob/82f331a3e7ff21712a5f839e0a62ba7c16110e44/src/Nethermind/Nethermind.Evm/TransactionProcessing/TransactionProcessor.cs)\
Provides transactions processing. Contains Execute(...) function, which contains state commiting lines.

[File](https://github.com/NethermindEth/nethermind/blob/d1208bc336bad37c8ab3ff425428cedcd2e0894b/src/Nethermind/Nethermind.State/StorageProvider.cs)\
Provides storage operations.

[File](https://github.com/NethermindEth/nethermind/blob/aec476d0689416dc89e66f59e317b4d1bb2a3b7d/src/Nethermind/Nethermind.State/StateProvider.cs)\
Provides state operations. Contains several getter functions. GetStorageRoot(Address address) makes possible to get storage root for contract address.

# To sum up
To my mind, there is no way to get contract's state at the time of the certain block. Neither web3js console commands, nor Nethermind internal functions provide it. The only contract's storage data we can access is current state and inputs for every transaction to contract.
