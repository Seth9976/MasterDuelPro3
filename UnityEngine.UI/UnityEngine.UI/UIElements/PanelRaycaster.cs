using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngine.UIElements
{
	// Token: 0x02000095 RID: 149
	[AddComponentMenu("UI Toolkit/Panel Raycaster (UI Toolkit)")]
	public class PanelRaycaster : BaseRaycaster, IRuntimePanelComponent
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x000188BA File Offset: 0x00016ABA
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x000188C4 File Offset: 0x00016AC4
		public IPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				BaseRuntimePanel newPanel = (BaseRuntimePanel)value;
				if (this.m_Panel != newPanel)
				{
					this.UnregisterCallbacks();
					this.m_Panel = newPanel;
					this.RegisterCallbacks();
				}
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000188F4 File Offset: 0x00016AF4
		private void RegisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed += this.OnPanelDestroyed;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00018915 File Offset: 0x00016B15
		private void UnregisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed -= this.OnPanelDestroyed;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00018936 File Offset: 0x00016B36
		private void OnPanelDestroyed()
		{
			this.panel = null;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001893F File Offset: 0x00016B3F
		private GameObject selectableGameObject
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel == null)
				{
					return null;
				}
				return panel.selectableGameObject;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00018952 File Offset: 0x00016B52
		public override int sortOrderPriority
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				return Mathf.FloorToInt((panel != null) ? panel.sortingPriority : 0f);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0001896F File Offset: 0x00016B6F
		public override int renderOrderPriority
		{
			get
			{
				int maxValue = int.MaxValue;
				int s_ResolvedSortingIndexMax = UIElementsRuntimeUtility.s_ResolvedSortingIndexMax;
				BaseRuntimePanel panel = this.m_Panel;
				return maxValue - (s_ResolvedSortingIndexMax - ((panel != null) ? panel.resolvedSortingIndex : 0));
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00018990 File Offset: 0x00016B90
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			int displayIndex = this.m_Panel.targetDisplay;
			Vector3 eventPosition = MultipleDisplayUtilities.GetRelativeMousePositionForRaycast(eventData);
			if ((int)eventPosition.z != displayIndex)
			{
				return;
			}
			Vector3 position = eventPosition;
			Vector2 delta = eventData.delta;
			float h = (float)Screen.height;
			if (displayIndex > 0 && displayIndex < Display.displays.Length)
			{
				h = (float)Display.displays[displayIndex].systemHeight;
			}
			position.y = h - position.y;
			delta.y = -delta.y;
			EventSystem eventSystem = UIElementsRuntimeUtility.activeEventSystem as EventSystem;
			if (eventSystem == null || eventSystem.currentInputModule == null)
			{
				return;
			}
			int pointerId = eventSystem.currentInputModule.ConvertUIToolkitPointerId(eventData);
			IEventHandler capturingElement = this.m_Panel.GetCapturingElement(pointerId);
			VisualElement ve = capturingElement as VisualElement;
			if (ve != null && ve.panel != this.m_Panel)
			{
				return;
			}
			IPanel capturingPanel = PointerDeviceState.GetPlayerPanelWithSoftPointerCapture(pointerId);
			if (capturingPanel != null && capturingPanel != this.m_Panel)
			{
				return;
			}
			if (capturingElement == null && capturingPanel == null)
			{
				Vector2 panelPosition;
				Vector2 vector;
				if (!this.m_Panel.ScreenToPanel(position, delta, out panelPosition, out vector, false))
				{
					return;
				}
				if (this.m_Panel.Pick(panelPosition) == null)
				{
					return;
				}
			}
			resultAppendList.Add(new RaycastResult
			{
				gameObject = this.selectableGameObject,
				module = this,
				screenPosition = eventPosition,
				displayIndex = this.m_Panel.targetDisplay
			});
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0001513D File Offset: 0x0001333D
		public override Camera eventCamera
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040002A9 RID: 681
		private BaseRuntimePanel m_Panel;
	}
}
