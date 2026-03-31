using UnityEngine;

public class Corn: FallingEntity
{
    public override int score => _isFalling ? 30 : 15;
    public override float respawnTime => Random.Range(4f, 8f);
}