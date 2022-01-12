using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class BaseButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RedPoint redPoint;

    Vector3 scale = new Vector3(0.9f, 0.9f, 0);//µã»÷Ëõ·Å±ÈÀý

    public bool isScale = true;
    public bool isPlaySound = true;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!button.interactable) return;
        //TODO ÉùÒô
        //if (isPlaySound) 
        //    SoundManager.Instance.PlayButton1();
        if (isScale)
            iTween.ScaleTo(gameObject, scale, 0.2f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!button.interactable) return;
        if (isScale)
            iTween.ScaleTo(gameObject, Vector3.one, 0.2f);
    }
}
