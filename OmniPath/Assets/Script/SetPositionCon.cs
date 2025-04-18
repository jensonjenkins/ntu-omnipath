using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;
using System.Collections.Generic;

public class SetPositionCon : MonoBehaviour
{
    public GameObject arCamera;

    [SerializeField]
    private ARSession session;
    [SerializeField]
    private XROrigin sessionOrigin;
    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>();

    private void Start()
    {
        // Optional: Add button listener if this script is on a UI Button
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(SetToHelper);
        }
    }

    private void SetToHelper()
    {
        string liftName = PlayerPrefs.GetString("Helper", ""); // default empty
        if (string.IsNullOrEmpty(liftName))
        {
            Debug.LogWarning("Lift name not found in PlayerPrefs");
            return;
        }

        Target currentTarget = navigationTargetObjects.Find(x => x.Name.ToLower() == liftName.ToLower());

        if (currentTarget != null)
        {
            session.Reset();

            // Reposition the AR session origin
            sessionOrigin.MoveCameraToWorldLocation(currentTarget.PositionObject.transform.position);

            // Optionally apply rotation
            sessionOrigin.transform.rotation = currentTarget.PositionObject.transform.rotation;
        }
    }
}
