using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine.Assertions;
using UnityEngine.Pool;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002F RID: 47
	internal sealed class DataBindingManager : IDisposable
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000799C File Offset: 0x00005B9C
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000079C7 File Offset: 0x00005BC7
		internal BindingLogLevel logLevel
		{
			get
			{
				return this.m_LogLevel ?? DataBindingManager.globalLogLevel;
			}
			set
			{
				this.m_LogLevel = new BindingLogLevel?(value);
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000079D5 File Offset: 0x00005BD5
		internal void ResetLogLevel()
		{
			this.m_LogLevel = null;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000079E4 File Offset: 0x00005BE4
		internal DataBindingManager(BaseVisualElementPanel panel)
		{
			this.m_Panel = panel;
			this.m_DataSourceTracker = new DataBindingManager.HierarchyDataSourceTracker(this);
			this.m_BindingsTracker = new DataBindingManager.HierarchyBindingTracker(panel);
			this.m_DetectedChangesFromUI = new List<DataBindingManager.ChangesFromUI>();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007A30 File Offset: 0x00005C30
		internal int GetTrackedDataSourcesCount()
		{
			return this.m_DataSourceTracker.GetTrackedDataSourcesCount();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007A50 File Offset: 0x00005C50
		internal bool TryGetLastVersion(object source, out long version)
		{
			return this.m_DataSourceTracker.TryGetLastVersion(source, out version);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007A6F File Offset: 0x00005C6F
		internal void UpdateVersion(object source, long version)
		{
			this.m_DataSourceTracker.UpdateVersion(source, version);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007A80 File Offset: 0x00005C80
		internal void CacheUIBindingResult(DataBindingManager.BindingData bindingData, BindingResult result)
		{
			bindingData.m_SourceToUILastUpdate = new BindingResult?(result);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007A90 File Offset: 0x00005C90
		internal bool TryGetLastUIBindingResult(DataBindingManager.BindingData bindingData, out BindingResult result)
		{
			bool flag = bindingData.m_SourceToUILastUpdate != null;
			bool flag2;
			if (flag)
			{
				result = bindingData.m_SourceToUILastUpdate.Value;
				flag2 = true;
			}
			else
			{
				result = default(BindingResult);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007ACF File Offset: 0x00005CCF
		internal void CacheSourceBindingResult(DataBindingManager.BindingData bindingData, BindingResult result)
		{
			bindingData.m_UIToSourceLastUpdate = new BindingResult?(result);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007AE0 File Offset: 0x00005CE0
		internal bool TryGetLastSourceBindingResult(DataBindingManager.BindingData bindingData, out BindingResult result)
		{
			bool flag = bindingData.m_UIToSourceLastUpdate != null;
			bool flag2;
			if (flag)
			{
				result = bindingData.m_UIToSourceLastUpdate.Value;
				flag2 = true;
			}
			else
			{
				result = default(BindingResult);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007B20 File Offset: 0x00005D20
		internal DataSourceContext GetResolvedDataSourceContext(VisualElement element, DataBindingManager.BindingData bindingData)
		{
			return (element.panel == this.m_Panel) ? this.m_DataSourceTracker.GetResolvedDataSourceContext(element, bindingData) : default(DataSourceContext);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007B58 File Offset: 0x00005D58
		internal int GetBoundElementsCount()
		{
			return this.m_BindingsTracker.GetTrackedElementsCount();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007B78 File Offset: 0x00005D78
		internal IEnumerable<VisualElement> GetBoundElements()
		{
			return this.m_BindingsTracker.GetBoundElements();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007B98 File Offset: 0x00005D98
		internal List<DataBindingManager.ChangesFromUI> GetChangedDetectedFromUI()
		{
			return this.m_DetectedChangesFromUI;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00007BB0 File Offset: 0x00005DB0
		internal List<PropertyPath> GetChangedDetectedFromSource(object dataSource)
		{
			return this.m_DataSourceTracker.GetChangesFromSource(dataSource);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007BCE File Offset: 0x00005DCE
		internal void ClearChangesFromSource(object dataSource)
		{
			this.m_DataSourceTracker.ClearChangesFromSource(dataSource);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007BE0 File Offset: 0x00005DE0
		internal List<DataBindingManager.BindingData> GetBindingData(VisualElement element)
		{
			DataBindingManager.BindingDataCollection collection;
			return (element.panel == this.m_Panel) ? (this.m_BindingsTracker.TryGetBindingCollection(element, out collection) ? collection.GetBindings() : DataBindingManager.s_Empty) : DataBindingManager.s_Empty;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007C28 File Offset: 0x00005E28
		internal bool TryGetBindingData(VisualElement element, in BindingId bindingId, out DataBindingManager.BindingData bindingData)
		{
			bindingData = null;
			DataBindingManager.BindingDataCollection collection;
			bool flag = element.panel == this.m_Panel && this.m_BindingsTracker.TryGetBindingCollection(element, out collection);
			bool flag2;
			if (flag)
			{
				flag2 = collection.TryGetBindingData(in bindingId, out bindingData);
			}
			else
			{
				bindingData = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00007C74 File Offset: 0x00005E74
		internal void RegisterBinding(VisualElement element, in BindingId bindingId, Binding binding)
		{
			Assert.IsFalse(binding == null);
			Assert.IsFalse((in bindingId).IsEmpty, "[UI Toolkit] Could not register binding on element of type '" + element.GetType().Name + "': target property path is empty.");
			DataBindingManager.BindingDataCollection collection;
			DataBindingManager.BindingData bindingData;
			bool flag = this.m_BindingsTracker.TryGetBindingCollection(element, out collection) && collection.TryGetBindingData(in bindingId, out bindingData);
			BindingActivationContext bindingActivationContext;
			if (flag)
			{
				Binding binding2 = bindingData.binding;
				bindingActivationContext = new BindingActivationContext(element, in bindingId);
				binding2.OnDeactivated(in bindingActivationContext);
				DataSourceContext currentResolvedContext = this.m_DataSourceTracker.GetResolvedDataSourceContext(element, bindingData);
				IDataSourceProvider provider = bindingData.binding as IDataSourceProvider;
				object newSource = ((provider != null) ? provider.dataSource : null);
				PropertyPath newSourcePath = ((provider != null) ? provider.dataSourcePath : default(PropertyPath));
				bool flag2 = currentResolvedContext.dataSource != newSource || currentResolvedContext.dataSourcePath != newSourcePath;
				if (flag2)
				{
					Binding binding3 = bindingData.binding;
					DataSourceContext dataSourceContext = new DataSourceContext(newSource, in newSourcePath);
					DataSourceContextChanged dataSourceContextChanged = new DataSourceContextChanged(element, in bindingId, in currentResolvedContext, in dataSourceContext);
					binding3.OnDataSourceChanged(in dataSourceContextChanged);
				}
				this.m_DataSourceTracker.DecreaseBindingRefCount(ref bindingData);
			}
			DataBindingManager.BindingData newBindingData = this.GetPooledBindingData(new BindingTarget(element, in bindingId), binding);
			this.m_DataSourceTracker.IncreaseBindingRefCount(ref newBindingData);
			this.m_BindingsTracker.StartTrackingBinding(element, newBindingData);
			bindingActivationContext = new BindingActivationContext(element, in bindingId);
			binding.OnActivated(in bindingActivationContext);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007DD0 File Offset: 0x00005FD0
		internal void UnregisterBinding(VisualElement element, in BindingId bindingId)
		{
			DataBindingManager.BindingDataCollection collection;
			bool flag = !this.m_BindingsTracker.TryGetBindingCollection(element, out collection);
			if (!flag)
			{
				DataBindingManager.BindingData bindingData;
				bool flag2 = collection.TryGetBindingData(in bindingId, out bindingData);
				if (flag2)
				{
					DataSourceContext currentResolvedContext = this.m_DataSourceTracker.GetResolvedDataSourceContext(element, bindingData);
					IDataSourceProvider provider = bindingData.binding as IDataSourceProvider;
					object newSource = ((provider != null) ? provider.dataSource : null);
					PropertyPath newSourcePath = ((provider != null) ? provider.dataSourcePath : default(PropertyPath));
					bool flag3 = currentResolvedContext.dataSource != newSource || currentResolvedContext.dataSourcePath != newSourcePath;
					if (flag3)
					{
						Binding binding = bindingData.binding;
						DataSourceContext dataSourceContext = new DataSourceContext(newSource, in newSourcePath);
						DataSourceContextChanged dataSourceContextChanged = new DataSourceContextChanged(element, in bindingId, in currentResolvedContext, in dataSourceContext);
						binding.OnDataSourceChanged(in dataSourceContextChanged);
					}
					Binding binding2 = bindingData.binding;
					BindingActivationContext bindingActivationContext = new BindingActivationContext(element, in bindingId);
					binding2.OnDeactivated(in bindingActivationContext);
					this.m_DataSourceTracker.DecreaseBindingRefCount(ref bindingData);
					this.m_BindingsTracker.StopTrackingBinding(element, bindingData);
					this.ReleasePoolBindingData(bindingData);
				}
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007ED4 File Offset: 0x000060D4
		internal void TransferBindingRequests(VisualElement element)
		{
			bool flag = !this.m_BindingsTracker.IsTrackingElement(element);
			if (!flag)
			{
				DataBindingManager.BindingDataCollection collection;
				bool flag2 = this.m_BindingsTracker.TryGetBindingCollection(element, out collection);
				if (flag2)
				{
					List<DataBindingManager.BindingData> bindings = collection.GetBindings();
					while (bindings.Count > 0)
					{
						List<DataBindingManager.BindingData> list = bindings;
						DataBindingManager.BindingData binding = list[list.Count - 1];
						DataBindingManager.CreateBindingRequest(element, in binding.target.bindingId, binding.binding);
						this.UnregisterBinding(element, in binding.target.bindingId);
					}
				}
				this.m_BindingsTracker.StopTrackingElement(element);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00007F71 File Offset: 0x00006171
		public void InvalidateCachedDataSource(HashSet<VisualElement> addedOrMovedElements, HashSet<VisualElement> removedElements)
		{
			this.m_DataSourceTracker.InvalidateCachedDataSource(addedOrMovedElements, removedElements);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007F82 File Offset: 0x00006182
		public void Dispose()
		{
			this.m_BindingsTracker.Dispose();
			this.m_DataSourceTracker.Dispose();
			this.m_DetectedChangesFromUI.Clear();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007FAC File Offset: 0x000061AC
		public static void CreateBindingRequest(VisualElement target, in BindingId bindingId, Binding binding)
		{
			List<DataBindingManager.BindingRequest> requests = (List<DataBindingManager.BindingRequest>)target.GetProperty(DataBindingManager.k_RequestBindingPropertyName);
			bool flag = requests == null;
			if (flag)
			{
				requests = new List<DataBindingManager.BindingRequest>();
				target.SetProperty(DataBindingManager.k_RequestBindingPropertyName, requests);
			}
			for (int i = 0; i < requests.Count; i++)
			{
				DataBindingManager.BindingRequest request = requests[i];
				bool flag2 = (in request.bindingId) == (in bindingId);
				if (flag2)
				{
					requests[i] = request.CancelRequest();
				}
			}
			requests.Add(new DataBindingManager.BindingRequest(in bindingId, binding, true));
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000803C File Offset: 0x0000623C
		public void ProcessBindingRequests(VisualElement element)
		{
			List<DataBindingManager.BindingRequest> requests = (List<DataBindingManager.BindingRequest>)element.GetProperty(DataBindingManager.k_RequestBindingPropertyName);
			bool flag = requests == null;
			if (!flag)
			{
				for (int index = 0; index < requests.Count; index++)
				{
					DataBindingManager.BindingRequest request = requests[index];
					bool flag2 = !request.shouldProcess;
					if (!flag2)
					{
						bool flag3 = (in request.bindingId) == (in DataBindingManager.k_ClearBindingsToken);
						if (flag3)
						{
							this.ClearAllBindings(element);
						}
						else
						{
							bool flag4 = (in request.bindingId) == (in BindingId.Invalid);
							if (flag4)
							{
								IPanel panel = element.panel;
								Panel panel2 = panel as Panel;
								string panelName = ((panel2 != null) ? panel2.name : null) ?? panel.visualTree.name;
								Debug.LogError(string.Concat(new string[]
								{
									"[UI Toolkit] Trying to set a binding on `",
									string.IsNullOrWhiteSpace(element.name) ? "<no name>" : element.name,
									" (",
									TypeUtility.GetTypeDisplayName(element.GetType()),
									")` without setting the \"property\" attribute is not supported (",
									panelName,
									")."
								}));
							}
							else
							{
								bool flag5 = request.binding != null;
								if (flag5)
								{
									this.RegisterBinding(element, in request.bindingId, request.binding);
								}
								else
								{
									this.UnregisterBinding(element, in request.bindingId);
								}
							}
						}
					}
				}
				requests.Clear();
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000081B0 File Offset: 0x000063B0
		private void ClearAllBindings(VisualElement element)
		{
			List<DataBindingManager.BindingData> list = CollectionPool<List<DataBindingManager.BindingData>, DataBindingManager.BindingData>.Get();
			try
			{
				list.AddRange(this.GetBindingData(element));
				foreach (DataBindingManager.BindingData bindingData in list)
				{
					this.UnregisterBinding(element, in bindingData.target.bindingId);
				}
			}
			finally
			{
				CollectionPool<List<DataBindingManager.BindingData>, DataBindingManager.BindingData>.Release(list);
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008240 File Offset: 0x00006440
		internal static bool AnyPendingBindingRequests(VisualElement element)
		{
			List<DataBindingManager.BindingRequest> requests = (List<DataBindingManager.BindingRequest>)element.GetProperty(DataBindingManager.k_RequestBindingPropertyName);
			bool flag = requests == null;
			return !flag && requests.Count > 0;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008278 File Offset: 0x00006478
		internal static bool TryGetBindingRequest(VisualElement element, in BindingId bindingId, out Binding binding)
		{
			List<DataBindingManager.BindingRequest> requests = (List<DataBindingManager.BindingRequest>)element.GetProperty(DataBindingManager.k_RequestBindingPropertyName);
			bool flag = requests == null;
			bool flag2;
			if (flag)
			{
				binding = null;
				flag2 = false;
			}
			else
			{
				for (int i = requests.Count - 1; i >= 0; i--)
				{
					DataBindingManager.BindingRequest request = requests[i];
					bool flag3 = (in bindingId) != (in request.bindingId);
					if (!flag3)
					{
						binding = request.binding;
						return true;
					}
				}
				binding = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000082F7 File Offset: 0x000064F7
		public void DirtyBindingOrder()
		{
			this.m_BindingsTracker.SetDirty();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008306 File Offset: 0x00006506
		public void TrackDataSource(object previous, object current)
		{
			this.m_DataSourceTracker.DecreaseRefCount(previous);
			this.m_DataSourceTracker.IncreaseRefCount(current);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008323 File Offset: 0x00006523
		public void ClearSourceCache()
		{
			this.m_DataSourceTracker.ClearSourceCache();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008334 File Offset: 0x00006534
		public DataBindingManager.BindingData GetPooledBindingData(BindingTarget target, Binding binding)
		{
			bool flag = this.m_BindingDataLocalPool.Count > 0;
			DataBindingManager.BindingData data;
			if (flag)
			{
				List<DataBindingManager.BindingData> bindingDataLocalPool = this.m_BindingDataLocalPool;
				data = bindingDataLocalPool[bindingDataLocalPool.Count - 1];
				this.m_BindingDataLocalPool.RemoveAt(this.m_BindingDataLocalPool.Count - 1);
			}
			else
			{
				data = new DataBindingManager.BindingData();
			}
			data.target = target;
			data.binding = binding;
			return data;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000839E File Offset: 0x0000659E
		public void ReleasePoolBindingData(DataBindingManager.BindingData data)
		{
			data.Reset();
			this.m_BindingDataLocalPool.Add(data);
		}

		// Token: 0x04000101 RID: 257
		private readonly List<DataBindingManager.BindingData> m_BindingDataLocalPool = new List<DataBindingManager.BindingData>(64);

		// Token: 0x04000102 RID: 258
		private static readonly PropertyName k_RequestBindingPropertyName = "__unity-binding-request";

		// Token: 0x04000103 RID: 259
		private static readonly BindingId k_ClearBindingsToken = "$__BindingManager--ClearAllBindings";

		// Token: 0x04000104 RID: 260
		internal static BindingLogLevel globalLogLevel = BindingLogLevel.All;

		// Token: 0x04000105 RID: 261
		private BindingLogLevel? m_LogLevel;

		// Token: 0x04000106 RID: 262
		private static readonly List<DataBindingManager.BindingData> s_Empty = new List<DataBindingManager.BindingData>();

		// Token: 0x04000107 RID: 263
		private readonly BaseVisualElementPanel m_Panel;

		// Token: 0x04000108 RID: 264
		private readonly DataBindingManager.HierarchyDataSourceTracker m_DataSourceTracker;

		// Token: 0x04000109 RID: 265
		private readonly DataBindingManager.HierarchyBindingTracker m_BindingsTracker;

		// Token: 0x0400010A RID: 266
		private readonly List<DataBindingManager.ChangesFromUI> m_DetectedChangesFromUI;

		// Token: 0x02000030 RID: 48
		private readonly struct BindingRequest
		{
			// Token: 0x060001A3 RID: 419 RVA: 0x000083E5 File Offset: 0x000065E5
			public BindingRequest(in BindingId bindingId, Binding binding, bool shouldProcess = true)
			{
				this.bindingId = bindingId;
				this.binding = binding;
				this.shouldProcess = shouldProcess;
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x00008404 File Offset: 0x00006604
			public DataBindingManager.BindingRequest CancelRequest()
			{
				return new DataBindingManager.BindingRequest(in this.bindingId, this.binding, false);
			}

			// Token: 0x0400010B RID: 267
			public readonly BindingId bindingId;

			// Token: 0x0400010C RID: 268
			public readonly Binding binding;

			// Token: 0x0400010D RID: 269
			public readonly bool shouldProcess;
		}

		// Token: 0x02000031 RID: 49
		private struct BindingDataCollection : IDisposable
		{
			// Token: 0x060001A5 RID: 421 RVA: 0x00008428 File Offset: 0x00006628
			public static DataBindingManager.BindingDataCollection Create()
			{
				return new DataBindingManager.BindingDataCollection
				{
					m_BindingPerId = CollectionPool<Dictionary<BindingId, DataBindingManager.BindingData>, KeyValuePair<BindingId, DataBindingManager.BindingData>>.Get(),
					m_Bindings = CollectionPool<List<DataBindingManager.BindingData>, DataBindingManager.BindingData>.Get()
				};
			}

			// Token: 0x060001A6 RID: 422 RVA: 0x0000845C File Offset: 0x0000665C
			public void AddBindingData(DataBindingManager.BindingData bindingData)
			{
				DataBindingManager.BindingData toRemove;
				bool flag = this.m_BindingPerId.TryGetValue(bindingData.target.bindingId, out toRemove);
				if (flag)
				{
					this.m_Bindings.Remove(toRemove);
				}
				this.m_BindingPerId[bindingData.target.bindingId] = bindingData;
				this.m_Bindings.Add(bindingData);
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x000084BC File Offset: 0x000066BC
			public bool TryGetBindingData(in BindingId bindingId, out DataBindingManager.BindingData data)
			{
				return this.m_BindingPerId.TryGetValue(bindingId, out data);
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x000084E0 File Offset: 0x000066E0
			public bool RemoveBindingData(DataBindingManager.BindingData bindingData)
			{
				DataBindingManager.BindingData toRemove;
				bool flag = !this.m_BindingPerId.TryGetValue(bindingData.target.bindingId, out toRemove);
				return !flag && this.m_Bindings.Remove(toRemove) && this.m_BindingPerId.Remove(toRemove.target.bindingId);
			}

			// Token: 0x060001A9 RID: 425 RVA: 0x0000853C File Offset: 0x0000673C
			public List<DataBindingManager.BindingData> GetBindings()
			{
				return this.m_Bindings;
			}

			// Token: 0x060001AA RID: 426 RVA: 0x00008554 File Offset: 0x00006754
			public int GetBindingCount()
			{
				return this.m_Bindings.Count;
			}

			// Token: 0x060001AB RID: 427 RVA: 0x00008574 File Offset: 0x00006774
			public void Dispose()
			{
				bool flag = this.m_BindingPerId != null;
				if (flag)
				{
					CollectionPool<Dictionary<BindingId, DataBindingManager.BindingData>, KeyValuePair<BindingId, DataBindingManager.BindingData>>.Release(this.m_BindingPerId);
				}
				this.m_BindingPerId = null;
				bool flag2 = this.m_Bindings != null;
				if (flag2)
				{
					CollectionPool<List<DataBindingManager.BindingData>, DataBindingManager.BindingData>.Release(this.m_Bindings);
				}
				this.m_Bindings = null;
			}

			// Token: 0x0400010E RID: 270
			private Dictionary<BindingId, DataBindingManager.BindingData> m_BindingPerId;

			// Token: 0x0400010F RID: 271
			private List<DataBindingManager.BindingData> m_Bindings;
		}

		// Token: 0x02000032 RID: 50
		internal class BindingData
		{
			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060001AC RID: 428 RVA: 0x000085C2 File Offset: 0x000067C2
			// (set) Token: 0x060001AD RID: 429 RVA: 0x000085CA File Offset: 0x000067CA
			public object localDataSource { get; set; }

			// Token: 0x060001AE RID: 430 RVA: 0x000085D4 File Offset: 0x000067D4
			public void Reset()
			{
				this.version += 1L;
				this.target = default(BindingTarget);
				this.binding = null;
				this.localDataSource = null;
				this.m_LastContext = default(DataSourceContext);
				this.m_SourceToUILastUpdate = null;
				this.m_UIToSourceLastUpdate = null;
			}

			// Token: 0x17000034 RID: 52
			// (set) Token: 0x060001AF RID: 431 RVA: 0x00008630 File Offset: 0x00006830
			public DataSourceContext context
			{
				set
				{
					bool flag = this.m_LastContext.dataSource == value.dataSource && this.m_LastContext.dataSourcePath == value.dataSourcePath;
					if (!flag)
					{
						DataSourceContext previous = this.m_LastContext;
						this.m_LastContext = value;
						Binding binding = this.binding;
						DataSourceContextChanged dataSourceContextChanged = new DataSourceContextChanged(this.target.element, in this.target.bindingId, in previous, in value);
						binding.OnDataSourceChanged(in dataSourceContextChanged);
						this.binding.MarkDirty();
					}
				}
			}

			// Token: 0x04000110 RID: 272
			public long version;

			// Token: 0x04000111 RID: 273
			public BindingTarget target;

			// Token: 0x04000112 RID: 274
			public Binding binding;

			// Token: 0x04000113 RID: 275
			private DataSourceContext m_LastContext;

			// Token: 0x04000115 RID: 277
			public BindingResult? m_SourceToUILastUpdate;

			// Token: 0x04000116 RID: 278
			public BindingResult? m_UIToSourceLastUpdate;
		}

		// Token: 0x02000033 RID: 51
		internal readonly struct ChangesFromUI
		{
			// Token: 0x060001B1 RID: 433 RVA: 0x000086BA File Offset: 0x000068BA
			public ChangesFromUI(DataBindingManager.BindingData bindingData)
			{
				this.bindingData = bindingData;
				this.version = bindingData.version;
				this.binding = bindingData.binding;
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060001B2 RID: 434 RVA: 0x000086DC File Offset: 0x000068DC
			public bool IsValid
			{
				get
				{
					return this.version == this.bindingData.version && this.binding == this.bindingData.binding;
				}
			}

			// Token: 0x04000117 RID: 279
			public readonly long version;

			// Token: 0x04000118 RID: 280
			public readonly Binding binding;

			// Token: 0x04000119 RID: 281
			public readonly DataBindingManager.BindingData bindingData;
		}

		// Token: 0x02000034 RID: 52
		private class HierarchyBindingTracker : IDisposable
		{
			// Token: 0x060001B3 RID: 435 RVA: 0x00008708 File Offset: 0x00006908
			public int GetTrackedElementsCount()
			{
				return this.m_BoundElements.Count;
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x00008728 File Offset: 0x00006928
			public List<VisualElement> GetBoundElements()
			{
				bool isDirty = this.m_IsDirty;
				if (isDirty)
				{
					this.OrderBindings(this.m_Panel.visualTree);
				}
				return this.m_OrderedBindings;
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x0000875C File Offset: 0x0000695C
			public HierarchyBindingTracker(BaseVisualElementPanel panel)
			{
				this.m_Panel = panel;
				this.m_BindingSorter = new DataBindingManager.HierarchyBindingTracker.HierarchicalBindingsSorter();
				this.m_BindingDataPerElement = new Dictionary<VisualElement, DataBindingManager.BindingDataCollection>();
				this.m_BoundElements = new HashSet<VisualElement>();
				this.m_OrderedBindings = new List<VisualElement>();
				this.m_IsDirty = true;
				this.m_OnPropertyChanged = new EventCallback<PropertyChangedEvent, Dictionary<VisualElement, DataBindingManager.BindingDataCollection>>(this.OnPropertyChanged);
			}

			// Token: 0x060001B6 RID: 438 RVA: 0x000087BD File Offset: 0x000069BD
			public void SetDirty()
			{
				this.m_IsDirty = true;
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x000087C8 File Offset: 0x000069C8
			public bool TryGetBindingCollection(VisualElement element, out DataBindingManager.BindingDataCollection collection)
			{
				return this.m_BindingDataPerElement.TryGetValue(element, out collection);
			}

			// Token: 0x060001B8 RID: 440 RVA: 0x000087E8 File Offset: 0x000069E8
			public bool IsTrackingElement(VisualElement element)
			{
				return this.m_BoundElements.Contains(element);
			}

			// Token: 0x060001B9 RID: 441 RVA: 0x00008808 File Offset: 0x00006A08
			public void StartTrackingBinding(VisualElement element, DataBindingManager.BindingData binding)
			{
				bool flag = this.m_BoundElements.Add(element);
				DataBindingManager.BindingDataCollection collection;
				if (flag)
				{
					collection = DataBindingManager.BindingDataCollection.Create();
					this.m_BindingDataPerElement.Add(element, collection);
					element.RegisterCallback<PropertyChangedEvent, Dictionary<VisualElement, DataBindingManager.BindingDataCollection>>(this.m_OnPropertyChanged, this.m_BindingDataPerElement, TrickleDown.NoTrickleDown);
				}
				else
				{
					bool flag2 = !this.m_BindingDataPerElement.TryGetValue(element, out collection);
					if (flag2)
					{
						throw new InvalidOperationException("Trying to add a binding to an element which doesn't have a binding collection. This is an internal bug. Please report using `Help > Report a Bug...`");
					}
				}
				binding.binding.MarkDirty();
				collection.AddBindingData(binding);
				this.m_BindingDataPerElement[element] = collection;
				this.SetDirty();
			}

			// Token: 0x060001BA RID: 442 RVA: 0x000088A0 File Offset: 0x00006AA0
			private void OnPropertyChanged(PropertyChangedEvent evt, Dictionary<VisualElement, DataBindingManager.BindingDataCollection> bindingCollection)
			{
				VisualElement target = evt.target as VisualElement;
				bool flag = target == null;
				if (flag)
				{
					throw new InvalidOperationException("Trying to track property changes on a non 'VisualElement'. This is an internal bug. Please report using `Help > Report a Bug...`");
				}
				DataBindingManager.BindingDataCollection collection;
				bool flag2 = !bindingCollection.TryGetValue(target, out collection);
				if (flag2)
				{
					throw new InvalidOperationException("Trying to track property changes on a 'VisualElement' that is not being tracked. This is an internal bug. Please report using `Help > Report a Bug...`");
				}
				BindingId property = evt.property;
				DataBindingManager.BindingData bindingData;
				Binding current;
				bool flag3 = collection.TryGetBindingData(in property, out bindingData) && target.TryGetBinding(evt.property, out current) && bindingData.binding == current;
				if (flag3)
				{
					this.m_Panel.dataBindingManager.m_DetectedChangesFromUI.Add(new DataBindingManager.ChangesFromUI(bindingData));
				}
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00008944 File Offset: 0x00006B44
			public void StopTrackingBinding(VisualElement element, DataBindingManager.BindingData binding)
			{
				DataBindingManager.BindingDataCollection collection;
				bool flag = this.m_BoundElements.Contains(element) && this.m_BindingDataPerElement.TryGetValue(element, out collection);
				if (flag)
				{
					collection.RemoveBindingData(binding);
					bool flag2 = collection.GetBindingCount() == 0;
					if (flag2)
					{
						this.StopTrackingElement(element);
						element.UnregisterCallback<PropertyChangedEvent, Dictionary<VisualElement, DataBindingManager.BindingDataCollection>>(this.m_OnPropertyChanged, TrickleDown.NoTrickleDown);
					}
					else
					{
						this.m_BindingDataPerElement[element] = collection;
					}
					this.SetDirty();
					return;
				}
				throw new InvalidOperationException("Trying to remove a binding to an element which doesn't have a binding collection. This is an internal bug. Please report using `Help > Report a Bug...`");
			}

			// Token: 0x060001BC RID: 444 RVA: 0x000089D0 File Offset: 0x00006BD0
			public void StopTrackingElement(VisualElement element)
			{
				DataBindingManager.BindingDataCollection collection;
				bool flag = this.m_BindingDataPerElement.TryGetValue(element, out collection);
				if (flag)
				{
					collection.Dispose();
				}
				this.m_BindingDataPerElement.Remove(element);
				this.m_BoundElements.Remove(element);
				this.SetDirty();
			}

			// Token: 0x060001BD RID: 445 RVA: 0x00008A1C File Offset: 0x00006C1C
			public void Dispose()
			{
				foreach (KeyValuePair<VisualElement, DataBindingManager.BindingDataCollection> kvp in this.m_BindingDataPerElement)
				{
					kvp.Value.Dispose();
				}
				this.m_BindingDataPerElement.Clear();
				this.m_BoundElements.Clear();
				this.m_OrderedBindings.Clear();
			}

			// Token: 0x060001BE RID: 446 RVA: 0x00008AA4 File Offset: 0x00006CA4
			private void OrderBindings(VisualElement root)
			{
				this.m_OrderedBindings.Clear();
				this.m_BindingSorter.boundElements = this.m_BoundElements;
				this.m_BindingSorter.results = this.m_OrderedBindings;
				this.m_BindingSorter.Traverse(root);
				this.m_IsDirty = false;
			}

			// Token: 0x0400011A RID: 282
			private readonly BaseVisualElementPanel m_Panel;

			// Token: 0x0400011B RID: 283
			private readonly DataBindingManager.HierarchyBindingTracker.HierarchicalBindingsSorter m_BindingSorter;

			// Token: 0x0400011C RID: 284
			private readonly Dictionary<VisualElement, DataBindingManager.BindingDataCollection> m_BindingDataPerElement;

			// Token: 0x0400011D RID: 285
			private readonly HashSet<VisualElement> m_BoundElements;

			// Token: 0x0400011E RID: 286
			private readonly List<VisualElement> m_OrderedBindings;

			// Token: 0x0400011F RID: 287
			private bool m_IsDirty;

			// Token: 0x04000120 RID: 288
			private EventCallback<PropertyChangedEvent, Dictionary<VisualElement, DataBindingManager.BindingDataCollection>> m_OnPropertyChanged;

			// Token: 0x02000035 RID: 53
			private class HierarchicalBindingsSorter : HierarchyTraversal
			{
				// Token: 0x17000036 RID: 54
				// (get) Token: 0x060001BF RID: 447 RVA: 0x00008AF6 File Offset: 0x00006CF6
				// (set) Token: 0x060001C0 RID: 448 RVA: 0x00008AFE File Offset: 0x00006CFE
				public HashSet<VisualElement> boundElements { get; set; }

				// Token: 0x17000037 RID: 55
				// (get) Token: 0x060001C1 RID: 449 RVA: 0x00008B07 File Offset: 0x00006D07
				// (set) Token: 0x060001C2 RID: 450 RVA: 0x00008B0F File Offset: 0x00006D0F
				public List<VisualElement> results { get; set; }

				// Token: 0x060001C3 RID: 451 RVA: 0x00008B18 File Offset: 0x00006D18
				public override void TraverseRecursive(VisualElement element, int depth)
				{
					bool flag = this.boundElements.Count == this.results.Count;
					if (!flag)
					{
						bool flag2 = this.boundElements.Contains(element);
						if (flag2)
						{
							this.results.Add(element);
						}
						base.Recurse(element, depth);
					}
				}
			}
		}

		// Token: 0x02000036 RID: 54
		private class HierarchyDataSourceTracker : IDisposable
		{
			// Token: 0x060001C5 RID: 453 RVA: 0x00008B74 File Offset: 0x00006D74
			private DataBindingManager.HierarchyDataSourceTracker.SourceInfo GetPooledSourceInfo()
			{
				bool flag = this.m_SourceInfosPool.Count > 0;
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo info;
				if (flag)
				{
					List<DataBindingManager.HierarchyDataSourceTracker.SourceInfo> sourceInfosPool = this.m_SourceInfosPool;
					info = sourceInfosPool[sourceInfosPool.Count - 1];
					this.m_SourceInfosPool.RemoveAt(this.m_SourceInfosPool.Count - 1);
				}
				else
				{
					info = new DataBindingManager.HierarchyDataSourceTracker.SourceInfo();
				}
				return info;
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x00008BD0 File Offset: 0x00006DD0
			private void ReleasePooledSourceInfo(DataBindingManager.HierarchyDataSourceTracker.SourceInfo info)
			{
				info.lastVersion = long.MinValue;
				info.refCount = 0;
				List<PropertyPath> detectedChangesNoAlloc = info.detectedChangesNoAlloc;
				if (detectedChangesNoAlloc != null)
				{
					detectedChangesNoAlloc.Clear();
				}
				this.m_SourceInfosPool.Add(info);
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x00008C0C File Offset: 0x00006E0C
			public HierarchyDataSourceTracker(DataBindingManager manager)
			{
				this.m_DataBindingManager = manager;
				this.m_ResolvedHierarchicalDataSourceContext = new Dictionary<VisualElement, DataSourceContext>();
				this.m_BindingRefCount = new Dictionary<Binding, int>();
				DataBindingManager.HierarchyDataSourceTracker.ObjectComparer dataSourceComparer = new DataBindingManager.HierarchyDataSourceTracker.ObjectComparer();
				this.m_SourceInfos = new Dictionary<object, DataBindingManager.HierarchyDataSourceTracker.SourceInfo>(dataSourceComparer);
				this.m_SourcesToRemove = new HashSet<object>(dataSourceComparer);
				this.m_InvalidateResolvedDataSources = new DataBindingManager.HierarchyDataSourceTracker.InvalidateDataSourcesTraversal(this);
				this.m_Handler = new EventHandler<BindablePropertyChangedEventArgs>(this.TrackPropertyChanges);
				this.m_VisualElementHandler = new EventCallback<PropertyChangedEvent, VisualElement>(this.OnVisualElementPropertyChanged);
			}

			// Token: 0x060001C8 RID: 456 RVA: 0x00008C98 File Offset: 0x00006E98
			internal void IncreaseBindingRefCount(ref DataBindingManager.BindingData bindingData)
			{
				Binding binding = bindingData.binding;
				bool flag = binding == null;
				if (!flag)
				{
					int refCount;
					bool flag2 = !this.m_BindingRefCount.TryGetValue(binding, out refCount);
					if (flag2)
					{
						refCount = 0;
					}
					IDataSourceProvider dataSourceProvider = binding as IDataSourceProvider;
					bool flag3 = dataSourceProvider != null;
					if (flag3)
					{
						this.IncreaseRefCount(dataSourceProvider.dataSource);
						bindingData.localDataSource = dataSourceProvider.dataSource;
					}
					this.m_BindingRefCount[binding] = refCount + 1;
				}
			}

			// Token: 0x060001C9 RID: 457 RVA: 0x00008D14 File Offset: 0x00006F14
			internal void DecreaseBindingRefCount(ref DataBindingManager.BindingData bindingData)
			{
				Binding binding = bindingData.binding;
				bool flag = binding == null;
				if (!flag)
				{
					int refCount;
					bool flag2 = !this.m_BindingRefCount.TryGetValue(binding, out refCount);
					if (flag2)
					{
						throw new InvalidOperationException("Trying to release a binding that isn't tracked. This is an internal bug. Please report using `Help > Report a Bug...`");
					}
					bool flag3 = refCount == 1;
					if (flag3)
					{
						this.m_BindingRefCount.Remove(binding);
					}
					else
					{
						this.m_BindingRefCount[binding] = refCount - 1;
					}
					IDataSourceProvider dataSourceProvider = binding as IDataSourceProvider;
					bool flag4 = dataSourceProvider != null;
					if (flag4)
					{
						this.DecreaseRefCount(dataSourceProvider.dataSource);
					}
				}
			}

			// Token: 0x060001CA RID: 458 RVA: 0x00008DA4 File Offset: 0x00006FA4
			internal void IncreaseRefCount(object dataSource)
			{
				bool flag = dataSource == null;
				if (!flag)
				{
					this.m_SourcesToRemove.Remove(dataSource);
					DataBindingManager.HierarchyDataSourceTracker.SourceInfo info;
					bool flag2 = !this.m_SourceInfos.TryGetValue(dataSource, out info);
					if (flag2)
					{
						info = (this.m_SourceInfos[dataSource] = this.GetPooledSourceInfo());
						INotifyBindablePropertyChanged notifier = dataSource as INotifyBindablePropertyChanged;
						bool flag3 = notifier != null;
						if (flag3)
						{
							notifier.propertyChanged += this.m_Handler;
						}
						VisualElement element = dataSource as VisualElement;
						bool flag4 = element != null;
						if (flag4)
						{
							element.RegisterCallback<PropertyChangedEvent, VisualElement>(this.m_VisualElementHandler, element, TrickleDown.NoTrickleDown);
						}
					}
					DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo = info;
					int num = sourceInfo.refCount + 1;
					sourceInfo.refCount = num;
				}
			}

			// Token: 0x060001CB RID: 459 RVA: 0x00008E50 File Offset: 0x00007050
			private void OnVisualElementPropertyChanged(PropertyChangedEvent evt, VisualElement element)
			{
				BindingId property = evt.property;
				this.TrackPropertyChanges(element, in property);
			}

			// Token: 0x060001CC RID: 460 RVA: 0x00008E74 File Offset: 0x00007074
			internal void DecreaseRefCount(object dataSource)
			{
				bool flag = dataSource == null;
				if (!flag)
				{
					DataBindingManager.HierarchyDataSourceTracker.SourceInfo info;
					bool flag2 = !this.m_SourceInfos.TryGetValue(dataSource, out info) || info.refCount == 0;
					if (flag2)
					{
						throw new InvalidOperationException("Trying to release a data source that isn't tracked. This is an internal bug. Please report using `Help > Report a Bug...`");
					}
					bool flag3 = info.refCount == 1;
					if (flag3)
					{
						info.refCount = 0;
						this.m_SourcesToRemove.Add(dataSource);
						INotifyBindablePropertyChanged notifier = dataSource as INotifyBindablePropertyChanged;
						bool flag4 = notifier != null;
						if (flag4)
						{
							notifier.propertyChanged -= this.m_Handler;
						}
						VisualElement element = dataSource as VisualElement;
						bool flag5 = element != null;
						if (flag5)
						{
							element.UnregisterCallback<PropertyChangedEvent, VisualElement>(this.m_VisualElementHandler, TrickleDown.NoTrickleDown);
						}
					}
					else
					{
						DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo = info;
						int num = sourceInfo.refCount - 1;
						sourceInfo.refCount = num;
					}
				}
			}

			// Token: 0x060001CD RID: 461 RVA: 0x00008F38 File Offset: 0x00007138
			public int GetTrackedDataSourcesCount()
			{
				return this.m_ResolvedHierarchicalDataSourceContext.Count;
			}

			// Token: 0x060001CE RID: 462 RVA: 0x00008F58 File Offset: 0x00007158
			public List<PropertyPath> GetChangesFromSource(object dataSource)
			{
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo info;
				return this.m_SourceInfos.TryGetValue(dataSource, out info) ? info.detectedChangesNoAlloc : null;
			}

			// Token: 0x060001CF RID: 463 RVA: 0x00008F84 File Offset: 0x00007184
			public void ClearChangesFromSource(object dataSource)
			{
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo info;
				bool flag = !this.m_SourceInfos.TryGetValue(dataSource, out info);
				if (!flag)
				{
					List<PropertyPath> detectedChangesNoAlloc = info.detectedChangesNoAlloc;
					if (detectedChangesNoAlloc != null)
					{
						detectedChangesNoAlloc.Clear();
					}
				}
			}

			// Token: 0x060001D0 RID: 464 RVA: 0x00008FBC File Offset: 0x000071BC
			public void InvalidateCachedDataSource(HashSet<VisualElement> elements, HashSet<VisualElement> removedElements)
			{
				List<VisualElement> toInvalidate = CollectionPool<List<VisualElement>, VisualElement>.Get();
				try
				{
					foreach (VisualElement element in elements)
					{
						toInvalidate.Add(element);
					}
					this.m_InvalidateResolvedDataSources.Invalidate(toInvalidate, removedElements);
				}
				finally
				{
					CollectionPool<List<VisualElement>, VisualElement>.Release(toInvalidate);
				}
			}

			// Token: 0x060001D1 RID: 465 RVA: 0x0000903C File Offset: 0x0000723C
			public DataSourceContext GetResolvedDataSourceContext(VisualElement element, DataBindingManager.BindingData bindingData)
			{
				object localDataSource = null;
				PropertyPath localDataSourcePath = default(PropertyPath);
				IDataSourceProvider dataSourceProvider = bindingData.binding as IDataSourceProvider;
				bool flag = dataSourceProvider != null;
				if (flag)
				{
					localDataSource = dataSourceProvider.dataSource;
					localDataSourcePath = dataSourceProvider.dataSourcePath;
				}
				object lastLocalDataSource = bindingData.localDataSource;
				object resolvedDataSource = localDataSource;
				PropertyPath resolvedDataSourcePath = localDataSourcePath;
				try
				{
					bool flag2 = localDataSource == null;
					if (flag2)
					{
						this.DecreaseRefCount(lastLocalDataSource);
						DataSourceContext resolvedHierarchicalContext = this.GetHierarchicalDataSourceContext(element);
						resolvedDataSource = resolvedHierarchicalContext.dataSource;
						PropertyPath propertyPath;
						if (localDataSourcePath.IsEmpty)
						{
							propertyPath = resolvedHierarchicalContext.dataSourcePath;
						}
						else
						{
							PropertyPath dataSourcePath = resolvedHierarchicalContext.dataSourcePath;
							propertyPath = PropertyPath.Combine(in dataSourcePath, in localDataSourcePath);
						}
						resolvedDataSourcePath = propertyPath;
						return new DataSourceContext(resolvedDataSource, in resolvedDataSourcePath);
					}
					bool flag3 = localDataSource != lastLocalDataSource;
					if (flag3)
					{
						this.DecreaseRefCount(lastLocalDataSource);
						this.IncreaseRefCount(localDataSource);
					}
				}
				finally
				{
					bindingData.localDataSource = localDataSource;
					DataSourceContext newResolvedContext = new DataSourceContext(resolvedDataSource, in resolvedDataSourcePath);
					bindingData.context = newResolvedContext;
				}
				return new DataSourceContext(resolvedDataSource, in resolvedDataSourcePath);
			}

			// Token: 0x060001D2 RID: 466 RVA: 0x00009140 File Offset: 0x00007340
			private void TrackPropertyChanges(object sender, BindablePropertyChangedEventArgs args)
			{
				BindingId propertyName = args.propertyName;
				this.TrackPropertyChanges(sender, in propertyName);
			}

			// Token: 0x060001D3 RID: 467 RVA: 0x00009164 File Offset: 0x00007364
			private void TrackPropertyChanges(object sender, PropertyPath propertyPath)
			{
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo info;
				bool flag = !this.m_SourceInfos.TryGetValue(sender, out info);
				if (!flag)
				{
					List<PropertyPath> list = info.detectedChanges;
					list.Add(propertyPath);
				}
			}

			// Token: 0x060001D4 RID: 468 RVA: 0x00009198 File Offset: 0x00007398
			public bool TryGetLastVersion(object source, out long version)
			{
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo;
				bool flag = source != null && this.m_SourceInfos.TryGetValue(source, out sourceInfo);
				bool flag2;
				if (flag)
				{
					version = sourceInfo.lastVersion;
					flag2 = true;
				}
				else
				{
					version = -1L;
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x060001D5 RID: 469 RVA: 0x000091D4 File Offset: 0x000073D4
			public void UpdateVersion(object source, long version)
			{
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo info = this.m_SourceInfos[source];
				info.lastVersion = version;
				this.m_SourceInfos[source] = info;
			}

			// Token: 0x060001D6 RID: 470 RVA: 0x00009208 File Offset: 0x00007408
			internal DataSourceContext GetHierarchicalDataSourceContext(VisualElement element)
			{
				DataSourceContext context;
				bool flag = this.m_ResolvedHierarchicalDataSourceContext.TryGetValue(element, out context);
				DataSourceContext dataSourceContext;
				if (flag)
				{
					dataSourceContext = context;
				}
				else
				{
					VisualElement current = element;
					PropertyPath path = default(PropertyPath);
					while (current != null)
					{
						PropertyPath propertyPath = current.dataSourcePath;
						bool flag2 = !propertyPath.IsEmpty;
						if (flag2)
						{
							propertyPath = current.dataSourcePath;
							path = PropertyPath.Combine(in propertyPath, in path);
						}
						bool flag3 = current.dataSource != null;
						if (flag3)
						{
							object source = current.dataSource;
							return this.m_ResolvedHierarchicalDataSourceContext[element] = new DataSourceContext(source, in path);
						}
						current = current.hierarchy.parent;
					}
					dataSourceContext = (this.m_ResolvedHierarchicalDataSourceContext[element] = new DataSourceContext(null, in path));
				}
				return dataSourceContext;
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x000092D9 File Offset: 0x000074D9
			internal void RemoveHierarchyDataSourceContextFromElement(VisualElement element)
			{
				this.m_ResolvedHierarchicalDataSourceContext.Remove(element);
			}

			// Token: 0x060001D8 RID: 472 RVA: 0x000092E9 File Offset: 0x000074E9
			public void Dispose()
			{
				this.m_ResolvedHierarchicalDataSourceContext.Clear();
				this.m_BindingRefCount.Clear();
				this.m_SourcesToRemove.Clear();
				this.m_SourceInfosPool.Clear();
				this.m_SourceInfos.Clear();
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x00009328 File Offset: 0x00007528
			public void ClearSourceCache()
			{
				foreach (object toRemove in this.m_SourcesToRemove)
				{
					DataBindingManager.HierarchyDataSourceTracker.SourceInfo info;
					bool flag = this.m_SourceInfos.TryGetValue(toRemove, out info);
					if (!flag)
					{
						throw new InvalidOperationException("Trying to release a data source that isn't tracked. This is an internal bug. Please report using `Help > Report a Bug...`");
					}
					bool flag2 = info.refCount == 0;
					if (!flag2)
					{
						throw new InvalidOperationException("Trying to release a data source that is still being referenced. This is an internal bug. Please report using `Help > Report a Bug...`");
					}
					this.m_SourceInfos.Remove(toRemove);
					this.ReleasePooledSourceInfo(info);
				}
				this.m_SourcesToRemove.Clear();
			}

			// Token: 0x04000123 RID: 291
			private readonly List<DataBindingManager.HierarchyDataSourceTracker.SourceInfo> m_SourceInfosPool = new List<DataBindingManager.HierarchyDataSourceTracker.SourceInfo>();

			// Token: 0x04000124 RID: 292
			private readonly DataBindingManager m_DataBindingManager;

			// Token: 0x04000125 RID: 293
			private readonly Dictionary<VisualElement, DataSourceContext> m_ResolvedHierarchicalDataSourceContext;

			// Token: 0x04000126 RID: 294
			private readonly Dictionary<Binding, int> m_BindingRefCount;

			// Token: 0x04000127 RID: 295
			private readonly Dictionary<object, DataBindingManager.HierarchyDataSourceTracker.SourceInfo> m_SourceInfos;

			// Token: 0x04000128 RID: 296
			private readonly HashSet<object> m_SourcesToRemove;

			// Token: 0x04000129 RID: 297
			private readonly DataBindingManager.HierarchyDataSourceTracker.InvalidateDataSourcesTraversal m_InvalidateResolvedDataSources;

			// Token: 0x0400012A RID: 298
			private readonly EventHandler<BindablePropertyChangedEventArgs> m_Handler;

			// Token: 0x0400012B RID: 299
			private readonly EventCallback<PropertyChangedEvent, VisualElement> m_VisualElementHandler;

			// Token: 0x02000037 RID: 55
			private class SourceInfo
			{
				// Token: 0x17000038 RID: 56
				// (get) Token: 0x060001DA RID: 474 RVA: 0x000093DC File Offset: 0x000075DC
				// (set) Token: 0x060001DB RID: 475 RVA: 0x000093E4 File Offset: 0x000075E4
				public long lastVersion { get; set; }

				// Token: 0x17000039 RID: 57
				// (get) Token: 0x060001DC RID: 476 RVA: 0x000093ED File Offset: 0x000075ED
				// (set) Token: 0x060001DD RID: 477 RVA: 0x000093F5 File Offset: 0x000075F5
				public int refCount { get; set; }

				// Token: 0x1700003A RID: 58
				// (get) Token: 0x060001DE RID: 478 RVA: 0x00009400 File Offset: 0x00007600
				public List<PropertyPath> detectedChanges
				{
					get
					{
						List<PropertyPath> list;
						if ((list = this.m_DetectedChanges) == null)
						{
							list = (this.m_DetectedChanges = new List<PropertyPath>());
						}
						return list;
					}
				}

				// Token: 0x1700003B RID: 59
				// (get) Token: 0x060001DF RID: 479 RVA: 0x00009425 File Offset: 0x00007625
				public List<PropertyPath> detectedChangesNoAlloc
				{
					get
					{
						return this.m_DetectedChanges;
					}
				}

				// Token: 0x0400012C RID: 300
				private List<PropertyPath> m_DetectedChanges;
			}

			// Token: 0x02000038 RID: 56
			private class InvalidateDataSourcesTraversal : HierarchyTraversal
			{
				// Token: 0x060001E1 RID: 481 RVA: 0x0000942D File Offset: 0x0000762D
				public InvalidateDataSourcesTraversal(DataBindingManager.HierarchyDataSourceTracker dataSourceTracker)
				{
					this.m_DataSourceTracker = dataSourceTracker;
					this.m_VisitedElements = new HashSet<VisualElement>();
				}

				// Token: 0x060001E2 RID: 482 RVA: 0x0000944C File Offset: 0x0000764C
				public void Invalidate(List<VisualElement> addedOrMovedElements, HashSet<VisualElement> removedElements)
				{
					this.m_VisitedElements.Clear();
					for (int i = 0; i < addedOrMovedElements.Count; i++)
					{
						VisualElement element = addedOrMovedElements[i];
						this.Traverse(element);
					}
					foreach (VisualElement element2 in removedElements)
					{
						bool flag = this.m_VisitedElements.Contains(element2);
						if (!flag)
						{
							this.Traverse(element2);
						}
					}
				}

				// Token: 0x060001E3 RID: 483 RVA: 0x000094EC File Offset: 0x000076EC
				public override void TraverseRecursive(VisualElement element, int depth)
				{
					bool flag = this.m_VisitedElements.Contains(element);
					if (!flag)
					{
						bool flag2 = depth > 0 && element.dataSource != null;
						if (!flag2)
						{
							this.m_VisitedElements.Add(element);
							this.m_DataSourceTracker.RemoveHierarchyDataSourceContextFromElement(element);
							base.Recurse(element, depth);
						}
					}
				}

				// Token: 0x0400012F RID: 303
				private readonly DataBindingManager.HierarchyDataSourceTracker m_DataSourceTracker;

				// Token: 0x04000130 RID: 304
				private readonly HashSet<VisualElement> m_VisitedElements;
			}

			// Token: 0x02000039 RID: 57
			private class ObjectComparer : IEqualityComparer<object>
			{
				// Token: 0x060001E4 RID: 484 RVA: 0x00009548 File Offset: 0x00007748
				bool IEqualityComparer<object>.Equals(object x, object y)
				{
					return x == y || EqualityComparer<object>.Default.Equals(x, y);
				}

				// Token: 0x060001E5 RID: 485 RVA: 0x00009570 File Offset: 0x00007770
				int IEqualityComparer<object>.GetHashCode(object obj)
				{
					return RuntimeHelpers.GetHashCode(obj);
				}
			}
		}
	}
}
