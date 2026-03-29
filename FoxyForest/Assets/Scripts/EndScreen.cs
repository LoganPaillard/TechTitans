using System;
using System.Collections;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public GameObject scoresContainer;
    private EndScore[] scores;
    private int scoreSpring = 0;
    private int scoreSummer = 0;
    private int scoreAutumn = 0;
    private float maxUpdateDuration = 3f;
   
    private void Awake()
    {
        scores = scoresContainer.GetComponentsInChildren<EndScore>();
    }

    private void Start()
    {
        SetScores();
    }

    private void SetScores()
    {
        StartCoroutine(SetScoresCoroutine());
    }

    private IEnumerator SetScoresCoroutine()
    {
        float endTime = Time.time + maxUpdateDuration;

        // Within the end time, each score will be updated until it reaches its final value in GameManager
        while (Time.time < endTime)
        {
            scoreSpring = Mathf.CeilToInt(Mathf.Lerp(scoreSpring, GameManager.Instance.scoreSpring, 0.1f));
            scoreSummer = Mathf.CeilToInt(Mathf.Lerp(scoreSummer, GameManager.Instance.scoreSummer, 0.1f));
            scoreAutumn = Mathf.CeilToInt(Mathf.Lerp(scoreAutumn, GameManager.Instance.scoreAutumn, 0.1f));

            scores[0].scoreText.text = $"{scoreSpring}";
            scores[1].scoreText.text = $"{scoreSummer}";
            scores[2].scoreText.text = $"{scoreAutumn}";

            yield return null;
        }
    }
}
