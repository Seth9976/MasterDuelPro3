using System;

namespace Spine
{
	// Token: 0x0200006A RID: 106
	public class PathConstraintData : ConstraintData
	{
		// Token: 0x0600037D RID: 893 RVA: 0x0000FC09 File Offset: 0x0000DE09
		public PathConstraintData(string name)
			: base(name)
		{
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000FC1D File Offset: 0x0000DE1D
		public ExposedList<BoneData> Bones
		{
			get
			{
				return this.bones;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000FC25 File Offset: 0x0000DE25
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000FC2D File Offset: 0x0000DE2D
		public SlotData Target
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

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000FC36 File Offset: 0x0000DE36
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000FC3E File Offset: 0x0000DE3E
		public PositionMode PositionMode
		{
			get
			{
				return this.positionMode;
			}
			set
			{
				this.positionMode = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000FC47 File Offset: 0x0000DE47
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000FC4F File Offset: 0x0000DE4F
		public SpacingMode SpacingMode
		{
			get
			{
				return this.spacingMode;
			}
			set
			{
				this.spacingMode = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000FC58 File Offset: 0x0000DE58
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000FC60 File Offset: 0x0000DE60
		public RotateMode RotateMode
		{
			get
			{
				return this.rotateMode;
			}
			set
			{
				this.rotateMode = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000FC69 File Offset: 0x0000DE69
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000FC71 File Offset: 0x0000DE71
		public float OffsetRotation
		{
			get
			{
				return this.offsetRotation;
			}
			set
			{
				this.offsetRotation = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000FC7A File Offset: 0x0000DE7A
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000FC82 File Offset: 0x0000DE82
		public float Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000FC8B File Offset: 0x0000DE8B
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000FC93 File Offset: 0x0000DE93
		public float Spacing
		{
			get
			{
				return this.spacing;
			}
			set
			{
				this.spacing = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		public float RotateMix
		{
			get
			{
				return this.mixRotate;
			}
			set
			{
				this.mixRotate = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000FCAD File Offset: 0x0000DEAD
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000FCB5 File Offset: 0x0000DEB5
		public float MixX
		{
			get
			{
				return this.mixX;
			}
			set
			{
				this.mixX = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000FCBE File Offset: 0x0000DEBE
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000FCC6 File Offset: 0x0000DEC6
		public float MixY
		{
			get
			{
				return this.mixY;
			}
			set
			{
				this.mixY = value;
			}
		}

		// Token: 0x040001F5 RID: 501
		internal ExposedList<BoneData> bones = new ExposedList<BoneData>();

		// Token: 0x040001F6 RID: 502
		internal SlotData target;

		// Token: 0x040001F7 RID: 503
		internal PositionMode positionMode;

		// Token: 0x040001F8 RID: 504
		internal SpacingMode spacingMode;

		// Token: 0x040001F9 RID: 505
		internal RotateMode rotateMode;

		// Token: 0x040001FA RID: 506
		internal float offsetRotation;

		// Token: 0x040001FB RID: 507
		internal float position;

		// Token: 0x040001FC RID: 508
		internal float spacing;

		// Token: 0x040001FD RID: 509
		internal float mixRotate;

		// Token: 0x040001FE RID: 510
		internal float mixX;

		// Token: 0x040001FF RID: 511
		internal float mixY;
	}
}
