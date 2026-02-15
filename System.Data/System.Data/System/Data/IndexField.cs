using System;

namespace System.Data
{
	// Token: 0x02000098 RID: 152
	internal readonly struct IndexField
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x0002658E File Offset: 0x0002478E
		internal IndexField(DataColumn column, bool isDescending)
		{
			this.Column = column;
			this.IsDescending = isDescending;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0002659E File Offset: 0x0002479E
		public static bool operator ==(IndexField if1, IndexField if2)
		{
			return if1.Column == if2.Column && if1.IsDescending == if2.IsDescending;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000265BE File Offset: 0x000247BE
		public override bool Equals(object obj)
		{
			return obj is IndexField && this == (IndexField)obj;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000265DB File Offset: 0x000247DB
		public override int GetHashCode()
		{
			return this.Column.GetHashCode() ^ this.IsDescending.GetHashCode();
		}

		// Token: 0x04000301 RID: 769
		public readonly DataColumn Column;

		// Token: 0x04000302 RID: 770
		public readonly bool IsDescending;
	}
}
