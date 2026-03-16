using UnityEngine;

public class Crow: Tappable
{
    public override int score => -70;
    public override float respawnTime => Random.Range(1f, 3f);
}