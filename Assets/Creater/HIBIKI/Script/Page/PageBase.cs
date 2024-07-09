using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PageBase : MonoBehaviour
{
    Canvas canvas;
    RectTransform canvasTransform;
    RectTransform rectTransform;

    [HideInInspector]
    public PageKind _pageKind;
    public enum PageKind
    {
        Attack,
        Summon,
        Support,
        Debilitate,
    }

    [SerializeField]
    float _cost;

    Vector2 _setPos;
    bool _clickActive = false;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasTransform = canvas.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();

        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp
        };

        pointerDownEntry.callback.AddListener((data) => OnClickDown((PointerEventData)data));
        pointerUpEntry.callback.AddListener((data) => OnClickUp((PointerEventData)data));
        eventTrigger.triggers.Add(pointerDownEntry);
        eventTrigger.triggers.Add(pointerUpEntry);
    }

    public abstract void PageActivation();

    public void OnClickDown(PointerEventData eventData)
    {
        _setPos = rectTransform.anchoredPosition;

        _clickActive = true;
        StartCoroutine(SelectingSelf());
    }

    public void OnClickUp(PointerEventData eventData)
    {
        _clickActive = false;
        StopCoroutine(SelectingSelf());
        PageActivation();

        rectTransform.anchoredPosition = _setPos;
    }


    IEnumerator SelectingSelf()
    {
        while (_clickActive)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out var mousePosition);

            rectTransform.anchoredPosition = new Vector2(mousePosition.x, mousePosition.y);

            yield return new WaitForEndOfFrame();
        }

    }
}
