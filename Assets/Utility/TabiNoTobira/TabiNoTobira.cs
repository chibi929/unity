using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chibi929
{
  public class TabiNoTobira : MonoBehaviour
  {
    [Header("周期")]
    [SerializeField]
    private float _speed = 1.0f;

    [Header("振幅"), Range(0.0f, 2.0f)]
    [SerializeField]
    private float _amplitude = 1.0f;

    [Header("周波数"), Range(0.0f, 5.0f)]
    [SerializeField]
    private float _frequency = 1.0f;

    [Header("継続時間")]
    [SerializeField]
    private float _duration = 5.0f;

    [Header("外側を黒で塗りつぶすかどうか")]
    [SerializeField]
    private bool _fillBlackOutside = false;

    private Material _material;

    private float _elapsedTime = 0.0f;
    private bool _isWarp = false;

    private void Start()
    {
      _material = new Material(Shader.Find("Chibi929/ImageEffectShader/TabiNoTobira"));
    }

    private void Update()
    {
      _material.SetFloat("_Speed", _speed);
      _material.SetFloat("_Frequency", _frequency);
      _material.SetFloat("_FillBlackOutSide", Convert.ToSingle(_fillBlackOutside));
      UpdateWarp();
      // _material.SetFloat("_Amplitude", _amplitude);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
      Graphics.Blit(src, dest, _material);
    }

    private void UpdateWarp()
    {
      if (Input.GetMouseButtonDown(0))
      {
        _isWarp = true;
        _elapsedTime = 0.0f;
      }

      if (_isWarp)
      {
        _elapsedTime += Time.deltaTime;

        var normalizedTime = _elapsedTime / _duration;
        var amplitude = Mathf.Lerp(0.0f, _amplitude, normalizedTime);
        _material.SetFloat("_Amplitude", amplitude);

        if (normalizedTime > 1.0)
        {
          _isWarp = false;
          _material.SetFloat("_Amplitude", 0.0f);
          return;
        }
      }
    }
  }
}
