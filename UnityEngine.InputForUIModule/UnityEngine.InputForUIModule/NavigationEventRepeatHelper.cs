using System;
using Unity.IntegerTime;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200002B RID: 43
	internal class NavigationEventRepeatHelper
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00004F73 File Offset: 0x00003173
		public void Reset()
		{
			this.m_ConsecutiveMoveCount = 0;
			this.m_LastDirection = NavigationEvent.Direction.None;
			this.m_PrevActionTime = DiscreteTime.Zero;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004F90 File Offset: 0x00003190
		public bool ShouldSendMoveEvent(DiscreteTime timestamp, NavigationEvent.Direction direction, bool axisButtonsWherePressedThisFrame)
		{
			bool flag = axisButtonsWherePressedThisFrame || direction != this.m_LastDirection || timestamp > this.m_PrevActionTime + ((this.m_ConsecutiveMoveCount == 1) ? this.m_InitialRepeatDelay : this.m_ConsecutiveRepeatDelay);
			bool flag2;
			if (flag)
			{
				this.m_ConsecutiveMoveCount = ((direction == this.m_LastDirection) ? (this.m_ConsecutiveMoveCount + 1) : 1);
				this.m_LastDirection = direction;
				this.m_PrevActionTime = timestamp;
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x040000CF RID: 207
		private int m_ConsecutiveMoveCount;

		// Token: 0x040000D0 RID: 208
		private NavigationEvent.Direction m_LastDirection;

		// Token: 0x040000D1 RID: 209
		private DiscreteTime m_PrevActionTime;

		// Token: 0x040000D2 RID: 210
		private readonly DiscreteTime m_InitialRepeatDelay = new DiscreteTime(0.5f);

		// Token: 0x040000D3 RID: 211
		private readonly DiscreteTime m_ConsecutiveRepeatDelay = new DiscreteTime(0.1f);
	}
}
