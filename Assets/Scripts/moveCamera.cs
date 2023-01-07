using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public float speed;
    public float sensitivity;

    public bool lockView = false;

    Vector2 mousePos;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        transform.Translate(transform.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * speed,Space.World);
        transform.Translate(transform.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed,Space.World);

        if (Input.GetButton("Jump")) transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (Input.GetKey(KeyCode.LeftShift)) transform.Translate(Vector3.down * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.F)) lockView = !lockView;

        if (withinBorders() && !lockView) rotate();
    }

    void rotate()
    {
        //this method makes the camera rotate to follow the mouse
        //Vector3 mousePosition = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f));

        mousePos.x += Input.GetAxis("Mouse X");
        mousePos.y += Input.GetAxis("Mouse Y");

        Camera.main.transform.rotation = Quaternion.Euler(-mousePos.y * sensitivity, mousePos.x * sensitivity, 0);
    }

    bool withinBorders()
    {
        return
        Input.mousePosition.y < Screen.height &&
        Input.mousePosition.y > 0 &&
        Input.mousePosition.x < Screen.width &&
        Input.mousePosition.x > 0;
    }
}
