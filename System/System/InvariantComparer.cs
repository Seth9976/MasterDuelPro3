using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x020000EF RID: 239
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x00011E96 File Offset: 0x00010096
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00011EB0 File Offset: 0x000100B0
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this.m_compareInfo.Compare(text, text2);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x0400038E RID: 910
		private CompareInfo m_compareInfo;

		// Token: 0x0400038F RID: 911
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
