using UnityEngine;

namespace PJL.AbilitySystem
{
    public abstract class SetAnimatorValue : AbilityEffect
    {
        [SerializeField] protected string _animatorKey;
        [SerializeField] private bool _setOnRemove;

        protected abstract void Set(Animator animator);
        protected abstract void RemoveSet(Animator animator);

        public override void Apply(IAbilityTarget target)
        {
            if (target.GameObject == null) return;
            var animator = target.GameObject.GetComponent<Animator>();
            if (animator == null) return;
            Set(animator);
        }

        public override void Remove(IAbilityTarget target)
        {
            if (!_setOnRemove) return;
            if (target.GameObject == null) return;
            var animator = target.GameObject.GetComponent<Animator>();
            if (animator == null) return;
            RemoveSet(animator);
        }
    }

    public class SetAnimatorTrigger : AbilityEffect
    {
        [SerializeField] private string _animatorKey;
        [SerializeField] private bool _resetOnRemove;

        public override void Apply(IAbilityTarget target)
        {
            if (target.GameObject == null) return;
            var animator = target.GameObject.GetComponent<Animator>();
            if (animator == null) return;

            animator.SetTrigger(_animatorKey);
        }

        public override void Remove(IAbilityTarget target)
        {
            if (!_resetOnRemove) return;
            if (target.GameObject == null) return;
            var animator = target.GameObject.GetComponent<Animator>();
            if (animator == null) return;

            animator.ResetTrigger(_animatorKey);
        }
    }

    public class SetAnimatorValueBool : SetAnimatorValue
    {
        [SerializeField] private bool _value, _valueOnRemove;

        protected override void Set(Animator animator) => animator.SetBool(_animatorKey, _value);

        protected override void RemoveSet(Animator animator) => animator.SetBool(_animatorKey, _valueOnRemove);
    }

    public class SetAnimatorValueFloat : SetAnimatorValue
    {
        [SerializeField] private float _value, _valueOnRemove;

        protected override void Set(Animator animator) => animator.SetFloat(_animatorKey, _value);

        protected override void RemoveSet(Animator animator) => animator.SetFloat(_animatorKey, _valueOnRemove);
    }

    public class SetAnimatorValueInt : SetAnimatorValue
    {
        [SerializeField] private int _value, _valueOnRemove;

        protected override void Set(Animator animator) => animator.SetInteger(_animatorKey, _value);

        protected override void RemoveSet(Animator animator) => animator.SetInteger(_animatorKey, _valueOnRemove);
    }
}
