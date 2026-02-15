using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Audio
{
	// Token: 0x0200001A RID: 26
	[StaticAccessor("AudioPlayableGraphExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioPlayableGraphExtensions.bindings.h")]
	internal static class AudioPlayableGraphExtensions
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00003DA8 File Offset: 0x00001FA8
		[NativeThrows]
		internal unsafe static bool InternalCreateAudioOutput(ref PlayableGraph graph, string name, out PlayableOutputHandle handle)
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
				flag = AudioPlayableGraphExtensions.InternalCreateAudioOutput_Injected(ref graph, ref managedSpanWrapper, out handle);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000124 RID: 292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateAudioOutput_Injected(ref PlayableGraph graph, ref ManagedSpanWrapper name, out PlayableOutputHandle handle);
	}
}
