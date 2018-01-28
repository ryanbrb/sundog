using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorSettings : MonoBehaviour {

  public float tinyMaximumTreshold = 1f;
  public float shortMaximumXThreshold = 5.0f;
  public float mediumMaximumXThreshold = 10.0f;
  public float longMaximumXThreshold = 20.0f;
    //anything bigger can be considered of extrea long
    //public float extraLongMaximumXthreshold = 45.0f;

    //click effects
    public GameObject tinyClickPrefab = null;
    public GameObject shortClickPrefab = null;
  public GameObject mediumClickPrefab = null;
  public GameObject longClickPrefab = null;
  public GameObject extraLongClickPrefab = null;

    //whistle effects
    public GameObject tinyWhistlePrefab = null;
    public GameObject shortWhistlePrefab = null;
  public GameObject mediumWhistlePrefab = null;
  public GameObject longWhistlePrefab = null;
  public GameObject extraLongWhistlePrefab = null;

    //stomp effects
    public GameObject tinyStompPrefab = null;
    public GameObject shortStompPrefab = null;
  public GameObject mediumStompPrefab = null;
  public GameObject longStompPrefab = null;
  public GameObject extraLongStompPrefab = null;

    //bark effects
    public GameObject tinyBarkPrefab = null;
    public GameObject shortBarkPrefab = null;
  public GameObject mediumBarkPrefab = null;
  public GameObject longBarkPrefab = null;
  public GameObject extraLongBarkPrefab = null;

    //monster effects
    public GameObject tinyMonsterPrefab = null;
    public GameObject shortMonsterPrefab = null;
    public GameObject mediumMonsterPrefab = null;
  public GameObject longMonsterPrefab = null;
  public GameObject extraLongMonsterPrefab = null;

  // Use this for initialization
  void Start () {
    SetAllReflectorEffectValues();
  }

  void SetAllReflectorEffectValues() 
  {
    Reflector[] reflectorsInLevel = GameObject.FindObjectsOfType<Reflector>();
    foreach (Reflector reflectorInstance in reflectorsInLevel) 
    {
      //in case we have other reflectors in level, we should just check to see
      //if these are in fact walls.      
      if(reflectorInstance.name.Contains("ReflectingWall")) 
      {
        //this may or may not use the local scale
        bool bIsTinyPiece = (reflectorInstance.transform.localScale.x < tinyMaximumTreshold);
        bool bIsShortPiece = (reflectorInstance.transform.localScale.x < shortMaximumXThreshold);
        bool bisMediumPiece = (reflectorInstance.transform.localScale.x < mediumMaximumXThreshold);
        bool bIsLongPiece = (reflectorInstance.transform.localScale.x < longMaximumXThreshold);
        bool bIsExtraLongPiece = (bIsShortPiece || bisMediumPiece || bIsLongPiece) ? false : true;

        if (bIsTinyPiece)
        {
            if (tinyClickPrefab != null) { reflectorInstance.clickEffect = tinyClickPrefab; }
            if (tinyWhistlePrefab != null) { reflectorInstance.whistleEffect = tinyWhistlePrefab; }
            if (tinyStompPrefab != null) { reflectorInstance.stompEffect = tinyStompPrefab; }
            if (tinyBarkPrefab != null) { reflectorInstance.barkEffect = tinyBarkPrefab; }
            if (tinyMonsterPrefab != null) { reflectorInstance.monsterEffect = tinyMonsterPrefab; }
        }
        
      }
    }
  }
}
