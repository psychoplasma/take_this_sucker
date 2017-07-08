using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponManager : MonoBehaviour {
	public GameObject WeaponHolderLayer;
	public GameObject WeaponHolder;
	public GameObject Weapon;
	public GameObject ScoreManager;
	public Text AmmoCount;
	public float WeaponThrowingForce;
	public float CalibrationConst = 0.2f; 	// must be less than 0.5f. 0.5f means that the player grabs the weapon at top. 
	public int totalAmmo = 20;
	public bool IsGameFinished {
		get {
			return isGameFinished;
		}
	}

	private const int MAX_NUM_WEAPON = 10;
	private GameObject weaponInstance;
	private Rigidbody weaponInstanceRigidBody;
	private Queue weaponQueue = new Queue();
	private bool dragged = false;
	private Transform weaponHolderTransform;
	private Vector3 initialPosition;
	private bool isGameFinished = false;

	private float weaponHolderWorldHeight;
	private float weaponHolderScreenHeight;
	private float distToWeaponHolder;
	private Vector3 leftBorder;
	private Vector3 rightBorder;
	private Vector3 downBorder;
	private Vector3 upBorder;

	private static WeaponManager weaponManager;

	public static WeaponManager Instance() {
		return weaponManager;
	}

	void Awake() {
		if(!weaponManager) {
			weaponManager = this;
		}

		updateAmmoCountText();
	}

	void Start() {
		weaponHolderTransform = WeaponHolder.GetComponent<Transform>();
		initialPosition = weaponHolderTransform.position;
		distToWeaponHolder = Vector3.Distance(weaponHolderTransform.position, Camera.main.transform.position);			
		leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, distToWeaponHolder));
		rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, distToWeaponHolder));
		downBorder = leftBorder;
		upBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.7f, distToWeaponHolder));

		weaponHolderWorldHeight = WeaponHolder.GetComponent<BoxCollider>().size.y * weaponHolderTransform.localScale.y;
		weaponHolderScreenHeight = Camera.main.WorldToScreenPoint(new Vector3 (0.0f, weaponHolderWorldHeight, distToWeaponHolder)).y;
//		Debug.Log ("Weapon height in world: " + weaponHolderWorldHeight + "\n\r on screen: " + weaponHolderScreenHeight);
	}

	void Update() {
		if (!isGameFinished) {

			#if UNITY_EDITOR
			
			if (Input.GetMouseButtonDown(0)) {
				if (!EventSystem.current.IsPointerOverGameObject()) {
					dragged = isWeaponHolderDragged(Input.mousePosition);
				}
			}

			if (Input.GetMouseButton(0)) {
				if (dragged) {
					moveWeapon(Input.mousePosition);
				}
			}
			
			if (Input.GetMouseButtonUp(0)) {
				if (dragged) {
					Ray ray = adjustWeaponHolderWorldPos(Input.mousePosition);
					ShootWeapon (ray);
					dragged = false;
				}
			}
			#endif

			#if UNITY_ANDROID

			if (Input.touchCount > 0) {
				Touch touch = Input.GetTouch (0);
				TouchPhase phase = touch.phase;

				switch (phase) {
				case TouchPhase.Began:
					if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId)) {
						dragged = isWeaponHolderDragged(touch.position);
					}
					break;

				case TouchPhase.Moved:
					if (dragged) {
						moveWeapon(touch.position);
					}
					break;

				case TouchPhase.Ended:
					if (dragged) {
						Ray ray = adjustWeaponHolderWorldPos(touch.position);
						ShootWeapon(ray);
						dragged = false;
					}
					break;
				}
			}
			#endif
		}
	}

	private bool isWeaponHolderDragged(Vector3 touchPos) {
		bool retVal = false;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (touchPos);

		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider.gameObject.name == "WeaponHolder") {
				retVal = true;
			}
		}

		return retVal;
	}

	private Ray adjustWeaponHolderWorldPos(Vector3 touchPos) {
		Vector3 pos = new Vector3 (touchPos.x, touchPos.y + (weaponHolderScreenHeight * (0.65f - CalibrationConst)));
		Ray ray = Camera.main.ScreenPointToRay(pos);

		return ray;
	}

	private void ShootWeapon(Ray ray) {
		// Hide the representation of weapon
		// untill the weapon hits somewhere else.
		WeaponHolder.SetActive(false);

		// Instantiate a new weapon to shoot.
		Vector3 pos = ray.origin;
		Quaternion rot = Quaternion.LookRotation(ray.direction);
		weaponInstance = Instantiate(Weapon, pos, rot) as GameObject;
		weaponInstance.transform.Rotate(Vector3.left * 90);
		weaponInstance.name = "Weapon";

		// Add a collision handler script to recenlty instantiated weapon.
		CollisionHandler colHandler = weaponInstance.AddComponent<CollisionHandler>() as CollisionHandler;
		colHandler.SetWeaponHolder(WeaponHolder);
		colHandler.SetScoreManager(ScoreManager);


		// Add force to the rigidbody of weapon to shoot it.
		weaponInstanceRigidBody = weaponInstance.GetComponent<Rigidbody>();
		weaponInstanceRigidBody.AddForce(ray.direction * WeaponThrowingForce, ForceMode.Acceleration);

		// Set the initial position after shooting.
		weaponHolderTransform.position = initialPosition;

		// Destroy the first weapon at a time if the 
		// number of total weapon exceeds MAX_NUM_WEAPON.
		weaponCollector(weaponInstance);

		// Update the remaining ammo.
		updateAmmo();
	}
		
	private void weaponCollector(GameObject weapon) {
		weaponQueue.Enqueue(weapon);

		if(weaponQueue.Count >= MAX_NUM_WEAPON) {
			GameObject garbage = weaponQueue.Dequeue() as GameObject;
			Transform parent = garbage.GetComponent<Transform> ().parent;

			if (parent != null && parent.gameObject != null) {
				Destroy (parent.gameObject);
			}
			Destroy(garbage);
		}
	}
		
	private void moveWeapon(Vector3 touchPos) {
		Vector3 pos = weaponHolderTransform.position;
		Ray ray = Camera.main.ScreenPointToRay(touchPos);
		RaycastHit hit;
		int layerMask = 1 << 8;
		Vector3 destPos;
		Vector3 calibVector = new Vector3(0.0f, weaponHolderWorldHeight * CalibrationConst, 0.0f);

		if(dragged) {
			WeaponHolderLayer.SetActive(true);
			if(Physics.Raycast(ray,out hit, 10.0f, layerMask))
			{
				if(hit.collider.gameObject.name == "WeaponHolderLayer") {
					destPos = boundInsideBox(pos, hit.point - calibVector);
					weaponHolderTransform.Translate(0, pos.y - destPos.y,  destPos.x - pos.x);
				}
			}
			WeaponHolderLayer.SetActive(false);
		}
	}

	private Vector2 boundInsideBox(Vector3 pos, Vector3 dest) {
		Vector3 calPos = new Vector3(dest.x, dest.y, dest.z);

		float w = (rightBorder.x-leftBorder.x) / 20;
		float h = 0.0f;

		if((dest.x < leftBorder.x + w) || (dest.x > rightBorder.x - w)){
			calPos.x = pos.x;

		}
		if((dest.y <  downBorder.y + h) || (dest.y > upBorder.y - h)){
			calPos.y = pos.y;

		}

		return calPos;
	}

	private void updateAmmo() {
		totalAmmo--;
		updateAmmoCountText();

		if (totalAmmo == 0) {
			isGameFinished = true;
		}

	}

	private void updateAmmoCountText() {
		AmmoCount.text = totalAmmo.ToString();
	}
}
