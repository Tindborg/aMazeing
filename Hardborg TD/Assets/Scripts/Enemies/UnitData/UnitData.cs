using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UnitData", order = 1)]
public class UnitData : ScriptableObject
{
   public GameObject Prefab = default;
}