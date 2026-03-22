using UnityEngine;

public class Milk: Entity
{
    public override int score => 100;
    public override float respawnTime => Random.Range(2f, 4f);
}