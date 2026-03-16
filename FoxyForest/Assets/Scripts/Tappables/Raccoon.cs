using UnityEngine;

public class Raccoon: Tappable
{
    public override int score => -100;
    public override float respawnTime => Random.Range(2f, 4f);
}