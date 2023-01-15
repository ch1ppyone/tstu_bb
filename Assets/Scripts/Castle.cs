using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CastleData _castleData;

    public Action<float> onDamage;
    public Action<string> onDestroy;



    [SerializeField] private float _hp;

    public List<Unit> units;

    private Animator _animator;



    private void Awake()
    {
        this.onDamage += Damage;
        this.onDestroy += Game.Instance.CheckStatus;
   
    }

    public void Start()
    {
        this._hp = _castleData.hp;
        _animator = GetComponent<Animator>();
    }


    public void Update()
    {
        units.RemoveAll(x => x == null);
    }

    public void Spawn(GameObject prefab)
    {
        units.Add(Instantiate(prefab, _spawnPoint.transform.position, prefab.transform.rotation).GetComponent<Unit>());
    }


    public void DestroyAll()
    {
        foreach (var unit in units)
        {
           Destroy(unit.gameObject);
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
        string layer = LayerMask.LayerToName(this.gameObject.layer);
        this.onDestroy?.Invoke(layer);
        _animator.SetTrigger("Death");
    }
}