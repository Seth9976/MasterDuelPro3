using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B3 RID: 691
	public struct TimerState : IEquatable<TimerState>
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x0004DC14 File Offset: 0x0004BE14
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x0004DC1C File Offset: 0x0004BE1C
		public long start { readonly get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0004DC25 File Offset: 0x0004BE25
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x0004DC2D File Offset: 0x0004BE2D
		public long now { readonly get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0004DC38 File Offset: 0x0004BE38
		public long deltaTime
		{
			get
			{
				return this.now - this.start;
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0004DC58 File Offset: 0x0004BE58
		public override bool Equals(object obj)
		{
			return obj is TimerState && this.Equals((TimerState)obj);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0004DC84 File Offset: 0x0004BE84
		public bool Equals(TimerState other)
		{
			return this.start == other.start && this.now == other.now && this.deltaTime == other.deltaTime;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0004DCC8 File Offset: 0x0004BEC8
		public override int GetHashCode()
		{
			int hashCode = 540054806;
			hashCode = hashCode * -1521134295 + this.start.GetHashCode();
			hashCode = hashCode * -1521134295 + this.now.GetHashCode();
			return hashCode * -1521134295 + this.deltaTime.GetHashCode();
		}
	}
}
