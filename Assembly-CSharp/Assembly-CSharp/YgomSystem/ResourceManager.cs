using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.U2D;
using YgomSystem.ResourceSystem;

namespace YgomSystem
{
	// Token: 0x020004B6 RID: 1206
	public class ResourceManager : MonoBehaviour
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x060026BE RID: 9918 RVA: 0x0000216D File Offset: 0x0000036D
		public float HttpTimeOut
		{
			[CompilerGenerated]
			get
			{
				return 0f;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int unloadWaitCnt
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetUnloadWaitLimit(ResourceManager.UnloadCheckLevel unloadLevel)
		{
			return 0;
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsAbortReady()
		{
			return false;
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x0000216D File Offset: 0x0000036D
		private void AtlasRequested(string spriteAtlasname, Action<SpriteAtlas> callback)
		{
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnloadResource(Resource res)
		{
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x0000216D File Offset: 0x0000036D
		private void RemoveRequest(Dictionary<uint, string> requestDictionary, uint key)
		{
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0000216D File Offset: 0x0000036D
		private void RemoveRequest(uint key, ResourceManager.ReqType queueId)
		{
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x000029CC File Offset: 0x00000BCC
		private ResourceManager.ReqType AddRequest(uint crc, string path)
		{
			return ResourceManager.ReqType.Sound;
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator UpdateLoadQueue(Dictionary<uint, string> requestDictionary, ResourceManager.ReqType queueId)
		{
			return null;
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x0000216D File Offset: 0x0000036D
		private void DestroyAllResources()
		{
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000029CC File Offset: 0x00000BCC
		private uint findResource(string path)
		{
			return 0U;
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000029CC File Offset: 0x00000BCC
		private uint load(string path, Type systemTypeInstance, ResourceManager.RequestCompleteHandler completeHandler, bool disableErrorNotify)
		{
			return 0U;
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x0000216D File Offset: 0x0000036D
		private void unload(string path, bool force)
		{
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x0000216D File Offset: 0x0000036D
		private void unload(uint crc, bool force)
		{
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x0000216D File Offset: 0x0000036D
		private void resetData()
		{
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x000029CC File Offset: 0x00000BCC
		private uint loadImmediate(string path, Type systemTypeInstance, ResourceManager.RequestCompleteHandler completeHandler, bool disableErrorNotify)
		{
			return 0U;
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x000F16CE File Offset: 0x000EF8CE
		private Resource getResource(string path, out string workPath)
		{
			workPath = null;
			return null;
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0000216A File Offset: 0x0000036A
		private Resource getResource(string path)
		{
			return null;
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0000216A File Offset: 0x0000036A
		private Resource getResource(uint crc)
		{
			return null;
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isDone(string path)
		{
			return false;
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isDone(uint crc)
		{
			return false;
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isError(string path)
		{
			return false;
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isError(uint crc)
		{
			return false;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x0000216A File Offset: 0x0000036A
		private string getWorkPath(string path)
		{
			return null;
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool checkAssetType(Type assetType, Type loadType)
		{
			return false;
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x0000216A File Offset: 0x0000036A
		private global::UnityEngine.Object getAsset(string path, Type getSystemType)
		{
			return null;
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x0000216A File Offset: 0x0000036A
		private byte[] getBytes(string path)
		{
			return null;
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x0000216A File Offset: 0x0000036A
		private List<T> getAssets<T>(string path)
		{
			return null;
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0000216D File Offset: 0x0000036D
		private void clearCache()
		{
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetErrorPath(string path)
		{
			return null;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallErrorHandler(Resource res, bool disableErrorPath = false)
		{
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x0000216D File Offset: 0x0000036D
		public static void EnableSoundLoadSlot()
		{
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DisableSoundLoadSlot()
		{
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DefaultSoundLoadSlot()
		{
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool Load(string path, Type systemTypeInstance = null, bool disableErrorNotify = false)
		{
			return false;
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000F16D4 File Offset: 0x000EF8D4
		public static bool Load(string path, out uint crc, Type systemTypeInstance = null, bool disableErrorNotify = false)
		{
			crc = 0U;
			return false;
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool Load(ResourceManager.RequestCompleteHandler handler, string path, Type systemTypeInstance = null)
		{
			return false;
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool Load(ResourceManager.RequestCompleteHandler handler, string path, bool disableErrorNotify, Type systemTypeInstance = null)
		{
			return false;
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool LoadImmediate(string path, Type systemTypeInstance = null, bool disableErrorNotify = false)
		{
			return false;
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool LoadImmediate(ResourceManager.RequestCompleteHandler handler, string path, Type systemTypeInstance = null)
		{
			return false;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool LoadImmediate(ResourceManager.RequestCompleteHandler handler, string path, bool disableErrorNotify, Type systemTypeInstance = null)
		{
			return false;
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000F16D4 File Offset: 0x000EF8D4
		public static bool LoadImmediate(string path, out uint crc, Type systemTypeInstance = null, bool disableErrorNotify = false)
		{
			crc = 0U;
			return false;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Unload(string path, bool force = false)
		{
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Unload(uint crc, bool force = false)
		{
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsNeedUnloadResources(ResourceManager.UnloadCheckLevel unloadLevel)
		{
			return false;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckUnloadResourcesAsync(ResourceManager.UnloadCheckLevel unloadLevel, Action onComplete = null)
		{
			return false;
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator CheckUnloadResourcesAsyncRoutine(ResourceManager.UnloadCheckLevel unloadLevel, Action onComplete = null)
		{
			return null;
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadResourcesAsync(Action onComplete = null)
		{
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator UnloadResourcesAsyncRoutine(Action onComplete = null)
		{
			return null;
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDone(string path)
		{
			return false;
		}

		// Token: 0x060026F5 RID: 9973 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsDone(uint crc)
		{
			return false;
		}

		// Token: 0x060026F6 RID: 9974 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsError(string path)
		{
			return false;
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsError(uint crc)
		{
			return false;
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0000216A File Offset: 0x0000036A
		public static global::UnityEngine.Object GetAsset(string path, Type getSystemType = null)
		{
			return null;
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] GetBytes(string path)
		{
			return null;
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<T> GetAssets<T>(string path)
		{
			return null;
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetErrorHandler(ResourceManager.ErrorHandler handler)
		{
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetRetryHandler(ResourceManager.RetryHandler handler)
		{
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetProgressHandler(ResourceManager.ProgressHandler handler)
		{
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearCache()
		{
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetRequest()
		{
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsAsyncLoading()
		{
			return false;
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool Exists(string filename)
		{
			return false;
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x0000216A File Offset: 0x0000036A
		public static byte[] Decompress(byte[] compressData)
		{
			return null;
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x0000216A File Offset: 0x0000036A
		private string RemoveAutoConvertPath(string path)
		{
			return null;
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x0000216A File Offset: 0x0000036A
		private static byte[] decompressedData(byte[] data)
		{
			return null;
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x0000216A File Offset: 0x0000036A
		private Texture2D spriteToTexture2D(Sprite spr)
		{
			return null;
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x0000216A File Offset: 0x0000036A
		private Sprite texture2DToSprite(Texture2D tex)
		{
			return null;
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x0000216D File Offset: 0x0000036D
		private void finishLoadAsset(uint key, Resource res)
		{
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator RebuildMaterialsAsync(global::UnityEngine.Object[] assets, string path, Type systemTypeInstance)
		{
			return null;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x0000216D File Offset: 0x0000036D
		private void RebuildMaterialsImmediate(global::UnityEngine.Object[] assets, string path, Type systemTypeInstance)
		{
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000F16DA File Offset: 0x000EF8DA
		private bool RebuildMaterials_Prepare(global::UnityEngine.Object[] assets, string path, Type systemTypeInstance, out byte[] matInfoBytes, out Dictionary<string, Material> mtrls, out Dictionary<string, Texture> texs)
		{
			matInfoBytes = null;
			mtrls = null;
			texs = null;
			return false;
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000F16E9 File Offset: 0x000EF8E9
		private void RebuildMaterials_UnpackInfo(byte[] matInfoBytes, out Dictionary<string, object> matInfo)
		{
			matInfo = null;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0000216D File Offset: 0x0000036D
		private void RebuildMaterials_ApplyToMaterial(object matInfoValue, Material mtrl, Dictionary<string, Texture> texs)
		{
		}

		// Token: 0x040027B4 RID: 10164
		private const bool IsDisableErrorPath = false;

		// Token: 0x040027B5 RID: 10165
		private static ResourceManager s_instance;

		// Token: 0x040027B6 RID: 10166
		private ResourceManager.ErrorHandler errorHandler;

		// Token: 0x040027B7 RID: 10167
		private ResourceManager.RetryHandler retryHandler;

		// Token: 0x040027B8 RID: 10168
		private ResourceManager.ProgressHandler progressHandler;

		// Token: 0x040027B9 RID: 10169
		private const int SoundQueueNum = 4;

		// Token: 0x040027BA RID: 10170
		private const int DefaultQueueNum = 32;

		// Token: 0x040027BB RID: 10171
		private Dictionary<uint, Resource> resourceDictionary;

		// Token: 0x040027BC RID: 10172
		private Dictionary<uint, string>[] requestDictionaries;

		// Token: 0x040027BD RID: 10173
		private const bool defaultSoundLoadSlot = true;

		// Token: 0x040027BE RID: 10174
		private bool m_useSoundLoadSlot;

		// Token: 0x040027BF RID: 10175
		private Coroutine[] updateLoadQueues;

		// Token: 0x040027C0 RID: 10176
		public int m_UnloadWaitCnt;

		// Token: 0x040027C1 RID: 10177
		private bool m_UnloadingResources;

		// Token: 0x040027C2 RID: 10178
		private ResourceLoader resourceLoader;

		// Token: 0x040027C3 RID: 10179
		private ResTypeChecker resTypeChecker;

		// Token: 0x040027C4 RID: 10180
		private const string matInfoPostfix = ".matinfo";

		// Token: 0x040027C5 RID: 10181
		private Dictionary<string, Shader> shaderCache;

		// Token: 0x020004B7 RID: 1207
		public enum UnloadCheckLevel
		{
			// Token: 0x040027C7 RID: 10183
			None,
			// Token: 0x040027C8 RID: 10184
			Low,
			// Token: 0x040027C9 RID: 10185
			Middle,
			// Token: 0x040027CA RID: 10186
			High,
			// Token: 0x040027CB RID: 10187
			Force
		}

		// Token: 0x020004B8 RID: 1208
		// (Invoke) Token: 0x0600270F RID: 9999
		public delegate void RequestCompleteHandler(string path);

		// Token: 0x020004B9 RID: 1209
		// (Invoke) Token: 0x06002713 RID: 10003
		public delegate void ErrorHandler(string path);

		// Token: 0x020004BA RID: 1210
		// (Invoke) Token: 0x06002717 RID: 10007
		public delegate bool? RetryHandler(bool isstart);

		// Token: 0x020004BB RID: 1211
		// (Invoke) Token: 0x0600271B RID: 10011
		public delegate void ProgressHandler(bool isshow);

		// Token: 0x020004BC RID: 1212
		public enum ReqType
		{
			// Token: 0x040027CD RID: 10189
			Sound,
			// Token: 0x040027CE RID: 10190
			Sound2,
			// Token: 0x040027CF RID: 10191
			Sound3,
			// Token: 0x040027D0 RID: 10192
			Sound4,
			// Token: 0x040027D1 RID: 10193
			Default,
			// Token: 0x040027D2 RID: 10194
			Default2,
			// Token: 0x040027D3 RID: 10195
			Default3,
			// Token: 0x040027D4 RID: 10196
			Default4,
			// Token: 0x040027D5 RID: 10197
			Default5,
			// Token: 0x040027D6 RID: 10198
			Default6,
			// Token: 0x040027D7 RID: 10199
			Default7,
			// Token: 0x040027D8 RID: 10200
			Default8,
			// Token: 0x040027D9 RID: 10201
			Default9,
			// Token: 0x040027DA RID: 10202
			Default10,
			// Token: 0x040027DB RID: 10203
			Default11,
			// Token: 0x040027DC RID: 10204
			Default12,
			// Token: 0x040027DD RID: 10205
			Default13,
			// Token: 0x040027DE RID: 10206
			Default14,
			// Token: 0x040027DF RID: 10207
			Default15,
			// Token: 0x040027E0 RID: 10208
			Default16,
			// Token: 0x040027E1 RID: 10209
			Default17,
			// Token: 0x040027E2 RID: 10210
			Default18,
			// Token: 0x040027E3 RID: 10211
			Default19,
			// Token: 0x040027E4 RID: 10212
			Default20,
			// Token: 0x040027E5 RID: 10213
			Default21,
			// Token: 0x040027E6 RID: 10214
			Default22,
			// Token: 0x040027E7 RID: 10215
			Default23,
			// Token: 0x040027E8 RID: 10216
			Default24,
			// Token: 0x040027E9 RID: 10217
			Default25,
			// Token: 0x040027EA RID: 10218
			Default26,
			// Token: 0x040027EB RID: 10219
			Default27,
			// Token: 0x040027EC RID: 10220
			Default28,
			// Token: 0x040027ED RID: 10221
			Default29,
			// Token: 0x040027EE RID: 10222
			Default30,
			// Token: 0x040027EF RID: 10223
			Default31,
			// Token: 0x040027F0 RID: 10224
			Default32,
			// Token: 0x040027F1 RID: 10225
			Num
		}
	}
}
