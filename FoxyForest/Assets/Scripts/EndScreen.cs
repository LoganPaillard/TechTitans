using System;
using System.Collections;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public GameObject scoresContainer;
    private EndScore[] scores;
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
        for (int i = 0; i < GameManager.Instance.scoreSeasons.Count; i++)
        {
            StartCoroutine(SetScoresCoroutine(i));
        }
    }

    private IEnumerator SetScoresCoroutine(int index)
    {
        Debug.Log($"Setting score for season {index} with value {GameManager.Instance.scoreSeasons[index]}");
        float score = 0;
        float endTime = Time.time + maxUpdateDuration;
        
        while (Time.time < endTime)
        {
            score = Mathf.CeilToInt(Mathf.Lerp(score, GameManager.Instance.scoreSeasons[index], 0.1f));
            scores[index].scoreText.text = $"{score}";
            yield return null;
        }
    }
}
