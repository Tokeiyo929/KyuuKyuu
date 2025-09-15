using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> tooltips = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HideAllTooltips()
    {
        foreach (GameObject tooltip in tooltips)
        {
            tooltip.SetActive(false);
        }
    }
}
