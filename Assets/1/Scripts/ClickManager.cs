using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    public List<ClickableObject> clickObjects;   // ���ٽ� clickSequence
    private bool isCooldown = false;
    private Camera playerCamera;

    private void Start()
    {
        if (clickObjects == null || clickObjects.Count == 0)
        {
            Debug.LogError("Click objects list is empty!");
            return;
        }

        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();

        // �������ж���ÿ�����ܱ��������ֻ�ܵ�һ�Σ�
        foreach (var obj in clickObjects)
        {
            obj.Activate();
        }
    }

    private void Update()
    {
        if (isCooldown) return;

        // ����Ƿ�����UIԪ��
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;  // ����������UI���������߼��
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                // �����б�����û��ƥ��� ClickableObject
                ClickableObject target = clickObjects.Find(obj => obj.gameObject == clickedObject);

                if (target != null && !target.HasBeenClicked)   // ȷ��û�����
                {
                    target.TriggerAction();
                    target.HasBeenClicked = true; // ���Ϊ�ѵ��

                    // ��ȴ
                    isCooldown = true;
                    StartCoroutine(Cooldown(target.animationDuration));
                }
                else
                {
                    Debug.Log("�㵽��Ч���壬���߸������ѵ����");
                }
            }
        }
    }

    public void TryClickFromUI(ClickableObject obj)
    {
        if (isCooldown) return;

        if (clickObjects.Contains(obj) && !obj.HasBeenClicked)
        {
            obj.TriggerAction();
            obj.HasBeenClicked = true;

            isCooldown = true;
            StartCoroutine(Cooldown(obj.animationDuration));
        }
        else
        {
            Debug.Log("UI���󲻴��ڣ������Ѿ������");
        }
    }

    IEnumerator Cooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        isCooldown = false;
    }

    public void AddList(ClickableObject CO)
    {
        if (CO != null)
        {
            CO.Activate();
            clickObjects.Add(CO);
        }
    }

}
