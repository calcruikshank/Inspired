using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetTransform : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = targetTransform.position;
    }
}
