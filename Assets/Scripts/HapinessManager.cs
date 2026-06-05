using System;
using UnityEngine;

public class HapinessManager : MonoBehaviour
{
    public void RegisterActivity()
    {

    }
}

[Serializable]
public struct Day
{
    int endDayNetJoy;
    public Decision[] decisions;
}
