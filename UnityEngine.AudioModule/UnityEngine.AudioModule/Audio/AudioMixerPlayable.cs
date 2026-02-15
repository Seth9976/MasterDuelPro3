using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	// Token: 0x02000018 RID: 24
	[StaticAccessor("AudioMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioMixerPlayable.bindings.h")]
	[NativeHeader("Modules/Audio/Public/Director/AudioMixerPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[RequiredByNativeCode]
	public struct AudioMixerPlayable : IPlayable, IEquatable<AudioMixerPlayable>
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00003C60 File Offset: 0x00001E60
		public static AudioMixerPlayable Create(PlayableGraph graph, int inputCount = 0, bool normalizeInputVolumes = false)
		{
			PlayableHandle handle = AudioMixerPlayable.CreateHandle(graph, inputCount, normalizeInputVolumes);
			return new AudioMixerPlayable(handle);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003C84 File Offset: 0x00001E84
		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount, bool normalizeInputVolumes)
		{
			PlayableHandle handle = PlayableHandle.Null;
			bool flag = !AudioMixerPlayable.CreateAudioMixerPlayableInternal(ref graph, normalizeInputVolumes, ref handle);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				handle.SetInputCount(inputCount);
				playableHandle = handle;
			}
			return playableHandle;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003CC0 File Offset: 0x00001EC0
		internal AudioMixerPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AudioMixerPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioMixerPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003CFC File Offset: 0x00001EFC
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003D14 File Offset: 0x00001F14
		public static implicit operator Playable(AudioMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003D34 File Offset: 0x00001F34
		public bool Equals(AudioMixerPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x06000120 RID: 288
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateAudioMixerPlayableInternal(ref PlayableGraph graph, bool normalizeInputVolumes, ref PlayableHandle handle);

		// Token: 0x0400002D RID: 45
		private PlayableHandle m_Handle;
	}
}
