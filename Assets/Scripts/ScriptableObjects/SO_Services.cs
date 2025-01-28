using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Service", menuName = "Startups/Services")]

public class SO_Services : ScriptableObject
{
    [SerializeField]
    private Util.Services type;
    [SerializeField]
    private Util.Tier tier;
    [TextArea(1, 10)]
    [SerializeField]
    private string description;
    [SerializeField]
    private float price;

    public Util.Services Type => type;
    public Util.Tier Tier => tier;
    public float Price => price;
    public string Description => description;
}
