using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{

    public TilePrefabs[] tileTypes; // prefab tiles should be in the same order as PlayerType.
    public GameObject player1;

    public static int mapSizeX = 3;
    public static int mapSizeY = 6;

    public static Tile[,] tiles;

    private void Awake()
    {
        // Allocate our map tiles
        tiles = new Tile[mapSizeX, mapSizeY];
        InitializeMapData();
        GenerateMapVisual();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Populates the scene with tile Prefabs and saves a reference to the instantiated objects in the tiles array.
    void GenerateMapVisual() 
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TilePrefabs tileType = tileTypes[ (int) tiles[x, y].GetTileOwner() ];
                tiles[x,y].instance =  Instantiate(tileType.tileVisualPrefab, new Vector3(0 , 0, 0), Quaternion.identity);
                Transform t = tiles[x,y].instance.GetComponent<Transform>();
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
                tiles[x, y] = new Tile();
                tiles[x, y].SetX(x);
                tiles[x, y].SetY(y);

                if (y < (mapSizeY/2) )
                {
                    tiles[x, y].SetTileOwner(PlayerType.Player1);
                }
                else
                {                   
                    tiles[x, y].SetTileOwner(PlayerType.Player2);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
