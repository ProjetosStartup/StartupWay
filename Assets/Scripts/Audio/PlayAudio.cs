using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField]
    private List<string> Name;
    // Start is called before the first frame update
    void OnEnable()
    {
        foreach (string name in Name)
            AudioController.Instance.Play(name);
    }
}
