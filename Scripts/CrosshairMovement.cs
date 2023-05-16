using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairMovement : MonoBehaviour
{
    [SerializeField] private float crosshairSpeed = 10f;

    private InputMaster _inputMaster;
    private InputAction _mouseDeltaInput;

    private RectTransform _thisObjectTransfrm;
    private Vector2 _screenMiddlePoint;
    private bool _canResetPosition = true;

    private void Awake()
    {
        _inputMaster = new InputMaster();
        _mouseDeltaInput = _inputMaster.Actions.MouseDelta;
        _inputMaster.Actions.HoldCrosshair.performed += _ => _canResetPosition = false;
        _inputMaster.Actions.HoldCrosshair.canceled += _ => _canResetPosition = true;

        _thisObjectTransfrm = GetComponent<RectTransform>();
        _screenMiddlePoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MoveCrosshair();   
    }

    private void MoveCrosshair()
    {
        Vector3 mouseDelta = _mouseDeltaInput.ReadValue<Vector2>();
        _thisObjectTransfrm.position += mouseDelta * crosshairSpeed;
    }

    public void ResetPosition()
    {
        if (!_canResetPosition) return;
        _thisObjectTransfrm.position = _screenMiddlePoint;
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Actions.HoldCrosshair.performed -= _ => _canResetPosition = false;
        _inputMaster.Actions.HoldCrosshair.canceled -= _ => _canResetPosition = true;
        _inputMaster.Disable();
    }
}
