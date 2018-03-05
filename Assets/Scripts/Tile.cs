using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tile is a predefined class in the Unity library, probably should rename this
public class Tile {
    // the Cartesian coordinates of the Tile
    private int x;
    private int y;
    private PlayerType tileOwner;
    public GameObject instance;

    public void SetX (int xCoord)
    {
        this.x = xCoord;
    }

    public int GetX ()
    {
        return this.x;
    }

    public void SetY(int yCoord)
    {
        this.y = yCoord;
    }

    public int GetY()
    {
        return this.y;
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
