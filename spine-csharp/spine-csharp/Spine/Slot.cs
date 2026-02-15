using System;

namespace Spine
{
	// Token: 0x02000081 RID: 129
	public class Slot
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x0001BB34 File Offset: 0x00019D34
		public Slot(SlotData data, Bone bone)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			if (bone == null)
			{
				throw new ArgumentNullException("bone", "bone cannot be null.");
			}
			this.data = data;
			this.bone = bone;
			if (data.hasSecondColor)
			{
				this.r2 = (this.g2 = (this.b2 = 0f));
			}
			this.SetToSetupPose();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		public Slot(Slot slot, Bone bone)
		{
			if (slot == null)
			{
				throw new ArgumentNullException("slot", "slot cannot be null.");
			}
			if (bone == null)
			{
				throw new ArgumentNullException("bone", "bone cannot be null.");
			}
			this.data = slot.data;
			this.bone = bone;
			this.r = slot.r;
			this.g = slot.g;
			this.b = slot.b;
			this.a = slot.a;
			if (slot.hasSecondColor)
			{
				this.r2 = slot.r2;
				this.g2 = slot.g2;
				this.b2 = slot.b2;
			}
			else
			{
				this.r2 = (this.g2 = (this.b2 = 0f));
			}
			this.hasSecondColor = slot.hasSecondColor;
			this.attachment = slot.attachment;
			this.sequenceIndex = slot.sequenceIndex;
			this.deform.AddRange(slot.deform);
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001BCBB File Offset: 0x00019EBB
		public SlotData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001BCC3 File Offset: 0x00019EC3
		public Bone Bone
		{
			get
			{
				return this.bone;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001BCCB File Offset: 0x00019ECB
		public Skeleton Skeleton
		{
			get
			{
				return this.bone.skeleton;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001BCD8 File Offset: 0x00019ED8
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		public float R
		{
			get
			{
				return this.r;
			}
			set
			{
				this.r = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001BCE9 File Offset: 0x00019EE9
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x0001BCF1 File Offset: 0x00019EF1
		public float G
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001BCFA File Offset: 0x00019EFA
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x0001BD02 File Offset: 0x00019F02
		public float B
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001BD0B File Offset: 0x00019F0B
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x0001BD13 File Offset: 0x00019F13
		public float A
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001BD1C File Offset: 0x00019F1C
		public void ClampColor()
		{
			this.r = MathUtils.Clamp(this.r, 0f, 1f);
			this.g = MathUtils.Clamp(this.g, 0f, 1f);
			this.b = MathUtils.Clamp(this.b, 0f, 1f);
			this.a = MathUtils.Clamp(this.a, 0f, 1f);
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001BD95 File Offset: 0x00019F95
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x0001BD9D File Offset: 0x00019F9D
		public float R2
		{
			get
			{
				return this.r2;
			}
			set
			{
				this.r2 = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001BDA6 File Offset: 0x00019FA6
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0001BDAE File Offset: 0x00019FAE
		public float G2
		{
			get
			{
				return this.g2;
			}
			set
			{
				this.g2 = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0001BDB7 File Offset: 0x00019FB7
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0001BDBF File Offset: 0x00019FBF
		public float B2
		{
			get
			{
				return this.b2;
			}
			set
			{
				this.b2 = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001BDC8 File Offset: 0x00019FC8
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0001BDD5 File Offset: 0x00019FD5
		public bool HasSecondColor
		{
			get
			{
				return this.data.hasSecondColor;
			}
			set
			{
				this.data.hasSecondColor = value;
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001BDE4 File Offset: 0x00019FE4
		public void ClampSecondColor()
		{
			this.r2 = MathUtils.Clamp(this.r2, 0f, 1f);
			this.g2 = MathUtils.Clamp(this.g2, 0f, 1f);
			this.b2 = MathUtils.Clamp(this.b2, 0f, 1f);
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001BE42 File Offset: 0x0001A042
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x0001BE4C File Offset: 0x0001A04C
		public Attachment Attachment
		{
			get
			{
				return this.attachment;
			}
			set
			{
				if (this.attachment == value)
				{
					return;
				}
				if (!(value is VertexAttachment) || !(this.attachment is VertexAttachment) || ((VertexAttachment)value).TimelineAttachment != ((VertexAttachment)this.attachment).TimelineAttachment)
				{
					this.deform.Clear(true);
				}
				this.attachment = value;
				this.sequenceIndex = -1;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0001BEAF File Offset: 0x0001A0AF
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x0001BEB7 File Offset: 0x0001A0B7
		public int SequenceIndex
		{
			get
			{
				return this.sequenceIndex;
			}
			set
			{
				this.sequenceIndex = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0001BEC8 File Offset: 0x0001A0C8
		public ExposedList<float> Deform
		{
			get
			{
				return this.deform;
			}
			set
			{
				if (this.deform == null)
				{
					throw new ArgumentNullException("deform", "deform cannot be null.");
				}
				this.deform = value;
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001BEEC File Offset: 0x0001A0EC
		public void SetToSetupPose()
		{
			this.r = this.data.r;
			this.g = this.data.g;
			this.b = this.data.b;
			this.a = this.data.a;
			if (this.HasSecondColor)
			{
				this.r2 = this.data.r2;
				this.g2 = this.data.g2;
				this.b2 = this.data.b2;
			}
			if (this.data.attachmentName == null)
			{
				this.Attachment = null;
				return;
			}
			this.attachment = null;
			this.Attachment = this.bone.skeleton.GetAttachment(this.data.index, this.data.attachmentName);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
		public override string ToString()
		{
			return this.data.name;
		}

		// Token: 0x040002BF RID: 703
		internal SlotData data;

		// Token: 0x040002C0 RID: 704
		internal Bone bone;

		// Token: 0x040002C1 RID: 705
		internal float r;

		// Token: 0x040002C2 RID: 706
		internal float g;

		// Token: 0x040002C3 RID: 707
		internal float b;

		// Token: 0x040002C4 RID: 708
		internal float a;

		// Token: 0x040002C5 RID: 709
		internal float r2;

		// Token: 0x040002C6 RID: 710
		internal float g2;

		// Token: 0x040002C7 RID: 711
		internal float b2;

		// Token: 0x040002C8 RID: 712
		internal bool hasSecondColor;

		// Token: 0x040002C9 RID: 713
		internal Attachment attachment;

		// Token: 0x040002CA RID: 714
		internal int sequenceIndex;

		// Token: 0x040002CB RID: 715
		internal ExposedList<float> deform = new ExposedList<float>();

		// Token: 0x040002CC RID: 716
		internal int attachmentState;
	}
}
