using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FllowPlayer : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<Player>().enabled && !MainGame.instance.isEnd)
        {
            gameObject.transform.position = new Vector2(8.6f, -33.8f);
        }
        else if (!player.GetComponent<Player>().enabled && MainGame.instance.isEnd)
        {
            gameObject.transform.position = new Vector2(28.98f, -0.9f);
        }
        else
        {
            gameObject.transform.position = player.transform.position;
        }
    }
}
