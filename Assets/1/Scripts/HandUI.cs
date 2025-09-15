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

    

    // ��������FirstState���г�ʼ��
    void Awake()
    {
        handText = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();

        // ��ʼ�ĵ��붯��
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, 1f);
    }

    private void Update()
    {
        // ʼ�ճ��������
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    // ��ʾָ���������ı�
    public void ShowText(int index)
    {
        // �����Ч����
        if (index < 0 || index >= steps.Count) return;

        // ��ȡ�����ı�
        string textToShow = steps[index].instruction;

        // ������ǰ�ı�
        handText.DOFade(0f, 0.5f).OnComplete(() =>
        {
            // �����ı�����
            handText.text = textToShow;

            // �������ı�
            handText.DOFade(1f, 0.5f);

            // ����ǵ����ڶ������裬����UI
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
    //    // ������ɵ����һ��
    //    int nextStepIndex = GetNextCompletedStepIndex(index);

    //    if (nextStepIndex >= 0 && nextStepIndex < steps.Count)
    //    {
    //        handText.text = steps[nextStepIndex].instruction;
    //    }
    //    else
    //    {
    //        // ���û���ҵ���Ч���裬��ָ�ԭ�ı�
    //        handText.text = currentText;
    //    }
    //}

    //private int GetNextCompletedStepIndex(int index)
    //{
    //    // ��ȡ��һ������ɲ��������
    //    for (int i = 0; i < steps.Count; i++)
    //    {
    //        if (steps[i].isCompleted && i >= index)
    //        {
    //            return i + 1; // ��ȡ��һ�����������
    //        }
    //    }
    //    return -1; // ��Ч����
    //}
}
