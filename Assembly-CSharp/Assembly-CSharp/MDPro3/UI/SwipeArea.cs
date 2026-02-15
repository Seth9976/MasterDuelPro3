using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x0200140F RID: 5135
	public class SwipeArea : MonoBehaviour, ICanvasRaycastFilter
	{
		// Token: 0x06009482 RID: 38018 RVA: 0x00152EB4 File Offset: 0x001510B4
		public void OnPointerDown(BaseEventData data)
		{
			this.startTouchPosition = UserInput.MousePos;
			this.stopTouch = false;
		}

		// Token: 0x06009483 RID: 38019 RVA: 0x00152EC8 File Offset: 0x001510C8
		public void OnPointerUp(BaseEventData data)
		{
			this.stopTouch = true;
			this.DetectSwipe();
		}

		// Token: 0x06009484 RID: 38020 RVA: 0x00152ED8 File Offset: 0x001510D8
		private void DetectSwipe()
		{
			if (this.stopTouch)
			{
				this.currentTouchPosition = UserInput.MousePos;
				Vector2 distance = this.currentTouchPosition - this.startTouchPosition;
				if (distance.magnitude > this.swipeRange && Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
				{
					if (distance.x > 0f)
					{
						UnityEvent onSwipeRight = this.OnSwipeRight;
						if (onSwipeRight == null)
						{
							return;
						}
						onSwipeRight.Invoke();
						return;
					}
					else if (distance.x < 0f)
					{
						UnityEvent onSwipeLeft = this.OnSwipeLeft;
						if (onSwipeLeft == null)
						{
							return;
						}
						onSwipeLeft.Invoke();
					}
				}
			}
		}

		// Token: 0x06009485 RID: 38021 RVA: 0x00152F6C File Offset: 0x0015116C
		public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
		{
			if (UserInput.MouseLeftUp)
			{
				this.OnPointerUp(new BaseEventData(EventSystem.current));
			}
			return true;
		}

		// Token: 0x0400D2CC RID: 53964
		public UnityEvent OnSwipeLeft;

		// Token: 0x0400D2CD RID: 53965
		public UnityEvent OnSwipeRight;

		// Token: 0x0400D2CE RID: 53966
		private Vector2 startTouchPosition;

		// Token: 0x0400D2CF RID: 53967
		private Vector2 currentTouchPosition;

		// Token: 0x0400D2D0 RID: 53968
		private bool stopTouch;

		// Token: 0x0400D2D1 RID: 53969
		public float swipeRange = 50f;
	}
}
