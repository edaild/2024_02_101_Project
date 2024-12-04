using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIManager : MonoBehaviour
{
    public static StatUIManager Instance { get; private set; }

    [Header("UI References")]
    public Slider hungerSlider;                 // ��� ������
    public Slider suitDurabilitySlider;         // ���ֺ� ������ ������
    public TextMeshProUGUI hungerText;          // ��� ��ġ �ؽ�Ʈ
    public TextMeshProUGUI durabilityText;      // ������ ��ġ �ؽ�Ʈ

    private SurvivalStats survivalStats;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        survivalStats = FindAnyObjectByType<SurvivalStats>();
        hungerSlider.maxValue = survivalStats.MaxHunger;                            // �����̴� �ִ밪 ����
        suitDurabilitySlider.maxValue = survivalStats.maxSuitDurability;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatUI();
    }

    private void UpdateStatUI()
    {
        // �����̴� �� ������Ʈ
        hungerSlider.value = survivalStats.currentHunger;
        suitDurabilitySlider.value = survivalStats.currentSuitDurability;

        // �ؽ�Ʈ ������Ʈ (�ۼ�Ʈ ǥ��)
        hungerText.text = $"��� : {survivalStats.GetHashCode():F0}%";
        durabilityText.text = $"���ֺ� :{survivalStats.GetSuitDurabilityPercentage():F0}%";

        // ���� ������ �� ���� ����
        hungerSlider.fillRect.GetComponent<Image>().color =
            survivalStats.currentHunger < survivalStats.MaxHunger * 0.3f ? Color.red : Color.green;     // ���� �ʷ� ������ ����

        suitDurabilitySlider.fillRect.GetComponent<Image>().color = 
            survivalStats.currentSuitDurability < survivalStats.maxSuitDurability * 0.3f?Color.red : Color.blue;    // ������ �Ķ� ������ ����
    }
}
