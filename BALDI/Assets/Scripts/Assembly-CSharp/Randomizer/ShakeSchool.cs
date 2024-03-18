using System.Collections.Generic;
using TMPro;
using UnityEngine;

// this script came from my test mod
// use this if you credit me
public class ShakeSchool : MonoBehaviour
{
    List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    List<GameObject> school = new List<GameObject>();
    List<Vector3> schoolOG = new List<Vector3>();
    List<Vector3> schoolOGRot = new List<Vector3>();
    List<MeshRenderer> schoolMesh = new List<MeshRenderer>();
    void Start()
    {
        MeshRenderer[] tempSchool = FindObjectsByType<MeshRenderer>(0);
        foreach (MeshRenderer mesh in tempSchool)
        {
            if (!mesh.name.ToLower().Contains("player") & mesh.GetComponent<BoxCollider>() == null & mesh.GetComponent<TextMeshPro>() == null)
            {
                Texture ogTex = null;
                if (!mesh.material.shader.name.Contains("Color"))
                    ogTex = mesh.material.mainTexture;
                mesh.material = new Material(Shader.Find("Unlit/GlitchShader"));
                mesh.material.SetTexture("_MainTex", ogTex);
                Vector2 tiling = new Vector2(-1f, -1f);
                school.Add(mesh.gameObject);
                schoolOG.Add(mesh.transform.localScale);
                schoolOGRot.Add(mesh.transform.eulerAngles);
                schoolMesh.Add(mesh);
            }
        }
        SpriteRenderer[] tempSprites = FindObjectsByType<SpriteRenderer>(0);
        foreach (SpriteRenderer spr in tempSprites)
        {
            sprites.Add(spr);
        }
    }

    public float shakeAMP = 0f;
    public float glitchAMP = 0f;
    public float glitchOffset = 0f;

    void Update()
    {
        if (shakeAMP != 0f || glitchAMP != 0f || glitchOffset != 0f)
        {
            for (int i = 0; i < school.Count; i++)
            {
                if (shakeAMP != 0f)
                {
                    Vector3 newScale = new Vector3(Random.Range(-shakeAMP, shakeAMP) + schoolOG[i].x, Random.Range(-shakeAMP, shakeAMP) + schoolOG[i].y, Random.Range(-shakeAMP, shakeAMP) + schoolOG[i].z);
                    school[i].transform.localScale = newScale;
                    Vector3 Rot = new Vector3(Random.Range(-shakeAMP * 100f, shakeAMP * 100f) + schoolOGRot[i].x, Random.Range(-shakeAMP * 100f, shakeAMP * 100f) + schoolOGRot[i].y, Random.Range(-shakeAMP * 100f, shakeAMP * 100f) + schoolOGRot[i].z);
                    school[i].transform.rotation = Quaternion.Euler(Rot);
                }
                if (glitchAMP != 0f)
                {
                    schoolMesh[i].material.SetFloat("_Amp", glitchAMP);
                }
                if (glitchOffset != 0f)
                {
                    schoolMesh[i].material.SetFloat("_GlitchOffset", glitchOffset);
                }
            }
        }
    }
}