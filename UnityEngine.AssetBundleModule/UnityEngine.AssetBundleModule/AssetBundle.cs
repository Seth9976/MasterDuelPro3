using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[ExcludeFromPreset]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromManagedStreamAsyncOperation.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetOperation.h")]
	[NativeHeader("Runtime/Scripting/ScriptingExportUtility.h")]
	[NativeHeader("Runtime/Scripting/ScriptingUtility.h")]
	[NativeHeader("AssetBundleScriptingClasses.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleSaveAndLoadHelper.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleUtility.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetUtility.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromFileAsyncOperation.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromMemoryAsyncOperation.h")]
	public class AssetBundle : Object
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private AssetBundle()
		{
		}

		// Token: 0x06000002 RID: 2
		[FreeFunction("GetAllAssetBundles")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssetBundle[] GetAllLoadedAssetBundles_Native();

		// Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		public static IEnumerable<AssetBundle> GetAllLoadedAssetBundles()
		{
			return AssetBundle.GetAllLoadedAssetBundles_Native();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		[FreeFunction("LoadFromFileAsync")]
		internal unsafe static AssetBundleCreateRequest LoadFromFileAsync_Internal(string path, uint crc, ulong offset)
		{
			AssetBundleCreateRequest assetBundleCreateRequest;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(path, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = path.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = AssetBundle.LoadFromFileAsync_Internal_Injected(ref managedSpanWrapper, crc, offset);
			}
			finally
			{
				IntPtr intPtr;
				IntPtr intPtr2 = intPtr;
				assetBundleCreateRequest = ((intPtr2 == 0) ? null : AssetBundleCreateRequest.BindingsMarshaller.ConvertToManaged(intPtr2));
				char* ptr = null;
			}
			return assetBundleCreateRequest;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		public static AssetBundleCreateRequest LoadFromFileAsync(string path)
		{
			return AssetBundle.LoadFromFileAsync_Internal(path, 0U, 0UL);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FC File Offset: 0x000002FC
		public static AssetBundleCreateRequest LoadFromFileAsync(string path, uint crc)
		{
			return AssetBundle.LoadFromFileAsync_Internal(path, crc, 0UL);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002118 File Offset: 0x00000318
		public static AssetBundleCreateRequest LoadFromFileAsync(string path, uint crc, ulong offset)
		{
			return AssetBundle.LoadFromFileAsync_Internal(path, crc, offset);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		[FreeFunction("LoadFromFile")]
		internal unsafe static AssetBundle LoadFromFile_Internal(string path, uint crc, ulong offset)
		{
			AssetBundle assetBundle;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(path, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = path.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = AssetBundle.LoadFromFile_Internal_Injected(ref managedSpanWrapper, crc, offset);
			}
			finally
			{
				IntPtr intPtr;
				assetBundle = Unmarshal.UnmarshalUnityObject<AssetBundle>(intPtr);
				char* ptr = null;
			}
			return assetBundle;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002198 File Offset: 0x00000398
		public static AssetBundle LoadFromFile(string path)
		{
			return AssetBundle.LoadFromFile_Internal(path, 0U, 0UL);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021B4 File Offset: 0x000003B4
		public static AssetBundle LoadFromFile(string path, uint crc, ulong offset)
		{
			return AssetBundle.LoadFromFile_Internal(path, crc, offset);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D0 File Offset: 0x000003D0
		public T LoadAsset<T>(string name) where T : Object
		{
			return (T)((object)this.LoadAsset(name, typeof(T)));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F8 File Offset: 0x000003F8
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		public Object LoadAsset(string name, Type type)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			bool flag3 = type == null;
			if (flag3)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAsset_Internal(name, type);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002258 File Offset: 0x00000458
		[NativeMethod("LoadAsset_Internal")]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[NativeThrows]
		private unsafe Object LoadAsset_Internal(string name, Type type)
		{
			Object @object;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = AssetBundle.LoadAsset_Internal_Injected(intPtr, ref managedSpanWrapper, type);
			}
			finally
			{
				IntPtr intPtr2;
				@object = Unmarshal.UnmarshalUnityObject<Object>(intPtr2);
				char* ptr = null;
			}
			return @object;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C8 File Offset: 0x000004C8
		public AssetBundleRequest LoadAssetAsync(string name, Type type)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			bool flag3 = type == null;
			if (flag3)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetAsync_Internal(name, type);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002328 File Offset: 0x00000528
		public AssetBundleRequest LoadAssetWithSubAssetsAsync(string name, Type type)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			bool flag3 = type == null;
			if (flag3)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssetsAsync_Internal(name, type);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002388 File Offset: 0x00000588
		public Object[] LoadAllAssets()
		{
			return this.LoadAllAssets(typeof(Object));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023AC File Offset: 0x000005AC
		public Object[] LoadAllAssets(Type type)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssets_Internal("", type);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023E4 File Offset: 0x000005E4
		public AssetBundleRequest LoadAllAssetsAsync()
		{
			return this.LoadAllAssetsAsync(typeof(Object));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002408 File Offset: 0x00000608
		public AssetBundleRequest LoadAllAssetsAsync<T>()
		{
			return this.LoadAllAssetsAsync(typeof(T));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000242C File Offset: 0x0000062C
		public AssetBundleRequest LoadAllAssetsAsync(Type type)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssetsAsync_Internal("", type);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002464 File Offset: 0x00000664
		[NativeMethod("LoadAssetAsync_Internal")]
		[NativeThrows]
		private unsafe AssetBundleRequest LoadAssetAsync_Internal(string name, Type type)
		{
			AssetBundleRequest assetBundleRequest;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = AssetBundle.LoadAssetAsync_Internal_Injected(intPtr, ref managedSpanWrapper, type);
			}
			finally
			{
				IntPtr intPtr2;
				IntPtr intPtr3 = intPtr2;
				assetBundleRequest = ((intPtr3 == 0) ? null : AssetBundleRequest.BindingsMarshaller.ConvertToManaged(intPtr3));
				char* ptr = null;
			}
			return assetBundleRequest;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024E0 File Offset: 0x000006E0
		[NativeMethod("Unload")]
		[NativeThrows]
		public void Unload(bool unloadAllLoadedObjects)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AssetBundle.Unload_Injected(intPtr, unloadAllLoadedObjects);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002504 File Offset: 0x00000704
		[NativeMethod("UnloadAsync")]
		[NativeThrows]
		public AssetBundleUnloadOperation UnloadAsync(bool unloadAllLoadedObjects)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = AssetBundle.UnloadAsync_Injected(intPtr, unloadAllLoadedObjects);
			return (intPtr2 == 0) ? null : AssetBundleUnloadOperation.BindingsMarshaller.ConvertToManaged(intPtr2);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002538 File Offset: 0x00000738
		[NativeThrows]
		[NativeMethod("LoadAssetWithSubAssets_Internal")]
		internal unsafe Object[] LoadAssetWithSubAssets_Internal(string name, Type type)
		{
			Object[] array;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				array = AssetBundle.LoadAssetWithSubAssets_Internal_Injected(intPtr, ref managedSpanWrapper, type);
			}
			finally
			{
				char* ptr = null;
			}
			return array;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025A0 File Offset: 0x000007A0
		[NativeThrows]
		[NativeMethod("LoadAssetWithSubAssetsAsync_Internal")]
		private unsafe AssetBundleRequest LoadAssetWithSubAssetsAsync_Internal(string name, Type type)
		{
			AssetBundleRequest assetBundleRequest;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = AssetBundle.LoadAssetWithSubAssetsAsync_Internal_Injected(intPtr, ref managedSpanWrapper, type);
			}
			finally
			{
				IntPtr intPtr2;
				IntPtr intPtr3 = intPtr2;
				assetBundleRequest = ((intPtr3 == 0) ? null : AssetBundleRequest.BindingsMarshaller.ConvertToManaged(intPtr3));
				char* ptr = null;
			}
			return assetBundleRequest;
		}

		// Token: 0x0600001A RID: 26
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadFromFileAsync_Internal_Injected(ref ManagedSpanWrapper path, uint crc, ulong offset);

		// Token: 0x0600001B RID: 27
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadFromFile_Internal_Injected(ref ManagedSpanWrapper path, uint crc, ulong offset);

		// Token: 0x0600001C RID: 28
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadAsset_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, Type type);

		// Token: 0x0600001D RID: 29
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadAssetAsync_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, Type type);

		// Token: 0x0600001E RID: 30
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unload_Injected(IntPtr _unity_self, bool unloadAllLoadedObjects);

		// Token: 0x0600001F RID: 31
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr UnloadAsync_Injected(IntPtr _unity_self, bool unloadAllLoadedObjects);

		// Token: 0x06000020 RID: 32
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object[] LoadAssetWithSubAssets_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, Type type);

		// Token: 0x06000021 RID: 33
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadAssetWithSubAssetsAsync_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, Type type);
	}
}
