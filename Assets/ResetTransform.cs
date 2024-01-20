using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour
{
    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localPosition = Vector3.zero;
        _rectTransform.localScale = Vector3.one;
    }
}
