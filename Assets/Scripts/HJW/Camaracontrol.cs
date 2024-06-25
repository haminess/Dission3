using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Camaracontrol : MonoBehaviour
{
    public Camera cam;
    public Vector3 mosposanchor;
    public Vector3 viewportmospos;
    private void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            mosposanchor = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        }
        if(Input.GetMouseButton(2))
        {
            cam.transform.position += new Vector3( mosposanchor.x - Maketile.instance.mospos.x, mosposanchor.y - Maketile.instance.mospos.y);
        }
        if(Input.mouseScrollDelta.y > 0 && !Makemadi.instance.chart)
        {
            if(cam.orthographicSize > 1.6f)
            {
                cam.orthographicSize -= 2;
            }
        }
        if (Input.mouseScrollDelta.y < 0 && !Makemadi.instance.chart)
        {
            cam.orthographicSize += 2;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cam.transform.position += new Vector3(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cam.transform.position += new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cam.transform.position += new Vector3(0, -0.1f, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cam.transform.position += new Vector3(0, 0.1f, 0);
        }
        viewportmospos = Camera.main.WorldToViewportPoint(Maketile.instance.mospos);
        if(Maketile.instance.makeingtrail)
        {
            if (viewportmospos.x > 0.9f)
            {
                cam.transform.position += new Vector3(0.1f, 0, 0);
            }
            if(viewportmospos.x < 0.1f)
            {
                cam.transform.position += new Vector3(-0.1f, 0, 0);
            }
            if(viewportmospos.y > 0.85f)
            {
                cam.transform.position += new Vector3(0, 0.1f, 0);
            }
            if(viewportmospos.y < 0.25f)
            {
                cam.transform.position += new Vector3(0, -0.1f, 0);
            }
        }
    }

}
