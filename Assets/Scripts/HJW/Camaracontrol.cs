using UnityEngine;
using UnityEngine.UI;

public class Camaracontrol : MonoBehaviour
{
    public Camera cam;

    public Sprite high;
    public Sprite nor;

    Sprite a;
    Image a_;
    public Vector3 real_mospos;
    private void Update()
    {
        if(Settings.popup)
        {
            return;
        }
        var mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        real_mospos = new Vector2( cam.transform.position.x - mospos.x, cam.transform.position.y - mospos.y);
        var e = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("Camctr"));
        if(e && e.collider.gameObject.GetComponent<Image>().sprite == nor)
        {
            a = e.collider.gameObject.GetComponent<Image>().sprite = high;
            a_ = e.collider.gameObject.GetComponent<Image>();
        }
        else if (e == false && a == high)
        {
            a_.sprite = nor;
        }
        if (e && Input.GetMouseButton(0))
        {
            switch (e.collider.name)
            {
                case "up":
                    up();
                    break;
                case "down":
                    down();
                    break;
                case "left":
                    left();
                    break;
                case "right":
                    right();
                    break;
            }
        }
        if (Input.GetMouseButton(2))
        {
            cam.transform.position = new Vector3(cam.transform.position.x - (real_mospos.x * 0.1f), cam.transform.position.y - (real_mospos.y * 0.1f), -10);
        }

    }
    public void up()
    {
        cam.transform.position += new Vector3(0, 0.1f, 0);
    }
    public void down()
    {
        cam.transform.position += new Vector3(0, -0.1f, 0);
    }
    public void left()
    {
        cam.transform.position += new Vector3(-0.1f, 0, 0);

    }
    public void right()
    {

        cam.transform.position += new Vector3(0.1f, 0, 0);
    }


}
