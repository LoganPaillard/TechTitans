using UnityEngine;

public class Mushroom: Tappable
{
    public override int score => 42;
    public override float respawnTime => Random.Range(2f, 4f);
}