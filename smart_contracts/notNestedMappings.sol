pragma solidity ^0.8.15;

contract notNestedMappings {

	mapping(uint => uint) um;

	constructor() {

		um[1] = 10;
		um[20] = 11;
		um[300] = 12;

	}

}