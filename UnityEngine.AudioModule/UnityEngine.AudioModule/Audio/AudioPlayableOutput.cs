using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	// Token: 0x0200001B RID: 27
	[RequiredByNativeCode]
	[NativeHeader("Modules/Audio/Public/Director/AudioPlayableOutput.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioPlayableOutput.bindings.h")]
	[StaticAccessor("AudioPlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Audio/Public/AudioSource.h")]
	public struct AudioPlayableOutput : IPlayableOutput
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00003E00 File Offset: 0x00002000
		public static AudioPlayableOutput Create(PlayableGraph graph, string name, AudioSource target)
		{
			PlayableOutputHandle handle;
			bool flag = !AudioPlayableGraphExtensions.InternalCreateAudioOutput(ref graph, name, out handle);
			AudioPlayableOutput audioPlayableOutput;
			if (flag)
			{
				audioPlayableOutput = AudioPlayableOutput.Null;
			}
			else
			{
				AudioPlayableOutput output = new AudioPlayableOutput(handle);
				output.SetTarget(target);
				audioPlayableOutput = output;
			}
			return audioPlayableOutput;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003E40 File Offset: 0x00002040
		internal AudioPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<AudioPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00003E7C File Offset: 0x0000207C
		public static AudioPlayableOutput Null
		{
			get
			{
				return new AudioPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003E98 File Offset: 0x00002098
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003EB0 File Offset: 0x000020B0
		public static implicit operator PlayableOutput(AudioPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003ED0 File Offset: 0x000020D0
		public static explicit operator AudioPlayableOutput(PlayableOutput output)
		{
			return new AudioPlayableOutput(output.GetHandle());
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003EEE File Offset: 0x000020EE
		public void SetTarget(AudioSource value)
		{
			AudioPlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003EFE File Offset: 0x000020FE
		public void SetEvaluateOnSeek(bool value)
		{
			AudioPlayableOutput.InternalSetEvaluateOnSeek(ref this.m_Handle, value);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003F10 File Offset: 0x00002110
		[NativeThrows]
		private static void InternalSetTarget(ref PlayableOutputHandle output, AudioSource target)
		{
			AudioPlayableOutput.InternalSetTarget_Injected(ref output, Object.MarshalledUnityObject.Marshal<AudioSource>(target));
		}

		// Token: 0x0600012E RID: 302
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetEvaluateOnSeek(ref PlayableOutputHandle output, bool value);

		// Token: 0x0600012F RID: 303
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetTarget_Injected(ref PlayableOutputHandle output, IntPtr target);

		// Token: 0x0400002E RID: 46
		private PlayableOutputHandle m_Handle;
	}
}
