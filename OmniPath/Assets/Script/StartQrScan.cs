using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartQrScan : MonoBehaviour
{
    public GameObject qrRecenter;

    // Update is called once per frame
    void Update()
    {
        if (!qrRecenter.activeSelf)
            {
              qrRecenter.SetActive(true);
              Debug.Log("qrRecenter was inactive, now it's enabled!");
            }
    }
}
