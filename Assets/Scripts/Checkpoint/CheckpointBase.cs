using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 1;

    private string checkpointKey = "CheckpointKey";
    private bool _checkpointActivated = false;

    private void Start()
    {
        TurnItOff();
    }

    private void OnTriggerEnter(Collider other)
    {        
        if(!_checkpointActivated && other.transform.tag == "Player") VerifyCheckpoint();
    }

    private void VerifyCheckpoint()
    {
        TurnItOn();
        SaveCheckpoint();
    }

    [Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);        
    }

    [Button]
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
    }

    private void SaveCheckpoint()
    {
        if(PlayerPrefs.GetInt(checkpointKey, 0) > key) PlayerPrefs.SetInt(checkpointKey, key);
        _checkpointActivated = true;
    }

}
