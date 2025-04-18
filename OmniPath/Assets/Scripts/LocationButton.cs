using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class LocationButton : MonoBehaviour
{
    public Button locationButton;
    public GameObject singleLocationMap, informationPanel, foodFilterBar, locationButtonList;
    public float latitude, longitude;
    public string nameText, addressText;
    public string imagePath;


    public void SetData(string name, string address, float lat, float lon, string locationImagePath) {
        latitude = lat;
        longitude = lon;
        nameText = name;
        addressText = address;
        imagePath = locationImagePath;
    }

    public void Initialize()
    {   
        singleLocationMap = UIManager.Instance.singleLocationMap;
        informationPanel = UIManager.Instance.informationPanel;
        foodFilterBar = UIManager.Instance.foodFilterBar;
        locationButtonList = UIManager.Instance.locationButtonList;

        if (singleLocationMap == null || informationPanel == null 
                || locationButtonList == null || foodFilterBar == null)
        {
            Debug.LogError("LocationButton: Missing required objects!");
            return;
        } 
    }    
    void Start(){
        locationButton.onClick.AddListener(HandleClick);
    }
   
    void HandleClick(){
        SetLayerVisibility();
        SendData(); 
    }

    void SendData(){
        if (singleLocationMap != null){
            SingleLocationMap mapViewerScript = singleLocationMap.GetComponent<SingleLocationMap>();
            if (mapViewerScript != null){
                mapViewerScript.ReceiveAndUpdateMap(latitude, longitude);
            }
        }
        if (informationPanel != null){
            InformationPanel infoPanelScript = informationPanel.GetComponent<InformationPanel>();
            if(infoPanelScript != null){
                infoPanelScript.ReceiveAndUpdate(nameText, addressText, imagePath);
            }
        }
    }

    void SetLayerVisibility() {
        singleLocationMap.SetActive(true);
        locationButtonList.SetActive(false);
        if(foodFilterBar != null){
            foodFilterBar.SetActive(false);
        }
    }
}
