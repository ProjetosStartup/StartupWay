using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAudio : MonoBehaviour
{
    [SerializeField] private bool isBMG;
    private void Start()
    {
        gameObject.GetComponent<Slider>().value = AudioController.Instance.GetVolume(isBMG);
        Slider(true);
        Slider(false);
    }
    // Start is called before the first frame update
    public void Slider(bool isBgm)
    {
        AudioController.Instance.SetVolume(isBgm, gameObject.GetComponent<Slider>().value);

    }
}
