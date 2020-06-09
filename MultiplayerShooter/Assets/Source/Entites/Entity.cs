using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //Serialized Fields
    [Header ("Entity")]
    [SerializeField] private int HP;


    //Sets up any listeners needed
    protected virtual void Initialize()
    {

    }

    
}
