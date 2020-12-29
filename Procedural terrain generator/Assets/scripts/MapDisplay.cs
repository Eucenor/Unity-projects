using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class MapDisplay{
    public static void DisplayMap(Texture2D texture, Renderer renderer, int mapWidth, int mapHeight){
        renderer.sharedMaterial.mainTexture = texture;
        renderer.transform.localScale = new Vector3(mapWidth,renderer.transform.localScale.y,mapHeight);
    }    
    public static void DrawMesh(MeshFilter meshFilter,MeshRenderer meshRenderer,MeshData meshData,Texture2D texture, int mapWidth,int mapHeight){
		meshFilter.sharedMesh = meshData.CreateMesh ();
		meshRenderer.sharedMaterial.mainTexture = texture;
    meshRenderer.transform.localScale = new Vector3(mapWidth,1,mapHeight);    

    }
}
