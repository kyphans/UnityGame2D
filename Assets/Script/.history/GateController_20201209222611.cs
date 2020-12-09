using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gateSprite;
    [SerializeField] private LayerMask gateLayer;
    public GameObject gatePos; 

    public Transform target;
    void Start()
    {
        
    }

}
