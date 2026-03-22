using UnityEngine;
using System.Collections;

public class Mushroom : Fruit
{
    public override int score => 42;
    public override float respawnTime => Random.Range(2f, 4f);
}