using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스가 버튼 위에 올려질 때 호출되는 이벤트
        SoundManager.Instance.PlaySFX("Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스가 버튼에서 벗어날 때 호출되는 이벤트
    }
}
