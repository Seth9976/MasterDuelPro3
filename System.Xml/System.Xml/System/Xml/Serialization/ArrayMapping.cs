using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000167 RID: 359
	internal class ArrayMapping : TypeMapping
	{
		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x00054112 File Offset: 0x00052312
		// (set) Token: 0x06001149 RID: 4425 RVA: 0x0005411A File Offset: 0x0005231A
		internal ElementAccessor[] Elements
		{
			get
			{
				return this.elements;
			}
			set
			{
				this.elements = value;
				this.sortedElements = null;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0005412C File Offset: 0x0005232C
		internal ElementAccessor[] ElementsSortedByDerivation
		{
			get
			{
				if (this.sortedElements != null)
				{
					return this.sortedElements;
				}
				if (this.elements == null)
				{
					return null;
				}
				this.sortedElements = new ElementAccessor[this.elements.Length];
				Array.Copy(this.elements, 0, this.sortedElements, 0, this.elements.Length);
				AccessorMapping.SortMostToLeastDerived(this.sortedElements);
				return this.sortedElements;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00054191 File Offset: 0x00052391
		// (set) Token: 0x0600114C RID: 4428 RVA: 0x00054199 File Offset: 0x00052399
		internal ArrayMapping Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x000541A2 File Offset: 0x000523A2
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x000541AA File Offset: 0x000523AA
		internal StructMapping TopLevelMapping
		{
			get
			{
				return this.topLevelMapping;
			}
			set
			{
				this.topLevelMapping = value;
			}
		}

		// Token: 0x04000849 RID: 2121
		private ElementAccessor[] elements;

		// Token: 0x0400084A RID: 2122
		private ElementAccessor[] sortedElements;

		// Token: 0x0400084B RID: 2123
		private ArrayMapping next;

		// Token: 0x0400084C RID: 2124
		private StructMapping topLevelMapping;
	}
}
