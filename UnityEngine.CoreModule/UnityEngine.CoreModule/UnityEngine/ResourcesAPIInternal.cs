using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x0200016D RID: 365
	[NativeHeader("Runtime/Export/Resources/Resources.bindings.h")]
	[NativeHeader("Runtime/Misc/ResourceManagerUtility.h")]
	internal static class ResourcesAPIInternal
	{
		// Token: 0x06000F55 RID: 3925
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[FreeFunction("Resources_Bindings::FindObjectsOfTypeAll")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeAll(Type type);

		// Token: 0x06000F56 RID: 3926 RVA: 0x00020504 File Offset: 0x0001E704
		[FreeFunction("GetShaderNameRegistry().FindShader")]
		public unsafe static Shader FindShaderByName(string name)
		{
			Shader shader;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = ResourcesAPIInternal.FindShaderByName_Injected(ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr;
				shader = Unmarshal.UnmarshalUnityObject<Shader>(intPtr);
				char* ptr = null;
			}
			return shader;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00020564 File Offset: 0x0001E764
		[NativeThrows]
		[FreeFunction("Resources_Bindings::Load")]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		public unsafe static Object Load(string path, [NotNull] Type systemTypeInstance)
		{
			if (systemTypeInstance == null)
			{
				ThrowHelper.ThrowArgumentNullException(systemTypeInstance, "systemTypeInstance");
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
				IntPtr intPtr = ResourcesAPIInternal.Load_Injected(ref managedSpanWrapper, systemTypeInstance);
			}
			finally
			{
				IntPtr intPtr;
				@object = Unmarshal.UnmarshalUnityObject<Object>(intPtr);
				char* ptr = null;
			}
			return @object;
		}

		// Token: 0x06000F58 RID: 3928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindShaderByName_Injected(ref ManagedSpanWrapper name);

		// Token: 0x06000F59 RID: 3929
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Load_Injected(ref ManagedSpanWrapper path, Type systemTypeInstance);
	}
}
