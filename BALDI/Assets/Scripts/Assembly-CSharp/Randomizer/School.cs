using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class School : MonoBehaviour
{
    RndTexture rTex;
    List<MeshRenderer> school = new List<MeshRenderer>();
    List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    void Start()
    {
        rTex = FindAnyObjectByType<RndTexture>();
        school = FindObjectsByType<MeshRenderer>(0).ToList();
        sprites = FindObjectsByType<SpriteRenderer>(0).ToList();
        
        // randomize all textures
        foreach (MeshRenderer mesh in school)
        {
            if (!mesh.transform.name.Contains("FireInTheHole"))
            {
                mesh.material.shader = Shader.Find("Legacy Shaders/Transparent/Cutout/Diffuse");
                mesh.material.mainTexture = rTex.GetRandomTexture();
            }
        }
        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite.name != "filename2")
            {
                // converting Texture to Sprite so we shouldn't do the array stuff again
                var tex = rTex.GetRandomTexture();
                sprite.sprite = Sprite.Create(tex as Texture2D, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }
        }
        
    }
}
