pragma solidity ^0.8.13;

contract StateChange {

	uint a = 10;
	uint b = 20;

	uint[] arr;

	function pushToArray(uint element) external {
		arr.push(element);
	}

}