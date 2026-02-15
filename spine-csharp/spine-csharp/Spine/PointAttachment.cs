using System;

namespace Spine
{
	// Token: 0x02000052 RID: 82
	public class PointAttachment : Attachment
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000AB3A File Offset: 0x00008D3A
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000AB42 File Offset: 0x00008D42
		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000AB4B File Offset: 0x00008D4B
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000AB53 File Offset: 0x00008D53
		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000AB5C File Offset: 0x00008D5C
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000AB64 File Offset: 0x00008D64
		public float Rotation
		{
			get
			{
				return this.rotation;
			}
			set
			{
				this.rotation = value;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000AB6D File Offset: 0x00008D6D
		public PointAttachment(string name)
			: base(name)
		{
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000AB76 File Offset: 0x00008D76
		protected PointAttachment(PointAttachment other)
			: base(other)
		{
			this.x = other.x;
			this.y = other.y;
			this.rotation = other.rotation;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000ABA3 File Offset: 0x00008DA3
		public void ComputeWorldPosition(Bone bone, out float ox, out float oy)
		{
			bone.LocalToWorld(this.x, this.y, out ox, out oy);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000ABBC File Offset: 0x00008DBC
		public float ComputeWorldRotation(Bone bone)
		{
			float num = this.rotation * 0.017453292f;
			float cos = (float)Math.Cos((double)num);
			float sin = (float)Math.Sin((double)num);
			float x = cos * bone.a + sin * bone.b;
			return MathUtils.Atan2Deg(cos * bone.c + sin * bone.d, x);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000AC0F File Offset: 0x00008E0F
		public override Attachment Copy()
		{
			return new PointAttachment(this);
		}

		// Token: 0x04000148 RID: 328
		internal float x;

		// Token: 0x04000149 RID: 329
		internal float y;

		// Token: 0x0400014A RID: 330
		internal float rotation;
	}
}
