using UnityEngine;

public class MissileScript : MonoBehaviour {

	public GameObject smokeTrail;
	public GameObject explosion;
	private GameObject player;
	private Rigidbody rb;

	public float speed;
	private float rotationSpeed = 1.2f;
	private float time = 0f;

	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {

		rb.velocity = (transform.up) * speed;

		InstantiateSmokeTrail();

		Vector3 direction = (player.transform.position - transform.position).normalized;
		float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
		Quaternion rotateToTarget = Quaternion.Euler(90, angle, 0);

		transform.rotation = Quaternion.Slerp(transform.rotation, rotateToTarget, Time.deltaTime * rotationSpeed);

		Destroy(gameObject, 20f);
	}

	private void InstantiateSmokeTrail() {
		time += Time.deltaTime;
		if (time > 0.1f) {
			Instantiate(smokeTrail, transform.position, smokeTrail.transform.rotation);
			time = 0f;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Enemy") {
			Instantiate(explosion, transform.position, explosion.transform.rotation);
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
		else if (other.tag == "Player") {
			// TODO: Morte do player
		}
	}
}