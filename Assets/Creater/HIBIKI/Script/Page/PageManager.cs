using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField]
    Vector2 _movePosOffset;


    void Start()
    {
        PageBase[] pages = GetComponentsInChildren<PageBase>();
        foreach (PageBase page in pages)
        {
            SetPage(page);
        }
    }

    void SetPage(PageBase page)
    {
        page.GetComponent<IPageInterface>().SetPagePropaty(_movePosOffset);
    }
}
