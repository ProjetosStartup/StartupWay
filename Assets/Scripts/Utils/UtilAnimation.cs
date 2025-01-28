using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class UtilAnimation
{
    public static void ChangeScale(Transform transform,float newScale,float duration,Ease ease = Ease.OutQuad)
    {
        transform.DOScale(newScale, duration).SetEase(ease);
    }
    public static void ChangeScaleAndDesactive(Transform transform, float newScale, float duration,Ease ease = Ease.OutQuad)
    {
        transform.DOScale(newScale, duration).SetEase(ease).OnComplete( () => transform.gameObject.SetActive(false));
    }
    public static Tween SetScaleAndChangeScale(Transform transform,float oldScale ,float newScale, float duration, Ease ease = Ease.OutQuad)
    {
        transform.localScale = Vector3.one * oldScale;
        return transform.DOScale(newScale, duration).SetEase(ease);

    }

  
}
