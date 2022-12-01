using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnActiveButton : MonoBehaviour
{
    public GameObject item;

    void Start()
    {
        Invoke("UnAtive", 0.01f);
    }

    void UnAtive()
    {
        this.gameObject.SetActive(false);
    }

}
