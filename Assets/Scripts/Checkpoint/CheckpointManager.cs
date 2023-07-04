using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = -1;
    public List<CheckpointBase> checkpoints;

    private void Start()
    {
        lastCheckpointKey = SaveManager.Instance.GetSetup().lastCheckpoint;
    }

    public bool HaveCheckpoint()
    {
        return lastCheckpointKey >= 0;
    }

    public void SaveCheckpoint(int checkpointKey)
    {
        if(checkpointKey > lastCheckpointKey)
        {
            lastCheckpointKey = checkpointKey;
        }
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpointToSpawn = checkpoints.Find(i => i.key == lastCheckpointKey);
        Debug.Log(checkpointToSpawn);
        return checkpointToSpawn?.transform.position ?? Player.Instance.transform.position;
    }
    public Vector3 GetPositionFromLastCheckpoint(int checkpointKey)
    {
        var checkpointToSpawn = checkpoints.Find(i => i.key == checkpointKey);
        Debug.Log(checkpointToSpawn);
        return checkpointToSpawn?.transform.position ?? Player.Instance.transform.position;
    }

}
