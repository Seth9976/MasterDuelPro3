using System;

namespace Spine
{
	// Token: 0x0200005E RID: 94
	public class EventData
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
		public int Int { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000CBFD File Offset: 0x0000ADFD
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000CC05 File Offset: 0x0000AE05
		public float Float { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000CC0E File Offset: 0x0000AE0E
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000CC16 File Offset: 0x0000AE16
		public string String { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000CC1F File Offset: 0x0000AE1F
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000CC27 File Offset: 0x0000AE27
		public string AudioPath { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000CC30 File Offset: 0x0000AE30
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000CC38 File Offset: 0x0000AE38
		public float Volume { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000CC41 File Offset: 0x0000AE41
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000CC49 File Offset: 0x0000AE49
		public float Balance { get; set; }

		// Token: 0x060002D7 RID: 727 RVA: 0x0000CC52 File Offset: 0x0000AE52
		public EventData(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "name cannot be null.");
			}
			this.name = name;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x040001B8 RID: 440
		internal string name;
	}
}
