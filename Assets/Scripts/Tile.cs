using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tile is a predefined class in the Unity library, so TileObject will be used instead.
public class Tile {

    private int xCoord;
    private int yCoord;
    private PlayerType tileOwner;
    public GameObject instance;

    public void SetXCoord (int xCoord)
    {
        this.xCoord = xCoord;
    }

    public int GetXCoord ()
    {
        return this.xCoord;
    }

    public void SetYCoord(int yCoord)
    {
        this.yCoord = yCoord;
    }

    public int GetYCoord()
    {
        return this.yCoord;
    }

    public void SetTileOwner(PlayerType tileOwner)
    {
        this.tileOwner = tileOwner;
    }

    public PlayerType GetTileOwner()
    {
        return this.tileOwner;
    }
}
