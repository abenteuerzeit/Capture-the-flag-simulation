# Assignment Details

## Assignment: Capture The Flag

- Language: C#
- Time: 3 hours

## Task Description

Your task is to implement a simple Capture The Flag game simulation, which also utilizes the rules of Rock Paper Scissors. There are 3 teams: Rocks, Papers and Scissors. Each team has a certain number of players that are scattered across the map (which is a rectangular grid). There are also several flags on the map.
The simulation happens in cycles. During one cycle, each player makes a move depending on its team’s custom logic (ordered by their positions, starting from top left corner, going right, then down). Players can move, fight each other (but not the same team) and capture flags. When a player walks on a flag, he captures it - it gets removed from the map and the player is granted 10 points. Once all the flags have been captured, the game ends and the team with most points wins. The game can also end if all players are dead before capturing all flags.

### The general movement pattern of all Players is the following:

- If the nearest flag is directly above or below the Player, move up or down respectively
- Otherwise move left or right in the direction of the nearest flag
- Players can only move one field at a time
- If the chosen position is occupied by a teammate, don’t move

### However, every team has its own custom rules to this pattern:

1. **Team Rock** - represented by letter `R`.
   - If the chosen field is occupied by an enemy, fight him

2. **Team Paper** - represented by letter `P`.
   - If the chosen field is occupied by an enemy, then move in the opposite direction
   - If that position is also occupied by an enemy, fight him
   - If moving to the opposite direction would mean walking out of map’s boundaries, then don’t move
   
3. **Team Scissors** - represented by letter `S`.
   - If the chosen field is occupied by a Paper, fight him
   - If the chosen field is occupied by a Rock, then move in the opposite direction
   - If that position is also occupied by an enemy, fight him
   - If moving to the opposite direction would mean walking out of map’s boundaries, then don’t move
   
### Flags are represented by the letter F. Empty positions are represented by ..
   
   Fight happens whenever a player attempts to walk on a position occupied by another player from another team. The rules are the following:
   - Rock kills Scissors
   - Scissors kill Paper
   - Paper kills Rock
   - After one player kills another, he takes his position
   - Winning a fight grants 5 points

The simulation is deterministic (meaning: there is no randomness). The unit tests are going to check the game’s behavior with a set of predefined scenarios. If these tests don’t pass, it means there are some bugs or missing features in the game’s logic.

## Key features that are going to be checked by unit test classes:

- - TestActorFactory - actor creation, actor creation based on given character
- TestExtensions - extension methods oriented around actors, directions, vectors
- TestGameLogic - player’s behavior, movement, fight, game simulation conditions
- TestGameMap - map creation, map-to-string generating, getting actors by positions, getting positions of actors
- TestPlayer - simulating fights, getting move directions based on nearest flag position
- TestScoreboard - getting ranked players, best players, for given state of the game

## Hints

- For string creation tasks (like GameMap.ToString()) it is recommended to use StringBuilder class. Also, when appending a new line, use Environment.NewLine, using just \n or \r might cause the unit tests to fail
- For splitting a string into lines, it is recommended to use var lines = Regex.Split(originalString, "\r\n|\r|\n");
- All methods that are checked by unit tests and require implementation by the students, are marked with throw new NotImplementedException();
- The game’s logic uses the standard coordinate system - (x, y). However, due to the fact how text-game simulation works and how the Console prints lines, not columns, the ActorsMatrix 2D array inside GameMap class inverts the coordinates order - (y, x). Also, the Y axis is inverted - the lower the number, the upper the position. Watch out for that.
- Positions and vectors utilize named tuples which were introduced in C# 7. If you’re not familiar with these, there’s a good source of information: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples



# Notes

The Capture The Flag game simulation involves several classes that work together to simulate the game. The main classes that are used in the simulation are:

Actor: This is the base class for all actors in the game, such as players and flags. It has a reference to the game's map, which is a 2D matrix that keeps track of the positions of all actors on the map.

ActorFactory: This is a static class that is responsible for creating new actor instances. It has a predefined collection of names for the players, and methods for creating new player and flag instances, as well as creating actors from a given character.

