using UnityEngine;
using UnityEngine.UI;

public class TabToggle : MonoBehaviour
{
    public Button toggleTabButton;

    public RectTransform tab;
    public Vector2 hiddenPosition;
    public Vector2 shownPosition;
    public float animationDuration = 0.3f;

    public bool isShown = false;
    private Coroutine moveCoroutine;

    void Start() {
        toggleTabButton.onClick.AddListener(ToggleTab);
    }

    public void ToggleTab()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);
    
        if(isShown){
            isShown = false;
        }else{
            isShown = true;
        }

        moveCoroutine = StartCoroutine(MoveTab(isShown ? shownPosition : hiddenPosition));
    }

    private System.Collections.IEnumerator MoveTab(Vector2 targetPos)
    {
        Vector2 startPos = tab.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            tab.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        tab.anchoredPosition = targetPos;
    }
}
