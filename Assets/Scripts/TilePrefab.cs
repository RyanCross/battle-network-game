using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyanCross.BattleNetworkGame
{
    // telling unity that this class is Serializable will allow it to show up in the inspector
    [System.Serializable]
    public class TilePrefabs
    {
        public string name;
        public GameObject tileVisualPrefab;
    }
}
