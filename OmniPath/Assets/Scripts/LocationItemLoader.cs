using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using Newtonsoft.Json;

public class LocationItemLoader : MonoBehaviour
{
    public GameObject placeItemPrefab;
    public Transform contentParent;
    public TMP_InputField searchInput;  // Reference to the search bar
    private List<PlaceData> allPlaces = new List<PlaceData>();
    private List<GameObject> spawnedItems = new List<GameObject>();

    private string jsonPath;

    void Start()
    {
        jsonPath = Path.Combine(Application.streamingAssetsPath, "north_spine_locations.json");

        LoadPlaces();
        searchInput.onValueChanged.AddListener(FilterPlaces);
    }

    void LoadPlaces()
    {
        if (!File.Exists(jsonPath)) return;
        string jsonText = File.ReadAllText(jsonPath);
        allPlaces = JsonConvert.DeserializeObject<List<PlaceData>>(jsonText);

        UpdateUI(allPlaces);
    }

    void UpdateUI(List<PlaceData> places)
    {
        // Clear previous UI elements
        foreach (GameObject item in spawnedItems)
            Destroy(item);
        spawnedItems.Clear();

        // Generate UI elements
        foreach (PlaceData place in places)
        {
            GameObject newItem = Instantiate(placeItemPrefab, contentParent);
            newItem.GetComponent<LocationItem>().SetData(place.name, place.address);
            var locationButton = newItem.GetComponent<LocationButton>();
            string imagePath;
            if(place.imgFileName != null){
                imagePath = Path.Combine(Application.streamingAssetsPath, place.imgFileName);
            }else{
                imagePath = Path.Combine(Application.streamingAssetsPath, "random.png");
            }
            locationButton.SetData(place.name, place.address, place.latitude, place.longitude, imagePath);
            locationButton.Initialize();
            spawnedItems.Add(newItem);
        }
    }

    void FilterPlaces(string searchText)
    {
        searchText = searchText.ToLower();
        List<PlaceData> filteredPlaces = allPlaces.FindAll(place => 
            place.name.ToLower().Contains(searchText) || place.address.ToLower().Contains(searchText));

        UpdateUI(filteredPlaces);
    }

    public void FilterSchool()
    {
        List<PlaceData> filteredPlaces = allPlaces.FindAll(place => place.category == "school");
        UpdateUI(filteredPlaces);
    }

    public void FilterLabs()
    {
        List<PlaceData> filteredPlaces = allPlaces.FindAll(place => place.category == "lab");
        UpdateUI(filteredPlaces);
    }

    public void FilterFood(int[] filter)
    {
        List<PlaceData> filteredPlaces = allPlaces.FindAll(place => 
                place.category == "food" && 
                (filter[0] == 0 || place.isHalal) && 
                (filter[1] == 0 || place.isVegan) && 
                (filter[2] + filter[3] + filter[4] == 0 ||
                 (filter[2] == 1 && place.priceRange == 0) || 
                 (filter[3] == 1 && place.priceRange == 1) ||
                 (filter[4] == 1 && place.priceRange == 2))
                );
        UpdateUI(filteredPlaces);
    }

    [System.Serializable]
    public class PlaceData
    {
        public string name;
        public string address;
        public float latitude;
        public float longitude;
        public string category;

        public string imgFileName;

        // food specific
        public bool isVegan;
        public bool isHalal;
        public int priceRange;
    }
}

