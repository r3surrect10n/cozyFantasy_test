using UnityEngine;

public class RaycastHandler : MonoBehaviour
{
    [Header("Distance settings")]
    [SerializeField, Range(0, 10)] private float _interactionDistance;
    [SerializeField, Range(50, 200)] private float _shootDistance;

    [Header("PlayerCamera")]
    [SerializeField] private Camera _playerCam;

    public RaycastHit ShooterHit {  get; private set; }
    public RaycastHit InteractionHit { get; private set; }
    public Transform CameraTramsform => _playerCam.transform;

    private Ray _playerLook;    

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        PlayerLook();
    }

    private void PlayerLook()
    {
        _playerLook = _playerCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(_playerLook.origin, _playerLook.direction * _shootDistance, Color.green);
        
        if (Physics.Raycast(_playerLook, out RaycastHit hit, _shootDistance))
        {
            ShooterHit = hit;

            if (hit.distance <= _interactionDistance)
                InteractionHit = hit;
            else
                InteractionHit = new RaycastHit();
        }
        else
        {
            ShooterHit = new RaycastHit();
            InteractionHit = new RaycastHit();
        }
    }
}
