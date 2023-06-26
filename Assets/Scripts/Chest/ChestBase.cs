using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    [SerializeField, BoxGroup("References")] public Animator animator;
    [SerializeField, BoxGroup("References")] public GameObject notification;

    [SerializeField, BoxGroup("Animation setup")] public float tweenDuration = .2f;
    [SerializeField, BoxGroup("Animation setup")] public float startScale;
    [SerializeField, BoxGroup("Animation setup")] public Ease tweenEase = Ease.OutBack;

    [ShowNonSerializedField] private string triggerOpen = "Open";

    private void Start()
    {
        startScale = notification.transform.localScale.x;
        HideNotification();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.transform.GetComponent<Player>();
        if(player != null)
        {
            ShowNotification();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.transform.GetComponent<Player>();
        if (player != null)
        {
            HideNotification();
        }
    }

    [Button]
    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, tweenDuration);
    }

    [Button]
    private void HideNotification()
    {
        notification.SetActive(false);
    }
}
