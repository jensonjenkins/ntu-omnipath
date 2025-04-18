using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SplashScreenLoad : MonoBehaviour
{
    public Slider loadingSlider; 
    public MenuTimer timer; 
    public int menuloadingtime; 
    void Start()
    {
        timer.StartTimer(); 
        StartCoroutine(UpdateLoadingProgress()); 
    }

    IEnumerator UpdateLoadingProgress()
    {
        while (timer.GetCurrentTime() < menuloadingtime)
        {
            float progress = (timer.GetCurrentTime() / menuloadingtime);
            loadingSlider.value = progress; 
            yield return null; 
        }
        SceneManager.LoadScene("Main Menu");
    }
}