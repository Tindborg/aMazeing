using UnityEngine;
using UnityEngine.Events;

public class PlayerBuilder : MonoBehaviour
{

    public static PlayerBuilder Instance;

    public Vector3Event UE_OnUpdate;
    public Vector3Event UE_OnLeftClick;
    public Vector3Event UE_OnRightClick;

    [Header("DEBUG")]

    [SerializeField]
    private TowerData _DebugTowerData = default;

    private TowerPreview _activeTowerPreview = null;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("Starting build preview");
            StartBuild(_DebugTowerData);
        }

        //

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerManager.Instance.Ground))
        {
            UE_OnUpdate.Invoke(hit.point);

            if (Input.GetMouseButtonDown(0)) UE_OnLeftClick.Invoke(hit.point);
            if (Input.GetMouseButtonDown(1)) UE_OnRightClick.Invoke(hit.point);

            //print("Raycast onhit");
        }/* else
        {
            //print("Raycast miss");
        }*/
    }

    public void StartBuild(TowerData towerData)
    {
        if (_activeTowerPreview == null)
        {
            Destroy(_activeTowerPreview);

            _activeTowerPreview = Instantiate(towerData.PreviewPrefab).GetComponent<TowerPreview>();
            _activeTowerPreview.MyTowerData = towerData;

            UE_OnUpdate.AddListener(_activeTowerPreview.OnUpdate);
            UE_OnLeftClick.AddListener(_activeTowerPreview.OnLeftClick);
        }
    }

}
