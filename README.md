# ntu-omnipath

# 📘 OmniPath How-To Guide

| No. | Task                                              | Steps |
|-----|---------------------------------------------------|-------|
| 1 | **How to Update the JSON File with New Destinations** | - Open `north_spine_locations.json` in a text/code editor.<br>- Add a new destination entry (name, latitude, longitude, zLevel).<br>- Save, commit, and push to the repository.<br>- Restart the backend server if needed. |
| 2 | **How to Replace a QR Code at a Physical Location** | - Identify the original anchor point.<br>- Reprint and laminate the QR code.<br>- Reinstall it in the same location.<br>- Test the scan to confirm it works. |
| 3 | **How to Recalibrate AR Path if Misaligned** | - Rescan the QR code under good lighting.<br>- Restart the app if needed.<br>- Adjust the QR placement and update coordinates if required. |
| 4 | **How to Deploy a New Version of the App** | - Finalize changes in Unity and Flask.<br>- Export the iOS build and open in Xcode.<br>- Archive and upload to TestFlight/App Store Connect.<br>- Monitor and notify users. |
| 5 | **How to Test if the Navigation Server is Working** | - Send a GET request via Postman/CURL.<br>- Confirm 200 OK response and valid JSON.<br>- Restart Flask server if issues occur. |
| 6 | **How to Update Destination Coordinates** | - Open the JSON file.<br>- Edit the lat/long/zLevel of the destination.<br>- Save and push updates.<br>- Restart server if needed. |
| 7 | **How to Handle Feedback Reports from Users** | - Note feedback from students<br>- Document and prioritize issues.<br>- Assign tasks or log tickets. |
| 8 | **How to Add a New Feature to the UI** | - Open the Unity project.<br>- Create UI elements under Canvas.<br>- Write and attach C# scripts.<br>- Connect in Unity Inspector and test before deployment. |
| 9 | **task** | - stap1<br>- step2<br> |
| 10 | **task** | - stap1<br>- step2<br> |
| 11 | **task** | - stap1<br>- step2<br> |


