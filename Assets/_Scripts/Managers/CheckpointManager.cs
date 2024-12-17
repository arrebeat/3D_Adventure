using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public PlayerController_Astronaut player { get; private set; }
    public List<CheckpointBase> checkpoints;
    public int lastCheckpointKey;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController_Astronaut>();
    }

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }

    public void SaveCheckpoint(int i)
    {
        lastCheckpointKey = i;
        SaveManager.instance.SaveLastCheckpoint(i);
        SaveManager.instance.SaveCurrentHp(player.healthBase.CurrentHp());
    }

    public Vector3 GetLastCheckpointPosition()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpointKey);
        return checkpoint.transform.position;
    }

    public Vector3 GetLastCheckpointPosition(bool load)
    {
        var checkpoint = checkpoints.Find(i => i.key == SaveManager.instance.saveSetup.lastCheckpoint);
        return checkpoint.transform.position;
    }

    public void DeactivateOtherCheckpoints(int key)
    {
        foreach (var checkpoint in checkpoints)
        {
            if (checkpoint.key != key) checkpoint.Deactivate();
        }
    }
}
