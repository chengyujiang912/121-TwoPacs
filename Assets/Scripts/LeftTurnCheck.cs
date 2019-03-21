using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTurnCheck : MonoBehaviour
{
    public bool leftTurnable = true;

    void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag ( "Wall"))
        {
            leftTurnable = false;
        }
        else
        {
            leftTurnable = true;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag ( "Wall"))
        {
            leftTurnable = true;
        }
    }

}
