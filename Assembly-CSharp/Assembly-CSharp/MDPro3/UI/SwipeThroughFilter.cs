using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x02001410 RID: 5136
	public class SwipeThroughFilter : MonoBehaviour, ICanvasRaycastFilter
	{
		// Token: 0x06009487 RID: 38023 RVA: 0x00152F99 File Offset: 0x00151199
		private void Start()
		{
			this.parent = base.GetComponentInParent<SwipeArea>();
		}

		// Token: 0x06009488 RID: 38024 RVA: 0x00152FA8 File Offset: 0x001511A8
		public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
		{
			if (UserInput.MouseLeftDown)
			{
				if (this.parent != null)
				{
					this.parent.OnPointerDown(new BaseEventData(EventSystem.current));
				}
			}
			else if (UserInput.MouseLeftUp && this.parent != null)
			{
				this.parent.OnPointerUp(new BaseEventData(EventSystem.current));
			}
			return true;
		}

		// Token: 0x0400D2D2 RID: 53970
		private SwipeArea parent;
	}
}
