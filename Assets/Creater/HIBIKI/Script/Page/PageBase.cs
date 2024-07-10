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

    [SerializeField]
    float _cost = default;

    [SerializeField]
    string _flavorText;

    Vector2 _setPos = default;
    bool _clickActive = false;

    private void Awake()
    {
        //�ʒu���擾����
        canvas = GetComponentInParent<Canvas>();
        canvasTransform = canvas.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();

        //EventTrigger���A�^�b�`
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

    //�y�[�W�̌��ʂ���������
    public abstract void PageActivation();

    //���̃y�[�W���ێ����ꂽ��
    public void OnClickDown()
    {
        _setPos = rectTransform.anchoredPosition;

        _clickActive = true;
        StartCoroutine(SelectingSelf());
    }

    //�y�[�W�̕ێ����������ꂽ��
    public void OnClickUp()
    {
        _clickActive = false;
        StopCoroutine(SelectingSelf());
        PageActivation();

        rectTransform.anchoredPosition = _setPos;

        flavorTextBox.text = _flavorText;
    }

    //�ێ�����Ă���Ԃ�
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