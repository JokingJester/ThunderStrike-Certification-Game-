using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParent : MonoBehaviour
{
    [SerializeField] private float _time;
    // Start is called before the first frame update
    void Start()
    {
        if (_time == 0)
            Destroy(this.gameObject, 3f);
        else
            Destroy(this.gameObject, _time);
    }
}
