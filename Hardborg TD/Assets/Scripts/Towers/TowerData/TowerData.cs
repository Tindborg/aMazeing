using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerData", order = 1)]
public class TowerData : ScriptableObject
{

    public GameObject PreviewPrefab = default;
    public GameObject Prefab = default;
    
}
