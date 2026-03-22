using UnityEngine;

public class Corn: Entity
{
    public override int score => 20;
    public override float respawnTime => Random.Range(2f, 4f);
}