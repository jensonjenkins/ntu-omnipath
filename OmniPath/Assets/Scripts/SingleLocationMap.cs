using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;

public class SingleLocationMap : MonoBehaviour
{
    public RawImage mapImage, topMapImage;  
    public Image pointImage;

    private int zoomLevel = 18;  
    private int tileX, tileY; 
    private const string OSM_URL = "https://tile.openstreetmap.org/{0}/{1}/{2}.png";

    public void ReceiveAndUpdateMap(float lat, float lon)
    {
        Debug.Log("SingleLocationMap Received: " + lat + ", " + lon);

        (int nX, int nY) = LatLonToTile(lat, lon, zoomLevel);
        StartCoroutine(LoadMapTile(nX, nY, lat, lon)); 
    }


    IEnumerator LoadMapTile(int x, int y, float lat, float lon)
    {
        string urlBottom= string.Format(OSM_URL, zoomLevel, x, y);
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(urlBottom))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                mapImage.texture = DownloadHandlerTexture.GetContent(request);
                RectTransform rt = pointImage.GetComponent<RectTransform>();
                (int sx, int sy) = LatLonToPx(lat, lon, zoomLevel);
                Debug.Log("sx: "+sx + ", sy: " + sy);
                rt.anchoredPosition = new Vector2(-370 + sx, 1400 - sy);
            }
            else
            {
                Debug.LogError("Error loading bottom map: " + request.error);
            }
        }
        string urlTop = string.Format(OSM_URL, zoomLevel, x, y-1);
        using (UnityWebRequest requestTop = UnityWebRequestTexture.GetTexture(urlTop))
        {
            yield return requestTop.SendWebRequest();

            if (requestTop.result == UnityWebRequest.Result.Success)
            {
                topMapImage.texture = DownloadHandlerTexture.GetContent(requestTop);
            }
            else
            {
                Debug.LogError("Error loading top map: " + requestTop.error);
            }
        }
    }
    
    (int, int) LatLonToPx(float lat, float lon, int zoom){
        double n = Math.Pow(2, zoom);
        double xtile = (lon + 180.0) / 360.0 * n;
        double ytile = (1.0 - Math.Log(Math.Tan(lat * Math.PI / 180.0) + 1.0 / Math.Cos(lat * Math.PI / 180.0)) / Math.PI) / 2.0 * n;

        double xPixel = (xtile - Math.Floor(xtile)) * 1179.0;
        double yPixel = (ytile - Math.Floor(ytile)) * 1179.0;
        return ((int)xPixel, (int)yPixel);
    }
    
    (int, int) LatLonToTile(float lat, float lon, int zoom)
    {
        int x = (int)((lon + 180.0f) / 360.0f * (1 << zoom));
        int y = (int)((1.0f - Mathf.Log(Mathf.Tan(lat * Mathf.Deg2Rad) + 1.0f / Mathf.Cos(lat * Mathf.Deg2Rad)) / Mathf.PI) / 2.0f * (1 << zoom));
        return (x, y);
    }
}
