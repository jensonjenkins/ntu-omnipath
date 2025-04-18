using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloorSelector : MonoBehaviour
{
    public Button incrementButton;
    public Button decrementButton;
    public TextMeshProUGUI floorText;
    
    public GameObject[] backgrounds; 

    private string[] floors = {"B1", "L1", "L2", "L3", "L4", "L5"};
    private int currentIndex = 1;

    void Start() {
        UpdateUI();
        incrementButton.onClick.AddListener(IncrementFloor);
        decrementButton.onClick.AddListener(DecrementFloor);
    }

    void IncrementFloor(){
        if(currentIndex < floors.Length - 1){
            currentIndex++;
            UpdateUI();
        }
    }

    void DecrementFloor(){
        if(currentIndex > 0){
            currentIndex--;
            UpdateUI();
        }
    }

    void UpdateUI(){
        floorText.text = floors[currentIndex];
        incrementButton.interactable = currentIndex < floors.Length - 1;
        decrementButton.interactable = currentIndex > 0;
        for(int i=0;i<backgrounds.Length; i++){
            backgrounds[i].SetActive(i==currentIndex);
        }
    }

}
