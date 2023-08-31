using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadeinout : MonoBehaviour
{
    Animation anim;
    public AnimationClip[] clip;
    public ChangeScene scene;
    private void Start()
    {
       anim = gameObject.GetComponent<Animation>();
        DontDestroyOnLoad(gameObject);
    }

    public void fadeoutin()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        anim.clip = clip[1];
        anim.Play();
        yield return new WaitForSeconds(0.8f);
        scene.ToMainGameScene();
        yield return new WaitForSeconds(0.5f);
        anim.clip = clip[0];
        anim.Play();
    }
}
