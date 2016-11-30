using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DungeonGenerator
{
    Ivector2 startPosition;
    
    
    
    enum pathChoices {deadEnd, keepGoing, changeDir, changeFork, crossroads }  
    int dimensionOfWorld;
    int minRoomDimension;
    int maxRoomDimension;
    int numberOfRooms;
    int[,] maze;
    bool[] connectedRooms;



    private int RoomId;
    private int maxNumberOfDoorsInRoom = 5;

    public DungeonGenerator(int dimensionOfWorld, int minRoomDimension, int maxRoomDimension, int numberOfRooms, int roomDoors)
    {
        // TODO: Complete member initialization
        this.dimensionOfWorld = dimensionOfWorld;
        this.minRoomDimension = minRoomDimension;
        this.maxRoomDimension = maxRoomDimension;
        this.numberOfRooms = numberOfRooms;
        maxNumberOfDoorsInRoom = roomDoors;
        maze = new int[dimensionOfWorld, dimensionOfWorld];
        startPosition = new Ivector2(  dimensionOfWorld / 2, dimensionOfWorld/2);

        FillWithWalls();
        GenerateRooms();
        generateSubMazeFromHere(new Ivector2(1, 1), new Ivector2(0, 1));
        connectedRooms = new bool[numberOfRooms+1];
        connectRooms();
        sparsify();
        addItems(50);
    }

    private void addItems(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Ivector2 itemPos = getRandomEmptySpace();
            maze[itemPos.x, itemPos.y] = UnityEngine.Random.Range(10, 13);
        }
    }

    public Ivector2 getRandomEmptySpace()
    {
        Ivector2 pos;
        do
        {
            pos = new Ivector2(UnityEngine.Random.Range(0, dimensionOfWorld), UnityEngine.Random.Range(0, dimensionOfWorld));
        } while (maze[pos.x, pos.y] != (int)Item.list.emptySpace);

        return pos;
    }

    private void sparsify()
    {
        for (int i = 1; i < dimensionOfWorld - 1; i++)
            for (int j = 1; j < dimensionOfWorld - 1; j++)
            {
                Ivector2 pos = new Ivector2(i, j);
                if (isDeadEnd(pos))
                {
                    int choice = UnityEngine.Random.Range(0, 20);
                    if (choice < 17)
                        fillCuldeSac(pos);

                    else

                        switch (choice)
                        {
                            case 17:
                                burstThrough(pos, (int)Item.list.door);
                                break;

                            case 18:

                                burstThrough(pos, (int)Item.list.hiddenDoor);// Hidden Door;
                                break;

                        }
                }

            }
    }

    private void burstThrough(Ivector2 pos, int id)
    {
        Ivector2 dir = new Ivector2(1, 0);

        Ivector2 next = pos;
        for (int k = 0; k < 4; k++)
        {
            if (isSuitableDoor(pos + dir, dir))
            {
                setMazeAt(pos + dir, id);

            }
            dir = ~dir;
        }
    }

    private void fillCuldeSac(Ivector2 pos)
    {

        Ivector2 dir = new Ivector2(1, 0);
        int total = 0;
        Ivector2 next = pos;
        for (int k = 0; k < 4; k++)
        {
            if (mazeAt(pos + dir) != 1)
            {
                total++;
                next = pos + dir;
            }
            dir = ~dir;
        }

        if (total == 1)
        {
            // Debug.Log(pos.x + " , " + pos.y);
            setMazeAt(pos, 1);
            fillCuldeSac(next);
        }
    }

    private bool isDeadEnd(Ivector2 pos)
    {
        if (mazeAt(pos) == 1) return false;

        Ivector2 dir = new Ivector2(1, 0);
        int total = 0;

        for (int k = 0; k < 4; k++)
        {
            if (mazeAt(pos + dir) == 1) total++;
            dir = ~dir;
        }
        return (total == 3);

    }

    private void connectRooms()
    {
        for (int i = 0; i < dimensionOfWorld; i++)
            for (int j = 0; j < dimensionOfWorld; j++)
            {
                //   Debug.Log(" Position " + i + " , " + j + " space Id " + maze[i, j]);
                if ((maze[i, j] < 0) && (!connectedRooms[-maze[i, j]]))
                {
                    int numberOfDoors = UnityEngine.Random.Range(0, maxNumberOfDoorsInRoom);
                    connectedRooms[-maze[i, j]] = true;

                    for (int d = 0; d < maxNumberOfDoorsInRoom; d++)

                        connectRoomAt(i, j);
                }

            }
    }

    private void connectRoomAt(int i, int j)
    {
        int RoomId = maze[i, j];
        Ivector2 exitDirection = newRandomDirection();
        Ivector2 absPerp = ~exitDirection;
        absPerp.x = Math.Abs(absPerp.x);
        absPerp.y = Math.Abs(absPerp.y);

        Ivector2 position = new Ivector2(i, j);
        position += UnityEngine.Random.Range(0, maxRoomDimension) * absPerp;
        if (IsInRoom(RoomId, position))
        {
            position = goToEdge(position, RoomId, exitDirection);

            if (isSuitableDoor(position, exitDirection)) setMazeAt(position, randomDoor());
            else connectRoomAt(i, j);
        }
        else connectRoomAt(i, j);
    }

    private int randomDoor()
    {
        /*
        int i = UnityEngine.Random.Range(0, 4);
        if (i == 1) return randomDoor();
        else return i;
        */
        int[] doorItems = new int[] {
            (int)Item.list.emptySpace,
            (int)Item.list.hiddenDoor,
            (int)Item.list.door
        };

        return doorItems[UnityEngine.Random.Range(0, doorItems.Length)];
    }

    private bool isSuitableDoor(Ivector2 position,Ivector2 exitDir)
    {

        return  (mazeAt(position) == 1) &&
                (mazeAt(position + ~exitDir) == 1) && 
                (mazeAt(position - ~exitDir) == 1) && 
                (mazeAt(position + exitDir) != 1) && 
                (mazeAt(position + exitDir) != mazeAt(position - exitDir)) &&
                (position.x > 0 && position.x < dimensionOfWorld - 1) &&
                (position.y > 0 && position.y < dimensionOfWorld - 1);
    }

    private Ivector2 goToEdge(Ivector2 position, int RoomId, Ivector2 exitDirection)
    {
        Ivector2 next;
        for (next = position; mazeAt(next) == RoomId; next += exitDirection) { }
        return next;
    }

    private bool IsInRoom(int RoomId, Ivector2 position)
    {
        return RoomId == mazeAt(position);
    }

    private int mazeAt(Ivector2 query)
    {
        if (isOnMap(query)) return maze[query.x, query.y];

        //   Debug.Log("Index O.O.R");
        return -999;
    }

    public void setMazeAt(Ivector2 Pos, int value)
    {
        maze[Pos.x, Pos.y] = value;
    }


    private bool isOnMap(Ivector2 posn)
    {
        return isOnMap(posn.x, posn.y);
    }

    private bool isOnMap(int i, int j)
    {
        return (i >= 0) && (i < dimensionOfWorld) && (j >= 0) & (j < dimensionOfWorld);
    }

    private void FillWithWalls()
    {
        for (int i = 0; i < dimensionOfWorld; i++)
            for (int j = 0; j < dimensionOfWorld; j++)
            {
                maze[i, j] = 1;

            }
    }

    private void generateSubMazeFromHere(Ivector2 position, Ivector2 preferredDirection)
    {

        //  Debug.Log("Generate SubMaze Position" + position.x + " , " + position.y);

        setMazeAt(position, 0);

        for (Ivector2[] availables = getAvailableDirections(position, preferredDirection); availables.Length > 0; availables = getAvailableDirections(position, preferredDirection))
        {

            int choice = UnityEngine.Random.Range(0, 10);
            Ivector2 chosenDirection = availables[0];

            if ((choice > 8) && (availables.Length > 1)) chosenDirection = availables[UnityEngine.Random.Range(1, availables.Length)];

            setMazeAt(position + chosenDirection, 0);

            generateSubMazeFromHere(position + 2 * chosenDirection, chosenDirection);
        }
    }

    private Ivector2[] getAvailableDirections(Ivector2 position, Ivector2 preferredDirection)
    {
        List<Ivector2> listOfAvailable = new List<Ivector2>();

        Ivector2 dir = preferredDirection;
        for (int i = 0; i < 4; i++)
        {
            //    Debug.Log("Checking " + (position + 2 * dir).x + " , " + (position + 2 * dir).y);
            if (mazeAt(position + 2 * dir) == 1)
            {
                //     Debug.Log("Yes Adding Direction " + dir.x + " , " + dir.y);
                listOfAvailable.Add(dir);

            }
            dir = ~dir;
        }

        return listOfAvailable.ToArray();

    }

    public void TestgetAvailableDirections(int pi, int pj, int di, int dj)
    {
        Debug.Log("Testing getAvailableDirections with position " + pi + " , " + pj + " with direction " + di + " , " + dj);
        Ivector2[] output = getAvailableDirections(new Ivector2(pi, pj), new Ivector2(di, dj));
        Debug.Log(" Output an array of length " + output.Length);
        for (int i = 0; i < output.Length; i++)
            Debug.Log(" Direction " + i + "  is " + output[i].x + " , " + output[i].y);

    }

    private bool canGererateSubMazeFromHere(Ivector2 startPosition)
    {
        bool canDo = false;
        for (int i = startPosition.x - 2; i <= startPosition.x + 2; i++)
            for (int j = startPosition.y - 2; j <= startPosition.y + 2; j++)
                canDo = canDo && (maze[i, j] == 1);
        return canDo;
    }

    private Ivector2 newRandomDirection()
    {
        return newRandomDirection(new Ivector2(0, 0));
    }

    private void GenerateRooms()
    {
        for (int roomNumber = 1; roomNumber <= numberOfRooms; roomNumber++)
        {
            int iSize = 2 * UnityEngine.Random.Range(minRoomDimension / 2, maxRoomDimension / 2) + 1;
            int jSize = 2 * UnityEngine.Random.Range(minRoomDimension / 2, maxRoomDimension / 2) + 1;

            int iPosition = 2 * (UnityEngine.Random.Range(2, dimensionOfWorld - iSize - 2) / 2) + 1;
            int jPosition = 2 * (UnityEngine.Random.Range(2, dimensionOfWorld - jSize - 2) / 2) + 1;
            if (canPlaceRoom(iPosition, jPosition, iSize, jSize))
                generateRoom(iPosition, jPosition, iSize, jSize, roomNumber);
            else roomNumber--;
        }
    }

    private bool canPlaceRoom(
        int iPosition,
        int jPosition,
        int iSize,
        int jSize)
    {
        bool canPlace = true;

        for (int i = iPosition - 1; i < iPosition + iSize + 1; i++)
            for (int j = jPosition - 1; j < jPosition + jSize + 1; j++)
            {
                canPlace = canPlace && (maze[i, j] == 1);
            };

        return canPlace;

    }
    private void generateRoom(
        int iPosition,
        int jPosition,
        int iSize,
        int jSize, int roomNumber)
    {
        for (int i = iPosition; i < iPosition + iSize; i++)
            for (int j = jPosition; j < jPosition + jSize; j++)
            {
                maze[i, j] = -roomNumber;
            };
    }

    internal int[,] generate()
    {
        return maze;
    }

    private Ivector2 newRandomDirection(Ivector2 direction)
    {
        Ivector2 output = direction;
        int roll = UnityEngine.Random.Range(0, 4);
        switch (roll)
        {
            case 0:
                output = new Ivector2(1, 0);
                break;
            case 1:
                output = new Ivector2(-1, 0);
                break;
            case 2:
                output = new Ivector2(0, 1);
                break;
            case 3:
                output = new Ivector2(0, -1);
                break;



        }
        if (output != direction) return output;
        else return newRandomDirection(direction);

    }

    private void initialSetupforConnectnessArray()
    {
        connectedRooms = new bool[numberOfRooms + 1];
    }

    private bool positionOutOfMap(int currenti, int currentj)
    {
        return (currenti < 0) || (currenti >= dimensionOfWorld) || (currenti < 0) || (currenti >= dimensionOfWorld);
    }
}
