using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class ChangeBorderActivity : MonoBehaviour
{
    // Start is called before the first frame update
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    public List<Collider2D> colliders = new List<Collider2D>();
    public List<Collider2D> GetColliders() { return colliders; }

    public bool col;

    private int health = 5;

    void Start()
    {
        col = true;
    }

    // Update is called once per frame
    void Update()
    {
        FindCol();
        CheckHealth();
    }

    public void FindCol()
    {
        
        foreach (Collider2D collider in colliders)
        {
            try
            {
                if (collider.gameObject.tag == "BorderList")
                {
                    col = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    break;
                }
                else
                {
                    col = true;
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            catch (Exception e)
            {
                colliders.Remove(collider);
            }

        }
    }

    public void CheckHealth()
    {
        if (health == 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (!colliders.Contains(collision) && collision.tag != "player" && collision.tag != "Bullet" && collision.tag != "Moveable")
        { 
            colliders.Add(collision); 
        }

        if (collision.gameObject.tag == "Bullet")
        {
            health--;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliders.Remove(collision);
/*        gameObject.GetComponent<SpriteRenderer>().enabled = true;*/
        //col = true;
        //gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
    }


}
