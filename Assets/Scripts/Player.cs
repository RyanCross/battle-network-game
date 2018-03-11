using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyanCross.BattleNetworkGame
{
    public class Player : MonoBehaviour
    {
        // tiles should already be initialized.     
        public int startX;
        public int startY;
        public PlayerType playerType;
        public float movementCooldown = .25f; // cooldown time between accepted movement input

        internal Transform PlayerTransform { get; private set; }
        internal Tile CurrentTile { get; private set; } 
        ControlScheme playerControls;
        float timeLastMoved;

        // Use this for initialization
        void Start()
        {
            if (TileMap.tiles != null)
            {
                // Initalize default controls:
                playerControls = new ControlScheme();
                playerControls.determineDefaults(playerType);
                PlayerTransform = this.GetComponent<Transform>();
                SetPlayerStartPos();

            }
            else
            {
                throw new TileMapNotInitException("Error, tile map was not initialized properly");
            }

            string[] temp = Input.GetJoystickNames();
            Debug.Log(temp);
        }

        // initializes the current tile of the player and sets the position of the player to the center of the starting tile
        private void SetPlayerStartPos()
        {
            if (this.GetComponent<BoxCollider>() != null)
            {
                // init starting location
                this.CurrentTile = TileMap.tiles[this.startX, this.startY];

                Transform tileTransform = CurrentTile.instance.GetComponent<Transform>();
                Bounds playerBounds = this.GetComponent<BoxCollider>().bounds;
                Bounds tileBounds = CurrentTile.instance.GetComponent<BoxCollider>().bounds;
                // put player at center of the tile
                Vector3 playerPos = new Vector3(tileTransform.position.x, CalculatePlayerYPos(playerBounds, tileBounds, PlayerTransform.localScale.y), tileTransform.position.z);
                PlayerTransform.SetPositionAndRotation(playerPos, Quaternion.identity);

            }
        }

        // updates player position and the current tile variable
        private void UpdatePlayerPosition(Tile[,] tiles, int tileCoordX, int tileCoordY)
        {
            if (this.GetComponent<BoxCollider>() != null)
            {
                CurrentTile = tiles[tileCoordX, tileCoordY];

                Transform tileTransform = CurrentTile.instance.GetComponent<Transform>();
                Bounds playerBounds = this.GetComponent<BoxCollider>().bounds;
                Bounds tileBounds = CurrentTile.instance.GetComponent<BoxCollider>().bounds;
                // put player at center of the tile
                Vector3 playerPos = new Vector3(tileTransform.position.x, PlayerTransform.position.y, tileTransform.position.z);
                PlayerTransform.SetPositionAndRotation(playerPos, Quaternion.identity);
            }
        }

        // Checks for movement and updates player positions to the correct tile if new attempted tile position is valid.
        private void Move()
        {
            // left 
            if (Input.GetAxisRaw(playerControls.horizontalAxisName) == -1)
            { 
                if (CurrentTile.yCoord - 1 >= 0)
                {
                    if (IsTileOwner(TileMap.tiles[CurrentTile.xCoord, CurrentTile.yCoord - 1]) && (Time.time >= timeLastMoved + movementCooldown))
                    {
                        timeLastMoved = Time.time;
                        UpdatePlayerPosition(TileMap.tiles, CurrentTile.xCoord, CurrentTile.yCoord - 1);
                    }
                }
            }
            // right
            else if (Input.GetAxisRaw(playerControls.horizontalAxisName) == 1)
            {
                if (CurrentTile.yCoord + 1 < TileMap.mapSizeY)
                {
                    if (IsTileOwner(TileMap.tiles[CurrentTile.xCoord, CurrentTile.yCoord + 1]) && (Time.time >= timeLastMoved + movementCooldown))
                    {
                        timeLastMoved = Time.time;
                        UpdatePlayerPosition(TileMap.tiles, CurrentTile.xCoord, CurrentTile.yCoord + 1);
                    }
                }
            }
            // up
            else if (Input.GetAxisRaw(playerControls.verticalAxisName) == 1)
            {
                if (CurrentTile.xCoord - 1 >= 0)
                {
                    if (IsTileOwner(TileMap.tiles[CurrentTile.xCoord - 1, CurrentTile.yCoord]) && (Time.time >= timeLastMoved + movementCooldown))
                    {
                        timeLastMoved = Time.time;
                        UpdatePlayerPosition(TileMap.tiles, CurrentTile.xCoord - 1, CurrentTile.yCoord);
                    }
                }
            }
            // down
            else if (Input.GetAxisRaw(playerControls.verticalAxisName) == -1)
            {
                if (CurrentTile.xCoord + 1 < TileMap.mapSizeX)
                {
                    if (IsTileOwner(TileMap.tiles[CurrentTile.xCoord + 1, CurrentTile.yCoord]) && (Time.time >= timeLastMoved + movementCooldown))
                    {
                        timeLastMoved = Time.time;
                        UpdatePlayerPosition(TileMap.tiles, CurrentTile.xCoord + 1, CurrentTile.yCoord);
                    }
                }
            }
        }


        // this is needed so the player object switches tiles it does not clip through the middle of the tile. 
        // places the player directly atop the tile.
        public float CalculatePlayerYPos(Bounds playerBounds, Bounds tileBounds, float playerScaleY)
        {
            float yPos = playerScaleY + playerBounds.min.y + tileBounds.max.y;
            return yPos;
        }

        // Update is called once per frame
        void Update()
        {
            if (CurrentTile == null)
            {
                Debug.Log("Something went wrong, currentTile of player is null");
            }
            else
            {
                this.Move();
            }
        }

        bool IsTileOwner(Tile tileToCheck)
        {
            if (this.playerType == tileToCheck.TileOwner)
            {
                return true;
            }
            return false;
        }
    }
}
