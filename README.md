# ntu-omnipath
A navigation application powered by AR to navigate NTU's North Main Spine.

This repository contains the final result and deliverables for group B's IM3180 AY24/25 project.

## Setting Up Locally
### Server
1. Set up a python virtual environment
```bash
python3 -m venv venv
source venv/bin/activate
```
2. Install key dependencies
```bash
pip install -r requirements.txt
```
3. Start the server
```bash
python3 waitress_server.py
```
### Client
Make sure to have the following installed:
- Version Xcode 16.2
- Unity 2023.1.15f1

1. Install the project files and open through Unity Hub.
2. Under 'File' click on 'Build Settings...' and the following window should appear.
   
   <img src="https://github.com/user-attachments/assets/ebe69854-9372-4973-b2cc-f0cfc4fbe085" width="450">
3. Select on 'iOS' and click on 'Build'. A build folder should appear in the chosen directory.
4. Open XCode, connect the desired iOS device using a data cable and select the folder built in step 3.
5. Build onto the iOS device.

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
  - If ran locally, restart the server and re-run `python3 waitress_server.py`
  - If deployed on server (e.g. render.com for our case), push to the tracked repository and let the set CI/CD pipelines run.
### How to Test if the Navigation Server is Working
  - To test the `get_lift` endpoint, an example would be the following if the server ran at `http:://0.0.0.0:8080` :
    ```bash
    curl  -X POST http://0.0.0.0:8080/api/get-lift \
          -H 'Content-Type: application/json' \
          -d '{"origin": "LT8 (NS)", "dest": "LT9 (NS)"}'
    ```
    The 'd' flag contains a JSON payload with fields "origin" and "dest". The values must be exactly as written in the `north_spine_locations.json`
  - Similarly to test the `get_toilet` endpoint, an example would be
    ```bash
    curl  -X POST http://0.0.0.0:8080/api/get-toilet \
          -H 'Content-Type: application/json' \
          -d '{"origin": "LT8 (NS)", "gender": "male"}'
    ```
    Where `gender` is either `male` or `female`.



