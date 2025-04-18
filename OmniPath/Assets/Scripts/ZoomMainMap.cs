using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ZoomMainMap : MonoBehaviour, IScrollHandler
{
    public Image imageDisplay;
    public Sprite img1, img2, img3;

    public Vector3 minZoomV = Vector3.one;
    public Vector3 midZoomV = new Vector3(2f, 2f, 2f);
    public Vector3 maxZoomV = new Vector3(4f, 4f, 4f);
    public float zoomSpeed = 0.1f;
    public float maxZoom = 10f;

    private Vector3 initialScale;

    void Awake() {
        imageDisplay.sprite = img1;
        initialScale = transform.localScale;
    }

    public void OnScroll(PointerEventData eventData){
        var delta = Vector3.one * (eventData.scrollDelta.y * zoomSpeed);
        var desiredScale = transform.localScale + delta;

        desiredScale = ClampDesiredScale(desiredScale);

        transform.localScale = desiredScale;

        UpdateImage(desiredScale);
    }

    private Vector3 ClampDesiredScale(Vector3 desiredScale){
        desiredScale = Vector3.Max(initialScale, desiredScale);
        desiredScale = Vector3.Min(initialScale * maxZoom, desiredScale);
        return desiredScale;
    }

    void UpdateImage(Vector3 ds){
        if (ds.magnitude >= midZoomV.magnitude && ds.magnitude < maxZoomV.magnitude){
            imageDisplay.sprite = img2;
        }else if (ds.magnitude >= maxZoomV.magnitude){
            imageDisplay.sprite = img3;
        }else{
            imageDisplay.sprite = img1;
        }
    } 
}
