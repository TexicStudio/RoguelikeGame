using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherButton : MonoBehaviour
{
    private GameObject _cellObject;

    void Start()
    {
        _cellObject = transform.parent.parent.gameObject;
    }


    public void Gather()
    {

    }
}
