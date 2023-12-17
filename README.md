This is a Checkers project I did to showcase my knowledge of object oriented design principals and to practice problem solving.
The final product is a checkers game with 3 playing modes:
- 2 player
- Player vs Computer
- Computer vs Computer
When a computer participates in the play, you can select the computer's strength.

Design Patters Used in the project:
Singleton, static factory, Adapter, Decorator, Template Method, Strategy Method, Observer, Command.
The Computer's position evaluation function is a simple material count function and can be easily changed dynamically (thanks to the strategy pattern).

The search algorithm I utilized was MiniMax, which was optimized using alpha-beta pruning. 
Further optimizations can be made on the algorithm by using iterative deepening and using a transposition table.
