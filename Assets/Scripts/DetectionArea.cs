using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    [SerializeField] private GameObject father;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.ToLower() == "player") 
        {
            father.GetComponent<AIController>().SetTarget(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.ToLower() == "player")
        {
            father.GetComponent<AIController>().SetTarget(other.gameObject);
        }
    }
}
