using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<CheckpointBase> checkpoints;
    public int lastCheckpointKey;

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }

    public void SaveCheckpoint(int i)
    {
        if (i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
        }
    }

    public Vector3 GetLastCheckpointPosition()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }
}
