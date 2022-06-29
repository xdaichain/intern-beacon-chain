pragma solidity ^0.8.13;

contract TestStorage {

    uint a = 5;
    
    uint[10] b = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];

    uint[] c;

    mapping(uint => uint) d;

    constructor() {
        
        c.push(11);
        c.push(12);

        d[1] = 20;
        d[2] = 30;

    }

}