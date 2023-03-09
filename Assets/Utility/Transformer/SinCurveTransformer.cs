using System;
using UnityEngine;

namespace Chibi929
{
  public class SinCurveTransformer : MonoBehaviour
  {
    [Serializable]
    private struct SettingParameter
    {
      public float amplitude;
      public float speed;
    }

    [Serializable]
    private struct SettingVector3
    {
      public SettingParameter x;
      public SettingParameter y;
      public SettingParameter z;
    }

    [SerializeField]
    private SettingVector3 posParams = new SettingVector3
    {
      y = new SettingParameter { amplitude = 1, speed = 1 }
    };

    [SerializeField]
    private SettingVector3 rotParams = new SettingVector3
    {
      x = new SettingParameter { amplitude = 10, speed = 1 },
      y = new SettingParameter { amplitude = 30, speed = 1 },
      z = new SettingParameter { amplitude = 20, speed = 1 }
    };

    private void Update()
    {
      UpdatePosition();
      UpdateRotation();
    }

    private void UpdatePosition()
    {
      var posX = posParams.x.amplitude * Mathf.Sin(Time.time * posParams.x.speed);
      var posY = posParams.y.amplitude * Mathf.Sin(Time.time * posParams.y.speed);
      var posZ = posParams.z.amplitude * Mathf.Sin(Time.time * posParams.z.speed);
      transform.localPosition = new Vector3(posX, posY, posZ);
    }

    private void UpdateRotation()
    {
      var rotX = rotParams.x.amplitude * Mathf.Sin(Time.time * rotParams.x.speed);
      var rotY = rotParams.y.amplitude * Mathf.Sin(Time.time * rotParams.y.speed);
      var rotZ = rotParams.z.amplitude * Mathf.Sin(Time.time * rotParams.z.speed);
      transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
    }
  }
}
