using UnityEngine;

public enum MusicID {
    None,
    MainMenu,
    Spring,
    Summer,
    Autumn,
    Winter
}

[System.Serializable]
public struct MusicTrack
{
    public MusicID trackID;
    public AudioClip clip;
}
 
public class MusicLibrary : MonoBehaviour
{
    public MusicTrack[] tracks;
    
    public AudioClip GetClipFromID(MusicID musicID)
    {
        foreach (var track in tracks)
        {
            if (track.trackID == musicID)
            {
                return track.clip;
            }
        }
        return null;
    }
}