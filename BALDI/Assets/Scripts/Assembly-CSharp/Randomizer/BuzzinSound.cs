using UnityEngine;
using UnityEngine.UI;

public class BuzzinSound : MonoBehaviour
{
    Toggle self;
    private void Start()
    {
        self = GetComponent<Toggle>();
        self.isOn = PlayerPrefs.GetInt("See that heart? No shit!", 0) == 1;
    }

    public void ToggleShit(bool check)
    {
        PlayerPrefs.SetInt("See that heart? No shit!", self.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}