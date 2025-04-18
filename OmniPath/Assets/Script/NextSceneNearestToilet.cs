using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneNearestToilt : MonoBehaviour
{
    // Start is called before the first frame update
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
        PlayerPrefs.SetString("Action", "findToilet");
        PlayerPrefs.Save();
        SceneManager.LoadScene("SampleScene");
    }
}
