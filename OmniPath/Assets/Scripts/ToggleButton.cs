using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{   
    private Button button;
    private bool isSelected = false; 
    private Color lightGray = new Color(233f / 255f, 233f / 255f, 233f / 255f, 1f);
    public delegate void OnToggleAction(bool isSelected);
    public event OnToggleAction OnToggle;

    void Start()
    {
        button = GetComponent<Button>();
        button.image.color = Color.white; 
        button.onClick.AddListener(ToggleSelection);
    }

    void ToggleSelection()
    {
        isSelected = !isSelected;
        if(isSelected){
            button.image.color = lightGray;
        }else{
            button.image.color = Color.white;
        }
        OnToggle?.Invoke(isSelected);
    }
}

