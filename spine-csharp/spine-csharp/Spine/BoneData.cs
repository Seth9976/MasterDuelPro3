using System;

namespace Spine
{
	// Token: 0x02000059 RID: 89
	public class BoneData
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000C9BB File Offset: 0x0000ABBB
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000C9C3 File Offset: 0x0000ABC3
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000C9CB File Offset: 0x0000ABCB
		public BoneData Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000C9D3 File Offset: 0x0000ABD3
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000C9DB File Offset: 0x0000ABDB
		public float Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000C9E4 File Offset: 0x0000ABE4
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000C9EC File Offset: 0x0000ABEC
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000C9F5 File Offset: 0x0000ABF5
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000C9FD File Offset: 0x0000ABFD
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000CA06 File Offset: 0x0000AC06
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000CA0E File Offset: 0x0000AC0E
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000CA17 File Offset: 0x0000AC17
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000CA1F File Offset: 0x0000AC1F
		public float ScaleX
		{
			get
			{
				return this.scaleX;
			}
			set
			{
				this.scaleX = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000CA28 File Offset: 0x0000AC28
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000CA30 File Offset: 0x0000AC30
		public float ScaleY
		{
			get
			{
				return this.scaleY;
			}
			set
			{
				this.scaleY = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000CA39 File Offset: 0x0000AC39
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000CA41 File Offset: 0x0000AC41
		public float ShearX
		{
			get
			{
				return this.shearX;
			}
			set
			{
				this.shearX = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000CA4A File Offset: 0x0000AC4A
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000CA52 File Offset: 0x0000AC52
		public float ShearY
		{
			get
			{
				return this.shearY;
			}
			set
			{
				this.shearY = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000CA5B File Offset: 0x0000AC5B
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000CA63 File Offset: 0x0000AC63
		public Inherit Inherit
		{
			get
			{
				return this.inherit;
			}
			set
			{
				this.inherit = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000CA74 File Offset: 0x0000AC74
		public bool SkinRequired
		{
			get
			{
				return this.skinRequired;
			}
			set
			{
				this.skinRequired = value;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000CA80 File Offset: 0x0000AC80
		public BoneData(int index, string name, BoneData parent)
		{
			if (index < 0)
			{
				throw new ArgumentException("index must be >= 0", "index");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name", "name cannot be null.");
			}
			this.index = index;
			this.name = name;
			this.parent = parent;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000C9C3 File Offset: 0x0000ABC3
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x0400019A RID: 410
		internal int index;

		// Token: 0x0400019B RID: 411
		internal string name;

		// Token: 0x0400019C RID: 412
		internal BoneData parent;

		// Token: 0x0400019D RID: 413
		internal float length;

		// Token: 0x0400019E RID: 414
		internal float x;

		// Token: 0x0400019F RID: 415
		internal float y;

		// Token: 0x040001A0 RID: 416
		internal float rotation;

		// Token: 0x040001A1 RID: 417
		internal float scaleX = 1f;

		// Token: 0x040001A2 RID: 418
		internal float scaleY = 1f;

		// Token: 0x040001A3 RID: 419
		internal float shearX;

		// Token: 0x040001A4 RID: 420
		internal float shearY;

		// Token: 0x040001A5 RID: 421
		internal Inherit inherit;

		// Token: 0x040001A6 RID: 422
		internal bool skinRequired;
	}
}
