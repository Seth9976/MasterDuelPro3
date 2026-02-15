using System;

namespace Spine
{
	// Token: 0x02000063 RID: 99
	public class IkConstraintData : ConstraintData
	{
		// Token: 0x0600033F RID: 831 RVA: 0x0000E780 File Offset: 0x0000C980
		public IkConstraintData(string name)
			: base(name)
		{
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000E794 File Offset: 0x0000C994
		public ExposedList<BoneData> Bones
		{
			get
			{
				return this.bones;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000E79C File Offset: 0x0000C99C
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		public BoneData Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000E7AD File Offset: 0x0000C9AD
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000E7B5 File Offset: 0x0000C9B5
		public float Mix
		{
			get
			{
				return this.mix;
			}
			set
			{
				this.mix = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000E7BE File Offset: 0x0000C9BE
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000E7C6 File Offset: 0x0000C9C6
		public float Softness
		{
			get
			{
				return this.softness;
			}
			set
			{
				this.softness = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000E7CF File Offset: 0x0000C9CF
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000E7D7 File Offset: 0x0000C9D7
		public int BendDirection
		{
			get
			{
				return this.bendDirection;
			}
			set
			{
				this.bendDirection = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		public bool Compress
		{
			get
			{
				return this.compress;
			}
			set
			{
				this.compress = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000E7F1 File Offset: 0x0000C9F1
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000E7F9 File Offset: 0x0000C9F9
		public bool Stretch
		{
			get
			{
				return this.stretch;
			}
			set
			{
				this.stretch = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000E802 File Offset: 0x0000CA02
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000E80A File Offset: 0x0000CA0A
		public bool Uniform
		{
			get
			{
				return this.uniform;
			}
			set
			{
				this.uniform = value;
			}
		}

		// Token: 0x040001D1 RID: 465
		internal ExposedList<BoneData> bones = new ExposedList<BoneData>();

		// Token: 0x040001D2 RID: 466
		internal BoneData target;

		// Token: 0x040001D3 RID: 467
		internal int bendDirection;

		// Token: 0x040001D4 RID: 468
		internal bool compress;

		// Token: 0x040001D5 RID: 469
		internal bool stretch;

		// Token: 0x040001D6 RID: 470
		internal bool uniform;

		// Token: 0x040001D7 RID: 471
		internal float mix;

		// Token: 0x040001D8 RID: 472
		internal float softness;
	}
}
