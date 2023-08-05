using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowShake : MonoBehaviour
{
    void Awake() {
        Vector3 offset = new Vector3(1, -1, 0);
        transform.DOMove(transform.position + offset, 1f, false).SetLoops(999999999).SetEase(Ease.InExpo);
    }
}
