using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
    public static string username;
    public static int time;

    public static bool LoggedIn { get { return username != null; } }

    public static void Logout()
    {
        username = null;
        time = 0;
    }
}

