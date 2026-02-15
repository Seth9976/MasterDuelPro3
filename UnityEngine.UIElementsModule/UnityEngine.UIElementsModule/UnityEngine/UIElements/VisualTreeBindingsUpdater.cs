using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x02000045 RID: 69
	internal class VisualTreeBindingsUpdater : BaseVisualTreeHierarchyTrackerUpdater
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000097CE File Offset: 0x000079CE
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeBindingsUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000097D5 File Offset: 0x000079D5
		public static bool disableBindingsThrottling { get; } = 0;

		// Token: 0x06000202 RID: 514 RVA: 0x000097DC File Offset: 0x000079DC
		private IBinding GetBindingObjectFromElement(VisualElement ve)
		{
			IBindable bindable = ve as IBindable;
			bool flag = bindable != null;
			if (flag)
			{
				bool flag2 = bindable.binding != null;
				if (flag2)
				{
					return bindable.binding;
				}
			}
			return VisualTreeBindingsUpdater.GetAdditionalBinding(ve);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000981B File Offset: 0x00007A1B
		private void StartTracking(VisualElement ve)
		{
			this.m_ElementsToAdd.Add(ve);
			this.m_ElementsToRemove.Remove(ve);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009838 File Offset: 0x00007A38
		private void StopTracking(VisualElement ve)
		{
			this.m_ElementsToRemove.Add(ve);
			this.m_ElementsToAdd.Remove(ve);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00009855 File Offset: 0x00007A55
		public Dictionary<object, object> temporaryObjectCache { get; } = new Dictionary<object, object>();

		// Token: 0x06000206 RID: 518 RVA: 0x00009860 File Offset: 0x00007A60
		public static IBinding GetAdditionalBinding(VisualElement ve)
		{
			return ve.GetProperty(VisualTreeBindingsUpdater.s_AdditionalBindingObjectVEPropertyName) as IBinding;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009884 File Offset: 0x00007A84
		private void StartTrackingRecursive(VisualElement ve)
		{
			IBinding u = this.GetBindingObjectFromElement(ve);
			bool flag = u != null;
			if (flag)
			{
				this.StartTracking(ve);
			}
			object bindingRequest = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
			bool flag2 = bindingRequest != null;
			if (flag2)
			{
				this.m_ElementsToBind.Add(ve);
			}
			int count = ve.hierarchy.childCount;
			for (int i = 0; i < count; i++)
			{
				VisualElement child = ve.hierarchy[i];
				this.StartTrackingRecursive(child);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009918 File Offset: 0x00007B18
		private void StopTrackingRecursive(VisualElement ve)
		{
			this.StopTracking(ve);
			object bindingRequest = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
			bool flag = bindingRequest != null;
			if (flag)
			{
				this.m_ElementsToBind.Remove(ve);
			}
			int count = ve.hierarchy.childCount;
			for (int i = 0; i < count; i++)
			{
				VisualElement child = ve.hierarchy[i];
				this.StopTrackingRecursive(child);
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009994 File Offset: 0x00007B94
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			base.OnVersionChanged(ve, versionChangeType);
			bool flag = (versionChangeType & VersionChangeType.Bindings) == VersionChangeType.Bindings;
			if (flag)
			{
				bool flag2 = this.GetBindingObjectFromElement(ve) != null;
				if (flag2)
				{
					this.StartTracking(ve);
				}
				else
				{
					this.StopTracking(ve);
				}
				object bindingRequests = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
				bool flag3 = bindingRequests != null;
				if (flag3)
				{
					this.m_ElementsToBind.Add(ve);
				}
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009A00 File Offset: 0x00007C00
		protected override void OnHierarchyChange(VisualElement ve, HierarchyChangeType type)
		{
			if (type != HierarchyChangeType.Add)
			{
				if (type == HierarchyChangeType.Remove)
				{
					this.StopTrackingRecursive(ve);
				}
			}
			else
			{
				this.StartTrackingRecursive(ve);
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009A34 File Offset: 0x00007C34
		private static long CurrentTime()
		{
			return Panel.TimeSinceStartupMs();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009A4C File Offset: 0x00007C4C
		public static bool ShouldProcessBindings(long startTime)
		{
			return VisualTreeBindingsUpdater.disableBindingsThrottling || VisualTreeBindingsUpdater.CurrentTime() - startTime < 100L;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00009A74 File Offset: 0x00007C74
		public void PerformTrackingOperations()
		{
			foreach (VisualElement element in this.m_ElementsToAdd)
			{
				IBinding updater = this.GetBindingObjectFromElement(element);
				bool flag = updater != null;
				if (flag)
				{
					this.m_ElementsWithBindings.Add(element);
				}
			}
			this.m_ElementsToAdd.Clear();
			foreach (VisualElement element2 in this.m_ElementsToRemove)
			{
				this.m_ElementsWithBindings.Remove(element2);
			}
			this.m_ElementsToRemove.Clear();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00009B4C File Offset: 0x00007D4C
		public override void Update()
		{
			base.Update();
			bool flag = this.m_ElementsToBind.Count > 0;
			if (flag)
			{
				using (VisualTreeBindingsUpdater.s_ProfilerBindingRequestsMarker.Auto())
				{
					long startTime = VisualTreeBindingsUpdater.CurrentTime();
					while (this.m_ElementsToBind.Count > 0 && VisualTreeBindingsUpdater.ShouldProcessBindings(startTime))
					{
						VisualElement element = this.m_ElementsToBind.FirstOrDefault<VisualElement>();
						bool flag2 = element != null;
						if (!flag2)
						{
							break;
						}
						this.m_ElementsToBind.Remove(element);
						List<IBindingRequest> bindingRequests = element.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
						bool flag3 = bindingRequests != null;
						if (flag3)
						{
							element.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, null);
							foreach (IBindingRequest req in bindingRequests)
							{
								req.Bind(element);
							}
							ObjectListPool<IBindingRequest>.Release(bindingRequests);
						}
					}
				}
			}
			this.PerformTrackingOperations();
			bool flag4 = this.m_ElementsWithBindings.Count > 0;
			if (flag4)
			{
				long currentTimeMs = VisualTreeBindingsUpdater.CurrentTime();
				bool flag5 = VisualTreeBindingsUpdater.disableBindingsThrottling || this.m_LastUpdateTime + 100L < currentTimeMs;
				if (flag5)
				{
					this.UpdateBindings();
					this.m_LastUpdateTime = currentTimeMs;
				}
			}
			bool flag6 = this.m_ElementsToBind.Count == 0;
			if (flag6)
			{
				this.temporaryObjectCache.Clear();
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00009CF8 File Offset: 0x00007EF8
		private void UpdateBindings()
		{
			foreach (VisualElement element in this.m_ElementsWithBindings)
			{
				IBinding updater = this.GetBindingObjectFromElement(element);
				bool flag = updater == null || element.elementPanel != base.panel;
				if (flag)
				{
					if (updater != null)
					{
						updater.Release();
					}
					this.StopTracking(element);
				}
				else
				{
					this.updatedBindings.Add(updater);
				}
			}
			foreach (IBinding u in this.updatedBindings)
			{
				u.PreUpdate();
			}
			foreach (IBinding u2 in this.updatedBindings)
			{
				u2.Update();
			}
			this.updatedBindings.Clear();
		}

		// Token: 0x04000141 RID: 321
		private static readonly PropertyName s_BindingRequestObjectVEPropertyName = "__unity-binding-request-object";

		// Token: 0x04000142 RID: 322
		private static readonly PropertyName s_AdditionalBindingObjectVEPropertyName = "__unity-additional-binding-object";

		// Token: 0x04000143 RID: 323
		private static readonly string s_Description = "Update Bindings";

		// Token: 0x04000144 RID: 324
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeBindingsUpdater.s_Description);

		// Token: 0x04000145 RID: 325
		private static readonly ProfilerMarker s_ProfilerBindingRequestsMarker = new ProfilerMarker("Bindings.Requests");

		// Token: 0x04000146 RID: 326
		private static ProfilerMarker s_MarkerUpdate = new ProfilerMarker("Bindings.Update");

		// Token: 0x04000147 RID: 327
		private static ProfilerMarker s_MarkerPoll = new ProfilerMarker("Bindings.PollElementsWithBindings");

		// Token: 0x04000149 RID: 329
		private readonly HashSet<VisualElement> m_ElementsWithBindings = new HashSet<VisualElement>();

		// Token: 0x0400014A RID: 330
		private readonly HashSet<VisualElement> m_ElementsToAdd = new HashSet<VisualElement>();

		// Token: 0x0400014B RID: 331
		private readonly HashSet<VisualElement> m_ElementsToRemove = new HashSet<VisualElement>();

		// Token: 0x0400014C RID: 332
		private long m_LastUpdateTime = 0L;

		// Token: 0x0400014D RID: 333
		private HashSet<VisualElement> m_ElementsToBind = new HashSet<VisualElement>();

		// Token: 0x0400014F RID: 335
		private List<IBinding> updatedBindings = new List<IBinding>();
	}
}
