using UnityEngine;
using UnityEngine.UI;

public class Camaracontrol : MonoBehaviour
{
    public Camera cam;

    public Sprite nor;
    public Sprite hightlight;

    GameObject temp;
    private void Update()
    {
        if(Settings.popup)
        {
            return;
        }
        var mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        var e = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("UI"));
        if(e && (e.collider.name == "up" || e.collider.name == "down" || e.collider.name == "left" || e.collider.name == "right"))
        {
            if(e.collider.gameObject.GetComponent<Image>().sprite == nor)
            {
                 e.collider.gameObject.GetComponent<Image>().sprite = hightlight;
                 temp =  e.collider.gameObject;
            }
        }
        else if(temp != null)
        {
            temp.GetComponent<Image>().sprite = nor;
            temp = null;
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
