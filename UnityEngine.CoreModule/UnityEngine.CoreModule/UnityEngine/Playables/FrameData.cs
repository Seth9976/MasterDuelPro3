using System;

namespace UnityEngine.Playables
{
	// Token: 0x020002FA RID: 762
	public struct FrameData
	{
		// Token: 0x06001528 RID: 5416 RVA: 0x0002CD04 File Offset: 0x0002AF04
		private bool HasFlags(FrameData.Flags flag)
		{
			return (this.m_Flags & flag) == flag;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0002CD24 File Offset: 0x0002AF24
		public float deltaTime
		{
			get
			{
				return (float)this.m_DeltaTime;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x0002CD40 File Offset: 0x0002AF40
		public float effectiveSpeed
		{
			get
			{
				return this.m_EffectiveSpeed;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0002CD58 File Offset: 0x0002AF58
		public FrameData.EvaluationType evaluationType
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Evaluate) ? FrameData.EvaluationType.Evaluate : FrameData.EvaluationType.Playback;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0002CD78 File Offset: 0x0002AF78
		public bool seekOccurred
		{
			get
			{
				return this.HasFlags(FrameData.Flags.SeekOccured);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x0002CD94 File Offset: 0x0002AF94
		public bool timeLooped
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Loop);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0002CDB0 File Offset: 0x0002AFB0
		public bool timeHeld
		{
			get
			{
				return this.HasFlags(FrameData.Flags.Hold);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0002CDCC File Offset: 0x0002AFCC
		public PlayableOutput output
		{
			get
			{
				return this.m_Output;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x0002CDE4 File Offset: 0x0002AFE4
		public PlayState effectivePlayState
		{
			get
			{
				bool flag = this.HasFlags(FrameData.Flags.EffectivePlayStateDelayed);
				PlayState playState;
				if (flag)
				{
					playState = PlayState.Delayed;
				}
				else
				{
					bool flag2 = this.HasFlags(FrameData.Flags.EffectivePlayStatePlaying);
					if (flag2)
					{
						playState = PlayState.Playing;
					}
					else
					{
						playState = PlayState.Paused;
					}
				}
				return playState;
			}
		}

		// Token: 0x040007F1 RID: 2033
		internal ulong m_FrameID;

		// Token: 0x040007F2 RID: 2034
		internal double m_DeltaTime;

		// Token: 0x040007F3 RID: 2035
		internal float m_Weight;

		// Token: 0x040007F4 RID: 2036
		internal float m_EffectiveWeight;

		// Token: 0x040007F5 RID: 2037
		internal double m_EffectiveParentDelay;

		// Token: 0x040007F6 RID: 2038
		internal float m_EffectiveParentSpeed;

		// Token: 0x040007F7 RID: 2039
		internal float m_EffectiveSpeed;

		// Token: 0x040007F8 RID: 2040
		internal FrameData.Flags m_Flags;

		// Token: 0x040007F9 RID: 2041
		internal PlayableOutput m_Output;

		// Token: 0x020002FB RID: 763
		[Flags]
		internal enum Flags
		{
			// Token: 0x040007FB RID: 2043
			Evaluate = 1,
			// Token: 0x040007FC RID: 2044
			SeekOccured = 2,
			// Token: 0x040007FD RID: 2045
			Loop = 4,
			// Token: 0x040007FE RID: 2046
			Hold = 8,
			// Token: 0x040007FF RID: 2047
			EffectivePlayStateDelayed = 16,
			// Token: 0x04000800 RID: 2048
			EffectivePlayStatePlaying = 32
		}

		// Token: 0x020002FC RID: 764
		public enum EvaluationType
		{
			// Token: 0x04000802 RID: 2050
			Evaluate,
			// Token: 0x04000803 RID: 2051
			Playback
		}
	}
}
