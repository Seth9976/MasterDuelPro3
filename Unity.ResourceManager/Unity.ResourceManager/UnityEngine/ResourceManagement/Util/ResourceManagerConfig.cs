using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace UnityEngine.ResourceManagement.Util
{
	// Token: 0x0200003F RID: 63
	public static class ResourceManagerConfig
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00006A84 File Offset: 0x00004C84
		public static bool ExtractKeyAndSubKey(object keyObj, out string mainKey, out string subKey)
		{
			string key = keyObj as string;
			if (key != null)
			{
				int i = key.IndexOf('[');
				if (i > 0)
				{
					int j = key.LastIndexOf(']');
					if (j > i)
					{
						mainKey = key.Substring(0, i);
						subKey = key.Substring(i + 1, j - (i + 1));
						return true;
					}
				}
			}
			mainKey = null;
			subKey = null;
			return false;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006AD8 File Offset: 0x00004CD8
		public static bool IsPathRemote(string path)
		{
			return path != null && path.StartsWith("http", StringComparison.Ordinal);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006AEC File Offset: 0x00004CEC
		public static string StripQueryParameters(string path)
		{
			if (path != null)
			{
				int idx = path.IndexOf('?');
				if (idx >= 0)
				{
					return path.Substring(0, idx);
				}
			}
			return path;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006B13 File Offset: 0x00004D13
		public static bool ShouldPathUseWebRequest(string path)
		{
			return (!ResourceManagerConfig.PlatformCanLoadLocallyFromUrlPath() || !File.Exists(path)) && path != null && path.Contains("://");
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006B36 File Offset: 0x00004D36
		private static bool PlatformCanLoadLocallyFromUrlPath()
		{
			return new List<RuntimePlatform> { RuntimePlatform.Android }.Contains(Application.platform);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006B50 File Offset: 0x00004D50
		public static Array CreateArrayResult(Type type, Object[] allAssets)
		{
			Type elementType = type.GetElementType();
			if (elementType == null)
			{
				return null;
			}
			int length = 0;
			foreach (Object asset in allAssets)
			{
				if (elementType.IsAssignableFrom(asset.GetType()))
				{
					length++;
				}
			}
			Array array = Array.CreateInstance(elementType, length);
			int index = 0;
			foreach (Object asset2 in allAssets)
			{
				if (elementType.IsAssignableFrom(asset2.GetType()))
				{
					array.SetValue(asset2, index++);
				}
			}
			return array;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006BE5 File Offset: 0x00004DE5
		public static TObject CreateArrayResult<TObject>(Object[] allAssets) where TObject : class
		{
			return ResourceManagerConfig.CreateArrayResult(typeof(TObject), allAssets) as TObject;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006C04 File Offset: 0x00004E04
		public static IList CreateListResult(Type type, Object[] allAssets)
		{
			Type[] genArgs = type.GetGenericArguments();
			IList list = Activator.CreateInstance(typeof(List<>).MakeGenericType(genArgs)) as IList;
			Type elementType = genArgs[0];
			if (list == null)
			{
				return null;
			}
			foreach (Object a in allAssets)
			{
				if (elementType.IsAssignableFrom(a.GetType()))
				{
					list.Add(a);
				}
			}
			return list;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006C6F File Offset: 0x00004E6F
		public static TObject CreateListResult<TObject>(Object[] allAssets)
		{
			return (TObject)((object)ResourceManagerConfig.CreateListResult(typeof(TObject), allAssets));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006C88 File Offset: 0x00004E88
		public static bool IsInstance<T1, T2>()
		{
			Type tA = typeof(T1);
			return typeof(T2).IsAssignableFrom(tA);
		}
	}
}
