using UnityEngine;

public class NestEggs: Tappable
{
    public override int score => -50;
    public override float respawnTime => Random.Range(1f, 3f);
}