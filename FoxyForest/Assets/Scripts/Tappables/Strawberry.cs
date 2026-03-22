using UnityEngine;
using System.Collections;

public class Strawberry: Fruit
{
    public override int score => 16;
    public override float respawnTime => Random.Range(2f, 4f);
}