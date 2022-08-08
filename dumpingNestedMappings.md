# Getting nested mappings with Geth dump function

[Previously](https://github.com/xdaichain/intern-beacon-chain/blob/main/migrationDbFromNethermindToGeth.md) Geth dump function was checked with the genesis block.
Now we want to get KV-pairs for nested mappings.

So, first of all we need to create a contract [nestedMappings.sol](https://github.com/xdaichain/intern-beacon-chain/blob/main/smart_contracts/nestedMappings.sol).\
Deploying it, stopping the node and trying to get dump. Unfortunately, we get the following result:
```sh
macbook@MacBook-Air-macbook nestedMappingsDump % ./geth --datadir gethLocalTestnet3 dump --incompletes
INFO [08-08|15:17:33.860] Maximum peer count                       ETH=50 LES=0 total=50
INFO [08-08|15:17:33.862] Set global gas cap                       cap=50,000,000
INFO [08-08|15:17:33.863] Allocated cache and file handles         database=/Users/macbook/Desktop/gnosis/nestedMappingsDump/gethLocalTestnet3/geth/chaindata cache=512.00MiB handles=5120 readonly=true
INFO [08-08|15:17:33.870] Opened ancient database                  database=/Users/macbook/Desktop/gnosis/nestedMappingsDump/gethLocalTestnet3/geth/chaindata/ancient readonly=true
INFO [08-08|15:17:33.873] State dump configured                    block=57 hash=0x36244aa97a2fad43c81bc6d2bc656ff20b33045f72d604f81262d6142185efe4 skipcode=false skipstorage=false start=0x0000000000000000000000000000000000000000000000000000000000000000 limit=0
INFO [08-08|15:17:33.873] Trie dumping started                     root=956d28..bd4a70
{"root":"0x956d281ec66904073ace7d989c99621fe4f279976bae333c54f0c3ecb3bd4a70"}
{"balance":"618970019642931514449562112","nonce":0,"root":"0x56e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b421","codeHash":"0xc5d2460186f7233c927e7db2dcc703c0e500b653ca82273b7bfad8045d85a470","address":"0x4e0e2ffd423b8248472f3be8e6de29fddf79e611","key":"0x4a6258c2a9bb0f6046f43be1840d2964f3a892ae78124c67a01ee31ab2f3f2fc"}
{"balance":"309485009821345068724781056","nonce":0,"root":"0x56e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b421","codeHash":"0xc5d2460186f7233c927e7db2dcc703c0e500b653ca82273b7bfad8045d85a470","address":"0x33e51be16e7ce76d1c5289f41e0b4fdd699aded0","key":"0x63781baf79b52838fd375d0c9699b4e9c44c5a21ee1208dfcb08fe9fee98be31"}
{"balance":"0","nonce":1,"root":"0xdf4aa4f7e40dc6954afaf1716253446e009c541a488dbb5846bebd43584321b0","codeHash":"0xa5810bc571ab82cf34511b398155b929879531fbddd6e7f767bf6d2bfe06fdd4","code":"0x6080604052348015600f57600080fd5b506004361060285760003560e01c8063195bd6fc14602d575b600080fd5b60436004803603810190603f919060a9565b6045565b005b8060016000858152602001908152602001600020600084815260200190815260200160002081905550505050565b600080fd5b6000819050919050565b6089816078565b8114609357600080fd5b50565b60008135905060a3816082565b92915050565b60008060006060848603121560bf5760be6073565b5b600060cb868287016096565b935050602060da868287016096565b925050604060e9868287016096565b915050925092509256fea264697066735822122090b5fdf41b4ea8a7d1fad204f1cf380d540c97f6d1e79dcfa7d4bb878f43938b64736f6c634300080f0033","storage":{"0x0000000000000000000000000000000000000000000000000000000000000000":"01"},"key":"0x6b5be089f674d0156fe0f8510d687afb3f5aebecb33196c7a06849624e242716"}
{"balance":"928455029463793829174343168","nonce":1,"root":"0x56e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b421","codeHash":"0xc5d2460186f7233c927e7db2dcc703c0e500b653ca82273b7bfad8045d85a470","address":"0x1368cf1678d28ea8aa0d06195425d7889725e65c","key":"0xd19dcd9f2981df682ab45a50803455302d5f09dad04f0c985d5ef48a8834d910"}
WARN [08-08|15:17:33.873] Dump incomplete due to missing preimages missing=1
INFO [08-08|15:17:33.873] Trie dumping complete                    accounts=4 elapsed="407.333µs"
macbook@MacBook-Air-macbook nestedMappingsDump % 
```
We can see, that dump is incomplete due to missing preimages. 

Ok, maybe it appears just in case of nested mappings? Let's try to use normal mappings.

[Contract.](https://github.com/xdaichain/intern-beacon-chain/blob/main/smart_contracts/notNestedMappings.sol)\
Deploying it, stopping the node and trying to get dump. Unfortunately, we get the following result:
```sh
macbook@MacBook-Air-macbook nestedMappingsDump % ./geth --datadir gethLocalTestnet2 dump --incompletes
INFO [08-08|15:43:13.576] Maximum peer count                       ETH=50 LES=0 total=50
INFO [08-08|15:43:13.578] Set global gas cap                       cap=50,000,000
INFO [08-08|15:43:13.579] Allocated cache and file handles         database=/Users/macbook/Desktop/gnosis/nestedMappingsDump/gethLocalTestnet2/geth/chaindata cache=512.00MiB handles=5120 readonly=true
INFO [08-08|15:43:13.588] Opened ancient database                  database=/Users/macbook/Desktop/gnosis/nestedMappingsDump/gethLocalTestnet2/geth/chaindata/ancient readonly=true
INFO [08-08|15:43:13.589] State dump configured                    block=167 hash=0x131cbe082b2264c21a5feff3c0e081a172b6fa58222301dc5a7cb5355a75da18 skipcode=false skipstorage=false start=0x0000000000000000000000000000000000000000000000000000000000000000 limit=0
INFO [08-08|15:43:13.589] Trie dumping started                     root=f03c5f..97272a
{"root":"0xf03c5f6e083a988b2b829fa6c9b56049b0e9b18598de379678d971dccb97272a"}
{"balance":"618970019642690137449562112","nonce":1,"root":"0x56e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b421","codeHash":"0xc5d2460186f7233c927e7db2dcc703c0e500b653ca82273b7bfad8045d85a470","address":"0x4e0e2ffd423b8248472f3be8e6de29fddf79e611","key":"0x4a6258c2a9bb0f6046f43be1840d2964f3a892ae78124c67a01ee31ab2f3f2fc"}
{"balance":"309485009821345068724781056","nonce":0,"root":"0x56e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b421","codeHash":"0xc5d2460186f7233c927e7db2dcc703c0e500b653ca82273b7bfad8045d85a470","address":"0x33e51be16e7ce76d1c5289f41e0b4fdd699aded0","key":"0x63781baf79b52838fd375d0c9699b4e9c44c5a21ee1208dfcb08fe9fee98be31"}
{"balance":"928455029464035206174343168","nonce":0,"root":"0x56e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b421","codeHash":"0xc5d2460186f7233c927e7db2dcc703c0e500b653ca82273b7bfad8045d85a470","address":"0x1368cf1678d28ea8aa0d06195425d7889725e65c","key":"0xd19dcd9f2981df682ab45a50803455302d5f09dad04f0c985d5ef48a8834d910"}
{"balance":"0","nonce":1,"root":"0x715925bf57dac5825cd2bf2a96ca900b4a9c54c3024f5f6e35699a6b16de6085","codeHash":"0x51d5cfc7cd06f4c15056fff33cf9461ec822cb4df8f141f2e693f8e26e2e7cb9","code":"0x6080604052600080fdfea26469706673582212205f111e3cd1ec02ac165e532b3c624fa00357282d10b626c7ba7a3d534824558464736f6c634300080f0033","storage":{"0x0000000000000000000000000000000000000000000000000000000000000000":"0c"},"key":"0xe064dce46581ea5d6c26bd791c7826100959c47ac30d9fab6b925aff3c423b3a"}
WARN [08-08|15:43:13.590] Dump incomplete due to missing preimages missing=1
INFO [08-08|15:43:13.590] Trie dumping complete                    accounts=4 elapsed="334.792µs"
macbook@MacBook-Air-macbook nestedMappingsDump %           
```
The same error.

Let's try not to use mappings at all, just several uints.

[Contract.](https://github.com/xdaichain/intern-beacon-chain/blob/main/smart_contracts/notMappings.sol)\
Deploying it, stopping the node and trying to get dump. Unfortunately, we get the following result:
```sh
macbook@MacBook-Air-macbook nestedMappingsDump % ./geth --datadir gethLocalTestnet2 dump --incompletes
INFO [08-08|15:54:07.466] Maximum peer count                       ETH=50 LES=0 total=50
INFO [08-08|15:54:07.468] Set global gas cap                       cap=50,000,000
INFO [08-08|15:54:07.470] Allocated cache and file handles         database=/Users/macbook/Desktop/gnosis/nestedMappingsDump/gethLocalTestnet2/geth/chaindata cache=512.00MiB handles=5120 readonly=true
INFO [08-08|15:54:07.477] Opened ancient database                  database=/Users/macbook/Desktop/gnosis/nestedMappingsDump/gethLocalTestnet2/geth/chaindata/ancient readonly=true
INFO [08-08|15:54:07.479] State dump configured                    block=44 hash=0x6a8ed8f694ad1383abfab2a02be5a9928f52f3d56708225f7efcfdee634eb600 skipcode=false skipstorage=false start=0x0000000000000000000000000000000000000000000000000000000000000000 limit=0
INFO [08-08|15:54:07.479] Trie dumping started                     root=8b1876..232c5d
{"root":"0x8b1876262835ce8186bfe7378b0396e6f5c181504cda74097ec64d7d56232c5d"}
{"balance":"618970019642690137449562112","nonce":1,"root":"0x56e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b421","codeHash":"0xc5d2460186f7233c927e7db2dcc703c0e500b653ca82273b7bfad8045d85a470","address":"0x4e0e2ffd423b8248472f3be8e6de29fddf79e611","key":"0x4a6258c2a9bb0f6046f43be1840d2964f3a892ae78124c67a01ee31ab2f3f2fc"}
{"balance":"309485009821345068724781056","nonce":0,"root":"0x56e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b421","codeHash":"0xc5d2460186f7233c927e7db2dcc703c0e500b653ca82273b7bfad8045d85a470","address":"0x33e51be16e7ce76d1c5289f41e0b4fdd699aded0","key":"0x63781baf79b52838fd375d0c9699b4e9c44c5a21ee1208dfcb08fe9fee98be31"}
{"balance":"928455029464035206174343168","nonce":0,"root":"0x56e81f171bcc55a6ff8345e692c0f86e5b48e01b996cadc001622fb5e363b421","codeHash":"0xc5d2460186f7233c927e7db2dcc703c0e500b653ca82273b7bfad8045d85a470","address":"0x1368cf1678d28ea8aa0d06195425d7889725e65c","key":"0xd19dcd9f2981df682ab45a50803455302d5f09dad04f0c985d5ef48a8834d910"}
{"balance":"0","nonce":1,"root":"0x8e8bb8b62ab4924366f9b0b2382818d954196df5df364737c151ba8115f233d8","codeHash":"0x1fca8744762b1af8bcfa29c847c9740ca20c71f7c00326bcd6d1626d6153f4b1","code":"0x6080604052600080fdfea2646970667358221220910c4fd1782fd4cf8bb6099c281c3c2c66107e38c3743ddfdce9c10483bbf5f864736f6c634300080f0033","storage":{"0x0000000000000000000000000000000000000000000000000000000000000000":"14"},"key":"0xe064dce46581ea5d6c26bd791c7826100959c47ac30d9fab6b925aff3c423b3a"}
WARN [08-08|15:54:07.479] Dump incomplete due to missing preimages missing=1
INFO [08-08|15:54:07.479] Trie dumping complete                    accounts=4 elapsed="308.084µs"
macbook@MacBook-Air-macbook nestedMappingsDump % 
```

## What is the preimage?
In Geth source code we can find [this](https://github.com/ethereum/go-ethereum/blob/e44d6551c3c872584722c366c863381f7e91df91/core/state/dump.go#L155).\
Going deeper in functions, we see the [GetKey function](https://github.com/ethereum/go-ethereum/blob/e44d6551c3c872584722c366c863381f7e91df91/trie/secure_trie.go#L194), which  returns the sha3 preimage of a hashed key that was previously used to store a value.\
And also we see [preimage function](https://github.com/ethereum/go-ethereum/blob/e44d6551c3c872584722c366c863381f7e91df91/trie/preimages.go#L61), which retrieves a cached trie node pre-image from memory. If it cannot be found cached, the method queries the persistent database for the content.

If I understand everything correctly, there is a way to retrieve a storage key in EVM terms from the global storage tree key.\
So, I was wrong with the conclusion, that Geth stores separate storage trees for every contarct.

The problem is to find out, why does a problem with missing preimages appear, when the contract is not defined with genesis block, but deployed.
