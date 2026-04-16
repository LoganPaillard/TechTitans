using UnityEngine;
 
public enum SoundID {
    Click,
    Hover,
    Buzzer
}

[System.Serializable]
public struct SoundEffect
{
    public SoundID groupID;
    public AudioClip[] clips;
}
 
public class SoundLibrary : MonoBehaviour
{
    public SoundEffect[] soundEffects;
 
    public AudioClip GetClipFromID(SoundID soundID)
    {
        foreach (var soundEffect in soundEffects)
        {
            if (soundEffect.groupID == soundID)
            {
                return soundEffect.clips[Random.Range(0, soundEffect.clips.Length)];
            }
        }
        return null;
    }
}