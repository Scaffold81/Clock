using Core.Clock.Interfaces;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Clock
{
    public abstract class ClockArrowInputBase : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        protected RectTransform arrow;

        protected IClockModel model;
        protected Vector2 originalMousePosition;
        protected float rotateSpeed = 1.0f;

        public void Init(IClockModel clockModel)
        {
            model = clockModel;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!model.IsTimeToSet) return;
            originalMousePosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!model.IsTimeToSet) return;

            var difference = eventData.position - originalMousePosition;
            var rotationAmount = difference.x * rotateSpeed;

            arrow.Rotate(new Vector3(0f, 0f, rotationAmount));
            originalMousePosition = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!model.IsTimeToSet) return;
            var date = model.LocalDateTime;

            var rotationValue = GetRotationValue();
            var timeUnit = GetTimeUnit();

            UpdateDateTime(date, rotationValue, timeUnit);
        }

        protected abstract float GetRotationValue();
        protected abstract int GetTimeUnit();
        protected abstract void UpdateDateTime(DateTime date, float rotationValue, int timeUnit);
    }
}

