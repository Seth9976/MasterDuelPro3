using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.SceneManagement;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x0200000C RID: 12
	public static class Addressables
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000030D0 File Offset: 0x000012D0
		private static AddressablesImpl m_Addressables
		{
			get
			{
				return Addressables.m_AddressablesInstance;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000030D7 File Offset: 0x000012D7
		public static string Version
		{
			get
			{
				return "";
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000030DE File Offset: 0x000012DE
		public static ResourceManager ResourceManager
		{
			get
			{
				return Addressables.m_Addressables.ResourceManager;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000030EA File Offset: 0x000012EA
		internal static AddressablesImpl Instance
		{
			get
			{
				return Addressables.m_Addressables;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000030F1 File Offset: 0x000012F1
		public static IInstanceProvider InstanceProvider
		{
			get
			{
				return Addressables.m_Addressables.InstanceProvider;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000030FD File Offset: 0x000012FD
		public static string ResolveInternalId(string id)
		{
			return Addressables.m_Addressables.ResolveInternalId(id);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000310A File Offset: 0x0000130A
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00003116 File Offset: 0x00001316
		public static Func<IResourceLocation, string> InternalIdTransformFunc
		{
			get
			{
				return Addressables.m_Addressables.InternalIdTransformFunc;
			}
			set
			{
				Addressables.m_Addressables.InternalIdTransformFunc = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003123 File Offset: 0x00001323
		// (set) Token: 0x06000044 RID: 68 RVA: 0x0000312F File Offset: 0x0000132F
		public static Action<UnityWebRequest> WebRequestOverride
		{
			get
			{
				return Addressables.m_Addressables.WebRequestOverride;
			}
			set
			{
				Addressables.m_Addressables.WebRequestOverride = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000313C File Offset: 0x0000133C
		public static string StreamingAssetsSubFolder
		{
			get
			{
				return Addressables.m_Addressables.StreamingAssetsSubFolder;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003148 File Offset: 0x00001348
		public static string BuildPath
		{
			get
			{
				return Addressables.m_Addressables.BuildPath;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003154 File Offset: 0x00001354
		public static string PlayerBuildDataPath
		{
			get
			{
				return Addressables.m_Addressables.PlayerBuildDataPath;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003160 File Offset: 0x00001360
		public static string RuntimePath
		{
			get
			{
				return Addressables.m_Addressables.RuntimePath;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000316C File Offset: 0x0000136C
		public static IEnumerable<IResourceLocator> ResourceLocators
		{
			get
			{
				return Addressables.m_Addressables.ResourceLocators;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003178 File Offset: 0x00001378
		[Conditional("ADDRESSABLES_LOG_ALL")]
		internal static void InternalSafeSerializationLog(string msg, LogType logType = LogType.Log)
		{
			if (Addressables.m_AddressablesInstance == null)
			{
				return;
			}
			switch (logType)
			{
			case LogType.Error:
				Addressables.m_AddressablesInstance.LogError(msg);
				return;
			case LogType.Assert:
				break;
			case LogType.Warning:
				Addressables.m_AddressablesInstance.LogWarning(msg);
				return;
			case LogType.Log:
				Addressables.m_AddressablesInstance.Log(msg);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031C8 File Offset: 0x000013C8
		[Conditional("ADDRESSABLES_LOG_ALL")]
		internal static void InternalSafeSerializationLogFormat(string format, LogType logType = LogType.Log, params object[] args)
		{
			if (Addressables.m_AddressablesInstance == null)
			{
				return;
			}
			switch (logType)
			{
			case LogType.Error:
				Addressables.m_AddressablesInstance.LogErrorFormat(format, args);
				return;
			case LogType.Assert:
				break;
			case LogType.Warning:
				Addressables.m_AddressablesInstance.LogWarningFormat(format, args);
				return;
			case LogType.Log:
				Addressables.m_AddressablesInstance.LogFormat(format, args);
				break;
			default:
				return;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000321A File Offset: 0x0000141A
		[Conditional("ADDRESSABLES_LOG_ALL")]
		public static void Log(string msg)
		{
			Addressables.m_Addressables.Log(msg);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003227 File Offset: 0x00001427
		[Conditional("ADDRESSABLES_LOG_ALL")]
		public static void LogFormat(string format, params object[] args)
		{
			Addressables.m_Addressables.LogFormat(format, args);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003235 File Offset: 0x00001435
		public static void LogWarning(string msg)
		{
			Addressables.m_Addressables.LogWarning(msg);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003242 File Offset: 0x00001442
		public static void LogWarningFormat(string format, params object[] args)
		{
			Addressables.m_Addressables.LogWarningFormat(format, args);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003250 File Offset: 0x00001450
		public static void LogError(string msg)
		{
			Addressables.m_Addressables.LogError(msg);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000325D File Offset: 0x0000145D
		public static void LogException(AsyncOperationHandle op, Exception ex)
		{
			Addressables.m_Addressables.LogException(op, ex);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000326B File Offset: 0x0000146B
		public static void LogException(Exception ex)
		{
			Addressables.m_Addressables.LogException(ex);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003278 File Offset: 0x00001478
		public static void LogErrorFormat(string format, params object[] args)
		{
			Addressables.m_Addressables.LogErrorFormat(format, args);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003286 File Offset: 0x00001486
		public static AsyncOperationHandle<IResourceLocator> InitializeAsync()
		{
			return Addressables.m_Addressables.InitializeAsync();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003292 File Offset: 0x00001492
		public static AsyncOperationHandle<IResourceLocator> InitializeAsync(bool autoReleaseHandle)
		{
			return Addressables.m_Addressables.InitializeAsync(autoReleaseHandle);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000329F File Offset: 0x0000149F
		public static AsyncOperationHandle<IResourceLocator> LoadContentCatalogAsync(string catalogPath, string providerSuffix = null)
		{
			return Addressables.m_Addressables.LoadContentCatalogAsync(catalogPath, false, providerSuffix);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000032AE File Offset: 0x000014AE
		public static AsyncOperationHandle<IResourceLocator> LoadContentCatalogAsync(string catalogPath, bool autoReleaseHandle, string providerSuffix = null)
		{
			return Addressables.m_Addressables.LoadContentCatalogAsync(catalogPath, autoReleaseHandle, providerSuffix);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000032BD File Offset: 0x000014BD
		public static AsyncOperationHandle<TObject> LoadAssetAsync<TObject>(IResourceLocation location)
		{
			return Addressables.m_Addressables.LoadAssetAsync<TObject>(location);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000032CA File Offset: 0x000014CA
		public static AsyncOperationHandle<TObject> LoadAssetAsync<TObject>(object key)
		{
			return Addressables.m_Addressables.LoadAssetAsync<TObject>(key);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000032D7 File Offset: 0x000014D7
		public static AsyncOperationHandle<IList<IResourceLocation>> LoadResourceLocationsAsync(IEnumerable keys, Addressables.MergeMode mode, Type type = null)
		{
			return Addressables.m_Addressables.LoadResourceLocationsAsync(keys, mode, type);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000032E6 File Offset: 0x000014E6
		public static AsyncOperationHandle<IList<IResourceLocation>> LoadResourceLocationsAsync(object key, Type type = null)
		{
			return Addressables.m_Addressables.LoadResourceLocationsAsync(key, type);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000032F4 File Offset: 0x000014F4
		public static AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(IList<IResourceLocation> locations, Action<TObject> callback)
		{
			return Addressables.m_Addressables.LoadAssetsAsync<TObject>(locations, callback, true);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003303 File Offset: 0x00001503
		public static AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(IList<IResourceLocation> locations, Action<TObject> callback, bool releaseDependenciesOnFailure)
		{
			return Addressables.m_Addressables.LoadAssetsAsync<TObject>(locations, callback, releaseDependenciesOnFailure);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003312 File Offset: 0x00001512
		public static AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(IEnumerable keys, Action<TObject> callback, Addressables.MergeMode mode)
		{
			return Addressables.m_Addressables.LoadAssetsAsync<TObject>(keys, callback, mode, true);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003322 File Offset: 0x00001522
		public static AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(string key, Action<TObject> callback = null)
		{
			return Addressables.m_Addressables.LoadAssetsAsync<TObject>(new List<string> { key }, callback, Addressables.MergeMode.None, true);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000333D File Offset: 0x0000153D
		public static AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(IEnumerable keys, Action<TObject> callback, Addressables.MergeMode mode, bool releaseDependenciesOnFailure)
		{
			return Addressables.m_Addressables.LoadAssetsAsync<TObject>(keys, callback, mode, releaseDependenciesOnFailure);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000334D File Offset: 0x0000154D
		public static AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(string key, bool releaseDependenciesOnFailure, Action<TObject> callback = null)
		{
			return Addressables.m_Addressables.LoadAssetsAsync<TObject>(new List<string> { key }, callback, Addressables.MergeMode.None, releaseDependenciesOnFailure);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003368 File Offset: 0x00001568
		public static AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(object key, Action<TObject> callback)
		{
			return Addressables.m_Addressables.LoadAssetsAsync<TObject>(key, callback, true);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003377 File Offset: 0x00001577
		public static AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(object key, Action<TObject> callback, bool releaseDependenciesOnFailure)
		{
			return Addressables.m_Addressables.LoadAssetsAsync<TObject>(key, callback, releaseDependenciesOnFailure);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003386 File Offset: 0x00001586
		public static void Release<TObject>(TObject obj)
		{
			Addressables.m_Addressables.Release<TObject>(obj);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003393 File Offset: 0x00001593
		public static void Release<TObject>(AsyncOperationHandle<TObject> handle)
		{
			handle.Release();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000339C File Offset: 0x0000159C
		public static void Release(AsyncOperationHandle handle)
		{
			handle.Release();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000033A5 File Offset: 0x000015A5
		public static bool ReleaseInstance(GameObject instance)
		{
			return Addressables.m_Addressables.ReleaseInstance(instance);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000033B2 File Offset: 0x000015B2
		public static bool ReleaseInstance(AsyncOperationHandle handle)
		{
			handle.Release();
			return true;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033BC File Offset: 0x000015BC
		public static bool ReleaseInstance(AsyncOperationHandle<GameObject> handle)
		{
			handle.Release();
			return true;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000033C6 File Offset: 0x000015C6
		public static AsyncOperationHandle<long> GetDownloadSizeAsync(object key)
		{
			return Addressables.m_Addressables.GetDownloadSizeAsync(key);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000033C6 File Offset: 0x000015C6
		public static AsyncOperationHandle<long> GetDownloadSizeAsync(string key)
		{
			return Addressables.m_Addressables.GetDownloadSizeAsync(key);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000033D3 File Offset: 0x000015D3
		public static AsyncOperationHandle<long> GetDownloadSizeAsync(IEnumerable keys)
		{
			return Addressables.m_Addressables.GetDownloadSizeAsync(keys);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000033E0 File Offset: 0x000015E0
		public static AsyncOperationHandle DownloadDependenciesAsync(object key, bool autoReleaseHandle = false)
		{
			return Addressables.m_Addressables.DownloadDependenciesAsync(key, autoReleaseHandle);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000033EE File Offset: 0x000015EE
		public static AsyncOperationHandle DownloadDependenciesAsync(IList<IResourceLocation> locations, bool autoReleaseHandle = false)
		{
			return Addressables.m_Addressables.DownloadDependenciesAsync(locations, autoReleaseHandle);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000033FC File Offset: 0x000015FC
		public static AsyncOperationHandle DownloadDependenciesAsync(IEnumerable keys, Addressables.MergeMode mode, bool autoReleaseHandle = false)
		{
			return Addressables.m_Addressables.DownloadDependenciesAsync(keys, mode, autoReleaseHandle);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000340B File Offset: 0x0000160B
		public static void ClearDependencyCacheAsync(object key)
		{
			Addressables.m_Addressables.ClearDependencyCacheAsync(key, true);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000341A File Offset: 0x0000161A
		public static void ClearDependencyCacheAsync(IList<IResourceLocation> locations)
		{
			Addressables.m_Addressables.ClearDependencyCacheAsync(locations, true);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003429 File Offset: 0x00001629
		public static void ClearDependencyCacheAsync(IEnumerable keys)
		{
			Addressables.m_Addressables.ClearDependencyCacheAsync(keys, true);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000340B File Offset: 0x0000160B
		public static void ClearDependencyCacheAsync(string key)
		{
			Addressables.m_Addressables.ClearDependencyCacheAsync(key, true);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003438 File Offset: 0x00001638
		public static AsyncOperationHandle<bool> ClearDependencyCacheAsync(object key, bool autoReleaseHandle)
		{
			return Addressables.m_Addressables.ClearDependencyCacheAsync(key, autoReleaseHandle);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003446 File Offset: 0x00001646
		public static AsyncOperationHandle<bool> ClearDependencyCacheAsync(IList<IResourceLocation> locations, bool autoReleaseHandle)
		{
			return Addressables.m_Addressables.ClearDependencyCacheAsync(locations, autoReleaseHandle);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003454 File Offset: 0x00001654
		public static AsyncOperationHandle<bool> ClearDependencyCacheAsync(IEnumerable keys, bool autoReleaseHandle)
		{
			return Addressables.m_Addressables.ClearDependencyCacheAsync(keys, autoReleaseHandle);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003438 File Offset: 0x00001638
		public static AsyncOperationHandle<bool> ClearDependencyCacheAsync(string key, bool autoReleaseHandle)
		{
			return Addressables.m_Addressables.ClearDependencyCacheAsync(key, autoReleaseHandle);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003462 File Offset: 0x00001662
		public static ResourceLocatorInfo GetLocatorInfo(string locatorId)
		{
			return Addressables.m_Addressables.GetLocatorInfo(locatorId);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000346F File Offset: 0x0000166F
		public static ResourceLocatorInfo GetLocatorInfo(IResourceLocator locator)
		{
			return Addressables.m_Addressables.GetLocatorInfo(locator.LocatorId);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003481 File Offset: 0x00001681
		public static AsyncOperationHandle<GameObject> InstantiateAsync(IResourceLocation location, Transform parent = null, bool instantiateInWorldSpace = false, bool trackHandle = true)
		{
			return Addressables.m_Addressables.InstantiateAsync(location, new InstantiationParameters(parent, instantiateInWorldSpace), trackHandle);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003496 File Offset: 0x00001696
		public static AsyncOperationHandle<GameObject> InstantiateAsync(IResourceLocation location, Vector3 position, Quaternion rotation, Transform parent = null, bool trackHandle = true)
		{
			return Addressables.m_Addressables.InstantiateAsync(location, position, rotation, parent, trackHandle);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000034A8 File Offset: 0x000016A8
		public static AsyncOperationHandle<GameObject> InstantiateAsync(object key, Transform parent = null, bool instantiateInWorldSpace = false, bool trackHandle = true)
		{
			return Addressables.m_Addressables.InstantiateAsync(key, parent, instantiateInWorldSpace, trackHandle);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000034B8 File Offset: 0x000016B8
		public static AsyncOperationHandle<GameObject> InstantiateAsync(object key, Vector3 position, Quaternion rotation, Transform parent = null, bool trackHandle = true)
		{
			return Addressables.m_Addressables.InstantiateAsync(key, position, rotation, parent, trackHandle);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000034CA File Offset: 0x000016CA
		public static AsyncOperationHandle<GameObject> InstantiateAsync(object key, InstantiationParameters instantiateParameters, bool trackHandle = true)
		{
			return Addressables.m_Addressables.InstantiateAsync(key, instantiateParameters, trackHandle);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000034D9 File Offset: 0x000016D9
		public static AsyncOperationHandle<GameObject> InstantiateAsync(IResourceLocation location, InstantiationParameters instantiateParameters, bool trackHandle = true)
		{
			return Addressables.m_Addressables.InstantiateAsync(location, instantiateParameters, trackHandle);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000034E8 File Offset: 0x000016E8
		public static AsyncOperationHandle<SceneInstance> LoadSceneAsync(object key, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100, SceneReleaseMode releaseMode = SceneReleaseMode.ReleaseSceneWhenSceneUnloaded)
		{
			return Addressables.m_Addressables.LoadSceneAsync(key, new LoadSceneParameters(loadMode), releaseMode, activateOnLoad, priority, true);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003500 File Offset: 0x00001700
		public static AsyncOperationHandle<SceneInstance> LoadSceneAsync(object key, LoadSceneMode loadMode, SceneReleaseMode releaseMode, bool activateOnLoad = true, int priority = 100)
		{
			return Addressables.m_Addressables.LoadSceneAsync(key, new LoadSceneParameters(loadMode), releaseMode, activateOnLoad, priority, true);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003518 File Offset: 0x00001718
		public static AsyncOperationHandle<SceneInstance> LoadSceneAsync(object key, LoadSceneParameters loadSceneParameters, bool activateOnLoad = true, int priority = 100)
		{
			return Addressables.m_Addressables.LoadSceneAsync(key, loadSceneParameters, SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, activateOnLoad, priority, true);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000352A File Offset: 0x0000172A
		public static AsyncOperationHandle<SceneInstance> LoadSceneAsync(object key, LoadSceneParameters loadSceneParameters, SceneReleaseMode releaseMode, bool activateOnLoad = true, int priority = 100)
		{
			return Addressables.m_Addressables.LoadSceneAsync(key, loadSceneParameters, releaseMode, activateOnLoad, priority, true);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000353D File Offset: 0x0000173D
		public static AsyncOperationHandle<SceneInstance> LoadSceneAsync(IResourceLocation location, LoadSceneMode loadMode = LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100)
		{
			return Addressables.m_Addressables.LoadSceneAsync(location, new LoadSceneParameters(loadMode), SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, activateOnLoad, priority, true);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003554 File Offset: 0x00001754
		public static AsyncOperationHandle<SceneInstance> LoadSceneAsync(IResourceLocation location, LoadSceneMode loadMode, SceneReleaseMode releaseMode, bool activateOnLoad = true, int priority = 100)
		{
			return Addressables.m_Addressables.LoadSceneAsync(location, new LoadSceneParameters(loadMode), releaseMode, activateOnLoad, priority, true);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000356C File Offset: 0x0000176C
		public static AsyncOperationHandle<SceneInstance> LoadSceneAsync(IResourceLocation location, LoadSceneParameters loadSceneParameters, bool activateOnLoad = true, int priority = 100)
		{
			return Addressables.m_Addressables.LoadSceneAsync(location, loadSceneParameters, SceneReleaseMode.ReleaseSceneWhenSceneUnloaded, activateOnLoad, priority, true);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000357E File Offset: 0x0000177E
		public static AsyncOperationHandle<SceneInstance> LoadSceneAsync(IResourceLocation location, LoadSceneParameters loadSceneParameters, SceneReleaseMode releaseMode, bool activateOnLoad = true, int priority = 100)
		{
			return Addressables.m_Addressables.LoadSceneAsync(location, loadSceneParameters, releaseMode, activateOnLoad, priority, true);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003591 File Offset: 0x00001791
		public static AsyncOperationHandle<SceneInstance> UnloadSceneAsync(SceneInstance scene, UnloadSceneOptions unloadOptions, bool autoReleaseHandle = true)
		{
			return Addressables.m_Addressables.UnloadSceneAsync(scene, unloadOptions, autoReleaseHandle);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000035A0 File Offset: 0x000017A0
		public static AsyncOperationHandle<SceneInstance> UnloadSceneAsync(AsyncOperationHandle handle, UnloadSceneOptions unloadOptions, bool autoReleaseHandle = true)
		{
			return Addressables.m_Addressables.UnloadSceneAsync(handle, unloadOptions, autoReleaseHandle);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000035AF File Offset: 0x000017AF
		public static AsyncOperationHandle<SceneInstance> UnloadSceneAsync(SceneInstance scene, bool autoReleaseHandle = true)
		{
			return Addressables.m_Addressables.UnloadSceneAsync(scene, UnloadSceneOptions.None, autoReleaseHandle);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000035BE File Offset: 0x000017BE
		public static AsyncOperationHandle<SceneInstance> UnloadSceneAsync(AsyncOperationHandle handle, bool autoReleaseHandle = true)
		{
			return Addressables.m_Addressables.UnloadSceneAsync(handle, UnloadSceneOptions.None, autoReleaseHandle);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000035CD File Offset: 0x000017CD
		public static AsyncOperationHandle<SceneInstance> UnloadSceneAsync(AsyncOperationHandle<SceneInstance> handle, bool autoReleaseHandle = true)
		{
			return Addressables.m_Addressables.UnloadSceneAsync(handle, UnloadSceneOptions.None, autoReleaseHandle);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000035DC File Offset: 0x000017DC
		public static AsyncOperationHandle<List<string>> CheckForCatalogUpdates(bool autoReleaseHandle = true)
		{
			return Addressables.m_Addressables.CheckForCatalogUpdates(autoReleaseHandle);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000035E9 File Offset: 0x000017E9
		public static AsyncOperationHandle<List<IResourceLocator>> UpdateCatalogs(IEnumerable<string> catalogs = null, bool autoReleaseHandle = true)
		{
			return Addressables.m_Addressables.UpdateCatalogs(catalogs, autoReleaseHandle, false);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000035F8 File Offset: 0x000017F8
		public static AsyncOperationHandle<List<IResourceLocator>> UpdateCatalogs(bool autoCleanBundleCache, IEnumerable<string> catalogs = null, bool autoReleaseHandle = true)
		{
			return Addressables.m_Addressables.UpdateCatalogs(catalogs, autoReleaseHandle, autoCleanBundleCache);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003607 File Offset: 0x00001807
		public static void AddResourceLocator(IResourceLocator locator, string localCatalogHash = null, IResourceLocation remoteCatalogLocation = null)
		{
			Addressables.m_Addressables.AddResourceLocator(locator, localCatalogHash, remoteCatalogLocation);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003616 File Offset: 0x00001816
		public static void RemoveResourceLocator(IResourceLocator locator)
		{
			Addressables.m_Addressables.RemoveResourceLocator(locator);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003623 File Offset: 0x00001823
		public static void ClearResourceLocators()
		{
			Addressables.m_Addressables.ClearResourceLocators();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000362F File Offset: 0x0000182F
		public static AsyncOperationHandle<bool> CleanBundleCache(IEnumerable<string> catalogsIds = null)
		{
			return Addressables.m_Addressables.CleanBundleCache(catalogsIds, false);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000363D File Offset: 0x0000183D
		public static ResourceLocationBase CreateCatalogLocationWithHashDependencies<T>(string remoteCatalogPath) where T : IResourceProvider
		{
			return Addressables.m_Addressables.CreateCatalogLocationWithHashDependencies<T>(remoteCatalogPath);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000364A File Offset: 0x0000184A
		public static ResourceLocationBase CreateCatalogLocationWithHashDependencies<T>(IResourceLocation remoteCatalogLocation) where T : IResourceProvider
		{
			return Addressables.m_Addressables.CreateCatalogLocationWithHashDependencies<T>(remoteCatalogLocation);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003657 File Offset: 0x00001857
		public static ResourceLocationBase CreateCatalogLocationWithHashDependencies<T>(string remoteCatalogPath, string remoteHashPath) where T : IResourceProvider
		{
			return Addressables.m_Addressables.CreateCatalogLocationWithHashDependencies<T>(remoteCatalogPath, remoteHashPath);
		}

		// Token: 0x0400002E RID: 46
		internal static bool reinitializeAddressables = true;

		// Token: 0x0400002F RID: 47
		internal static AddressablesImpl m_AddressablesInstance = new AddressablesImpl(new LRUCacheAllocationStrategy(1000, 1000, 100, 10));

		// Token: 0x04000030 RID: 48
		public const string kAddressablesRuntimeDataPath = "AddressablesRuntimeDataPath";

		// Token: 0x04000031 RID: 49
		private const string k_AddressablesLogConditional = "ADDRESSABLES_LOG_ALL";

		// Token: 0x04000032 RID: 50
		public const string kAddressablesRuntimeBuildLogPath = "AddressablesRuntimeBuildLog";

		// Token: 0x04000033 RID: 51
		public static string LibraryPath = "Library/com.unity.addressables/";

		// Token: 0x04000034 RID: 52
		public static string BuildReportPath = "Library/com.unity.addressables/BuildReports/";

		// Token: 0x0200000D RID: 13
		public enum MergeMode
		{
			// Token: 0x04000036 RID: 54
			None,
			// Token: 0x04000037 RID: 55
			UseFirst = 0,
			// Token: 0x04000038 RID: 56
			Union,
			// Token: 0x04000039 RID: 57
			Intersection
		}
	}
}
