using UnityEngine;
using System.Collections;

public class Cherry : Fruit
{
    public override int score => 10;
    public override float respawnTime => Random.Range(2f, 4f);
}