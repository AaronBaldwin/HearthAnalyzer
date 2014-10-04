HearthAnalyzer
==============

HearthAnalyzer is a project seeking to accomplish the following goals:

1. Provide an as accurate as possible implementation of the game mechanics and rules of Hearthstone.
2. Read and apply the game state from Hearthstone into HearthAnalyzer's internal representation
3. Given a game state, calculate the "best" next move based on some heuristic
4. Run complete simulations
5. Replays of games constructed from a log file

How it will work
-----------------
Hearthstone currently logs events from the game into a log file stored on the local user's machine. We can query and parse
this file to determine the current game state and play it back and keep HearthAnalyzer's game state up to date.
This will allow HearthAnalyzer to look at the current game board and calculate the next best move or provide the player
a number of options. 

Eventually, we can run Monte Carlo simulations to determine the viability of an input deck.

Design
--------------
HearthAnalyzer will be modular, extensible and largely consist of the following components:

1. **HearthAnalyzer.Core** - This will contain the core game mechanics, rules, and cards. This will contain all of the classes 
necessary to simulate and play a game of Hearthstone.
2. **HearthAnalyzer.LiveRunner** - This will attach to a game of HearthStone and listen for events emitted by the built-in 
Hearthstone logger and provide an accessible instance of the game state.
3. **HearthAnalyzer.Analyzer** - This will calculate the next best move(s) given the game state.
4. **HearthAnalyzer.Simulator** - This project will have the capability of running Monte Carlo simulations given a deck to play 
and optional decks to play against.

Disclaimer
---------------
This project just started and is a long way from completion. Don't expect anything out of it and you won't be disappointed :)

Want to help?
---------------
Feel free to message me or send a pull request. The primary focus right now is to implement each card.