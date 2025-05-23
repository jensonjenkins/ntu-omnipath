using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using ZXing;

public class GetImageAlternative : MonoBehaviour{

    [SerializeField]
    private ARCameraBackground aRCameraBackground;
    [SerializeField]
    private RenderTexture targetRenderTexture;
    [SerializeField]
    private TextMeshProUGUI qrCodeText;

    private Texture2D cameraImageTexture;
    private IBarcodeReader reader = new BarcodeReader();

    private void Update() {
        Graphics.Blit(null, targetRenderTexture, aRCameraBackground.material);
        cameraImageTexture = new Texture2D(targetRenderTexture.width, targetRenderTexture.height, TextureFormat.RGBA32, false);
        Graphics.CopyTexture(targetRenderTexture, cameraImageTexture);

        //Detect and decode the barcode inside the bitmap
        var result = reader.Decode(cameraImageTexture.GetPixels32(), cameraImageTexture.width, cameraImageTexture.height);

        //Do smth w the result
        if (result != null) {
            qrCodeText.text = result.Text;
        }
    }
}