using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
    public Rigidbody rb;
    private void Start()
    {
        rb.AddForce(AddRandomForce(), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
            Destroy(this.gameObject);
    }

    Vector3 AddRandomForce() {
        float _x = Random.Range(-3.0f, 3.0f);
        float _y = Random.Range(0.0f, 3.0f);

        return new Vector3(_x, _y, 0);
    }
}
