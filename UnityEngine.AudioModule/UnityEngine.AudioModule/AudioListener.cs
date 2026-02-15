using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	[StaticAccessor("AudioListenerBindings", StaticAccessorType.DoubleColon)]
	[RequireComponent(typeof(Transform))]
	public sealed class AudioListener : AudioBehaviour
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002880 File Offset: 0x00000A80
		[NativeThrows]
		private unsafe static void GetOutputDataHelper([Out] float[] samples, int channel)
		{
			try
			{
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
				AudioListener.GetOutputDataHelper_Injected(out blittableArrayWrapper, channel);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028D0 File Offset: 0x00000AD0
		[NativeThrows]
		private unsafe static void GetSpectrumDataHelper([Out] float[] samples, int channel, FFTWindow window)
		{
			try
			{
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
				AudioListener.GetSpectrumDataHelper_Injected(out blittableArrayWrapper, channel, window);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000042 RID: 66
		// (set) Token: 0x06000043 RID: 67
		public static extern float volume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000044 RID: 68
		// (set) Token: 0x06000045 RID: 69
		[NativeProperty("ListenerPause")]
		public static extern bool pause
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002920 File Offset: 0x00000B20
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002944 File Offset: 0x00000B44
		public AudioVelocityUpdateMode velocityUpdateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioListener>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioListener.get_velocityUpdateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioListener>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioListener.set_velocityUpdateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002968 File Offset: 0x00000B68
		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public static float[] GetOutputData(int numSamples, int channel)
		{
			float[] samples = new float[numSamples];
			AudioListener.GetOutputDataHelper(samples, channel);
			return samples;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000298A File Offset: 0x00000B8A
		public static void GetOutputData(float[] samples, int channel)
		{
			AudioListener.GetOutputDataHelper(samples, channel);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002998 File Offset: 0x00000B98
		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData and pass a pre allocated array instead.")]
		public static float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] samples = new float[numSamples];
			AudioListener.GetSpectrumDataHelper(samples, channel, window);
			return samples;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029BB File Offset: 0x00000BBB
		public static void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			AudioListener.GetSpectrumDataHelper(samples, channel, window);
		}

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputDataHelper_Injected(out BlittableArrayWrapper samples, int channel);

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpectrumDataHelper_Injected(out BlittableArrayWrapper samples, int channel, FFTWindow window);

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioVelocityUpdateMode get_velocityUpdateMode_Injected(IntPtr _unity_self);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_velocityUpdateMode_Injected(IntPtr _unity_self, AudioVelocityUpdateMode value);
	}
}
