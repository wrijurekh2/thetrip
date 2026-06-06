using System;
using UnityEngine;
using UnityEngine.Events;

public class Button2D : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onClick;

    [SerializeField]
    private AudioClip clickSound;   // assign each button's click sound in the Inspector

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnMouseDown()
    {
        if (audioManager != null && clickSound != null)
            audioManager.PlaySFX(clickSound);

        onClick.Invoke();
    }

}
