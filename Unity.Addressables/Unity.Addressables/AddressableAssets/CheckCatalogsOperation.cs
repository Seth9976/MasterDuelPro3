using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets.ResourceProviders;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x02000039 RID: 57
	internal class CheckCatalogsOperation : AsyncOperationBase<List<string>>
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00006381 File Offset: 0x00004581
		public CheckCatalogsOperation(AddressablesImpl aa)
		{
			this.m_Addressables = aa;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006390 File Offset: 0x00004590
		public AsyncOperationHandle<List<string>> Start(List<ResourceLocatorInfo> locatorInfos)
		{
			this.m_LocatorInfos = new List<ResourceLocatorInfo>(locatorInfos.Count);
			this.m_LocalHashes = new List<string>(locatorInfos.Count);
			List<IResourceLocation> locations = new List<IResourceLocation>(locatorInfos.Count);
			foreach (ResourceLocatorInfo rl in locatorInfos)
			{
				if (rl.CanUpdateContent)
				{
					locations.Add(rl.HashLocation);
					this.m_LocalHashes.Add(rl.LocalHash);
					this.m_LocatorInfos.Add(rl);
				}
			}
			ContentCatalogProvider ccp = this.m_Addressables.ResourceManager.ResourceProviders.FirstOrDefault((IResourceProvider rp) => rp.GetType() == typeof(ContentCatalogProvider)) as ContentCatalogProvider;
			if (ccp != null)
			{
				ccp.DisableCatalogUpdateOnStart = false;
			}
			this.m_DepOp = this.m_Addressables.ResourceManager.CreateGroupOperation<string>(locations);
			return this.m_Addressables.ResourceManager.StartOperation<List<string>>(this, this.m_DepOp);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000064B0 File Offset: 0x000046B0
		protected override bool InvokeWaitForCompletion()
		{
			if (base.IsDone)
			{
				return true;
			}
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
			ResourceManager rm2 = this.m_RM;
			if (rm2 != null)
			{
				rm2.Update(Time.unscaledDeltaTime);
			}
			return base.IsDone;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000652D File Offset: 0x0000472D
		protected override void Destroy()
		{
			this.m_DepOp.Release();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000653A File Offset: 0x0000473A
		public override void GetDependencies(List<AsyncOperationHandle> dependencies)
		{
			dependencies.Add(this.m_DepOp);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006550 File Offset: 0x00004750
		internal static List<string> ProcessDependentOpResults(IList<AsyncOperationHandle> results, List<ResourceLocatorInfo> locatorInfos, List<string> localHashes, out string errorString, out bool success)
		{
			List<string> result = new List<string>();
			List<string> errorMsgList = new List<string>();
			for (int i = 0; i < results.Count; i++)
			{
				AsyncOperationHandle remHashOp = results[i];
				string remoteHash = remHashOp.Result as string;
				if (!string.IsNullOrEmpty(remoteHash) && remoteHash != localHashes[i])
				{
					result.Add(locatorInfos[i].Locator.LocatorId);
					locatorInfos[i].ContentUpdateAvailable = true;
				}
				else if (remHashOp.OperationException != null)
				{
					result.Add(null);
					locatorInfos[i].ContentUpdateAvailable = false;
					errorMsgList.Add(remHashOp.OperationException.Message);
				}
			}
			errorString = null;
			if (errorMsgList.Count > 0)
			{
				if (errorMsgList.Count == result.Count)
				{
					result = null;
					errorString = "CheckCatalogsOperation failed with the following errors: ";
				}
				else
				{
					errorString = "Partial success in CheckCatalogsOperation with the following errors: ";
				}
				foreach (string str in errorMsgList)
				{
					errorString = errorString + "\n" + str;
				}
			}
			success = errorMsgList.Count == 0;
			return result;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000668C File Offset: 0x0000488C
		protected override void Execute()
		{
			string errorString;
			bool success;
			List<string> result = CheckCatalogsOperation.ProcessDependentOpResults(this.m_DepOp.Result, this.m_LocatorInfos, this.m_LocalHashes, out errorString, out success);
			base.Complete(result, success, errorString);
		}

		// Token: 0x040000B5 RID: 181
		private AddressablesImpl m_Addressables;

		// Token: 0x040000B6 RID: 182
		private List<string> m_LocalHashes;

		// Token: 0x040000B7 RID: 183
		private List<ResourceLocatorInfo> m_LocatorInfos;

		// Token: 0x040000B8 RID: 184
		private AsyncOperationHandle<IList<AsyncOperationHandle>> m_DepOp;
	}
}
