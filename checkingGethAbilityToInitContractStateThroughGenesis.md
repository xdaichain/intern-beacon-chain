## The contract

```
pragma solidity ^0.8.13;

contract toCheck {

}
```

## Creating abi and bin files with solc

## Adding contract info to genesis file
Everything remains as it was [here](https://github.com/xdaichain/intern-beacon-chain/blob/main/privateNetworks_contracts_snapSync.md), except

```
"alloc": {
    "0x0000000000000000000000000000000000000020": {
      "code": "0x60806_a_lot_of_numbers_here",
      "storage": {
        "0x0000000000000000000000000000000000000000000000000000000000012345": "0x010101"
      },
      "balance": "0x11000000000000000000000"
    },
    "a0307211ADba0D010e32f9178D01658fca06b406": {
      "balance": "0x10000000000000000000000"
    },
    ...
  },
```

## Deploying nodes according to [this](https://github.com/xdaichain/intern-beacon-chain/blob/main/privateNetworks_contracts_snapSync.md)

## Accessing contract storage with

```sh
var contractAddress = "0x0000000000000000000000000000000000000020"
eth.getStorageAt(contractAddress, 74565)
```

The result will be "0x0000000000000000000000000000000000000000000000000000000000010101"

Number 74565 is decimal for "0x0000000000000000000000000000000000000000000000000000000000012345" storage address.

## Used links

https://medium.com/coinmonks/a-practical-walkthrough-smart-contract-storage-d3383360ea1b
