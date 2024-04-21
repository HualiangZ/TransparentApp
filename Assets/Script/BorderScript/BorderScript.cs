using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    public GameObject borderTop;
    public GameObject borderBottom;
    public GameObject borderLeft;
    public GameObject borderRight;
    //public Camera cam;

    public Vector2 pos;
    public Vector2 pos2;

    [DllImport("user32.dll")]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }
    RECT rct;

    // Start is called before the first frame update
    void Start()
    {
        borderTop = GameObject.Find("BorderTop");
        borderLeft = GameObject.Find("BorderLeft");
        borderRight = GameObject.Find("BorderRight");
        borderBottom = GameObject.Find("BorderBottom");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resize(Vector2 pos, Vector2 pos2)
    {
        if (borderTop != null)
        {
            borderTop.transform.localScale = new Vector2(pos.x - pos2.x, 1);
        }

        if (borderBottom != null)
        {
            borderBottom.transform.localScale = new Vector2(pos.x - pos2.x, 1);
        }

        if(borderLeft != null)
        {
            borderLeft.transform.localScale = new Vector2(1, pos.y - pos2.y);
        }

        if(borderRight != null)
        {
            borderRight.transform.localScale = new Vector2(1, pos.y - pos2.y);
        }
        
    }

    public void ReLocate(Vector2 pos, Vector2 pos2)
    {
        if (borderTop != null)
        {
            borderTop.transform.position = new Vector2(pos.x - ((pos.x - pos2.x) / 2), pos.y);
        }
        if ((borderBottom != null))
        {
            borderBottom.transform.position = new Vector2(pos.x - ((pos.x - pos2.x) / 2), pos.y - (pos.y - pos2.y) + 2);
        }
        if((borderLeft != null))
        {
            borderLeft.transform.position = new Vector2(pos.x + 2, pos.y - ((pos.y - pos2.y) / 2));
        }if((borderRight != null))
        {
            borderRight.transform.position = new Vector2(pos.x - (pos.x - pos2.x) - 2, pos.y - ((pos.y - pos2.y) / 2));
        }

    }

    public void SetPos(IntPtr hWnd)
    {

        if (GetWindowRect(hWnd, out rct))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(rct.Left, 1080 - rct.Top));
            Vector2 pos2 = Camera.main.ScreenToWorldPoint(new Vector2(rct.Right, 1080 - rct.Bottom));
            Resize(pos, pos2);
            ReLocate(pos, pos2);

        }
    }
}
