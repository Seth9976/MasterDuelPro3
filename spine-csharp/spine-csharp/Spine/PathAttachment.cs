using System;

namespace Spine
{
	// Token: 0x02000051 RID: 81
	public class PathAttachment : VertexAttachment
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000AAA2 File Offset: 0x00008CA2
		// (set) Token: 0x06000207 RID: 519 RVA: 0x0000AAAA File Offset: 0x00008CAA
		public float[] Lengths
		{
			get
			{
				return this.lengths;
			}
			set
			{
				this.lengths = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000AAB3 File Offset: 0x00008CB3
		// (set) Token: 0x06000209 RID: 521 RVA: 0x0000AABB File Offset: 0x00008CBB
		public bool Closed
		{
			get
			{
				return this.closed;
			}
			set
			{
				this.closed = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000AACC File Offset: 0x00008CCC
		public bool ConstantSpeed
		{
			get
			{
				return this.constantSpeed;
			}
			set
			{
				this.constantSpeed = value;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A2FB File Offset: 0x000084FB
		public PathAttachment(string name)
			: base(name)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		protected PathAttachment(PathAttachment other)
			: base(other)
		{
			this.lengths = new float[other.lengths.Length];
			Array.Copy(other.lengths, 0, this.lengths, 0, this.lengths.Length);
			this.closed = other.closed;
			this.constantSpeed = other.constantSpeed;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000AB32 File Offset: 0x00008D32
		public override Attachment Copy()
		{
			return new PathAttachment(this);
		}

		// Token: 0x04000145 RID: 325
		internal float[] lengths;

		// Token: 0x04000146 RID: 326
		internal bool closed;

		// Token: 0x04000147 RID: 327
		internal bool constantSpeed;
	}
}
