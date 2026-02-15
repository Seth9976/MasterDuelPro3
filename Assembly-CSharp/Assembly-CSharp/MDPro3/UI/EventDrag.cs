using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x0200137E RID: 4990
	public class EventDrag : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x06009069 RID: 36969 RVA: 0x0013C0EC File Offset: 0x0013A2EC
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				if (eventData.button == PointerEventData.InputButton.Right)
				{
					Action<PointerEventData> action = this.onBeginDragRight;
					if (action == null)
					{
						return;
					}
					action(eventData);
				}
				return;
			}
			Action<PointerEventData> action2 = this.onBeginDrag;
			if (action2 == null)
			{
				return;
			}
			action2(eventData);
		}

		// Token: 0x0600906A RID: 36970 RVA: 0x0013C122 File Offset: 0x0013A322
		public void OnDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				if (eventData.button == PointerEventData.InputButton.Right)
				{
					Action<PointerEventData> action = this.onDragRight;
					if (action == null)
					{
						return;
					}
					action(eventData);
				}
				return;
			}
			Action<PointerEventData> action2 = this.onDrag;
			if (action2 == null)
			{
				return;
			}
			action2(eventData);
		}

		// Token: 0x0600906B RID: 36971 RVA: 0x0013C158 File Offset: 0x0013A358
		public void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				if (eventData.button == PointerEventData.InputButton.Right)
				{
					Action<PointerEventData> action = this.onEndDragRight;
					if (action == null)
					{
						return;
					}
					action(eventData);
				}
				return;
			}
			Action<PointerEventData> action2 = this.onEndDrag;
			if (action2 == null)
			{
				return;
			}
			action2(eventData);
		}

		// Token: 0x0600906C RID: 36972 RVA: 0x0013C18E File Offset: 0x0013A38E
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				if (eventData.button == PointerEventData.InputButton.Right)
				{
					Action<PointerEventData> action = this.onClickRight;
					if (action == null)
					{
						return;
					}
					action(eventData);
				}
				return;
			}
			Action<PointerEventData> action2 = this.onClick;
			if (action2 == null)
			{
				return;
			}
			action2(eventData);
		}

		// Token: 0x0600906D RID: 36973 RVA: 0x0013C1C4 File Offset: 0x0013A3C4
		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				if (eventData.button == PointerEventData.InputButton.Right)
				{
					Action<PointerEventData> action = this.onPointerDownRight;
					if (action == null)
					{
						return;
					}
					action(eventData);
				}
				return;
			}
			Action<PointerEventData> action2 = this.onPointerDown;
			if (action2 == null)
			{
				return;
			}
			action2(eventData);
		}

		// Token: 0x0600906E RID: 36974 RVA: 0x0013C1FA File Offset: 0x0013A3FA
		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				if (eventData.button == PointerEventData.InputButton.Right)
				{
					Action<PointerEventData> action = this.onPointUpRight;
					if (action == null)
					{
						return;
					}
					action(eventData);
				}
				return;
			}
			Action<PointerEventData> action2 = this.onPointUp;
			if (action2 == null)
			{
				return;
			}
			action2(eventData);
		}

		// Token: 0x0600906F RID: 36975 RVA: 0x0013C230 File Offset: 0x0013A430
		public void OnPointerEnter(PointerEventData eventData)
		{
			Action<PointerEventData> action = this.onPointerEnter;
			if (action == null)
			{
				return;
			}
			action(eventData);
		}

		// Token: 0x06009070 RID: 36976 RVA: 0x0013C243 File Offset: 0x0013A443
		public void OnPointerExit(PointerEventData eventData)
		{
			Action<PointerEventData> action = this.onPointerExit;
			if (action == null)
			{
				return;
			}
			action(eventData);
		}

		// Token: 0x0400CF17 RID: 53015
		public Action<PointerEventData> onBeginDrag;

		// Token: 0x0400CF18 RID: 53016
		public Action<PointerEventData> onEndDrag;

		// Token: 0x0400CF19 RID: 53017
		public Action<PointerEventData> onDrag;

		// Token: 0x0400CF1A RID: 53018
		public Action<PointerEventData> onClick;

		// Token: 0x0400CF1B RID: 53019
		public Action<PointerEventData> onPointerDown;

		// Token: 0x0400CF1C RID: 53020
		public Action<PointerEventData> onPointUp;

		// Token: 0x0400CF1D RID: 53021
		public Action<PointerEventData> onBeginDragRight;

		// Token: 0x0400CF1E RID: 53022
		public Action<PointerEventData> onEndDragRight;

		// Token: 0x0400CF1F RID: 53023
		public Action<PointerEventData> onDragRight;

		// Token: 0x0400CF20 RID: 53024
		public Action<PointerEventData> onClickRight;

		// Token: 0x0400CF21 RID: 53025
		public Action<PointerEventData> onPointerDownRight;

		// Token: 0x0400CF22 RID: 53026
		public Action<PointerEventData> onPointUpRight;

		// Token: 0x0400CF23 RID: 53027
		public Action<PointerEventData> onPointerEnter;

		// Token: 0x0400CF24 RID: 53028
		public Action<PointerEventData> onPointerExit;
	}
}
