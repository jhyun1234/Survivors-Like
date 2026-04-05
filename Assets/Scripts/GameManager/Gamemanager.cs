using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    public PlayerController player;

    void Awake()
    {
        instance = this;
        
    }

}
