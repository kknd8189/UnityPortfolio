using UnityEngine;
using UnityEngine.EventSystems;

public class SynergyUI : MonoBehaviour, IPointerClickHandler
{
    public int SynergyNum;
    public int LocatedSequence = -1;

    public PointerEventData.InputButton btn = PointerEventData.InputButton.Right;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == btn)
        {
            UIManager.Instance.ExplainSynergy(SynergyNum);
        }
    }
}