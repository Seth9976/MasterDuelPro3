using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.Initialization;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.AsyncOperations
{
	// Token: 0x02000008 RID: 8
	internal class InitalizationObjectsOperation : AsyncOperationBase<bool>
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002170 File Offset: 0x00000370
		public void Init(AsyncOperationHandle<ResourceManagerRuntimeData> rtdOp, AddressablesImpl addressables)
		{
			this.m_RtdOp = rtdOp;
			this.m_Addressables = addressables;
			this.m_Addressables.ResourceManager.RegisterForCallbacks();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002190 File Offset: 0x00000390
		protected override string DebugName
		{
			get
			{
				return "InitializationObjectsOperation";
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002198 File Offset: 0x00000398
		internal bool LogRuntimeWarnings(string pathToBuildLogs)
		{
			if (!File.Exists(pathToBuildLogs))
			{
				return false;
			}
			PackedPlayModeBuildLogs packedPlayModeBuildLogs = JsonUtility.FromJson<PackedPlayModeBuildLogs>(File.ReadAllText(pathToBuildLogs));
			bool messageLogged = false;
			foreach (PackedPlayModeBuildLogs.RuntimeBuildLog log in packedPlayModeBuildLogs.RuntimeBuildLogs)
			{
				messageLogged = true;
				switch (log.Type)
				{
				case LogType.Error:
					Addressables.LogError(log.Message);
					break;
				case LogType.Warning:
					Addressables.LogWarning(log.Message);
					break;
				}
			}
			return messageLogged;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002234 File Offset: 0x00000434
		protected override bool InvokeWaitForCompletion()
		{
			if (base.IsDone)
			{
				return true;
			}
			if (this.m_RtdOp.IsValid() && !this.m_RtdOp.IsDone)
			{
				this.m_RtdOp.WaitForCompletion();
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
			if (this.m_DepOp.IsValid() && !this.m_DepOp.IsDone)
			{
				this.m_DepOp.WaitForCompletion();
			}
			ResourceManager rm2 = this.m_RM;
			if (rm2 != null)
			{
				rm2.Update(Time.unscaledDeltaTime);
			}
			return base.IsDone;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022D8 File Offset: 0x000004D8
		protected override void Execute()
		{
			ResourceManagerRuntimeData rtd = this.m_RtdOp.Result;
			if (rtd == null)
			{
				Addressables.LogError("RuntimeData is null.  Please ensure you have built the correct Player Content.");
				base.Complete(true, true, "");
				return;
			}
			List<AsyncOperationHandle> initOperations = new List<AsyncOperationHandle>();
			foreach (ObjectInitializationData i in rtd.InitializationObjects)
			{
				if (!(i.ObjectType.Value == null))
				{
					try
					{
						AsyncOperationHandle o = i.GetAsyncInitHandle(this.m_Addressables.ResourceManager, null);
						initOperations.Add(o);
					}
					catch (Exception ex)
					{
						Addressables.LogErrorFormat("Exception thrown during initialization of object {0}: {1}", new object[]
						{
							i,
							ex.ToString()
						});
					}
				}
			}
			if (initOperations.Count > 0)
			{
				this.m_DepOp = this.m_Addressables.ResourceManager.CreateGenericGroupOperation(initOperations, true);
				this.m_DepOp.Completed += delegate(AsyncOperationHandle<IList<AsyncOperationHandle>> obj)
				{
					bool success = obj.Status == AsyncOperationStatus.Succeeded;
					base.Complete(true, success, success ? "" : string.Format("{0}, status={1}, result={2} failed initialization.", obj.DebugName, obj.Status, obj.Result));
					this.m_DepOp.Release();
				};
				return;
			}
			base.Complete(true, true, "");
		}

		// Token: 0x0400000B RID: 11
		private AsyncOperationHandle<ResourceManagerRuntimeData> m_RtdOp;

		// Token: 0x0400000C RID: 12
		private AddressablesImpl m_Addressables;

		// Token: 0x0400000D RID: 13
		private AsyncOperationHandle<IList<AsyncOperationHandle>> m_DepOp;
	}
}
