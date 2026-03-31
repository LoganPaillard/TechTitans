using UnityEngine;

public class Acorn: FallingEntity
{
    public override int score => 10;
    public override float respawnTime => Random.Range(5f, 10f);
}