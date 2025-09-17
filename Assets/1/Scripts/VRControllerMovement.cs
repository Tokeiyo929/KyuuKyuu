using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Playables;  // ����PlayableDirector
using UnityEngine.Events;  // ����UnityEvent

public class VRControllerMovement : MonoBehaviour
{
    public ActionBasedController actionBasedController;  // ����XR�ֱ�������
    public PlayableDirector playableDirector;  // ����PlayableDirector���
    public bool setOrigin = false;  // �ⲿ���Ƶ�boolֵ��Ĭ��Ϊfalse
    public UnityEvent onTimesReached;  // ��times�ﵽ3ʱ�������¼�

    private Vector3 initialPosition;  // ��ʼλ�ã��趨ԭ�㣩

    [SerializeField] private float baseTime = 16.5f;  // ����ʱ������Ϊ16.5��
    [SerializeField] private float timeEnd = 17f;  // 17�봥���¼�
    [SerializeField] private float timeStart = 16f;
    [SerializeField] private int maxTimes = 3;  // �ﵽ3��ʱ�����¼�
    [SerializeField] private float speed = -5f;

    private int times = 0;  // ��¼ʱ�䵽��17��Ĵ���
    private bool hasReachedEnd = false;  // ��־λ��ȷ��ֻ��ʱ���һ�δﵽ17��ʱ��¼

    // �ⲿ���õķ�������������ԭ��
    public void SetOrigin()
    {
        initialPosition = actionBasedController.transform.position;  // ��¼��ǰλ��Ϊԭ��
        Debug.Log("Origin set at: " + initialPosition);
        setOrigin = true;
    }

    void Update()
    {
        // ����Ѿ�����ԭ�㣬���㵱ǰ��ƫ����
        if (setOrigin)
        {
            // ��ȡ��ǰ�ֱ�λ��
            Vector3 currentPosition = actionBasedController.transform.position;

            // �����άƫ������ֻ����X��Y�ᣩ
            Vector2 offset = new Vector2(currentPosition.x - initialPosition.x, currentPosition.y - initialPosition.y);

            // �����ǰ��ƫ����
            Debug.Log("Offset (X, Y): " + offset);

            // ʹ��X��ƫ����������Timeline��ʱ�䣬16��Ϊ���ģ�����ƫ��
            float normalizedTime = Mathf.Clamp(baseTime + offset.x * -5f, timeStart, timeEnd);  // ��16��Ļ�����ƫ�ƣ�ȷ��ʱ����16��17��֮��
            playableDirector.time = normalizedTime;  // ����Timeline��ʱ��
            Debug.Log("Timeline time: " + normalizedTime);

            // ֻ��ʱ���һ�ε���17��ʱ��¼�������¼�
            if (Mathf.Approximately(normalizedTime, timeEnd) && !hasReachedEnd)
            {
                hasReachedEnd = true;  // ���ñ�־λ����ֹ�ظ�����
                times++;
                Debug.Log("Times reached: " + times);

                // ���times�ﵽ3�Σ�ִ��UnityEvent
                if (times == maxTimes)
                {
                    onTimesReached.Invoke();  // ִ���¼�
                    Debug.Log("Event triggered at times = " + maxTimes);
                    times = 0;  // ��ѡ������times������
                }
            }
            else if (normalizedTime <= timeStart)
            {
                hasReachedEnd = false;  // ���ʱ��С��16�룬�����ٴδ���
            }
        }
    }

    public void ExitedEnter()
    {
        setOrigin = false;
    }
}
