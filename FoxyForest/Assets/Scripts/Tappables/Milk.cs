using UnityEngine;

public class Milk: Tappable
{
    public override int score => -100;
    public override float respawnTime => Random.Range(2f, 4f);
}