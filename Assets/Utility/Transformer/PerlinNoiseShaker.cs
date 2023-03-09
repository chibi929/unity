using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chibi929
{
  public class PerlinNoiseShaker : MonoBehaviour
  {
    [SerializeField]
    private float _shakeTime = 0.2f;

    [SerializeField]
    private float _amplitude = 1.0f;

    // 初期位置を保持しておく変数
    private Vector3 _initLocalPos;

    private bool _isShaking = false;
    private float _elapsedTime = 0.0f;

    private void Awake()
    {
      _initLocalPos = transform.localPosition;
    }

    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        _elapsedTime = 0.0f;
        _isShaking = true;
      }
    }

    private void LateUpdate()
    {
      if (_isShaking)
      {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime < _shakeTime)
        {
          UpdatePosition();
        }
        else
        {
          _isShaking = false;
          transform.localPosition = _initLocalPos;
        }
      }
    }

    private void UpdatePosition()
    {
      var noiseX = GetNoise();
      var noiseY = GetNoise();
      transform.localPosition = _initLocalPos + new Vector3(noiseX, noiseY, 0);
    }

    private float GetNoise()
    {
      var offset = Random.Range(0.0f, 256.0f);

      // パーリンノイズは 0～1 の値を返すので、-0.5 して -0.5～0.5 の間に補正する
      var correctionValue = Mathf.PerlinNoise(Time.time + offset, 0) - 0.5f;

      // 補正後の値を2倍することで -1～1 の間の値を得られるようにする
      return _amplitude * correctionValue * 2;
    }
  }
}
