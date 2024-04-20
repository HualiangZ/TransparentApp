using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    public Transform obj;

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

        

        var targetPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        targetPos.z = transform.position.z;
        Debug.Log(targetPos.x + " : "+  targetPos.y);
        SetCursorPos((int) targetPos.x, 1080 -(int) targetPos.y);

        var targetPos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos2.z = transform.position.z;
        //Debug.Log(targetPos2.x + " : " + targetPos2.y);
        SetCursorPos((int)targetPos2.x, 1080 - (int)targetPos2.y);
    }
}
