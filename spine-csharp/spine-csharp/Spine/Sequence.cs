using System;
using System.Text;

namespace Spine
{
	// Token: 0x02000054 RID: 84
	public class Sequence
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000B33A File Offset: 0x0000953A
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000B342 File Offset: 0x00009542
		public int Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000B34B File Offset: 0x0000954B
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000B353 File Offset: 0x00009553
		public int Digits
		{
			get
			{
				return this.digits;
			}
			set
			{
				this.digits = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000B35C File Offset: 0x0000955C
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000B364 File Offset: 0x00009564
		public int SetupIndex
		{
			get
			{
				return this.setupIndex;
			}
			set
			{
				this.setupIndex = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000B36D File Offset: 0x0000956D
		public TextureRegion[] Regions
		{
			get
			{
				return this.regions;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000B375 File Offset: 0x00009575
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B380 File Offset: 0x00009580
		public Sequence(int count)
		{
			object obj = Sequence.nextIdLock;
			lock (obj)
			{
				this.id = Sequence.nextID++;
			}
			this.regions = new TextureRegion[count];
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000B3E0 File Offset: 0x000095E0
		public Sequence(Sequence other)
		{
			object obj = Sequence.nextIdLock;
			lock (obj)
			{
				this.id = Sequence.nextID++;
			}
			this.regions = new TextureRegion[other.regions.Length];
			Array.Copy(other.regions, 0, this.regions, 0, this.regions.Length);
			this.start = other.start;
			this.digits = other.digits;
			this.setupIndex = other.setupIndex;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000B484 File Offset: 0x00009684
		public void Apply(Slot slot, IHasTextureRegion attachment)
		{
			int index = slot.SequenceIndex;
			if (index == -1)
			{
				index = this.setupIndex;
			}
			if (index >= this.regions.Length)
			{
				index = this.regions.Length - 1;
			}
			TextureRegion region = this.regions[index];
			if (attachment.Region != region)
			{
				attachment.Region = region;
				attachment.UpdateRegion();
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000B4D8 File Offset: 0x000096D8
		public string GetPath(string basePath, int index)
		{
			StringBuilder buffer = new StringBuilder(basePath.Length + this.digits);
			buffer.Append(basePath);
			string frame = (this.start + index).ToString();
			for (int i = this.digits - frame.Length; i > 0; i--)
			{
				buffer.Append('0');
			}
			buffer.Append(frame);
			return buffer.ToString();
		}

		// Token: 0x04000163 RID: 355
		private static int nextID = 0;

		// Token: 0x04000164 RID: 356
		private static readonly object nextIdLock = new object();

		// Token: 0x04000165 RID: 357
		internal readonly int id;

		// Token: 0x04000166 RID: 358
		internal readonly TextureRegion[] regions;

		// Token: 0x04000167 RID: 359
		internal int start;

		// Token: 0x04000168 RID: 360
		internal int digits;

		// Token: 0x04000169 RID: 361
		internal int setupIndex;
	}
}
