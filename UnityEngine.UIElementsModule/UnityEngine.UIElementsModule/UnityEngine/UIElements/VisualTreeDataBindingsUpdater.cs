using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using Unity.Properties;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x02000047 RID: 71
	internal class VisualTreeDataBindingsUpdater : BaseVisualTreeHierarchyTrackerUpdater
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00009F0B File Offset: 0x0000810B
		private DataBindingManager bindingManager
		{
			get
			{
				return base.panel.dataBindingManager;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00009F18 File Offset: 0x00008118
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeDataBindingsUpdater.s_UpdateProfilerMarker;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009F20 File Offset: 0x00008120
		protected override void OnHierarchyChange(VisualElement ve, HierarchyChangeType type)
		{
			bool flag = this.bindingManager.GetBoundElementsCount() == 0 && this.bindingManager.GetTrackedDataSourcesCount() == 0;
			if (!flag)
			{
				if (type != HierarchyChangeType.Add)
				{
					if (type != HierarchyChangeType.Remove)
					{
						this.m_DataSourceChangedRequests.Add(ve);
					}
					else
					{
						this.m_DataSourceChangedRequests.Remove(ve);
						this.m_RemovedElements.Add(ve);
					}
				}
				else
				{
					this.m_RemovedElements.Remove(ve);
					this.m_DataSourceChangedRequests.Add(ve);
				}
				this.bindingManager.DirtyBindingOrder();
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009FB4 File Offset: 0x000081B4
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			base.OnVersionChanged(ve, versionChangeType);
			bool flag = (versionChangeType & VersionChangeType.BindingRegistration) == VersionChangeType.BindingRegistration;
			if (flag)
			{
				this.m_BindingRegistrationRequests.Add(ve);
			}
			bool flag2 = (versionChangeType & VersionChangeType.DataSource) == VersionChangeType.DataSource;
			if (flag2)
			{
				this.m_DataSourceChangedRequests.Add(ve);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A00C File Offset: 0x0000820C
		private void CacheAndLogBindingResult(bool appliedOnUiCache, in DataBindingManager.BindingData bindingData, in BindingResult result)
		{
			BindingLogLevel logLevel = this.bindingManager.logLevel;
			bool flag = logLevel == BindingLogLevel.None;
			if (!flag)
			{
				bool flag2 = logLevel == BindingLogLevel.Once;
				if (flag2)
				{
					BindingResult previousResult;
					if (appliedOnUiCache)
					{
						this.bindingManager.TryGetLastUIBindingResult(bindingData, out previousResult);
					}
					else
					{
						this.bindingManager.TryGetLastSourceBindingResult(bindingData, out previousResult);
					}
					bool flag3 = previousResult.status != result.status || previousResult.message != result.message;
					if (flag3)
					{
						this.LogResult(in result);
					}
				}
				else
				{
					this.LogResult(in result);
				}
			}
			if (appliedOnUiCache)
			{
				this.bindingManager.CacheUIBindingResult(bindingData, result);
			}
			else
			{
				this.bindingManager.CacheSourceBindingResult(bindingData, result);
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A0D8 File Offset: 0x000082D8
		private void LogResult(in BindingResult result)
		{
			bool flag = string.IsNullOrWhiteSpace(result.message);
			if (!flag)
			{
				Panel panel = base.panel as Panel;
				string panelName = ((panel != null) ? panel.name : null) ?? base.panel.visualTree.name;
				Debug.LogWarning(result.message + " (" + panelName + ")");
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A140 File Offset: 0x00008340
		public override void Update()
		{
			base.Update();
			this.ProcessAllBindingRequests();
			this.ProcessDataSourceChangedRequests();
			this.ProcessPropertyChangedEvents(this.m_RanUpdate);
			this.m_BoundsElement.AddRange(this.bindingManager.GetBoundElements());
			foreach (VisualElement element in this.m_BoundsElement)
			{
				List<DataBindingManager.BindingData> bindings = this.bindingManager.GetBindingData(element);
				int i = 0;
				while (i < bindings.Count)
				{
					DataBindingManager.BindingData bindingData = bindings[i];
					object source;
					PropertyPath resolvedDataSourcePath;
					using (VisualTreeDataBindingsUpdater.s_ShouldUpdateBindingProfilerMarker.Auto())
					{
						DataSourceContext resolvedContext = this.bindingManager.GetResolvedDataSourceContext(element, bindingData);
						source = resolvedContext.dataSource;
						resolvedDataSourcePath = resolvedContext.dataSourcePath;
						ValueTuple<bool, long> dataSourceVersion = this.GetDataSourceVersion(source);
						bool changed = dataSourceVersion.Item1;
						long version = dataSourceVersion.Item2;
						bool flag = bindingData.binding == null;
						if (flag)
						{
							goto IL_0323;
						}
						bool flag2 = source != null && this.m_TrackedObjects.Add(source);
						if (flag2)
						{
							this.m_VersionChanges.Add(new VisualTreeDataBindingsUpdater.VersionInfo(source, version));
						}
						bool isDirty = bindingData.binding.isDirty;
						if (isDirty)
						{
							this.m_DirtyBindings.Add(bindingData.binding);
						}
						bool flag3 = !this.m_Updater.ShouldProcessBindingAtStage(bindingData.binding, BindingUpdateStage.UpdateUI, changed, this.m_DirtyBindings.Contains(bindingData.binding));
						if (flag3)
						{
							goto IL_0323;
						}
						bool flag4 = bindingData.binding.updateTrigger == BindingUpdateTrigger.OnSourceChanged && source is INotifyBindablePropertyChanged && !bindingData.binding.isDirty;
						if (flag4)
						{
							List<PropertyPath> changedPaths = this.bindingManager.GetChangedDetectedFromSource(source);
							bool flag5 = changedPaths == null || changedPaths.Count == 0;
							if (flag5)
							{
								goto IL_0323;
							}
							bool processBinding = false;
							foreach (PropertyPath path in changedPaths)
							{
								bool flag6 = this.IsPrefix(in path, in resolvedDataSourcePath);
								if (flag6)
								{
									processBinding = true;
									break;
								}
							}
							bool flag7 = !processBinding;
							if (flag7)
							{
								goto IL_0323;
							}
						}
					}
					goto IL_0227;
					IL_0323:
					i++;
					continue;
					IL_0227:
					bool flag8 = source != null;
					if (flag8)
					{
						this.m_KnownSources.Add(source);
					}
					bool wasDirty = bindingData.binding.isDirty;
					bindingData.binding.ClearDirty();
					BindingContext context = new BindingContext(element, in bindingData.target.bindingId, in resolvedDataSourcePath, source);
					BindingResult result = default(BindingResult);
					long bindingVersion = bindingData.version;
					using (VisualTreeDataBindingsUpdater.s_UpdateBindingProfilerMarker.Auto())
					{
						result = this.m_Updater.UpdateUI(in context, bindingData.binding);
					}
					this.CacheAndLogBindingResult(true, in bindingData, in result);
					bool flag9 = bindingData.version == bindingVersion;
					if (flag9)
					{
						BindingStatus status = result.status;
						BindingStatus bindingStatus = status;
						if (bindingStatus != BindingStatus.Success)
						{
							if (bindingStatus == BindingStatus.Pending)
							{
								if (wasDirty)
								{
									bindingData.binding.MarkDirty();
								}
							}
						}
						else
						{
							this.m_RanUpdate.Add(bindingData.binding);
						}
					}
					goto IL_0323;
				}
			}
			foreach (VisualTreeDataBindingsUpdater.VersionInfo versionInfo in this.m_VersionChanges)
			{
				this.bindingManager.UpdateVersion(versionInfo.source, versionInfo.version);
			}
			this.ProcessPropertyChangedEvents(this.m_RanUpdate);
			foreach (object touchedSource in this.m_KnownSources)
			{
				this.bindingManager.ClearChangesFromSource(touchedSource);
			}
			this.m_BoundsElement.Clear();
			this.m_VersionChanges.Clear();
			this.m_TrackedObjects.Clear();
			this.m_RanUpdate.Clear();
			this.m_KnownSources.Clear();
			this.m_DirtyBindings.Clear();
			this.bindingManager.ClearSourceCache();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A62C File Offset: 0x0000882C
		[return: TupleElementNames(new string[] { "changed", "version" })]
		private ValueTuple<bool, long> GetDataSourceVersion(object source)
		{
			long version;
			bool flag = this.bindingManager.TryGetLastVersion(source, out version);
			ValueTuple<bool, long> valueTuple;
			if (flag)
			{
				IDataSourceViewHashProvider versioned = source as IDataSourceViewHashProvider;
				bool flag2 = versioned == null;
				if (flag2)
				{
					valueTuple = new ValueTuple<bool, long>(source != null, version + 1L);
				}
				else
				{
					long currentVersion = versioned.GetViewHashCode();
					valueTuple = ((currentVersion == version) ? new ValueTuple<bool, long>(false, version) : new ValueTuple<bool, long>(true, currentVersion));
				}
			}
			else
			{
				IDataSourceViewHashProvider versioned2 = source as IDataSourceViewHashProvider;
				bool flag3 = versioned2 != null;
				if (flag3)
				{
					valueTuple = new ValueTuple<bool, long>(true, versioned2.GetViewHashCode());
				}
				else
				{
					valueTuple = new ValueTuple<bool, long>(source != null, 0L);
				}
			}
			return valueTuple;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A6C8 File Offset: 0x000088C8
		private bool IsPrefix(in PropertyPath prefix, in PropertyPath path)
		{
			bool flag = path.Length < prefix.Length;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < prefix.Length; i++)
				{
					PropertyPathPart prefixPart = prefix[i];
					PropertyPathPart part = path[i];
					bool flag3 = prefixPart.Kind != part.Kind;
					if (flag3)
					{
						return false;
					}
					switch (prefixPart.Kind)
					{
					case PropertyPathPartKind.Name:
					{
						bool flag4 = prefixPart.Name != part.Name;
						if (flag4)
						{
							return false;
						}
						break;
					}
					case PropertyPathPartKind.Index:
					{
						bool flag5 = prefixPart.Index != part.Index;
						if (flag5)
						{
							return false;
						}
						break;
					}
					case PropertyPathPartKind.Key:
					{
						bool flag6 = prefixPart.Key != part.Key;
						if (flag6)
						{
							return false;
						}
						break;
					}
					default:
						throw new ArgumentOutOfRangeException();
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A7CC File Offset: 0x000089CC
		private void ProcessDataSourceChangedRequests()
		{
			using (VisualTreeDataBindingsUpdater.s_ProcessDataSourcesProfilerMarker.Auto())
			{
				bool flag = this.m_DataSourceChangedRequests.Count == 0 && this.m_RemovedElements.Count == 0;
				if (!flag)
				{
					this.m_DataSourceChangedRequests.RemoveWhere((VisualElement e) => e.panel == null);
					this.bindingManager.InvalidateCachedDataSource(this.m_DataSourceChangedRequests, this.m_RemovedElements);
					this.m_DataSourceChangedRequests.Clear();
					this.m_RemovedElements.Clear();
				}
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A888 File Offset: 0x00008A88
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.bindingManager.Dispose();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A8A0 File Offset: 0x00008AA0
		private void ProcessAllBindingRequests()
		{
			using (VisualTreeDataBindingsUpdater.s_ProcessBindingRequestsProfilerMarker.Auto())
			{
				for (int i = 0; i < this.m_BindingRegistrationRequests.Count; i++)
				{
					VisualElement element = this.m_BindingRegistrationRequests[i];
					bool flag = element.panel == null;
					if (!flag)
					{
						Assert.IsTrue(element.panel == base.panel);
						this.ProcessBindingRequests(element);
					}
				}
				this.m_BindingRegistrationRequests.Clear();
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A944 File Offset: 0x00008B44
		private void ProcessBindingRequests(VisualElement element)
		{
			this.bindingManager.ProcessBindingRequests(element);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A954 File Offset: 0x00008B54
		private void ProcessPropertyChangedEvents(HashSet<Binding> ranUpdate)
		{
			List<DataBindingManager.ChangesFromUI> data = this.bindingManager.GetChangedDetectedFromUI();
			for (int index = 0; index < data.Count; index++)
			{
				DataBindingManager.ChangesFromUI change = data[index];
				bool flag = !change.IsValid;
				if (!flag)
				{
					DataBindingManager.BindingData bindingData = change.bindingData;
					Binding binding = bindingData.binding;
					VisualElement element = bindingData.target.element;
					bool flag2 = !this.m_Updater.ShouldProcessBindingAtStage(binding, BindingUpdateStage.UpdateSource, true, false);
					if (!flag2)
					{
						bool flag3 = ranUpdate.Contains(binding);
						if (!flag3)
						{
							DataSourceContext resolvedContext = this.bindingManager.GetResolvedDataSourceContext(bindingData.target.element, bindingData);
							object source = resolvedContext.dataSource;
							PropertyPath resolvedSourcePath = resolvedContext.dataSourcePath;
							BindingContext toDataSourceContext = new BindingContext(element, in bindingData.target.bindingId, in resolvedSourcePath, source);
							BindingResult result = this.m_Updater.UpdateSource(in toDataSourceContext, binding);
							this.CacheAndLogBindingResult(false, in bindingData, in result);
							bool flag4 = result.status == BindingStatus.Success;
							if (flag4)
							{
								bool flag5 = !change.IsValid;
								if (!flag5)
								{
									bool wasDirty = bindingData.binding.isDirty;
									bindingData.binding.ClearDirty();
									BindingContext context = new BindingContext(element, in bindingData.target.bindingId, in resolvedSourcePath, source);
									result = this.m_Updater.UpdateUI(in context, binding);
									this.CacheAndLogBindingResult(true, in bindingData, in result);
									bool flag6 = result.status == BindingStatus.Pending;
									if (flag6)
									{
										bool flag7 = wasDirty;
										if (flag7)
										{
											bindingData.binding.MarkDirty();
										}
										else
										{
											bindingData.binding.ClearDirty();
										}
									}
								}
							}
						}
					}
				}
			}
			data.Clear();
		}

		// Token: 0x04000153 RID: 339
		private static readonly ProfilerMarker s_UpdateProfilerMarker = new ProfilerMarker("Update Runtime Bindings");

		// Token: 0x04000154 RID: 340
		private static readonly ProfilerMarker s_ProcessBindingRequestsProfilerMarker = new ProfilerMarker("Process Binding Requests");

		// Token: 0x04000155 RID: 341
		private static readonly ProfilerMarker s_ProcessDataSourcesProfilerMarker = new ProfilerMarker("Process Data Sources");

		// Token: 0x04000156 RID: 342
		private static readonly ProfilerMarker s_ShouldUpdateBindingProfilerMarker = new ProfilerMarker("Should Update Binding");

		// Token: 0x04000157 RID: 343
		private static readonly ProfilerMarker s_UpdateBindingProfilerMarker = new ProfilerMarker("Update Binding");

		// Token: 0x04000158 RID: 344
		private readonly BindingUpdater m_Updater = new BindingUpdater();

		// Token: 0x04000159 RID: 345
		private readonly List<VisualElement> m_BindingRegistrationRequests = new List<VisualElement>();

		// Token: 0x0400015A RID: 346
		private readonly HashSet<VisualElement> m_DataSourceChangedRequests = new HashSet<VisualElement>();

		// Token: 0x0400015B RID: 347
		private readonly HashSet<VisualElement> m_RemovedElements = new HashSet<VisualElement>();

		// Token: 0x0400015C RID: 348
		private readonly List<VisualElement> m_BoundsElement = new List<VisualElement>();

		// Token: 0x0400015D RID: 349
		private readonly List<VisualTreeDataBindingsUpdater.VersionInfo> m_VersionChanges = new List<VisualTreeDataBindingsUpdater.VersionInfo>();

		// Token: 0x0400015E RID: 350
		private readonly HashSet<object> m_TrackedObjects = new HashSet<object>();

		// Token: 0x0400015F RID: 351
		private readonly HashSet<Binding> m_RanUpdate = new HashSet<Binding>();

		// Token: 0x04000160 RID: 352
		private readonly HashSet<object> m_KnownSources = new HashSet<object>();

		// Token: 0x04000161 RID: 353
		private readonly HashSet<Binding> m_DirtyBindings = new HashSet<Binding>();

		// Token: 0x02000048 RID: 72
		private readonly struct VersionInfo
		{
			// Token: 0x06000222 RID: 546 RVA: 0x0000ABE8 File Offset: 0x00008DE8
			public VersionInfo(object source, long version)
			{
				this.source = source;
				this.version = version;
			}

			// Token: 0x04000162 RID: 354
			public readonly object source;

			// Token: 0x04000163 RID: 355
			public readonly long version;
		}
	}
}
