using UnityEngine;
using UnityEngine.UI;

public class FilterLabsButton : MonoBehaviour
{
    public LocationItemLoader loadScript;
    public Searchbar visibilityScript;
    
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null){
            btn.onClick.AddListener(FilterAndChangeVisibility);
        }
    }

    public void FilterAndChangeVisibility() {
        loadScript.FilterLabs();
        visibilityScript.SetLayerVisibility(true);
    }
}
