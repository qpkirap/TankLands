using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MapContainer : MonoBehaviour
    {
        [SerializeField] private Transform playerPoint;
        [SerializeField] private List<Transform> enemyPoints;
        
        public Transform PlayerPoint => playerPoint;
        public IReadOnlyList<Transform> EnemyPoints => enemyPoints;
    }
}