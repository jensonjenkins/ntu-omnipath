using UnityEngine;
using TMPro;

public class LocationItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI addressText;

    public void SetData(string name, string address){
        nameText.text = name;
        addressText.text = address;
    }

}
