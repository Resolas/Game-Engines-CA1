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

This is the generation parameters, that allows me to edit the size, and what can spawn as part of the ship, (Note: zSpacing is to correctly space out to prevent overlap, in this case I've set it to 20)
``` C#
[Header("Generation Settings")]
    public int frontMaxLength = 5;
    public int rearMaxLength = 5;
    public int frontMinLength = 3;
    public int rearMinLength = 3;

    public EnemyPrefabTable myGenTable;

    public float zSpacing;


```

This is the function that generates the ship (note: I may have messed up with the naming going where but it does exactly what it does)
``` C#

    void GenerateShip()
    {
        int frontRng = Random.Range(frontMinLength,frontMaxLength);
        int rearRng = Random.Range(rearMinLength,rearMaxLength);

        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;

        // Front
        for (int i = 1; i < frontRng; i++)
        {

            if (i < frontRng - 1)
            {
                int bodyMod = Random.Range(0, myGenTable.bodyModules.Length);
                GameObject bodyBlock = Instantiate(myGenTable.bodyModules[bodyMod], new Vector3(xPos,yPos,zPos + zSpacing * -i),transform.rotation);
                bodyBlock.transform.parent = gameObject.transform;
            }
            else
            {
                int rearMod = Random.Range(0, myGenTable.rearModules.Length);
                GameObject rearBlock = Instantiate(myGenTable.rearModules[rearMod], new Vector3(xPos, yPos, zPos + zSpacing * -i), transform.rotation);
                rearBlock.transform.parent = gameObject.transform;
            }
            
        }

        // Rear
        for (int i = 1; i < rearRng; i++)
        {

            if (i < rearRng - 1)
            {
                int bodyMod = Random.Range(0, myGenTable.bodyModules.Length);
                GameObject bodyBlock = Instantiate(myGenTable.bodyModules[bodyMod], new Vector3(xPos, yPos, zPos + zSpacing * i), transform.rotation);
                bodyBlock.transform.parent = gameObject.transform;
            }
            else
            {
                int frontMod = Random.Range(0, myGenTable.bowModules.Length);
                GameObject frontBlock = Instantiate(myGenTable.bowModules[frontMod], new Vector3(xPos, yPos, zPos + zSpacing * i), transform.rotation);
                frontBlock.transform.parent = gameObject.transform;
            }

            
        }


    }



```

Boss Generation ---
While the boss has no script on its own it relies mainly on turrets and other existing scripts that lets it do things, I used a simple coroutine script that will slowly generate overtime generate all the boss' weapons available to it in the prefab tables.

This here uses a simple coroutine script, instead of spawning all the modules at once, it spawns them at 1 second intervals until it reaches the spawnCount, once instantiated the object will be parented referenced transform in this case an empty rotator
``` C#
public class BossModuleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myCoRoutine = StartCoroutine(SpawnModules());
    }
    Coroutine myCoRoutine;

    public int spawnCount = 20;
    public BossPrefabTable myPrefabTable;
    public Transform parentToRing;
    

    IEnumerator SpawnModules()
    {

        while (true)
        {

            for (int i = 0; i < spawnCount; i ++)
            {
                int rng = Random.Range(0,myPrefabTable.myModules.Length);
                GameObject mySpawnedMod = Instantiate(myPrefabTable.myModules[rng],transform.position,transform.rotation);
                mySpawnedMod.transform.SetParent(parentToRing);

                yield return new WaitForSeconds(1);
            }

            StopCoroutine(myCoRoutine);
        }
    }
}



```

Turrets ---
The turrets use 'Turret' and 'FiringSys' Scripts, in which together allows me to create various turret types and weapons, firstly the turret checks for viable tags in targets, when an object is in its overlapsphere, it will check for its tag and what this turret is looking for e.g 'if (target.CompareTag("Enemy") && findEnemy != true) continue;' if the findEnemy is not true it will ignore and skip that iteration and move to the next until find______ bool is satisfied and runs the full code, it will also check for LOS if its not in LOS it will skip iteration etc, as for the FiringSys is where we setup our weapons, it is also used in player mechs and fixed weapons, once fully setup, drag it to the Turret scripts 'myWeapons[]' array, which then uses that to fire with.

This part of the turret script here, tracks the target using various variables, so with that I can create both friend and foe turrets by ticking the findEnemy or findFriendly for example, and if its not satisfied it will skip the iteration of THAT target in range, and it will also skip if there is no LOS between that object and the turret hopefully sparing some computing resources.
``` C#

    void TrackTarget()
    {

        Collider[] targets = Physics.OverlapSphere(transform.position,range);
        

        float nearestDist = Mathf.Infinity;
        Transform closestTarget = null;
        bool isVis;

        foreach (Collider target in targets)
        {
            if (target.CompareTag("Untagged") || target.CompareTag("Projectile") || target.CompareTag("Ground")) continue;    // Ignore everything that's Untagged
            if (target.CompareTag("Enemy") && findEnemy != true) continue;
            if (target.CompareTag("Friendly") && findFriendly != true) continue;
            if (target.CompareTag("Player") && findPlayer != true) continue;
            if (target.CompareTag("Structure") && findStructure != true) continue;

            // LOS
            RaycastHit hit;

            if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit) && hit.transform.tag == target.transform.tag)
            {
                isVis = true;
                Debug.DrawLine(target.transform.position, transform.position, Color.green);
            }
            else
            {
                isVis = false;
                Debug.DrawLine(target.transform.position, transform.position, Color.red);
                continue;
            }




            //    Vector3 pointAB = target.transform.position - transform.position;
            getDistToTarget = Vector3.Distance(transform.position,target.transform.position);   // Get dist between point A and B
            
            
            if (getDistToTarget <= nearestDist && isVis)                                                 // If new object is closer than previous, becomes the new closest target
            {
                nearestDist = getDistToTarget;
                closestTarget = target.GetComponent<Transform>();
                
            }

            if (closestTarget != null && nearestDist < range && isVis)      // If not null finalize the target
            {
                myTarget = closestTarget.transform;
                
                Debug.Log("TrackTestHasTarget");
            }
            else
            {
                myTarget = null;                // NOTE this does not seem to work, target is reset at IEnumerator
                Debug.Log("TrackTestNull");
            }
         //   Debug.Log("nearest target " + nearestDist + " " + closestTarget + " " + myTarget);

         //   myTarget = closestTarget.GetComponent<Transform>();   // Finalized Target

        }

    }

```

Homing and Getting Target ---
This is the most interesting and prominent pieces of code in the project, that makes the 'game' look nice and the more interesting parts for me to code and learn from, as the homing code is self explanatory below, I'll talk about the target identification, how it gets its is A it gets its target from the intermediary script (FiringSys) which is from the turret tracking its target, which sets its homingTarget of the target's transform upon instantiation, which in turn it will lock onto any anything the turret is targeting and chases it.
``` C#

void ModeHoming()
    {
        if (myHomingTarget != null)
        {
            myRB.velocity = transform.forward * speed;

            var targetRot = Quaternion.LookRotation(myHomingTarget.position - transform.position);

            myRB.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed * Time.deltaTime));
        }
        else
        {
            for (int i = 0; i < effectsOnHit.Length; i++)
            {
                Instantiate(effectsOnHit[i], transform.position, Quaternion.identity);
            }

            for (int i = 0; i < leaveExEffectsOnHit.Length; i++)
            {
                leaveExEffectsOnHit[i].transform.parent = null;
            }

            Destroy(gameObject);
        }
    }

```
and B which the player uses via raycast.collider out from the center of the camera, and sends back its transform directly to the missiles upon instantiation which it will lock onto ANYTHING, it also gets the position for where the normal guns shoot at via hit.point.
``` C#

void GetFiringPosition()
    {

        RaycastHit hit;

        if (Physics.Raycast(myCamera.transform.position, myCamera.transform.forward, out hit, Mathf.Infinity))
        {

            var getPoint = hit.point;   // The position the ray hits
            Vector3 hitPos = getPoint;
            var getCol = hit.collider;  // The target data for homing weapons

            for (int i = 0; i < myWeaponsTrans.Length; i++)
            {
                if (weaponLookPos[i]) myWeaponsTrans[i].transform.LookAt(hitPos);

                if (getLockOn[i]) myWeaponSys[i].setTarget = getCol.transform;


            }

            Debug.Log("RAYPOS + " + hitPos + "  " + getCol.transform);
        }


    }

```


Player ---
--- See instructions to work: how it works is that it uses rigidbody.addrelativeforce to navigate its surroundings, when its on ground the player has to press space then once in the air space again to start flying which turns gravity off for the rigidbody, as for weapons control it uses raycast from the center of the camera marked by the crosshair, to tell where the assigned gunArms to fire and gets target data such as transform position for homing missiles to work.

This is the movement part of the script, by using relativeforce, instead of transform.translate, gave me a lot of opportunities of what to do rigidbody/physics based movement, albeit the submission's movement is unfinished, I will now know what to do with this type of movement, what it does with relativeforce, I can rotate the player, move and sidestep on its local axis instead of global.
``` C#
void Movement()
    {

        horz = Input.GetAxis("Horizontal"); // Strafe side to side
        vert = Input.GetAxis("Vertical");   // Move forwards & backwards

        if (isFlying)   // Flight Mode
        {
            

            myRB.useGravity = false;
            height = Input.GetAxis("Fly");   // Ascend & Descend
            myRB.AddForce(Vector3.up * speed * height * Time.deltaTime);




        }
        else {
            myRB.useGravity = true;
        }


        myRB.AddRelativeForce(Vector3.right * speed * horz * Time.deltaTime);
        myRB.AddRelativeForce(Vector3.forward * speed * vert * Time.deltaTime);

        

        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            myRB.velocity = new Vector3(myRB.velocity.x,jumpPower,myRB.velocity.z);

            
        }
        else if (onGround != true && Input.GetKeyDown(KeyCode.Space)) isFlying = true;       // sets it to flying while not on ground

        transform.Rotate(0,Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime,0);    // Rotate the player with mouse
        rotYCam.transform.Rotate(-Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime,0,0); // rotate child object to look up and down
    }
```

Note: I'm not going to talk about every script, only the important parts and the ones that are seen on the video otherwise it will take too long!

# References

Mech Inspiration
https://static.myfigurecollection.net/upload/pictures/2010/10/08/112464.jpeg

Boss Weapon Inspiration
https://i.ytimg.com/vi/JI_Hhy1hras/maxresdefault.jpg

Homing Missile Tutorial
https://www.youtube.com/watch?v=feTek1j1Beo&ab_channel=XenosmashGames

# What I'm most Proud of in the Assignment

First thing I'm most proud of is getting turrets to work mostly the way I wanted them to, along with the intent of making the scripts as modular as possible for my usage which I most likely achieved in my perspective, but on the flipside with the nature of the project with so many objects that targetable caused intense lagspikes, I would definately try and improve on the code and use it for future projects

Second would be the Player controller, as it shows progress towards my ability to make games that I want with that kind of controller other than making generation code and AIs. Especially for games involving rigidbody/physics based controls

Third and last thing would be the explosion effects I've worked on with the Unity's Shuriken particle system, which is supposed to bring the wow factor to the visuals instead of using color changing materials to do so, adding in the post process really made them stand out.

Overall, I'm quite proud of my experimentation in this project especially in these areas of code, raycasts, AI, Player Controllers and city/world generation, as I may have worked on them before, it further solidifies my understanding of these concepts, what can be done and cannot, although the scope of the project maybe too big for just one person to handle and left the end product to be rather unrefined or unfinished,has no sound at all and some content in the project left unused, it is an opportunity for me to try out and experiment on as many areas of code, it may imply that the project was never meant to be finished and is supposed to be a means to an end, which is to allow me to code games without a hint of uncertainty.

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







