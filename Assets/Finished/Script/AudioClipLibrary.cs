using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AudioClipLibrary", menuName = "Audio/ClipLibrary")]
public class AudioClipLibrary : ScriptableObject
{
    public List<ClipEntry> ambienceClips;
    public List<ClipEntry> sfxClips;
    public List<ClipEntry> environmentClips;

    [System.Serializable]
    public class ClipEntry
    {
        public string name;
        public AudioClip clip;
    }
}
