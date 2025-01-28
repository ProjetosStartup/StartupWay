using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private GameObject player;
    private GameObject shadow;

    public GameObject Player { get => player; }
    public GameObject Shadow { get => shadow; }
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        shadow = GameObject.FindGameObjectWithTag("Shadow");
    }

}
