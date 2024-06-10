// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PickupDrop : MonoBehaviour
// {
//     [SerializeField] private Transform playerCameraTransform;
//     [SerializeField] private LayerMask pickUpLayerMask;
//     [SerializeField] private Transform ObjectGrabPointTransform;

//     private ObjectGrabable objectGrabable;
//     private AudioSource audioSource; // Reference to AudioSource component

//     public PotionBar potionBar; // Reference to the PotionBar script

//     private bool isCoroutineRunning = false; // Flag to check if the coroutine is already running

//     private void Start()
//     {
//         audioSource = GetComponent<AudioSource>();
//         // audioSource.Pause();
//     }

//     private void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.F))
//         {
//             if (objectGrabable == null)
//             {
//                 float pickupDistance = 6f;
//                 if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickUpLayerMask))
//                 {
//                     if (raycastHit.transform.TryGetComponent(out objectGrabable))
//                     {
//                         objectGrabable.Grab(ObjectGrabPointTransform);
//                          // Play pickup sound
//                     }
//                 }
//             }
//             else
//             {
//                 objectGrabable.Drop();
//                 objectGrabable = null;
//             }
//         }

//         // if (Input.GetKeyDown(KeyCode.T) && objectGrabable != null && objectGrabable.CompareTag("Log") && potionBar.slider.value > 0 && !isCoroutineRunning)
//         // {
//         //     // PlaySound();
//         //     StartCoroutine(DelayedAction());
//         // }
//     }

//     // private IEnumerator DelayedAction()
//     // {
//     //     // isCoroutineRunning = true;

//     //     // yield return new WaitForSeconds(2f); // Wait for 2 seconds

//     //     // fireScript.TriggerFire(objectGrabable.gameObject);
//     //     // potionBar.ReduceSpellBar();

//     //     // isCoroutineRunning = false; // Reset flag after coroutine is finished
//     // }

//     private void PlaySound()
//     {
//         Debug.Log("Hoy na");
//         if (audioSource != null)
//         {
//             audioSource.Play();
//         }
//     }
// }
