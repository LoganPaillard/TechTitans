using UnityEngine;

public class Grape: Fruit
{
    public override int score => 8;
    public override float respawnTime => Random.Range(2f, 4f);
}