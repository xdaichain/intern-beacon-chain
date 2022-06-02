pragma solidity ^0.8.13;

contract killable {

	address payable public owner;

	constructor() {
		owner = payable(msg.sender);
	}

	function kill() external {

		require(msg.sender == owner);
		selfdestruct(owner);

	}

}

contract collectingEthers is killable {

	receive() external payable {}

	function withdraw(uint _amountOfEthers) external {

		require(msg.sender == owner);
		owner.transfer(_amountOfEthers);

	}

	function ethersIsInTheTrove() external view returns(uint) {
		return address(this).balance;
	}

}