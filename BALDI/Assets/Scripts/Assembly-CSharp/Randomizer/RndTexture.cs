using System.Collections.Generic;
using UnityEngine;

public class RndTexture : MonoBehaviour
{
    // i didnt find that to get all textures in the game
    public List<Texture> textures;

    public Texture GetRandomTexture()
    {
        return textures[Random.Range(0, textures.Count-1)];
    }
}
