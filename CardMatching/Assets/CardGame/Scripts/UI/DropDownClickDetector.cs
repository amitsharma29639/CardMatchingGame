
using JTools.Sound.Core;
using JTools.Sound.Core.Constants;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropDownClickDetector : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlayOneShot(AudioGroupConstants.GAMEPLAYSFX , AudioGroupConstants.BUTTON_CLICK, AudioGroupConstants.GAMEPLAYSFX);
    }
}


