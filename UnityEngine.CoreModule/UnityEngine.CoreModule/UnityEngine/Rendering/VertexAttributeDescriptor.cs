using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000340 RID: 832
	[UsedByNativeCode]
	public struct VertexAttributeDescriptor : IEquatable<VertexAttributeDescriptor>
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0002EBAC File Offset: 0x0002CDAC
		// (set) Token: 0x0600164A RID: 5706 RVA: 0x0002EBB4 File Offset: 0x0002CDB4
		public VertexAttribute attribute { readonly get; set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0002EBBD File Offset: 0x0002CDBD
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x0002EBC5 File Offset: 0x0002CDC5
		public VertexAttributeFormat format { readonly get; set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0002EBCE File Offset: 0x0002CDCE
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x0002EBD6 File Offset: 0x0002CDD6
		public int dimension { readonly get; set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0002EBDF File Offset: 0x0002CDDF
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x0002EBE7 File Offset: 0x0002CDE7
		public int stream { readonly get; set; }

		// Token: 0x06001651 RID: 5713 RVA: 0x0002EBF0 File Offset: 0x0002CDF0
		public VertexAttributeDescriptor(VertexAttribute attribute = VertexAttribute.Position, VertexAttributeFormat format = VertexAttributeFormat.Float32, int dimension = 3, int stream = 0)
		{
			this.attribute = attribute;
			this.format = format;
			this.dimension = dimension;
			this.stream = stream;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0002EC14 File Offset: 0x0002CE14
		public override string ToString()
		{
			return string.Format("(attr={0} fmt={1} dim={2} stream={3})", new object[] { this.attribute, this.format, this.dimension, this.stream });
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0002EC70 File Offset: 0x0002CE70
		public override int GetHashCode()
		{
			int h = 17;
			h = (int)(h * 23 + this.attribute);
			h = (int)(h * 23 + this.format);
			h = h * 23 + this.dimension;
			return h * 23 + this.stream;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0002ECB8 File Offset: 0x0002CEB8
		public override bool Equals(object other)
		{
			bool flag = !(other is VertexAttributeDescriptor);
			return !flag && this.Equals((VertexAttributeDescriptor)other);
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0002ECEC File Offset: 0x0002CEEC
		public bool Equals(VertexAttributeDescriptor other)
		{
			return this.attribute == other.attribute && this.format == other.format && this.dimension == other.dimension && this.stream == other.stream;
		}
	}
}
