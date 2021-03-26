using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitObject : MonoBehaviour
{
    FruitInstancer poolSystem = FruitInstancer.instance;
    public GameObject topPart;
    public GameObject bottomPart;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground") {
            gameObject.SetActive(false);
            FruitInstancer.instance.playerHealth--;
        }
        else if(other.tag == "Player") {
            FruitInstancer.instance.playerPoints += CalculatePoints();
            gameObject.SetActive(false);
            Instantiate(topPart, transform.position, Quaternion.identity);
            Instantiate(bottomPart, transform.position, Quaternion.identity);
        }
        else {
            return;
        }
    }

    int CalculatePoints() {
        int _pointsToAdd = 0;
        _pointsToAdd += (int) GetComponent<Rigidbody>().velocity.y;
        _pointsToAdd += Random.Range(0, 7);
        return _pointsToAdd;
    }
}
