using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x02000034 RID: 52
	[StaticAccessor("AnimationPlayableGraphExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableGraphExtensions.bindings.h")]
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	internal static class AnimationPlayableGraphExtensions
	{
		// Token: 0x06000282 RID: 642 RVA: 0x000060F0 File Offset: 0x000042F0
		[NativeThrows]
		internal unsafe static bool InternalCreateAnimationOutput(ref PlayableGraph graph, string name, out PlayableOutputHandle handle)
		{
			bool flag;
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
				flag = AnimationPlayableGraphExtensions.InternalCreateAnimationOutput_Injected(ref graph, ref managedSpanWrapper, out handle);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000283 RID: 643
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateAnimationOutput_Injected(ref PlayableGraph graph, ref ManagedSpanWrapper name, out PlayableOutputHandle handle);
	}
}
