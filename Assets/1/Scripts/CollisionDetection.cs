using UnityEngine;
using UnityEngine.Events;

public class CollisionDetection : MonoBehaviour
{
    // 公开的UnityEvent，用于执行发生碰撞时的事件
    [Header("Collision Events")]
    public UnityEvent OnCollisionDetected;

    [SerializeField] private GameObject targetObject;    // 目标物体

    private Collider targetCollider;
    private Collider objectCollider;

    private void Start()
    {
        // 获取目标物体和当前物体的Collider
        targetCollider = targetObject.GetComponent<Collider>();
        objectCollider = GetComponent<Collider>();

        // Debug信息：初始化时输出Collider信息
        if (targetCollider != null)
        {
            Debug.Log("Target Object Collider Initialized: " + targetCollider);
        }
        else
        {
            Debug.LogWarning("Target Object does not have a Collider!");
        }

        if (objectCollider != null)
        {
            Debug.Log("Current Object Collider Initialized: " + objectCollider);
        }
        else
        {
            Debug.LogWarning("Current Object does not have a Collider!");
        }
    }

    private void Update()
    {
        // 每帧检测碰撞
        if (objectCollider != null && targetCollider != null && targetCollider.isTrigger)
        {
            // 检查是否发生碰撞
            if (objectCollider.bounds.Intersects(targetCollider.bounds))
            {
                // Debug信息：碰撞发生时输出
                Debug.Log("Collision Detected between " + gameObject.name + " and " + targetObject.name);

                // 发生碰撞，执行事件
                OnCollisionDetected.Invoke();
            }
            else
            {
                // Debug信息：没有碰撞时输出
                Debug.Log("No Collision between " + gameObject.name + " and " + targetObject.name);
            }
        }
        else
        {
            // 如果没有找到Collider，输出警告
            Debug.LogWarning("Collider is missing on one or both objects!");
        }
    }
}
