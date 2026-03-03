using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
 
public class SeasonWipe : SceneTransition
{
    public Image[] seasons;
    public CanvasGroup crossFade;

    private Image currentSeason;
 
    public override IEnumerator AnimateTransitionIn()
    {
        currentSeason = getSeason();
        crossFade.alpha = 1f;
        
        var tweener = currentSeason.rectTransform.DOAnchorPos(Vector2.zero, 0.25f);
     
        yield return tweener.WaitForCompletion();
    }
 
    public override IEnumerator AnimateTransitionOut()
    {
        var tweener = crossFade.DOFade(0f, 0.25f);
        
        yield return tweener.WaitForCompletion();

        currentSeason.rectTransform.anchoredPosition = new Vector2(2500, 1250);
    }

    private Image getSeason()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainMenu":
                return seasons[0];
            case "Spring":
                return seasons[1];
            case "Summer":
                return seasons[2];
            case "Autumn":
                return seasons[3];
            default:
                return seasons[0];
        }
    }
}