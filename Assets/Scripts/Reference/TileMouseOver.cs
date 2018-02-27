using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMouseOver : MonoBehaviour {
    public MeshRenderer meshRenderer;
    public Material tileMaterial;
    public MeshCollider meshCollider;
    Color normalColor;
    Color highlightColor;
    

	// Use this for initialization
	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        tileMaterial = GetComponent<MeshRenderer>().material;
        meshCollider = GetComponent<MeshCollider>();
        normalColor = Color.cyan;
        highlightColor = Color.magenta;
        tileMaterial.color = normalColor;

	}
	
	// Update is called once per frame
		
	void Update () {

        // Out of our camera, we will send a ray forward.
        Ray mousePointerRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Returns true or false if that ray hits something. 
        //Physics.Raycast(mousePointerRay, )

        //output parameters do not need to be initialized as they will be populated by the function requiring them
        RaycastHit hitInfo;

        // out keyword causes arguments to be passed by ref, much like the ref keyword but ref requires that the variable be initialized before it is passed
        // does a raycast specifically against the collider of this object.
        if (meshCollider.Raycast(mousePointerRay, out hitInfo, Mathf.Infinity))
        {
            tileMaterial.color = highlightColor;
        }
        else
        {
            tileMaterial.color = normalColor;
        }
	}

    //void OnMouseOver()
    //{
    //    //the flaw with on MouseOver if theres another "something" over the tile (ie. a cube), we won't be able to hover it
    //    tileMaterial.color = Color.red;
    //}

    //private void OnMouseExit()
    //{
    //    tileMaterial.color = Color.blue;
    //}
}
