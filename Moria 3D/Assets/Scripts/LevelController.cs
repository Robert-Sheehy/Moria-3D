using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    public Transform wallPrefab;
    public Transform doorPrefab;
    
    private GameObject player;

    public int[,] world;
    bool[,] spawned;
    int dimensionOfWorld = 101;

    int minRoomDimension = 3;
    int maxRoomDimension = 20;
    int numberOfRooms = 40;
    private int startPosi, startPosj;
    // Use this for initialization
    public DungeonGenerator dungeon;
    private int PosI, PosJ, DirI, DirJ;
    private int maxNumbersOfDoorsinRooms = 3;

    private ObjectManager objManager = new ObjectManager();

    void Start()
    {
        dungeon = new DungeonGenerator(dimensionOfWorld, minRoomDimension, maxRoomDimension, numberOfRooms, maxNumbersOfDoorsinRooms);
        player = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Ivector2 emptySpace = dungeon.getRandomEmptySpace();
        player.transform.position = correctPosition(emptySpace.x, emptySpace.y);
        player.AddComponent<CharacterControl>();

        world = dungeon.generate();
        spawned = new bool[dimensionOfWorld, dimensionOfWorld];

    }

    public Item.list getDoor(Ivector2 position)
    {
        switch(world[position.x, position.y]){

            case (int)Item.list.hiddenDoor:
                return Item.list.hiddenDoor;
            case (int)Item.list.door:
                return Item.list.door;
            default:
                return Item.list.emptySpace;
        }
        //return (world[position.x, position.y] == (int)Item.list.hiddenDoor || world[position.x, position.y] == (int)Item.list.door);
    }

    public Ivector2 getRandomEmptyspace()
    {
        Ivector2 pos;
        do
        {
            pos = new Ivector2(Random.Range(0, dimensionOfWorld - 1), Random.Range(0, dimensionOfWorld - 1));
        } while (queryLevel(pos) != (int)Item.list.emptySpace);

        return pos;
    }

    void openDoorsAroundPoint(float radius, Ivector2 position, float chance)
    {
        for(int i = 0; i < dimensionOfWorld; i++) 
            for(int j = 0; j < dimensionOfWorld; j++)
            {
                if(Vector2.Distance(new Vector2(position.x, position.y), new Vector2(i, j)) < radius)
                {
                    if ((getDoor(new Ivector2(i, j)) == Item.list.door) || (getDoor(new Ivector2(i, j)) == Item.list.hiddenDoor && Random.Range(0.0F, 1.0F) < chance)) {

                        swapWorldObjects(new Ivector2(i, j), Item.list.emptySpace);
                    }
                }
            }
    }

    public int queryLevel(Ivector2 pos)
    {
        return queryLevel(pos.x, pos.y);
    }

    public int queryLevel(int x, int y)
    {
        if (x < (dimensionOfWorld - 1) && (x >= 0) &&  (y < dimensionOfWorld - 1) && (y >= 0))
        {
            return world[x, y];
        }
        else return (int)Item.list.emptySpace;
    }

    private void swapWorldObjects(Ivector2 position, Item.list toReplace)
    {
        GameObject g;
        try
        {
            g = getGameObjectAtWorldPosition(position, new GameObject[] { player });
        } catch(System.Exception e)
        {
            throw (new System.Exception("unable to swap objects: " + e.ToString()));
        }

        Destroy(g);
        world[position.x, position.y] = (int)toReplace;
        SpawnObjectInWorld(position.x, position.y);
    }

    public GameObject getGameObjectAtWorldPosition(Ivector2 position)
    {
        return getGameObjectAtWorldPosition(position, new GameObject[0]);
    }

    public GameObject getGameObjectAtWorldPosition(Ivector2 position, GameObject[] ignorelist) 
    {
        Collider[] c;
        GameObject result = new GameObject();
        result.transform.position = new Vector3(float.MinValue, 0, float.MinValue);
        c = Physics.OverlapSphere(correctPosition(position.x, position.y), 0.25F);
        if(c.Length > 0)
        {
            foreach(Collider collider in c)
            {
                if(Vector3.Distance(collider.transform.position, result.transform.position) > Vector3.Distance(collider.transform.position, correctPosition(position.x, position.y))){
                    foreach(GameObject g in ignorelist)
                    {
                        if(collider.gameObject != g)
                        {
                            result = collider.gameObject;
                        }
                    }
                }
            }
        } else throw (new System.Exception("No GameObject found at position " + position.x + ", " + position.y));

        return result;
    }

    void Update()
    {

        //KeyController.runOnKey(KeyCode.W, KeyController.keyPressType.keyDown, moveForwards()); // run given method on keypress
        /*
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.transform.position += new Vector3(0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            player.transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.transform.position += new Vector3(0, 0, -1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            player.transform.position += new Vector3(1, 0, 0);
        }
        */
        SpawnWorldItemsAroundPoint(5, (int)player.transform.position.x, (int)player.transform.position.z);
        /*
        if (Input.GetKeyDown(KeyCode.Space))
            dungeon.TestgetAvailableDirections(PosI, PosJ, DirI, DirJ);
        */
    }

    public void SpawnWorldItemsAroundPoint(float radius, int x, int y)
    {
        Vector2 pos = new Vector2(x, y);
        int ri = 0;
        int rj = 0;
        bool roomFound = false;
        for (int i = 0; i < dimensionOfWorld; i++)
        {
            for (int j = 0; j < dimensionOfWorld; j++)
            {
                if (Vector2.Distance(pos, new Vector2(i, j)) < radius && spawned[i, j] == false)
                {
                    SpawnObjectInWorld(i, j);
                    if (world[i, j] < 0)
                    {
                        roomFound = true;
                        ri = i; rj = j;

                    }
                }
            }
        }
        if (roomFound)
            makeRoomVisible(ri, rj);

        openDoorsAroundPoint(2f, new Ivector2(x, y), 0.2F);
    }

    void makeRoomVisible(int x, int y)
    {

        int roomNumber = world[x, y];
        if (world[x, y] < 0)
        {
            int widthLeft = 0,
                widthRight = 0,
                heightUp = 0,
                heightDown = 0;

            while (world[x - widthLeft, y] == roomNumber)
                widthLeft++;

            while (world[x + widthRight, y] == roomNumber)
                widthRight++;

            while (world[x, y + heightUp] == roomNumber)
                heightUp++;

            while (world[x, y - heightDown] == roomNumber)
                heightDown++;

            for (int i = x - widthLeft; i <= x + widthRight; i++)
            {
                for (int j = y - heightDown; j <= y + heightUp; j++)
                {
                    if (!objectAlreadySpawnedAt(i, j)) SpawnObjectInWorld(i, j);
                   
                }
            }
        }
    }

    private bool objectAlreadySpawnedAt(int i, int j)
    {

        return spawned[i, j];
    }
    
    private void PlaceItemIdRandomlyOnMap(Item.list id)
    {
        Vector2 randomPos;
        do
        {

            randomPos = new Vector2(UnityEngine.Random.Range(1, dimensionOfWorld - 2), UnityEngine.Random.Range(1, dimensionOfWorld - 2));
        } while (world[(int)randomPos.x, (int)randomPos.y] > 0);

        world[(int)randomPos.x, (int)randomPos.y] = (int)id;
    }

    private Vector2 newRandomDirection(Vector2 direction)
    {
        Vector2 output = direction;
        int roll = UnityEngine.Random.Range(0, 4);
        switch (roll)
        {
            case 0: output = new Vector2(1, 0);
                break;
            case 1:
                output = new Vector2(-1, 0);
                break;
            case 2:
                output = new Vector2(0, 1);
                break;
            case 3:
                output = new Vector2(0, -1);
                break;
        }
        if (output != direction) return output;
        else return newRandomDirection(direction);

    }

    private void SpawnObjectInWorld(int i, int j)
    {


        spawned[i, j] = true;
        int charId = world[i, j];

        UnityEngine.Object currentItem;

        switch (charId)
        {
            case (int)Item.list.wall:
                currentItem = Instantiate(wallPrefab, correctPosition(i, j), Quaternion.identity);
                currentItem.name = "wall";
                break;

            case (int)Item.list.hiddenDoor:
                currentItem = Instantiate(wallPrefab, correctPosition(i, j), Quaternion.identity);
                currentItem.name = "hiddenDoor";
                break;

            case (int)Item.list.door:
                currentItem = Instantiate(doorPrefab, correctPosition(i, j), Quaternion.identity);
                currentItem.name = "door";
                break;
        }
    }


    private Quaternion newRandomDirection()
    {
        switch (UnityEngine.Random.Range(0, 6))
        {
            case 0:
                return Quaternion.AngleAxis(90, Vector3.up);

            case 1:
                return Quaternion.AngleAxis(-90, Vector3.up);

            case 2:
                return Quaternion.AngleAxis(180, Vector3.up);

            case 3:
                return Quaternion.AngleAxis(90, Vector3.right);

            case 4:
                return Quaternion.AngleAxis(-90, Vector3.right);
        }


        return Quaternion.identity;
    }

    public Vector3 correctPosition(int i, int j)
    {
        return new Vector3(i, 0, j);
    }
}
