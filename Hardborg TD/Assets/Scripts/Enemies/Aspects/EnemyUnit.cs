using UnityEngine;
using UnityEngine.Events;

public class EnemyUnit : MonoBehaviour
{
    public Brain MyBrain;
    public Navigator MyNavigator;
    public Health MyHealth;
    public UnityEvent UE_OnRemove;

    public void Remove()
    {
        UE_OnRemove.Invoke();
        Destroy(gameObject);
    }
}
