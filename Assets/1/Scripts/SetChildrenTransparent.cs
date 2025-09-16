using UnityEngine;

public class SetChildrenTransparent : MonoBehaviour
{
    [Header("透明度设置")]
    [Range(0, 1)] public float minTransparency = 0.2f; // 最小透明度
    [Range(0, 1)] public float maxTransparency = 0.8f; // 最大透明度
    public float breathSpeed = 1.0f;                   // 呼吸速度

    [Header("作用范围控制")]
    [SerializeField] private bool affectSelf = true;      // 是否作用到自身
    [SerializeField] private bool affectChildren = true;  // 是否作用到子物体

    private Renderer[] childRenderers;
    private Renderer ownRenderer;
    private float currentTransparency;

    void Start()
    {
        if (affectChildren) childRenderers = GetComponentsInChildren<Renderer>();
        if (affectSelf) ownRenderer = GetComponent<Renderer>();

        // 初始化材质为透明模式
        if (affectChildren) ApplyToMaterials(childRenderers, ConfigureMaterial);
        if (affectSelf) ApplyToMaterials(ownRenderer, ConfigureMaterial);

        // 初始化透明度
        currentTransparency = minTransparency;
        UpdateTransparency();
    }

    void Update()
    {
        // 呼吸透明度
        currentTransparency = Mathf.Lerp(minTransparency, maxTransparency,
            (Mathf.Sin(Time.time * breathSpeed) + 1f) / 2f);

        UpdateTransparency();
    }

    /// <summary>
    /// 更新所有材质的透明度
    /// </summary>
    private void UpdateTransparency()
    {
        if (affectChildren) ApplyToMaterials(childRenderers, m => SetMaterialTransparency(m, currentTransparency));
        if (affectSelf) ApplyToMaterials(ownRenderer, m => SetMaterialTransparency(m, currentTransparency));
    }

    /// <summary>
    /// 设置材质为透明渲染模式
    /// </summary>
    private void ConfigureMaterial(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.SetInt("_ZTest", (int)UnityEngine.Rendering.CompareFunction.LessEqual);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + 1;

        // URP/HDRP 支持
        if (material.HasProperty("_Surface"))
        {
            material.SetFloat("_Surface", 1f); // 0=Opaque, 1=Transparent
        }
    }

    /// <summary>
    /// 设置材质的透明度
    /// </summary>
    private void SetMaterialTransparency(Material material, float alpha)
    {
        Color color = material.color;
        color.a = alpha;
        material.color = color;
    }

    /// <summary>
    /// 遍历渲染器应用操作
    /// </summary>
    private void ApplyToMaterials(Renderer[] renderers, System.Action<Material> action)
    {
        if (renderers == null) return;
        foreach (var renderer in renderers)
            ApplyToMaterials(renderer, action);
    }

    /// <summary>
    /// 遍历单个渲染器应用操作
    /// </summary>
    private void ApplyToMaterials(Renderer renderer, System.Action<Material> action)
    {
        if (renderer == null) return;
        foreach (var material in renderer.materials)
            action?.Invoke(material);
    }
}
