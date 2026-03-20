using UnityEngine;

public class Plum: Tappable
{
    public override int score => 22;
    public override float respawnTime => Random.Range(2f, 4f);
}