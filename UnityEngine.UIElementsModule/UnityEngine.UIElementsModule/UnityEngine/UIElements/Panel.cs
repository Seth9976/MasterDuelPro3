using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028D RID: 653
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class Panel : BaseVisualElementPanel
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x00049770 File Offset: 0x00047970
		public sealed override VisualElement visualTree
		{
			get
			{
				return this.m_RootContainer;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00049788 File Offset: 0x00047988
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x00049790 File Offset: 0x00047990
		public sealed override EventDispatcher dispatcher { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0004979C File Offset: 0x0004799C
		public TimerEventScheduler timerEventScheduler
		{
			get
			{
				TimerEventScheduler timerEventScheduler;
				if ((timerEventScheduler = this.m_Scheduler) == null)
				{
					timerEventScheduler = (this.m_Scheduler = new TimerEventScheduler());
				}
				return timerEventScheduler;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x000497C8 File Offset: 0x000479C8
		internal override IScheduler scheduler
		{
			get
			{
				return this.timerEventScheduler;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x000497E0 File Offset: 0x000479E0
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x000497E8 File Offset: 0x000479E8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal override IStylePropertyAnimationSystem styleAnimationSystem
		{
			get
			{
				return this.m_StylePropertyAnimationSystem;
			}
			set
			{
				bool flag = this.m_StylePropertyAnimationSystem == value;
				if (!flag)
				{
					IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.m_StylePropertyAnimationSystem;
					if (stylePropertyAnimationSystem != null)
					{
						stylePropertyAnimationSystem.CancelAllAnimations();
					}
					this.m_StylePropertyAnimationSystem = value;
				}
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0004981E File Offset: 0x00047A1E
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x00049826 File Offset: 0x00047A26
		public override ScriptableObject ownerObject { get; protected set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0004982F File Offset: 0x00047A2F
		public override ContextType contextType { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00049837 File Offset: 0x00047A37
		public override SavePersistentViewData saveViewData { get; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0004983F File Offset: 0x00047A3F
		public override GetViewDataDictionary getViewDataDictionary { get; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00049847 File Offset: 0x00047A47
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x0004984F File Offset: 0x00047A4F
		public sealed override FocusController focusController { get; set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00049858 File Offset: 0x00047A58
		// (set) Token: 0x06001180 RID: 4480 RVA: 0x00049860 File Offset: 0x00047A60
		public override EventInterests IMGUIEventInterests { get; set; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00049869 File Offset: 0x00047A69
		private static LoadResourceFunction loadResourceFunc { get; }

		// Token: 0x06001182 RID: 4482 RVA: 0x00049870 File Offset: 0x00047A70
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static Object LoadResource(string pathName, Type type, float dpiScaling)
		{
			bool flag = Panel.loadResourceFunc != null;
			Object obj;
			if (flag)
			{
				obj = Panel.loadResourceFunc(pathName, type, dpiScaling);
			}
			else
			{
				obj = Resources.Load(pathName, type);
			}
			return obj;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000498AD File Offset: 0x00047AAD
		internal void Focus()
		{
			this.m_JustReceivedFocus = true;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000498B7 File Offset: 0x00047AB7
		internal void Blur()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.BlurLastFocusedElement();
			}
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x000498CC File Offset: 0x00047ACC
		public override void ValidateFocus()
		{
			bool justReceivedFocus = this.m_JustReceivedFocus;
			if (justReceivedFocus)
			{
				this.m_JustReceivedFocus = false;
				FocusController focusController = this.focusController;
				if (focusController != null)
				{
					focusController.SetFocusToLastFocusedElement();
				}
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00049900 File Offset: 0x00047B00
		// (set) Token: 0x06001187 RID: 4487 RVA: 0x00049918 File Offset: 0x00047B18
		internal string name
		{
			get
			{
				return this.m_PanelName;
			}
			set
			{
				this.m_PanelName = value;
				this.CreateMarkers();
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00049929 File Offset: 0x00047B29
		// (set) Token: 0x06001189 RID: 4489 RVA: 0x00049934 File Offset: 0x00047B34
		public IDebugPanelChangeReceiver panelChangeReceiver
		{
			get
			{
				return this.m_PanelChangeReceiver;
			}
			set
			{
				this.m_PanelChangeReceiver = value;
				bool flag = value != null;
				if (flag)
				{
					Debug.LogWarning("IPanelChangeReceiver suscribed to panel '" + this.name + "' and may affect performance. The callback should be used only in debugging scenario and won't work outside development builds");
				}
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0004996C File Offset: 0x00047B6C
		private void CreateMarkers()
		{
			string appendName = (string.IsNullOrEmpty(this.m_PanelName) ? "" : ("." + this.m_PanelName));
			this.m_MarkerBeforeUpdate = new ProfilerMarker("Panel.BeforeUpdate" + appendName);
			this.m_MarkerUpdate = new ProfilerMarker("Panel.Update" + appendName);
			this.m_MarkerRender = new ProfilerMarker("Panel.Render" + appendName);
			this.m_MarkerLayout = new ProfilerMarker("Panel.Layout" + appendName);
			this.m_MarkerBindings = new ProfilerMarker("Panel.Bindings" + appendName);
			this.m_MarkerDataBinding = new ProfilerMarker("Panel.DataBinding" + appendName);
			this.m_MarkerAnimations = new ProfilerMarker("Panel.Animations" + appendName);
			this.m_MarkerPanelChangeReceiver = new ProfilerMarker("Panel.PanelChangeReceiver" + appendName);
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00049A4F File Offset: 0x00047C4F
		private static TimeMsFunction TimeSinceStartup { get; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00049A56 File Offset: 0x00047C56
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x00049A5E File Offset: 0x00047C5E
		public override int IMGUIContainersCount { get; set; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00049A67 File Offset: 0x00047C67
		public override IMGUIContainer rootIMGUIContainer { get; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x00049A6F File Offset: 0x00047C6F
		internal override uint version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00049A77 File Offset: 0x00047C77
		internal override uint hierarchyVersion
		{
			get
			{
				return this.m_HierarchyVersion;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x00049A80 File Offset: 0x00047C80
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x00049A98 File Offset: 0x00047C98
		public override AtlasBase atlas
		{
			get
			{
				return this.m_Atlas;
			}
			set
			{
				bool flag = this.m_Atlas != value;
				if (flag)
				{
					AtlasBase atlas = this.m_Atlas;
					if (atlas != null)
					{
						atlas.InvokeRemovedFromPanel(this);
					}
					this.m_Atlas = value;
					base.InvokeAtlasChanged();
					AtlasBase atlas2 = this.m_Atlas;
					if (atlas2 != null)
					{
						atlas2.InvokeAssignedToPanel(this);
					}
				}
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00049AEC File Offset: 0x00047CEC
		public Panel(ScriptableObject ownerObject, ContextType contextType, EventDispatcher dispatcher)
		{
			Debug.Assert(contextType == ContextType.Player, "In a player, panel context type must be set to Player.");
			contextType = ContextType.Player;
			this.ownerObject = ownerObject;
			this.contextType = contextType;
			this.dispatcher = dispatcher;
			this.repaintData = new RepaintData();
			this.cursorManager = new CursorManager();
			base.contextualMenuManager = null;
			this.dataBindingManager = new DataBindingManager(this);
			this.m_VisualTreeUpdater = new VisualTreeUpdater(this);
			this.m_RootContainer = ((contextType == ContextType.Editor) ? new EditorPanelRootElement() : new PanelRootElement());
			this.visualTree.SetPanel(this);
			this.focusController = new FocusController(new VisualElementFocusRing(this.visualTree, VisualElementFocusRing.DefaultFocusOrder.ChildOrder));
			this.styleAnimationSystem = new StylePropertyAnimationSystem();
			this.CreateMarkers();
			base.InvokeHierarchyChanged(this.visualTree, HierarchyChangeType.Add);
			this.atlas = new DynamicAtlas();
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00049BE8 File Offset: 0x00047DE8
		protected override void Dispose(bool disposing)
		{
			bool disposed = base.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.atlas = null;
					this.m_VisualTreeUpdater.Dispose();
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00049C28 File Offset: 0x00047E28
		public static long TimeSinceStartupMs()
		{
			TimeMsFunction timeSinceStartup = Panel.TimeSinceStartup;
			return (timeSinceStartup != null) ? timeSinceStartup() : Panel.DefaultTimeSinceStartupMs();
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00049C50 File Offset: 0x00047E50
		internal static long DefaultTimeSinceStartupMs()
		{
			return (long)(Time.realtimeSinceStartup * 1000f);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00049C70 File Offset: 0x00047E70
		private static VisualElement PickAll(VisualElement root, Vector2 point, List<VisualElement> picked = null, bool includeIgnoredElement = false)
		{
			return Panel.PerformPick(root, point, picked, includeIgnoredElement);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00049C90 File Offset: 0x00047E90
		private static VisualElement PerformPick(VisualElement root, Vector2 point, List<VisualElement> picked = null, bool includeIgnoredElement = false)
		{
			bool flag = root.resolvedStyle.display == DisplayStyle.None;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = null;
			}
			else
			{
				bool flag2 = root.pickingMode == PickingMode.Ignore && root.hierarchy.childCount == 0 && !includeIgnoredElement;
				if (flag2)
				{
					visualElement = null;
				}
				else
				{
					bool flag3 = !root.worldBoundingBox.Contains(point);
					if (flag3)
					{
						visualElement = null;
					}
					else
					{
						Vector2 localPoint = root.WorldToLocal(point);
						bool containsPoint = root.ContainsPoint(localPoint);
						bool flag4 = !containsPoint && root.ShouldClip();
						if (flag4)
						{
							visualElement = null;
						}
						else
						{
							VisualElement returnedChild = null;
							int cCount = root.hierarchy.childCount;
							for (int i = cCount - 1; i >= 0; i--)
							{
								VisualElement child = root.hierarchy[i];
								VisualElement result = Panel.PerformPick(child, point, picked, includeIgnoredElement);
								bool flag5 = returnedChild == null && result != null;
								if (flag5)
								{
									bool flag6 = picked == null;
									if (flag6)
									{
										return result;
									}
									returnedChild = result;
								}
							}
							bool flag7 = root.visible && (root.pickingMode == PickingMode.Position || includeIgnoredElement) && containsPoint;
							if (flag7)
							{
								if (picked != null)
								{
									picked.Add(root);
								}
								bool flag8 = returnedChild == null;
								if (flag8)
								{
									returnedChild = root;
								}
							}
							visualElement = returnedChild;
						}
					}
				}
			}
			return visualElement;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00049DEC File Offset: 0x00047FEC
		public override VisualElement PickAll(Vector2 point, List<VisualElement> picked)
		{
			this.ValidateLayout();
			bool flag = picked != null;
			if (flag)
			{
				picked.Clear();
			}
			return Panel.PickAll(this.visualTree, point, picked, false);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00049E24 File Offset: 0x00048024
		public override VisualElement Pick(Vector2 point)
		{
			this.ValidateLayout();
			Vector2 mousePos;
			bool isTemporary;
			VisualElement element = this.m_TopElementUnderPointers.GetTopElementUnderPointer(PointerId.mousePointerId, out mousePos, out isTemporary);
			bool flag = !isTemporary && Panel.<Pick>g__PixelOf|105_0(mousePos) == Panel.<Pick>g__PixelOf|105_0(point);
			VisualElement visualElement;
			if (flag)
			{
				visualElement = element;
			}
			else
			{
				visualElement = Panel.PickAll(this.visualTree, point, null, false);
			}
			return visualElement;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00049E84 File Offset: 0x00048084
		public override void ValidateLayout()
		{
			bool flag = !this.m_ValidatingLayout;
			if (flag)
			{
				this.m_ValidatingLayout = true;
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
				this.m_ValidatingLayout = false;
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00049ED6 File Offset: 0x000480D6
		public override void UpdateAnimations()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Animation);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00049EE6 File Offset: 0x000480E6
		public override void UpdateBindings()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Bindings);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00049EF6 File Offset: 0x000480F6
		public override void ApplyStyles()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00049F08 File Offset: 0x00048108
		private void UpdateForRepaint()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.DataBinding);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Repaint);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00049F58 File Offset: 0x00048158
		public override void Repaint(Event e)
		{
			this.m_RepaintVersion = this.version;
			this.repaintData.repaintEvent = e;
			using (this.m_MarkerBeforeUpdate.Auto())
			{
				base.InvokeBeforeUpdate();
			}
			Action<Panel> action = Panel.beforeAnyRepaint;
			if (action != null)
			{
				action(this);
			}
			using (this.m_MarkerUpdate.Auto())
			{
				this.UpdateForRepaint();
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00049FFC File Offset: 0x000481FC
		public override void Render()
		{
			base.Render();
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0004A008 File Offset: 0x00048208
		internal override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			this.m_Version += 1U;
			this.m_VisualTreeUpdater.OnVersionChanged(ve, versionChangeType);
			bool flag = this.panelChangeReceiver != null;
			if (flag)
			{
				using (this.m_MarkerPanelChangeReceiver.Auto())
				{
					this.panelChangeReceiver.OnVisualElementChange(ve, versionChangeType);
				}
			}
			bool flag2 = (versionChangeType & VersionChangeType.Hierarchy) == VersionChangeType.Hierarchy;
			if (flag2)
			{
				this.m_HierarchyVersion += 1U;
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0004A094 File Offset: 0x00048294
		internal override IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase)
		{
			return this.m_VisualTreeUpdater.GetUpdater(phase);
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x0004A0B2 File Offset: 0x000482B2
		internal virtual Color HyperlinkColor
		{
			get
			{
				return Color.blue;
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0004A0CA File Offset: 0x000482CA
		[CompilerGenerated]
		internal static Vector2Int <Pick>g__PixelOf|105_0(Vector2 p)
		{
			return Vector2Int.FloorToInt(p);
		}

		// Token: 0x04000A19 RID: 2585
		internal const int k_DefaultPixelsPerUnit = 100;

		// Token: 0x04000A1A RID: 2586
		private VisualElement m_RootContainer;

		// Token: 0x04000A1B RID: 2587
		private VisualTreeUpdater m_VisualTreeUpdater;

		// Token: 0x04000A1C RID: 2588
		private IStylePropertyAnimationSystem m_StylePropertyAnimationSystem;

		// Token: 0x04000A1D RID: 2589
		private string m_PanelName;

		// Token: 0x04000A1E RID: 2590
		private uint m_Version = 0U;

		// Token: 0x04000A1F RID: 2591
		private uint m_RepaintVersion = 0U;

		// Token: 0x04000A20 RID: 2592
		private uint m_HierarchyVersion = 0U;

		// Token: 0x04000A21 RID: 2593
		private ProfilerMarker m_MarkerBeforeUpdate;

		// Token: 0x04000A22 RID: 2594
		private ProfilerMarker m_MarkerUpdate;

		// Token: 0x04000A23 RID: 2595
		private ProfilerMarker m_MarkerRender;

		// Token: 0x04000A24 RID: 2596
		private ProfilerMarker m_MarkerLayout;

		// Token: 0x04000A25 RID: 2597
		private ProfilerMarker m_MarkerBindings;

		// Token: 0x04000A26 RID: 2598
		private ProfilerMarker m_MarkerDataBinding;

		// Token: 0x04000A27 RID: 2599
		private ProfilerMarker m_MarkerAnimations;

		// Token: 0x04000A28 RID: 2600
		private ProfilerMarker m_MarkerPanelChangeReceiver;

		// Token: 0x04000A29 RID: 2601
		private static ProfilerMarker s_MarkerPickAll = new ProfilerMarker("Panel.PickAll");

		// Token: 0x04000A2B RID: 2603
		private TimerEventScheduler m_Scheduler;

		// Token: 0x04000A33 RID: 2611
		private bool m_JustReceivedFocus;

		// Token: 0x04000A34 RID: 2612
		private IDebugPanelChangeReceiver m_PanelChangeReceiver;

		// Token: 0x04000A38 RID: 2616
		private AtlasBase m_Atlas;

		// Token: 0x04000A39 RID: 2617
		private bool m_ValidatingLayout = false;

		// Token: 0x04000A3A RID: 2618
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<Panel> beforeAnyRepaint;
	}
}
