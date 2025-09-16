using UnityEngine;

public class SetChildrenTransparent : MonoBehaviour
{
    [Header("͸��������")]
    [Range(0, 1)] public float minTransparency = 0.2f; // ��С͸����
    [Range(0, 1)] public float maxTransparency = 0.8f; // ���͸����
    public float breathSpeed = 1.0f;                   // �����ٶ�

    [Header("���÷�Χ����")]
    [SerializeField] private bool affectSelf = true;      // �Ƿ����õ�����
    [SerializeField] private bool affectChildren = true;  // �Ƿ����õ�������

    private Renderer[] childRenderers;
    private Renderer ownRenderer;
    private float currentTransparency;

    void Start()
    {
        if (affectChildren) childRenderers = GetComponentsInChildren<Renderer>();
        if (affectSelf) ownRenderer = GetComponent<Renderer>();

        // ��ʼ������Ϊ͸��ģʽ
        if (affectChildren) ApplyToMaterials(childRenderers, ConfigureMaterial);
        if (affectSelf) ApplyToMaterials(ownRenderer, ConfigureMaterial);

        // ��ʼ��͸����
        currentTransparency = minTransparency;
        UpdateTransparency();
    }

    void Update()
    {
        // ����͸����
        currentTransparency = Mathf.Lerp(minTransparency, maxTransparency,
            (Mathf.Sin(Time.time * breathSpeed) + 1f) / 2f);

        UpdateTransparency();
    }

    /// <summary>
    /// �������в��ʵ�͸����
    /// </summary>
    private void UpdateTransparency()
    {
        if (affectChildren) ApplyToMaterials(childRenderers, m => SetMaterialTransparency(m, currentTransparency));
        if (affectSelf) ApplyToMaterials(ownRenderer, m => SetMaterialTransparency(m, currentTransparency));
    }

    /// <summary>
    /// ���ò���Ϊ͸����Ⱦģʽ
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

        // URP/HDRP ֧��
        if (material.HasProperty("_Surface"))
        {
            material.SetFloat("_Surface", 1f); // 0=Opaque, 1=Transparent
        }
    }

    /// <summary>
    /// ���ò��ʵ�͸����
    /// </summary>
    private void SetMaterialTransparency(Material material, float alpha)
    {
        Color color = material.color;
        color.a = alpha;
        material.color = color;
    }

    /// <summary>
    /// ������Ⱦ��Ӧ�ò���
    /// </summary>
    private void ApplyToMaterials(Renderer[] renderers, System.Action<Material> action)
    {
        if (renderers == null) return;
        foreach (var renderer in renderers)
            ApplyToMaterials(renderer, action);
    }

    /// <summary>
    /// ����������Ⱦ��Ӧ�ò���
    /// </summary>
    private void ApplyToMaterials(Renderer renderer, System.Action<Material> action)
    {
        if (renderer == null) return;
        foreach (var material in renderer.materials)
            action?.Invoke(material);
    }
}
