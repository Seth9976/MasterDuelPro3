using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Exceptions;
using UnityEngine.ResourceManagement.Profiling;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.SceneManagement;

namespace UnityEngine.ResourceManagement
{
	// Token: 0x0200000B RID: 11
	public class ResourceManager : IDisposable
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002CA1 File Offset: 0x00000EA1
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public static Action<AsyncOperationHandle, Exception> ExceptionHandler { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002CB0 File Offset: 0x00000EB0
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public Func<IResourceLocation, string> InternalIdTransformFunc { get; set; }

		// Token: 0x06000046 RID: 70 RVA: 0x00002CC1 File Offset: 0x00000EC1
		public string TransformInternalId(IResourceLocation location)
		{
			if (this.InternalIdTransformFunc != null)
			{
				return this.InternalIdTransformFunc(location);
			}
			return location.InternalId;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002CDE File Offset: 0x00000EDE
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002CE6 File Offset: 0x00000EE6
		public Action<UnityWebRequest> WebRequestOverride { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002CEF File Offset: 0x00000EEF
		internal int OperationCacheCount
		{
			get
			{
				return this.m_AssetOperationCache.Count;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002CFC File Offset: 0x00000EFC
		internal int InstanceOperationCount
		{
			get
			{
				return this.m_TrackedInstanceOperations.Count;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002D09 File Offset: 0x00000F09
		internal int DeferredCompleteCallbacksCount
		{
			get
			{
				return this.m_DeferredCompleteCallbacks.Count;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D16 File Offset: 0x00000F16
		internal int DeferredCallbackCount
		{
			get
			{
				List<ResourceManager.DeferredCallbackRegisterRequest> deferredCallbacksToRegister = this.m_DeferredCallbacksToRegister;
				if (deferredCallbacksToRegister == null)
				{
					return 0;
				}
				return deferredCallbacksToRegister.Count;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D29 File Offset: 0x00000F29
		public void AddUpdateReceiver(IUpdateReceiver receiver)
		{
			if (receiver == null)
			{
				return;
			}
			this.m_UpdateReceivers.Add(receiver);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D3B File Offset: 0x00000F3B
		public void RemoveUpdateReciever(IUpdateReceiver receiver)
		{
			if (receiver == null)
			{
				return;
			}
			if (this.m_UpdatingReceivers)
			{
				if (this.m_UpdateReceiversToRemove == null)
				{
					this.m_UpdateReceiversToRemove = new List<IUpdateReceiver>();
				}
				this.m_UpdateReceiversToRemove.Add(receiver);
				return;
			}
			this.m_UpdateReceivers.Remove(receiver);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002D76 File Offset: 0x00000F76
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002D7E File Offset: 0x00000F7E
		public IAllocationStrategy Allocator
		{
			get
			{
				return this.m_allocator;
			}
			set
			{
				this.m_allocator = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002D87 File Offset: 0x00000F87
		public IList<IResourceProvider> ResourceProviders
		{
			get
			{
				return this.m_ResourceProviders;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D8F File Offset: 0x00000F8F
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002D97 File Offset: 0x00000F97
		public CertificateHandler CertificateHandlerInstance { get; set; }

		// Token: 0x06000054 RID: 84 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public ResourceManager(IAllocationStrategy alloc = null)
		{
			this.m_ReleaseOpNonCached = new Action<IAsyncOperation>(this.OnOperationDestroyNonCached);
			this.m_ReleaseOpCached = new Action<IAsyncOperation>(this.OnOperationDestroyCached);
			this.m_ReleaseInstanceOp = new Action<IAsyncOperation>(this.OnInstanceOperationDestroy);
			IAllocationStrategy allocationStrategy;
			if (alloc != null)
			{
				allocationStrategy = alloc;
			}
			else
			{
				IAllocationStrategy allocationStrategy2 = new LRUCacheAllocationStrategy(1000, 1000, 100, 10);
				allocationStrategy = allocationStrategy2;
			}
			this.m_allocator = allocationStrategy;
			this.m_ResourceProviders.OnElementAdded += new Action<IResourceProvider>(this.OnObjectAdded);
			this.m_ResourceProviders.OnElementRemoved += new Action<IResourceProvider>(this.OnObjectRemoved);
			this.m_UpdateReceivers.OnElementAdded += delegate(IUpdateReceiver x)
			{
				this.RegisterForCallbacks();
			};
			ProfilerRuntime.Initialise();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002EB4 File Offset: 0x000010B4
		private void OnObjectAdded(object obj)
		{
			IUpdateReceiver updateReceiver = obj as IUpdateReceiver;
			if (updateReceiver != null)
			{
				this.AddUpdateReceiver(updateReceiver);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002ED4 File Offset: 0x000010D4
		private void OnObjectRemoved(object obj)
		{
			IUpdateReceiver updateReceiver = obj as IUpdateReceiver;
			if (updateReceiver != null)
			{
				this.RemoveUpdateReciever(updateReceiver);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EF2 File Offset: 0x000010F2
		internal void RegisterForCallbacks()
		{
			if (this.CallbackHooksEnabled && !this.m_RegisteredForCallbacks)
			{
				this.m_RegisteredForCallbacks = true;
				ComponentSingleton<MonoBehaviourCallbackHooks>.Instance.OnUpdateDelegate += this.Update;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F24 File Offset: 0x00001124
		public IResourceProvider GetResourceProvider(Type t, IResourceLocation location)
		{
			if (location != null)
			{
				IResourceProvider prov = null;
				int hash = location.ProviderId.GetHashCode() * 31 + ((t == null) ? 0 : t.GetHashCode());
				if (!this.m_providerMap.TryGetValue(hash, out prov))
				{
					for (int i = 0; i < this.ResourceProviders.Count; i++)
					{
						IResourceProvider p = this.ResourceProviders[i];
						if (p.ProviderId.Equals(location.ProviderId, StringComparison.Ordinal) && (t == null || p.CanProvide(t, location)))
						{
							this.m_providerMap.Add(hash, prov = p);
							break;
						}
					}
				}
				return prov;
			}
			return null;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FCC File Offset: 0x000011CC
		private Type GetDefaultTypeForLocation(IResourceLocation loc)
		{
			IResourceProvider provider = this.GetResourceProvider(null, loc);
			if (provider == null)
			{
				return typeof(object);
			}
			Type t = provider.GetDefaultType(loc);
			if (!(t != null))
			{
				return typeof(object);
			}
			return t;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003010 File Offset: 0x00001210
		private int CalculateLocationsHash(IList<IResourceLocation> locations, Type t = null)
		{
			if (locations == null || locations.Count == 0)
			{
				return 0;
			}
			int hash = 17;
			foreach (IResourceLocation loc in locations)
			{
				Type t2 = ((t != null) ? t : this.GetDefaultTypeForLocation(loc));
				hash = hash * 31 + loc.Hash(t2);
			}
			return hash;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003084 File Offset: 0x00001284
		private AsyncOperationHandle ProvideResource(IResourceLocation location, Type desiredType = null, bool releaseDependenciesOnFailure = true)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			IResourceProvider provider = null;
			if (desiredType == null)
			{
				provider = this.GetResourceProvider(desiredType, location);
				if (provider == null)
				{
					UnknownResourceProviderException ex = new UnknownResourceProviderException(location);
					return this.CreateCompletedOperationInternal<object>(null, false, ex, releaseDependenciesOnFailure);
				}
				desiredType = provider.GetDefaultType(location);
			}
			if (provider == null)
			{
				provider = this.GetResourceProvider(desiredType, location);
			}
			IOperationCacheKey key = this.CreateCacheKeyForLocation(provider, location, desiredType);
			IAsyncOperation op;
			if (this.m_AssetOperationCache.TryGetValue(key, out op))
			{
				op.IncrementReferenceCount();
				return new AsyncOperationHandle(op, location.ToString());
			}
			Type provType;
			if (!this.m_ProviderOperationTypeCache.TryGetValue(desiredType, out provType))
			{
				this.m_ProviderOperationTypeCache.Add(desiredType, provType = typeof(ProviderOperation<>).MakeGenericType(new Type[] { desiredType }));
			}
			op = this.CreateOperation<IAsyncOperation>(provType, provType.GetHashCode(), key, this.m_ReleaseOpCached);
			int depHash = location.DependencyHashCode;
			AsyncOperationHandle<IList<AsyncOperationHandle>> depOp = (location.HasDependencies ? this.ProvideResourceGroupCached(location.Dependencies, depHash, null, null, releaseDependenciesOnFailure) : default(AsyncOperationHandle<IList<AsyncOperationHandle>>));
			((IGenericProviderOperation)op).Init(this, provider, location, depOp, releaseDependenciesOnFailure);
			AsyncOperationHandle handle = this.StartOperation(op, depOp);
			handle.LocationName = location.ToString();
			if (depOp.IsValid())
			{
				depOp.Release();
			}
			return handle;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000031CC File Offset: 0x000013CC
		internal IAsyncOperation GetOperationFromCache(IResourceLocation location, Type desiredType)
		{
			IResourceProvider provider = this.GetResourceProvider(desiredType, location);
			IOperationCacheKey key = this.CreateCacheKeyForLocation(provider, location, desiredType);
			IAsyncOperation op;
			if (this.m_AssetOperationCache.TryGetValue(key, out op))
			{
				return op;
			}
			return null;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003200 File Offset: 0x00001400
		internal IOperationCacheKey CreateCacheKeyForLocation(IResourceProvider provider, IResourceLocation location, Type desiredType = null)
		{
			AssetBundleProvider abProvider = provider as AssetBundleProvider;
			if (abProvider != null)
			{
				return abProvider.CreateCacheKeyForLocation(this, location, desiredType);
			}
			return new LocationCacheKey(location, desiredType);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003228 File Offset: 0x00001428
		public AsyncOperationHandle<TObject> ProvideResource<TObject>(IResourceLocation location)
		{
			return this.ProvideResource(location, typeof(TObject), true).Convert<TObject>();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000324F File Offset: 0x0000144F
		public AsyncOperationHandle<TObject> StartOperation<TObject>(AsyncOperationBase<TObject> operation, AsyncOperationHandle dependency)
		{
			operation.Start(this, dependency, this.m_UpdateCallbacks);
			return operation.Handle;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003265 File Offset: 0x00001465
		internal AsyncOperationHandle StartOperation(IAsyncOperation operation, AsyncOperationHandle dependency)
		{
			operation.Start(this, dependency, this.m_UpdateCallbacks);
			return operation.Handle;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000327B File Offset: 0x0000147B
		private void OnInstanceOperationDestroy(IAsyncOperation o)
		{
			this.m_TrackedInstanceOperations.Remove(o as ResourceManager.InstanceOperation);
			this.Allocator.Release(o.GetType().GetHashCode(), o);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032A6 File Offset: 0x000014A6
		private void OnOperationDestroyNonCached(IAsyncOperation o)
		{
			this.Allocator.Release(o.GetType().GetHashCode(), o);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032C0 File Offset: 0x000014C0
		private void OnOperationDestroyCached(IAsyncOperation o)
		{
			this.Allocator.Release(o.GetType().GetHashCode(), o);
			ICachable cachable = o as ICachable;
			if (((cachable != null) ? cachable.Key : null) != null)
			{
				this.RemoveOperationFromCache(cachable.Key);
				cachable.Key = null;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003310 File Offset: 0x00001510
		internal T CreateOperation<T>(Type actualType, int typeHash, IOperationCacheKey cacheKey, Action<IAsyncOperation> onDestroyAction) where T : IAsyncOperation
		{
			if (cacheKey == null)
			{
				T op = (T)((object)this.Allocator.New(actualType, typeHash));
				op.OnDestroy = onDestroyAction;
				return op;
			}
			T op2 = (T)((object)this.Allocator.New(actualType, typeHash));
			op2.OnDestroy = onDestroyAction;
			ICachable cachable = op2 as ICachable;
			if (cachable != null)
			{
				cachable.Key = cacheKey;
				this.AddOperationToCache(cacheKey, op2);
			}
			return op2;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000338A File Offset: 0x0000158A
		internal void AddOperationToCache(IOperationCacheKey key, IAsyncOperation operation)
		{
			if (!this.IsOperationCached(key))
			{
				this.m_AssetOperationCache.Add(key, operation);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000033A2 File Offset: 0x000015A2
		internal bool RemoveOperationFromCache(IOperationCacheKey key)
		{
			return !this.IsOperationCached(key) || this.m_AssetOperationCache.Remove(key);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000033BB File Offset: 0x000015BB
		internal bool IsOperationCached(IOperationCacheKey key)
		{
			return this.m_AssetOperationCache.ContainsKey(key);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002CEF File Offset: 0x00000EEF
		internal int CachedOperationCount()
		{
			return this.m_AssetOperationCache.Count;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033C9 File Offset: 0x000015C9
		internal void ClearOperationCache()
		{
			this.m_AssetOperationCache.Clear();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000033D8 File Offset: 0x000015D8
		public AsyncOperationHandle<TObject> CreateCompletedOperation<TObject>(TObject result, string errorMsg)
		{
			bool success = string.IsNullOrEmpty(errorMsg);
			return this.CreateCompletedOperationInternal<TObject>(result, success, (!success) ? new Exception(errorMsg) : null, true);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003401 File Offset: 0x00001601
		public AsyncOperationHandle<TObject> CreateCompletedOperationWithException<TObject>(TObject result, Exception exception)
		{
			return this.CreateCompletedOperationInternal<TObject>(result, exception == null, exception, true);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003410 File Offset: 0x00001610
		internal AsyncOperationHandle<TObject> CreateCompletedOperationInternal<TObject>(TObject result, bool success, Exception exception, bool releaseDependenciesOnFailure = true)
		{
			ResourceManager.CompletedOperation<TObject> cop = this.CreateOperation<ResourceManager.CompletedOperation<TObject>>(typeof(ResourceManager.CompletedOperation<TObject>), typeof(ResourceManager.CompletedOperation<TObject>).GetHashCode(), null, this.m_ReleaseOpNonCached);
			cop.Init(result, success, exception, releaseDependenciesOnFailure);
			return this.StartOperation<TObject>(cop, default(AsyncOperationHandle));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000345F File Offset: 0x0000165F
		public void Release(AsyncOperationHandle handle)
		{
			handle.Release();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003468 File Offset: 0x00001668
		public AsyncOperationHandle<TObject> Acquire<TObject>(AsyncOperationHandle<TObject> handle)
		{
			return handle.Acquire();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003471 File Offset: 0x00001671
		public void Acquire(AsyncOperationHandle handle)
		{
			handle.Acquire();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000347C File Offset: 0x0000167C
		private GroupOperation AcquireGroupOpFromCache(IOperationCacheKey key)
		{
			IAsyncOperation opGeneric;
			if (this.m_AssetOperationCache.TryGetValue(key, out opGeneric))
			{
				opGeneric.IncrementReferenceCount();
				return (GroupOperation)opGeneric;
			}
			return null;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000034A8 File Offset: 0x000016A8
		public AsyncOperationHandle<IList<AsyncOperationHandle>> CreateGroupOperation<T>(IList<IResourceLocation> locations)
		{
			GroupOperation op = this.CreateOperation<GroupOperation>(typeof(GroupOperation), ResourceManager.s_GroupOperationTypeHash, null, this.m_ReleaseOpNonCached);
			List<AsyncOperationHandle> ops = new List<AsyncOperationHandle>(locations.Count);
			foreach (IResourceLocation loc in locations)
			{
				ops.Add(this.ProvideResource<T>(loc));
			}
			op.Init(ops, true, false);
			return this.StartOperation<IList<AsyncOperationHandle>>(op, default(AsyncOperationHandle));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003540 File Offset: 0x00001740
		internal AsyncOperationHandle<IList<AsyncOperationHandle>> CreateGroupOperation<T>(IList<IResourceLocation> locations, bool allowFailedDependencies)
		{
			GroupOperation op = this.CreateOperation<GroupOperation>(typeof(GroupOperation), ResourceManager.s_GroupOperationTypeHash, null, this.m_ReleaseOpNonCached);
			List<AsyncOperationHandle> ops = new List<AsyncOperationHandle>(locations.Count);
			foreach (IResourceLocation loc in locations)
			{
				ops.Add(this.ProvideResource<T>(loc));
			}
			GroupOperation.GroupOperationSettings settings = GroupOperation.GroupOperationSettings.None;
			if (allowFailedDependencies)
			{
				settings |= GroupOperation.GroupOperationSettings.AllowFailedDependencies;
			}
			op.Init(ops, settings);
			return this.StartOperation<IList<AsyncOperationHandle>>(op, default(AsyncOperationHandle));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000035E0 File Offset: 0x000017E0
		public AsyncOperationHandle<IList<AsyncOperationHandle>> CreateGenericGroupOperation(List<AsyncOperationHandle> operations, bool releasedCachedOpOnComplete = false)
		{
			GroupOperation op = this.CreateOperation<GroupOperation>(typeof(GroupOperation), ResourceManager.s_GroupOperationTypeHash, new AsyncOpHandlesCacheKey(operations), releasedCachedOpOnComplete ? this.m_ReleaseOpCached : this.m_ReleaseOpNonCached);
			op.Init(operations, true, false);
			return this.StartOperation<IList<AsyncOperationHandle>>(op, default(AsyncOperationHandle));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003634 File Offset: 0x00001834
		internal AsyncOperationHandle<IList<AsyncOperationHandle>> ProvideResourceGroupCached(IList<IResourceLocation> locations, int groupHash, Type desiredType, Action<AsyncOperationHandle> callback, bool releaseDependenciesOnFailure = true)
		{
			DependenciesCacheKey depsKey = new DependenciesCacheKey(locations, groupHash);
			GroupOperation op = this.AcquireGroupOpFromCache(depsKey);
			AsyncOperationHandle<IList<AsyncOperationHandle>> handle;
			if (op == null)
			{
				op = this.CreateOperation<GroupOperation>(typeof(GroupOperation), ResourceManager.s_GroupOperationTypeHash, depsKey, this.m_ReleaseOpCached);
				List<AsyncOperationHandle> ops = new List<AsyncOperationHandle>(locations.Count);
				foreach (IResourceLocation loc in locations)
				{
					ops.Add(this.ProvideResource(loc, desiredType, releaseDependenciesOnFailure));
				}
				op.Init(ops, releaseDependenciesOnFailure, false);
				handle = this.StartOperation<IList<AsyncOperationHandle>>(op, default(AsyncOperationHandle));
			}
			else
			{
				handle = op.Handle;
			}
			if (callback != null)
			{
				IList<AsyncOperationHandle> depOps = op.GetDependentOps();
				for (int i = 0; i < depOps.Count; i++)
				{
					depOps[i].Completed += callback;
				}
			}
			return handle;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003728 File Offset: 0x00001928
		public AsyncOperationHandle<IList<TObject>> ProvideResources<TObject>(IList<IResourceLocation> locations, Action<TObject> callback = null)
		{
			return this.ProvideResources<TObject>(locations, true, callback);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003734 File Offset: 0x00001934
		public AsyncOperationHandle<IList<TObject>> ProvideResources<TObject>(IList<IResourceLocation> locations, bool releaseDependenciesOnFailure, Action<TObject> callback = null)
		{
			if (locations == null)
			{
				return this.CreateCompletedOperation<IList<TObject>>(null, "Null Location");
			}
			Action<AsyncOperationHandle> callbackGeneric = null;
			if (callback != null)
			{
				callbackGeneric = delegate(AsyncOperationHandle x)
				{
					callback((TObject)((object)x.Result));
				};
			}
			AsyncOperationHandle<IList<AsyncOperationHandle>> typelessHandle = this.ProvideResourceGroupCached(locations, this.CalculateLocationsHash(locations, typeof(TObject)), typeof(TObject), callbackGeneric, releaseDependenciesOnFailure);
			AsyncOperationHandle<IList<TObject>> asyncOperationHandle = this.CreateChainOperation<IList<TObject>>(typelessHandle, delegate(AsyncOperationHandle resultHandle)
			{
				AsyncOperationHandle<IList<AsyncOperationHandle>> handleToHandles = resultHandle.Convert<IList<AsyncOperationHandle>>();
				List<TObject> list = new List<TObject>();
				Exception exception = null;
				if (handleToHandles.Status == AsyncOperationStatus.Succeeded)
				{
					using (IEnumerator<AsyncOperationHandle> enumerator = handleToHandles.Result.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							AsyncOperationHandle r = enumerator.Current;
							list.Add(r.Convert<TObject>().Result);
						}
						goto IL_00F5;
					}
				}
				bool foundSuccess = false;
				if (!releaseDependenciesOnFailure)
				{
					foreach (AsyncOperationHandle handle in handleToHandles.Result)
					{
						if (handle.Status == AsyncOperationStatus.Succeeded)
						{
							list.Add(handle.Convert<TObject>().Result);
							foundSuccess = true;
						}
						else
						{
							list.Add(default(TObject));
						}
					}
				}
				if (!foundSuccess)
				{
					list = null;
					exception = new ResourceManagerException("ProvideResources failed", handleToHandles.OperationException);
				}
				else
				{
					exception = new ResourceManagerException("Partial success in ProvideResources.  Some items failed to load. See earlier logs for more info.", handleToHandles.OperationException);
				}
				IL_00F5:
				return this.CreateCompletedOperationInternal<IList<TObject>>(list, exception == null, exception, releaseDependenciesOnFailure);
			}, releaseDependenciesOnFailure);
			typelessHandle.Release();
			return asyncOperationHandle;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000037D4 File Offset: 0x000019D4
		public AsyncOperationHandle<TObject> CreateChainOperation<TObject, TObjectDependency>(AsyncOperationHandle<TObjectDependency> dependentOp, Func<AsyncOperationHandle<TObjectDependency>, AsyncOperationHandle<TObject>> callback)
		{
			ChainOperation<TObject, TObjectDependency> op = this.CreateOperation<ChainOperation<TObject, TObjectDependency>>(typeof(ChainOperation<TObject, TObjectDependency>), typeof(ChainOperation<TObject, TObjectDependency>).GetHashCode(), null, null);
			op.Init(dependentOp, callback, true);
			return this.StartOperation<TObject>(op, dependentOp);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000381C File Offset: 0x00001A1C
		public AsyncOperationHandle<TObject> CreateChainOperation<TObject>(AsyncOperationHandle dependentOp, Func<AsyncOperationHandle, AsyncOperationHandle<TObject>> callback)
		{
			ChainOperationTypelessDepedency<TObject> cOp = new ChainOperationTypelessDepedency<TObject>();
			cOp.Init(dependentOp, callback, true);
			return this.StartOperation<TObject>(cOp, dependentOp);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003840 File Offset: 0x00001A40
		public AsyncOperationHandle<TObject> CreateChainOperation<TObject, TObjectDependency>(AsyncOperationHandle<TObjectDependency> dependentOp, Func<AsyncOperationHandle<TObjectDependency>, AsyncOperationHandle<TObject>> callback, bool releaseDependenciesOnFailure = true)
		{
			ChainOperation<TObject, TObjectDependency> op = this.CreateOperation<ChainOperation<TObject, TObjectDependency>>(typeof(ChainOperation<TObject, TObjectDependency>), typeof(ChainOperation<TObject, TObjectDependency>).GetHashCode(), null, null);
			op.Init(dependentOp, callback, releaseDependenciesOnFailure);
			return this.StartOperation<TObject>(op, dependentOp);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003888 File Offset: 0x00001A88
		public AsyncOperationHandle<TObject> CreateChainOperation<TObject>(AsyncOperationHandle dependentOp, Func<AsyncOperationHandle, AsyncOperationHandle<TObject>> callback, bool releaseDependenciesOnFailure = true)
		{
			ChainOperationTypelessDepedency<TObject> cOp = new ChainOperationTypelessDepedency<TObject>();
			cOp.Init(dependentOp, callback, releaseDependenciesOnFailure);
			return this.StartOperation<TObject>(cOp, dependentOp);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000038AC File Offset: 0x00001AAC
		public AsyncOperationHandle<SceneInstance> ProvideScene(ISceneProvider sceneProvider, IResourceLocation location, LoadSceneMode loadSceneMode, bool activateOnLoad, int priority)
		{
			if (sceneProvider == null)
			{
				throw new NullReferenceException("sceneProvider is null");
			}
			return sceneProvider.ProvideScene(this, location, new LoadSceneParameters(loadSceneMode), SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, activateOnLoad, priority);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000038CF File Offset: 0x00001ACF
		public AsyncOperationHandle<SceneInstance> ProvideScene(ISceneProvider sceneProvider, IResourceLocation location, LoadSceneParameters loadSceneParameters, bool activateOnLoad, int priority)
		{
			if (sceneProvider == null)
			{
				throw new NullReferenceException("sceneProvider is null");
			}
			return sceneProvider.ProvideScene(this, location, loadSceneParameters, SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, activateOnLoad, priority);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000038ED File Offset: 0x00001AED
		public AsyncOperationHandle<SceneInstance> ProvideScene(ISceneProvider sceneProvider, IResourceLocation location, LoadSceneParameters loadSceneParameters, SceneReleaseMode releaseMode, bool activateOnLoad, int priority)
		{
			if (sceneProvider == null)
			{
				throw new NullReferenceException("sceneProvider is null");
			}
			return sceneProvider.ProvideScene(this, location, loadSceneParameters, releaseMode, activateOnLoad, priority);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000390C File Offset: 0x00001B0C
		public AsyncOperationHandle<SceneInstance> ReleaseScene(ISceneProvider sceneProvider, AsyncOperationHandle<SceneInstance> sceneLoadHandle)
		{
			if (sceneProvider == null)
			{
				throw new NullReferenceException("sceneProvider is null");
			}
			return sceneProvider.ReleaseScene(this, sceneLoadHandle);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003924 File Offset: 0x00001B24
		public AsyncOperationHandle<GameObject> ProvideInstance(IInstanceProvider provider, IResourceLocation location, InstantiationParameters instantiateParameters)
		{
			if (provider == null)
			{
				throw new NullReferenceException("provider is null.  Assign a valid IInstanceProvider object before using.");
			}
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			AsyncOperationHandle<GameObject> depOp = this.ProvideResource<GameObject>(location);
			ResourceManager.InstanceOperation baseOp = this.CreateOperation<ResourceManager.InstanceOperation>(typeof(ResourceManager.InstanceOperation), ResourceManager.s_InstanceOperationTypeHash, null, this.m_ReleaseInstanceOp);
			baseOp.Init(this, provider, instantiateParameters, depOp);
			this.m_TrackedInstanceOperations.Add(baseOp);
			return this.StartOperation<GameObject>(baseOp, depOp);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003998 File Offset: 0x00001B98
		public void CleanupSceneInstances(Scene scene)
		{
			List<ResourceManager.InstanceOperation> handlesToRelease = null;
			foreach (ResourceManager.InstanceOperation h in this.m_TrackedInstanceOperations)
			{
				if (h.Result == null && scene == h.InstanceScene())
				{
					if (handlesToRelease == null)
					{
						handlesToRelease = new List<ResourceManager.InstanceOperation>();
					}
					handlesToRelease.Add(h);
				}
			}
			if (handlesToRelease != null)
			{
				foreach (ResourceManager.InstanceOperation h2 in handlesToRelease)
				{
					this.m_TrackedInstanceOperations.Remove(h2);
					h2.DecrementReferenceCount();
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003A64 File Offset: 0x00001C64
		private void ExecuteDeferredCallbacks()
		{
			this.m_InsideExecuteDeferredCallbacksMethod = true;
			for (int i = 0; i < this.m_DeferredCompleteCallbacks.Count; i++)
			{
				this.m_DeferredCompleteCallbacks[i].InvokeCompletionEvent();
				this.m_DeferredCompleteCallbacks[i].DecrementReferenceCount();
			}
			this.m_DeferredCompleteCallbacks.Clear();
			this.m_InsideExecuteDeferredCallbacksMethod = false;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003AC4 File Offset: 0x00001CC4
		internal void RegisterForDeferredCallback(IAsyncOperation op, bool incrementRefCount = true)
		{
			if (this.CallbackHooksEnabled && this.m_InsideExecuteDeferredCallbacksMethod)
			{
				if (this.m_DeferredCallbacksToRegister == null)
				{
					this.m_DeferredCallbacksToRegister = new List<ResourceManager.DeferredCallbackRegisterRequest>();
				}
				this.m_DeferredCallbacksToRegister.Add(new ResourceManager.DeferredCallbackRegisterRequest
				{
					operation = op,
					incrementRefCount = incrementRefCount
				});
				return;
			}
			if (incrementRefCount)
			{
				op.IncrementReferenceCount();
			}
			this.m_DeferredCompleteCallbacks.Add(op);
			this.RegisterForCallbacks();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003B34 File Offset: 0x00001D34
		internal void Update(float unscaledDeltaTime)
		{
			if (this.m_InsideUpdateMethod)
			{
				throw new Exception("Reentering the Update method is not allowed.  This can happen when calling WaitForCompletion on an operation while inside of a callback.");
			}
			this.m_InsideUpdateMethod = true;
			this.m_UpdateCallbacks.Invoke(unscaledDeltaTime);
			this.m_UpdatingReceivers = true;
			for (int i = 0; i < this.m_UpdateReceivers.Count; i++)
			{
				this.m_UpdateReceivers[i].Update(unscaledDeltaTime);
			}
			this.m_UpdatingReceivers = false;
			if (this.m_UpdateReceiversToRemove != null)
			{
				foreach (IUpdateReceiver r in this.m_UpdateReceiversToRemove)
				{
					this.m_UpdateReceivers.Remove(r);
				}
				this.m_UpdateReceiversToRemove = null;
			}
			if (this.m_DeferredCallbacksToRegister != null)
			{
				foreach (ResourceManager.DeferredCallbackRegisterRequest callback in this.m_DeferredCallbacksToRegister)
				{
					this.RegisterForDeferredCallback(callback.operation, callback.incrementRefCount);
				}
				this.m_DeferredCallbacksToRegister = null;
			}
			this.ExecuteDeferredCallbacks();
			this.m_InsideUpdateMethod = false;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003C64 File Offset: 0x00001E64
		public void Dispose()
		{
			if (ComponentSingleton<MonoBehaviourCallbackHooks>.Exists && this.m_RegisteredForCallbacks)
			{
				ComponentSingleton<MonoBehaviourCallbackHooks>.Instance.OnUpdateDelegate -= this.Update;
				this.m_RegisteredForCallbacks = false;
			}
		}

		// Token: 0x04000020 RID: 32
		internal bool CallbackHooksEnabled = true;

		// Token: 0x04000021 RID: 33
		private ListWithEvents<IResourceProvider> m_ResourceProviders = new ListWithEvents<IResourceProvider>();

		// Token: 0x04000022 RID: 34
		private IAllocationStrategy m_allocator;

		// Token: 0x04000023 RID: 35
		internal ListWithEvents<IUpdateReceiver> m_UpdateReceivers = new ListWithEvents<IUpdateReceiver>();

		// Token: 0x04000024 RID: 36
		private List<IUpdateReceiver> m_UpdateReceiversToRemove;

		// Token: 0x04000025 RID: 37
		private bool m_UpdatingReceivers;

		// Token: 0x04000026 RID: 38
		private bool m_InsideUpdateMethod;

		// Token: 0x04000027 RID: 39
		internal Dictionary<int, IResourceProvider> m_providerMap = new Dictionary<int, IResourceProvider>();

		// Token: 0x04000028 RID: 40
		private Dictionary<IOperationCacheKey, IAsyncOperation> m_AssetOperationCache = new Dictionary<IOperationCacheKey, IAsyncOperation>();

		// Token: 0x04000029 RID: 41
		private HashSet<ResourceManager.InstanceOperation> m_TrackedInstanceOperations = new HashSet<ResourceManager.InstanceOperation>();

		// Token: 0x0400002A RID: 42
		internal DelegateList<float> m_UpdateCallbacks = DelegateList<float>.CreateWithGlobalCache();

		// Token: 0x0400002B RID: 43
		private List<IAsyncOperation> m_DeferredCompleteCallbacks = new List<IAsyncOperation>();

		// Token: 0x0400002C RID: 44
		private bool m_InsideExecuteDeferredCallbacksMethod;

		// Token: 0x0400002D RID: 45
		private List<ResourceManager.DeferredCallbackRegisterRequest> m_DeferredCallbacksToRegister;

		// Token: 0x0400002E RID: 46
		private Action<IAsyncOperation> m_ReleaseOpNonCached;

		// Token: 0x0400002F RID: 47
		private Action<IAsyncOperation> m_ReleaseOpCached;

		// Token: 0x04000030 RID: 48
		private Action<IAsyncOperation> m_ReleaseInstanceOp;

		// Token: 0x04000031 RID: 49
		private static int s_GroupOperationTypeHash = typeof(GroupOperation).GetHashCode();

		// Token: 0x04000032 RID: 50
		private static int s_InstanceOperationTypeHash = typeof(ResourceManager.InstanceOperation).GetHashCode();

		// Token: 0x04000034 RID: 52
		private bool m_RegisteredForCallbacks;

		// Token: 0x04000035 RID: 53
		private Dictionary<Type, Type> m_ProviderOperationTypeCache = new Dictionary<Type, Type>();

		// Token: 0x0200000C RID: 12
		public enum DiagnosticEventType
		{
			// Token: 0x04000037 RID: 55
			AsyncOperationFail,
			// Token: 0x04000038 RID: 56
			AsyncOperationCreate,
			// Token: 0x04000039 RID: 57
			AsyncOperationPercentComplete,
			// Token: 0x0400003A RID: 58
			AsyncOperationComplete,
			// Token: 0x0400003B RID: 59
			AsyncOperationReferenceCount,
			// Token: 0x0400003C RID: 60
			AsyncOperationDestroy
		}

		// Token: 0x0200000D RID: 13
		private struct DeferredCallbackRegisterRequest
		{
			// Token: 0x0400003D RID: 61
			internal IAsyncOperation operation;

			// Token: 0x0400003E RID: 62
			internal bool incrementRefCount;
		}

		// Token: 0x0200000E RID: 14
		private class CompletedOperation<TObject> : AsyncOperationBase<TObject>
		{
			// Token: 0x06000088 RID: 136 RVA: 0x00003CCC File Offset: 0x00001ECC
			public void Init(TObject result, bool success, string errorMsg, bool releaseDependenciesOnFailure = true)
			{
				this.Init(result, success, (!string.IsNullOrEmpty(errorMsg)) ? new Exception(errorMsg) : null, releaseDependenciesOnFailure);
			}

			// Token: 0x06000089 RID: 137 RVA: 0x00003CE9 File Offset: 0x00001EE9
			public void Init(TObject result, bool success, Exception exception, bool releaseDependenciesOnFailure = true)
			{
				base.Result = result;
				this.m_Success = success;
				this.m_Exception = exception;
				this.m_ReleaseDependenciesOnFailure = releaseDependenciesOnFailure;
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600008A RID: 138 RVA: 0x00003D08 File Offset: 0x00001F08
			protected override string DebugName
			{
				get
				{
					return "CompletedOperation";
				}
			}

			// Token: 0x0600008B RID: 139 RVA: 0x00003D0F File Offset: 0x00001F0F
			protected override bool InvokeWaitForCompletion()
			{
				ResourceManager rm = this.m_RM;
				if (rm != null)
				{
					rm.Update(Time.unscaledDeltaTime);
				}
				if (!this.HasExecuted)
				{
					base.InvokeExecute();
				}
				return true;
			}

			// Token: 0x0600008C RID: 140 RVA: 0x00003D36 File Offset: 0x00001F36
			protected override void Execute()
			{
				base.Complete(base.Result, this.m_Success, this.m_Exception, this.m_ReleaseDependenciesOnFailure);
			}

			// Token: 0x0400003F RID: 63
			private bool m_Success;

			// Token: 0x04000040 RID: 64
			private Exception m_Exception;

			// Token: 0x04000041 RID: 65
			private bool m_ReleaseDependenciesOnFailure;
		}

		// Token: 0x0200000F RID: 15
		internal class InstanceOperation : AsyncOperationBase<GameObject>
		{
			// Token: 0x0600008D RID: 141 RVA: 0x00003D56 File Offset: 0x00001F56
			public void Init(ResourceManager rm, IInstanceProvider instanceProvider, InstantiationParameters instantiationParams, AsyncOperationHandle<GameObject> dependency)
			{
				this.m_RM = rm;
				this.m_dependency = dependency;
				this.m_instanceProvider = instanceProvider;
				this.m_instantiationParams = instantiationParams;
				this.m_scene = default(Scene);
			}

			// Token: 0x0600008E RID: 142 RVA: 0x00003D84 File Offset: 0x00001F84
			internal override DownloadStatus GetDownloadStatus(HashSet<object> visited)
			{
				if (!this.m_dependency.IsValid())
				{
					return new DownloadStatus
					{
						IsDone = base.IsDone
					};
				}
				return this.m_dependency.InternalGetDownloadStatus(visited);
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00003DC1 File Offset: 0x00001FC1
			public override void GetDependencies(List<AsyncOperationHandle> deps)
			{
				deps.Add(this.m_dependency);
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000090 RID: 144 RVA: 0x00003DD4 File Offset: 0x00001FD4
			protected override string DebugName
			{
				get
				{
					if (this.m_instanceProvider == null)
					{
						return "Instance<Invalid>";
					}
					return string.Format("Instance<{0}>({1}", this.m_instanceProvider.GetType().Name, this.m_dependency.IsValid() ? this.m_dependency.DebugName : "Invalid");
				}
			}

			// Token: 0x06000091 RID: 145 RVA: 0x00003E28 File Offset: 0x00002028
			public Scene InstanceScene()
			{
				return this.m_scene;
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00003E30 File Offset: 0x00002030
			protected override void Destroy()
			{
				this.m_instanceProvider.ReleaseInstance(this.m_RM, this.m_instance);
			}

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000093 RID: 147 RVA: 0x00003E49 File Offset: 0x00002049
			protected override float Progress
			{
				get
				{
					return this.m_dependency.PercentComplete;
				}
			}

			// Token: 0x06000094 RID: 148 RVA: 0x00003E58 File Offset: 0x00002058
			protected override bool InvokeWaitForCompletion()
			{
				if (this.m_dependency.IsValid() && !this.m_dependency.IsDone)
				{
					this.m_dependency.WaitForCompletion();
				}
				ResourceManager rm = this.m_RM;
				if (rm != null)
				{
					rm.Update(Time.unscaledDeltaTime);
				}
				if (this.m_instance == null && !this.HasExecuted)
				{
					base.InvokeExecute();
				}
				return base.IsDone;
			}

			// Token: 0x06000095 RID: 149 RVA: 0x00003EC4 File Offset: 0x000020C4
			protected override void Execute()
			{
				Exception e = this.m_dependency.OperationException;
				if (this.m_dependency.Status == AsyncOperationStatus.Succeeded)
				{
					this.m_instance = this.m_instanceProvider.ProvideInstance(this.m_RM, this.m_dependency, this.m_instantiationParams);
					if (this.m_instance != null)
					{
						this.m_scene = this.m_instance.scene;
					}
					base.Complete(this.m_instance, true, null);
					return;
				}
				base.Complete(this.m_instance, false, string.Format("Dependency operation failed with {0}.", e));
			}

			// Token: 0x04000042 RID: 66
			private AsyncOperationHandle<GameObject> m_dependency;

			// Token: 0x04000043 RID: 67
			private InstantiationParameters m_instantiationParams;

			// Token: 0x04000044 RID: 68
			private IInstanceProvider m_instanceProvider;

			// Token: 0x04000045 RID: 69
			private GameObject m_instance;

			// Token: 0x04000046 RID: 70
			private Scene m_scene;
		}
	}
}
