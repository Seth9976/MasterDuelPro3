using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000A3 RID: 163
	[NativeHeader("Runtime/Misc/CachingManager.h")]
	[StaticAccessor("GetCachingManager()", StaticAccessorType.Dot)]
	public sealed class Caching
	{
		// Token: 0x1700006B RID: 107
		// (set) Token: 0x0600029F RID: 671
		public static extern bool compressionEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002A0 RID: 672
		public static extern bool ready
		{
			[NativeName("GetIsReady")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00006674 File Offset: 0x00004874
		public static bool ClearCachedVersion(string assetBundleName, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersionInternal(assetBundleName, hash);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000066A4 File Offset: 0x000048A4
		[NativeName("ClearCachedVersion")]
		internal unsafe static bool ClearCachedVersionInternal(string assetBundleName, Hash128 hash)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetBundleName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = assetBundleName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Caching.ClearCachedVersionInternal_Injected(ref managedSpanWrapper, ref hash);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000066FC File Offset: 0x000048FC
		public static bool ClearOtherCachedVersions(string assetBundleName, Hash128 hash)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersions(assetBundleName, hash, true);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000672C File Offset: 0x0000492C
		public static bool ClearAllCachedVersions(string assetBundleName)
		{
			bool flag = string.IsNullOrEmpty(assetBundleName);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.ClearCachedVersions(assetBundleName, default(Hash128), false);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00006764 File Offset: 0x00004964
		internal unsafe static bool ClearCachedVersions(string assetBundleName, Hash128 hash, bool keepInputVersion)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetBundleName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = assetBundleName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Caching.ClearCachedVersions_Injected(ref managedSpanWrapper, ref hash, keepInputVersion);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000067C0 File Offset: 0x000049C0
		public static bool IsVersionCached(CachedAssetBundle cachedBundle)
		{
			bool flag = string.IsNullOrEmpty(cachedBundle.name);
			if (flag)
			{
				throw new ArgumentException("Input AssetBundle name cannot be null or empty.");
			}
			return Caching.IsVersionCached("", cachedBundle.name, cachedBundle.hash);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00006808 File Offset: 0x00004A08
		[NativeName("IsCached")]
		internal unsafe static bool IsVersionCached(string url, string assetBundleName, Hash128 hash)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(url, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = url.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(assetBundleName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = assetBundleName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				flag = Caching.IsVersionCached_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, ref hash);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return flag;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00006894 File Offset: 0x00004A94
		public static Cache AddCache(string cachePath)
		{
			bool flag = string.IsNullOrEmpty(cachePath);
			if (flag)
			{
				throw new ArgumentNullException("Cache path cannot be null or empty.");
			}
			bool isReadonly = false;
			bool flag2 = cachePath.Replace('\\', '/').StartsWith(Application.streamingAssetsPath);
			if (flag2)
			{
				isReadonly = true;
			}
			else
			{
				bool flag3 = !Directory.Exists(cachePath);
				if (flag3)
				{
					throw new ArgumentException("Cache path '" + cachePath + "' doesn't exist.");
				}
				bool flag4 = (File.GetAttributes(cachePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
				if (flag4)
				{
					isReadonly = true;
				}
			}
			bool valid = Caching.GetCacheByPath(cachePath).valid;
			if (valid)
			{
				throw new InvalidOperationException("Cache with path '" + cachePath + "' has already been added.");
			}
			return Caching.AddCache(cachePath, isReadonly);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00006948 File Offset: 0x00004B48
		[NativeName("AddCachePath")]
		internal unsafe static Cache AddCache(string cachePath, bool isReadonly)
		{
			Cache cache2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(cachePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = cachePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Cache cache;
				Caching.AddCache_Injected(ref managedSpanWrapper, isReadonly, out cache);
			}
			finally
			{
				char* ptr = null;
				Cache cache;
				cache2 = cache;
			}
			return cache2;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000069A4 File Offset: 0x00004BA4
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		[NativeName("Caching_GetCacheHandleByPath")]
		[NativeThrows]
		public unsafe static Cache GetCacheByPath(string cachePath)
		{
			Cache cache2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(cachePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = cachePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Cache cache;
				Caching.GetCacheByPath_Injected(ref managedSpanWrapper, out cache);
			}
			finally
			{
				char* ptr = null;
				Cache cache;
				cache2 = cache;
			}
			return cache2;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00006A00 File Offset: 0x00004C00
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache defaultCache
		{
			[NativeName("Caching_GetDefaultCacheHandle")]
			get
			{
				Cache cache;
				Caching.get_defaultCache_Injected(out cache);
				return cache;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00006A18 File Offset: 0x00004C18
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00006A30 File Offset: 0x00004C30
		[StaticAccessor("CachingManagerWrapper", StaticAccessorType.DoubleColon)]
		public static Cache currentCacheForWriting
		{
			[NativeName("Caching_GetCurrentCacheHandle")]
			get
			{
				Cache cache;
				Caching.get_currentCacheForWriting_Injected(out cache);
				return cache;
			}
			[NativeThrows]
			[NativeName("Caching_SetCurrentCacheByHandle")]
			set
			{
				Caching.set_currentCacheForWriting_Injected(ref value);
			}
		}

		// Token: 0x060002AE RID: 686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearCachedVersionInternal_Injected(ref ManagedSpanWrapper assetBundleName, [In] ref Hash128 hash);

		// Token: 0x060002AF RID: 687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearCachedVersions_Injected(ref ManagedSpanWrapper assetBundleName, [In] ref Hash128 hash, bool keepInputVersion);

		// Token: 0x060002B0 RID: 688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsVersionCached_Injected(ref ManagedSpanWrapper url, ref ManagedSpanWrapper assetBundleName, [In] ref Hash128 hash);

		// Token: 0x060002B1 RID: 689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddCache_Injected(ref ManagedSpanWrapper cachePath, bool isReadonly, out Cache ret);

		// Token: 0x060002B2 RID: 690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCacheByPath_Injected(ref ManagedSpanWrapper cachePath, out Cache ret);

		// Token: 0x060002B3 RID: 691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_defaultCache_Injected(out Cache ret);

		// Token: 0x060002B4 RID: 692
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_currentCacheForWriting_Injected(out Cache ret);

		// Token: 0x060002B5 RID: 693
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_currentCacheForWriting_Injected([In] ref Cache value);
	}
}
