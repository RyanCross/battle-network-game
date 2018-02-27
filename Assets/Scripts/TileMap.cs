using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
 
    // even though public is here, by default it wont show up in the inspector. Need to add Serializable to the class
    public TilePrefabs[] tileTypes;
    public GameObject player1;

    public int mapSizeX = 3;
    public int mapSizeY = 6;

    public TileObject[,] tiles;

    // Use this for initialization
    void Start()
    {
        // Allocate our map tiles
        tiles = new TileObject[mapSizeX, mapSizeY];
        InitializeMapData();
        GenerateMapVisual();
    }

    // Populates the scene with tile Prefabs and saves a reference to the instantiated objects in the tiles array.
    void GenerateMapVisual() 
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TilePrefabs tileType = tileTypes[ (int) tiles[x, y].GetTileOwner() ];
                GameObject tileInstant =  Instantiate(tileType.tileVisualPrefab, new Vector3(0 , 0, 0), Quaternion.identity);
                Transform t = tileInstant.GetComponent<Transform>();
                float localScaleX = t.localScale.x;
                float localScaleZ = t.localScale.z;

                //the y coordinate of the 2 dimensional array will actually represent the z position
                t.SetPositionAndRotation(new Vector3(x * localScaleX, 0, y * localScaleZ), Quaternion.identity);
            }
        }
    }

    // Allocate space for the map tiles, assigns coordinates to their tiles and assigns the default ownership in order to create an even playing field.
    void InitializeMapData()
    {
        if (mapSizeY % 2 != 0 )
        {
            throw new InvalidMapSizeException("Error, the Y dimension of the map must be an even number");
        }

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = new TileObject();
                tiles[x, y].SetXCoord(x);
                tiles[x, y].SetYCoord(y);

                if (y < (mapSizeY/2) )
                {
                    tiles[x, y].SetTileOwner(TileOwner.Player1);
                }
                else
                {                   
                    tiles[x, y].SetTileOwner(TileOwner.Player2);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MovePlayerTo()
    {

    }

}
