using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    #region Singleton

    public static CheckpointManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(this);
            }
        }
    }

    #endregion Singleton

    public List<Transform> Checkpoints;

    public Transform GetNextCheckpoint( Transform current_checkpoint )
    {
        return Checkpoints[0];
    }
}
