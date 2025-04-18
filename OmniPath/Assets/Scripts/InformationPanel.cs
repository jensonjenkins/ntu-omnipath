using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class InformationPanel : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI addressText;
    public RawImage locationImage;

    public void ReceiveAndUpdate(string name, string address, string imagePath){
        nameText.text = name;
        PlayerPrefs.SetString ("Destination", name);
        PlayerPrefs.Save();
        addressText.text = address;
        LoadAndApplyImage(imagePath, locationImage);
    }

    void LoadAndApplyImage(string path, RawImage rawImage)
    {
        if (!File.Exists(path)) return;

        byte[] imageData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData); // Load image into texture
        rawImage.texture = texture;
    }
}
