﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // tiles should already be initialized.

    Transform playerTransform;
    public int startX;
    public int startY;
    public Tile currentTile;
    public PlayerType playerType;

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

    // Checks for movement and updates player position to the correct tile if new attempted tile position is valid.
    private void checkForMovement()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentTile.GetYCoord() + 1 <= TileMap.mapSizeY)
            {
                if (isTileOwner(TileMap.tiles[currentTile.GetXCoord(), currentTile.GetYCoord() + 1]))
                updatePlayerPosition(TileMap.tiles, currentTile.GetXCoord(), currentTile.GetYCoord() + 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentTile.GetYCoord() - 1 >= 0)
            {
                updatePlayerPosition(TileMap.tiles, currentTile.GetXCoord(), currentTile.GetYCoord() - 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentTile.GetXCoord() + 1 <= TileMap.mapSizeX)
            {
                updatePlayerPosition(TileMap.tiles, currentTile.GetXCoord() + 1, currentTile.GetYCoord());
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentTile.GetXCoord() - 1 >=  0)
            {
                updatePlayerPosition(TileMap.tiles, currentTile.GetXCoord() - 1, currentTile.GetYCoord());
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
            this.checkForMovement();
        }
    }

    bool isTileOwner(Tile tileToCheck)
    {
        if (this.playerType == tileToCheck)
        {
            return true;
        }
        return false;
    }


}
