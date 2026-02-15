using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200039E RID: 926
	public struct BlendState : IEquatable<BlendState>
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x000363C4 File Offset: 0x000345C4
		public static BlendState defaultValue
		{
			get
			{
				return new BlendState(false, false);
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000363E0 File Offset: 0x000345E0
		public BlendState(bool separateMRTBlend = false, bool alphaToMask = false)
		{
			this.m_BlendState0 = RenderTargetBlendState.defaultValue;
			this.m_BlendState1 = RenderTargetBlendState.defaultValue;
			this.m_BlendState2 = RenderTargetBlendState.defaultValue;
			this.m_BlendState3 = RenderTargetBlendState.defaultValue;
			this.m_BlendState4 = RenderTargetBlendState.defaultValue;
			this.m_BlendState5 = RenderTargetBlendState.defaultValue;
			this.m_BlendState6 = RenderTargetBlendState.defaultValue;
			this.m_BlendState7 = RenderTargetBlendState.defaultValue;
			this.m_SeparateMRTBlendStates = Convert.ToByte(separateMRTBlend);
			this.m_AlphaToMask = Convert.ToByte(alphaToMask);
			this.m_Padding = 0;
		}

		// Token: 0x170003A1 RID: 929
		// (set) Token: 0x06001941 RID: 6465 RVA: 0x00036465 File Offset: 0x00034665
		public RenderTargetBlendState blendState0
		{
			set
			{
				this.m_BlendState0 = value;
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00036470 File Offset: 0x00034670
		public bool Equals(BlendState other)
		{
			return this.m_BlendState0.Equals(other.m_BlendState0) && this.m_BlendState1.Equals(other.m_BlendState1) && this.m_BlendState2.Equals(other.m_BlendState2) && this.m_BlendState3.Equals(other.m_BlendState3) && this.m_BlendState4.Equals(other.m_BlendState4) && this.m_BlendState5.Equals(other.m_BlendState5) && this.m_BlendState6.Equals(other.m_BlendState6) && this.m_BlendState7.Equals(other.m_BlendState7) && this.m_SeparateMRTBlendStates == other.m_SeparateMRTBlendStates && this.m_AlphaToMask == other.m_AlphaToMask;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00036540 File Offset: 0x00034740
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is BlendState && this.Equals((BlendState)obj);
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00036578 File Offset: 0x00034778
		public override int GetHashCode()
		{
			int hashCode = this.m_BlendState0.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_BlendState1.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_BlendState2.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_BlendState3.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_BlendState4.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_BlendState5.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_BlendState6.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_BlendState7.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_SeparateMRTBlendStates.GetHashCode();
			return (hashCode * 397) ^ this.m_AlphaToMask.GetHashCode();
		}

		// Token: 0x04000B82 RID: 2946
		private RenderTargetBlendState m_BlendState0;

		// Token: 0x04000B83 RID: 2947
		private RenderTargetBlendState m_BlendState1;

		// Token: 0x04000B84 RID: 2948
		private RenderTargetBlendState m_BlendState2;

		// Token: 0x04000B85 RID: 2949
		private RenderTargetBlendState m_BlendState3;

		// Token: 0x04000B86 RID: 2950
		private RenderTargetBlendState m_BlendState4;

		// Token: 0x04000B87 RID: 2951
		private RenderTargetBlendState m_BlendState5;

		// Token: 0x04000B88 RID: 2952
		private RenderTargetBlendState m_BlendState6;

		// Token: 0x04000B89 RID: 2953
		private RenderTargetBlendState m_BlendState7;

		// Token: 0x04000B8A RID: 2954
		private byte m_SeparateMRTBlendStates;

		// Token: 0x04000B8B RID: 2955
		private byte m_AlphaToMask;

		// Token: 0x04000B8C RID: 2956
		private short m_Padding;
	}
}
