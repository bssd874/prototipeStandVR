using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppControl : MonoBehaviour
{
    public string Url;

    public void open()
    {
        Url = "file:///" + Application.persistentDataPath + "/Game";
        Application.OpenURL(Url);
    }
}
