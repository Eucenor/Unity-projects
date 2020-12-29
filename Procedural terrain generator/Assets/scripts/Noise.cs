using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth ,int mapHeight,int octaves , float noiseScale , float persistence,float lacunarity , int seed,  Vector2 offset)
    {
        //local variables
        float[,] noiseMap = new float[mapWidth,mapHeight];
        float minNoise = float.MaxValue;
        float maxNoise = float.MinValue;
        float halfWidth = mapWidth/2f;
        float halfHeight = mapHeight/2f;
        
        System.Random prng = new System.Random (seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
		for (int i = 0; i < octaves; i++) {
			float offsetX = prng.Next (-100000, 100000) + offset.x;
			float offsetY = prng.Next (-100000, 100000) + offset.y;
			octaveOffsets [i] = new Vector2 (offsetX, offsetY);
		}

        //loop to get noiseMap
        for(int y = 0;y<mapHeight;y++){
            for(int x = 0;x<mapWidth;x++){
                
                float frequency = 1;
                float amplitude = 1;
                float noiseHeight = 0;
                if(noiseScale<=0){
                    noiseScale = 0.0001f;
                }
                for(int i = 0;i<octaves; i++){
                    
                    float sampleX = (x-halfWidth)/noiseScale * frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHeight)/noiseScale * frequency + octaveOffsets[i].y;
                    float perlinValue = Mathf.PerlinNoise(sampleX,sampleY);
                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistence;
                    frequency *= lacunarity;
                
                }
                if(noiseHeight<minNoise){
                    minNoise = noiseHeight;
                }
                if(noiseHeight>maxNoise){
                    maxNoise  = noiseHeight;
                }
                noiseMap[x,y] = noiseHeight;
            }
        }
        for(int y = 0;y<mapHeight;y++){
            for(int x = 0;x<mapWidth;x++){
             noiseMap[x,y] = Mathf.InverseLerp(minNoise,maxNoise,noiseMap[x,y]);
            }
        }
        return noiseMap;      
    }
}
