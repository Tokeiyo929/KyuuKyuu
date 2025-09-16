using UnityEngine;
using UnityEngine.Events;

public class CollisionDetection : MonoBehaviour
{
    // ������UnityEvent������ִ�з�����ײʱ���¼�
    [Header("Collision Events")]
    public UnityEvent OnCollisionDetected;

    [SerializeField] private GameObject targetObject;    // Ŀ������

    private Collider targetCollider;
    private Collider objectCollider;

    private void Start()
    {
        // ��ȡĿ������͵�ǰ�����Collider
        targetCollider = targetObject.GetComponent<Collider>();
        objectCollider = GetComponent<Collider>();

        // Debug��Ϣ����ʼ��ʱ���Collider��Ϣ
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
        // ÿ֡�����ײ
        if (objectCollider != null && targetCollider != null && targetCollider.isTrigger)
        {
            // ����Ƿ�����ײ
            if (objectCollider.bounds.Intersects(targetCollider.bounds))
            {
                // Debug��Ϣ����ײ����ʱ���
                Debug.Log("Collision Detected between " + gameObject.name + " and " + targetObject.name);

                // ������ײ��ִ���¼�
                OnCollisionDetected.Invoke();
            }
            else
            {
                // Debug��Ϣ��û����ײʱ���
                Debug.Log("No Collision between " + gameObject.name + " and " + targetObject.name);
            }
        }
        else
        {
            // ���û���ҵ�Collider���������
            Debug.LogWarning("Collider is missing on one or both objects!");
        }
    }
}
