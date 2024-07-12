using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PageBase : MonoBehaviour
{
    Canvas canvas;
    RectTransform canvasTransform = default;
    RectTransform rectTransform = default;

    [SerializeField]
    TextMeshProUGUI flavorTextBox;

    [HideInInspector]
    public PageKind _pageKind = default;
    public enum PageKind
    {
        Attack,
        Summon,
        Support,
        Debilitate,
    }

    [SerializeField, Tooltip("コストの入力欄")]
    float _cost = default;

    [SerializeField, Tooltip("フレーバーテキスト欄")]
    string _flavorText;

    [Tooltip("カードが置いてあった場所")]
    Vector2 _setPos = default;
    bool _clickActive = false;

    [Tooltip("カードが発動された場所")]
    public Vector2 activePos;

    private void Awake()
    {
        //位置を取得する
        canvas = GetComponentInParent<Canvas>();
        canvasTransform = canvas.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();

        //EventTriggerをアタッチ
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry pointerDownEntry = new()
        {
            eventID = EventTriggerType.PointerDown
        };
        EventTrigger.Entry pointerUpEntry = new()
        {
            eventID = EventTriggerType.PointerUp
        };

        pointerDownEntry.callback.AddListener((data) => OnClickDown());
        pointerUpEntry.callback.AddListener((data) => OnClickUp());
        eventTrigger.triggers.Add(pointerDownEntry);
        eventTrigger.triggers.Add(pointerUpEntry);
    }

    //ページの効果が発動する
    public abstract bool PageActivation(Vector3 Pos);

    //このページが保持された時
    public void OnClickDown()
    {
        _setPos = rectTransform.anchoredPosition;

        _clickActive = true;
        StartCoroutine(SelectingSelf());

        flavorTextBox.text = _flavorText;
    }

    //ページの保持が解除された時
    public void OnClickUp()
    {
        _clickActive = false;

        Vector3 activePos = ActivePosition();

        StopCoroutine(SelectingSelf());
        if (PageActivation(activePos))
        {
            Debug.Log("発動成功");
        }
        else
        {
            Debug.Log("発動失敗");
        }

        rectTransform.anchoredPosition = _setPos;

        flavorTextBox.text = "";
    }

    //保持されている間は
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
    Vector3 ActivePosition()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 direction = (transform.position - cameraPos).normalized;

        return direction;
    }
}
