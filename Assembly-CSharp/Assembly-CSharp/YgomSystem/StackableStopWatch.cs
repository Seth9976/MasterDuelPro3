using System;
using System.Text;

namespace YgomSystem
{
	// Token: 0x020004D3 RID: 1235
	public class StackableStopWatch
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isMeasuring
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x0000216A File Offset: 0x0000036A
		public string currentLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x0000216D File Offset: 0x0000036D
		public void Start(string label)
		{
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x0000216D File Offset: 0x0000036D
		public void End()
		{
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x0000216D File Offset: 0x0000036D
		public void EndAll()
		{
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x0000216D File Offset: 0x0000036D
		public void Print(bool header = true, bool detail = false)
		{
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x0000216D File Offset: 0x0000036D
		public void OutputResult(StringBuilder sb, bool header, bool detail)
		{
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x0000216D File Offset: 0x0000036D
		public void OutputLogger(bool header = true, bool detail = false)
		{
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x0000216A File Offset: 0x0000036A
		public string ToString(bool header, bool detail)
		{
			return null;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString()
		{
			return null;
		}
	}
}
