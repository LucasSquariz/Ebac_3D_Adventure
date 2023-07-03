using Items;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        [SerializeField, BoxGroup("References")] public ParticleSystem itemParticleSystem;
        [SerializeField, BoxGroup("References")] public GameObject graphicItem;
        [SerializeField, BoxGroup("References")] public Collider itemCollider;
        [SerializeField, BoxGroup("References")] public SFXType sfxType;

        [SerializeField, BoxGroup("Item setup")] public ItemType itemType;

        [SerializeField, BoxGroup("Animation setup")] public float timeToHide = .1f;

        [SerializeField, BoxGroup("Audio setup")] public AudioSource audioSource;

        [ShowNonSerializedField] private string compareTag = "Player";

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

        private void PlaySFX()
        {
            SFXPool.Instance.PlayByType(sfxType);
        }

        protected virtual void Collect()
        {
            PlaySFX();
            if (itemCollider != null) itemCollider.enabled = false;
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
            if (itemParticleSystem != null) itemParticleSystem.Play();            
            if (audioSource != null) audioSource.Play();
            ItemManager.Instance.AddByType(itemType);
        }
    }
}

