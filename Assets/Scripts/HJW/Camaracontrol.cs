using UnityEngine;
using UnityEngine.UI;

public class Camaracontrol : MonoBehaviour
{
    public Camera cam;
    public Vector3 mosposanchor;
    public Vector3 curmospos;
    private void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            mosposanchor = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        }
        if(Input.GetMouseButton(2))
        {
            curmospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
            cam.transform.position += new Vector3( mosposanchor.x - curmospos.x, mosposanchor.y - curmospos.y);
        }
        if(Input.mouseScrollDelta.y > 0 && !Makemadi.instance.chart && !Maketile.instance.mos.hidingpointer)
        {
            if(cam.orthographicSize > 1.6f)
            {
                cam.orthographicSize -= 2;
            }
        }
        if (Input.mouseScrollDelta.y < 0 && !Makemadi.instance.chart && !Maketile.instance.mos.hidingpointer)
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

    }

}
