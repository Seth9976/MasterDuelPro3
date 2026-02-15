using System;

namespace Spine
{
	// Token: 0x0200005D RID: 93
	public class Event
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000CB49 File Offset: 0x0000AD49
		public EventData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000CB51 File Offset: 0x0000AD51
		public float Time
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000CB59 File Offset: 0x0000AD59
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000CB61 File Offset: 0x0000AD61
		public int Int
		{
			get
			{
				return this.intValue;
			}
			set
			{
				this.intValue = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000CB6A File Offset: 0x0000AD6A
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000CB72 File Offset: 0x0000AD72
		public float Float
		{
			get
			{
				return this.floatValue;
			}
			set
			{
				this.floatValue = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000CB7B File Offset: 0x0000AD7B
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000CB83 File Offset: 0x0000AD83
		public string String
		{
			get
			{
				return this.stringValue;
			}
			set
			{
				this.stringValue = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				this.volume = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000CB9D File Offset: 0x0000AD9D
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000CBA5 File Offset: 0x0000ADA5
		public float Balance
		{
			get
			{
				return this.balance;
			}
			set
			{
				this.balance = value;
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000CBAE File Offset: 0x0000ADAE
		public Event(float time, EventData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			this.time = time;
			this.data = data;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000CBD7 File Offset: 0x0000ADD7
		public override string ToString()
		{
			return this.data.Name;
		}

		// Token: 0x040001B1 RID: 433
		internal readonly EventData data;

		// Token: 0x040001B2 RID: 434
		internal readonly float time;

		// Token: 0x040001B3 RID: 435
		internal int intValue;

		// Token: 0x040001B4 RID: 436
		internal float floatValue;

		// Token: 0x040001B5 RID: 437
		internal string stringValue;

		// Token: 0x040001B6 RID: 438
		internal float volume;

		// Token: 0x040001B7 RID: 439
		internal float balance;
	}
}