Extensions: This class contains extension methods for the Direction enum, which is used to represent the directions that players can move in. These methods include converting between direction and vector, and getting the opposite direction.

Flag: This class represents a flag on the map. It has a captured property that indicates whether the flag has been captured by a player or not.

Game: This class represents the game itself. It contains the game's map, as well as a list of all players and flags on the map. It also has a method for simulating one round of the game, which includes moving players and checking for captures and fights.

GameMap: This class represents the map of the game. It has a 2D matrix of all actor references, as well as lists of all players and flags on the map. It also has methods for creating the map from a given string, and for getting actors by position and positions by actors.

Player: This class represents a player in the game. It has properties for the player's team, position, and captured flags. It also has a method for simulating a fight between two players.

Program: This is the main program class, which runs the game simulation.

Rock, Paper, and Scissors: These classes represent the different teams that players can be on. They inherit from the Player class, and have their own custom rules for movement and fighting.

Scoreboard: This class keeps track of the scores of all players and teams in the game. It has methods for getting the ranked players and best players for a given state of the game.

The simulator will work by first creating a GameMap from a given string, which represents the initial state of the game. Then, the Game class will simulate one round of the game by moving players and checking for captures and fights. This will be done in a loop until all flags have been captured or all players are dead. The Scoreboard class will then be used to determine the winner of the game.

To implement the task, I will first need to complete the methods in the ActorFactory class that are responsible for creating new actor instances. I will then need to complete the methods in the GameMap class that are responsible for creating the map and getting actors by position and positions by actors. Next, I will need to complete the methods in the Player class that are responsible for simulating fights. Finally, I will need to complete the methods in the Game class that are responsible for simulating one round of the game, including moving players and checking for captures and fights. Once all of these classes have been completed and tested, the simulator should be fully functional.

The Capture The Flag game simulation involves several classes that work together to simulate the game. The main classes that are used in the simulation are:

Actor: This is the base class for all actors in the game, such as players and flags. It has a reference to the game's map, which is a 2D matrix that keeps track of the positions of all actors on the map.

ActorFactory: This is a static class that is responsible for creating new actor instances. It has a predefined collection of names for the players, and methods for creating new player and flag instances, as well as creating actors from a given character.

Extensions: This class contains extension methods for the Direction enum, which is used to represent the directions that players can move in. These methods include converting between direction and vector, and getting the opposite direction.

Flag: This class represents a flag on the map. It has a captured property that indicates whether the flag has been captured by a player or not.

Game: This class represents the game itself. It contains the game's map, as well as a list of all players and flags on the map. It also has a method for simulating one round of the game, which includes moving players and checking for captures and fights.

GameMap: This class represents the map of the game. It has a 2D matrix of all actor references, as well as lists of all players and flags on the map. It also has methods for creating the map from a given string, and for getting actors by position and positions by actors.

Player: This class represents a player in the game. It has properties for the player's team, position, and captured flags. It also has a method for simulating a fight between two players.

Program: This is the main program class, which runs the game simulation.

Rock, Paper, and Scissors: These classes represent the different teams that players can be on. They inherit from the Player class, and have their own custom rules for movement and fighting.

Scoreboard: This class keeps track of the scores of all players and teams in the game. It has methods for getting the ranked players and best players for a given state of the game.

The simulator will work by first creating a GameMap from a given string, which represents the initial state of the game. Then, the Game class will simulate one round of the game by moving players and checking for captures and fights. This will be done in a loop until all flags have been captured or all players are dead. The Scoreboard class will then be used to determine the winner of the game.

To implement the task, I will first need to complete the methods in the ActorFactory class that are responsible for creating new actor instances. I will then need to complete the methods in the GameMap class that are responsible for creating the map and getting actors by position and positions by actors. Next, I will need to complete the methods in the Player class that are responsible for simulating fights. Finally, I will need to complete the methods in the Game class that are responsible for simulating one round of the game, including moving players and checking for captures and fights. Once all of these classes have been completed and tested, the simulator should be fully functional.