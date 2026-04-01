using UnityEngine;

public class Plum: FallingEntity
{
    public override int score => _isFalling ? 22 : 11;
    public override float respawnTime => Random.Range(4f, 8f);
}