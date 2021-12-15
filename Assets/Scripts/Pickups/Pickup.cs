using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is the base class for other pick up scripts to inherit from
/// </summary>
public class Pickup : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The effect to create when this pickup is collected")]
    public GameObject pickUpEffect;
    public float respawnTime = 5f;

    /// <summary>
    /// Description:
    /// Standard unity function called when a trigger is entered by another collider
    /// Input:
    /// Collider collision
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="collision">The collider that has entered the trigger</param>
    private void OnTriggerEnter(Collider collision)
    {
        DoOnPickup(collision);
    }

    /// <summary>
    /// Description:
    /// Function called when this pickup is picked up
    /// Inputs: Collider collision
    /// Outputs: N/A
    /// </summary>
    /// <param name="collision">The collider that is picking up this pickup</param>
    public virtual void DoOnPickup(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if (pickUpEffect != null)
            {
                Instantiate(pickUpEffect, transform.position, Quaternion.identity, null);
            }
            if (this.gameObject.tag == "Respawn")
            {
                StartCoroutine(waiter(respawnTime, this.gameObject));
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator waiter(float seconds, GameObject respawn)
    {
        if (respawn.activeInHierarchy)
        {
            respawn.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
            respawn.GetComponent<Collider>().enabled = false;
        }
        yield return new WaitForSeconds(seconds);
        respawn.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
        respawn.GetComponent<Collider>().enabled = true;
    }
}
