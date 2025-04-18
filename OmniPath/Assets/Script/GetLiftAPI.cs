using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Text;
using System.Collections;

public class TestAPI : MonoBehaviour
{
    private TMP_Text textMeshPro;

    public string origin;
    public string dest;

    public GameObject arObject;
    public GameObject qrObject;
    public GameObject indicator;

    private string action;

    void Start()
    {
        if (PlayerPrefs.HasKey("Action"))
        {
            action = PlayerPrefs.GetString("Action");
        }
        else
        {
            action = "findLift";
        }

        if (action == "findToilet"){
            // origin = "LT3 (NS)";
            string gender  = "Female";
            if (PlayerPrefs.HasKey("Origin"))
            {
                origin = PlayerPrefs.GetString("Origin");
            }
            StartCoroutine(GetToilet(origin, gender));
        }
        else
        {
            // origin = "LT8 (NS)";
            // dest = "Tutorial Room + 1 (NS)";
            if (PlayerPrefs.HasKey("Destination"))
            {
                dest = PlayerPrefs.GetString("Destination");
            }
            if (PlayerPrefs.HasKey("Origin"))
            {
                origin = PlayerPrefs.GetString("Origin");
            }
            StartCoroutine(GetLift(origin, dest));
        }
    }

    IEnumerator GetLift(string origin, string dest)
    {
        textMeshPro = GetComponent<TMP_Text>();

        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component not found!");
            yield break;
        }

        textMeshPro.text = "Navigate from " + origin + " to " + dest;

        string json = "{ \"origin\": \"" + origin + "\", \"dest\": \"" + dest + "\" }";

