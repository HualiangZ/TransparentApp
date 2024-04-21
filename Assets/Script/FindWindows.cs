using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class FindWindows : MonoBehaviour
{

    //public TMP_Text test;

    public GameObject BorderList;

    public List<GameObject> BorderListInst =  new List<GameObject>();
    GameObject obj;
    //GameObject obj2;

    public Camera testCamera;
    //find windows
    [DllImport("user32.dll")]
    public static extern int EnumWindows(WndEnumProc lpEnumFunc, int lParam);
    public delegate bool WndEnumProc(IntPtr hWnd, IntPtr lParam);
    public List<IntPtr> windows = new List<IntPtr>();

    [DllImport("user32.dll")]
    static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    public struct RECT
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }
    //============
    RECT rct;
    //RECT rct2;

    void Start()
    {
        windows = EnumWindows();
        for (int i = 0; i < windows.Count; i++)
        {
            
            GameObject newGO = Instantiate(BorderList);
            BorderListInst.Add(newGO);
        }

        StartCoroutine(MoveBorderC());


        obj = Instantiate(BorderList);
    }

    // Update is called once per frame
    void Update()
    {

        if (EnumWindows().Count != windows.Count)
        {
            BorderListInst.Clear();
            windows = EnumWindows();
            for (int i = 0; i < windows.Count; i++)
            {
                GameObject newGO = (GameObject)Instantiate(BorderList);
                BorderListInst.Add(newGO);
            }
        }

        //MoveBorders();


        //MoveBordersCheat();


    }

    private static List<IntPtr> EnumWindows()
    {

        var result = new List<IntPtr>();
        //int style = GetWindowLong(hWnd, -16);
        EnumWindows(new WndEnumProc((hWnd, lParam) =>
        {
            if (IsWindowVisible(hWnd) && IsAppWindow(hWnd))
            {
                result.Add(hWnd);
                return true;
            }
            return true;
        }), 0);

        return result;
    }

    private static bool IsAppWindow(IntPtr hWnd)
    {
        int style = GetWindowLong(hWnd, -16); // GWL_STYLE

        // check for WS_VISIBLE and WS_CAPTION flags
        // (that the window is visible and has a title bar)
        return (style & 0x10C00000) == 0x10C00000;
    }

    private void MoveBordersCheat()
    {
        if (GetWindowRect(EnumWindows()[0], out rct))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(rct.Left, 1080 - rct.Top));
            Vector2 pos2 = Camera.main.ScreenToWorldPoint(new Vector2(rct.Right, 1080 - rct.Bottom));
            obj.GetComponent<BorderScript>().Resize(pos, pos2);
            obj.GetComponent<BorderScript>().ReLocate(pos, pos2);

        }
    }
/*    private void MoveBordersCheat2()
    {
        if (GetWindowRect(EnumWindows()[1], out rct2))
        {
            Debug.Log("True");

            Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(rct2.Left, 1080 - rct2.Top));
            Vector2 pos2 = Camera.main.ScreenToWorldPoint(new Vector2(rct2.Right, 1080 - rct2.Bottom));
            *//*obj2.GetComponent<BorderScript>().Resize(pos, pos2);
            obj2.GetComponent<BorderScript>().ReLocate(pos, pos2);*//*
            obj2.GetComponent<BorderScript>().pos = pos;
            obj2.GetComponent<BorderScript>().pos2 = pos2;

        }
    }*/

    private void MoveBorders()
    {
        for (int i = 0; i<windows.Count; i++)
        {
            if (GetWindowRect(windows[i], out rct))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(rct.Left, 1080 - rct.Top));
                Vector2 pos2 = Camera.main.ScreenToWorldPoint(new Vector2(rct.Right, 1080 - rct.Bottom));
                BorderListInst[i].GetComponent<BorderScript>().Resize(pos, pos2);
                BorderListInst[i].GetComponent<BorderScript>().ReLocate(pos, pos2);

            }
        }

    }

    IEnumerator MoveBorderC()
    {
        for(; ; )
        {
            for (int i = 0; i < windows.Count; i++)
            {
                if (GetWindowRect(windows[i], out rct))
                {
                    Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(rct.Left, 1080 - rct.Top));
                    Vector2 pos2 = Camera.main.ScreenToWorldPoint(new Vector2(rct.Right, 1080 - rct.Bottom));
                    BorderListInst[i].GetComponent<BorderScript>().Resize(pos, pos2);
                    BorderListInst[i].GetComponent<BorderScript>().ReLocate(pos, pos2);

                }
                yield return new WaitForSeconds(0.2f);
            }

        }
    }


}
