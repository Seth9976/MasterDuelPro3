using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Audio
{
	// Token: 0x02000016 RID: 22
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	[NativeHeader("Modules/Audio/Public/AudioMixer.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioMixer.bindings.h")]
	public class AudioMixer : Object
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00003B90 File Offset: 0x00001D90
		[NativeMethod]
		public unsafe bool SetFloat(string name, float value)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
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
				flag = AudioMixer.SetFloat_Injected(intPtr, ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003BF8 File Offset: 0x00001DF8
		[NativeMethod]
		public unsafe bool GetFloat(string name, out float value)
		{
			bool float_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixer>(this);
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
				float_Injected = AudioMixer.GetFloat_Injected(intPtr, ref managedSpanWrapper, out value);
			}
			finally
			{
				char* ptr = null;
			}
			return float_Injected;
		}

		// Token: 0x06000118 RID: 280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetFloat_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, float value);

		// Token: 0x06000119 RID: 281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetFloat_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, out float value);
	}
}
