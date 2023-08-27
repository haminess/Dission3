using UnityEngine;

public class Camaracontrol : MonoBehaviour
{
    public Camera cam;

    private void Update()
    {
        var mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
        var e = Physics2D.Raycast(mospos, Vector3.forward, 2, LayerMask.GetMask("UI"));
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
