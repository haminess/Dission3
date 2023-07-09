using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Maketile : MonoBehaviour
{
    public GameObject curpointer;
    public GameObject pointer;
    public GameObject prefeb;
    public Vector3 mospos;
    public int index;
    [Space(20)]
    public Vector2[] boxpos;
    public int mode;

    private bool holding;
    // Start is called before the first frame update
    void Start()
    {
        index = 1;
        mode = 0;
        holding = false;
    }

    // Update is called once per frame
    void Update()
    {
        mospos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 9);
            if(mospos.y < 0 && mospos.x > 0)
            {
                curpointer.transform.position = new Vector2((int)mospos.x + 0.5f, (int)mospos.y - 0.5f);
            }
            else if(mospos.y > 0 && mospos.x < 0)
            {
                curpointer.transform.position = new Vector2((int)mospos.x - 0.5f, (int)mospos.y + 0.5f);
            }
            else if(mospos.y < 0 && mospos.x < 0)
            {
                curpointer.transform.position = new Vector2((int)mospos.x - 0.5f, (int)mospos.y - 0.5f);
            }
            else
            {
                curpointer.transform.position = new Vector2((int)mospos.x + 0.5f, (int)mospos.y + 0.5f);
            }

        if(Input.GetMouseButtonDown(0))
        {
            var e = Physics2D.Raycast(mospos, Vector3.forward, 2);
            switch (mode)
            {
                case 0: //make
                    if (e && e.collider.tag == "tile" || e && e.collider.tag == "ui")
                    {
                        return;
                    }
                    Instantiate(prefeb, new Vector2(curpointer.transform.position.x, curpointer.transform.position.y), Quaternion.identity, gameObject.transform);
                    break;
                case 1: //erase
                    if (e && e.collider.tag == "tile")
                    {
                        Destroy(e.collider.gameObject);
                    }
                    break;
                case 2: //edit
                    if(holding)
                    {
                        if(e && e.collider.tag == "tile")
                        {
                            curpointer.GetComponent<BoxCollider2D>().enabled = true;
                            curpointer = e.collider.gameObject;
                            curpointer.GetComponent<BoxCollider2D>().enabled = false;
                        }
                        else
                        {
                            curpointer.GetComponent<BoxCollider2D>().enabled = true;
                            curpointer = pointer;
                            holding = false;
                        }
                    }
                    else if (e && e.collider.tag == "tile")
                    {
                        curpointer = e.collider.gameObject;
                        e.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        pointer.GetComponent<SpriteRenderer>().enabled = false;
                        holding = true;
                    }
                    break;
            };
        }
        repaint();
    }

    void repaint()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponentInChildren<TextMeshPro>().text = (i+1).ToString();
            Array.Resize(ref boxpos, gameObject.transform.childCount);
            boxpos[i] = gameObject.transform.GetChild(i).position;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(mospos, new Vector3(mospos.x, mospos.y, mospos.z + 2));
    }

    #region switch
    public void erase()
    {
        mode = 1;
        pointer.GetComponent<SpriteRenderer>().enabled = true;
    }
    public void make()
    {
        mode = 0;
        pointer.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void edit()
    {
        mode = 2;
        pointer.GetComponent<SpriteRenderer>().enabled = false;
    }
    #endregion
}
