using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform m_target;
	public float m_distance = 12.0f;
	public float m_xSpeed = 200.0f;
	public float m_ySpeed = 200.0f;
	public float m_zoomSpeed = 500.0f;
	public float m_minDistance = 2.5f;
	public float m_maxDistance = 20.0f;
	public float m_maxXDeg = 60.0f;
	
	private float m_xDeg = 0.0f;
	private float m_yDeg = 0.0f;
	private float m_rotateSpeed = 5.0f;

	void Start() {
		Vector3 angles = transform.eulerAngles;
		m_xDeg = angles.x;
		m_yDeg = angles.y;
	}		

	void LateUpdate() {
		//Gets the mouse position
		m_xDeg += Input.GetAxis("Mouse X") * m_rotateSpeed;
        m_yDeg -= Input.GetAxis("Mouse Y") * m_rotateSpeed;
		float horizontal = Input.GetAxis("Mouse X") * m_rotateSpeed;

		//Lets scrollwheel zoom in and out
		m_distance += -(Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * m_zoomSpeed * Mathf.Abs(m_distance);

		//sets the minimum scroll distance between camera and player
		if (m_distance < m_minDistance) {
            m_distance = m_minDistance;
        }

		//sets the maximum scroll distance between camera and player
        if (m_distance > m_maxDistance) {
            m_distance = m_maxDistance;
        }
		
		//sets the amount the camera can move above and below the player
		if(m_yDeg >= m_maxXDeg) {
			m_yDeg = m_maxXDeg;
		} else if (m_yDeg <= -m_maxXDeg) {
			m_yDeg = -m_maxXDeg;
		}

		//Rotates the camera to mouse position
		Quaternion rotation = Quaternion.Euler(m_yDeg, m_xDeg, 0);
		Vector3 position = rotation * new Vector3(0.0f, 2.0f, -m_distance) + m_target.position;
		transform.rotation = rotation;
		transform.position = position;

		//rotates the player to camera's rotation
		m_target.transform.Rotate(0, horizontal, 0);
	}
}