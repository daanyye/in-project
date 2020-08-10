using UnityEngine;
using UnityEngine.UI;

public class FaqPopUpUIScript : MonoBehaviour
{
    public Text whatIsTheGameAbout;
    public Text howDoIGetPoints;
    public Text nutritionalRecommendations;
    public Text keyBinds;

    private void Start()
    {
        manageFaqTexts(true, false, false, false);
    }

    public void onWhatIsThisGameAboutClick()
    {
        manageFaqTexts(true, false, false, false);
    }

    public void onHowDoIGetPointsClick()
    {
        manageFaqTexts(false, true, false, false);
    }

    public void onNutritionalRecommendationsClick()
    {
        manageFaqTexts(false, false, true, false);
    }

    public void onKeyBindsClick()
    {
        manageFaqTexts(false, false, false, true);
    }

    private void manageFaqTexts(bool onWhat, bool onHow, bool onNutritional, bool onKeyBinds)
    {
        whatIsTheGameAbout.gameObject.SetActive(onWhat);
        howDoIGetPoints.gameObject.SetActive(onHow);
        nutritionalRecommendations.gameObject.SetActive(onNutritional);
        keyBinds.gameObject.SetActive(onKeyBinds);
    }
}
