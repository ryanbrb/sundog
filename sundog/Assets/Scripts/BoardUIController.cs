using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardUIController : MonoBehaviour
{
//	public GameObject basePrefab;
//
//	//private TangoPointCloud m_pointCloud;
//	private GameObject baseObject;
//	private GameController gameController;
//  public static bool isEmpire;
//  public static bool isRebel;
//
//  void Start ()
//	{
//		//m_pointCloud = FindObjectOfType<TangoPointCloud> ();
//		Canvas.SetActive (false);
//		isEmpire = false;
//		isRebel = true;
//		_empireHealth = 100.0f;
//		_rebelHealth = 100.0f;
//	}
//
//	void Update ()
//	{
//		if (Input.touchCount == 1) {
//			// Trigger place base function when single touch ended.
//			Touch t = Input.GetTouch (0);
//			if (t.phase == TouchPhase.Ended && baseObject == null) {
//				PlaceBase (t.position);
//			}
//		} else if (Input.touchCount == 2) {
//			// Move base if two touch
//			Touch t = Input.GetTouch (0);
//			if (t.phase == TouchPhase.Ended && baseObject != null) {
//				MoveBase (t.position);
//			}
//		}
//	}
//
//	void PlaceBase (Vector2 touchPosition)
//	{
//		// Find the plane.
//		Camera cam = Camera.main;
//		Vector3 planeCenter;
//		Plane plane;
//		//if (!m_pointCloud.FindPlane (cam, touchPosition, out planeCenter, out plane)) {
//		//	Debug.Log ("cannot find plane.");
//		//	return;
//		//}
//
//		// Place base on the surface, and make it always face the camera.
//		if (Vector3.Angle (plane.normal, Vector3.up) < 30.0f) {
//			Vector3 up = plane.normal;
//			Vector3 right = Vector3.Cross (plane.normal, cam.transform.forward).normalized;
//			Vector3 forward = Vector3.Cross (right, plane.normal).normalized;
//			baseObject = Instantiate (basePrefab, planeCenter, Quaternion.LookRotation (forward, up));
//			gameController = baseObject.GetComponent<GameController> ();
//			TextLaunch.SetActive (false);
//			Canvas.SetActive (true);
//		} else {
//			Debug.Log ("surface is too steep for base to stand on.");
//		}
//	}
//
//	void MoveBase (Vector2 touchPosition)
//	{
//		// Find the plane.
//		Camera cam = Camera.main;
//		Vector3 planeCenter;
//		Plane plane;
////		if (!m_pointCloud.FindPlane (cam, touchPosition, out planeCenter, out plane)) {
////			Debug.Log ("cannot find plane.");
////			return;
////		}
//
//		// Place base on the surface, and make it always face the camera.
//		if (Vector3.Angle (plane.normal, Vector3.up) < 30.0f) {
//			Vector3 up = plane.normal;
//			Vector3 right = Vector3.Cross (plane.normal, cam.transform.forward).normalized;
//			Vector3 forward = Vector3.Cross (right, plane.normal).normalized;
//			baseObject.transform.SetPositionAndRotation (planeCenter, Quaternion.LookRotation (forward, up));
//		} else {
//			Debug.Log ("surface is too steep for base to stand on.");
//		}
//	}
//
//	public void CommitTurn ()
//	{
//		if (gameController == null) {
//			gameController = GameObject.FindObjectOfType<GameController> ();
//		}
//		gameController.CommitTurn ();
//	}
//		
//	public Image FighterHealthImg;
//	public Image EnemiHealthImg;
//	public Image TimeImg;
//	public GameObject Canvas;
//	public GameObject TextLaunch;
//	public GameObject EmpireFaction;
//	public GameObject RebelFaction;
//	private float _empireHealth;
//	private float _rebelHealth;
//	private bool empireAlreadyReinforce = false;
//	private bool rebelAlreadyReinforce = false;
//
//	public void EmpireAttack ()
//	{
//		isEmpire = true;
//		isRebel = false;
//		EmpireFaction.SetActive (false);
//		RebelFaction.SetActive (false);
//	}
//
//	public void RebelAttack ()
//	{
//		isEmpire = false;
//		isRebel = true;
//		EmpireFaction.SetActive (false);
//		RebelFaction.SetActive (false);
//	}
//
//	public void EmpireHealth(float damage)
//	{
//		_empireHealth += damage;
//		FighterHealthImg.fillAmount = (_empireHealth / 100.0f);
//
//		if(FighterHealthImg.fillAmount < 0.5f && !empireAlreadyReinforce)
//		{
//			FighterHealthImg.fillAmount = 1.0f;
//			_empireHealth = 100.0f;
//			gameController.Reinforcement("empire");
//			empireAlreadyReinforce = true;
//		}
//	}
//
//	public void RebelHealth(float damage)
//	{
//		_rebelHealth += damage;
//		EnemiHealthImg.fillAmount = (_rebelHealth / 100.0f);
//
//		if(EnemiHealthImg.fillAmount < 0.5f && !rebelAlreadyReinforce)
//		{
//			EnemiHealthImg.fillAmount = 1.0f;
//			_rebelHealth = 100.0f;
//			gameController.Reinforcement("rebel");
//			rebelAlreadyReinforce = true;
//		}
//	}
//
//	public void TimeManager(float time)
//	{
//		TimeImg.fillAmount = (time / 5.0f);
//	}
}