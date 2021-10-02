using UnityEngine;

public class LayerManager : MonoBehaviour
{

    #region Singleton

    public static LayerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            if (Instance != this)
            {
                Destroy(this);
            }
        }
    }

    #endregion Singleton

    public LayerMask Ground = default;
    public LayerMask Towers = default;
    public LayerMask Blockers = default;

}
