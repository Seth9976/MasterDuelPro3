using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	// Token: 0x0200024E RID: 590
	[NativeHeader("Runtime/Export/SceneManager/SceneManager.bindings.h")]
	[NativeHeader("Runtime/SceneManager/SceneManager.h")]
	[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
	internal static class SceneManagerAPIInternal
	{
		// Token: 0x060014BA RID: 5306 RVA: 0x0002BCBC File Offset: 0x00029EBC
		[NativeThrows]
		public unsafe static AsyncOperation LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			AsyncOperation asyncOperation;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(sceneName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = sceneName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = SceneManagerAPIInternal.LoadSceneAsyncNameIndexInternal_Injected(ref managedSpanWrapper, sceneBuildIndex, ref parameters, mustCompleteNextFrame);
			}
			finally
			{
				IntPtr intPtr;
				IntPtr intPtr2 = intPtr;
				asyncOperation = ((intPtr2 == 0) ? null : AsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr2));
				char* ptr = null;
			}
			return asyncOperation;
		}

		// Token: 0x060014BB RID: 5307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadSceneAsyncNameIndexInternal_Injected(ref ManagedSpanWrapper sceneName, int sceneBuildIndex, [In] ref LoadSceneParameters parameters, bool mustCompleteNextFrame);
	}
}
