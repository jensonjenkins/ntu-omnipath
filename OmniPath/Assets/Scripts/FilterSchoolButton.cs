using UnityEngine;
using UnityEngine.UI;

public class FilterSchoolButton : MonoBehaviour
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
        loadScript.FilterSchool();
        visibilityScript.SetLayerVisibility(true);
    }
}
