using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Nethermind.Core;
using Nethermind.Core.Crypto;
using Nethermind.Int256;
using Nethermind.Serialization.Rlp;
using Nethermind.Trie;

namespace Nethermind.State.RetrieveStorage
{

	public class Retriever : ITreeVisitor
	{

		private int _pathTraversalIndex;
        private Address _address = Address.Zero;

        private Nibble[] _fullAccountPath;

        private Dictionary<Keccak, StorageNodeInfo> _storageNodeInfos = new();
        private HashSet<Keccak> _nodeToVisitFilter = new();

        private string _filePath = "/Users/macbook/Desktop/gnosis/Result/storageForContract.txt";

		private class StorageNodeInfo
        {
            public StorageNodeInfo()
            {
                StorageIndices = new List<int>();
            }

            public int PathIndex { get; set; }
            public List<int> StorageIndices { get; }
        }

        private static Keccak ToKey(byte[] index)
        {
            return Keccak.Compute(index);
        }

        private static byte[] ToKey(UInt256 index)
        {
            byte[] bytes = new byte[32];
            index.ToBigEndian(bytes);
            return bytes;
        }


		public Retriever(byte[] hashedAddress)
		{
			_fullAccountPath = Nibbles.FromBytes(hashedAddress);

		}

		public Retriever(Address hashedAddress)
			:this(Keccak.Compute(hashedAddress?.Bytes ?? Address.Zero.Bytes).Bytes)
		{
		}

		// building result

		public bool ShouldVisit(Keccak nextNode)
        {
            if (_storageNodeInfos.ContainsKey(nextNode))
            {
                _pathTraversalIndex = _storageNodeInfos[nextNode].PathIndex;
            }

            return _nodeToVisitFilter.Contains(nextNode);
        }

        public void VisitTree(Keccak rootHash, TrieVisitContext trieVisitContext)
        {
        }

        public void VisitMissingNode(Keccak nodeHash, TrieVisitContext trieVisitContext)
        {
        }

        public void VisitBranch(TrieNode node, TrieVisitContext trieVisitContext)
        {
        	_nodeToVisitFilter.Remove(node.Keccak);

        	if (trieVisitContext.IsStorage)
        	{
        		//HashSet<int> bumpedIndexes = new();
        		foreach (int storageIndex in _storageNodeInfos[node.Keccak].StorageIndices)
                {
                    Keccak childHash = node.GetChildHash(storageIndex);
                    if (childHash is not null)
                    {
                        if (!_storageNodeInfos.ContainsKey(childHash))
                        {
                            _storageNodeInfos[childHash] = new StorageNodeInfo();
                        }

                        /*
                        if (!bumpedIndexes.Contains((byte) childIndex))
                        {
                            bumpedIndexes.Add((byte) childIndex);
                            _storageNodeInfos[childHash].PathIndex = _pathTraversalIndex + 1;
                        }
                        */
                        
                        _storageNodeInfos[childHash].StorageIndices.Add(storageIndex);
                        _nodeToVisitFilter.Add(childHash);
                    }
                }
        	}
        	/*
        	else
            {
                _nodeToVisitFilter.Add(node.GetChildHash((byte) _fullAccountPath[_pathTraversalIndex]));
            }
            */

            _pathTraversalIndex++;
        }

        public void VisitExtension(TrieNode node, TrieVisitContext trieVisitContext)
        {
        	_nodeToVisitFilter.Remove(node.Keccak);

        	Keccak childHash = node.GetChildHash(0);

        	if (trieVisitContext.IsStorage)
            {
            	_storageNodeInfos[childHash] = new StorageNodeInfo();
                _storageNodeInfos[childHash].PathIndex = _pathTraversalIndex + node.Path.Length;

                foreach (int storageIndex in _storageNodeInfos[node.Keccak].StorageIndices)
                {
                    _storageNodeInfos[childHash].StorageIndices.Add(storageIndex);
                    _nodeToVisitFilter.Add(childHash);
                }
            }

            if (IsPathMatched(node, _fullAccountPath))
            {
                _nodeToVisitFilter.Add(childHash);
                _pathTraversalIndex += node.Path.Length;
            }
        }

        public void VisitLeaf(TrieNode node, TrieVisitContext trieVisitContext, byte[] value)
        {
        	_nodeToVisitFilter.Remove(node.Keccak);

            if (trieVisitContext.IsStorage)
            {
            	foreach (int storageIndex in _storageNodeInfos[node.Keccak].StorageIndices)
                {
                    //Nibble[] thisStoragePath = _fullStoragePaths[storageIndex];
                    //bool isPathMatched = IsPathMatched(node, thisStoragePath);
                    
                    byte[] tmp = new RlpStream(node.Value).DecodeByteArray();
                	logging(storageIndex, tmp);                    
                }
            }
            /*
            else
            {
            }
            */
            _pathTraversalIndex = 0;
        }

        private bool IsPathMatched(TrieNode node, Nibble[] path)
        {
            bool isPathMatched = true;
            for (int i = _pathTraversalIndex; i < node.Path.Length + _pathTraversalIndex; i++)
            {
                if ((byte) path[i] != node.Path[i - _pathTraversalIndex])
                {
                    isPathMatched = false;
                    break;
                }
            }

            return isPathMatched;
        }

        private AccountDecoder _accountDecoder = new();

        public void VisitCode(Keccak codeHash, TrieVisitContext trieVisitContext)
        {
            throw new InvalidOperationException($"{nameof(Retriever)} does never expect to visit code");
        }

        public void logging(int storageIndex, byte[] value)
        {
        	// make logger
        	// find out, how eth_getProof is called in the code, how byte[] is converted to value hex

            File.WriteAllText(_filePath, storageIndex + " : " + value);

        }

        public string BuildResult() {

            return "Check file " + _filePath;

        }

	}

}