using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public Color color = Color.red;
    public float duration = .1f;
    public string colorParameter = "_EmissionColor";

    private Tween _currTween;

    private void OnValidate()
    {
        if(meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        if(skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }    

    [Button]
    public void Flash()
    {
        if(meshRenderer != null && (_currTween == null || _currTween.IsComplete())) 
        {
            _currTween = meshRenderer.material.DOColor(color, colorParameter, duration).SetLoops(2, LoopType.Yoyo).SetAutoKill(false);
        }   
            

        if (skinnedMeshRenderer != null && (_currTween == null || _currTween.IsComplete()))
        {
            _currTween = skinnedMeshRenderer.material.DOColor(color, colorParameter, duration).SetLoops(2, LoopType.Yoyo).SetAutoKill(false);
        }          

    }
}
