using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.name == "reposition" && collision.name == "PlayerPrefab")
        {
            collision.transform.position = Vector3.zero;
            collision.GetComponent<LMove>().CurPos = Vector3.zero;
        }
    }
}
