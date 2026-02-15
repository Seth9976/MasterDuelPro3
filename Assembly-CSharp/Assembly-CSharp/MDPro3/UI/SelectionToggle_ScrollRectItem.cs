using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x020013D0 RID: 5072
	public class SelectionToggle_ScrollRectItem : SelectionToggle
	{
		// Token: 0x060092FA RID: 37626 RVA: 0x0014AD4B File Offset: 0x00148F4B
		protected override void OnDisable()
		{
			base.OnDisable();
			this.Cancel();
		}

		// Token: 0x060092FB RID: 37627 RVA: 0x0014662E File Offset: 0x0014482E
		protected override void Awake()
		{
			base.Awake();
			this.exclusiveToggle = true;
			this.canToggleOffSelf = false;
			this.toggleWhenSelected = true;
		}

		// Token: 0x060092FC RID: 37628 RVA: 0x0014AD59 File Offset: 0x00148F59
		protected override void ToggleOn()
		{
			base.ToggleOn();
			base.Manager.GetElement<RectTransform>("Offset").DOAnchorPosX(48f, this.switchTime, false).SetEase(Ease.OutQuart);
		}

		// Token: 0x060092FD RID: 37629 RVA: 0x0014AD8A File Offset: 0x00148F8A
		protected override void ToggleOff()
		{
			base.ToggleOff();
			base.Manager.GetElement<RectTransform>("Offset").DOAnchorPosX(0f, this.switchTime, false).SetEase(Ease.OutQuart);
		}

		// Token: 0x060092FE RID: 37630 RVA: 0x0014ADBB File Offset: 0x00148FBB
		public virtual void ToggleOnNow()
		{
			this.isOn = true;
			base.Manager.GetElement<RectTransform>("Offset").DOAnchorPosX(48f, 0f, false);
		}

		// Token: 0x060092FF RID: 37631 RVA: 0x0014ADE5 File Offset: 0x00148FE5
		public virtual void ToggleOffNow()
		{
			this.isOn = false;
			base.Manager.GetElement<RectTransform>("Offset").DOAnchorPosX(0f, 0f, false);
		}

		// Token: 0x06009300 RID: 37632 RVA: 0x0014AE0F File Offset: 0x0014900F
		public virtual void Dispose()
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06009301 RID: 37633 RVA: 0x0014AE1C File Offset: 0x0014901C
		public virtual void Refresh()
		{
			this.Cancel();
			if (base.gameObject.activeInHierarchy)
			{
				this.cts = new CancellationTokenSource();
				this.RefreshAsync();
			}
		}

		// Token: 0x06009302 RID: 37634 RVA: 0x0014AE44 File Offset: 0x00149044
		protected virtual async UniTask RefreshAsync()
		{
			this.refreshed = false;
			await UniTask.Yield();
			this.refreshed = true;
		}

		// Token: 0x06009303 RID: 37635 RVA: 0x0014AE88 File Offset: 0x00149088
		private void Cancel()
		{
			try
			{
				CancellationTokenSource cancellationTokenSource = this.cts;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
				CancellationTokenSource cancellationTokenSource2 = this.cts;
				if (cancellationTokenSource2 != null)
				{
					cancellationTokenSource2.Dispose();
				}
			}
			finally
			{
				this.cts = null;
			}
		}

		// Token: 0x06009304 RID: 37636 RVA: 0x0014AED4 File Offset: 0x001490D4
		protected override void OnNavigation(AxisEventData eventData)
		{
			if (this.simpleMove)
			{
				if (eventData.moveDir == MoveDirection.Up)
				{
					for (int i = 0; i < base.transform.parent.childCount; i++)
					{
						if (base.transform.parent.GetChild(i).GetComponent<SelectionToggle_ScrollRectItem>().index == this.index - 1)
						{
							UserInput.NextSelectionIsAxis = true;
							EventSystem.current.SetSelectedGameObject(base.transform.parent.GetChild(i).gameObject);
							return;
						}
					}
					return;
				}
				if (eventData.moveDir == MoveDirection.Down)
				{
					for (int j = 0; j < base.transform.parent.childCount; j++)
					{
						if (base.transform.parent.GetChild(j).GetComponent<SelectionToggle_ScrollRectItem>().index == this.index + 1)
						{
							UserInput.NextSelectionIsAxis = true;
							EventSystem.current.SetSelectedGameObject(base.transform.parent.GetChild(j).gameObject);
							return;
						}
					}
					return;
				}
			}
			else
			{
				base.OnNavigation(eventData);
			}
		}

		// Token: 0x0400D15D RID: 53597
		[HideInInspector]
		public bool refreshed;

		// Token: 0x0400D15E RID: 53598
		protected float switchTime = 0.2f;

		// Token: 0x0400D15F RID: 53599
		protected bool simpleMove = true;

		// Token: 0x0400D160 RID: 53600
		protected CancellationTokenSource cts;
	}
}
