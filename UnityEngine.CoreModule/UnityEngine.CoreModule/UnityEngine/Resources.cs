using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x0200016F RID: 367
	[NativeHeader("Runtime/Export/Resources/Resources.bindings.h")]
	[NativeHeader("Runtime/Misc/ResourceManagerUtility.h")]
	public sealed class Resources
	{
		// Token: 0x06000F61 RID: 3937 RVA: 0x0002061C File Offset: 0x0001E81C
		internal static T[] ConvertObjects<T>(Object[] rawObjects) where T : Object
		{
			bool flag = rawObjects == null;
			T[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				T[] typedObjects = new T[rawObjects.Length];
				for (int i = 0; i < typedObjects.Length; i++)
				{
					typedObjects[i] = (T)((object)rawObjects[i]);
				}
				array = typedObjects;
			}
			return array;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00020668 File Offset: 0x0001E868
		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return ResourcesAPI.ActiveAPI.FindObjectsOfTypeAll(type);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00020688 File Offset: 0x0001E888
		public static T[] FindObjectsOfTypeAll<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.FindObjectsOfTypeAll(typeof(T)));
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x000206B0 File Offset: 0x0001E8B0
		public static Object Load(string path)
		{
			return Resources.Load(path, typeof(Object));
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x000206D4 File Offset: 0x0001E8D4
		public static T Load<T>(string path) where T : Object
		{
			return (T)((object)Resources.Load(path, typeof(T)));
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x000206FC File Offset: 0x0001E8FC
		public static Object Load(string path, Type systemTypeInstance)
		{
			return ResourcesAPI.ActiveAPI.Load(path, systemTypeInstance);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002071C File Offset: 0x0001E91C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction("GetScriptingBuiltinResource", ThrowsException = true)]
		public unsafe static Object GetBuiltinResource([NotNull] Type type, string path)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
			}
			Object @object;
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
				IntPtr builtinResource_Injected = Resources.GetBuiltinResource_Injected(type, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr builtinResource_Injected;
				@object = Unmarshal.UnmarshalUnityObject<Object>(builtinResource_Injected);
				char* ptr = null;
			}
			return @object;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002078C File Offset: 0x0001E98C
		public static T GetBuiltinResource<T>(string path) where T : Object
		{
			return (T)((object)Resources.GetBuiltinResource(typeof(T), path));
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x000207B4 File Offset: 0x0001E9B4
		[FreeFunction("Resources_Bindings::UnloadUnusedAssets")]
		public static AsyncOperation UnloadUnusedAssets()
		{
			IntPtr intPtr = Resources.UnloadUnusedAssets_Injected();
			return (intPtr == 0) ? null : AsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr);
		}

		// Token: 0x06000F6A RID: 3946
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBuiltinResource_Injected(Type type, ref ManagedSpanWrapper path);

		// Token: 0x06000F6B RID: 3947
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr UnloadUnusedAssets_Injected();
	}
}
