using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class PlayerInputController : BaseController
    {
        private Controls controls;
        private GameState gameState;

        private InputState inputState => gameState.InputState;

        public override async UniTask Init(Dictionary<Type, object> injections)
        {
            await base.Init(injections);
            
            if (injections.TryGetValue(typeof(GameState), out object gameStateObj)) gameState = (GameState)gameStateObj;
            
            controls ??= new();
            
            controls.Enable();
            
            controls.PC.MoveDown.performed += context => Move(Vector2.down, true);
            controls.PC.MoveLeft.performed += context => Move(Vector2.left, true);
            controls.PC.MoveRight.performed += context => Move(Vector2.right, true);
            controls.PC.MoveUp.performed += context => Move(Vector2.up, true);
            
            controls.PC.MoveDown.canceled += context => Move(Vector2.down, false);
            controls.PC.MoveLeft.canceled += context => Move(Vector2.left, false);
            controls.PC.MoveRight.canceled += context => Move(Vector2.right, false);
            controls.PC.MoveUp.canceled += context => Move(Vector2.up, false);
            
            controls.PC.Target.performed += context => Target(context.ReadValue<Vector2>());
            controls.PC.Target.canceled += context => Target(Vector2.zero);
            
            controls.PC.Fire.performed += context => Fire();
            controls.PC.WeaponNext.performed += context => NextSkill();
            controls.PC.WeaponPrev.performed += context => PreviousSkill();
        }
        
        private void Move(Vector2 value, bool isPress)
        {
            Debug.Log(value + " " + isPress);
            
            inputState.MoveAction(value, isPress);
        }

        private void Target(Vector2 value)
        {
            Debug.Log(value);
        }

        private void Fire()
        {
            Debug.Log("fire");
        }

        private void NextSkill()
        {
            Debug.Log("NextSkill");
        }
        
        private void PreviousSkill()
        {
            Debug.Log("PreviousSkill");
        }

        private void OnDisable()
        {
            controls?.Disable();
        }
    }
}