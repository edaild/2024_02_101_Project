using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIManager : MonoBehaviour
{
    public static StatUIManager Instance { get; private set; }

    [Header("UI References")]
    public Slider hungerSlider;                 // 허기 게이지
    public Slider suitDurabilitySlider;         // 우주복 내구도 게이지
    public TextMeshProUGUI hungerText;          // 허기 수치 텍스트
    public TextMeshProUGUI durabilityText;      // 내구도 수치 텍스트

    private SurvivalStats survivalStats;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        survivalStats = FindAnyObjectByType<SurvivalStats>();
        hungerSlider.maxValue = survivalStats.MaxHunger;                            // 슬라이더 최대값 설정
        suitDurabilitySlider.maxValue = survivalStats.maxSuitDurability;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatUI();
    }

    private void UpdateStatUI()
    {
        // 슬라이더 값 업데이트
        hungerSlider.value = survivalStats.currentHunger;
        suitDurabilitySlider.value = survivalStats.currentSuitDurability;

        // 텍스트 업데이트 (퍼센트 표시)
        hungerText.text = $"허기 : {survivalStats.GetHashCode():F0}%";
        durabilityText.text = $"우주복 :{survivalStats.GetSuitDurabilityPercentage():F0}%";

        // 위험 상태일 떄 색상 변경
        hungerSlider.fillRect.GetComponent<Image>().color =
            survivalStats.currentHunger < survivalStats.MaxHunger * 0.3f ? Color.red : Color.green;     // 보통 초록 위험은 빨강

        suitDurabilitySlider.fillRect.GetComponent<Image>().color = 
            survivalStats.currentSuitDurability < survivalStats.maxSuitDurability * 0.3f?Color.red : Color.blue;    // 보통은 파랑 위협은 빨강
    }
}
