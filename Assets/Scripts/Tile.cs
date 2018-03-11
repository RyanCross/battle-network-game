using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyanCross.BattleNetworkGame
{
    public class Tile
    {
        internal int xCoord { get; set; }
        internal int yCoord { get; set; }
        internal PlayerType TileOwner { get; set;}
        public GameObject instance;
    }
}

