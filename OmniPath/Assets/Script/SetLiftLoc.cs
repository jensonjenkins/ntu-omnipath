using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;
using System.Collections.Generic;

public class SetLiftLoc : MonoBehaviour
{
    public GameObject arCamera;

    [SerializeField]
    private ARSession session;
    [SerializeField]
    private XROrigin sessionOrigin;
    [SerializeField]
    private GameObject indicator;
    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>();

    private void Start()
    {
        // Optional: Add button listener if this script is on a UI Button
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(SetToLift);
        }
    }

    private void SetToLift()
    {
        string liftName = PlayerPrefs.GetString("Lift2", ""); // default empty
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

            // Reposition the indicator: match X & Z, offset Y by +1
            Vector3 originPos = sessionOrigin.transform.position;
            Vector3 indicatorPos = new Vector3(originPos.x, originPos.y + 1f, originPos.z);
            indicator.transform.position = indicatorPos;
        }
    }
}
