using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i {
        get {
            if (_i == null) _i = Instantiate(Resources.Load("GameAssets") as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    public Transform pfDamagePopUp;
    public GameObject pfEnemy;
    public GameObject Puff;
    public GameObject Blood;
    public GameObject boom;
    public GameObject icetrail;
    public GameObject flametrail;
    public GameObject dustCloud;
    public GameObject bloodstain1;
    public GameObject questButton;
    public GameObject basicAttack;
}
