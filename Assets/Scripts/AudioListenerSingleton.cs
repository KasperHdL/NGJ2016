using UnityEngine;
using System.Collections;

public class AudioListenerSingleton : MonoBehaviour {

  private static AudioListenerSingleton instance = null;

  private void Awake() {
    if (instance && instance != this) {
      DestroyImmediate(gameObject);
      return;
    } else {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
  }
}