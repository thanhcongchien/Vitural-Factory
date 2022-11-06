using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform _pickupPoint;
    private GameObject _pickupObject;
    private Rigidbody _pickupRigidbody;


    [Header("Physics Settings")]
    [SerializeField] private float _pickupRange = 5.0f;
    [SerializeField] private float _pickupForce = 150.0f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_pickupObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, _pickupRange))
                {
                    // pickup object
                    PickUp(hit.transform.gameObject);
                }

            }
            else
            {
                // drop object
                Drop();
            }
        }
        if (_pickupObject != null)
        {
            // move object
        }
    }

    void MoveObject(){
        if(Vector3.Distance(_pickupObject.transform.position, _pickupPoint.position) > 0.1f){
            Vector3 direction = _pickupPoint.position - _pickupObject.transform.position;
            _pickupRigidbody.AddForce(direction * _pickupForce);
        }
    }


    void PickUp(GameObject pickupObject)
    {

        if (pickupObject.GetComponent<Rigidbody>() != null)
        {
            _pickupRigidbody = pickupObject.GetComponent<Rigidbody>();
            _pickupRigidbody.useGravity = false;
            _pickupRigidbody.drag = 10;
            _pickupRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            _pickupRigidbody.transform.parent = _pickupPoint;
            _pickupObject = pickupObject;
        }
    }

    void Drop()
    {
        _pickupRigidbody.useGravity = true;
        _pickupRigidbody.drag = 1;
        _pickupRigidbody.constraints = RigidbodyConstraints.None;

        _pickupObject.transform.parent = null;
        _pickupObject = null;
    }
}
