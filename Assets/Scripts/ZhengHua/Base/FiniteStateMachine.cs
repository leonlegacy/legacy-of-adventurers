using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengHua
{ 
    public class FiniteStateMachine<T, E> : SingtonMono<T> where E : Enum where T : MonoBehaviour
    {
        protected List<E> stateList = new List<E>();

        protected Dictionary<E, Action> EnterActions;
        protected Dictionary<E, Action> UpdateActions;
        protected Dictionary<E, Action> EndActions;

        protected List<E> historyStateList = new List<E>();
        protected int nowStateFrame = 0;
        public E nowState;


        public override void Awake()
        {
            base.Awake();
            EnterActions = new Dictionary<E, Action>();
            UpdateActions = new Dictionary<E, Action>();
            EndActions = new Dictionary<E, Action>();
        }

        public void Register(E targetState, Action enterAction, Action updateAction, Action endAction)
        {
            stateList.Add(targetState);
            EnterActions.TryAdd(targetState, enterAction);
            UpdateActions.TryAdd(targetState, updateAction);
            EndActions.TryAdd(targetState, endAction);
        }

        public void ChangeState(E targetState)
        {
            if (EndActions.TryGetValue(nowState, out Action end))
            {
                end.Invoke();
            }
            nowState = targetState;
        }

        public virtual void Start()
        {
            if(stateList.Count > 0)
            {
                nowState = stateList[0];
            }
        }

        public void Update()
        {
            if (nowStateFrame == 0)
            {
                if(EnterActions.TryGetValue(nowState,out Action enter))
                {
                    enter.Invoke();
                }
            }
            else
            {
                if (UpdateActions.TryGetValue(nowState, out Action update))
                {
                    update.Invoke();
                }
            }
            nowStateFrame++;
        }
    }
}