using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonControl : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [field: SerializeField] public Animator EXanimator;
    bool show;

    private void Start()
    {
        show = false;
    }
    public void clickButton()
    {
        if (show) ShowEX();
        else HideEx();
    }
    private void ShowEX()
    {
        show = true;
        EXanimator.CrossFadeInFixedTime("Explan", 4f);
    }
    private void HideEx()
    {
        show = false;
        EXanimator.CrossFadeInFixedTime("Explan1", 4f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowEX();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideEx();
    }
}
