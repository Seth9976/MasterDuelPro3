using System;

namespace Ookii.Dialogs
{
	// Token: 0x0200004B RID: 75
	public class TimerEventArgs : EventArgs
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00008402 File Offset: 0x00006602
		public TimerEventArgs(int tickCount)
		{
			this._tickCount = tickCount;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00008414 File Offset: 0x00006614
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000842C File Offset: 0x0000662C
		public bool ResetTickCount
		{
			get
			{
				return this._resetTickCount;
			}
			set
			{
				this._resetTickCount = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00008438 File Offset: 0x00006638
		public int TickCount
		{
			get
			{
				return this._tickCount;
			}
		}

		// Token: 0x040001F5 RID: 501
		private int _tickCount;

		// Token: 0x040001F6 RID: 502
		private bool _resetTickCount;
	}
}
