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
    int startPosi, startPosj;
    public GameObject wallPrefab;

    // Use this for initialization
    void Start()
    {
        world = new int[dimensionOfWorld, dimensionOfWorld];
        startPosi = dimensionOfWorld / 2;
        startPosj = startPosi;

        for (int i = 0; i < dimensionOfWorld; i++)
            for (int j = 0; j < dimensionOfWorld; j++)
                world[i, j] = 1;

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
                canPlace = canPlace && (world[i, j] == 1);

            }

        return canPlace;
    }

    private void generateRoom(int iPos, int jPos, int iSize, int jSize, int roomNumber)
    {
        int randPos = UnityEngine.Random.Range(2, minRoomDimension - 1);
        int randNum = UnityEngine.Random.Range(1, 3);

        for (int i = iPos; i < iPos + iSize; i++)
            for (int j = jPos; j < jPos + jSize; j++)
            {
                world[i, j] = -roomNumber;

                if(randNum == 1)
                {
                    if (i == iPos && j == jPos + jSize - randPos)
                        world[i, j] = 1;
                }
                else
                    if (j == jPos && i == iPos + iSize - randPos)
                        world[i, j] = 1;
            }
    }

    private void spawnObjectsInWorld(int i, int j)
    {
        int charId = world[i, j];

        if (charId != 1)
            Instantiate(wallPrefab, correctPos(i, j), Quaternion.identity);
    }

    private Vector3 correctPos(int i, int j)
    {
        return new Vector3(i, 0, j);
    }
}