        UnityWebRequest www = UnityWebRequest.Post("https://ntu-map-scripts.onrender.com/api/get-lift", json, "application/json");
        www.timeout = 8;

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {

            string responseText = www.downloadHandler.text;

            // Check if stair_coordinates is empty
            if (responseText.Contains("[]"))
            {
                //string lift1 = null;
                //string lift2 = null;

                // textMeshPro.text = "No stair coordinates available.";
                Debug.Log("Stair coordinates are empty.");
                qrObject.SetActive(false);
                arObject.SetActive(true);
                indicator.SetActive(true);
            }
            else
            {

                int startIndex = responseText.IndexOf("\"stair_coordinates\":") + 21;
                int endIndex = responseText.IndexOf("]", startIndex);

                string stairCoordinatesStr = responseText.Substring(startIndex, endIndex - startIndex).Trim();
                string[] coordinates = stairCoordinatesStr.Split(new[] { '[', ']', ',', '"' }, System.StringSplitOptions.RemoveEmptyEntries);

                string lift1 = coordinates[0];
                string lift2 = coordinates[1];

                int startLevel = int.Parse(lift1[lift1.Length - 1].ToString());
                int endLevel = int.Parse(lift2[lift2.Length - 1].ToString());

                if (startLevel > endLevel || lift1=="-1" || lift2=="-1")
                {
                    json = "{ \"origin\": \"" + dest + "\", \"dest\": \"" + origin + "\" }";
                    www = UnityWebRequest.Post("https://ntu-map-scripts.onrender.com/api/get-lift", json, "application/json");
                    yield return www.SendWebRequest();
                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        responseText = www.downloadHandler.text;
                        startIndex = responseText.IndexOf("\"stair_coordinates\":") + 21;
                        endIndex = responseText.IndexOf("]", startIndex);

                        stairCoordinatesStr = responseText.Substring(startIndex, endIndex - startIndex).Trim();
                        coordinates = stairCoordinatesStr.Split(new[] { '[', ']', ',', '"' }, System.StringSplitOptions.RemoveEmptyEntries);

                        lift2 = coordinates[0];
                        lift1 = coordinates[1];
                        startLevel = int.Parse(lift1[lift1.Length - 1].ToString());
                        endLevel = int.Parse(lift2[lift2.Length - 1].ToString());
                    }
                }

                PlayerPrefs.SetString("Lift1", lift1);
                PlayerPrefs.Save();

                PlayerPrefs.SetString("Lift2", lift2);
                PlayerPrefs.Save();


                GameObject liftMsgObject = GameObject.Find(lift1);
                TextMeshPro msgText = liftMsgObject.GetComponent<TextMeshPro>();
                if (msgText != null)
                {
                    msgText.text = "Take the lift to level " + lift2[lift2.Length - 1].ToString();
                }

                // string arrow1 = "LT" + lift1[lift1.Length - 1].ToString() + "-" + lift1[2].ToString();
                // GameObject arrowObject1 = GameObject.Find(arrow1);

                if (startLevel < endLevel)
                {
                    for (int i = startLevel; i < endLevel; i++)
                    {
                        string arrow1 = "LT" + i.ToString() + "-" + lift1[2].ToString();
                        GameObject arrowObject1 = GameObject.Find(arrow1);
                        arrowObject1.transform.eulerAngles = new Vector3(
                            arrowObject1.transform.eulerAngles.x,
                            arrowObject1.transform.eulerAngles.y,
                            0
                        );
                        arrowObject1.transform.localScale = new Vector3(
                            arrowObject1.transform.localScale.x,
                            arrowObject1.transform.localScale.x,
                            arrowObject1.transform.localScale.x
                        );
                    }
                }
                else{
                    for (int i = startLevel; i > endLevel; i--)
                    {
                        string arrow1 = "LT" + i.ToString() + "-" + lift1[2].ToString();
                        GameObject arrowObject1 = GameObject.Find(arrow1);
                        arrowObject1.transform.eulerAngles = new Vector3(
                            arrowObject1.transform.eulerAngles.x,
                            arrowObject1.transform.eulerAngles.y,
                            180
                        );
                        arrowObject1.transform.localScale = new Vector3(
                            arrowObject1.transform.localScale.x,
                            arrowObject1.transform.localScale.x,
                            arrowObject1.transform.localScale.x
                        );
                    }
                }

                string arrow2 = "LT" + lift2[lift2.Length - 1].ToString() + "-" + lift2[2].ToString();
                GameObject arrowObject2 = GameObject.Find(arrow2);
                arrowObject2.transform.eulerAngles = new Vector3(
                    arrowObject2.transform.eulerAngles.x,
                    arrowObject2.transform.eulerAngles.y,
                    270
                );
                arrowObject2.transform.localScale = new Vector3(
                    arrowObject2.transform.localScale.x,
                    arrowObject2.transform.localScale.x,
                    arrowObject2.transform.localScale.x
                );

                textMeshPro.text = lift1 + ", "+lift2;
                Debug.Log("Request done: " + www.downloadHandler.text);

                qrObject.SetActive(false);
                arObject.SetActive(true);
                indicator.SetActive(true);
            }
        }
        else
        {
            textMeshPro.text = "API failed: " + www.error;
            Debug.LogError("Request failed: " + www.error);
        }
    }

    IEnumerator GetToilet(string origin, string gender)
    {
        textMeshPro = GetComponent<TMP_Text>();

        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro component not found!");
            yield break;
        }

        textMeshPro.text = "Navigation from " + origin + " to the nearest toilet";

        string json = "{ \"origin\": \"" + origin + "\", \"gender\": \"" + gender + "\" }";

        UnityWebRequest www = UnityWebRequest.Post("https://ntu-map-scripts.onrender.com/api/get-toilet", json, "application/json");
        www.timeout = 8;

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {

            string responseText = www.downloadHandler.text;
            int startIndex = responseText.IndexOf(":") + 2;
            int endIndex = responseText.LastIndexOf("\"");
            string nearestToilet = responseText.Substring(startIndex, endIndex - startIndex);

            PlayerPrefs.SetString("Destination", nearestToilet);
            PlayerPrefs.Save();

            textMeshPro.text = nearestToilet;
            Debug.Log("Request done: " + www.downloadHandler.text);

            qrObject.SetActive(false);
            arObject.SetActive(true);
            indicator.SetActive(true);
        }
        else
        {
            textMeshPro.text = "API failed: " + www.error;
            Debug.LogError("Request failed: " + www.error);
        }
    }
}
