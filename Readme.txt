The sample command provided and the out put derived is wrong in my point of view
1.The rover is at index [1,1] and pointing north
2.FFRFLFLF is the sample command
3.so F,F moves the rover northwards basically to [0,1] then to index out of range 
4. i have treated the outer bounds as the actual boundary so the move cannot be made at [0,1]
5.the first two moves even if the outer bounds is not considered as the boundary will not create an output if the move is northwards
6.This is my understanding but i may be wrong :)