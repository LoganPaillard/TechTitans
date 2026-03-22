using UnityEngine;

public class Raccoon: Animal
{
    public override int score => -100;
    public override float respawnTime => Random.Range(2f, 4f);
}