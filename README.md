# Puzzle-Dungeon
A multiplayer Unity game developed in collaboration with 2 coursemates for a group project module.

Every year the inhabitants of the underground (Humans, Wizards, and Monsters) host a puzzle dungeon constest in which they determine the smartest creatures in the world. Only teams consisting of creatures of each race can enter the contest. Does your team have what it takes to win?

The game uses class mechanics to encourage players to collaborate, supporting one team of 4 players at a time.

- Knight: moves crates out of the way
- Wizard: melts ice that is blocking the path
- Archer: shoots arrows at targets to open doors and enable other puzzle elements
- Lizard: digs in dig spots to find items that can be traded with an NPC
The players must complete all the puzzles in a level to proceed to the next level. There are 3 levels: green, orange, and purple. Each level has a different room shape to reflect the domain it is based in. The Human dungeon is an orange castle, the Wizard dungeon is a purple maze, and the Monster dungeon is a green cave. This game was a proof of concept and therefore only one level has been made.

The game is hosted on a player's machine and can be accessed by other players using the host's IP address.

## My contribution to the project:
- Menu implementation
- Hosting, joining, and leaving the game
- Randomly allocating player classes upon joining the game
- Random level generation (2 different algorithms)
- Minimap
- Basic player movement
- Initial puzzle element functionality

## Credits
- Rhys Fourie: tileset and character sprites recolour, puzzle room designs
- Sam Thornton: multiplayer puzzle synchronising, final puzzle functionality, improved collisions
- 0x72 on itch.io: original tileset

## Techonologies used
- Unity 2D v2020.3.27f1
- Mirror library v57.0.0
- ParrelSync (for testing the multiplayer)
