using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterManager : MonoBehaviour
{
    public LocationItemLoader loadScript;

    public ToggleButton vegan;
    public ToggleButton halal;
    public ToggleButton priceLow;
    public ToggleButton priceMid;
    public ToggleButton priceHigh;

    private int[] buttonStates = new int[5];

    void Start()
    {
        // dumb reverse logic to counteract a few dumb choices
        vegan.OnToggle += (isSelected) =>       UpdateButtonState(0, isSelected);
        halal.OnToggle += (isSelected) =>       UpdateButtonState(1, isSelected);
        priceLow.OnToggle += (isSelected) =>    UpdateButtonState(2, isSelected);
        priceMid.OnToggle += (isSelected) =>    UpdateButtonState(3, isSelected);
        priceHigh.OnToggle += (isSelected) =>   UpdateButtonState(4, isSelected);
    }
   
    void UpdateButtonState(int index, bool isSelected){
        buttonStates[index] = isSelected ? 1 : 0;

        // Debug.Log($"Button {index} is {(isSelected?"Unselected" : "Selected")}");

        loadScript.FilterFood(buttonStates);
    }
}
