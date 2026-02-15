using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000357 RID: 855
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/GPUFence.h")]
	public struct GraphicsFence
	{
		// Token: 0x0600166E RID: 5742 RVA: 0x0002F1FC File Offset: 0x0002D3FC
		internal static SynchronisationStageFlags TranslateSynchronizationStageToFlags(SynchronisationStage s)
		{
			return (s == SynchronisationStage.VertexProcessing) ? SynchronisationStageFlags.VertexProcessing : SynchronisationStageFlags.PixelProcessing;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x0002F218 File Offset: 0x0002D418
		internal void InitPostAllocation()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				bool supportsGraphicsFence = SystemInfo.supportsGraphicsFence;
				if (supportsGraphicsFence)
				{
					throw new NullReferenceException("The internal fence ptr is null, this should not be possible for fences that have been correctly constructed using Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
				}
				this.m_Version = this.GetPlatformNotSupportedVersion();
			}
			else
			{
				this.m_Version = GraphicsFence.GetVersionNumber(this.m_Ptr);
			}
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0002F270 File Offset: 0x0002D470
		internal bool IsFencePending()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			return !flag && this.m_Version == GraphicsFence.GetVersionNumber(this.m_Ptr);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0002F2B0 File Offset: 0x0002D4B0
		internal void Validate()
		{
			bool flag = this.m_Version == 0 || (SystemInfo.supportsGraphicsFence && this.m_Version == this.GetPlatformNotSupportedVersion());
			if (flag)
			{
				throw new InvalidOperationException("This GraphicsFence object has not been correctly constructed see Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
			}
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0002F2F0 File Offset: 0x0002D4F0
		private int GetPlatformNotSupportedVersion()
		{
			return -1;
		}

		// Token: 0x06001673 RID: 5747
		[NativeThrows]
		[FreeFunction("GPUFenceInternals::GetVersionNumber")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVersionNumber(IntPtr fencePtr);

		// Token: 0x04000A0E RID: 2574
		internal IntPtr m_Ptr;

		// Token: 0x04000A0F RID: 2575
		internal int m_Version;

		// Token: 0x04000A10 RID: 2576
		internal GraphicsFenceType m_FenceType;
	}
}
