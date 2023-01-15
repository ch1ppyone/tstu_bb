using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type {Lancer, Saber, Rider}

[CreateAssetMenu(fileName = "UnitData", menuName = "Game/Unit")]
public class UnitData : ScriptableObject
{
    [SerializeField] private Type _type ;
    [SerializeField] private float _hp ;
    [SerializeField] private float _power;
    
    [SerializeField] private float _speed;
    
    [SerializeField] private Type _atkup ;
    [SerializeField] private Type _defdown ;
    
    public Type type => _type;

    public float hp => _hp;

    public float power => _power;
    public float  speed => _speed;
    
    public Type atkup => _atkup;
    public Type defdown => _defdown;
    
}