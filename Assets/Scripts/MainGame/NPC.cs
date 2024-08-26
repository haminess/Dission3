using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject chatPrefab;
    public string[] talk;
    public float maxWaitTime = 5;

    float waitTime = 0;

    StoryManager storyManager;

    // Start is called before the first frame update
    void Start()
    {
        storyManager = GameObject.Find("GameManager").GetComponent<StoryManager>();
        Animator anim = GetComponent<Animator>();
        if(anim)
        {
            anim.SetBool(0, true);
        }
    }

    private void Update()
    {
        if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && waitTime <= 0)
        {
            if (talk.Length == 0)
            {
                return;
            }
            waitTime = maxWaitTime;
            StartCoroutine(storyManager.Typing(gameObject, talk[Random.Range(0, talk.Length)]));
        }
    }
}
