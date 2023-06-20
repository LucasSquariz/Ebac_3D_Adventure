using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Color color = Color.red;
    public float duration = .1f;

    private Color defaultColor;
    private Tween _currTween;

    private void Start()
    {
        defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    [Button]
    public void Flash()
    {
        if(!_currTween.IsActive()) 
            _currTween = meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
    }
}
