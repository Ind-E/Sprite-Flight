<div align="center">
<h1>Type Flight</h1>
<a href=https://play.unity.com/en/games/8b4d1f69-8fdb-4da4-ab1d-52ebe0483145/type-flight)>Play Here</a>
</div>
<br/>

<img width="1312" height="695" alt="image" src="https://github.com/user-attachments/assets/e0692dcc-f86c-4eae-b079-9b232c5e811a" />


## How to Play

Click with the mouse to move your ship

Asteroids will spawn in over time, try to survive as long as possible

Destroy asteroids by typing in a valid english word that contains the sequence
of 3 letters below them and pressing enter. No duplicate words are allowed.

## Extensions

### 1. Improved movement

The ship will now rotate over time to look at your mouse, and the physics are
tuned so that the ship is easier to control. I did this so that more of the
focus of the game is on typing, and it's not as much about maneuvering.

### 2. Screen Wrapping

If the ship or an asteroid would go off screen, they instead wrap around to the
other side of the screen. I implemented this by creating a clone of the object
mirrored either horizontally or vertically (or both) when it's close to an
edge, and then swapping its position with the clone when its centerpoint
crosses the boundary. I had to add extra logic to not re-randomize the speed
and size and speed of the asteroid when the clones are created.

### 3. Spawning Asteroids Over Time

I implemented spawning asteroids over time with a Spawner class that constantly
ticks down a timer. In addition, whenever an asteroid is destroyed, the timer
decreases by 25%.

### 4. Typing

I sourced a list of english words, and did some pre-processing in python to
find all 3-letter combos from that dictionary, then filtered to only 3-letter
combos that had at least 500 valid english words containing them. I then
created a Dictionary singleton to load this data at the start of the game,
implemented text rendering below each asteroid, and input handling for the
keyboard and displayed it below the ship. The Dictionary is a singleton so that
each asteroid can access the its list of 3-letter combos when spawning to
choose a random one, and to use its deictionary to check if the user's typed
word is a valid english word, without duplicating the list of english words in
memory. When running in the editor, it caused the game to stall for a bit when
the game started and it's loading all the words into memory but that problem
isn't present in the web build.

## Reflection

I am not a fan of the ai generated assignment description
