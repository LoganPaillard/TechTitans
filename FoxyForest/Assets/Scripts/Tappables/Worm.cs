using UnityEngine;

public class Worm: Tappable
{
    public override int score => 40;
    public override float respawnTime => Random.Range(2f, 4f);
}