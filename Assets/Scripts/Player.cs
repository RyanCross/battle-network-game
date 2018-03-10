using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // tiles should already be initialized.
    Transform playerTransform;
    public int startX;
    public int startY;
    public Tile currentTile; //refactor to private
    public PlayerType playerType;
    ControlScheme playerControls;

    float movementCooldown = .25f; // cooldown time between accepted movement input
    float timeLastMoved;


    // Use this for initialization
    void Start () {
        if (TileMap.tiles != null)
        {
            // Initalize default controls:
            playerControls = new ControlScheme();
            playerControls.determineDefaults(playerType);
            playerTransform = this.GetComponent<Transform>();
            setPlayerStartPos();
            
        }
        else
        {
            throw new TileMapNotInitException("Error, tile map was not initialized properly");
        }

        string[] temp = Input.GetJoystickNames();
        Debug.Log(temp);
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
        if (Input.GetAxisRaw(playerControls.horizontalAxisName) == -1)
        {
            if (currentTile.GetY() - 1 >= 0)
            {
                if (isTileOwner(TileMap.tiles[currentTile.GetX(), currentTile.GetY() - 1]) && (Time.time >= timeLastMoved + movementCooldown))
                {
                    timeLastMoved = Time.time;
                    updatePlayerPosition(TileMap.tiles, currentTile.GetX(), currentTile.GetY() - 1);
                }
            }
        }
        // right
        else if (Input.GetAxisRaw(playerControls.horizontalAxisName) == 1)
        {
            if (currentTile.GetY() + 1 < TileMap.mapSizeY)
            {
                if (isTileOwner(TileMap.tiles[currentTile.GetX(), currentTile.GetY() + 1]) && (Time.time >= timeLastMoved + movementCooldown))
                {
                    timeLastMoved = Time.time;
                    updatePlayerPosition(TileMap.tiles, currentTile.GetX(), currentTile.GetY() + 1);
                }
            }
        }
        // up
        else if (Input.GetAxisRaw(playerControls.verticalAxisName) == 1)
        {
            if (currentTile.GetX() - 1 >= 0)
            {
                if (isTileOwner(TileMap.tiles[currentTile.GetX() - 1, currentTile.GetY()]) && (Time.time >= timeLastMoved + movementCooldown))
                {
                    timeLastMoved = Time.time;
                    updatePlayerPosition(TileMap.tiles, currentTile.GetX() - 1, currentTile.GetY());
                }
            }
        }
        // down
        else if (Input.GetAxisRaw(playerControls.verticalAxisName) == -1)
        {
            if (currentTile.GetX() + 1 < TileMap.mapSizeX)
            {
                if (isTileOwner(TileMap.tiles[currentTile.GetX() + 1, currentTile.GetY()]) && (Time.time >= timeLastMoved + movementCooldown))
                {
                    timeLastMoved = Time.time;
                    updatePlayerPosition(TileMap.tiles, currentTile.GetX() + 1, currentTile.GetY());
                }
            }   
        }
    }


    // this is needed so the player object switches tiles it does not clip through the middle of the tile. 
    // places the player directly atop the tile.
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
