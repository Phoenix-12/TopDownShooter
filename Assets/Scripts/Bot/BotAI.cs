using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(IControllable))]
public class BotAI : MonoBehaviour
{
    [SerializeField] private Transform _targetEnemy;
    [SerializeField]private NavMeshAgent _navMeshAgent;

    [SerializeField] private IControllable _controllable;
    [SerializeField] private State _state;

    [SerializeField] private GameObject _targetVisual;

    [SerializeField] private TMP_Text _stateText;

    private Vector3 _targetAim;
    
    private float _timer = 0;
    private float _reloadTimer = 0f;
    private float _reloadTime = 1f;

    enum State
    {
        Idle,
        SeeTarget,
        SeeReflect,
        Aim,
        Shoot,
        Move
    }

    public void SetControllable(IControllable conrollable)
    {
        _controllable = conrollable;
    }

    private void Awake()
    {
        _controllable = GetComponent<IControllable>();
        
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updatePosition = false;

        _state = State.Idle;
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            default:
            case State.Idle:
                _state = State.Move;
                break;

            case State.SeeTarget:
                _state = State.Aim;
                break;

            case State.SeeReflect:
                _state = State.Aim;
                break;

            case State.Aim:
                if (IsAimOnTarget())
                    _state = State.Shoot;
                else if (IsSeeTarget() || IsSeeReflect())
                    AimOnTarget(_targetEnemy.position);
                else
                    _state = State.Move;
                break;

            case State.Move:
                if (IsSeeTarget())
                    _state = State.SeeTarget;
                else if (IsSeeReflect()) 
                    _state = State.SeeReflect;

                MoveTowardTarget();

                AimOnTarget(_targetAim);
                if (_timer > 0.02f)
                {
                    UpdateNavMeshAgent();
                    _timer = 0f;
                }
                else
                    _timer += Time.fixedDeltaTime;
                
                break;

            case State.Shoot:
                if (_reloadTimer > _reloadTime)
                {
                    _controllable.Shoot();
                    _reloadTimer = 0f;
                }
                _state = State.Move;
                break;
        }
        _reloadTimer += Time.fixedDeltaTime;
        _stateText.text = _state.ToString();
    }

    private void UpdateNavMeshAgent()
    {
        Vector3[] res = new Vector3[3];
        _navMeshAgent.path.GetCornersNonAlloc(res);
        if (Vector3.Distance(transform.position, res[1]) < 1f)
        {
            _navMeshAgent.Warp(res[1]);
        }
        else
        {
            _navMeshAgent.Warp(transform.position);
        }
        _navMeshAgent.SetDestination(_targetEnemy.position);
    }

    private void MoveTowardTarget()
    {
        Vector3[] res = new Vector3[3];
        _navMeshAgent.path.GetCornersNonAlloc(res);
        _targetAim = _navMeshAgent.steeringTarget;
        _targetVisual.transform.position = _navMeshAgent.steeringTarget;
    }

    private bool IsSeeTarget()
    {
        if (Vector3.Distance(_targetAim, _targetEnemy.position) < 1f)
        {
            return true;
        }
        return false;
    }

    private bool IsSeeReflect()
    {
        return false;
    }

    private void AimOnTarget(Vector3 target)
    {
        Vector3 aimVec = (transform.position - target).normalized;
        float angle = Mathf.Atan2(aimVec.y, aimVec.x) * Mathf.Rad2Deg + 90f;
        float deltaAnlge = 5.0f;

        int current_rot = (int)transform.rotation.eulerAngles.z;

        if (angle < 0)
            angle = 180 + angle;
        else
            angle = angle - 180;
        current_rot -= 180;

        if (Vector3.Angle(-transform.up, aimVec) < deltaAnlge)
        {
            _controllable.Turn(0);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
            _controllable.Move(new Vector2(0, 0.5f));
        }
        else if (IsLeftVector(aimVec, -transform.up))
        {
            _controllable.Turn(-1);
        }
        else
        {
            _controllable.Turn(1);
        }
    }

    private bool IsLeftVector(Vector3 main, Vector3 additional)
    {
        if (main.x * additional.y - additional.x * main.y > 0)
            return true;
        return false;
    }

    private bool IsAimOnTarget()
    {
        Vector3 aimVec = (transform.position - _targetAim).normalized;
        if (Vector3.Angle(-transform.up, aimVec) < 3f)
            return true;
        return false;
    }
}
