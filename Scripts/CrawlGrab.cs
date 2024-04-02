using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CrawlGrab : MonoBehaviour
{
    public float crawlSpeed = 1.0f;
    public OVRInput.Controller inputSource; // Set this to the appropriate hand (LeftHand or RightHand)
    public float grabRange = 0.5f; // Set the grabbing range

    private bool isGrabbing = false;
    private Vector3 initialGrabPosition;
    private Vector3 previousHandPosition;
    Vector3 currentGrabPosition;

    [SerializeField] GameObject parentObject;





    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, inputSource) & (isGrabbing != true)) //make sure it isnt already true
        {

            Debug.Log("Grabbing");
            if (TryGrabCrawlableObject())
            {
                Debug.Log("GRABBING INSIDE");
                isGrabbing = true;
                initialGrabPosition = GetControllerPosition();
                previousHandPosition = GetControllerPosition();
            }

        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, inputSource))
        {
            // Stop grabbing
            isGrabbing = false;
        }


        //if (isGrabbing)
        //{

            //currentGrabPosition = GetControllerPosition();

            // Calculate the movement based on the change in hand position
            //Vector3 movement = currentGrabPosition - previousHandPosition;

            // Apply movement to the player GameObject
            //transform.Translate(-movement * crawlSpeed);

            // Update the previous hand position for the next frame
            //previousHandPosition = currentGrabPosition;


        //}

        if (isGrabbing)
        {

            currentGrabPosition = GetControllerPosition();

            // Calculate the movement based on the change in hand position
            Vector3 movement = currentGrabPosition - previousHandPosition;

            // Apply movement to the player GameObject
            parentObject.transform.Translate(-movement * crawlSpeed);

            // Update the previous hand position for the next frame
            previousHandPosition = currentGrabPosition;


        }

    }

    private bool TryGrabCrawlableObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, grabRange);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("crawlable"))
            {
                return true;
            }
        }

        return false;
    }


    private Vector3 GetControllerPosition()
    {
        // Get the position of the specified hand controller
        return OVRInput.GetLocalControllerPosition(inputSource);
    }
}