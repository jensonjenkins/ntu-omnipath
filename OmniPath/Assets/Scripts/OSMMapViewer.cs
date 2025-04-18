using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class OSMMapViewer : MonoBehaviour
{
    public RawImage mapImage;  
    public Button zoomInButton, zoomOutButton;

    private int zoomLevel = 16;  // Initial zoom level (1-19)
    private int tileX, tileY; 
    private Vector2 lastMousePosition;
    private float nsLat = 1.3463f, nsLon = 103.680822f;
    private Vector2 currentOffset = Vector2.zero;  
    private RectTransform mapRect;
    private const string OSM_URL = "https://tile.openstreetmap.org/{0}/{1}/{2}.png";

    void Start()
    {
        mapRect = mapImage.rectTransform;
        (tileX, tileY) = LatLonToTile(nsLat, nsLon, zoomLevel);
        StartCoroutine(LoadMapTile(tileX, tileY));

        zoomInButton.onClick.AddListener(() => ChangeZoom(1));
        zoomOutButton.onClick.AddListener(() => ChangeZoom(-1));
    }

    IEnumerator LoadMapTile(int X, int Y)
    {
        string url = string.Format(OSM_URL, zoomLevel, X, Y);
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                mapImage.texture = DownloadHandlerTexture.GetContent(request);
            }
            else
            {
                Debug.LogError("Error loading map: " + request.error);
            }
        }
    }

    void ChangeZoom(int change)
    {
        zoomLevel = Mathf.Clamp(zoomLevel + change, 1, 19);
        (int tileX, int tileY) = LatLonToTile(nsLat, nsLon, zoomLevel);
        StartCoroutine(LoadMapTile(tileX, tileY));
    }

    (int, int) LatLonToTile(float lat, float lon, int zoom)
    {
        int x = (int)((lon + 180.0f) / 360.0f * (1 << zoom));
        int y = (int)((1.0f - Mathf.Log(Mathf.Tan(lat * Mathf.Deg2Rad) + 1.0f / Mathf.Cos(lat * Mathf.Deg2Rad)) / Mathf.PI) / 2.0f * (1 << zoom));
        return (x, y);
    }
}

