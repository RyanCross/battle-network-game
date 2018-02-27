using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tells unity this class is indeed seriaziable so public variables of this class show up in inspector.
[System.Serializable]
//Tiletype doesn't need  implement the MonoBehavior Interface, its just a regular C# class
public class TilePrefabs {

    public string name;
    public GameObject tileVisualPrefab;
}
