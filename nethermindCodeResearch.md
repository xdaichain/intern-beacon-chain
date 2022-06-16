## Saving block number count and current block in db :
https://github.com/NethermindEth/nethermind/blob/1d2f21ba04d4fd14cada95479d57b8d368b70af7/src/Nethermind/Nethermind.Baseline/Tree/BaselineTree.cs#L63

## SaveCurrentBlockInDb function:
https://github.com/NethermindEth/nethermind/blob/1d2f21ba04d4fd14cada95479d57b8d368b70af7/src/Nethermind/Nethermind.Baseline/Tree/BaselineTreeMetadata.cs#L181

Starting sequence, encoding lastBlockDbHash, encoding lastBlockWithLeaves, saving to kv storage

## Encode(long value) function:
https://github.com/NethermindEth/nethermind/blob/1d2f21ba04d4fd14cada95479d57b8d368b70af7/src/Nethermind/Nethermind.Serialization.Rlp/RlpStream.cs#L299

Circular shifting value and writing bytes

## MetadataBuildDbKey function:
https://github.com/NethermindEth/nethermind/blob/1d2f21ba04d4fd14cada95479d57b8d368b70af7/src/Nethermind/Nethermind.Baseline/Tree/BaselineTreeMetadata.cs#L109

Encoding DbPrefix, encoding blockNumber, encoding 2 previous values. Result is stored in bytes

## WriteByte function:
https://github.com/NethermindEth/nethermind/blob/7db573cb160eb649c34cf5a2dc13bb75f4e2f45a/src/Nethermind/Nethermind.Crypto/KeccakRlpStream.cs#L45

Updating keccak hash with needed byte.


## Stuff associated with MPT
https://github.com/NethermindEth/nethermind/blob/1d2f21ba04d4fd14cada95479d57b8d368b70af7/src/Nethermind/Nethermind.Merkleization/MerkleTree.cs

## Stuff associated with CanonicalHashTrie
https://github.com/NethermindEth/nethermind/blob/3d6a3a07035b6a86b7537f7ba07c1d315c29c148/src/Nethermind/Nethermind.Synchronization/LesSync/CanonicalHashTrie.cs

## Article about MPT
https://medium.com/@chiqing/merkle-patricia-trie-explained-ae3ac6a7e123

## Needed:
Find out what decoding means according to https://github.com/NethermindEth/nethermind/blob/a2c0d7f2a42224f6d052ed2097e9da443113fc59/src/Nethermind/Nethermind.Serialization.Rlp/RlpStream.cs#L685





## Example of block

Some([253, 140, 58, 24, 18, 121, 168, 212, 6, 240, 51, 151, 237, 150, 130, 131, 192, 151, 224, 244, 186, 43, 203, 66, 63, 56, 88, 167, 138, 93, 52, 147])

Some([249, 2, 91, 249, 2, 86, 160, 247, 255, 41, 144, 62, 19, 184, 5, 181, 68, 157, 107, 151, 52, 36, 132, 21, 183, 154, 4, 222, 209, 131, 83, 137, 92, 75, 26, 83, 164, 29, 214, 160, 29, 204, 77, 232, 222, 199, 93, 122, 171, 133, 181, 103, 182, 204, 212, 26, 211, 18, 69, 27, 148, 138, 116, 19, 240, 161, 66, 253, 64, 212, 147, 71, 148, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 160, 15, 65, 11, 106, 110, 174, 61, 49, 86, 236, 205, 150, 106, 200, 66, 164, 197, 69, 196, 121, 33, 185, 254, 54, 56, 109, 225, 129, 82, 207, 221, 207, 160, 86, 232, 31, 23, 27, 204, 85, 166, 255, 131, 69, 230, 146, 192, 248, 110, 91, 72, 224, 27, 153, 108, 173, 192, 1, 98, 47, 181, 227, 99, 180, 33, 160, 86, 232, 31, 23, 27, 204, 85, 166, 255, 131, 69, 230, 146, 192, 248, 110, 91, 72, 224, 27, 153, 108, 173, 192, 1, 98, 47, 181, 227, 99, 180, 33, 185, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 36, 131, 160, 0, 0, 128, 132, 98, 150, 47, 198, 184, 97, 78, 101, 116, 104, 101, 114, 109, 105, 110, 100, 32, 49, 46, 49, 48, 46, 49, 55, 45, 48, 45, 51, 50, 97, 57, 98, 102, 54, 97, 100, 45, 50, 5, 198, 118, 140, 134, 37, 180, 169, 112, 83, 147, 77, 233, 92, 251, 142, 214, 167, 110, 193, 184, 120, 98, 64, 65, 44, 45, 126, 138, 49, 150, 226, 112, 112, 255, 44, 184, 126, 1, 8, 136, 212, 152, 16, 50, 49, 67, 104, 220, 197, 105, 45, 0, 143, 188, 250, 63, 119, 101, 188, 43, 42, 62, 203, 1, 160, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 136, 0, 0, 0, 0, 0, 0, 0, 0, 192, 192])

## HEX formatting

"FD8C3A181279A8D406F03397ED968283C097E0F4BA2BCB423F3858A78A5D3493"

"F9025BF90256A0F7FF29903E13B805B5449D6B9734248415B79A04DED18353895C4B1A53A41DD6A01DCC4DE8DEC75D7AAB85B567B6CCD41AD312451B948A7413F0A142FD40D49347940000000000000000000000000000000000000000A00F410B6A6EAE3D3156ECCD966AC842A4C545C47921B9FE36386DE18152CFDDCFA056E81F171BCC55A6FF8345E692C0F86E5B48E01B996CADC001622FB5E363B421A056E81F171BCC55A6FF8345E692C0F86E5B48E01B996CADC001622FB5E363B421B9010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000022483A00000808462962FC6B8614E65746865726D696E6420312E31302E31372D302D3332613962663661642D3205C6768C8625B4A97053934DE95CFB8ED6A76EC1B8786240412C2D7E8A3196E27070FF2CB87E010888D4981032314368DCC5692D008FBCFA3F7765BC2B2A3ECB01A00000000000000000000000000000000000000000000000000000000000000000880000000000000000C0C0"



__Byte_160__ suits for keccak rlp starting according to https://github.com/NethermindEth/nethermind/blob/1d2f21ba04d4fd14cada95479d57b8d368b70af7/src/Nethermind/Nethermind.Serialization.Rlp/RlpStream.cs#L177
It can be met in the example above: ..., 160, 0, 0, ...

__Byte_136__ suits for 6 byte rlp starting according to https://github.com/NethermindEth/nethermind/blob/1d2f21ba04d4fd14cada95479d57b8d368b70af7/src/Nethermind/Nethermind.Serialization.Rlp/RlpStream.cs#L379
It can be met in the example above: ..., 136, 0, 0, ...

__Byte_249__ maybe suits for StartSequence according to https://github.com/NethermindEth/nethermind/blob/a2c0d7f2a42224f6d052ed2097e9da443113fc59/src/Nethermind/Nethermind.Serialization.Rlp/RlpStream.cs#L106 
