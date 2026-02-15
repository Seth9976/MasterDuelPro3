using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Modules/Audio/Public/ScriptBindings/Audio.bindings.h")]
	[StaticAccessor("GetAudioManager()", StaticAccessorType.Dot)]
	public sealed class AudioSettings
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (remove) Token: 0x06000002 RID: 2 RVA: 0x00002084 File Offset: 0x00000284
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event AudioSettings.AudioConfigurationChangeHandler OnAudioConfigurationChanged;

		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		[RequiredByNativeCode]
		internal static void InvokeOnAudioConfigurationChanged(bool deviceWasChanged)
		{
			bool flag = AudioSettings.OnAudioConfigurationChanged != null;
			if (flag)
			{
				AudioSettings.OnAudioConfigurationChanged(deviceWasChanged);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020DE File Offset: 0x000002DE
		[RequiredByNativeCode]
		internal static void InvokeOnAudioSystemShuttingDown()
		{
			Action onAudioSystemShuttingDown = AudioSettings.OnAudioSystemShuttingDown;
			if (onAudioSystemShuttingDown != null)
			{
				onAudioSystemShuttingDown();
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F1 File Offset: 0x000002F1
		[RequiredByNativeCode]
		internal static void InvokeOnAudioSystemStartedUp()
		{
			Action onAudioSystemStartedUp = AudioSettings.OnAudioSystemStartedUp;
			if (onAudioSystemStartedUp != null)
			{
				onAudioSystemStartedUp();
			}
		}

		// Token: 0x04000026 RID: 38
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action OnAudioSystemShuttingDown;

		// Token: 0x04000027 RID: 39
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action OnAudioSystemStartedUp;

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x06000007 RID: 7
		public delegate void AudioConfigurationChangeHandler(bool deviceWasChanged);
	}
}
