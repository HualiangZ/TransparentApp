using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    public Transform obj;
    private Collider2D collider;
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int x, int y);

    private struct Margins
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        //tempVect = tempVect * 500 * Time.deltaTime;

        obj.transform.position += tempVect;

        if(collider != null)
        {
            var targetPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            targetPos.z = transform.position.z;
            Debug.Log(targetPos.x + " : " + targetPos.y);
            SetCursorPos((int)targetPos.x, 1080 - (int)targetPos.y);
        }
        else
        {
            Move();
        }

        

        //if collition then
        //  unlink game object 
        //  addforce to gameobject
        //  move mouse to gameobject
        //  relink mouse to gameobject
        //end if
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Bullet")
        {
            collider = col.collider;
            StartCoroutine(DeathToOtherObject(col.gameObject));
        }
        
    }

    private void Move()
    {
        var targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = transform.position.z;
        transform.position = targetPos;
    }

    IEnumerator DeathToOtherObject(GameObject gameObject)
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
