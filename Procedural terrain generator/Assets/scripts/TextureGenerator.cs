using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator{

    public static Texture2D GenerateNoiseTexture(float[,] noiseMap, Color[] color,Texture2D texture, int width = 100, int height = 100){
        

        for(int y = 0;y<height;y++){
            for(int x = 0;x<width;x++){
                color[y*width+x] = Color.Lerp(Color.black,Color.white,noiseMap[x,y]);
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(color);
        texture.Apply();
        return texture;
    }
        public static Texture2D GenerateColorTexture(float[,] noiseMap, Color[] color,Texture2D texture, int width, int height,TerrainType[] regions){
            for(int y = 0;y<height;y++){
                for(int x = 0;x<width;x++){
                    float currentHeight = noiseMap[x,y];
                    for(int i = 0;i<regions.Length;i++){
                        if(currentHeight<regions[i].height){
                            color[y*width + x] = regions[i].color;
                            break;
                        }
                    }
                }
            }
            texture.filterMode = FilterMode.Point;
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.SetPixels(color);
            texture.Apply();
            return texture;
        }
}
