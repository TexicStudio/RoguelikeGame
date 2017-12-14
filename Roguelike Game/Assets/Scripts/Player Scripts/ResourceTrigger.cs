using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTrigger : MonoBehaviour
{
    public GameObject GatherButton;



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            return;
        if (other.tag == "Not Passable Obj")
        {
            GameObject gatherButtonClone = Instantiate(GatherButton, other.transform, false);
            gatherButtonClone.name = "Gather Button Canvas For " + other.name;
        }
        //Debug.Log("child goes ouch!");
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Not Passable Obj")
        {
            int lastChild = other.transform.childCount - 1;
            Destroy(other.transform.GetChild(lastChild).gameObject);
        }
    }
}
