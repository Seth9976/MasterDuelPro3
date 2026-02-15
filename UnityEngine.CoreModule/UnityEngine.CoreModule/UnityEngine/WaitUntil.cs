using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001CC RID: 460
	public sealed class WaitUntil : CustomYieldInstruction
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x000261FC File Offset: 0x000243FC
		public override bool keepWaiting
		{
			get
			{
				bool flag = this.m_MaxExecutionTime == -1.0;
				bool flag2;
				if (flag)
				{
					flag2 = !this.m_Predicate();
				}
				else
				{
					bool flag3 = this.GetTime() > this.m_MaxExecutionTime;
					if (flag3)
					{
						this.m_TimeoutCallback();
						flag2 = false;
					}
					else
					{
						flag2 = !this.m_Predicate();
					}
				}
				return flag2;
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00026265 File Offset: 0x00024465
		public WaitUntil(Func<bool> predicate)
		{
			this.m_Predicate = predicate;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00026285 File Offset: 0x00024485
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private double GetTime()
		{
			return (this.m_TimeoutMode == WaitTimeoutMode.InGameTime) ? Time.timeAsDouble : Time.realtimeSinceStartupAsDouble;
		}

		// Token: 0x040006AF RID: 1711
		private readonly Func<bool> m_Predicate;

		// Token: 0x040006B0 RID: 1712
		private readonly Action m_TimeoutCallback;

		// Token: 0x040006B1 RID: 1713
		private readonly WaitTimeoutMode m_TimeoutMode;

		// Token: 0x040006B2 RID: 1714
		private readonly double m_MaxExecutionTime = -1.0;
	}
}
