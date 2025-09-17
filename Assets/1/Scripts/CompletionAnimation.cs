using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using FSM;  // 引入 UI 命名空间

public class CompletionAnimation : MonoBehaviour
{
    public Machine machine; // 引用状态机
    [Header("动画部分")]
    public TMPro.TextMeshProUGUI completionText;
    public TMPro.TextMeshProUGUI buttonText;
    public Image image; // 新增 Image 变量
    public Button nextButton; // 新增 Button 变量

    public HandUI handUI;

    void OnEnable()
    {
        completionText.enabled = false; // 初始时隐藏文本
        nextButton.gameObject.SetActive(false); // 初始时隐藏按钮
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
        // 让 image 的缩放从 (0,0,0) 到 (1,1,1)
        image.transform.localScale = new Vector3(1, 0, 1);
        image.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            // 当 image 动画完成后，播放文本动画
            completionText.enabled = true;
            completionText.transform.localScale = Vector3.zero;
            completionText.DOFade(1, 0.5f).SetDelay(0.2f);  // 文本淡入
            completionText.transform.DOScale(Vector3.one, 0.5f).SetDelay(0.2f).OnComplete(() =>
            {
                // 在文本缩放完成后，启动按钮动画
                nextButton.gameObject.SetActive(true); // 显示按钮
                nextButton.transform.localScale = Vector3.zero; // 设置按钮初始缩放为 0
                nextButton.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
                {
                    // 添加按钮监听事件
                    nextButton.onClick.RemoveAllListeners(); 
                    nextButton.onClick.AddListener(() =>EnterNextModule(str));
                });
            });
        });
    }
    public void StartCompletionAnimationNoStr(int showIndex)
    {
        // 让 image 的缩放从 (0,0,0) 到 (1,1,1)
        image.transform.localScale = new Vector3(1, 0, 1);
        image.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            // 当 image 动画完成后，播放文本动画
            completionText.enabled = true;
            completionText.transform.localScale = Vector3.zero;
            completionText.DOFade(1, 0.5f).SetDelay(0.2f);  // 文本淡入
            completionText.transform.DOScale(Vector3.one, 0.5f).SetDelay(0.2f).OnComplete(() =>
            {
                // 在文本缩放完成后，启动按钮动画
                nextButton.gameObject.SetActive(true); // 显示按钮
                nextButton.transform.localScale = Vector3.zero; // 设置按钮初始缩放为 0
                nextButton.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
                {
                    // 添加按钮监听事件
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

    // 进入下一模块的方法
    private void EnterNextModule(string str)
    {
        machine.TryToChangeStateByName(str);
        this.gameObject.SetActive(false); // 隐藏当前的通关动画对象
    }
}
