using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using NaughtyAttributes;

namespace ArreTools.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> statesDictionary;
        
        public float timeToStart = 1f;


        private StateBase _currentState;
        public StateBase CurrentState
        {
            get { return _currentState; }
        }

        public void Init()
        {
            statesDictionary = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            statesDictionary.Add(typeEnum, state);    
        }

        public void SwitchState(T state, params object[] objs)
        {
            //Debug.Log(objs[0] + " ARRE " + state);
            if (_currentState != null)
                _currentState.OnStateExit();

            _currentState = statesDictionary[state];
            _currentState.OnStateEnter(objs);
            Debug.Log("Current State = " + _currentState);
        }

        void Update()
        {
            if (_currentState != null)
                _currentState.OnStateStay();   
        }
    }

}
