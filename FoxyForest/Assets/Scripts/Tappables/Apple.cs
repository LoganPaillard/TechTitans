using UnityEngine;
using System.Collections;

public class Apple : Fruit
{
    public override int score => 12;
    public override float respawnTime => Random.Range(2f, 4f);
}