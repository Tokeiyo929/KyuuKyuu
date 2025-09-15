using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class HandUI : MonoBehaviour
{
    [System.Serializable]
    public class StepData
    {
        public string instruction;
        public bool isCompleted = false;
    }

    [Header("Step Data")]
    public List<StepData> steps;

    [Header("UI Elements")]
    private TextMeshProUGUI handText;
    private CanvasGroup canvasGroup;
    private int currentStepIndex;

    

    // 必须早于FirstState进行初始化
    void Awake()
    {
        handText = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();

        // 初始的淡入动画
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, 1f);
    }

    private void Update()
    {
        // 始终朝向摄像机
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    // 显示指定索引的文本
    public void ShowText(int index)
    {
        // 检查有效索引
        if (index < 0 || index >= steps.Count) return;

        // 获取步骤文本
        string textToShow = steps[index].instruction;

        // 淡出当前文本
        handText.DOFade(0f, 0.5f).OnComplete(() =>
        {
            // 更新文本内容
            handText.text = textToShow;

            // 淡入新文本
            handText.DOFade(1f, 0.5f);

            // 如果是倒数第二个步骤，隐藏UI
            //if (index == steps.Count - 2)
            //{
            //    canvasGroup.DOFade(0f, 1f).OnComplete(() =>
            //    {
            //        gameObject.SetActive(false);
            //    });
            //}
        });
    }

    //private void UpdateText(int index, string currentText)
    //{
    //    // 查找完成的最后一步
    //    int nextStepIndex = GetNextCompletedStepIndex(index);

    //    if (nextStepIndex >= 0 && nextStepIndex < steps.Count)
    //    {
    //        handText.text = steps[nextStepIndex].instruction;
    //    }
    //    else
    //    {
    //        // 如果没有找到有效步骤，则恢复原文本
    //        handText.text = currentText;
    //    }
    //}

    //private int GetNextCompletedStepIndex(int index)
    //{
    //    // 获取下一个已完成步骤的索引
    //    for (int i = 0; i < steps.Count; i++)
    //    {
    //        if (steps[i].isCompleted && i >= index)
    //        {
    //            return i + 1; // 获取下一个步骤的索引
    //        }
    //    }
    //    return -1; // 无效索引
    //}
}
