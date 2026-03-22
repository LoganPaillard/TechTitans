using UnityEngine;
using System.Collections;

public class Apple : Fruit
{
    public override int score => 12;
    public override float respawnTime => Random.Range(2f, 4f);
    public override float growDuration => 0.8f;
}