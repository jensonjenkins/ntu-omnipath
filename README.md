# ntu-omnipath

## Setting Up Locally
### Client
### Server

## 📘 OmniPath How-To Guide


### How to Update the JSON File with New Destinations
  - Open `north_spine_locations.json` in a text/code editor.
  - Add a new destination entry with the following format (compulsory fields):
  ```json
  {
    "name" : "The Location Name",
    "address" : "Level - L1",
    "latitude" : 1.123,
    "longitude" : 103.123,
    "level" : 0
  }
  ```
  - Save, commit, and push to the repository.
  - Restart the backend server if needed. 
### How to Replace a QR Code at a Physical Location 
  - Identify the original anchor point.
  - Reprint and laminate the QR code.
  - Reinstall it in the same location.
  - Test the scan to confirm it works.
### How to Recalibrate AR Path if Misaligned 
  - Rescan the QR code under good lighting.
  - Restart the app if needed.
  - Adjust the QR placement and update coordinates if required.
### How to Deploy a New Version of the App
  - Finalize changes in Unity and Flask.
  - Export the iOS build and open in Xcode.
  - Archive and upload to TestFlight/App Store Connect.
  - Monitor and notify users.
### How to Test if the Navigation Server is Working
  - Send a GET request via Postman/CURL.
  - Confirm 200 OK response and valid JSON.
  - Restart Flask server if issues occur.
### How to Update Destination Coordinates
  - Open the JSON file.
  - Edit the lat/long/zLevel of the destination.
  - Save and push updates.
  - Restart server if needed.
### How to Handle Feedback Reports from Users
  - Note feedback from students.
  - Document and prioritize issues.
  - Assign tasks or log tickets.
### How to Add a New Feature to the UI
  - Open the Unity project.
  - Create UI elements under Canvas.
  - Write and attach C# scripts.
  - Connect in Unity Inspector and test before deployment.



