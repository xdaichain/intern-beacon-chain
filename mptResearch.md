## How merkle tree is presented in Nethermind
https://github.com/NethermindEth/nethermind/blob/1d2f21ba04d4fd14cada95479d57b8d368b70af7/src/Nethermind/Nethermind.Merkleization/MerkleTree.cs

Trie builder
https://github.com/NethermindEth/nethermind/blob/aec476d0689416dc89e66f59e317b4d1bb2a3b7d/src/Nethermind/Nethermind.Core.Test/Builders/TrieBuilder.cs

## Code providing data transforming to merkle tree(should be checked later)
https://github.com/NethermindEth/nethermind/blob/1d2f21ba04d4fd14cada95479d57b8d368b70af7/src/Nethermind/Nethermind.Merkleization/Merkleizer.cs
https://github.com/NethermindEth/nethermind/blob/1d2f21ba04d4fd14cada95479d57b8d368b70af7/src/Nethermind/Nethermind.Merkleization/ShaMerkleTree.cs

## A lot of code providing Patricia tree operations
https://github.com/NethermindEth/nethermind/blob/a2c0d7f2a42224f6d052ed2097e9da443113fc59/src/Nethermind/Nethermind.Trie/PatriciaTree.cs

Only one constructor has code. Has code for nodes commiting, working with root hash, nodes connecting(several cases with different node types are explained)

## Code for different data types storing
[StateTree](https://github.com/NethermindEth/nethermind/blob/a2c0d7f2a42224f6d052ed2097e9da443113fc59/src/Nethermind/Nethermind.State/StateTree.cs)\
[ReceiptTrie](https://github.com/NethermindEth/nethermind/blob/aec476d0689416dc89e66f59e317b4d1bb2a3b7d/src/Nethermind/Nethermind.State/Proofs/ReceiptTrie.cs)\
[TxTrie](https://github.com/NethermindEth/nethermind/blob/aec476d0689416dc89e66f59e317b4d1bb2a3b7d/src/Nethermind/Nethermind.State/Proofs/TxTrie.cs)\
[StorageTree](https://github.com/NethermindEth/nethermind/blob/a2c0d7f2a42224f6d052ed2097e9da443113fc59/src/Nethermind/Nethermind.State/StorageTree.cs)\
[CanonicalHashTrie](https://github.com/NethermindEth/nethermind/blob/3d6a3a07035b6a86b7537f7ba07c1d315c29c148/src/Nethermind/Nethermind.Synchronization/LesSync/CanonicalHashTrie.cs)\
[PersistentReceiptStorage](https://github.com/NethermindEth/nethermind/blob/master/src/Nethermind/Nethermind.Blockchain/Receipts/PersistentReceiptStorage.cs)

## Possible cases of trie construction are presented in the file [trieCases.txt](https://github.com/NethermindEth/nethermind/blob/a2c0d7f2a42224f6d052ed2097e9da443113fc59/src/Nethermind/Nethermind.Trie/trieCases.txt), stored in Nethermind.Trie github folder.


There are 2 categories:

First:
"Cases for state trie where branches never have value field populated and leafs are always longer than 32bytes
Smallest possible leaf size is 69 (RLP of a sequence of 33 (code hash) + 33 (storage root) + 1 (nonce of 0) + 1 (balance of 0))
So the leaves are never included by value so the smallest stable branch size is:
81 (RLP of a sequence of 14 nulls and 2 * 33 (node ref))"

Second:
"Cases for receipt trie or tx trie where branches can have value fields (due to various lengths of keys).
Still no node can be included by value because leaves are always longer than 32 bytes (they include tx signature > 64 bytes or receipt RLP that includes bloom / 256 bytes)"

Trie cases are also illustrated for both categories.
