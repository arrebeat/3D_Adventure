using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public CheckpointManager checkpointManager { get; private set; }
    public int key = 01;
    public List<MeshRenderer> meshRenderers;
    public ParticleSystem activeParticles;
    public bool isActive = false;

    private string checkpointKey = "CheckpointKey";

    void OnValidate()
    {
        checkpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player") CheckpointCheck();
    }

    private void CheckpointCheck()
    {
        if (!isActive)
        {
            Activate();
            SaveCheckpoint();
        }
    }

    [NaughtyAttributes.Button]
    private void Activate()
    {
        isActive = true;
        
        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.material.EnableKeyword("_EMISSION");
        }

        activeParticles.Play();

        checkpointManager.DeactivateOtherCheckpoints(key);
    }

    [NaughtyAttributes.Button]
    public void Deactivate()
    {
        isActive = false;

        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.material.DisableKeyword("_EMISSION");
        }

        activeParticles.Stop();
    }

    private void SaveCheckpoint()
    {
        checkpointManager.SaveCheckpoint(key);
    }
}
