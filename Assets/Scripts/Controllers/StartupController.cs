using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupController : MonoBehaviour
{
    public static StartupController Instance;

    [SerializeField]
    private SO_Startup startup;
    public SO_Startup Startup { get => startup; set => startup = value; }

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if(startup != null)
            startup.Team.TierCalculator();
    }
}
