using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;

public class RandomEventController : MonoBehaviour
{
    public static RandomEventController Instance;

    [SerializeField]
    private List<SO_RandomEvent> randomEvent;
    [SerializeField]
    private List<SO_RandomEvent> randomEventCoppy;

    private int totalCount;
    private bool inout;

    private void Awake()
    {
        Instance = this;
        totalCount = randomEvent.Count;
        inout = true;
    }

    public SO_RandomEvent GetRandomEvent()
    {
        Util.Region region = MovementController.Instance.ActualTile.region;

        SO_RandomEvent temp = new SO_RandomEvent();

        if (inout)
        {
            temp = randomEvent.Where(q => q.region == region || q.region == Util.Region.All).OrderBy(q => Random.value).FirstOrDefault();
            randomEventCoppy.Add(temp); 
            randomEvent.Remove(temp);
        }
        else
        {
            temp = randomEventCoppy.Where(q => q.region == region || q.region == Util.Region.All).OrderBy(q => Random.value).FirstOrDefault();
            randomEvent.Add(temp);
            randomEventCoppy.Remove(temp);
        }

        if (randomEvent.Count == 0)
        {
            inout = false;

        }
        else if (randomEvent.Count == totalCount)
        {
            inout = true;
        }

        return temp;
    }
}
