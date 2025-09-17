using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using FSM;  // ���� UI �����ռ�

public class CompletionAnimation : MonoBehaviour
{
    public Machine machine; // ����״̬��
    [Header("��������")]
    public TMPro.TextMeshProUGUI completionText;
    public TMPro.TextMeshProUGUI buttonText;
    public Image image; // ���� Image ����
    public Button nextButton; // ���� Button ����

    public HandUI handUI;

    void OnEnable()
    {
        completionText.enabled = false; // ��ʼʱ�����ı�
        nextButton.gameObject.SetActive(false); // ��ʼʱ���ذ�ť
    }

    public void SetTitleText(string str)
    {
        completionText.text = str;
    }
    public void SetButtonText(string str)
    {
        buttonText.text = str;
    }
    public void StartCompletionAnimation(string str)
    {
        // �� image �����Ŵ� (0,0,0) �� (1,1,1)
        image.transform.localScale = new Vector3(1, 0, 1);
        image.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            // �� image ������ɺ󣬲����ı�����
            completionText.enabled = true;
            completionText.transform.localScale = Vector3.zero;
            completionText.DOFade(1, 0.5f).SetDelay(0.2f);  // �ı�����
            completionText.transform.DOScale(Vector3.one, 0.5f).SetDelay(0.2f).OnComplete(() =>
            {
                // ���ı�������ɺ�������ť����
                nextButton.gameObject.SetActive(true); // ��ʾ��ť
                nextButton.transform.localScale = Vector3.zero; // ���ð�ť��ʼ����Ϊ 0
                nextButton.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
                {
                    // ��Ӱ�ť�����¼�
                    nextButton.onClick.RemoveAllListeners(); 
                    nextButton.onClick.AddListener(() =>EnterNextModule(str));
                });
            });
        });
    }
    public void StartCompletionAnimationNoStr(int showIndex)
    {
        // �� image �����Ŵ� (0,0,0) �� (1,1,1)
        image.transform.localScale = new Vector3(1, 0, 1);
        image.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            // �� image ������ɺ󣬲����ı�����
            completionText.enabled = true;
            completionText.transform.localScale = Vector3.zero;
            completionText.DOFade(1, 0.5f).SetDelay(0.2f);  // �ı�����
            completionText.transform.DOScale(Vector3.one, 0.5f).SetDelay(0.2f).OnComplete(() =>
            {
                // ���ı�������ɺ�������ť����
                nextButton.gameObject.SetActive(true); // ��ʾ��ť
                nextButton.transform.localScale = Vector3.zero; // ���ð�ť��ʼ����Ϊ 0
                nextButton.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
                {
                    // ��Ӱ�ť�����¼�
                    nextButton.onClick.RemoveAllListeners();
                    nextButton.onClick.AddListener(() => {
                        handUI.ShowText(showIndex);
                        this.gameObject.SetActive(false);
                    });
                    //nextButton.onClick.AddListener(() => EnterNextModule(str));
                });
            });
        });
    }

    // ������һģ��ķ���
    private void EnterNextModule(string str)
    {
        machine.TryToChangeStateByName(str);
        this.gameObject.SetActive(false); // ���ص�ǰ��ͨ�ض�������
    }
}
