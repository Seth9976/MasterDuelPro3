using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Profiling;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.SceneManagement;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x02000062 RID: 98
	public class SceneProvider : ISceneProvider2, ISceneProvider
	{
		// Token: 0x06000229 RID: 553 RVA: 0x000091BF File Offset: 0x000073BF
		public AsyncOperationHandle<SceneInstance> ProvideScene(ResourceManager resourceManager, IResourceLocation location, LoadSceneMode loadSceneMode, bool activateOnLoad, int priority)
		{
			return this.ProvideScene(resourceManager, location, new LoadSceneParameters(loadSceneMode), activateOnLoad, priority);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000091D3 File Offset: 0x000073D3
		public AsyncOperationHandle<SceneInstance> ProvideScene(ResourceManager resourceManager, IResourceLocation location, LoadSceneParameters loadSceneParameters, bool activateOnLoad, int priority)
		{
			return this.ProvideScene(resourceManager, location, loadSceneParameters, SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, activateOnLoad, priority);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000091E4 File Offset: 0x000073E4
		public AsyncOperationHandle<SceneInstance> ProvideScene(ResourceManager resourceManager, IResourceLocation location, LoadSceneParameters loadSceneParameters, SceneReleaseMode releaseMode, bool activateOnLoad, int priority)
		{
			AsyncOperationHandle<IList<AsyncOperationHandle>> depOp = default(AsyncOperationHandle<IList<AsyncOperationHandle>>);
			if (location.HasDependencies)
			{
				depOp = resourceManager.ProvideResourceGroupCached(location.Dependencies, location.DependencyHashCode, typeof(IAssetBundleResource), null, true);
			}
			SceneProvider.SceneOp op = new SceneProvider.SceneOp(resourceManager, this);
			op.Init(location, loadSceneParameters, releaseMode, activateOnLoad, priority, depOp);
			AsyncOperationHandle<SceneInstance> asyncOperationHandle = resourceManager.StartOperation<SceneInstance>(op, depOp);
			if (depOp.IsValid())
			{
				depOp.Release();
			}
			return asyncOperationHandle;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009254 File Offset: 0x00007454
		public AsyncOperationHandle<SceneInstance> ReleaseScene(ResourceManager resourceManager, AsyncOperationHandle<SceneInstance> sceneLoadHandle)
		{
			return ((ISceneProvider2)this).ReleaseScene(resourceManager, sceneLoadHandle, UnloadSceneOptions.None);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009260 File Offset: 0x00007460
		AsyncOperationHandle<SceneInstance> ISceneProvider2.ReleaseScene(ResourceManager resourceManager, AsyncOperationHandle<SceneInstance> sceneLoadHandle, UnloadSceneOptions unloadOptions)
		{
			SceneProvider.UnloadSceneOp unloadOp = new SceneProvider.UnloadSceneOp();
			unloadOp.Init(sceneLoadHandle, unloadOptions);
			return resourceManager.StartOperation<SceneInstance>(unloadOp, sceneLoadHandle);
		}

		// Token: 0x02000063 RID: 99
		private class SceneOp : AsyncOperationBase<SceneInstance>, IUpdateReceiver
		{
			// Token: 0x0600022F RID: 559 RVA: 0x00009288 File Offset: 0x00007488
			public SceneOp(ResourceManager rm, ISceneProvider2 provider)
			{
				this.m_ResourceManager = rm;
				this.m_provider = provider;
			}

			// Token: 0x06000230 RID: 560 RVA: 0x000092A0 File Offset: 0x000074A0
			internal override DownloadStatus GetDownloadStatus(HashSet<object> visited)
			{
				if (!this.m_DepOp.IsValid())
				{
					return new DownloadStatus
					{
						IsDone = base.IsDone
					};
				}
				return this.m_DepOp.InternalGetDownloadStatus(visited);
			}

			// Token: 0x06000231 RID: 561 RVA: 0x000092DD File Offset: 0x000074DD
			public void Init(IResourceLocation location, LoadSceneMode loadSceneMode, bool activateOnLoad, int priority, AsyncOperationHandle<IList<AsyncOperationHandle>> depOp)
			{
				this.Init(location, new LoadSceneParameters(loadSceneMode), SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, activateOnLoad, priority, depOp);
			}

			// Token: 0x06000232 RID: 562 RVA: 0x000092F4 File Offset: 0x000074F4
			public void Init(IResourceLocation location, LoadSceneParameters loadSceneParameters, SceneReleaseMode releaseMode, bool activateOnLoad, int priority, AsyncOperationHandle<IList<AsyncOperationHandle>> depOp)
			{
				this.m_DepOp = (depOp.IsValid() ? depOp.Acquire() : depOp);
				this.m_Location = location;
				this.m_LoadSceneParameters = loadSceneParameters;
				this.m_ReleaseMode = releaseMode;
				this.m_ActivateOnLoad = activateOnLoad;
				this.m_Priority = priority;
			}

			// Token: 0x06000233 RID: 563 RVA: 0x00009340 File Offset: 0x00007540
			protected override bool InvokeWaitForCompletion()
			{
				if (this.m_DepOp.IsValid() && !this.m_DepOp.IsDone)
				{
					this.m_DepOp.WaitForCompletion();
				}
				ResourceManager rm = this.m_RM;
				if (rm != null)
				{
					rm.Update(Time.unscaledDeltaTime);
				}
				if (!this.HasExecuted)
				{
					base.InvokeExecute();
				}
				Stopwatch timer = new Stopwatch();
				timer.Start();
				while (!base.IsDone)
				{
					((IUpdateReceiver)this).Update(Time.unscaledDeltaTime);
					if (this.m_Inst.m_Operation.progress == 0f && timer.ElapsedMilliseconds > 5000L)
					{
						throw new Exception("Infinite loop detected within LoadSceneAsync.WaitForCompletion. For more information see the notes under the Scenes section of the \"Synchronous Addressables\" page of the Addressables documentation, or consider using asynchronous scene loading code.");
					}
					if (this.m_Inst.m_Operation.allowSceneActivation && Mathf.Approximately(this.m_Inst.m_Operation.progress, 0.9f))
					{
						base.Result = this.m_Inst;
						return true;
					}
				}
				return base.IsDone;
			}

			// Token: 0x06000234 RID: 564 RVA: 0x0000942A File Offset: 0x0000762A
			public override void GetDependencies(List<AsyncOperationHandle> deps)
			{
				if (this.m_DepOp.IsValid())
				{
					deps.Add(this.m_DepOp);
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000235 RID: 565 RVA: 0x0000944A File Offset: 0x0000764A
			protected override string DebugName
			{
				get
				{
					return string.Format("Scene({0})", (this.m_Location == null) ? "Invalid" : AsyncOperationBase<SceneInstance>.ShortenPath(this.m_ResourceManager.TransformInternalId(this.m_Location), false));
				}
			}

			// Token: 0x06000236 RID: 566 RVA: 0x0000947C File Offset: 0x0000767C
			protected override void Execute()
			{
				bool loadingFromBundle = false;
				if (this.m_DepOp.IsValid())
				{
					foreach (AsyncOperationHandle d in this.m_DepOp.Result)
					{
						IAssetBundleResource abResource = d.Result as IAssetBundleResource;
						if (abResource != null && abResource.GetAssetBundle() != null)
						{
							loadingFromBundle = true;
						}
					}
				}
				if (!this.m_DepOp.IsValid() || this.m_DepOp.OperationException == null)
				{
					this.m_Inst = this.InternalLoadScene(this.m_Location, loadingFromBundle, this.m_LoadSceneParameters, this.m_ActivateOnLoad, this.m_Priority);
					((IUpdateReceiver)this).Update(0f);
				}
				else
				{
					base.Complete(this.m_Inst, false, this.m_DepOp.OperationException, true);
				}
				this.HasExecuted = true;
			}

			// Token: 0x06000237 RID: 567 RVA: 0x00009564 File Offset: 0x00007764
			internal SceneInstance InternalLoadScene(IResourceLocation location, bool loadingFromBundle, LoadSceneParameters loadSceneParameters, bool activateOnLoad, int priority)
			{
				string internalId = this.m_ResourceManager.TransformInternalId(location);
				AsyncOperation op = this.InternalLoad(internalId, loadingFromBundle, loadSceneParameters);
				op.allowSceneActivation = activateOnLoad;
				op.priority = priority;
				return new SceneInstance
				{
					m_Operation = op,
					Scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1),
					ReleaseSceneOnSceneUnloaded = (this.m_ReleaseMode == SceneReleaseMode.ReleaseSceneWhenSceneUnloaded)
				};
			}

			// Token: 0x06000238 RID: 568 RVA: 0x000095CC File Offset: 0x000077CC
			private AsyncOperation InternalLoad(string path, bool loadingFromBundle, LoadSceneParameters loadSceneParameters)
			{
				ProfilerRuntime.AddSceneOperation(base.Handle, this.m_Location, ContentStatus.Loading);
				return SceneManager.LoadSceneAsync(path, loadSceneParameters);
			}

			// Token: 0x06000239 RID: 569 RVA: 0x000095E8 File Offset: 0x000077E8
			protected override void Destroy()
			{
				if (this.m_Inst.Scene.IsValid())
				{
					this.m_provider.ReleaseScene(this.m_ResourceManager, base.Handle, UnloadSceneOptions.None).ReleaseHandleOnCompletion();
				}
				if (this.m_DepOp.IsValid())
				{
					this.m_DepOp.Release();
				}
				base.Destroy();
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x0600023A RID: 570 RVA: 0x00009648 File Offset: 0x00007848
			protected override float Progress
			{
				get
				{
					float depOpWeight = 0.9f;
					float loadOpWeight = 0.1f;
					float progress = 0f;
					if (this.m_Inst.m_Operation != null)
					{
						progress += this.m_Inst.m_Operation.progress * loadOpWeight;
					}
					if (!this.m_DepOp.IsDone)
					{
						progress += this.m_DepOp.PercentComplete * depOpWeight;
					}
					else
					{
						progress += depOpWeight;
					}
					return progress;
				}
			}

			// Token: 0x0600023B RID: 571 RVA: 0x000096B0 File Offset: 0x000078B0
			void IUpdateReceiver.Update(float unscaledDeltaTime)
			{
				if (this.m_Inst.m_Operation != null && (this.m_Inst.m_Operation.isDone || (!this.m_Inst.m_Operation.allowSceneActivation && Mathf.Approximately(this.m_Inst.m_Operation.progress, 0.9f))))
				{
					this.m_ResourceManager.RemoveUpdateReciever(this);
					ProfilerRuntime.AddSceneOperation(base.Handle, this.m_Location, ContentStatus.Active);
					base.Complete(this.m_Inst, true, null);
				}
			}

			// Token: 0x040000F9 RID: 249
			private bool m_ActivateOnLoad;

			// Token: 0x040000FA RID: 250
			private SceneInstance m_Inst;

			// Token: 0x040000FB RID: 251
			private IResourceLocation m_Location;

			// Token: 0x040000FC RID: 252
			private LoadSceneParameters m_LoadSceneParameters;

			// Token: 0x040000FD RID: 253
			private SceneReleaseMode m_ReleaseMode;

			// Token: 0x040000FE RID: 254
			private int m_Priority;

			// Token: 0x040000FF RID: 255
			private AsyncOperationHandle<IList<AsyncOperationHandle>> m_DepOp;

			// Token: 0x04000100 RID: 256
			private ResourceManager m_ResourceManager;

			// Token: 0x04000101 RID: 257
			private ISceneProvider2 m_provider;
		}

		// Token: 0x02000064 RID: 100
		private class UnloadSceneOp : AsyncOperationBase<SceneInstance>
		{
			// Token: 0x0600023C RID: 572 RVA: 0x0000973A File Offset: 0x0000793A
			public void Init(AsyncOperationHandle<SceneInstance> sceneLoadHandle, UnloadSceneOptions options)
			{
				if (sceneLoadHandle.IsValid())
				{
					this.m_sceneLoadHandle = sceneLoadHandle;
					this.m_Instance = this.m_sceneLoadHandle.Result;
				}
				this.m_UnloadOptions = options;
			}

			// Token: 0x0600023D RID: 573 RVA: 0x00009764 File Offset: 0x00007964
			protected override void Execute()
			{
				if (this.m_sceneLoadHandle.IsValid() && this.m_Instance.Scene.isLoaded)
				{
					AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(this.m_Instance.Scene, this.m_UnloadOptions);
					if (unloadOp == null)
					{
						this.UnloadSceneCompleted(null);
					}
					else
					{
						unloadOp.completed += this.UnloadSceneCompleted;
					}
				}
				else
				{
					this.UnloadSceneCompleted(null);
				}
				this.HasExecuted = true;
			}

			// Token: 0x0600023E RID: 574 RVA: 0x000097D8 File Offset: 0x000079D8
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
				Debug.LogWarning("Cannot unload a Scene with WaitForCompletion. Scenes must be unloaded asynchronously.");
				return true;
			}

			// Token: 0x0600023F RID: 575 RVA: 0x0000980C File Offset: 0x00007A0C
			private void UnloadSceneCompleted(AsyncOperation obj)
			{
				base.Complete(this.m_Instance, true, "");
				if (this.m_sceneLoadHandle.IsValid() && this.m_sceneLoadHandle.ReferenceCount > 0)
				{
					if (this.m_sceneLoadHandle.ReferenceCount == 1)
					{
						ProfilerRuntime.SceneReleased(this.m_sceneLoadHandle);
					}
					this.m_sceneLoadHandle.Release();
				}
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x06000240 RID: 576 RVA: 0x0000986A File Offset: 0x00007A6A
			protected override float Progress
			{
				get
				{
					return this.m_sceneLoadHandle.PercentComplete;
				}
			}

			// Token: 0x04000102 RID: 258
			private SceneInstance m_Instance;

			// Token: 0x04000103 RID: 259
			private AsyncOperationHandle<SceneInstance> m_sceneLoadHandle;

			// Token: 0x04000104 RID: 260
			private UnloadSceneOptions m_UnloadOptions;
		}
	}
}
