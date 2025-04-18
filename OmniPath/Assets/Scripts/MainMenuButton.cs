using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour {
     
    public GameObject singleLocationMap, informationPanel, mainMap, searchIcon, categoryBar, backButton;
    public float latitude, longitude;
    public string imgFileName, nameText, addressText;
    private string imagePath; 

    void Start(){
        Button btn = GetComponent<Button>();
        if (btn != null){
            btn.onClick.AddListener(HandleClick);
        }
        imagePath = Path.Combine(Application.streamingAssetsPath, imgFileName);
    }

    void HandleClick() {
        SetLayerVisibility();
        SendData();
    }

    void SendData(){
        SingleLocationMap mapViewerScript = singleLocationMap.GetComponent<SingleLocationMap>();
        mapViewerScript.ReceiveAndUpdateMap(latitude, longitude);
        InformationPanel infoPanelScript = informationPanel.GetComponent<InformationPanel>();
        infoPanelScript.ReceiveAndUpdate(nameText, addressText, imagePath);
    }

    void SetLayerVisibility() {
        singleLocationMap.SetActive(true);
        backButton.SetActive(true);
        mainMap.SetActive(false);
        searchIcon.SetActive(false);
        categoryBar.SetActive(false);
    }

}
