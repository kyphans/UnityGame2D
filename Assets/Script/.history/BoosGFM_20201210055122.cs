using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;                                                        

public class BoosGFM : MonoBehaviour
{
    // Start is called before the first frame update
    public AIPath aiPath;

    // // Update is called once per frame
    void Update()
    {
         if (aiPath.desiredVelocity.x >= 0.1f)
        {
            transform.localScale = new Vector3(5f, 5f, 0f);
        }
        else if (aiPath.desiredVelocity.x <= -0.1f)
        {
            transform.localScale = new Vector3(-5f, 5f, 0f);
        }
    }
}
