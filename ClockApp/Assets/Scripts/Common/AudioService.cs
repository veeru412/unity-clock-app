using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Interface;

namespace Assets.Scripts.Common
{
  public class AudioService : IAudioService
  {
    private readonly Dictionary<string, AudioClip> audioClipCache = new();
    public void PlaySound(string id)
    {
      var audioClip = GetAudio(id);
      if(audioClip != null)
      {
        AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
      }
    }

    private AudioClip GetAudio(string id)
    {
      var taskCompleted = audioClipCache.TryGetValue(id, out var clip);
      if (taskCompleted)
      {
        return clip;
      }
      else
      {
        clip = Resources.Load<AudioClip>($"Sounds/{id}");
        if(clip == null)
        {
          return null;
        }
        audioClipCache.Add(id, clip);
        return clip;
      }
    }
  }
}