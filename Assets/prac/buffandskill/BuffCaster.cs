using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCaster : MonoBehaviour
{
    public GameObject reciever;
    // Start is called before the first frame update
    void Start()
    {
        BuffTest testBuff = reciever.AddComponent<BuffTest>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
