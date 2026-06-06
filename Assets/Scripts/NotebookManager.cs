using System.Collections;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    [SerializeField]
    private float animationDuration;
    [SerializeField]
    private Vector2 onScreenPosition;
    [SerializeField]
    private Vector2 offScreenPosition;
    private bool isOnScreen = false;

    public bool isBusy {  get; private set; } = false;

    private void Awake()
    {
        transform.position = offScreenPosition;
    }
    public void MoveOnScreen()
    {
        if (isOnScreen) return;
        isOnScreen = true;
        StartCoroutine(Glide(onScreenPosition));
    }
    public void MoveOffScreen()
    {
        if (!isOnScreen) return;
        isOnScreen = false;
        StartCoroutine(Glide(offScreenPosition));
    }

    IEnumerator Glide(Vector2 targetPosition)
    {
        isBusy = true;
        Vector2 startPosition = transform.position;
        float stage = 0f;
        while (stage < 1f)
        {
            yield return null;
            stage += Time.deltaTime / animationDuration;
            transform.position = Vector2.Lerp(startPosition, targetPosition, stage);
        }
        transform.position = targetPosition;
        isBusy = false;
    }
}
