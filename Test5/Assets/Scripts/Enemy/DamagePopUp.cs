using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{

    public static void Create(Vector3 position, float damageAmount, Color customColor){
      Transform damagePopUpTransform = Instantiate(GameAssets.i.pfDamagePopUp, position, Quaternion.identity);
      DamagePopUp damagePopup = damagePopUpTransform.GetComponent<DamagePopUp>();
      damagePopup.Setup(damageAmount, customColor);
    }



    private static int sortingOrder;
    private const float dissappear_time_max = 1f;
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake(){
      textMesh = transform.GetComponent<TextMeshPro>();
      transform.position = new Vector3(transform.position.x,transform.position.y + 40,transform.position.z);

    }
    public void Setup(float damageAmount, Color? customColor = null){
      textMesh.SetText(damageAmount.ToString());
      //textColor = textMesh.color;
      textColor = customColor ?? textMesh.color;
      textMesh.color = textColor;
      disappearTimer = dissappear_time_max;
      moveVector = new Vector3(0, 20) * 3f;
      sortingOrder ++;
      textMesh.sortingOrder = sortingOrder;
    }

    private void Update(){

      transform.position += moveVector * Time.deltaTime;
      moveVector -= moveVector * 1f * Time.deltaTime;

      if (disappearTimer > dissappear_time_max * 0.5f){
        float increaseScaleAmount = 10f;
        transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
      } else {
        float increaseScaleAmount = 10f;
        transform.localScale -= Vector3.one * increaseScaleAmount * Time.deltaTime;
      }


      disappearTimer -= Time.deltaTime;
      if (disappearTimer < 0){
        float disappearSpeed = 2f;
        textColor.a -= disappearSpeed * Time.deltaTime;
        textMesh.color = textColor;

        if (textColor.a < 0){
          Destroy(gameObject);
        }
      }
    }
}
