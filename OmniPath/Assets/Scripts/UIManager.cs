using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject singleLocationMap;
    public GameObject informationPanel;
    public GameObject locationButtonList;
    public GameObject foodFilterBar;

    void Awake()
    {
        Instance = this;
    }
}
