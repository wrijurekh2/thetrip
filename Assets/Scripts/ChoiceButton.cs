using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ChoiceButton : MonoBehaviour
{
    [HideInInspector] public Activity activity;
    [HideInInspector] public ChoiceListController controller;

    private TextMeshProUGUI label;
    private SpriteRenderer spriteRenderer;
    private bool isSelected = false;
    private bool isDisabled = false;

    void Awake()
    {
        label = GetComponentInChildren<TextMeshProUGUI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(Activity activity, ChoiceListController controller)
    {
        this.activity = activity;
        this.controller = controller;
        if (label != null)
            label.text = activity.writtenDescription;
    }

    void OnMouseDown()
    {
        if (isDisabled || isSelected) return;
        controller.OnChoiceClicked(this);
    }

    void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(
                UnityEngine.InputSystem.Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (!isHovering) OnHoverEnter();
                isHovering = true;
            }
            else
            {
                if (isHovering) OnHoverExit();
                isHovering = false;
            }
        }   

    bool isHovering = false;

    void OnHoverEnter()
    {
        if (isDisabled || isSelected) return;
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(0.82f, 0.65f, 0.4f, 0.4f);
    }

    void OnHoverExit()
    {
        if (isDisabled || isSelected) return;
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        if (spriteRenderer != null)
            spriteRenderer.color = selected
                ? new Color(1f, 0.85f, 0.5f, 0.5f)
                : new Color(1f, 1f, 1f, 0f);
        if (label != null)
            label.text = selected
                ? $"<s>{activity.writtenDescription}</s>"
                : activity.writtenDescription;
    }

    public void SetDisabled(bool disabled)
    {
        isDisabled = disabled;
        if (label != null)
            label.color = disabled
                ? new Color(0.5f, 0.5f, 0.5f, 0.4f)
                : new Color(0.24f, 0.13f, 0.02f, 1f);
    }
}