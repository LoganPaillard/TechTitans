using System;
using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;
using System.Collections.Generic;

public class EndScreen : MonoBehaviour
{
    public GameObject scoresContainer;
    public GameObject itemScoresContainer;
    public GameObject starsContainer;
    public Sprite[] starSprites;
    private Dictionary<string, EndScore> seasonScores = new();
    private Dictionary<string, ItemScore> itemScores = new();
    private Dictionary<string, Star> stars = new();
    private float maxUpdateDuration = 3f;
   
    private void Awake()
    {
        foreach (var endScore in scoresContainer.GetComponentsInChildren<EndScore>())
        {
            seasonScores[endScore.gameObject.name] = endScore;
        }

        foreach (var itemScore in itemScoresContainer.GetComponentsInChildren<ItemScore>())
        {
            itemScores[itemScore.gameObject.name] = itemScore;
        }

        foreach (var star in starsContainer.GetComponentsInChildren<Star>())
        {
            star.starIcon.sprite = starSprites[0];
            stars[star.gameObject.name] = star;
        }
    }

    private void Start()
    {
        SetScores();
        SetItemScores();
    }

    private void SetScores()
    {
        foreach (var kvp in GameManager.Instance.scoreSeasons)
        {
            StartCoroutine(SetScoresCoroutine(kvp.Key, kvp.Value));
        }
    }

    private IEnumerator SetScoresCoroutine(SceneID sceneId, int scoreValue)
    {
        float score = 0;
        float endTime = Time.time + maxUpdateDuration;
        EndScore endScore = seasonScores[sceneId.ToString()];
        
        if (scoreValue < 0)
        {
            endScore.scoreText.color = Color.red;
        }
        else
        {
            endScore.scoreText.color = Color.green;
        }

        while (Time.time < endTime)
        {
            score = Mathf.CeilToInt(Mathf.Lerp(score, scoreValue, 0.1f));
            endScore.scoreText.text = $"{score}";

            yield return null;
        }

        if (scoreValue > 1000)
        {
            stars[sceneId.ToString()].starIcon.sprite = starSprites[1];
        }
    }

    private void SetItemScores()
    {
        foreach (var kvp in GameManager.Instance.countItems)
        {
            StartCoroutine(SetItemScoresCoroutine(kvp.Key, kvp.Value));
        }
    }

    private IEnumerator SetItemScoresCoroutine(string itemName, int count)
    {
        float score = 0;
        float endTime = Time.time + maxUpdateDuration;
        ItemScore itemScore = itemScores[itemName];

        while (Time.time < endTime)
        {
            score = Mathf.CeilToInt(Mathf.Lerp(score, count, 0.1f));
            itemScore.scoreText.text = $"{score}";

            yield return null;
        }
    }
}
