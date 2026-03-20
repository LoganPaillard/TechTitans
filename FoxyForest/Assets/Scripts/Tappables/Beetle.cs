using UnityEngine;

public class Beetle: Tappable
{
    public override int score => -100;
    public override float respawnTime => Random.Range(2f, 4f);
}