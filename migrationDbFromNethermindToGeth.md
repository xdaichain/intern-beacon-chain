# Getting same dumps for Nethermind and Geth

While researching Geth code I met the "geth dump" function, that returns the full dump of state tree for the node. For example, we have state tree, consisting of
```json
    "0x0000000000000000000000000000000000000020": {
      "code": "0x6080604052348015600f57600080fd5b50603f80601d6000396000f3fe6080604052600080fdfea264697066735822122006a506ce93a2f3125a819bd7537c98a14ee621b45917882fec99989f578e0ff264736f6c634300080d0033",
      "storage": {
        "0x0000000000000000000000000000000000000000000000000000000000012345": "0x010101"
      },
      "balance": "0x11000000000000000000000"
    },
    "bd95720cd30a00f3b374644b7255d6a160dbc692": {
      "balance": "0x10000000000000000000000"
    },
    "955329ca1e210bb0acd8ac83e30f077e6b80e0c8": {
      "balance": "0x20000000000000000000000"
    },
    "60d0739841de1f4a35d43203c7d40efd966a209e": {
      "balance": "0x30000000000000000000000"
    }
```

In this case Nethermind's dump of the state tree returns for the 0x20 contarct the following data
```sh
++01 ACCOUNT  0e0b0f0d070d0b0b060800040305010800040404030d0f00010a0408020e060405010901020b0808040c0d02060b03010500040b0b07070b0b0e0c09080602 -> b6ae8cb67f663828792121b0368a04060411479d47865e6dcd42a9ce36022130
++01   NONCE: 0
++01   BALANCE: 328827822935179135520079872
++01   IS_CONTRACT: True
++++CODE 0x56f6ab9c1c1b10605962ab975f1b590de4e39fed6731b437008fce8199907dfa
++++STORAGE LEAF  030801080e01020c01090305090a0c070b0b080e07090b03030c0201040401030902040c070f0f0c060903080a01070206020806080c0a04040401060c0e0607 -> 675e181d545cd872a816dbe4c014c417a0dacdcbab3992667a726467a47b37a1
++++STORAGE   KEY : 203818e12c19359ac7bb8e79b33c214413924c7ffc6938a17262868ca44416ce67  VALUE: 0x10101
```

For the same contract Geth's dump function returns
```json
{"balance":"328827822935179135520079872",
"nonce":0,
"root":"0x675e181d545cd872a816dbe4c014c417a0dacdcbab3992667a726467a47b37a1",
"codeHash":"0x56f6ab9c1c1b10605962ab975f1b590de4e39fed6731b437008fce8199907dfa",
"code":"0x6080604052348015600f57600080fd5b50603f80601d6000396000f3fe6080604052600080fdfea264697066735822122006a506ce93a2f3125a819bd7537c98a14ee621b45917882fec99989f578e0ff264736f6c634300080d0033",
"storage":{"0x0000000000000000000000000000000000000000000000000000000000012345":"010101"},
"address":"0x0000000000000000000000000000000000000020",
"key":"0x1ebfd7dbb6804351804443df01a482e6451912b884cd26b31504bb77bbec9862"}
```

We see the matching 0x675e181d545cd872a816dbe4c014c417a0dacdcbab3992667a726467a47b37a1 value, which suggests that the tree is built in the same way in both clients, but Geth provides key-value pairs for storage elements in the dump.
Upon a detailed study of the mechanism for allocating storage elements during a dump, an understanding (possibly wrong) appeared that for each contract, Geth stores a separate storage tree so that you can navigate it by the original keys, and not by their hashes.

The corresponding Geth code:\
[Snapshot](https://github.com/ethereum/go-ethereum/blob/b196ad1c165ecd6c9edaca520e7161a58e50eb06/cmd/geth/snapshot.go#L525)\
[Fast iterator](https://github.com/ethereum/go-ethereum/blob/master/core/state/snapshot/iterator_fast.go)

If the above information is correct, the idea to internally migrate the Nethermind's db to Geth's db can't be completed due to the inability to retrieve keys from Nethermind's db. 
