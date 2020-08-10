using UnityEngine;
using UnityEngine.UI;

public class AttributeScript : MonoBehaviour
{
    public Text atkNumber;
    public Text hpNumber;
    public Text defNumber;
    public Text freePointsToUseOnATK;
    public Text freePointsToUseOnHP;
    public Text freePointsToUseOnDEF;
    public Text powerNumber;
    public Text healthPoints;
    public Text attackDamagePoints;
    public Text damageReductionPercentage;

    private int tempAddedAtkNumber = 0;
    private int tempAddedHpNumber = 0;
    private int tempAddedDefNumber = 0;

    public Button applyButton;

    private void Start()
    {
        InitializeAttributeNumbers();
        CalculateStats();
    }

    private void Update()
    {
        if (tempAddedAtkNumber > 0 || tempAddedHpNumber > 0 || tempAddedDefNumber > 0)
            applyButton.gameObject.SetActive(true);
        else
            applyButton.gameObject.SetActive(false);
    }

    private void InitializeAttributeNumbers()
    {
        atkNumber.text = PlayerPrefs.GetInt("atkNumber").ToString();
        defNumber.text = PlayerPrefs.GetInt("defNumber").ToString();
        hpNumber.text = PlayerPrefs.GetInt("hpNumber").ToString();
        freePointsToUseOnATK.text = PlayerPrefs.GetInt("freePointsATK").ToString();
        freePointsToUseOnHP.text = PlayerPrefs.GetInt("freePointsHP").ToString();
        freePointsToUseOnDEF.text = PlayerPrefs.GetInt("freePointsDEF").ToString();
    }

    private void CalculateStats()
    {
        int tempAtkNumber = int.Parse(atkNumber.text);
        int tempHpNumber = int.Parse(hpNumber.text);
        int tempDefNumber = int.Parse(defNumber.text);
        attackDamagePoints.text = Mathf.Floor(100 + (Mathf.Log(tempAtkNumber, 2) * 100)).ToString();
        healthPoints.text = Mathf.Floor(100 + (Mathf.Log(tempHpNumber, 2) * 100)).ToString();
        damageReductionPercentage.text = (tempDefNumber * 0.1f).ToString() + "%";
        powerNumber.text = (tempAtkNumber * tempHpNumber * tempDefNumber).ToString();

        PlayerPrefs.SetInt("attackDamagePoints", int.Parse(attackDamagePoints.text));
        PlayerPrefs.SetInt("healthPoints", int.Parse(healthPoints.text));
        PlayerPrefs.SetFloat("damageReductionPercentage", float.Parse(damageReductionPercentage.text.Replace("%", "")));
    }

    public void addAttack()
    {
        if (checkAvailablePoints("ATK"))
        {
            atkNumber.text = (int.Parse(atkNumber.text) + 1).ToString();
            freePointsToUseOnATK.text = (int.Parse(freePointsToUseOnATK.text) - 1).ToString();
            tempAddedAtkNumber += 1;
            CalculateStats();
        }
    }

    public void subAttack()
    {
        if (tempAddedAtkNumber != 0)
        {
            atkNumber.text = (int.Parse(atkNumber.text) - 1).ToString();
            freePointsToUseOnATK.text = (int.Parse(freePointsToUseOnATK.text) + 1).ToString();
            tempAddedAtkNumber -= 1;
            CalculateStats();
        }
    }
    public void addHealth()
    {
        if (checkAvailablePoints("HP"))
        {
            hpNumber.text = (int.Parse(hpNumber.text) + 1).ToString();
            freePointsToUseOnHP.text = (int.Parse(freePointsToUseOnHP.text) - 1).ToString();
            tempAddedHpNumber += 1;
            CalculateStats();
        }
    }

    public void subHealth()
    {
        if (tempAddedHpNumber != 0)
        {
            hpNumber.text = (int.Parse(hpNumber.text) - 1).ToString();
            freePointsToUseOnHP.text = (int.Parse(freePointsToUseOnHP.text) + 1).ToString();
            tempAddedHpNumber -= 1;
            CalculateStats();
        }
    }

    public void addDefence()
    {
        if (checkAvailablePoints("DEF"))
        {
            defNumber.text = (int.Parse(defNumber.text) + 1).ToString();
            freePointsToUseOnDEF.text = (int.Parse(freePointsToUseOnDEF.text) - 1).ToString();
            tempAddedDefNumber += 1;
            CalculateStats();
        }
    }

    public void subDefence()
    {
        if (tempAddedDefNumber != 0)
        {
            defNumber.text = (int.Parse(defNumber.text) - 1).ToString();
            freePointsToUseOnDEF.text = (int.Parse(freePointsToUseOnDEF.text) + 1).ToString();
            tempAddedDefNumber -= 1;
            CalculateStats();
        }
    }

    private void setPrefAttributes(int atkNumber, int hpNumber, int defNumber)
    {
        PlayerPrefs.SetInt("atkNumber", atkNumber);
        PlayerPrefs.SetInt("hpNumber", hpNumber);
        PlayerPrefs.SetInt("defNumber", defNumber);
    }

    private void setPrefFreePoints(int freeATKPoints, int freeHPPoints, int freeDEFPoints)
    {
        PlayerPrefs.SetInt("freePointsATK", freeATKPoints);
        PlayerPrefs.SetInt("freePointsHP", freeHPPoints);
        PlayerPrefs.SetInt("freePointsDEF", freeDEFPoints);
    }

    private bool checkAvailablePoints(string attributeType)
    {
        switch (attributeType)
        {
            case "ATK":
                if (int.Parse(freePointsToUseOnATK.text) > 0)
                    return true;
                return false;

            case "HP":
                if (int.Parse(freePointsToUseOnHP.text) > 0)
                    return true;
                return false;

            case "DEF":
                if (int.Parse(freePointsToUseOnDEF.text) > 0)
                    return true;
                return false;
            default:
                return false;
        }
    }

    public void onAcceptClick()
    {
        setPrefAttributes(int.Parse(atkNumber.text), int.Parse(hpNumber.text), int.Parse(defNumber.text));
        setPrefFreePoints(int.Parse(freePointsToUseOnATK.text), int.Parse(freePointsToUseOnHP.text), int.Parse(freePointsToUseOnDEF.text));
        resetAddedNumbers();
    }

    private void resetAddedNumbers()
    {
        tempAddedAtkNumber = 0;
        tempAddedDefNumber = 0;
        tempAddedHpNumber = 0;
    }
}
