using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IncramentalManager : MonoBehaviour
{
    #region Singleton
    public static IncramentalManager instance = null;
    #endregion
    #region Incramental Specs:
    [Header("Income Specs:")]
    public float incomeFactor;
    [SerializeField] int incomeLevel;
    [SerializeField] int incomeMoney;
    [SerializeField] Slider incomeMoneySlider;
    [Space]
    [Header("Start Units Specs:")]
    public int unitsCount=1;
    [SerializeField] int unitsLevel;
    [SerializeField] int unitsMoney;
    [SerializeField] Slider unitsMoneySlider;
    #endregion
    #region Incramental TextMP
    [Header("Incramental Factor Text")]
    [HideInInspector] public TextMeshProUGUI incomeFactorTextMP;
    [HideInInspector] public TextMeshProUGUI unitsCountTextMP;

    [Header("Incramental Level Text")]
    [HideInInspector] public TextMeshProUGUI incomeLevelTextMP;
    [HideInInspector] public TextMeshProUGUI unitsLevelTextMP;

    [Header("Incramental Money Text")]
    [HideInInspector] public TextMeshProUGUI incomeMoneyTextMP;
    [HideInInspector] public TextMeshProUGUI unitsMoneyTextMP;
    #endregion
    private void Awake()
    {
        if (instance == null) instance = this;
        #region PlayerPrefs
        incomeFactor = PlayerPrefs.GetFloat("IncomeFactor", 1f);
        incomeLevel = PlayerPrefs.GetInt("IncomeLevel", 1);
        incomeMoney = PlayerPrefs.GetInt("IncomeMoney", 10);

        unitsCount = PlayerPrefs.GetInt("UnitsCount", 0);
        unitsLevel = PlayerPrefs.GetInt("UnitsLevel", 1);
        unitsMoney = PlayerPrefs.GetInt("UnitsMoney", 10);
        #endregion
    }
    void Start()
    {
        #region Find TextMP
        incomeFactorTextMP = GameObject.Find("IncomeFactorTextMP").GetComponent<TextMeshProUGUI>();
        incomeLevelTextMP = GameObject.Find("IncomeLevelTextMP").GetComponent<TextMeshProUGUI>();
        incomeMoneyTextMP = GameObject.Find("IncomeMoneyTextMP").GetComponent<TextMeshProUGUI>();

        unitsCountTextMP = GameObject.Find("UnitsCountTextMP").GetComponent<TextMeshProUGUI>();
        unitsLevelTextMP = GameObject.Find("UnitsLevelTextMP").GetComponent<TextMeshProUGUI>();
        unitsMoneyTextMP = GameObject.Find("UnitsMoneyTextMP").GetComponent<TextMeshProUGUI>();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        incomeLevelTextMP.text = incomeLevel.ToString();
        incomeMoneyTextMP.text = UIManager.instance.moneyCount + "/" + incomeMoney.ToString();
        incomeFactorTextMP.text = PlayerPrefs.GetFloat("IncomeFactor", incomeFactor).ToString() + "x";

        unitsLevelTextMP.text = unitsLevel.ToString();
        unitsMoneyTextMP.text = UIManager.instance.moneyCount + "/" + unitsMoney.ToString();
        unitsCountTextMP.text = PlayerPrefs.GetFloat("UnitsCount", unitsCount).ToString();
    }
    #region Increase Increamental
    public void IncreaseIncome()
    {
        if (UIManager.instance.moneyCount >= incomeMoney)
        {
            UIManager.instance.moneyCount -= incomeMoney;
            incomeLevel++;
            incomeMoney += 20;
            incomeFactor += .1f;

            PlayerPrefs.SetInt("Diamond", UIManager.instance.moneyCount);
            PlayerPrefs.SetInt("IncomeLevel", incomeLevel);
            PlayerPrefs.SetInt("IncomeMoney", incomeMoney);
            PlayerPrefs.SetFloat("IncomeFactor", incomeFactor);
            incomeLevelTextMP.text = incomeLevel.ToString();
            incomeMoneyTextMP.text = UIManager.instance.moneyCount + "/" + incomeMoney.ToString();
            incomeFactorTextMP.text = incomeFactor.ToString() + "k";
            UIManager.instance.moneyNumText.text = UIManager.instance.moneyCount.ToString();
        }
    }
    public void IncreaseUnits()
    {
        if (UIManager.instance.moneyCount >= unitsMoney)
        {
            UIManager.instance.moneyCount -= unitsMoney;
            unitsLevel++;
            unitsMoney += 20;
            unitsCount += 1;
            WeaponSpecs.instance.IncreaseDummy();
            WeaponSpecs.instance.EvolveForGuns();
           
            PlayerPrefs.SetInt("Money", UIManager.instance.moneyCount);
            PlayerPrefs.SetInt("UnitsLevel", unitsLevel);
            PlayerPrefs.SetInt("UnitsMoney", unitsMoney);
            PlayerPrefs.SetInt("UnitsCount", unitsCount);
            unitsLevelTextMP.text = unitsLevel.ToString();
            unitsMoneyTextMP.text = UIManager.instance.moneyCount + "/" + unitsMoney.ToString();
            unitsCountTextMP.text = incomeFactor.ToString();
            UIManager.instance.moneyNumText.text = UIManager.instance.moneyCount.ToString();

        }
    }
    #endregion
    #region Slider Check
    public void IncomeMoneySliderCheck()
    {
        incomeMoneySlider.value = ((float)UIManager.instance.moneyCount / (float)incomeMoney);
    }
    public void UnitsMoneySliderCheck()
    {
        unitsMoneySlider.value = ((float)UIManager.instance.moneyCount / (float)unitsMoney);
    }
    #endregion
}
