# Game-Engines-CA1        Alien Invasion Game

Game Visualizer Assignment for Game Engines  -------------------------     
Student Name - Munfung Look -  
Student Number - C18510403  
Class: DT508
Module: Game Engines 1

# Description

A playable game visualizer, with an alien invasion setting laying siege over upon a futuristic city in the night sky, lasers will be fired in every direction creating a massive laser lightshow and fantastical explosions in the night sky, with that in mind the game can be left on without being played, and have the background play by itself, with an ongoing war between the city and the extraterrestrial invaders

When engaged with, the player will spawn in as a mecha/fighter to fight off the initial alien invasion spawned in as background for visualization purposes, and then the enemies will spawn in waves from then on, until the last wave the boss will spawn in as a wondrous lightshow of a ship which will lay down a brilliant blinding siege upon the city and the individual parts will target the player as well, once defeated, you gain new interations to the game's visualizer system and return to the menu, where everything returns back to visualiser mode again.

Overall the game will be largely built mainly with a city generator code, generating destructible buildings, the player has to defend it from the invaders, the boss will use some similar code from the city generator, that can generate a new unique boss shape every time a new game has started, as for the player controller will be the usual WASD SHIFT SPACE controls to move across the 3 axis, which is subject to change and fine tuned later in the development process, as for the systematic which is a big part of the game's 'visualizer' if pulled off correctly, is where the enemies will randomly fire at the city below and also have allies and enemies interact with each other by firing at each other, and finally the dynamics and the visuals themselves are particle effects assisted by post processing effects such as bloom, chromatic aberration etc.

# Instructions For Use
 Movement Control
- WASD keys - to move
- Space & Ctrl - to ascend/jump & descend
- Space - if on Ground Jump, In Mid Air to start flying

 Weapon Control
- Hold Left Click - fire laser
- Hold Right Click - fire missile
- Hold Q & look at something  - fire guided missiles

# How it works

City Generation ---
First of all, it starts with mainland generator which creates the height layers and district surfaces which in turn generates the cities, it will use raycast to hit a position to spawn the baseTowers that generates the building, both mainland generator and district/city generator uses the same script.
``` C#
test
```

Enemy Spawning ---
how the spawning will work is that the enemy ships will first spawn in as a single ship core module, it is essentially derived from the city tower generation script modified to build a ship instead, which in turn generates the other available modules in its referenced scriptable object generation table, which essentially allows endless configurations of new ships to spawn in, its generation settings can be edited in its script or prefab, adjusting its maximum length, give a different generation table etc...
``` C#

```

Boss Generation ---
While the boss has no script on its own it relies mainly on turrets and other existing scripts that lets it do things, I used a simple coroutine script that will slowly generate overtime generate all the boss' weapons available to it in the prefab tables.
``` C#

```

Turrets ---
The turrets use 'Turret' and 'FiringSys' Scripts, in which together allows me to create various turret types and weapons, firstly the turret checks for viable tags in targets, when an object is in its overlapsphere, it will check for its tag and what this turret is looking for e.g 'if (target.CompareTag("Enemy") && findEnemy != true) continue;' if the findEnemy is not true it will ignore and skip that iteration and move to the next until find______ bool is satisfied and runs the full code, it will also check for LOS if its not in LOS it will skip iteration etc, as for the FiringSys is where we setup our weapons, it is also used in player mechs and fixed weapons, once fully setup, drag it to the Turret scripts 'myWeapons[]' array, which then uses that to fire with.
``` C#

```

Player ---
--- See instructions to work: how it works is that it uses rigidbody.addrelativeforce to navigate its surroundings, when its on ground the player has to press space then once in the air space again to start flying which turns gravity off for the rigidbody, as for weapons control it uses raycast from the center of the camera marked by the crosshair, to tell where the assigned gunArms to fire and gets target data such as transform position for homing missiles to work.
``` C#

```

# References

# What I'm most Proud of in the Assignment

First thing I'm most proud of is getting turrets to work mostly the way I wanted them to, along with the intent of making the scripts as modular as possible for my usage which I most likely achieved in my perspective, but on the flipside with the nature of the project with so many objects that targetable caused intense lagspikes, I would definately try and improve on the code and use it for future projects

Second would be the Player controller, as it shows progress towards my ability to make games that I want with that kind of controller other than making generation code and AIs.

Third and last thing would be the explosion effects I've worked on with the Unity's Shuriken particle system, which is supposed to bring the wow factor to the visuals instead of using color changing materials to do so, adding in the post process really made them stand out.

Overall, I'm quite proud of my experimentation in this project especially in these areas of code, raycasts, AI, Player Controllers and city/world generation, as I may have worked on them before, it further solidifies my understanding of these concepts, what can be done and cannot, although the scope of the project maybe too big for just one person to handle and left the end product to be rather unrefined or unfinished and has no sound at all, it is an opportunity for me to try out and experiment on as many areas of code, it may imply that the project was never meant to be finished and is supposed to be a means to an end, which is to allow me to code games without a hint of uncertainty.

# Proposal submitted earlier can go here
# Game-Engines-CA1        

Game Visualizer Assignment for Game Engines  -------------------------     Student Name - Munfung Look -  Student Number - C18510403  
Class: DT508


Description

A playable game visualizer, with an alien invasion setting laying siege over upon a futuristic city in the night sky, lasers will be fired in every direction creating a massive laser lightshow and fantastical explosions in the night sky, with that in mind the game can be left on without being played, and have the background play by itself, with an ongoing war between the city and the extraterrestrial invaders

When engaged with, the player will spawn in as a mecha/fighter to fight off the initial alien invasion spawned in as background for visualization purposes, and then the enemies will spawn in waves from then on, until the last wave the boss will spawn in as a wondrous lightshow of a ship which will lay down a brilliant blinding siege upon the city and the individual parts will target the player as well, once defeated, you gain new interations to the game's visualizer system and return to the menu, where everything returns back to visualiser mode again.

Overall the game will be largely built mainly with a city generator code, generating destructible buildings, the player has to defend it from the invaders, the boss will use some similar code from the city generator, that can generate a new unique boss shape every time a new game has started, as for the player controller will be the usual WASD SHIFT SPACE controls to move across the 3 axis, which is subject to change and fine tuned later in the development process, as for the systematic which is a big part of the game's 'visualizer' if pulled off correctly, is where the enemies will randomly fire at the city below and also have allies and enemies interact with each other by firing at each other, and finally the dynamics and the visuals themselves are particle effects assisted by post processing effects such as bloom, chromatic aberration etc.

Theme/Mood: (Theme(s) may or not be fully included in the final product)
- Synthwave 80s
- Alien Invasion
- Mecha
- Lightshow

Used Packages:
- Probuilder
- Post Processing
- (Add new if any)

PRODUCT BACKLOG

IMPORTANT ITEMS: (Most important - Top > Down)
- Procedurally Generated City System
- Procedurally Generated Boss System
- Fully Destructible City, Units & Boss ()
- City & Boss Prefab Gen Models, & Materials
- Player Controller
- Systematic Interactions In Game
- EXPLOSIONS, Lasers, Giant Death Rays, 
- Alien Ships, Mechas/Fighters
- Enemy/Ally Unit Targeting Systems

MINOR/POLISH ITEMS:
- Menu System
- Splash Screen
- Player Mecha/Fighter Model
- Visualizer Centerpiece
- Optimization
- More Optimization....

HOPES & DREAMS: (Only implement if all of the above is done and/or system hardware allows it)
- Procedurally Generated Enemy Units
- City Physics







