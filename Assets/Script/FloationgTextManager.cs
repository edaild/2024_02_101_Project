using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloationgTextManager : MonoBehaviour
{
    public static FloationgTextManager Instance;          // �̱���
    public GameObject textPrefab;                        //UI �ؽ�Ʈ ������

    private void Awake()
    {
        Instance = this;                                // �̱��� ���
    }

    public void Show(string text, Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);               // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ

        GameObject textobj = Instantiate(textPrefab, transform);                    // ui �ؽ�Ʈ ����
        textobj.transform.position = screenPos;

        TextMeshProUGUI temp = textobj.GetComponent<TextMeshProUGUI>();
        if(temp != null)
        {
            temp.text = text;

            StartCoroutine(AnimateText(textobj));                                   // ���� �ִϸ��̼� ȿ�� ����
        }
    }

    private IEnumerator AnimateText(GameObject textObj)
    {
        float duration = 1f;                                                        // ���۽ð�
        float timer = 0;                                                            // ����� Ÿ�̸�

        Vector3 startPos = textObj.transform.position;          
        TextMeshProUGUI temp = textObj.GetComponent<TextMeshProUGUI>();             // �޾ƿ� ������Ʈ ���� TMP ��Ʈ ����

        while(timer < duration)                                                      // Ÿ�̸�
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            textObj.transform.position = startPos + Vector3.up * (progress * 50f);      // ��Ʈ�� ���� �ö�� ȿ���� �ش�

            if (temp != null)
            {
                temp.alpha = 1 - progress;                                             // ���̵� �ƿ� ȿ��
            }

            yield return null;
        }
        Destroy(textObj);
    }
}
