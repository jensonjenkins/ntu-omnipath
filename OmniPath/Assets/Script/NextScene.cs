using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
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
        PlayerPrefs.SetString("Action", "findLift");
        PlayerPrefs.Save();
        SceneManager.LoadScene("SampleScene");
    }
}
