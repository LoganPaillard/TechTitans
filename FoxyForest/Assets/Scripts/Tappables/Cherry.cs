using UnityEngine;

public class Cherry: Tappable
{
    public override int score => 10;
    public override float respawnTime => Random.Range(1f, 3f);
}