using System.Collections.Generic;
using Unity.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;

using TMPro;

using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class QrCodeRecenter : MonoBehaviour
{
    [SerializeField]
    private ARSession session;
    [SerializeField]
    private XROrigin sessionOrigin;
    [SerializeField]
    private ARCameraManager cameraManager;
    [SerializeField]
    private GameObject indicator;
    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>();

    //public GameObject arObject;
    public GameObject scanMsg;
    public GameObject apiMsg;

    public string dest;
    public string origin;

    private Texture2D cameraImageTexture;
    private IBarcodeReader reader = new BarcodeReader();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            origin = "Lee Wee Nam Library (NS)";

            PlayerPrefs.SetString("Origin", origin);
            PlayerPrefs.Save();

            SetQrCodeRecenterTarget(origin);
            scanMsg.SetActive(false);
            apiMsg.SetActive(true);

            gameObject.SetActive(false);
            // arObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        cameraManager.frameReceived += OnCameraFrameRecieved;
    }

    private void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameRecieved;
    }

    private void OnCameraFrameRecieved(ARCameraFrameEventArgs eventArgs)
    {

        if (!cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            return;
        }

        var conversionParams = new XRCpuImage.ConversionParams
        {
            //Get the entire image
            inputRect = new RectInt(0, 0, image.width, image.height),

            //DOwnsample by 2
            outputDimensions = new Vector2Int(image.width/6, image.height/6),

            //Choose RGB format
            outputFormat = TextureFormat.RGBA32,

            //Filp across the verticle axis (mirror image)
            transformation = XRCpuImage.Transformation.MirrorY
        };

        //Check number of bytes needed to store the image
        int size = image.GetConvertedDataSize(conversionParams);

        //Allocate a buffer to store the image
        var buffer = new NativeArray<byte>(size, Allocator.Temp);

        //Extract the image data
        image.Convert(conversionParams, buffer);

        //The image was converted to RGBA32 format and written into the provided buffer
        //dispose of the image to prevent leak resources
        image.Dispose();

        //process the image and pass it to a computer version algorithm
        //apply it to a texture to visualize it

        //put image into texture to visualize
        cameraImageTexture = new Texture2D(
            conversionParams.outputDimensions.x,
            conversionParams.outputDimensions.y,
            conversionParams.outputFormat,
            false);

        cameraImageTexture.LoadRawTextureData(buffer);
        cameraImageTexture.Apply();

        //done w temp
        buffer.Dispose();

        //Detect and decode the barcode inside the bitmap
        var result = reader.Decode(cameraImageTexture.GetPixels32(), cameraImageTexture.width, cameraImageTexture.height);


        //GameObject textObject = GameObject.Find("ScanMsg");
        //TextMeshProUGUI textComponent = textObject.GetComponent<TextMeshProUGUI>();

        if (result != null)
        {
            PlayerPrefs.SetString("Origin", result.Text);
            PlayerPrefs.Save();

            SetQrCodeRecenterTarget(result.Text);
            scanMsg.SetActive(false);
            apiMsg.SetActive(true);

            gameObject.SetActive(false);
        }
    }

    private void SetQrCodeRecenterTarget(string targetText)
    {
        Target currentTarget = navigationTargetObjects.Find(x => x.Name.ToLower().Equals(targetText.ToLower()));
        if (currentTarget != null)
        {
            //Reset position and rotation of ARSession
            session.Reset();

            //Add offset for recentering
            sessionOrigin.transform.position = currentTarget.PositionObject.transform.position;
            sessionOrigin.transform.rotation = currentTarget.PositionObject.transform.rotation;

            // Reposition the indicator: match X & Z, offset Y by +1
            Vector3 originPos = sessionOrigin.transform.position;
            Vector3 indicatorPos = new Vector3(originPos.x, originPos.y + 1f, originPos.z);
            indicator.transform.position = indicatorPos;
        }
    }
}
