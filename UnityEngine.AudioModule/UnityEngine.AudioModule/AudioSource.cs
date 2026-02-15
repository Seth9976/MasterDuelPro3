using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Audio;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	[RequireComponent(typeof(Transform))]
	[StaticAccessor("AudioSourceBindings", StaticAccessorType.DoubleColon)]
	public sealed class AudioSource : AudioBehaviour
	{
		// Token: 0x06000051 RID: 81 RVA: 0x000029D0 File Offset: 0x00000BD0
		private static float GetPitch([NotNull] AudioSource source)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			return AudioSource.GetPitch_Injected(intPtr);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A08 File Offset: 0x00000C08
		private static void SetPitch([NotNull] AudioSource source, float pitch)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			AudioSource.SetPitch_Injected(intPtr, pitch);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A40 File Offset: 0x00000C40
		private static void PlayHelper([NotNull] AudioSource source, ulong delay)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			AudioSource.PlayHelper_Injected(intPtr, delay);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A78 File Offset: 0x00000C78
		private void Play(double delay)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.Play_Injected(intPtr, delay);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A9C File Offset: 0x00000C9C
		private static void PlayOneShotHelper([NotNull] AudioSource source, [NotNull] AudioClip clip, float volumeScale)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			if (clip == null)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(clip);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			AudioSource.PlayOneShotHelper_Injected(intPtr, intPtr2, volumeScale);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002AF8 File Offset: 0x00000CF8
		private void Stop(bool stopOneShots)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.Stop_Injected(intPtr, stopOneShots);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B1C File Offset: 0x00000D1C
		[NativeThrows]
		private static void SetCustomCurveHelper([NotNull] AudioSource source, AudioSourceCurveType type, AnimationCurve curve)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			AudioSource.SetCustomCurveHelper_Injected(intPtr, type, (curve == null) ? ((IntPtr)0) : AnimationCurve.BindingsMarshaller.ConvertToNative(curve));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B64 File Offset: 0x00000D64
		private static AnimationCurve GetCustomCurveHelper([NotNull] AudioSource source, AudioSourceCurveType type)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr customCurveHelper_Injected = AudioSource.GetCustomCurveHelper_Injected(intPtr, type);
			return (customCurveHelper_Injected == 0) ? null : AnimationCurve.BindingsMarshaller.ConvertToManaged(customCurveHelper_Injected);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002BAC File Offset: 0x00000DAC
		private unsafe static void GetOutputDataHelper([NotNull] AudioSource source, [Out] float[] samples, int channel)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(source, "source");
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (samples != null)
				{
					fixed (float[] array = samples)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				AudioSource.GetOutputDataHelper_Injected(intPtr, out blittableArrayWrapper, channel);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C20 File Offset: 0x00000E20
		[NativeThrows]
		private unsafe static void GetSpectrumDataHelper([NotNull] AudioSource source, [Out] float[] samples, int channel, FFTWindow window)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(source);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(source, "source");
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (samples != null)
				{
					fixed (float[] array = samples)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				AudioSource.GetSpectrumDataHelper_Injected(intPtr, out blittableArrayWrapper, channel, window);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002C94 File Offset: 0x00000E94
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public float volume
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_volume_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_volume_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002CDC File Offset: 0x00000EDC
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public float pitch
		{
			get
			{
				return AudioSource.GetPitch(this);
			}
			set
			{
				AudioSource.SetPitch(this, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002D00 File Offset: 0x00000F00
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002D24 File Offset: 0x00000F24
		[NativeProperty("SecPosition")]
		public float time
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_time_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_time_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002D48 File Offset: 0x00000F48
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002D6C File Offset: 0x00000F6C
		[NativeProperty("SamplePosition")]
		public int timeSamples
		{
			[NativeMethod(IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_timeSamples_Injected(intPtr);
			}
			[NativeMethod(IsThreadSafe = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_timeSamples_Injected(intPtr, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002D8F File Offset: 0x00000F8F
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002D9C File Offset: 0x00000F9C
		public AudioClip clip
		{
			get
			{
				return this.resource as AudioClip;
			}
			set
			{
				this.resource = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002DA8 File Offset: 0x00000FA8
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public AudioResource resource
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AudioResource>(AudioSource.get_resource_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_resource_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AudioResource>(value));
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002DF8 File Offset: 0x00000FF8
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002E20 File Offset: 0x00001020
		public AudioMixerGroup outputAudioMixerGroup
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AudioMixerGroup>(AudioSource.get_outputAudioMixerGroup_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_outputAudioMixerGroup_Injected(intPtr, Object.MarshalledUnityObject.Marshal<AudioMixerGroup>(value));
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E48 File Offset: 0x00001048
		[ExcludeFromDocs]
		public void Play()
		{
			AudioSource.PlayHelper(this, 0UL);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E54 File Offset: 0x00001054
		public void Play([DefaultValue("0")] ulong delay)
		{
			AudioSource.PlayHelper(this, delay);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002E5F File Offset: 0x0000105F
		public void PlayDelayed(float delay)
		{
			this.Play((delay < 0f) ? 0.0 : (-(double)delay));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002E7F File Offset: 0x0000107F
		public void PlayScheduled(double time)
		{
			this.Play((time < 0.0) ? 0.0 : time);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002EA1 File Offset: 0x000010A1
		[ExcludeFromDocs]
		public void PlayOneShot(AudioClip clip)
		{
			this.PlayOneShot(clip, 1f);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002EB4 File Offset: 0x000010B4
		public void PlayOneShot(AudioClip clip, [DefaultValue("1.0F")] float volumeScale)
		{
			bool flag = clip == null;
			if (flag)
			{
				Debug.LogWarning("PlayOneShot was called with a null AudioClip.");
			}
			else
			{
				AudioSource.PlayOneShotHelper(this, clip, volumeScale);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002EE4 File Offset: 0x000010E4
		public void SetScheduledStartTime(double time)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.SetScheduledStartTime_Injected(intPtr, time);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F08 File Offset: 0x00001108
		public void SetScheduledEndTime(double time)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.SetScheduledEndTime_Injected(intPtr, time);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002F2B File Offset: 0x0000112B
		public void Stop()
		{
			this.Stop(true);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002F38 File Offset: 0x00001138
		public void Pause()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.Pause_Injected(intPtr);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F5C File Offset: 0x0000115C
		public void UnPause()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.UnPause_Injected(intPtr);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F80 File Offset: 0x00001180
		internal void SkipToNextElementIfHasContainer()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioSource.SkipToNextElementIfHasContainer_Injected(intPtr);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002FA4 File Offset: 0x000011A4
		public bool isPlaying
		{
			[NativeName("IsPlayingScripting")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_isPlaying_Injected(intPtr);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002FC8 File Offset: 0x000011C8
		internal bool isContainerPlaying
		{
			[NativeName("IsContainerPlaying")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_isContainerPlaying_Injected(intPtr);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002FEC File Offset: 0x000011EC
		internal ActivePlayable[] containerActivePlayables
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_containerActivePlayables_Injected(intPtr);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003010 File Offset: 0x00001210
		public bool isVirtual
		{
			[NativeName("GetLastVirtualState")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_isVirtual_Injected(intPtr);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003032 File Offset: 0x00001232
		[ExcludeFromDocs]
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position)
		{
			AudioSource.PlayClipAtPoint(clip, position, 1f);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003044 File Offset: 0x00001244
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position, [DefaultValue("1.0F")] float volume)
		{
			GameObject go = new GameObject("One shot audio");
			go.transform.position = position;
			AudioSource source = (AudioSource)go.AddComponent(typeof(AudioSource));
			source.clip = clip;
			source.spatialBlend = 1f;
			source.volume = volume;
			source.Play();
			Object.Destroy(go, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000030CC File Offset: 0x000012CC
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000030F0 File Offset: 0x000012F0
		public bool loop
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_loop_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_loop_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003114 File Offset: 0x00001314
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003138 File Offset: 0x00001338
		public bool ignoreListenerVolume
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_ignoreListenerVolume_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_ignoreListenerVolume_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000315C File Offset: 0x0000135C
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003180 File Offset: 0x00001380
		public bool playOnAwake
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_playOnAwake_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_playOnAwake_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000031A4 File Offset: 0x000013A4
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000031C8 File Offset: 0x000013C8
		public bool ignoreListenerPause
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_ignoreListenerPause_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_ignoreListenerPause_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000031EC File Offset: 0x000013EC
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003210 File Offset: 0x00001410
		public AudioVelocityUpdateMode velocityUpdateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_velocityUpdateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_velocityUpdateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003234 File Offset: 0x00001434
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003258 File Offset: 0x00001458
		[NativeProperty("StereoPan")]
		public float panStereo
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_panStereo_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_panStereo_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000327C File Offset: 0x0000147C
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000032A0 File Offset: 0x000014A0
		[NativeProperty("SpatialBlendMix")]
		public float spatialBlend
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_spatialBlend_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_spatialBlend_Injected(intPtr, value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000032C4 File Offset: 0x000014C4
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000032E8 File Offset: 0x000014E8
		public bool spatialize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_spatialize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_spatialize_Injected(intPtr, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000330C File Offset: 0x0000150C
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003330 File Offset: 0x00001530
		public bool spatializePostEffects
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_spatializePostEffects_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_spatializePostEffects_Injected(intPtr, value);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003353 File Offset: 0x00001553
		public void SetCustomCurve(AudioSourceCurveType type, AnimationCurve curve)
		{
			AudioSource.SetCustomCurveHelper(this, type, curve);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003360 File Offset: 0x00001560
		public AnimationCurve GetCustomCurve(AudioSourceCurveType type)
		{
			return AudioSource.GetCustomCurveHelper(this, type);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000337C File Offset: 0x0000157C
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000033A0 File Offset: 0x000015A0
		public float reverbZoneMix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_reverbZoneMix_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_reverbZoneMix_Injected(intPtr, value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000033C4 File Offset: 0x000015C4
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000033E8 File Offset: 0x000015E8
		public bool bypassEffects
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_bypassEffects_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_bypassEffects_Injected(intPtr, value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000340C File Offset: 0x0000160C
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003430 File Offset: 0x00001630
		public bool bypassListenerEffects
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_bypassListenerEffects_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_bypassListenerEffects_Injected(intPtr, value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003454 File Offset: 0x00001654
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003478 File Offset: 0x00001678
		public bool bypassReverbZones
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_bypassReverbZones_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_bypassReverbZones_Injected(intPtr, value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000349C File Offset: 0x0000169C
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000034C0 File Offset: 0x000016C0
		public float dopplerLevel
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_dopplerLevel_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_dopplerLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000034E4 File Offset: 0x000016E4
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003508 File Offset: 0x00001708
		public float spread
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_spread_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_spread_Injected(intPtr, value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000352C File Offset: 0x0000172C
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003550 File Offset: 0x00001750
		public int priority
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_priority_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_priority_Injected(intPtr, value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003574 File Offset: 0x00001774
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003598 File Offset: 0x00001798
		public bool mute
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_mute_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_mute_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000035BC File Offset: 0x000017BC
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000035E0 File Offset: 0x000017E0
		public float minDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_minDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_minDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003604 File Offset: 0x00001804
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003628 File Offset: 0x00001828
		public float maxDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_maxDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_maxDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000364C File Offset: 0x0000184C
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003670 File Offset: 0x00001870
		public AudioRolloffMode rolloffMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioSource.get_rolloffMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioSource.set_rolloffMode_Injected(intPtr, value);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003694 File Offset: 0x00001894
		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public float[] GetOutputData(int numSamples, int channel)
		{
			float[] samples = new float[numSamples];
			AudioSource.GetOutputDataHelper(this, samples, channel);
			return samples;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000036B7 File Offset: 0x000018B7
		public void GetOutputData(float[] samples, int channel)
		{
			AudioSource.GetOutputDataHelper(this, samples, channel);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000036C4 File Offset: 0x000018C4
		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData and pass a pre allocated array instead.")]
		public float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] samples = new float[numSamples];
			AudioSource.GetSpectrumDataHelper(this, samples, channel, window);
			return samples;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000036E8 File Offset: 0x000018E8
		public void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			AudioSource.GetSpectrumDataHelper(this, samples, channel, window);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000036F8 File Offset: 0x000018F8
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000371A File Offset: 0x0000191A
		[Obsolete("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float minVolume
		{
			get
			{
				Debug.LogError("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003728 File Offset: 0x00001928
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000374A File Offset: 0x0000194A
		[Obsolete("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float maxVolume
		{
			get
			{
				Debug.LogError("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003758 File Offset: 0x00001958
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000377A File Offset: 0x0000197A
		[Obsolete("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float rolloffFactor
		{
			get
			{
				Debug.LogError("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003788 File Offset: 0x00001988
		public bool SetSpatializerFloat(int index, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioSource.SetSpatializerFloat_Injected(intPtr, index, value);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000037AC File Offset: 0x000019AC
		public bool GetSpatializerFloat(int index, out float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioSource.GetSpatializerFloat_Injected(intPtr, index, out value);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000037D0 File Offset: 0x000019D0
		public bool GetAmbisonicDecoderFloat(int index, out float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioSource.GetAmbisonicDecoderFloat_Injected(intPtr, index, out value);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000037F4 File Offset: 0x000019F4
		public bool SetAmbisonicDecoderFloat(int index, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioSource.SetAmbisonicDecoderFloat_Injected(intPtr, index, value);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003818 File Offset: 0x00001A18
		internal float GetAudioRandomContainerRuntimeMeterValue()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioSource>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioSource.GetAudioRandomContainerRuntimeMeterValue_Injected(intPtr);
		}

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetPitch_Injected(IntPtr source);

		// Token: 0x060000B6 RID: 182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPitch_Injected(IntPtr source, float pitch);

		// Token: 0x060000B7 RID: 183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayHelper_Injected(IntPtr source, ulong delay);

		// Token: 0x060000B8 RID: 184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(IntPtr _unity_self, double delay);

		// Token: 0x060000B9 RID: 185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayOneShotHelper_Injected(IntPtr source, IntPtr clip, float volumeScale);

		// Token: 0x060000BA RID: 186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self, bool stopOneShots);

		// Token: 0x060000BB RID: 187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomCurveHelper_Injected(IntPtr source, AudioSourceCurveType type, IntPtr curve);

		// Token: 0x060000BC RID: 188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCustomCurveHelper_Injected(IntPtr source, AudioSourceCurveType type);

		// Token: 0x060000BD RID: 189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputDataHelper_Injected(IntPtr source, out BlittableArrayWrapper samples, int channel);

		// Token: 0x060000BE RID: 190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpectrumDataHelper_Injected(IntPtr source, out BlittableArrayWrapper samples, int channel, FFTWindow window);

		// Token: 0x060000BF RID: 191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_volume_Injected(IntPtr _unity_self);

		// Token: 0x060000C0 RID: 192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_volume_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000C1 RID: 193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_time_Injected(IntPtr _unity_self);

		// Token: 0x060000C2 RID: 194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_time_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_timeSamples_Injected(IntPtr _unity_self);

		// Token: 0x060000C4 RID: 196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_timeSamples_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_resource_Injected(IntPtr _unity_self);

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_resource_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060000C7 RID: 199
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_outputAudioMixerGroup_Injected(IntPtr _unity_self);

		// Token: 0x060000C8 RID: 200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_outputAudioMixerGroup_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060000C9 RID: 201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScheduledStartTime_Injected(IntPtr _unity_self, double time);

		// Token: 0x060000CA RID: 202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScheduledEndTime_Injected(IntPtr _unity_self, double time);

		// Token: 0x060000CB RID: 203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Pause_Injected(IntPtr _unity_self);

		// Token: 0x060000CC RID: 204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnPause_Injected(IntPtr _unity_self);

		// Token: 0x060000CD RID: 205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SkipToNextElementIfHasContainer_Injected(IntPtr _unity_self);

		// Token: 0x060000CE RID: 206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPlaying_Injected(IntPtr _unity_self);

		// Token: 0x060000CF RID: 207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isContainerPlaying_Injected(IntPtr _unity_self);

		// Token: 0x060000D0 RID: 208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ActivePlayable[] get_containerActivePlayables_Injected(IntPtr _unity_self);

		// Token: 0x060000D1 RID: 209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isVirtual_Injected(IntPtr _unity_self);

		// Token: 0x060000D2 RID: 210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_loop_Injected(IntPtr _unity_self);

		// Token: 0x060000D3 RID: 211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loop_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000D4 RID: 212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_ignoreListenerVolume_Injected(IntPtr _unity_self);

		// Token: 0x060000D5 RID: 213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ignoreListenerVolume_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000D6 RID: 214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_playOnAwake_Injected(IntPtr _unity_self);

		// Token: 0x060000D7 RID: 215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_playOnAwake_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000D8 RID: 216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_ignoreListenerPause_Injected(IntPtr _unity_self);

		// Token: 0x060000D9 RID: 217
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_ignoreListenerPause_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000DA RID: 218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioVelocityUpdateMode get_velocityUpdateMode_Injected(IntPtr _unity_self);

		// Token: 0x060000DB RID: 219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_velocityUpdateMode_Injected(IntPtr _unity_self, AudioVelocityUpdateMode value);

		// Token: 0x060000DC RID: 220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_panStereo_Injected(IntPtr _unity_self);

		// Token: 0x060000DD RID: 221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_panStereo_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000DE RID: 222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_spatialBlend_Injected(IntPtr _unity_self);

		// Token: 0x060000DF RID: 223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spatialBlend_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000E0 RID: 224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_spatialize_Injected(IntPtr _unity_self);

		// Token: 0x060000E1 RID: 225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spatialize_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000E2 RID: 226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_spatializePostEffects_Injected(IntPtr _unity_self);

		// Token: 0x060000E3 RID: 227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spatializePostEffects_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000E4 RID: 228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_reverbZoneMix_Injected(IntPtr _unity_self);

		// Token: 0x060000E5 RID: 229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reverbZoneMix_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000E6 RID: 230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_bypassEffects_Injected(IntPtr _unity_self);

		// Token: 0x060000E7 RID: 231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bypassEffects_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000E8 RID: 232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_bypassListenerEffects_Injected(IntPtr _unity_self);

		// Token: 0x060000E9 RID: 233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bypassListenerEffects_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000EA RID: 234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_bypassReverbZones_Injected(IntPtr _unity_self);

		// Token: 0x060000EB RID: 235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bypassReverbZones_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000EC RID: 236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_dopplerLevel_Injected(IntPtr _unity_self);

		// Token: 0x060000ED RID: 237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dopplerLevel_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000EE RID: 238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_spread_Injected(IntPtr _unity_self);

		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_spread_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_priority_Injected(IntPtr _unity_self);

		// Token: 0x060000F1 RID: 241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_priority_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000F2 RID: 242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_mute_Injected(IntPtr _unity_self);

		// Token: 0x060000F3 RID: 243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mute_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000F4 RID: 244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_minDistance_Injected(IntPtr _unity_self);

		// Token: 0x060000F5 RID: 245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_minDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000F6 RID: 246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxDistance_Injected(IntPtr _unity_self);

		// Token: 0x060000F7 RID: 247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000F8 RID: 248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioRolloffMode get_rolloffMode_Injected(IntPtr _unity_self);

		// Token: 0x060000F9 RID: 249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rolloffMode_Injected(IntPtr _unity_self, AudioRolloffMode value);

		// Token: 0x060000FA RID: 250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetSpatializerFloat_Injected(IntPtr _unity_self, int index, float value);

		// Token: 0x060000FB RID: 251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetSpatializerFloat_Injected(IntPtr _unity_self, int index, out float value);

		// Token: 0x060000FC RID: 252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetAmbisonicDecoderFloat_Injected(IntPtr _unity_self, int index, out float value);

		// Token: 0x060000FD RID: 253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetAmbisonicDecoderFloat_Injected(IntPtr _unity_self, int index, float value);

		// Token: 0x060000FE RID: 254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAudioRandomContainerRuntimeMeterValue_Injected(IntPtr _unity_self);
	}
}
