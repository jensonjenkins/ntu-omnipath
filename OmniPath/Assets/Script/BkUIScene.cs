using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BkUIScene : MonoBehaviour
{
    void Start()
    {
        // Optional: Attach via code
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(SwitchScene);
        }
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(0);
    }
}
