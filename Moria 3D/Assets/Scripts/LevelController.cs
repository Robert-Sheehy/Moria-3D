using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{

    public Transform wallPrefab;
    public Transform doorPrefab;

    int[,] world;
    bool[,] spawned;
    int dimensionOfWorld = 101;

    int minRoomDimension = 3;
    int maxRoomDimension = 20;
    int numberOfRooms = 40;
    private int startPosi, startPosj;
    // Use this for initialization
    DungeonGenerator dungeon;
    private int PosI, PosJ, DirI, DirJ;
    private int maxNumbersOfDoorsinRooms = 3;

    void Start()
    {
        dungeon = new DungeonGenerator(dimensionOfWorld, minRoomDimension, maxRoomDimension, numberOfRooms, maxNumbersOfDoorsinRooms);
        world = dungeon.generate();
        spawned = new bool[dimensionOfWorld, dimensionOfWorld];
        
        SpawnWorldItemsAroundPoint(1000, UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1));

    }

    public void isHiddenDoor(Ivector2 position)
    {


    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            dungeon.TestgetAvailableDirections(PosI, PosJ, DirI, DirJ);

    }

    public void SpawnWorldItemsAroundPoint(int radius, int x, int y)
    {
        Vector2 pos = new Vector2(x, y);
        int ri = 0;
        int rj = 0;
        bool roomFound = false;
        for (int i = 0; i < dimensionOfWorld; i++)
        {
            for (int j = 0; j < dimensionOfWorld; j++)
            {
                if (Vector2.Distance(pos, new Vector2(i, j)) < radius)
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

        Object currentItem;

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

    private Vector3 correctPosition(int i, int j)
    {
        return new Vector3(i, 0, j);
    }
}
