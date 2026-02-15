using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Audio;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[StaticAccessor("AudioClipBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/Audio.bindings.h")]
	public sealed class AudioClip : AudioResource
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002104 File Offset: 0x00000304
		private AudioClip()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000211C File Offset: 0x0000031C
		private unsafe static bool GetData([NotNull] AudioClip clip, Span<float> data, int samplesOffset)
		{
			if (clip == null)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(clip);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			Span<float> span = data;
			bool data_Injected;
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				data_Injected = AudioClip.GetData_Injected(intPtr, ref managedSpanWrapper, samplesOffset);
			}
			return data_Injected;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002174 File Offset: 0x00000374
		private unsafe static bool SetData([NotNull] AudioClip clip, ReadOnlySpan<float> data, int samplesOffset)
		{
			if (clip == null)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(clip);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(clip, "clip");
			}
			ReadOnlySpan<float> readOnlySpan = data;
			bool flag;
			fixed (float* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				flag = AudioClip.SetData_Injected(intPtr, ref managedSpanWrapper, samplesOffset);
			}
			return flag;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021CC File Offset: 0x000003CC
		private static AudioClip Construct_Internal()
		{
			return Unmarshal.UnmarshalUnityObject<AudioClip>(AudioClip.Construct_Internal_Injected());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E4 File Offset: 0x000003E4
		private string GetName()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				AudioClip.GetName_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002224 File Offset: 0x00000424
		private unsafe void CreateUserSound(string name, int lengthSamples, int channels, int frequency, bool stream)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
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
				AudioClip.CreateUserSound_Injected(intPtr, ref managedSpanWrapper, lengthSamples, channels, frequency, stream);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002290 File Offset: 0x00000490
		[NativeProperty("LengthSec")]
		public float length
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_length_Injected(intPtr);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022B4 File Offset: 0x000004B4
		[NativeProperty("SampleCount")]
		public int samples
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_samples_Injected(intPtr);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022D8 File Offset: 0x000004D8
		[NativeProperty("ChannelCount")]
		public int channels
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_channels_Injected(intPtr);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022FC File Offset: 0x000004FC
		public int frequency
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_frequency_Injected(intPtr);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002320 File Offset: 0x00000520
		[Obsolete("Use AudioClip.loadState instead to get more detailed information about the loading process.")]
		public bool isReadyToPlay
		{
			[NativeName("ReadyToPlay")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_isReadyToPlay_Injected(intPtr);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002344 File Offset: 0x00000544
		public AudioClipLoadType loadType
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_loadType_Injected(intPtr);
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002368 File Offset: 0x00000568
		public bool LoadAudioData()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioClip.LoadAudioData_Injected(intPtr);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000238C File Offset: 0x0000058C
		public bool UnloadAudioData()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AudioClip.UnloadAudioData_Injected(intPtr);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023B0 File Offset: 0x000005B0
		public bool preloadAudioData
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_preloadAudioData_Injected(intPtr);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023D4 File Offset: 0x000005D4
		public bool ambisonic
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_ambisonic_Injected(intPtr);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023F8 File Offset: 0x000005F8
		public bool loadInBackground
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_loadInBackground_Injected(intPtr);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000241C File Offset: 0x0000061C
		public AudioDataLoadState loadState
		{
			[NativeMethod(Name = "AudioClipBindings::GetLoadState", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioClip.get_loadState_Injected(intPtr);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002440 File Offset: 0x00000640
		public bool GetData(Span<float> data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool flag2;
			if (flag)
			{
				Debug.Log("AudioClip.GetData failed; AudioClip " + this.GetName() + " contains no data");
				flag2 = false;
			}
			else
			{
				flag2 = AudioClip.GetData(this, data, offsetSamples);
			}
			return flag2;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000248C File Offset: 0x0000068C
		public bool GetData(float[] data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool flag2;
			if (flag)
			{
				Debug.Log("AudioClip.GetData failed; AudioClip " + this.GetName() + " contains no data");
				flag2 = false;
			}
			else
			{
				flag2 = AudioClip.GetData(this, data.AsSpan<float>(), offsetSamples);
			}
			return flag2;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024DC File Offset: 0x000006DC
		public bool SetData(float[] data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool flag2;
			if (flag)
			{
				Debug.Log("AudioClip.SetData failed; AudioClip " + this.GetName() + " contains no data");
				flag2 = false;
			}
			else
			{
				bool flag3 = offsetSamples < 0 || offsetSamples >= this.samples;
				if (flag3)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid offsetSamples");
				}
				bool flag4 = data == null || data.Length == 0;
				if (flag4)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid data");
				}
				flag2 = AudioClip.SetData(this, data.AsSpan<float>(), offsetSamples);
			}
			return flag2;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000256C File Offset: 0x0000076C
		public bool SetData(ReadOnlySpan<float> data, int offsetSamples)
		{
			bool flag = this.channels <= 0;
			bool flag2;
			if (flag)
			{
				Debug.Log("AudioClip.SetData failed; AudioClip " + this.GetName() + " contains no data");
				flag2 = false;
			}
			else
			{
				bool flag3 = offsetSamples < 0 || offsetSamples >= this.samples;
				if (flag3)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid offsetSamples");
				}
				bool flag4 = data.Length == 0;
				if (flag4)
				{
					throw new ArgumentException("AudioClip.SetData failed; invalid data");
				}
				flag2 = AudioClip.SetData(this, data, offsetSamples);
			}
			return flag2;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025F4 File Offset: 0x000007F4
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002614 File Offset: 0x00000814
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002634 File Offset: 0x00000834
		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, pcmsetpositioncallback);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002658 File Offset: 0x00000858
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, null, null);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000267C File Offset: 0x0000087C
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback)
		{
			return AudioClip.Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026A0 File Offset: 0x000008A0
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, AudioClip.PCMReaderCallback pcmreadercallback, AudioClip.PCMSetPositionCallback pcmsetpositioncallback)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = lengthSamples <= 0;
			if (flag2)
			{
				throw new ArgumentException("Length of created clip must be larger than 0");
			}
			bool flag3 = channels <= 0;
			if (flag3)
			{
				throw new ArgumentException("Number of channels in created clip must be greater than 0");
			}
			bool flag4 = frequency <= 0;
			if (flag4)
			{
				throw new ArgumentException("Frequency in created clip must be greater than 0");
			}
			AudioClip clip = AudioClip.Construct_Internal();
			bool flag5 = pcmreadercallback != null;
			if (flag5)
			{
				clip.m_PCMReaderCallback += pcmreadercallback;
			}
			bool flag6 = pcmsetpositioncallback != null;
			if (flag6)
			{
				clip.m_PCMSetPositionCallback += pcmsetpositioncallback;
			}
			clip.CreateUserSound(name, lengthSamples, channels, frequency, stream);
			return clip;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000024 RID: 36 RVA: 0x00002744 File Offset: 0x00000944
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x0000277C File Offset: 0x0000097C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event AudioClip.PCMReaderCallback m_PCMReaderCallback = null;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000026 RID: 38 RVA: 0x000027B4 File Offset: 0x000009B4
		// (remove) Token: 0x06000027 RID: 39 RVA: 0x000027EC File Offset: 0x000009EC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event AudioClip.PCMSetPositionCallback m_PCMSetPositionCallback = null;

		// Token: 0x06000028 RID: 40 RVA: 0x00002824 File Offset: 0x00000A24
		[RequiredByNativeCode]
		private void InvokePCMReaderCallback_Internal(float[] data)
		{
			bool flag = this.m_PCMReaderCallback != null;
			if (flag)
			{
				this.m_PCMReaderCallback(data);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000284C File Offset: 0x00000A4C
		[RequiredByNativeCode]
		private void InvokePCMSetPositionCallback_Internal(int position)
		{
			bool flag = this.m_PCMSetPositionCallback != null;
			if (flag)
			{
				this.m_PCMSetPositionCallback(position);
			}
		}

		// Token: 0x0600002A RID: 42
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetData_Injected(IntPtr clip, ref ManagedSpanWrapper data, int samplesOffset);

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetData_Injected(IntPtr clip, ref ManagedSpanWrapper data, int samplesOffset);

		// Token: 0x0600002C RID: 44
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Construct_Internal_Injected();

		// Token: 0x0600002D RID: 45
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600002E RID: 46
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateUserSound_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, int lengthSamples, int channels, int frequency, bool stream);

		// Token: 0x0600002F RID: 47
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_length_Injected(IntPtr _unity_self);

		// Token: 0x06000030 RID: 48
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_samples_Injected(IntPtr _unity_self);

		// Token: 0x06000031 RID: 49
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_channels_Injected(IntPtr _unity_self);

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_frequency_Injected(IntPtr _unity_self);

		// Token: 0x06000033 RID: 51
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadyToPlay_Injected(IntPtr _unity_self);

		// Token: 0x06000034 RID: 52
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioClipLoadType get_loadType_Injected(IntPtr _unity_self);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadAudioData_Injected(IntPtr _unity_self);

		// Token: 0x06000036 RID: 54
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UnloadAudioData_Injected(IntPtr _unity_self);

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_preloadAudioData_Injected(IntPtr _unity_self);

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_ambisonic_Injected(IntPtr _unity_self);

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_loadInBackground_Injected(IntPtr _unity_self);

		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioDataLoadState get_loadState_Injected(IntPtr _unity_self);

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x0600003C RID: 60
		public delegate void PCMReaderCallback(float[] data);

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x0600003E RID: 62
		public delegate void PCMSetPositionCallback(int position);
	}
}
