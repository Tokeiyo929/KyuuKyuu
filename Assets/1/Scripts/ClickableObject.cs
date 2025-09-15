using HighlightPlus;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    [Header("˲ʱִ��")]
    [Tooltip("�������ų���ʱ�䣨�룩����Ҫ��0")]
    public float animationDuration = 0.0f;
    public bool playTimeLine = false;
    public float timeLineStartTime = 0f; // ʱ���߿�ʼʱ���
    public TimelineController timelineController;
    public HighlightEffect outline;
    public UnityEvent onShunKanClickEvents;

    [Header("��ʱִ��")]
    [Tooltip("�ӳ�ִ�� onClickEvents ��ʱ��")]
    public float delayTime = 0.5f;
    public UnityEvent onClickEvents;

    [Header("���ƫ������")]
    public bool isBias = false;
    public float cameraBiasTime = 0f;
    public UnityEvent onBiasCameraEvents;

    [Header("����")]
    public Animator animator;
    public string triggerName = "None";

    public bool deactiveAfterAnimation = false;
    public bool showUIAfterAnimation = false;
    public bool enterSceneAfterAnimation = false;
    public bool showGameObjectAfterAnimation = false;
    public bool hideGameObjectAfterAnimation = false;

    public GameObject uiPreset;  // ÿ���������UIԤ��
    public GameObject showGameObject;
    public GameObject showGameObject02;
    public GameObject hideGameObject;
    public GameObject hideGameObject02;
    public bool HasBeenClicked { get; set; } = false;



    public event Action<ClickableObject> OnClickCompleted;

    public bool IsActive { get; private set; } = false;

    public void Activate()
    {
        IsActive = true;
        if (outline != null) outline.enabled = true;
    }

    public void Deactivate()
    {
        IsActive = false;
        if (outline != null) outline.enabled = false;
    }

    public void TriggerAction()
    {
        Deactivate();

        if (animator != null)
            animator.SetTrigger(triggerName);

        MonoBehaviour mb = Camera.main.GetComponent<MonoBehaviour>();
        if (mb != null)
            mb.StartCoroutine(WaitForSeconds(animationDuration, () =>
            {
                if (showUIAfterAnimation)
                    ShowUI();

                if (showGameObjectAfterAnimation)
                {
                    showGameObject.SetActive(true);
                    if (showGameObject02 != null)
                        showGameObject02.SetActive(true);
                }

                if (hideGameObjectAfterAnimation)
                {
                    hideGameObject.SetActive(false);
                    if (hideGameObject02 != null)
                        hideGameObject02.SetActive(false);
                }

                if (enterSceneAfterAnimation)
                    EnterNewScene();

                if (deactiveAfterAnimation)
                    gameObject.SetActive(false);

                if (playTimeLine)
                    timelineController.PlayTimelineAtTime(timeLineStartTime);

                onShunKanClickEvents?.Invoke();

                OnClickCompleted?.Invoke(this);
            }));

        // �ӳ�ִ�� onClickEvents
        if (delayTime > 0)
        {
            mb.StartCoroutine(WaitForSeconds(delayTime, () =>
            {
                onClickEvents?.Invoke();
            }));
        }
        else
        {
            onClickEvents?.Invoke();
        }

        if (isBias)
        {
            mb.StartCoroutine(WaitForSeconds(cameraBiasTime, () =>
            {
                onBiasCameraEvents?.Invoke();
            }));
        }
    }

    // ������ OnUIButtonClicked ����
    public void OnUIButtonClicked()
    {
        if (!IsActive) return;

        // ���������� onClickEvents ��ע����¼�
        onClickEvents?.Invoke();

        // ������ԭ���ĵ�������߼�
        ClickManager clickManager = FindObjectOfType<ClickManager>();
        if (clickManager != null)
        {
            clickManager.TryClickFromUI(this);
        }
    }

    private System.Collections.IEnumerator WaitForSeconds(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }

    public void EnterNewScene()
    {
        // ���������ӽ����³������߼�
        // ���磺SceneManager.LoadScene("NewSceneName");
        //ProcessFlowManager.Instance.ShowNextStepButton(true);
    }

    private void ShowUI()
    {
        if (uiPreset != null)
        {
            //instantiatedUI = Instantiate(uiPrefab, UIManager.Instance.uiCanvas.transform);
            //UIManager.Instance.ShowPopup(uiPreset);
        }
    }
}
