pragma solidity ^0.8.15;

contract nestedMappings {

	mapping(uint => uint) um;

	mapping(uint => mapping(uint => uint)) m;

	constructor() {

		um[1] = 10;
		um[20] = 11;
		um[300] = 12;

		m[1][2] = 130;
		m[10][15] = 777;
		m[0][0] = 1;

	}

	function addToNestedMapping(uint n1, uint n2, uint n3) external {

		m[n1][n2] = n3;

	}

}