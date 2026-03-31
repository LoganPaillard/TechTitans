using UnityEngine;
public class Apple : FallingEntity
{
    public override int score => _isFalling ? 20 : 10;
    public override float respawnTime => Random.Range(5f, 10f);
}