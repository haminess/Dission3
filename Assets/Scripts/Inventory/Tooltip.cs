using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text tName;
    public Text tContext;

    public void SetupTooltip(string name, string context)
    {
        tName.text = name;
        tContext.text = context;
    }

    float halfWidth;
    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        halfWidth = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;

        if (rt.anchoredPosition.x + rt.sizeDelta.x > halfWidth)
        {
            rt.pivot = new Vector2(1, 1);
        }
        else
        {
            rt.pivot = new Vector2(0, 1);
        }
    }
}
