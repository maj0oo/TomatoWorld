using Assets;
using Assets.Models;
using Assets.Models.Inventory;
using Assets.Models.Pots;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject doors;
    public Camera cam;
    public Text text;
    public Text info;
    public float speed = 12f;
    public float gravity = -30f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    private LookManager lookManager;
    private InventoryManager inventoryManager;
    public static bool busy = false;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = new InventoryManager();
        info.gameObject.SetActive(false);
        InventoryManager.info = info;
        lookManager = new LookManager(text, cam, doors);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (!busy)
        {
            Move();
        }
        lookManager.CheckLook();
        lookManager.ChangeNewPotPlace();
        inventoryManager.UpdateTextInfo();
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            inventoryManager.SwitchTomatoType(scrollType.up);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            inventoryManager.SwitchTomatoType(scrollType.down);
        }
        if(Input.GetKeyDown(KeyCode.E) && PotsManager.isSettingNewPot)
        {
            PotsManager.TrySetNewPot();
        }
    }
    private void Move()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = 0f;
        float z = 0f;

        if (isGrounded)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }
        else
        {
            x = Input.GetAxis("Horizontal") / 2;
            z = Input.GetAxis("Vertical") / 2;
        }

        Vector3 move = move = transform.right * x + transform.forward * z;

        controller.Move(move * (Input.GetKey(KeyCode.LeftShift) ? speed * 2 : speed) * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
