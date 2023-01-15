using System;
using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitState _state;
    [SerializeField] private bool _reverse;
    [SerializeField] UnitData data;

    public Action<float> onDamage;

    private Transform _path;
    private Transform[] _waypoints;

    private int _currentPoint;

    [SerializeField] private float _hp;
    private Animator _animator;

    [SerializeField] private Unit _currentTarget;
    [SerializeField] private Castle _targetCastle;

    

    private enum UnitState
    {
        Run,
        Attack,
        Death
    }


    private void Awake()
    {
        this.onDamage += Damage;
    }

    private void Start()
    {
        this._hp = data.hp;

        _path = Game.Instance.path;
        _animator = GetComponent<Animator>();


        WayPointsPrepare();

        _state = UnitState.Run;
    }


    private void Update()
    {
        if (_currentTarget == null)
        {
            _animator.SetTrigger("Run");
            Move();
        }

        if (_state == UnitState.Attack)
        {
            
        }


    }


    private void WayPointsPrepare()
    {
        _waypoints = new Transform [_path.childCount];

        for (var i = 0; i < _path.childCount; i++) _waypoints[i] = _path.GetChild(i);

        if (_reverse)
            Array.Reverse(_waypoints);
    }


    private void Move()
    {
        if (_currentPoint >= _waypoints.Length)
            _currentPoint = _waypoints.Length - 1;


        Transform target = _waypoints[_currentPoint];

        transform.position =
            Vector3.MoveTowards(transform.position, target.position, (data.speed / 10) * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint++;
        }
    }


    IEnumerator OnTriggerStay(Collider collision)
    {
      
        
        float damage = 0;
        
        if (this.gameObject.layer != collision.gameObject.layer)
        {
            
            _animator.SetTrigger("Attack");
            
            if (collision.TryGetComponent(out Unit unit))
            {
                this._currentTarget = unit;
                damage = CalculateDamage(_currentTarget.data.type);
                _currentTarget.onDamage?.Invoke(damage);
            }

            if (collision.TryGetComponent(out Castle castle))
            {
                this._targetCastle = castle;
                damage = data.power;
                yield return new WaitForSeconds(2f);
                _targetCastle.onDamage?.Invoke(damage);
            }
        }
    }


    public void Damage(float damage)
    {
        if (this._hp <= 0)
            Death();

        this._hp -= damage;
    }

    public void Death()
    {
        _animator.SetTrigger("Death");
        Destroy(this.gameObject);
    }

    public void Idle()
    {
        _animator.SetTrigger("Idle"); 
    }

    private float CalculateDamage(Type targetType)
    {
        float damage = 0;
        if (data.type == targetType)
            damage = data.power;
        else if (data.atkup == targetType)
            damage = data.power * 2;
        else damage = data.power / 2;

        return damage;
    }
}