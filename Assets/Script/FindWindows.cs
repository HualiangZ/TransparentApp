using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class FindWindows : MonoBehaviour
{

    public TMP_Text test;

    public GameObject testSquare;

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

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;        
        public int Top;         
        public int Right;       
        public int Bottom;      
    }
    //============
    RECT rct;

    void Start()
    {
        windows = EnumWindows();
    }

    // Update is called once per frame
    void Update()
    {
        if(EnumWindows().Count != windows.Count)
        {
            windows = EnumWindows();
            
        }
        DrawBorder();



        /*Instantiate(testSquare);*/

        //testSquare.transform.localScale = new Vector2(rct.Right  - rct.Left  + 1, rct.Bottom  - rct.Top  + 1);
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

    private void DrawBorder()
    {
        if (GetWindowRect(windows[0], out rct))
        {
            Vector2 pos = testCamera.ScreenToWorldPoint(new Vector2(rct.Left, 1080 - rct.Top));
            testSquare.transform.position = pos;
            test.text = rct.Left + " : " + rct.Top;
        }
    }

}
