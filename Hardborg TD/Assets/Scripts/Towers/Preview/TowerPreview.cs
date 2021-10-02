using UnityEngine;

public class TowerPreview : MonoBehaviour
{

    [SerializeField]
    private int SideLength = 2;

    [SerializeField]
    private MeshRenderer[] _MeshRenderers = default;

    [SerializeField]
    private Material _MatValid = default;
    [SerializeField]
    private Material _MatInvalid = default;

    //

    [HideInInspector]
    public TowerData MyTowerData = null;

    //

    private void Awake()
    {
        CacheMeshRenderers();
        SetMaterial(_MatValid);
    }

    private Vector3 GetGridPosition(Vector3 worldPos)
    {
        switch (SideLength)
        {
            case 1:
                return new Vector3(Mathf.FloorToInt(worldPos.x), 0f, Mathf.FloorToInt(worldPos.z));
            case 2:
                return new Vector3(Mathf.RoundToInt(worldPos.x), 0f, Mathf.RoundToInt(worldPos.z));
        }

        Debug.LogError("No handler for side length " + SideLength, this);
        return Vector3.zero;
    }

    public void OnUpdate(Vector3 worldMousePos)
    {
        transform.position = GetGridPosition(worldMousePos);

        if (Physics.Raycast(transform.position + Vector3.up * 10f, Vector3.down, 20f, LayerManager.Instance.Blockers))
        {
            SetMaterial(_MatInvalid);
        } else
        {
            SetMaterial(_MatValid);
        }
    }

    public void OnLeftClick(Vector3 worldMousePos)
    {
        Instantiate(MyTowerData.Prefab, GetGridPosition(worldMousePos), Quaternion.identity);

        Destroy(gameObject);
    }

    private void SetMaterial(Material mat)
    {
        foreach (MeshRenderer ren in _MeshRenderers)
        {
            ren.sharedMaterial = mat;
        }
    }

    public void CacheMeshRenderers()
    {
        _MeshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

}
