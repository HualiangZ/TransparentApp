using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Diagnostics;
using UnityEngine.AI;
using UnityEngine.XR;

public class Transparent : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    public static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);


    [DllImport("user32.dll")]
    static extern bool EnumWindows(CallBackPtr lpEnumFunc, IntPtr lParam);

    private delegate bool CallBackPtr(IntPtr hWnd, IntPtr lParam);
    private CallBackPtr callBackPtr;
    private struct Margins
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins margins);

    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    const uint LWA_COLORKEY = 0x00000001;
    private IntPtr hWnd;
    private int GWL_EXSTYLE = -20;

    //test
    [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
    ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);
    public delegate bool EnumDelegate(IntPtr hWnd, int lParam);
    //=====






    void Start()
    {
        //MessageBox(new IntPtr(0), "Hello world", "Dialog", 0);
#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

        Margins margins = new Margins{cxLeftWidth = -1};
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
        SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);


#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
