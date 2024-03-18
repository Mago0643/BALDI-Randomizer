using UnityEngine;
using UnityEngine.UI;

public class RandomBillboardToggle : MonoBehaviour
{
    Toggle self;
    private void Start()
    {
        self = GetComponent<Toggle>();
        self.isOn = PlayerPrefs.GetInt("RandomBilboard", 0) == 0;
    }

    public void ValueChanged(bool check)
    {
        PlayerPrefs.SetInt("RandomBilboard", self.isOn ? 0 : 1);
        PlayerPrefs.Save();
    }
}