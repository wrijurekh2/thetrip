using System.Collections;
using UnityEngine;

public class PersonGraphic : MonoBehaviour
{
    [field: SerializeField]
    public PersonTag personTag {  get; private set; }
    [SerializeField]
    private float animationTime;
    [SerializeField]
    private Transform friendshipCircleCenter;
    [SerializeField]
    private float friendshipCircleRadius;
    [SerializeField]
    private float outerCircleRadius;
    [SerializeField]
    private float innerClampHappiness;
    [SerializeField]
    private float outerClampHappiness;
    [SerializeField]
    private Vector2 offsetDirection;

    private float h = -10;

    public void UpdateHappiness(float happiness, bool instantMove = false)
    {
        // Stop previous moves to avoid them clashing
        StopAllCoroutines();
        // Convert new happiness to a radius value
        if (happiness > 0)
        {
            // Calculate how close to the center of the circle it should be
            float ratio = Mathf.Max(0, ((innerClampHappiness - happiness) / innerClampHappiness));
            float radius = ratio * friendshipCircleRadius;
            StartCoroutine(MoveToNewRadius(radius, instantMove));
        }
        else
        {
            // Calculate how far past the bounds of the friendship circle it should be
            float absHappiness = Mathf.Abs(happiness);
            float ratio = Mathf.Min(1, absHappiness / outerClampHappiness);
            float radius = ratio * (outerCircleRadius - friendshipCircleRadius) + friendshipCircleRadius;
            StartCoroutine(MoveToNewRadius(radius, instantMove));
        }
    }

    private IEnumerator MoveToNewRadius(float radius, bool instantMove)
    {
        Vector2 oldPosition = transform.localPosition;
        Vector2 localCenterPosition = transform.parent.InverseTransformPoint(friendshipCircleCenter.position);
        Vector2 normalisedOffsetDirection = offsetDirection.normalized;
        Vector2 targetPosition = localCenterPosition + normalisedOffsetDirection * radius;
        if (instantMove)
        {
            transform.localPosition = targetPosition;
            yield break;
        }
        float stage = 0f;
        while (stage < 1f)
        {
            transform.localPosition = Vector2.Lerp(oldPosition, targetPosition, stage);
            yield return null;
            stage += Time.deltaTime / animationTime;
        }
        transform.localPosition = targetPosition;
    }
}
