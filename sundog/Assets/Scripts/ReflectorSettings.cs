using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorSettings : MonoBehaviour {

  public float shortMaximumXThreshold = 5.0f;
  public float mediumMaximumXThreshold = 10.0f;
  public float longMaximumXThreshold = 20.0f;
  //anything bigger can be considered of extrea long
  //public float extraLongMaximumXthreshold = 45.0f;

  //click effects
  public GameObject shortClickPrefab = null;
  public GameObject mediumClickPrefab = null;
  public GameObject longClickPrefab = null;
  public GameObject extraLongClickPrefab = null;

  //whistle effects
  public GameObject shortWhistlePrefab = null;
  public GameObject mediumWhistlePrefab = null;
  public GameObject longWhistlePrefab = null;
  public GameObject extraLongWhistlePrefab = null;

  //stomp effects
  public GameObject shortStompPrefab = null;
  public GameObject mediumStompPrefab = null;
  public GameObject longStompPrefab = null;
  public GameObject extraLongStompPrefab = null;

  //bark effects
  public GameObject shortBarkPrefab = null;
  public GameObject mediumBarkPrefab = null;
  public GameObject longBarkPrefab = null;
  public GameObject extraLongBarkPrefab = null;

  //monster effects
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
        bool bIsShortPiece = (reflectorInstance.transform.localScale.x < shortMaximumXThreshold);
        bool bisMediumPiece = (reflectorInstance.transform.localScale.x < mediumMaximumXThreshold);
        bool bIsLongPiece = (reflectorInstance.transform.localScale.x < longMaximumXThreshold);
        bool bIsExtraLongPiece = (bIsShortPiece || bisMediumPiece || bIsLongPiece) ? true : false;

        if(bIsShortPiece) 
        {
          if (shortClickPrefab != null) { reflectorInstance.clickEffect = shortClickPrefab; }
          if (shortWhistlePrefab != null) { reflectorInstance.whistleEffect = shortWhistlePrefab; }
          if (shortStompPrefab != null) { reflectorInstance.stompEffect = shortStompPrefab; }
          if (shortBarkPrefab != null) { reflectorInstance.barkEffect = shortBarkPrefab; }
          if (shortMonsterPrefab != null) { reflectorInstance.monsterEffect = shortMonsterPrefab; }
        }

        else if (bisMediumPiece) 
        {
          if (mediumClickPrefab != null) { reflectorInstance.clickEffect = mediumClickPrefab; }
          if (mediumWhistlePrefab != null) { reflectorInstance.whistleEffect = mediumWhistlePrefab; }
          if (mediumStompPrefab != null) { reflectorInstance.stompEffect = mediumStompPrefab; }
          if (mediumBarkPrefab != null) { reflectorInstance.barkEffect = mediumBarkPrefab; }
          if (mediumMonsterPrefab != null) { reflectorInstance.monsterEffect = mediumMonsterPrefab; }
        } 
        
        else if (bIsLongPiece) 
        {
          if (longClickPrefab != null) { reflectorInstance.clickEffect = longClickPrefab; }
          if (longWhistlePrefab != null) { reflectorInstance.whistleEffect = longWhistlePrefab; }
          if (longStompPrefab != null) { reflectorInstance.stompEffect = longStompPrefab; }
          if (longBarkPrefab != null) { reflectorInstance.barkEffect = longBarkPrefab; }
          if (longMonsterPrefab != null) { reflectorInstance.monsterEffect = longMonsterPrefab; }
        } 
        
        else if (bIsExtraLongPiece) {
          if (extraLongClickPrefab != null) { reflectorInstance.clickEffect = extraLongClickPrefab; }
          if (extraLongWhistlePrefab != null) { reflectorInstance.whistleEffect = extraLongWhistlePrefab; }
          if (extraLongStompPrefab != null) { reflectorInstance.stompEffect = extraLongStompPrefab; }
          if (extraLongBarkPrefab != null) { reflectorInstance.barkEffect = extraLongBarkPrefab; }
          if (extraLongMonsterPrefab != null) { reflectorInstance.monsterEffect = extraLongMonsterPrefab; }
        }
      }
    }
  }
}
