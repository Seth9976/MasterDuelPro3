using System;

namespace WindBot.Game.AI
{
	// Token: 0x0200022D RID: 557
	[AttributeUsage(AttributeTargets.Class)]
	public class DeckAttribute : Attribute
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00032CE4 File Offset: 0x00030EE4
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x00032CEC File Offset: 0x00030EEC
		public string Name { get; private set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00032CF5 File Offset: 0x00030EF5
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x00032CFD File Offset: 0x00030EFD
		public string File { get; private set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00032D06 File Offset: 0x00030F06
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x00032D0E File Offset: 0x00030F0E
		public string Level { get; private set; }

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00032D17 File Offset: 0x00030F17
		public DeckAttribute(string name, string file = null, string level = "Normal")
		{
			if (string.IsNullOrEmpty(file))
			{
				file = name;
			}
			this.Name = name;
			this.File = file;
			this.Level = level;
		}
	}
}
