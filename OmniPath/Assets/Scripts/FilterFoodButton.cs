using UnityEngine;
using UnityEngine.UI;

public class FilterFoodButton : MonoBehaviour
{
    public LocationItemLoader loadScript;
    public Searchbar visibilityScript;

    public GameObject filterBar;

    private int[] filter = new int[5];
    
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null){
            btn.onClick.AddListener(FilterAndChangeVisibility);
        }
    }
    
    public void FilterAndChangeVisibility() {
        loadScript.FilterFood(filter);
        visibilityScript.SetLayerVisibility(true);
        filterBar.SetActive(true);
    }
}
