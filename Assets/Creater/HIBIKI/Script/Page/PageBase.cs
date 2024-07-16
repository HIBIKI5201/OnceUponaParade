using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PageBase : MonoBehaviour, IPageInterface
{
    Canvas canvas;
    RectTransform canvasTransform = default;
    RectTransform rectTransform = default;

    [Header("カード情報")]

    Vector2 _movePosOffset;

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

    static float _costCounter;

    [SerializeField, Tooltip("コストの入力欄")]
    float _cost = default;

    [SerializeField, Tooltip("フレーバーテキスト欄")]
    string _flavorText;

    [Tooltip("カードが置いてあった場所")]
    Vector2 _setPos = default;
    bool _clickActive = false;

    [HideInInspector, Tooltip("カードが発動された場所")]
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
    public abstract bool PageActivation(Vector3 activePoint);

    //このページが保持された時
    public void OnClickDown()
    {
        _setPos = rectTransform.anchoredPosition;

        _clickActive = true;
        StartCoroutine(SelectingSelf());

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x / 3, rectTransform.sizeDelta.y / 3);

        flavorTextBox.text = _flavorText;
    }

    //ページの保持が解除された時
    public void OnClickUp()
    {
        _clickActive = false;
        StopCoroutine(SelectingSelf());

        if (PageActivation(ActivePosition()))
        {
            Debug.Log("発動成功");

            _costCounter -= _cost;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("発動失敗");

            rectTransform.anchoredPosition = _setPos;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * 3, rectTransform.sizeDelta.y * 3);
        }

        flavorTextBox.text = "";
    }

    //保持されている間は
    IEnumerator SelectingSelf()
    {
        while (_clickActive)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasTransform,
            Input.mousePosition + (Vector3)_movePosOffset,
            canvas.worldCamera,
            out var mousePosition);

            rectTransform.anchoredPosition = new Vector2(mousePosition.x, mousePosition.y);

            yield return new WaitForEndOfFrame();
        }

    }
    Vector3 ActivePosition()
    {
        //マウスの場所
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasTransform, Input.mousePosition, canvas.worldCamera, out Vector3 mousePosition);

        Vector3 direction = (mousePosition - Camera.main.transform.position).normalized;

        Physics.Raycast(mousePosition, direction, out RaycastHit activePos, Mathf.Infinity, LayerMask.GetMask("Ground"));


        return activePos.point;
    }

    void IPageInterface.SetPagePropaty(Vector2 movePosOffset)
    {
        _movePosOffset = movePosOffset;
    }
}
