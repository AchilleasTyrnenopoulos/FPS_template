using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseCursorHandler 
{    
    public static void EnableCursor()
    {

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public static void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
