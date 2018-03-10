using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // tiles should already be initialized.

    Transform playerTransform;
    public int startX;
    public int startY;
    public Tile currentTile;
    public PlayerType playerType;

    KeyCode left = KeyCode.Joystick1Button2;
    KeyCode right = KeyCode.Joystick1Button1;
    KeyCode up = KeyCode.Joystick1Button3;
    KeyCode down = KeyCode.Joystick1Button0;

    bool isAxisHorizontalInUse = false;
    bool isAxisVerticalInUse = false;
    float movementCooldown = .2f; // cooldown time in seconds
    float timeLastMoved;





    public KeyCode shoot;

    // Use this for initialization
    void Start () {
        if (TileMap.tiles != null)
        {
            playerTransform = this.GetComponent<Transform>();
            setPlayerStartPos();
        }
        else
        {
            throw new TileMapNotInitException("Error, tile map was not initialized properly");
        }

        string[] temp = Input.GetJoystickNames();
        Debug.Log(temp);
        timeLastMoved = Time.time; // this is going to cause an initial delay.

	}

    // initializes the current tile of the player and sets the position of the player to the center of the starting tile
    private void setPlayerStartPos()
    {
        if (this.GetComponent<BoxCollider>() != null)
        {
            // init starting location
            this.currentTile = TileMap.tiles[this.startX, this.startY];

            Transform tileTransform = currentTile.instance.GetComponent<Transform>();
            Bounds playerBounds = this.GetComponent<BoxCollider>().bounds;
            Bounds tileBounds = currentTile.instance.GetComponent<BoxCollider>().bounds;
            // put player at center of the tile
            Vector3 playerPos = new Vector3(tileTransform.position.x, calculatePlayerYPos(playerBounds, tileBounds, playerTransform.localScale.y), tileTransform.position.z);
            playerTransform.SetPositionAndRotation(playerPos, Quaternion.identity);

        }   
    }

    // updates player position and the current tile variable
    private void updatePlayerPosition(Tile[,] tiles, int tileCoordX, int tileCoordY)
    {
        if (this.GetComponent<BoxCollider>() != null)
        {
            currentTile = tiles[tileCoordX, tileCoordY];

            Transform tileTransform = currentTile.instance.GetComponent<Transform>();
            Bounds playerBounds = this.GetComponent<BoxCollider>().bounds;
            Bounds tileBounds = currentTile.instance.GetComponent<BoxCollider>().bounds;
            // put player at center of the tile
            Vector3 playerPos = new Vector3(tileTransform.position.x, playerTransform.position.y, tileTransform.position.z);
            playerTransform.SetPositionAndRotation(playerPos, Quaternion.identity);
        }
    }

    // Checks for movement and updates player positions to the correct tile if new attempted tile position is valid.
    private void Move()
    {
        // left 
        if (Input.GetAxisRaw("HorizontalXbox360") == -1)
        {
            if (currentTile.GetY() - 1 >= 0)
            {
                if (isTileOwner(TileMap.tiles[currentTile.GetX(), currentTile.GetY() - 1]) && isAxisHorizontalInUse == false && (Time.time >= timeLastMoved + movementCooldown))
                {
                    isAxisHorizontalInUse = true;
                    timeLastMoved = Time.time;
                    updatePlayerPosition(TileMap.tiles, currentTile.GetX(), currentTile.GetY() - 1);
                }
                else
                {
                    isAxisHorizontalInUse = false;
                }
            }
        }
        // right
        else if (Input.GetAxisRaw("HorizontalXbox360") == 1)
        {
            if (currentTile.GetY() + 1 < TileMap.mapSizeY)
            {
                if (isTileOwner(TileMap.tiles[currentTile.GetX(), currentTile.GetY() + 1]) && isAxisHorizontalInUse == false && (Time.time >= timeLastMoved + movementCooldown))
                {
                    isAxisHorizontalInUse = true;
                    timeLastMoved = Time.time;
                    updatePlayerPosition(TileMap.tiles, currentTile.GetX(), currentTile.GetY() + 1);
                }
                else
                {
                    isAxisHorizontalInUse = false;
                }
            }
        }
        // up
        else if (Input.GetAxisRaw("VerticalXbox360") == 1)
        {
            if (currentTile.GetX() - 1 >= 0)
            {
                if (isTileOwner(TileMap.tiles[currentTile.GetX() - 1, currentTile.GetY()]) && isAxisVerticalInUse == false)
                {
                    isAxisVerticalInUse = true;
                    updatePlayerPosition(TileMap.tiles, currentTile.GetX() - 1, currentTile.GetY());
                }
                else
                {
                    isAxisVerticalInUse = false;
                }
            }
        }
        // down
        else if (Input.GetAxisRaw("VerticalXbox360") == -1)
        {
            if (currentTile.GetX() + 1 < TileMap.mapSizeX)
            {
                if (isTileOwner(TileMap.tiles[currentTile.GetX() + 1, currentTile.GetY()]) && isAxisVerticalInUse == false)
                {
                    isAxisVerticalInUse = true;
                    updatePlayerPosition(TileMap.tiles, currentTile.GetX() + 1, currentTile.GetY());
                }
                else
                {
                    isAxisVerticalInUse = false;
                }
            }   
        }
    }


    // this is needed so the player object switches tiles it does not clip through the middle of the tile. 
    // puts the player objects bottom face at the top face of the tile.
    public float calculatePlayerYPos (Bounds playerBounds, Bounds tileBounds, float playerScaleY)
    {
        float yPos = playerScaleY + playerBounds.min.y + tileBounds.max.y;
        return yPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTile == null)
        {
            Debug.Log("Something went wrong, currentTile of player is null");
        }
        else
        {
            this.Move();
        }
    }

    bool isTileOwner(Tile tileToCheck)
    {
        if (this.playerType == tileToCheck.GetTileOwner())
        {
            return true;
        }
        return false;
    }


}
