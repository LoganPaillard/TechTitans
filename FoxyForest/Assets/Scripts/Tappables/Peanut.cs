using UnityEngine;

public class Peanut: Tappable
{
    public override int score => -32;
    public override float respawnTime => Random.Range(1f, 3f);
}