using System.Collections;
using Meta.XR.EnvironmentDepth;
using PassthroughCameraSamples;
using UnityEngine;
using UnityEngine.UI;

public class SetupCanvas : MonoBehaviour
{
    private WebCamTextureManager webCamTextureManager;

    [SerializeField] private RawImage pcaDisplay;
    [SerializeField] private RawImage pcaShaderDisplay;
    [SerializeField] private Material swirlMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        webCamTextureManager = FindFirstObjectByType<WebCamTextureManager>();

        if (webCamTextureManager != null)
        {
            StartCoroutine(SetupPCA());
        }
        else
        {
            Debug.LogError("WebCamTextureManager not found in the scene.");
        }
    }

    private IEnumerator SetupPCA()
    {
        Debug.Log("Waiting for PCA to initialize...");
        while (webCamTextureManager.WebCamTexture == null)
        {
            yield return null;
        }
        Debug.Log("PCA setup complete.");

        pcaDisplay.texture = webCamTextureManager.WebCamTexture;

        swirlMaterial.SetTexture("_MainTex", webCamTextureManager.WebCamTexture);
        pcaShaderDisplay.material = swirlMaterial;
    }
}