using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013AE RID: 5038
	[RequireComponent(typeof(TMP_InputField))]
	public class SelectionInputField : SelectionButton
	{
		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x060091FE RID: 37374 RVA: 0x00145870 File Offset: 0x00143A70
		public TMP_InputField InputField
		{
			get
			{
				return this.m_InputField = ((this.m_InputField != null) ? this.m_InputField : base.GetComponent<TMP_InputField>());
			}
		}

		// Token: 0x060091FF RID: 37375 RVA: 0x001458A2 File Offset: 0x00143AA2
		public override void OnPointerExit(PointerEventData eventData)
		{
			this.hovering = false;
			this.pressing = false;
			if (!this.InputField.isFocused)
			{
				this.OnExit();
			}
		}

		// Token: 0x06009200 RID: 37376 RVA: 0x001458C8 File Offset: 0x00143AC8
		protected override void HoverOn()
		{
			base.HoverOn();
			GameObject hover = base.Manager.GetElement("ButtonHover");
			if (hover != null)
			{
				CanvasGroup cg;
				if (hover.TryGetComponent<CanvasGroup>(out cg))
				{
					cg.alpha = 1f;
				}
				DOTweenAnimation animation;
				if (hover.TryGetComponent<DOTweenAnimation>(out animation))
				{
					animation.DORestart();
				}
			}
		}

		// Token: 0x06009201 RID: 37377 RVA: 0x0014591C File Offset: 0x00143B1C
		protected override void HoverOff(bool forced = false)
		{
			base.HoverOff(forced);
			GameObject hover = base.Manager.GetElement("ButtonHover");
			if (hover != null)
			{
				DOTweenAnimation animation;
				if (hover.TryGetComponent<DOTweenAnimation>(out animation))
				{
					animation.DOPause();
				}
				CanvasGroup cg;
				if (hover.TryGetComponent<CanvasGroup>(out cg))
				{
					cg.alpha = 0f;
				}
			}
		}

		// Token: 0x06009202 RID: 37378 RVA: 0x00145970 File Offset: 0x00143B70
		public virtual void SetSubmitEvent(UnityAction<string> call)
		{
			InputField inputField = base.Selectable as InputField;
			if (inputField != null)
			{
				inputField.onSubmit.AddListener(call);
			}
		}

		// Token: 0x06009203 RID: 37379 RVA: 0x00145998 File Offset: 0x00143B98
		protected override void OnNavigation(AxisEventData eventData)
		{
			if (eventData.moveDir == MoveDirection.Up)
			{
				if (this.InputField.isFocused)
				{
					int num;
					if (int.TryParse(this.InputField.text, out num))
					{
						this.InputField.text = (num + this.upNum).ToString();
					}
					return;
				}
				if (this.navigationEvent.onUpNavigation.GetPersistentEventCount() > 0)
				{
					this.navigationEvent.onUpNavigation.Invoke();
					return;
				}
			}
			else if (eventData.moveDir == MoveDirection.Down)
			{
				if (this.InputField.isFocused)
				{
					int num2;
					if (int.TryParse(this.InputField.text, out num2))
					{
						this.InputField.text = (num2 - this.upNum).ToString();
					}
					return;
				}
				if (this.navigationEvent.onDownNavigation.GetPersistentEventCount() > 0)
				{
					this.navigationEvent.onDownNavigation.Invoke();
					return;
				}
			}
			else if (eventData.moveDir == MoveDirection.Left)
			{
				if (this.InputField.isFocused)
				{
					int num3;
					if (int.TryParse(this.InputField.text, out num3))
					{
						this.InputField.text = (num3 - this.rightNum).ToString();
					}
					return;
				}
				if (this.navigationEvent.onLeftNavigation.GetPersistentEventCount() > 0)
				{
					this.navigationEvent.onLeftNavigation.Invoke();
					return;
				}
			}
			else if (eventData.moveDir == MoveDirection.Right)
			{
				if (this.InputField.isFocused)
				{
					int num4;
					if (int.TryParse(this.InputField.text, out num4))
					{
						this.InputField.text = (num4 + this.rightNum).ToString();
					}
					return;
				}
				if (this.navigationEvent.onRightNavigation.GetPersistentEventCount() > 0)
				{
					this.navigationEvent.onRightNavigation.Invoke();
					return;
				}
			}
			base.OnNavigation(eventData);
		}

		// Token: 0x0400D098 RID: 53400
		[Header("SelectionButton_InputField")]
		[SerializeField]
		private int upNum = 10;

		// Token: 0x0400D099 RID: 53401
		[SerializeField]
		private int rightNum = 1;

		// Token: 0x0400D09A RID: 53402
		private TMP_InputField m_InputField;
	}
}
