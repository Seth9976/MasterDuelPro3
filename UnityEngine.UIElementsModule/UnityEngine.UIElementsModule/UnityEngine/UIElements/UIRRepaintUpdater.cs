using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AD RID: 685
	internal class UIRRepaintUpdater : BaseVisualTreeUpdater, IPanelRenderer
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x0004CC61 File Offset: 0x0004AE61
		public UIRRepaintUpdater()
		{
			base.panelChanged += this.OnPanelChanged;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x0004CC7E File Offset: 0x0004AE7E
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return UIRRepaintUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0004CC85 File Offset: 0x0004AE85
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x0004CC90 File Offset: 0x0004AE90
		public bool forceGammaRendering
		{
			get
			{
				return this.m_ForceGammaRendering;
			}
			set
			{
				bool flag = this.m_ForceGammaRendering == value;
				if (!flag)
				{
					this.m_ForceGammaRendering = value;
					this.DestroyRenderChain();
				}
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0004CCBB File Offset: 0x0004AEBB
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x0004CCC4 File Offset: 0x0004AEC4
		public uint vertexBudget
		{
			get
			{
				return this.m_VertexBudget;
			}
			set
			{
				bool flag = this.m_VertexBudget == value;
				if (!flag)
				{
					this.m_VertexBudget = value;
					this.DestroyRenderChain();
				}
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0004CCEF File Offset: 0x0004AEEF
		public bool drawStats { get; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0004CCF7 File Offset: 0x0004AEF7
		public bool breakBatches { get; }

		// Token: 0x0600127E RID: 4734 RVA: 0x0004CD00 File Offset: 0x0004AF00
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				bool transformChanged = (versionChangeType & VersionChangeType.Transform) > (VersionChangeType)0;
				bool sizeChanged = (versionChangeType & VersionChangeType.Size) > (VersionChangeType)0;
				bool overflowChanged = (versionChangeType & VersionChangeType.Overflow) > (VersionChangeType)0;
				bool borderRadiusChanged = (versionChangeType & VersionChangeType.BorderRadius) > (VersionChangeType)0;
				bool borderWidthChanged = (versionChangeType & VersionChangeType.BorderWidth) > (VersionChangeType)0;
				bool renderHintsChanged = (versionChangeType & VersionChangeType.RenderHints) > (VersionChangeType)0;
				bool disableRenderingChanged = (versionChangeType & VersionChangeType.DisableRendering) > (VersionChangeType)0;
				bool repaintChanged = (versionChangeType & VersionChangeType.Repaint) > (VersionChangeType)0;
				bool flag2 = renderHintsChanged;
				if (flag2)
				{
					this.renderChain.UIEOnRenderHintsChanged(ve);
				}
				bool flag3 = transformChanged || sizeChanged || borderWidthChanged;
				if (flag3)
				{
					this.renderChain.UIEOnTransformOrSizeChanged(ve, transformChanged, sizeChanged || borderWidthChanged);
				}
				bool flag4 = overflowChanged || borderRadiusChanged;
				if (flag4)
				{
					this.renderChain.UIEOnClippingChanged(ve, false);
				}
				bool flag5 = (versionChangeType & VersionChangeType.Opacity) > (VersionChangeType)0;
				if (flag5)
				{
					this.renderChain.UIEOnOpacityChanged(ve, false);
				}
				bool flag6 = (versionChangeType & VersionChangeType.Color) > (VersionChangeType)0;
				if (flag6)
				{
					this.renderChain.UIEOnColorChanged(ve);
				}
				bool flag7 = repaintChanged;
				if (flag7)
				{
					this.renderChain.UIEOnVisualsChanged(ve, false);
				}
				bool flag8 = disableRenderingChanged && !repaintChanged;
				if (flag8)
				{
					this.renderChain.UIEOnDisableRenderingChanged(ve);
				}
			}
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0004CE38 File Offset: 0x0004B038
		public override void Update()
		{
			bool flag = this.renderChain == null;
			if (flag)
			{
				this.InitRenderChain();
			}
			bool flag2 = this.renderChain == null || this.renderChain.device == null;
			if (!flag2)
			{
				this.renderChain.ProcessChanges();
				this.renderChain.drawStats = this.drawStats;
				this.renderChain.device.breakBatches = this.breakBatches;
			}
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0004CEB0 File Offset: 0x0004B0B0
		public void Render()
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				Debug.Assert(!this.renderChain.drawInCameras);
				this.renderChain.Render();
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0004CEF0 File Offset: 0x0004B0F0
		protected virtual RenderChain CreateRenderChain()
		{
			return new RenderChain(base.panel);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0004CF0D File Offset: 0x0004B10D
		static UIRRepaintUpdater()
		{
			Utility.GraphicsResourcesRecreate += UIRRepaintUpdater.OnGraphicsResourcesRecreate;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0004CF3C File Offset: 0x0004B13C
		private static void OnGraphicsResourcesRecreate(bool recreate)
		{
			bool flag = !recreate;
			if (flag)
			{
				UIRenderDevice.PrepareForGfxDeviceRecreate();
			}
			Dictionary<int, Panel>.Enumerator it = UIElementsUtility.GetPanelsIterator();
			while (it.MoveNext())
			{
				if (recreate)
				{
					KeyValuePair<int, Panel> keyValuePair = it.Current;
					AtlasBase atlas = keyValuePair.Value.atlas;
					if (atlas != null)
					{
						atlas.Reset();
					}
				}
				else
				{
					KeyValuePair<int, Panel> keyValuePair = it.Current;
					keyValuePair.Value.panelRenderer.Reset();
				}
			}
			bool flag2 = !recreate;
			if (flag2)
			{
				UIRenderDevice.FlushAllPendingDeviceDisposes();
			}
			else
			{
				UIRenderDevice.WrapUpGfxDeviceRecreate();
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0004CFC6 File Offset: 0x0004B1C6
		private void OnPanelChanged(BaseVisualElementPanel obj)
		{
			this.DetachFromPanel();
			this.AttachToPanel();
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0004CFD8 File Offset: 0x0004B1D8
		private void AttachToPanel()
		{
			Debug.Assert(this.attachedPanel == null);
			bool flag = base.panel == null;
			if (!flag)
			{
				this.attachedPanel = base.panel;
				this.attachedPanel.isFlatChanged += this.OnPanelIsFlatChanged;
				this.attachedPanel.atlasChanged += this.OnPanelAtlasChanged;
				this.attachedPanel.hierarchyChanged += this.OnPanelHierarchyChanged;
				Debug.Assert(this.attachedPanel.panelRenderer == null);
				this.attachedPanel.panelRenderer = this;
				BaseRuntimePanel runtimePanel = base.panel as BaseRuntimePanel;
				bool flag2 = runtimePanel != null;
				if (flag2)
				{
					runtimePanel.drawsInCamerasChanged += this.OnPanelDrawsInCamerasChanged;
				}
			}
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0004D0A4 File Offset: 0x0004B2A4
		private void DetachFromPanel()
		{
			bool flag = this.attachedPanel == null;
			if (!flag)
			{
				this.DestroyRenderChain();
				BaseRuntimePanel runtimePanel = base.panel as BaseRuntimePanel;
				bool flag2 = runtimePanel != null;
				if (flag2)
				{
					runtimePanel.drawsInCamerasChanged -= this.OnPanelDrawsInCamerasChanged;
				}
				this.attachedPanel.isFlatChanged -= this.OnPanelIsFlatChanged;
				this.attachedPanel.atlasChanged -= this.OnPanelAtlasChanged;
				this.attachedPanel.hierarchyChanged -= this.OnPanelHierarchyChanged;
				Debug.Assert(this.attachedPanel.panelRenderer == this);
				this.attachedPanel.panelRenderer = null;
				this.attachedPanel = null;
			}
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0004D161 File Offset: 0x0004B361
		private void InitRenderChain()
		{
			Debug.Assert(this.attachedPanel != null);
			this.renderChain = this.CreateRenderChain();
			this.renderChain.UIEOnChildAdded(this.attachedPanel.visualTree);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0004D196 File Offset: 0x0004B396
		public void Reset()
		{
			this.DestroyRenderChain();
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0004D1A0 File Offset: 0x0004B3A0
		private void DestroyRenderChain()
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				this.renderChain.Dispose();
				this.renderChain = null;
				this.ResetAllElementsDataRecursive(this.attachedPanel.visualTree);
			}
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0004D1E2 File Offset: 0x0004B3E2
		private void OnPanelIsFlatChanged()
		{
			this.DestroyRenderChain();
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0004D1E2 File Offset: 0x0004B3E2
		private void OnPanelAtlasChanged()
		{
			this.DestroyRenderChain();
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0004D1E2 File Offset: 0x0004B3E2
		private void OnPanelDrawsInCamerasChanged()
		{
			this.DestroyRenderChain();
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0004D1EC File Offset: 0x0004B3EC
		private void OnPanelHierarchyChanged(VisualElement ve, HierarchyChangeType changeType)
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				switch (changeType)
				{
				case HierarchyChangeType.Add:
					this.renderChain.UIEOnChildAdded(ve);
					break;
				case HierarchyChangeType.Remove:
					this.renderChain.UIEOnChildRemoving(ve);
					break;
				case HierarchyChangeType.Move:
					this.renderChain.UIEOnChildrenReordered(ve);
					break;
				}
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0004D250 File Offset: 0x0004B450
		private void ResetAllElementsDataRecursive(VisualElement ve)
		{
			ve.renderChainData = default(RenderChainVEData);
			int childrenCount = ve.hierarchy.childCount - 1;
			while (childrenCount >= 0)
			{
				this.ResetAllElementsDataRecursive(ve.hierarchy[childrenCount--]);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0004D2A2 File Offset: 0x0004B4A2
		// (set) Token: 0x06001290 RID: 4752 RVA: 0x0004D2AA File Offset: 0x0004B4AA
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001291 RID: 4753 RVA: 0x0004D2B4 File Offset: 0x0004B4B4
		protected override void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.DetachFromPanel();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000AB9 RID: 2745
		private BaseVisualElementPanel attachedPanel;

		// Token: 0x04000ABA RID: 2746
		internal RenderChain renderChain;

		// Token: 0x04000ABB RID: 2747
		private static readonly string s_Description = "Update Rendering";

		// Token: 0x04000ABC RID: 2748
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(UIRRepaintUpdater.s_Description);

		// Token: 0x04000ABD RID: 2749
		private bool m_ForceGammaRendering;

		// Token: 0x04000ABE RID: 2750
		private uint m_VertexBudget;
	}
}
