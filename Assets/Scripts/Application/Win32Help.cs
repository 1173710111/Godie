using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
public class Win32Help
{
    private delegate bool Wndenumproc(System.IntPtr hwnd, uint lParam);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool EnumWindows(Wndenumproc lpEnumFunc, uint lParam);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern System.IntPtr GetParent(System.IntPtr hWnd);
    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(System.IntPtr hWnd, ref uint lpdwProcessId);

    [DllImport("kernel32.dll")]
    private static extern void SetLastError(uint dwErrCode);


    /// <summary>
    /// 获取当前进程的窗口句柄
    /// </summary>
    /// <returns></returns>
    public static System.IntPtr GetProcessWnd()
    {
        var ptrWnd = System.IntPtr.Zero;
        var pid = (uint)System.Diagnostics.Process.GetCurrentProcess().Id;  // 当前进程 ID  

        var bResult = EnumWindows(delegate (System.IntPtr hwnd, uint lParam)
        {
            uint id = 0;

            if (GetParent(hwnd) != System.IntPtr.Zero)
                return true;
            GetWindowThreadProcessId(hwnd, ref id);
            if (id != lParam)
                return true;
            ptrWnd = hwnd;   // 把句柄缓存起来  
            SetLastError(0);    // 设置无错误  
            return false;   // 返回 false 以终止枚举窗口  
        }, pid);

        return (!bResult && Marshal.GetLastWin32Error() == 0) ? ptrWnd : System.IntPtr.Zero;
    }

    [DllImport("imm32.dll")]
    private static extern System.IntPtr ImmGetContext(System.IntPtr hwnd);
    [DllImport("imm32.dll")]
    private static extern bool ImmGetOpenStatus(System.IntPtr himc);
    [DllImport("imm32.dll")]
    private static extern bool ImmSetOpenStatus(System.IntPtr himc, bool b);

    /// <summary>
    /// 设置输入法状态
    /// </summary>
    /// <param name="tf"></param>
    public static void SetImeEnable(bool tf)
    {
        var handle = GetProcessWnd();
        var hIme = ImmGetContext(handle);
        ImmSetOpenStatus(hIme, tf);
    }

    /// <summary>
    /// 获取输入法状态
    /// </summary>
    /// <returns></returns>
    public bool GetImeStatus()
    {
        var handle = GetProcessWnd();
        var hIme = ImmGetContext(handle);
        return ImmGetOpenStatus(hIme);
    }
}