using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Audio
{
	// Token: 0x02000015 RID: 21
	[RequiredByNativeCode]
	[StaticAccessor("AudioClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Audio/Public/Director/AudioClipPlayable.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioClipPlayable.bindings.h")]
	public struct AudioClipPlayable : IPlayable, IEquatable<AudioClipPlayable>
	{
		// Token: 0x06000104 RID: 260 RVA: 0x0000389C File Offset: 0x00001A9C
		public static AudioClipPlayable Create(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle handle = AudioClipPlayable.CreateHandle(graph, clip, looping);
			AudioClipPlayable playable = new AudioClipPlayable(handle);
			bool flag = clip != null;
			if (flag)
			{
				playable.SetDuration((double)clip.length);
			}
			return playable;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000038DC File Offset: 0x00001ADC
		private static PlayableHandle CreateHandle(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle handle = PlayableHandle.Null;
			bool flag = !AudioClipPlayable.InternalCreateAudioClipPlayable(ref graph, clip, looping, ref handle);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				playableHandle = handle;
			}
			return playableHandle;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003910 File Offset: 0x00001B10
		internal AudioClipPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AudioClipPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AudioClipPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000394C File Offset: 0x00001B4C
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003964 File Offset: 0x00001B64
		public static implicit operator Playable(AudioClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003984 File Offset: 0x00001B84
		public static explicit operator AudioClipPlayable(Playable playable)
		{
			return new AudioClipPlayable(playable.GetHandle());
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000039A4 File Offset: 0x00001BA4
		public bool Equals(AudioClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000039C8 File Offset: 0x00001BC8
		internal void SetVolume(float value)
		{
			bool flag = value < 0f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable volume outside of range (0.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetVolumeInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003A14 File Offset: 0x00001C14
		internal void SetStereoPan(float value)
		{
			bool flag = value < -1f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable stereo pan outside of range (-1.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetStereoPanInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00003A60 File Offset: 0x00001C60
		internal void SetSpatialBlend(float value)
		{
			bool flag = value < 0f || value > 1f;
			if (flag)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable spatial blend outside of range (0.0 - 1.0): " + value.ToString());
			}
			AudioClipPlayable.SetSpatialBlendInternal(ref this.m_Handle, value);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003AAC File Offset: 0x00001CAC
		public void Seek(double startTime, double startDelay, [DefaultValue("0")] double duration)
		{
			AudioClipPlayable.SetStartDelayInternal(ref this.m_Handle, startDelay);
			bool flag = duration > 0.0;
			if (flag)
			{
				double seekTime = startDelay + duration;
				bool flag2 = seekTime >= this.m_Handle.GetDuration();
				if (flag2)
				{
					this.m_Handle.SetDone(true);
				}
				this.m_Handle.SetDuration(duration + startTime);
				AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, startDelay + duration);
			}
			else
			{
				this.m_Handle.SetDone(true);
				this.m_Handle.SetDuration(double.MaxValue);
				AudioClipPlayable.SetPauseDelayInternal(ref this.m_Handle, 0.0);
			}
			this.m_Handle.SetTime(startTime);
			this.m_Handle.Play();
		}

		// Token: 0x0600010F RID: 271
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVolumeInternal(ref PlayableHandle hdl, float volume);

		// Token: 0x06000110 RID: 272
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStereoPanInternal(ref PlayableHandle hdl, float stereoPan);

		// Token: 0x06000111 RID: 273
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSpatialBlendInternal(ref PlayableHandle hdl, float spatialBlend);

		// Token: 0x06000112 RID: 274
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetStartDelayInternal(ref PlayableHandle hdl, double delay);

		// Token: 0x06000113 RID: 275
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPauseDelayInternal(ref PlayableHandle hdl, double delay);

		// Token: 0x06000114 RID: 276 RVA: 0x00003B74 File Offset: 0x00001D74
		[NativeThrows]
		private static bool InternalCreateAudioClipPlayable(ref PlayableGraph graph, AudioClip clip, bool looping, ref PlayableHandle handle)
		{
			return AudioClipPlayable.InternalCreateAudioClipPlayable_Injected(ref graph, Object.MarshalledUnityObject.Marshal<AudioClip>(clip), looping, ref handle);
		}

		// Token: 0x06000115 RID: 277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateAudioClipPlayable_Injected(ref PlayableGraph graph, IntPtr clip, bool looping, ref PlayableHandle handle);

		// Token: 0x0400002C RID: 44
		private PlayableHandle m_Handle;
	}
}
