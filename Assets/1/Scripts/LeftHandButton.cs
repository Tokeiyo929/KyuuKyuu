using UnityEngine;

public class LeftHandButton : MonoBehaviour
{
    private Transform cameraOffset;
    private bool isStand = true;

    private void Awake()
    {
        if (InputEvent.Instance != null)
        {
            InputEvent.Instance.onLeftAXButtonUp += LeftAXButtonUp;
            InputEvent.Instance.onLeftAXButtonEnter += LeftAXButtonEnter;
            InputEvent.Instance.onLeftAXButtonDown += LeftAXButtonDown;
        }
        else
        {
            Debug.LogError("InputEvent.Instance 为 null");
        }
    }

    private void Start()
    {
        GameObject cameraOffsetObj = GameObject.Find("Camera Offset");
        if (cameraOffsetObj != null)
        {
            cameraOffset = cameraOffsetObj.transform;
        }
        else
        {
            Debug.LogError("找不到CameraOffset对象！");
        }
    }

    private void OnDestroy()
    {
        if (InputEvent.Instance != null)
        {
            InputEvent.Instance.onLeftAXButtonUp -= LeftAXButtonUp;
            InputEvent.Instance.onLeftAXButtonEnter -= LeftAXButtonEnter;
            InputEvent.Instance.onLeftAXButtonDown -= LeftAXButtonDown;
        }
    }

    void LeftAXButtonDown()
    {
        Debug.Log("左手柄A键按下中···");
    }

    void LeftAXButtonEnter()
    {
        Debug.Log("按下左手柄A键");
        if (cameraOffset != null)
        {
            cameraOffset.localPosition = isStand ? new Vector3(0, -0.7f, 0) : new Vector3(0, 0, 0);
            isStand = !isStand;
        }
    }

    void LeftAXButtonUp()
    {
        Debug.Log("抬起左手柄A键");
    }
}