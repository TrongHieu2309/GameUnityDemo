using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class VolumeText : MonoBehaviour 
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro;
    private TextMeshProUGUI txt;

    void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        float volumeValue = Mathf.RoundToInt(PlayerPrefs.GetFloat(volumeName) * 100);
        txt.text = textIntro + volumeValue.ToString();
    }
}