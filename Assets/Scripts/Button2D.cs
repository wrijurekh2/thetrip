using System;
using UnityEngine;
using UnityEngine.Events;

public class Button2D : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onClick;

    private void OnMouseDown()
    {
        onClick.Invoke();
    }
}
