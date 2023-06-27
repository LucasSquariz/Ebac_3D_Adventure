using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChanger : MonoBehaviour
{
    [SerializeField, BoxGroup("References")] public SkinnedMeshRenderer mesh;
    [SerializeField, BoxGroup("References")] public Texture2D texture;

    [ShowNonSerializedField] private string shaderIdName = "_EmissionMap";

    [Button]
    private void ChangeTexture()
    {
        mesh.materials[0].SetTexture(shaderIdName, texture);
    }
}
