using UnityEngine;

public class OrderInLayerByZ : MonoBehaviour
{

    private float xpos;
    private float ypos;
    private Transform tf;

    private void Start()
    {
        tf = GetComponent<Transform>();
        xpos = tf.position.x;
        ypos = tf.position.y;
        tf.position = new Vector3(xpos, ypos, ypos / 1000.0f);
    }
}
