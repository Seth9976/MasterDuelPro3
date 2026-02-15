using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x0200004A RID: 74
	[DisplayName("AssetBundle Provider")]
	public class AssetBundleProvider : ResourceProviderBase
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x0000800A File Offset: 0x0000620A
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			AssetBundleProvider.m_UnloadingBundles = new Dictionary<string, AssetBundleUnloadOperation>();
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008016 File Offset: 0x00006216
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000801D File Offset: 0x0000621D
		protected internal static Dictionary<string, AssetBundleUnloadOperation> UnloadingBundles
		{
			get
			{
				return AssetBundleProvider.m_UnloadingBundles;
			}
			internal set
			{
				AssetBundleProvider.m_UnloadingBundles = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008025 File Offset: 0x00006225
		internal static int UnloadingAssetBundleCount
		{
			get
			{
				return AssetBundleProvider.m_UnloadingBundles.Count;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00008031 File Offset: 0x00006231
		internal static int AssetBundleCount
		{
			get
			{
				return AssetBundle.GetAllLoadedAssetBundles().Count<AssetBundle>() - AssetBundleProvider.UnloadingAssetBundleCount;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008044 File Offset: 0x00006244
		internal static void WaitForAllUnloadingBundlesToComplete()
		{
			if (AssetBundleProvider.UnloadingAssetBundleCount > 0)
			{
				AssetBundleUnloadOperation[] array = AssetBundleProvider.m_UnloadingBundles.Values.ToArray<AssetBundleUnloadOperation>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].WaitForCompletion();
				}
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008080 File Offset: 0x00006280
		public override void Provide(ProvideHandle providerInterface)
		{
			AssetBundleUnloadOperation unloadOp;
			if (AssetBundleProvider.m_UnloadingBundles.TryGetValue(providerInterface.Location.InternalId, out unloadOp) && unloadOp.isDone)
			{
				unloadOp = null;
			}
			new AssetBundleResource().Start(providerInterface, unloadOp, new Func<UnityWebRequestResult, bool>(this.ShouldRetryDownloadError));
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000080CA File Offset: 0x000062CA
		public override Type GetDefaultType(IResourceLocation location)
		{
			return typeof(IAssetBundleResource);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000080D8 File Offset: 0x000062D8
		public override void Release(IResourceLocation location, object asset)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			if (asset == null)
			{
				if (!(location is DownloadOnlyLocation))
				{
					Debug.LogWarningFormat("Releasing null asset bundle from location {0}.  This is an indication that the bundle failed to load.", new object[] { location });
				}
				return;
			}
			AssetBundleResource bundle = asset as AssetBundleResource;
			AssetBundleUnloadOperation unloadOp;
			if (bundle != null && bundle.Unload(out unloadOp))
			{
				AssetBundleProvider.m_UnloadingBundles.Add(location.InternalId, unloadOp);
				unloadOp.completed += delegate(AsyncOperation op)
				{
					AssetBundleProvider.m_UnloadingBundles.Remove(location.InternalId);
				};
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008002 File Offset: 0x00006202
		public virtual bool ShouldRetryDownloadError(UnityWebRequestResult uwrResult)
		{
			return uwrResult.ShouldRetryDownloadError();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000816B File Offset: 0x0000636B
		internal virtual IOperationCacheKey CreateCacheKeyForLocation(ResourceManager rm, IResourceLocation location, Type desiredType)
		{
			return new IdCacheKey(location.GetType(), rm.TransformInternalId(location));
		}

		// Token: 0x040000CF RID: 207
		internal static Dictionary<string, AssetBundleUnloadOperation> m_UnloadingBundles = new Dictionary<string, AssetBundleUnloadOperation>();
	}
}
