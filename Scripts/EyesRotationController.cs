using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EyesRotationController : MonoBehaviour
{
    private InputMaster _inputMaster;
    private InputAction _leftClickAction;

    [SerializeField] private EyesRotation eyesRotation;
    [SerializeField] private ViewTargetMovement viewTargetMovement;
    [SerializeField] private CrosshairMovement crosshairMovement;
    [SerializeField] private HeadRotation headRotation;

    [SerializeField] private Transform viewTargetPoint;
    [SerializeField] private Transform rayStartPoint;
    [SerializeField] private Transform crosshairTransform;

    [SerializeField] private Camera eyeCamera;

    [SerializeField] private Vector2 sizeToTakeoverView;
    [SerializeField] private float rotationDistance;

    private Vector3 lastPos;
    private Vector2 _screenMiddlePoint;

    private void Awake()
    {
        _inputMaster = new InputMaster();
        _leftClickAction = _inputMaster.Actions.LeftClick;
        _leftClickAction.performed += _ => MoveTargetPoint();
        _inputMaster.Actions.RelaxEyes.performed += _ => RelaxEyes();

        _screenMiddlePoint = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        CheckDistance();

        eyesRotation.RotateEyes(lastPos, viewTargetPoint.position);
        lastPos = viewTargetPoint.position;
    }

    private void CheckDistance()
    {
        if (!eyesRotation.IsRelaxed)
        {
            if (Physics.Linecast(rayStartPoint.position, viewTargetPoint.position, out RaycastHit hitInfo))
            {
                if (hitInfo.transform != viewTargetPoint.parent)
                {
                    Vector2 objectSizeOnScreen = ObjectSizeCalculator.CalculateObjectSizeOnCamera(hitInfo.transform.gameObject, eyeCamera);
                    Debug.Log(hitInfo.transform.name + " " + objectSizeOnScreen);
                    if (objectSizeOnScreen.x >= sizeToTakeoverView.x) MoveTargetPoint();
                }
            }

            if (viewTargetPoint.parent == null)
            {
                MoveTargetPoint();
            }
        }

        if (Vector2.Distance(_screenMiddlePoint, crosshairTransform.position) >= rotationDistance)
        {
            MoveTargetPoint();
        }
    }

    private void MoveTargetPoint()
    {
        Ray ray = eyeCamera.ScreenPointToRay(crosshairTransform.position);
        viewTargetMovement.MoveTarget(ray);

        eyesRotation.IsRelaxed = false;
        crosshairMovement.ResetPosition();
        headRotation.StartResetTimer();
    }

    private void RelaxEyes()
    {
        viewTargetMovement.Reset();
        eyesRotation.IsRelaxed = true;
        lastPos = Vector3.forward * 100f;
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDestroy()
    {
        _inputMaster.Actions.LeftClick.performed -= _ => MoveTargetPoint();
        _inputMaster.Actions.RelaxEyes.performed -= _ => RelaxEyes();
        _inputMaster.Disable();
    }
}
