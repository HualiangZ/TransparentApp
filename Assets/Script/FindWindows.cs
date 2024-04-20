using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using TMPro;
using UnityEngine;

public class FindWindows : MonoBehaviour
{

    public TMP_Text test;

    //find windows
    [DllImport("user32.dll")]
    public static extern int EnumWindows(WndEnumProc lpEnumFunc, int lParam);
    public delegate bool WndEnumProc(IntPtr hWnd, IntPtr lParam);
    public List<IntPtr> windows = new List<IntPtr>();

    [DllImport("user32.dll")]
    static extern bool IsWindowVisible(IntPtr hWnd);

    //============

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        test.text = EnumWindows().Count.ToString();
    }

    public static List<IntPtr> EnumWindows()
    {

        var result = new List<IntPtr>();

        EnumWindows(new WndEnumProc((hwnd, lParam) =>
        {
            if (IsWindowVisible(hwnd))
            {
                result.Add(hwnd);
                return true;
            }
            return true;
        }), 0);

        return result;
    }


}
