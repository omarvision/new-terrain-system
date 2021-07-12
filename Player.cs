using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    public float LookSensitivity = 90.0f;
    public float JumpForce = 5.0f;
    private Camera cam = null;
    private Rigidbody rb = null;
    private Renderer rend = null;

    private void Start()
    {
        rend = this.GetComponent<Renderer>();
        rb = this.GetComponent<Rigidbody>();
        rb.angularDrag = 10.0f; //dampen spin when hit

        //Note: for this part to work right, camera has to be a child
        cam = this.transform.GetComponentInChildren<Camera>();
        cam.transform.position = rend.bounds.center;
        cam.transform.Translate(Vector3.up * rend.bounds.size.x);
        cam.transform.rotation = this.transform.rotation;

        //lock mouse to game screen (we are going to control look around)
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        //move across X, Z plane (ground)
        float horz = Input.GetAxis("Horizontal");  // -1 ... 1
        float vert = Input.GetAxis("Vertical");
        this.transform.Translate(Vector3.right * horz * MoveSpeed * Time.deltaTime);
        this.transform.Translate(Vector3.forward * vert * MoveSpeed * Time.deltaTime);

        //look around
        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");
        this.transform.localRotation *= Quaternion.AngleAxis(mousex * LookSensitivity * Time.deltaTime, Vector3.up);
        cam.transform.localRotation *= Quaternion.AngleAxis(mousey * LookSensitivity * Time.deltaTime, Vector3.left);

        //jump
        if (Input.GetButtonDown("Jump") == true) //space
        {
            rb.AddRelativeForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        //unlock mouse from gamescreen
        if (Input.GetButtonDown("Cancel") == true) //esc
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}