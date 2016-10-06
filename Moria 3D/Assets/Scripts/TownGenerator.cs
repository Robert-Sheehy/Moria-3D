using UnityEngine;
using System.Collections;
using System;

public class TownGenerator : MonoBehaviour
{
    int[,] world;
    int dimensionOfWorld = 50;
    int minRoomDimension = 8;
    int maxRoomDimension = 12;
    int numberOfRooms = 6;
    public int doorNumber = 0;
    int startPosi, startPosj;
    public GameObject wallPrefab;
    public GameObject doorPrefab;

    // Use this for initialization
    void Start()
    {
        world = new int[dimensionOfWorld, dimensionOfWorld];
        startPosi = dimensionOfWorld / 2;
        startPosj = startPosi;

        for (int i = 0; i < dimensionOfWorld; i++)
            for (int j = 0; j < dimensionOfWorld; j++)
            {
                world[i, j] = 0;

                if (i == 0 || j == 0 || i == dimensionOfWorld - 1 || j == dimensionOfWorld - 1)
                    world[i, j] = -7;
            }

        generateLevel();

        for (int i = 0; i < dimensionOfWorld; i++)
            for (int j = 0; j < dimensionOfWorld; j++)
                spawnObjectsInWorld(i, j);

        GameObject Plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Plane.transform.position = new Vector3(startPosi, 0, startPosj) - Vector3.one / 2;
        Plane.transform.localScale = new Vector3(5, 1, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void generateLevel()
    {
        for (int roomNumber = 1; roomNumber <= numberOfRooms; roomNumber++)
        {
            int iSize = UnityEngine.Random.Range(minRoomDimension, maxRoomDimension);
            int jSize = UnityEngine.Random.Range(minRoomDimension, maxRoomDimension);

            int jPos = UnityEngine.Random.Range(2, dimensionOfWorld - iSize - 2);
            int iPos = UnityEngine.Random.Range(2, dimensionOfWorld - iSize - 2);

            if (canPlaceRoom(iPos, jPos, iSize, jSize))
                generateRoom(iPos, jPos, iSize, jSize, roomNumber);

            else roomNumber--;

        }
    }

    private bool canPlaceRoom(int iPos, int jPos, int iSize, int jSize)
    {
        bool canPlace = true;

        for (int i = iPos - 2; i < iPos + iSize + 2; i++)
            for (int j = jPos - 2; j < jPos + jSize + 2; j++) {

                /*i = Mathf.Clamp(i, 0, dimensionOfWorld - 1);
                j = Mathf.Clamp(j, 0, dimensionOfWorld - 1);*/
                canPlace = canPlace && valueInarrayRange(i) && valueInarrayRange(j) && (world[i, j] == 0);

            }

        return canPlace;
    }

    private bool valueInarrayRange(int i)
    {
        return (i >= 0) && (i < dimensionOfWorld);
    }

    private void generateRoom(int iPos, int jPos, int iSize, int jSize, int roomNumber)
    {
        int randPos = UnityEngine.Random.Range(2, minRoomDimension - 1);
        int randNum = UnityEngine.Random.Range(1, 5);

        for (int i = iPos; i < iPos + iSize; i++)
            for (int j = jPos; j < jPos + jSize; j++)
            {
                world[i, j] = -roomNumber;

                if(randNum == 1)
                {
                    if (i == iPos && j == jPos + jSize - randPos)
                    {
                        world[i, j] = 0;
                        doorNumber = -roomNumber;
                        Instantiate(doorPrefab, correctPos(i,j), Quaternion.identity);
                        doorPrefab.name = "Door Number " + doorNumber;
                    }
                }
                else if(randNum == 2)
                {
                    if (j == jPos && i == iPos + iSize - randPos)
                    {
                        world[i, j] = 0;
                        doorNumber = -roomNumber;
                        Instantiate(doorPrefab, correctPos(i, j), Quaternion.identity);
                        doorPrefab.name = "Door Number " + doorNumber;
                    }   
                }
                else if(randNum == 3)
                {
                    if (i == iPos + iSize - 1 && j == jPos + jSize - randPos)
                    {
                        world[i, j] = 0;
                        doorNumber = -roomNumber;
                        Instantiate(doorPrefab, correctPos(i, j), Quaternion.identity);
                        doorPrefab.name = "Door Number " + doorNumber;
                    }
                }
                else
                {
                    if (j == jPos + jSize - 1 && i == iPos + iSize - randPos)
                    {
                        world[i, j] = 0;
                        doorNumber = -roomNumber;
                        Instantiate(doorPrefab, correctPos(i, j), Quaternion.identity);
                        doorPrefab.name = "Door Number " + doorNumber;
                    }
                }
            }
    }

    private void spawnObjectsInWorld(int i, int j)
    {
        int charId = world[i, j];

        if (charId < 0)
            Instantiate(wallPrefab, correctPos(i, j), Quaternion.identity);
    }

    private Vector3 correctPos(int i, int j)
    {
        return new Vector3(i, 0, j);
    }

    private void changeScene()
    {

    }
}
