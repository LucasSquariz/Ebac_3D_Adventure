using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        [SerializeField, BoxGroup("References")] public SkinnedMeshRenderer mesh;
        [SerializeField, BoxGroup("References")] public Texture2D texture;

        [ShowNonSerializedField] private string shaderIdName = "_EmissionMap";
        private Texture2D _defaultTexture;

        private void Start()
        {
            _defaultTexture = (Texture2D)mesh.materials[0].GetTexture(shaderIdName);
        }

        [Button]
        private void ChangeTexture()
        {
            mesh.materials[0].SetTexture(shaderIdName, texture);
        }

        public void ChangeTexture(ClothSetup setup)
        {
            mesh.materials[0].SetTexture(shaderIdName, setup.texture);
        }

        public void ResetTexture()
        {
            mesh.materials[0].SetTexture(shaderIdName, _defaultTexture);
        }
    }
}

