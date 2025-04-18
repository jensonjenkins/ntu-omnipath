using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SetNaviTarget : MonoBehaviour {

    [SerializeField]
    private TMP_Dropdown navigationTargetDropdown;
    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>();

    // private NavMeshPath path;
    // private LineRenderer line;
    // private Vector3 targetPosition = Vector3.zero;

    private NavMeshPath path1; // Path for the ori-lift line
    private NavMeshPath path2; // Path for the lift-dest line

    private LineRenderer line1; // Line renderer for the ori-lift path
    private LineRenderer line2; // Line renderer for the lift-dest path

    private Vector3 targetPosition1 = Vector3.zero;
    private Vector3 targetPosition2 = Vector3.zero;
    private Vector3 destPosition = Vector3.zero;

    private bool haveLift = false;

    public GameObject arObject;

    public string dest;

    private bool lineToggle = true;

    private void Start(){
        // path = new NavMeshPath();
        // line = transform.GetComponent<LineRenderer>();
        // line.enabled = lineToggle;
        //
        // if (PlayerPrefs.HasKey("Lift1"))
        // {
        //     dest = PlayerPrefs.GetString("Lift1");
        //     Debug.Log("Destination loaded: " + dest);
        // }
        //
        // string selectedText = dest;
        // //string selectedText = navigationTargetDropdown.options[1].text;
        // Target currentTarget = navigationTargetObjects.Find(x => x.Name.Equals(selectedText));
        // if (currentTarget != null)
        // {
        //     targetPosition = currentTarget.PositionObject.transform.position;
        // }
        path1 = new NavMeshPath();
        path2 = new NavMeshPath();

        line1 = transform.GetComponent<LineRenderer>();
        line2 = transform.GetComponent<LineRenderer>();

        line1.enabled = lineToggle;
        line2.enabled = lineToggle;

        //Lift1 position
        if (PlayerPrefs.HasKey("Lift1"))
        {
            haveLift = true;
            dest = PlayerPrefs.GetString("Lift1");
            string dest1 = dest;
            Target currentTarget1 = navigationTargetObjects.Find(x => x.Name.Equals(dest1));

            if (currentTarget1 != null)
            {
                targetPosition1 = currentTarget1.PositionObject.transform.position;
            }
        }

        //Lift2 position
        if (PlayerPrefs.HasKey("Lift2"))
        {
            haveLift = true;
            dest = PlayerPrefs.GetString("Lift2");
            string dest2 = dest;
            Target currentTarget2 = navigationTargetObjects.Find(x => x.Name.Equals(dest2));

            if (currentTarget2 != null)
            {
                targetPosition2 = currentTarget2.PositionObject.transform.position;
            }
        }

        //Dest position
        if (PlayerPrefs.HasKey("Destination"))
        {
            dest = PlayerPrefs.GetString("Destination");

            string enddest = dest;
            Target currentTarget3 = navigationTargetObjects.Find(x => x.Name.Equals(enddest));

            if (currentTarget3 != null)
            {
                destPosition = currentTarget3.PositionObject.transform.position;
            }
        }

    }

    private void Update() {
        // if (arObject.activeSelf && lineToggle && targetPosition != Vector3.zero){
        //     NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);
        //     line.positionCount = path.corners.Length;
        //     line.SetPositions(path.corners);
        // }
        if (arObject.activeSelf && lineToggle)
        {
            if (haveLift)
            {
                // Calculate path to Lift1 (First line)
                if (targetPosition1 != Vector3.zero)
                {
                    NavMesh.CalculatePath(transform.position, targetPosition1, NavMesh.AllAreas, path1);
                    // First line
                    if (path1.status == NavMeshPathStatus.PathComplete)
                    {
                        PlayerPrefs.SetString("Helper", PlayerPrefs.GetString("Lift1"));
                        PlayerPrefs.Save();

                        line1.positionCount = path1.corners.Length;
                        line1.SetPositions(path1.corners);
                    }
                    // Second line
                    else
                    {
                        PlayerPrefs.SetString("Helper", PlayerPrefs.GetString("Destination"));
                        PlayerPrefs.Save();

                        NavMesh.CalculatePath(transform.position, destPosition, NavMesh.AllAreas, path2);
                        if (path2.status == NavMeshPathStatus.PathComplete)
                        {
                            line2.positionCount = path2.corners.Length;
                            line2.SetPositions(path2.corners);
                        }
                        else
                        {
                            NavMesh.CalculatePath(targetPosition2, destPosition, NavMesh.AllAreas, path2);
                            line2.positionCount = path2.corners.Length;
                            line2.SetPositions(path2.corners);
                        }
                    }
                }
            }
            else {
                NavMesh.CalculatePath(transform.position, destPosition, NavMesh.AllAreas, path2);
                line2.positionCount = path2.corners.Length;
                line2.SetPositions(path2.corners);
            }
        }
    }

    public void ToogleVisibility() {
        // lineToggle = !lineToggle;
        // line.enabled = lineToggle;
        lineToggle = !lineToggle;
        line1.enabled = lineToggle;
        line2.enabled = lineToggle;
    }

    // public void SetCurrentNavigationTarget(int selectedValues) {
    //     targetPosition = Vector3.zero;
    //     string selectedText = navigationTargetDropdown.options[selectedValues].text;
    //     Target currentTarget = navigationTargetObjects.Find(x => x.Name.Equals(selectedText));
    //     if (currentTarget != null) {
    //         targetPosition = currentTarget.PositionObject.transform.position;
    //     }
    // }
}
