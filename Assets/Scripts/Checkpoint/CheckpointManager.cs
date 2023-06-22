using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;
    public List<CheckpointBase> checkpoints;

    public bool HaveCheckpoint()
    {
        return lastCheckpointKey > 0;
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
        return checkpointToSpawn.transform.position;
    }
}
