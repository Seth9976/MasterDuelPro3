using System;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	public class AssetReference : IKeyEvaluator
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00006107 File Offset: 0x00004307
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000610F File Offset: 0x0000430F
		public AsyncOperationHandle OperationHandle
		{
			get
			{
				return this.m_Operation;
			}
			internal set
			{
				this.m_Operation = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00006118 File Offset: 0x00004318
		public virtual object RuntimeKey
		{
			get
			{
				if (this.m_AssetGUID == null)
				{
					this.m_AssetGUID = string.Empty;
				}
				if (!string.IsNullOrEmpty(this.m_SubObjectName))
				{
					return string.Format("{0}[{1}]", this.m_AssetGUID, this.m_SubObjectName);
				}
				return this.m_AssetGUID;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00006157 File Offset: 0x00004357
		public virtual string AssetGUID
		{
			get
			{
				return this.m_AssetGUID;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000615F File Offset: 0x0000435F
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00006167 File Offset: 0x00004367
		public virtual string SubObjectName
		{
			get
			{
				return this.m_SubObjectName;
			}
			set
			{
				this.m_SubObjectName = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00006170 File Offset: 0x00004370
		internal virtual Type SubObjectType
		{
			get
			{
				if (!string.IsNullOrEmpty(this.m_SubObjectName) && this.m_SubObjectType != null)
				{
					return Type.GetType(this.m_SubObjectType);
				}
				return null;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006194 File Offset: 0x00004394
		public bool IsValid()
		{
			return this.m_Operation.IsValid();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000061A1 File Offset: 0x000043A1
		public bool IsDone
		{
			get
			{
				return this.m_Operation.IsDone;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000061AE File Offset: 0x000043AE
		public AssetReference()
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000061C1 File Offset: 0x000043C1
		public AssetReference(string guid)
		{
			this.m_AssetGUID = guid;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000061DB File Offset: 0x000043DB
		public virtual Object Asset
		{
			get
			{
				if (!this.m_Operation.IsValid())
				{
					return null;
				}
				return this.m_Operation.Result as Object;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000061FC File Offset: 0x000043FC
		public override string ToString()
		{
			return "[" + this.m_AssetGUID + "]";
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006214 File Offset: 0x00004414
		private static AsyncOperationHandle<T> CreateFailedOperation<T>()
		{
			Addressables.InitializeAsync();
			return Addressables.ResourceManager.CreateCompletedOperation<T>(default(T), new Exception("Attempting to load an asset reference that has no asset assigned to it.").Message);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000624C File Offset: 0x0000444C
		public virtual AsyncOperationHandle<TObject> LoadAssetAsync<TObject>()
		{
			AsyncOperationHandle<TObject> result = default(AsyncOperationHandle<TObject>);
			if (this.m_Operation.IsValid())
			{
				Debug.LogError("Attempting to load AssetReference that has already been loaded. Handle is exposed through getter OperationHandle");
			}
			else
			{
				result = Addressables.LoadAssetAsync<TObject>(this.RuntimeKey);
				this.OperationHandle = result;
			}
			return result;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006294 File Offset: 0x00004494
		public virtual AsyncOperationHandle<SceneInstance> LoadSceneAsync(LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100)
		{
			AsyncOperationHandle<SceneInstance> result = default(AsyncOperationHandle<SceneInstance>);
			if (this.m_Operation.IsValid())
			{
				Debug.LogError("Attempting to load AssetReference Scene that has already been loaded. Handle is exposed through getter OperationHandle");
			}
			else
			{
				result = Addressables.LoadSceneAsync(this.RuntimeKey, loadMode, activateOnLoad, priority, SceneReleaseMode.ReleaseSceneWhenSceneUnloaded);
				this.OperationHandle = result;
			}
			return result;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000062DF File Offset: 0x000044DF
		public virtual AsyncOperationHandle<SceneInstance> UnLoadScene()
		{
			return Addressables.UnloadSceneAsync(this.m_Operation, true);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000062ED File Offset: 0x000044ED
		public virtual AsyncOperationHandle<GameObject> InstantiateAsync(Vector3 position, Quaternion rotation, Transform parent = null)
		{
			return Addressables.InstantiateAsync(this.RuntimeKey, position, rotation, parent, true);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000062FE File Offset: 0x000044FE
		public virtual AsyncOperationHandle<GameObject> InstantiateAsync(Transform parent = null, bool instantiateInWorldSpace = false)
		{
			return Addressables.InstantiateAsync(this.RuntimeKey, parent, instantiateInWorldSpace, true);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006310 File Offset: 0x00004510
		public virtual bool RuntimeKeyIsValid()
		{
			string guid = this.RuntimeKey.ToString();
			int subObjectIndex = guid.IndexOf('[');
			if (subObjectIndex != -1)
			{
				guid = guid.Substring(0, subObjectIndex);
			}
			Guid result;
			return Guid.TryParse(guid, out result);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006347 File Offset: 0x00004547
		public virtual void ReleaseAsset()
		{
			if (!this.m_Operation.IsValid())
			{
				Debug.LogWarning("Cannot release a null or unloaded asset.");
				return;
			}
			this.m_Operation.Release();
			this.m_Operation = default(AsyncOperationHandle);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006378 File Offset: 0x00004578
		public virtual void ReleaseInstance(GameObject obj)
		{
			Addressables.ReleaseInstance(obj);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000020F4 File Offset: 0x000002F4
		public virtual bool ValidateAsset(Object obj)
		{
			return true;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000020F4 File Offset: 0x000002F4
		public virtual bool ValidateAsset(string path)
		{
			return true;
		}

		// Token: 0x040000B1 RID: 177
		[FormerlySerializedAs("m_assetGUID")]
		[SerializeField]
		protected internal string m_AssetGUID = "";

		// Token: 0x040000B2 RID: 178
		[SerializeField]
		private string m_SubObjectName;

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		private string m_SubObjectType;

		// Token: 0x040000B4 RID: 180
		private AsyncOperationHandle m_Operation;
	}
}
