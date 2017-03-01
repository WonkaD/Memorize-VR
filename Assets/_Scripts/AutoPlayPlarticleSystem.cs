using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayPlarticleSystem : MonoBehaviour
{
    public GameObject Particle;

    void OnEnable()
    {
        Instantiate(Particle, gameObject.transform).transform.localPosition= Vector3.zero;
    }

}
