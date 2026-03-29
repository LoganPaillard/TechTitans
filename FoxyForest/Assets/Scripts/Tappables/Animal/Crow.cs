using UnityEngine;

public class Crow: Animal
{
    public override int score => -70;
    public override float respawnTime => Random.Range(1f, 3f);
}