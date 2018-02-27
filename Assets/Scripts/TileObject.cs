using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject {

    private int xCoord;
    private int yCoord;
    private TileOwner tileOwner;
    public GameObject tilePrefab;

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

    public void SetTileOwner(TileOwner tileOwner)
    {
        this.tileOwner = tileOwner;
    }

    public TileOwner GetTileOwner()
    {
        return this.tileOwner;
    }
}
