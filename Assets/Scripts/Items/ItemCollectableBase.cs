using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem itemParticleSystem;
    public float timeToHide = .1f;
    public GameObject graphicItem;

    [SerializeField, BoxGroup("Audio setup")] public AudioSource audioSource;

    private void Awake()
    {
        if (itemParticleSystem != null)
        {
            itemParticleSystem.transform.SetParent(null);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }
    protected virtual void Collect()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideObject", timeToHide);
        OnCollect();
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if (itemParticleSystem != null)
        {
            itemParticleSystem.Play();
        }
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
