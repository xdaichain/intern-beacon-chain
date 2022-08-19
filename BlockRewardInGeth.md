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

