using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{  
  public class UtilClass{

    public const int sortingOrderDefault = 5000;
    public static float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    public static Vector3 GetMouseWorldPosition(){
      Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
      vec.z = 0f;
      return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ() {
      return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) {
      return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) {
      Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
      return worldPosition;
    }
    public static Vector3 GetRandomDir() {
      return new Vector3(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f,1f)).normalized;
    }

    public static Vector3 GetRandomPos(){
      return new Vector3(Random.Range(-0.5f,0.5f),Random.Range(-0.5f,0.5f));
    }

    public delegate void Callback();
    public static IEnumerator Wait(float duration, Callback callback){
      yield return new WaitForSeconds(duration);
      if(callback != null){
        callback();
      }
      
    }

  }
}

