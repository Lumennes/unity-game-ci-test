using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.Networking;

[ExecuteInEditMode]
public class AudioStreamCash : MonoBehaviour
{
  public bool Cash;
  public bool DinamicCash = true;

  public List<Clip> infoList = new List<Clip>();
  public AudioClip this[int index] => infoList[index].Cash;
  AudioClip[] infoListUnity;

  public static AudioStreamCash instance;

  private void Awake()
  {
    if (instance == null)
      instance = this;
    else
      Destroy(gameObject);

#if UNITY_EDITOR
    Listen();
#endif

#if !UNITY_EDITOR
     
       foreach (var item in infoList)
        {
            item.ClearCash();
        }
#endif

    if (Cash)
      LoadCash();
  }
  private void Start()
  {    
    LoadLIstUnity();

    if (instance == this && Application.isPlaying)
      DontDestroyOnLoad(gameObject);
  }
  private void Listen()
  {
    infoList.Clear();

    LoadExt("mp3", AudioType.MPEG);
    LoadExt("wav", AudioType.WAV);
    LoadExt("ogg", AudioType.OGGVORBIS);
  }
  void LoadLIstUnity()
  {
    infoListUnity = Resources.LoadAll<AudioClip>("");
    foreach (var item in infoListUnity)
      infoList.Add(new Clip(item, true));
  }

  void LoadExt(string ext, AudioType type)
  {
    DirectoryInfo dir = new (Application.streamingAssetsPath);
    FileInfo[] info = dir.GetFiles("*." + ext);
    foreach (var item in info)
    {
      if (Regex.IsMatch(item.Name, @"\p{IsCyrillic}"))
        Debug.LogError($"Переиминуй {item.Name}");

      infoList.Add(new Clip(Application.streamingAssetsPath, item.Name, ext, type, Cash || DinamicCash));
    }
  }

  void LoadCash()
  {
    foreach (var item in infoList)
      StartCoroutine(item.GetFile());
  }

  public static Clip Find(string name)
  {
    if(instance)
      return instance.infoList.Find(x => x.Name == name);
    return null;
  }
}

[Serializable]
public class Clip
{
  public string name;
  public string Path;
  public string Ext;
  public AudioType Type;
  private bool UnityFile;
  public AudioClip Cash;

  public bool Cashing;
  public bool IsCashing => Cash != null;
  public string Name { get => name; }
  public Clip(string path, string name, string ext, AudioType type, bool cash)
  {
    Ext = ext;
    this.name = name.Substring(0, name.Length - Ext.Length - 1);
    Path = path;
    Type = type;
    Cashing = cash;
    UnityFile = false;
  }

  public Clip(AudioClip audio, bool cash)
  {
    Cashing = cash;
    UnityFile = true;
    Cash = audio;
    name = audio.name;
  }

  public IEnumerator GetFile(Action<AudioClip> action = null)
  {
    if (UnityFile || IsCashing)
    {
      action?.Invoke(Cash);
    }
    else
    {
      string Url = Application.streamingAssetsPath + "/" + Name + "." + Ext;
      UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(Url, Type);
      request.SendWebRequest();
      while (!request.isDone)
      {
        yield return null;
      }
      AudioClip cash = DownloadHandlerAudioClip.GetContent(request);
      if (Cashing)
      {
        Cash = cash;
        Cash.name = Name;
      }
      action?.Invoke(cash);
    }
  }

  public void ClearCash()
  {
    Cash = null;
  }
}
