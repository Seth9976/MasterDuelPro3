using System;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000FB RID: 251
	public abstract class Asn1Structured : Asn1Object
	{
		// Token: 0x06000633 RID: 1587 RVA: 0x00019184 File Offset: 0x00017384
		protected internal Asn1Structured(Asn1Identifier id)
			: this(id, 10)
		{
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001918F File Offset: 0x0001738F
		protected internal Asn1Structured(Asn1Identifier id, int size)
			: base(id)
		{
			this.content = new Asn1Object[size];
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000191A4 File Offset: 0x000173A4
		protected internal Asn1Structured(Asn1Identifier id, Asn1Object[] newContent, int size)
			: base(id)
		{
			this.content = newContent;
			this.contentIndex = size;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000191BB File Offset: 0x000173BB
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000191C8 File Offset: 0x000173C8
		[CLSCompliant(false)]
		protected internal void decodeStructured(Asn1Decoder dec, Stream in_Renamed, int len)
		{
			int[] array = new int[1];
			while (len > 0)
			{
				this.add(dec.decode(in_Renamed, array));
				len -= array[0];
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000191F8 File Offset: 0x000173F8
		public Asn1Object[] toArray()
		{
			Asn1Object[] array = new Asn1Object[this.contentIndex];
			Array.Copy(this.content, 0, array, 0, this.contentIndex);
			return array;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00019228 File Offset: 0x00017428
		public void add(Asn1Object value_Renamed)
		{
			if (this.contentIndex == this.content.Length)
			{
				Asn1Object[] array = new Asn1Object[this.contentIndex + this.contentIndex];
				Array.Copy(this.content, 0, array, 0, this.contentIndex);
				this.content = array;
			}
			Asn1Object[] array2 = this.content;
			int num = this.contentIndex;
			this.contentIndex = num + 1;
			array2[num] = value_Renamed;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001928C File Offset: 0x0001748C
		public void set_Renamed(int index, Asn1Object value_Renamed)
		{
			if (index >= this.contentIndex || index < 0)
			{
				throw new IndexOutOfRangeException("Asn1Structured: get: index " + index.ToString() + ", size " + this.contentIndex.ToString());
			}
			this.content[index] = value_Renamed;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000192CB File Offset: 0x000174CB
		public Asn1Object get_Renamed(int index)
		{
			if (index >= this.contentIndex || index < 0)
			{
				throw new IndexOutOfRangeException("Asn1Structured: set: index " + index.ToString() + ", size " + this.contentIndex.ToString());
			}
			return this.content[index];
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00019309 File Offset: 0x00017509
		public int size()
		{
			return this.contentIndex;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00019314 File Offset: 0x00017514
		[CLSCompliant(false)]
		public virtual string toString(string type)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(type);
			for (int i = 0; i < this.contentIndex; i++)
			{
				stringBuilder.Append(this.content[i]);
				if (i != this.contentIndex - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.Append(" }");
			return base.ToString() + stringBuilder.ToString();
		}

		// Token: 0x040004F9 RID: 1273
		private Asn1Object[] content;

		// Token: 0x040004FA RID: 1274
		private int contentIndex;
	}
}
