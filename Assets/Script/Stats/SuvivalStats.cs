using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float MaxHunger = 100;               // �ִ� ��ⷮ
    public float currentHunger;                 // ���� ��ⷮ
    public float hungerDecreaseRate = 1;        // �ʴ� ��� ���ҷ�

    [Header("Space Suit Settings")]
    public float maxSuitDurability = 100;       // �ִ� ���ֺ� ������
    public float currentSuitDurability;         // ���� ���ֺ� ������
    public float havestingDamage = 5.0f;        // ������ ���ֺ� ������


    private bool isGameOver = false;            // ���� ���� ����
    private bool isPaused = false;              // �Ͻ� ���� ����
    private float hungerTimer = 0;              // ��� ���� Ÿ�̸�

    // Start is called before the first frame update
    void Start()
    {
        // ���� ���۽� ���ݵ��� �ִ� �� ���·� ����
        currentHunger = MaxHunger;
        currentSuitDurability = maxSuitDurability;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver || isPaused) return;

        hungerTimer += Time.deltaTime;

        if (hungerTimer >= 1.0f)
        {
            currentHunger = Mathf.Max(0, currentHunger - hungerDecreaseRate);
            hungerTimer = 0;

            CheckDeath();
        }
    }

    // ������ ������ ���ֺ� ������
    public void DamageOnHarvesting()
    {
        if (isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Max(0, currentSuitDurability - havestingDamage);          // 0�� ���Ϸ� �ȳ������� ���� ���ؼ�
        CheckDeath();
    }

    //������ ���۽� ���ֺ� ������
    public void DamageOnCrafting()
    {
        if (isGameOver || isPaused) return;

        currentSuitDurability = Mathf.Max(0, currentSuitDurability - havestingDamage);          // 0�� ���Ϸ� �ȳ������� ���� ���ؼ�
        CheckDeath();
    }

    // ���� ����� ��� ȸ��
    public void EatFood(float amount)
    {
        if (isGameOver || isPaused) return;                                     // ���� ���ᳪ �ߴ� ���¿����� �������� �ʰ�

        currentHunger = Mathf.Min(MaxHunger, currentHunger + amount);           //MaxHunger ���� �ѱ��� �ʱ� ����

        if (FloationgTextManager.Instance != null)
        {
            FloationgTextManager.Instance.Show($"��� ȸ�� ���� + {amount}", transform.position + Vector3.up);
        }

    }

    // ���ֺ� ���� (ũ����Ż�� ������ ���� ŰƮ ���)
    public void RepairSuit(float amount)
    {
        if (isGameOver || isPaused) return;                                                     // ���� ���ᳪ �ߴ� ���¿����� �������� �ʰ�

        currentSuitDurability = Mathf.Min(maxSuitDurability, currentSuitDurability + amount); // maxSuitDurability ���� �ѱ��� �ʱ� ����

        if (FloationgTextManager.Instance != null)
        {
            FloationgTextManager.Instance.Show($"���ֺ� ���� + {amount}", transform.position + Vector3.up);
        }
    }

    private void CheckDeath()                                       // �÷��̾� ��� ó�� üũ �Լ�
    {
        if ((currentHunger <= 0 || currentSuitDurability <= 0)) 
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        isGameOver = true;
        Debug.Log("�÷��̾� ���!");
        //ToDO : ��� ó�� �߰� (���� ���� UI, ������ ���)
    }

    public float GetHungerPercentage()      // ����� & ���� �Լ�
    {
        return (currentHunger / MaxHunger) * 100;
    }

    public float GetSuitDurabilityPercentage()      // ��Ʈ % ���� �Լ�
    {
        return (currentSuitDurability / MaxHunger) * 100;
    }


    public bool IsGameOver()                    // ���� ���� Ȯ�� �Լ�
    {
        return isGameOver;
    }

    public void ResetStats()            // ���� �Լ� �ۼ� (������ �ʱ�ȭ �뵵)
    {
        isGameOver = false;
        isPaused = false;
        currentHunger = MaxHunger;
        currentSuitDurability = maxSuitDurability;
        hungerTimer = 0;
    }
}
