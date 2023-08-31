using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadeinout : MonoBehaviour
{
    Animation anim;
    public ChangeScene scene;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        DontDestroyOnLoad(gameObject);
    }


    public void fadeinout()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        anim.clip = anim.GetClip("fadeout");
        anim.Play();
        yield return new WaitForSeconds(0.5f);
        scene.ToMainGameScene();
        yield return new WaitForSeconds(0.3f);
        anim.clip = anim.GetClip("fadein");
        anim.Play();
    }
}
