# BlockReward contract in Geth

The given links:\
https://numerous-basement-567.notion.site/Geth-on-Gnosis-Chain-after-TheMerge-85995686617b4513aff03450187b4583 \
https://github.com/varasev/test-block-reward \
https://github.com/gnosischain/posdao-test-setup/tree/d2828623e551398330214d6a4a51705a6baa7503

I tried to run the given scripts, but every time a lot of errors appear. I think, it is because of the arm architecture of my macbook.\
I tried to launch several virtual machines, but it gave no profit, because the disk image for the vm should use the arm architecture too.\
Now I am trying to create a correct Dockerfile with the given BlockReward projects to build docker image and be able to launch it inside the container.

# Code research

OpenEthereum contract caller\
https://github.com/openethereum/openethereum/blob/d8305c52ea805e62d7532c3ac76386873984d326/crates/ethcore/src/engines/block_reward.rs#L116

It's realization\
https://github.com/openethereum/openethereum/blob/6e06824c23f05f3e231dd6ebde29c3253399a6f0/crates/ethcore/src/engines/mod.rs#L208

Geth analogue\
https://github.com/ethereum/go-ethereum/blob/a41ea8a97cd0f9db7a87e2dd15b380d4f1fbc311/ethclient/gethclient/gethclient.go#L140

But it's said, that CallContract executes a message call transaction, which is directly executed in the VM of the node, but never mined into the blockchain.

Geth Execute function\
https://github.com/ethereum/go-ethereum/blob/b1e72f7ea998ad662166bcf23705ca59cf81e925/core/vm/runtime/runtime.go#L102

It executes code and returns the EVM's return value, the new state and an error if it failed.

Geth EVM Call function\
https://github.com/ethereum/go-ethereum/blob/3b967d16caf306ccf8eb78b3a68bec36fa2a52ee/core/vm/evm.go#L168

It's said, that Call executes the contract associated with the addr with the given input as parameters. It also handles any necessary value transfer required and takes the necessary steps to create accounts and reverses the state in case of an execution error or failed value transfer.

OpenEthereum contract call\
https://github.com/openethereum/openethereum/blob/d8305c52ea805e62d7532c3ac76386873984d326/crates/ethcore/src/engines/block_reward.rs#L127

The way contract abi is stored in OpenEthereum\
https://github.com/openethereum/openethereum/blob/d8305c52ea805e62d7532c3ac76386873984d326/crates/ethcore/res/contracts/block_reward.json

Geth accumulateRewards function, which should be changed\
https://github.com/ethereum/go-ethereum/blob/e44d6551c3c872584722c366c863381f7e91df91/consensus/ethash/consensus.go#L652

The steps to make Geth work with BlockReward contract:
1. Provide BlockReward contract definition to Geth code
2. Add BlockReward.reward execution step in the end of every block production
3. Run tests to check the results


# Quick update
Adding some new stuff to GETH code [here](https://github.com/alien111/go-ethereum).

BlockReward contract class added, made it similar to checkpointoracle class. Have some problems with getting the output of the transaction(non-constant call), needed to get the result of reward function.

BlockReward class files: https://github.com/alien111/go-ethereum/tree/master/contracts/BlockReward/contract_

Currently the problem is to get the result of the BlockReward.reward transaction once it's mined into the blockchain. After solving it, all we have to do is just add contract calling to [the clique consensus file](https://github.com/alien111/go-ethereum/blob/master/consensus/clique/clique.go) and test the right reward distribution.

There is only one contract in GETH. It's [checkpointoracle](https://github.com/alien111/go-ethereum/tree/master/contracts/checkpointoracle). It has several view functions, they use just constant calls. And one non-constant function, but it returns only one bool variable, which is never retrieved in the code of GETH. Researching the code, I haven't found any functions, that could help to solve the problem so far. 

# Quick update 2 

Checked, what is done in [this commit](https://github.com/poanetwork/quorum/commit/0e922bd8412b2c2019624c82a2b129f5f580d8c2).
It didn't work, no input parameters have been passed to the contract, just empty arrays. After fixing the code to be logically similar to OpenEthereum one, I tried to test it on a testnet of 2 nodes with 1 sealer-node. The contract's storage hash remains the same when the blocks keep sealing. Looking deeper, we can see, that the additional evm is created in the code and only the balances can be updated in this case. Now I'm struggling to fix it by copying the additional evm's state to the main one.

# Quick update 3

The version of BlockReward contract with dummyCounter is stored [here](https://github.com/xdaichain/intern-beacon-chain/blob/main/smart_contracts/BlockReward.sol). dummyCounter is needed to check if the state is changing in the end of every block processing.

So, it really does. The state is changing, the rewards are distributed properly.

The latest commit to our version of GETH is stored [here](https://github.com/alien111/go-ethereum/commit/f8731114a2f9c1bfed4dc367b1221af71d31bf0c). Currently, extra receivers are added just before calling reward function to be able to test the code.

To test the BlockReward contarct it's needed to initialize a chain, deploy the contract and hardcode the contarct's address to the GETH code.

# Final update

addExtraReceiver call removed from code. Contract's address is now added through the config file.

The task was to make BlockReward work with Clique engine, so now contract should be passed tp config like
```sh
...
"clique": {
      "period": 5,
      "epoch": 30000,
      "blockReward": "0x649dd44aca6b4db506507fd4c32fc4173bae2ce1"
    }
...
```

The final commit: https://github.com/alien111/go-ethereum/commit/c00619780dabad8c85c8fa19c6bfbfae40cdc16c
