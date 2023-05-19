﻿using System;
using System.Collections.Generic;

namespace Task.Switch.Structure.HFSM
{
    public interface IDeclarative<TStateObject>
    {
        State<TStateObject> Initialize(Action<TStateObject> init);
        State<TStateObject> Enter(Action<TStateObject> enter);
        State<TStateObject> Update(Action<TStateObject> update);
        State<TStateObject> Exit(Action<TStateObject> exit);
        
        Transition<TStateObject> Translate(Func<TStateObject,bool> valid);
        Transition<TStateObject> To(int id);
        public int Id { set; get; }
    }
    
    public class StateMachineBuilder<TStateObject>
    {
        private Stack<StateMachineBuilder<TStateObject>> m_BuilderStack;
        private Stack<IDeclarative<TStateObject>> m_DeclarativeStack;
        private StateMachine<TStateObject> m_Machine;
        public StateMachineBuilder(StateMachine<TStateObject> machine)
        {
            m_DeclarativeStack = new Stack<IDeclarative<TStateObject>>();
            m_Machine = machine;
        }

        public void InitializeBuilderStack()
        {
            m_BuilderStack = new Stack<StateMachineBuilder<TStateObject>>();
            m_BuilderStack.Push(this);
        }

        public StateMachineBuilder<TStateObject> NewState(int id)
        {
            State<TStateObject> state = new State<TStateObject>(id);
            m_Machine.AddState(state);
            m_DeclarativeStack.Push(state);
            return this;
        }

        public StateMachineBuilder<TStateObject> NewStateMachine(int id)
        {
            StateMachine<TStateObject> machine = new StateMachine<TStateObject>(id);
            m_Machine.AddState(machine);
            m_DeclarativeStack.Push(machine);
            return this;
        }
        public StateMachineBuilder<TStateObject> Builder
        {
            get
            {
                StateMachine<TStateObject> stateMachine =  m_DeclarativeStack.Peek() as StateMachine<TStateObject>;
                if (stateMachine != null)
                {
                    StateMachineBuilder<TStateObject> builder = stateMachine.Builder;
                    builder.m_BuilderStack = m_BuilderStack;
                    builder.m_BuilderStack.Push(this);
                    return builder;
                }                  
                return this;
            }
        }

        public StateMachineBuilder<TStateObject> Build()
        {
            m_Machine.OnInitialize(m_Machine.GetStateObject());   
            return m_BuilderStack.Pop();
        }

        public StateMachineBuilder<TStateObject> SetDefault(int id)
        {
            m_Machine.SetDefault(id);
            return this;
        }

        public StateMachineBuilder<TStateObject> Initialize(Action<TStateObject> init)
        {
            if(m_DeclarativeStack.TryPeek(out IDeclarative<TStateObject> state))
                state.Initialize(init);
            return this;
        }

        public StateMachineBuilder<TStateObject> Enter(Action<TStateObject> enter)
        {
            if (m_DeclarativeStack.TryPeek(out IDeclarative<TStateObject> state))
                state.Enter(enter);
            return this;
        }

        public StateMachineBuilder<TStateObject> Update(Action<TStateObject> update)
        {
            if (m_DeclarativeStack.TryPeek(out IDeclarative<TStateObject> state))
                state.Update(update);
            return this;
        }

        public StateMachineBuilder<TStateObject> Exit(Action<TStateObject> exit)
        {
            if (m_DeclarativeStack.TryPeek(out IDeclarative<TStateObject> state))
                state.Exit(exit);
            return this;
        }

        public StateMachineBuilder<TStateObject> When(Func<TStateObject,bool> valid)
        {
            if(m_DeclarativeStack.TryPeek(out IDeclarative<TStateObject> state))
            {
                Transition<TStateObject> transition = new Transition<TStateObject>(state.Id,int.MinValue,valid);
                m_Machine.AddTransition(transition);
                m_DeclarativeStack.Push(transition);
            }
            return this;
        }

        public StateMachineBuilder<TStateObject> Any(Func<TStateObject,bool> valid)
        {
            for(int i = 0; i < m_Machine.StateCount; ++i)
            {
                Transition<TStateObject> transition = new Transition<TStateObject>(m_Machine.GetStateAt(i).Id,int.MinValue,valid);
                m_Machine.AddTransition(transition);
                m_DeclarativeStack.Push(transition);
            }
            return this;
        }

        public StateMachineBuilder<TStateObject> Where(int fromId, Func<TStateObject, bool> valid)
        {
            for (int i = 0; i < m_Machine.StateCount; ++i)
            {
                int stateId = m_Machine.GetStateAt(i).Id;
                if((fromId & stateId) == stateId)
                {
                    Transition<TStateObject> transition = new Transition<TStateObject>(m_Machine.GetStateAt(i).Id, int.MinValue, valid);
                    m_Machine.AddTransition(transition);
                    m_DeclarativeStack.Push(transition);
                }              
            }
            return this;
        }

        public StateMachineBuilder<TStateObject> To(int id)
        {
            while (m_DeclarativeStack.TryPeek(out IDeclarative<TStateObject> transition) && transition.GetType() == typeof(Transition<TStateObject>))
            {
                if(id != transition.Id)
                    transition.To(id);
                m_DeclarativeStack.Pop();
            }
            return this;
        }

        public StateMachineBuilder<TStateObject> End()
        {
            m_DeclarativeStack.Pop();
            return this;
        }
    }
}