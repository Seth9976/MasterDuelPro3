using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Audio
{
	// Token: 0x02000012 RID: 18
	[StaticAccessor("AudioSampleProviderBindings", StaticAccessorType.DoubleColon)]
	[NativeType(Header = "Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.h")]
	public class AudioSampleProvider
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0000383C File Offset: 0x00001A3C
		[RequiredByNativeCode]
		private void InvokeSampleFramesAvailable(int sampleFrameCount)
		{
			bool flag = this.sampleFramesAvailable != null;
			if (flag)
			{
				this.sampleFramesAvailable(this, (uint)sampleFrameCount);
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003868 File Offset: 0x00001A68
		[RequiredByNativeCode]
		private void InvokeSampleFramesOverflow(int droppedSampleFrameCount)
		{
			bool flag = this.sampleFramesOverflow != null;
			if (flag)
			{
				this.sampleFramesOverflow(this, (uint)droppedSampleFrameCount);
			}
		}

		// Token: 0x0400002A RID: 42
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private AudioSampleProvider.SampleFramesHandler sampleFramesAvailable;

		// Token: 0x0400002B RID: 43
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private AudioSampleProvider.SampleFramesHandler sampleFramesOverflow;

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x06000102 RID: 258
		public delegate void SampleFramesHandler(AudioSampleProvider provider, uint sampleFrameCount);
	}
}
