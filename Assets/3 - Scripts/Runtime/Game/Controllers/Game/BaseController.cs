using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class BaseController : MonoBehaviour
    {
        public async virtual UniTask Init(Dictionary<Type, object> injections)
        {
        }
    }
}