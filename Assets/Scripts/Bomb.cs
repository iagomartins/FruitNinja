using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") {
            gameObject.SetActive(false);
            FruitInstancer.instance.isGameOver = true;
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
