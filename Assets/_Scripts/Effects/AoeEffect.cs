using System;
using UnityEngine;

public class AoeEffect : MonoBehaviour {

    [SerializeField] private float destroyTime = .5f;

    private Vector3 _target;
    
    private void Start() {
        Destroy(gameObject, destroyTime);
        _target = transform.lossyScale * 2;
    }

    private void Update() {
        transform.localScale = Vector3.MoveTowards(transform.lossyScale, _target, .1f);
    }
}