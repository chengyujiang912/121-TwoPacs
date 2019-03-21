using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTurnCheck : MonoBehaviour
{
    public bool rightTurnable = true;

    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag ( "Wall"))
        {
            rightTurnable = false;
        }
        else
        {
            rightTurnable = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag ( "Wall"))
        {
            rightTurnable = true;
        }
    }
}
