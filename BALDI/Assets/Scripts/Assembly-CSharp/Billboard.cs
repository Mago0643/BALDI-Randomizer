using UnityEngine;

// Token: 0x020000B3 RID: 179
public class Billboard : MonoBehaviour
{
	// Token: 0x06000927 RID: 2343 RVA: 0x00020BD5 File Offset: 0x0001EFD5
	private void Start()
	{
		this.m_Camera = Camera.main;
		noRandom = PlayerPrefs.GetInt("RandomBilboard", 0) == 1;
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00020BE2 File Offset: 0x0001EFE2
	private void LateUpdate()
	{
		Vector3 rot = base.transform.position + this.m_Camera.transform.rotation * Vector3.forward;
		// its like raldis crackhouse billboard thing
		if (!noRandom & transform.name != "filename2")
		{
			rot.x += Random.Range(-360, 360f);
			rot.y += Random.Range(-360, 360f);
			rot.z += Random.Range(-360, 360f);
		}
        base.transform.LookAt(rot); // Look towards the player
	}

	// Token: 0x040005AC RID: 1452
	private Camera m_Camera;
	bool noRandom = false;
}
