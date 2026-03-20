using UnityEngine;

public class Acorn: Tappable
{
    public override int score => 10;
    public override float respawnTime => Random.Range(2f, 4f);
}