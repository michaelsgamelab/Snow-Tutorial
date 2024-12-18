using UnityEngine;

public class SnowController : MonoBehaviour
{
    public ComputeShader snowComputeShader;
    public RenderTexture snowRT;
    public float colorValueToAdd;

    private string snowImageProperty = "snowImage";
    private string colorValueProperty = "colorValueToAdd";
    private string resolutionProperty = "resolution";
    private string positionXProperty = "positionX";
    private string positionYProperty = "positionY";
    private string spotSizeProperty = "spotSize";

    private string csMainKernel = "CSMain";
    private string fillWhiteKernel = "FillWhite";

    private MeshRenderer meshRenderer;

    public int resolution = 512;

    private void Awake()
    {
        CreateRenderTexture();
        SetRTColorToWhite();
        SetMaterialTexture();
        InvokeRepeating(nameof(AddSnowLayer), .1f, .1f);
        ExtendBoundsofMesh();
    }

    void CreateRenderTexture()
    {
        snowRT = new RenderTexture(resolution, resolution, 24);
        snowRT.enableRandomWrite = true;
        snowRT.Create();
    }

    void SetRTColorToWhite()
    {
        int kernel_handle = snowComputeShader.FindKernel(fillWhiteKernel);
        snowComputeShader.SetTexture(kernel_handle, snowImageProperty, snowRT);
        snowComputeShader.SetFloat(colorValueProperty, colorValueToAdd);
        snowComputeShader.SetFloat(resolutionProperty, resolution);
        snowComputeShader.SetFloat(positionXProperty, 0);
        snowComputeShader.SetFloat(positionYProperty, 0);
        snowComputeShader.SetFloat(spotSizeProperty, 0);
        snowComputeShader.Dispatch(kernel_handle, snowRT.width / 8, snowRT.height / 8, 1);
    }

    void SetMaterialTexture()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.SetTexture("_PathTexture", snowRT);
    }

    void AddSnowLayer()
    {
        int kernel_handle = snowComputeShader.FindKernel(csMainKernel);
        snowComputeShader.SetTexture(kernel_handle, snowImageProperty, snowRT);
        snowComputeShader.SetFloat(colorValueProperty, colorValueToAdd);
        snowComputeShader.SetFloat(resolutionProperty, resolution);
        snowComputeShader.SetFloat(positionXProperty, 0);
        snowComputeShader.SetFloat(positionYProperty, 0);
        snowComputeShader.SetFloat(spotSizeProperty, 0);
        snowComputeShader.Dispatch(kernel_handle, snowRT.width / 8, snowRT.height / 8, 1);
    }

    void ExtendBoundsofMesh()
    {
        Bounds bounds = GetComponent<MeshFilter>().mesh.bounds;
        bounds.extents = new Vector3(2, 0, 2);
        GetComponent<MeshFilter>().mesh.bounds = bounds;
    }
}
