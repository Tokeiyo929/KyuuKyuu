using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Playables;  // 引用PlayableDirector
using UnityEngine.Events;  // 引用UnityEvent

public class VRControllerMovement : MonoBehaviour
{
    public ActionBasedController actionBasedController;  // 引用XR手柄控制器
    public PlayableDirector playableDirector;  // 引用PlayableDirector组件
    public bool setOrigin = false;  // 外部控制的bool值，默认为false
    public UnityEvent onTimesReached;  // 当times达到3时触发的事件

    private Vector3 initialPosition;  // 初始位置（设定原点）

    [SerializeField] private float baseTime = 16.5f;  // 基础时间设置为16.5秒
    [SerializeField] private float timeEnd = 17f;  // 17秒触发事件
    [SerializeField] private float timeStart = 16f;
    [SerializeField] private int maxTimes = 3;  // 达到3次时触发事件
    [SerializeField] private float speed = -5f;

    private int times = 0;  // 记录时间到达17秒的次数
    private bool hasReachedEnd = false;  // 标志位，确保只在时间第一次达到17秒时记录

    // 外部调用的方法，用来设置原点
    public void SetOrigin()
    {
        initialPosition = actionBasedController.transform.position;  // 记录当前位置为原点
        Debug.Log("Origin set at: " + initialPosition);
        setOrigin = true;
    }

    void Update()
    {
        // 如果已经设置原点，计算当前的偏移量
        if (setOrigin)
        {
            // 获取当前手柄位置
            Vector3 currentPosition = actionBasedController.transform.position;

            // 计算二维偏移量（只计算X和Y轴）
            Vector2 offset = new Vector2(currentPosition.x - initialPosition.x, currentPosition.y - initialPosition.y);

            // 输出当前的偏移量
            Debug.Log("Offset (X, Y): " + offset);

            // 使用X轴偏移量来更新Timeline的时间，16秒为中心，左右偏移
            float normalizedTime = Mathf.Clamp(baseTime + offset.x * -5f, timeStart, timeEnd);  // 在16秒的基础上偏移，确保时间在16到17秒之间
            playableDirector.time = normalizedTime;  // 更新Timeline的时间
            Debug.Log("Timeline time: " + normalizedTime);

            // 只在时间第一次到达17秒时记录并触发事件
            if (Mathf.Approximately(normalizedTime, timeEnd) && !hasReachedEnd)
            {
                hasReachedEnd = true;  // 设置标志位，防止重复触发
                times++;
                Debug.Log("Times reached: " + times);

                // 如果times达到3次，执行UnityEvent
                if (times == maxTimes)
                {
                    onTimesReached.Invoke();  // 执行事件
                    Debug.Log("Event triggered at times = " + maxTimes);
                    times = 0;  // 可选：重置times计数器
                }
            }
            else if (normalizedTime <= timeStart)
            {
                hasReachedEnd = false;  // 如果时间小于16秒，允许再次触发
            }
        }
    }

    public void ExitedEnter()
    {
        setOrigin = false;
    }
}
