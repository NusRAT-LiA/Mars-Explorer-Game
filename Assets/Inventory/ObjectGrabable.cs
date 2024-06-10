// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ObjectGrabable : MonoBehaviour
// {
//     private Rigidbody objectRigidbody;
//     private Transform objectGrabPointTransform;
//     private bool activated = false; // Flag to indicate if the object has been activated

//     private void Awake()
//     {
//         objectRigidbody = GetComponent<Rigidbody>();
//     }

//     public void Grab(Transform objectGrabPointTransform)
//     {
//         if (!activated) // Check if the object is not activated
//         {
//             this.objectGrabPointTransform = objectGrabPointTransform;
//             objectRigidbody.useGravity = false;
//         }
//     }

//     public void Drop()
//     {
//         this.objectGrabPointTransform = null;
//         objectRigidbody.useGravity = true;
//     }

//     private void FixedUpdate()
//     {
//         if (objectGrabPointTransform != null)
//         {
//             float lerpSpeed = 10f;
//             Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
//             objectRigidbody.MovePosition(newPosition);
//         }
//     }

//     public void Activate()
//     {
//         // Check if the object is tagged as "Log"
//         if (gameObject.CompareTag("Log"))
//         {
//             activated = true;
//         }
//     }
// }
