using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGuide : MonoBehaviour
{
    public GameObject guidePrefab;
    public Transform guideParent;
    public string[] content;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show()
    {
        StartCoroutine(ShowCo(content));
    }

    IEnumerator ShowCo(string[] _content)
    {
        GameObject guide = Instantiate(guidePrefab, guideParent);
        guide.GetComponent<Guide>().explain = _content;
        while (true)
        {
            if (guide == null)
            {
                break;
            }
            yield return null;
        }
    }
}
