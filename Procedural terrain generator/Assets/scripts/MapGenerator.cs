using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] private float persistence = 0.5f;
    [SerializeField] private float lacunarity = 2;
    [SerializeField] private int octaves =3;
    [SerializeField] private float noiseScale = 22.5f;
    [SerializeField] private int seed = 0;
    [SerializeField] private int mapWidth = 100;
    [SerializeField] private int mapHeight = 100;
    [SerializeField] private Renderer renderer;
    [SerializeField] public bool autoUpdate;
    [SerializeField] private TerrainType[] regions;
    [SerializeField] private Vector2 offset;
    public AnimationCurve heightCurve;
    public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
    public float heightMultiplier = 1;
     enum MapType{NoiseMap,ColorMap,Mesh};
     [SerializeField] private MapType mapType = MapType.NoiseMap;
    public void GenerateMap(){
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth,mapHeight,octaves,noiseScale,persistence,lacunarity,seed,offset);  
        Texture2D texture = new Texture2D(mapWidth,mapHeight);
        switch(mapType){
            case MapType.ColorMap:{
                 texture = TextureGenerator.GenerateColorTexture(noiseMap,new Color[mapWidth*mapHeight],new Texture2D(mapWidth,mapHeight), mapWidth,mapHeight,regions);
                  MapDisplay.DisplayMap(texture,renderer,mapWidth,mapHeight);   
                 break;
            }
            case MapType.NoiseMap:{
                 texture = TextureGenerator.GenerateNoiseTexture(noiseMap,new Color[mapWidth*mapHeight],new Texture2D(mapWidth,mapHeight), mapWidth,mapHeight);
                 MapDisplay.DisplayMap(texture,renderer,mapWidth,mapHeight);   
                break;
            }
            case MapType.Mesh:{
                texture = TextureGenerator.GenerateColorTexture(noiseMap,new Color[mapWidth*mapHeight],new Texture2D(mapWidth,mapHeight), mapWidth,mapHeight,regions);
                MapDisplay.DrawMesh(meshFilter,meshRenderer,MeshGenerator.GenerateTerrainMesh(noiseMap , heightMultiplier, heightCurve),texture, mapWidth,mapHeight);
                break;
            }
        }
        
        
    }
}
[System.Serializable]
public struct TerrainType{
    public string name;
    public Color color;
    public float height;
}