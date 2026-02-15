using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028E RID: 654
	internal abstract class BaseRuntimePanel : Panel
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0004A0D2 File Offset: 0x000482D2
		// (set) Token: 0x060011A8 RID: 4520 RVA: 0x0004A0DC File Offset: 0x000482DC
		public GameObject selectableGameObject
		{
			get
			{
				return this.m_SelectableGameObject;
			}
			set
			{
				bool flag = this.m_SelectableGameObject != value;
				if (flag)
				{
					this.AssignPanelToComponents(null);
					this.m_SelectableGameObject = value;
					this.AssignPanelToComponents(this);
				}
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0004A113 File Offset: 0x00048313
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x0004A11C File Offset: 0x0004831C
		public float sortingPriority
		{
			get
			{
				return this.m_SortingPriority;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_SortingPriority, value);
				if (flag)
				{
					this.m_SortingPriority = value;
					bool flag2 = this.contextType == ContextType.Player;
					if (flag2)
					{
						UIElementsRuntimeUtility.SetPanelOrderingDirty();
					}
				}
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060011AB RID: 4523 RVA: 0x0004A15C File Offset: 0x0004835C
		// (remove) Token: 0x060011AC RID: 4524 RVA: 0x0004A194 File Offset: 0x00048394
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action destroyed;

		// Token: 0x060011AD RID: 4525 RVA: 0x0004A1CC File Offset: 0x000483CC
		protected BaseRuntimePanel(ScriptableObject ownerObject, EventDispatcher dispatcher = null)
			: base(ownerObject, ContextType.Player, dispatcher)
		{
			this.m_RuntimePanelCreationIndex = BaseRuntimePanel.s_CurrentRuntimePanelCounter++;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0004A230 File Offset: 0x00048430
		protected override void Dispose(bool disposing)
		{
			bool disposed = base.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					Action action = this.destroyed;
					if (action != null)
					{
						action();
					}
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060011AF RID: 4527 RVA: 0x0004A26C File Offset: 0x0004846C
		// (remove) Token: 0x060011B0 RID: 4528 RVA: 0x0004A2A4 File Offset: 0x000484A4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action drawsInCamerasChanged;

		// Token: 0x060011B1 RID: 4529 RVA: 0x0004A2D9 File Offset: 0x000484D9
		private void InvokeDrawsInCamerasChanged()
		{
			Action action = this.drawsInCamerasChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0004A2F0 File Offset: 0x000484F0
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0004A308 File Offset: 0x00048508
		internal bool drawsInCameras
		{
			get
			{
				return this.m_DrawsInCameras;
			}
			set
			{
				bool flag = this.m_DrawsInCameras != value;
				if (flag)
				{
					this.m_DrawsInCameras = value;
					this.InvokeDrawsInCamerasChanged();
				}
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0004A338 File Offset: 0x00048538
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x0004A350 File Offset: 0x00048550
		internal float pixelsPerUnit
		{
			get
			{
				return this.m_PixelsPerUnit;
			}
			set
			{
				this.m_PixelsPerUnit = value;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0004A35A File Offset: 0x0004855A
		// (set) Token: 0x060011B7 RID: 4535 RVA: 0x0004A362 File Offset: 0x00048562
		internal int targetDisplay { get; set; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0004A36B File Offset: 0x0004856B
		internal int screenRenderingWidth
		{
			get
			{
				return BaseRuntimePanel.getScreenRenderingWidth(this.targetDisplay);
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0004A378 File Offset: 0x00048578
		internal int screenRenderingHeight
		{
			get
			{
				return BaseRuntimePanel.getScreenRenderingHeight(this.targetDisplay);
			}
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0004A385 File Offset: 0x00048585
		internal virtual void Update()
		{
			this.scheduler.UpdateScheduledEvents();
			this.ValidateFocus();
			this.ValidateLayout();
			this.UpdateAnimations();
			this.UpdateBindings();
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0004A3B0 File Offset: 0x000485B0
		internal static int getScreenRenderingHeight(int display)
		{
			return (display >= 0 && display < Display.displays.Length) ? Display.displays[display].renderingHeight : Screen.height;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0004A3E4 File Offset: 0x000485E4
		internal static int getScreenRenderingWidth(int display)
		{
			return (display >= 0 && display < Display.displays.Length) ? Display.displays[display].renderingWidth : Screen.width;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0004A418 File Offset: 0x00048618
		public override void Render()
		{
			bool drawsInCameras = this.drawsInCameras;
			if (drawsInCameras)
			{
				Debug.LogError("Panel.Render() must not be called on a panel that draws in cameras.");
			}
			else
			{
				bool flag = this.targetTexture == null;
				if (flag)
				{
					RenderTexture rt = RenderTexture.active;
					int width = ((rt != null) ? rt.width : this.screenRenderingWidth);
					int height = ((rt != null) ? rt.height : this.screenRenderingHeight);
					GL.Viewport(new Rect(0f, 0f, (float)width, (float)height));
					base.Render();
				}
				else
				{
					Camera oldCam = Camera.current;
					RenderTexture oldRT = RenderTexture.active;
					Camera.SetupCurrent(null);
					RenderTexture.active = this.targetTexture;
					GL.Viewport(new Rect(0f, 0f, (float)this.targetTexture.width, (float)this.targetTexture.height));
					base.Render();
					Camera.SetupCurrent(oldCam);
					RenderTexture.active = oldRT;
				}
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0004A514 File Offset: 0x00048714
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x0004A51C File Offset: 0x0004871C
		public Func<Vector2, Vector2> screenToPanelSpace
		{
			get
			{
				return this.m_ScreenToPanelSpace;
			}
			set
			{
				this.m_ScreenToPanelSpace = value ?? BaseRuntimePanel.DefaultScreenToPanelSpace;
			}
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0004A530 File Offset: 0x00048730
		internal Vector2 ScreenToPanel(Vector2 screen)
		{
			return this.screenToPanelSpace(screen) / base.scale;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0004A55C File Offset: 0x0004875C
		internal bool ScreenToPanel(Vector2 screenPosition, Vector2 screenDelta, out Vector2 panelPosition, out Vector2 panelDelta, bool allowOutside = false)
		{
			panelPosition = this.ScreenToPanel(screenPosition);
			bool flag = !allowOutside;
			Vector2 panelPrevPosition;
			if (flag)
			{
				Rect panelRect = this.visualTree.layout;
				bool flag2 = !panelRect.Contains(panelPosition);
				if (flag2)
				{
					panelDelta = screenDelta;
					return false;
				}
				panelPrevPosition = this.ScreenToPanel(screenPosition - screenDelta);
				bool flag3 = !panelRect.Contains(panelPrevPosition);
				if (flag3)
				{
					panelDelta = screenDelta;
					return true;
				}
			}
			else
			{
				panelPrevPosition = this.ScreenToPanel(screenPosition - screenDelta);
			}
			panelDelta = panelPosition - panelPrevPosition;
			return true;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0004A60C File Offset: 0x0004880C
		private void AssignPanelToComponents(BaseRuntimePanel panel)
		{
			bool flag = this.selectableGameObject == null;
			if (!flag)
			{
				List<IRuntimePanelComponent> components;
				using (CollectionPool<List<IRuntimePanelComponent>, IRuntimePanelComponent>.Get(out components))
				{
					this.selectableGameObject.GetComponents<IRuntimePanelComponent>(components);
					foreach (IRuntimePanelComponent component in components)
					{
						component.panel = panel;
					}
				}
			}
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0004A6A8 File Offset: 0x000488A8
		internal void PointerLeavesPanel(int pointerId, Vector2 position)
		{
			base.ClearCachedElementUnderPointer(pointerId, null);
			base.CommitElementUnderPointers();
			PointerDeviceState.SavePointerPosition(pointerId, position, null, this.contextType);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0004A6CA File Offset: 0x000488CA
		internal void PointerEntersPanel(int pointerId, Vector2 position)
		{
			PointerDeviceState.SavePointerPosition(pointerId, position, this, this.contextType);
		}

		// Token: 0x04000A3B RID: 2619
		private GameObject m_SelectableGameObject;

		// Token: 0x04000A3C RID: 2620
		private static int s_CurrentRuntimePanelCounter = 0;

		// Token: 0x04000A3D RID: 2621
		internal readonly int m_RuntimePanelCreationIndex;

		// Token: 0x04000A3E RID: 2622
		private float m_SortingPriority = 0f;

		// Token: 0x04000A3F RID: 2623
		internal int resolvedSortingIndex = 0;

		// Token: 0x04000A42 RID: 2626
		private bool m_DrawsInCameras;

		// Token: 0x04000A43 RID: 2627
		private float m_PixelsPerUnit = 100f;

		// Token: 0x04000A44 RID: 2628
		internal RenderTexture targetTexture = null;

		// Token: 0x04000A45 RID: 2629
		internal int worldSpaceLayer = 0;

		// Token: 0x04000A47 RID: 2631
		internal static readonly Func<Vector2, Vector2> DefaultScreenToPanelSpace = (Vector2 p) => p;

		// Token: 0x04000A48 RID: 2632
		private Func<Vector2, Vector2> m_ScreenToPanelSpace = BaseRuntimePanel.DefaultScreenToPanelSpace;
	}
}
