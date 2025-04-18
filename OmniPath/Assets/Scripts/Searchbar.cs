using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Searchbar : MonoBehaviour, ISelectHandler
{
    public Button backButton;
    public GameObject[] showOnSelect;
    public GameObject[] hideOnSelect;

    public GameObject filterBar;

    public Image searchBarImage;
    public Color selectedColor = Color.gray;
    public Color defaultColor = Color.white;


    void Start(){
        SetLayerVisibility(false);
        filterBar.SetActive(false);
        backButton.onClick.AddListener(hideComponents);
    }

    public void OnSelect(BaseEventData eventData){
        SetLayerVisibility(true);
    }

    void hideComponents(){
        SetLayerVisibility(false);
        filterBar.SetActive(false);
    }

    public void SetLayerVisibility(bool isSelected){

        foreach(GameObject obj in showOnSelect){
            obj.SetActive(isSelected);
        }
        foreach(GameObject obj in hideOnSelect){
            obj.SetActive(!isSelected);
        }
        if (searchBarImage != null){
            searchBarImage.color = isSelected ? selectedColor : defaultColor;
        }

    }
}
