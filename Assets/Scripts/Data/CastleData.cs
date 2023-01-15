using UnityEngine;

[CreateAssetMenu(fileName = "CastleData", menuName = "Game/Castle")]
public class CastleData : ScriptableObject
{
    [SerializeField] private float _hp ;
    public float hp => _hp;
}
