using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PageBase : MonoBehaviour, IPageInterface
{
    Canvas canvas;
    RectTransform canvasTransform = default;
    RectTransform rectTransform = default;

    [Header("�J�[�h���")]

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

    [SerializeField, Tooltip("�R�X�g�̓��͗�")]
    float _cost = default;

    [SerializeField, Tooltip("�t���[�o�[�e�L�X�g��")]
    string _flavorText;

    [Tooltip("�J�[�h���u���Ă������ꏊ")]
    Vector2 _setPos = default;
    bool _clickActive = false;

    [HideInInspector, Tooltip("�J�[�h���������ꂽ�ꏊ")]
    public Vector2 activePos;

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
    public abstract bool PageActivation(Vector3 activePoint);

    //���̃y�[�W���ێ����ꂽ��
    public void OnClickDown()
    {
        _setPos = rectTransform.anchoredPosition;

        _clickActive = true;
        StartCoroutine(SelectingSelf());

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x / 3, rectTransform.sizeDelta.y / 3);

        flavorTextBox.text = _flavorText;
    }

    //�y�[�W�̕ێ����������ꂽ��
    public void OnClickUp()
    {
        _clickActive = false;
        StopCoroutine(SelectingSelf());

        if (PageActivation(ActivePosition()))
        {
            Debug.Log("��������");

            _costCounter -= _cost;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("�������s");

            rectTransform.anchoredPosition = _setPos;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * 3, rectTransform.sizeDelta.y * 3);
        }

        flavorTextBox.text = "";
    }

    //�ێ�����Ă���Ԃ�
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
        //�}�E�X�̏ꏊ
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
