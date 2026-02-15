using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000039 RID: 57
	[Serializable]
	public class TextStyle
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000B2FC File Offset: 0x000094FC
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000B314 File Offset: 0x00009514
		public uint[] styleOpeningTagArray
		{
			get
			{
				return this.m_OpeningTagArray;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000B32C File Offset: 0x0000952C
		public uint[] styleClosingTagArray
		{
			get
			{
				return this.m_ClosingTagArray;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000B344 File Offset: 0x00009544
		internal TextStyle(string styleName, string styleOpeningDefinition, string styleClosingDefinition)
		{
			this.m_Name = styleName;
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(styleName);
			this.m_OpeningDefinition = styleOpeningDefinition;
			this.m_ClosingDefinition = styleClosingDefinition;
			this.RefreshStyle();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000B378 File Offset: 0x00009578
		public void RefreshStyle()
		{
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Name);
			int s = this.m_OpeningDefinition.Length;
			this.m_OpeningTagArray = new uint[s];
			this.m_OpeningTagUnicodeArray = new uint[s];
			for (int i = 0; i < s; i++)
			{
				this.m_OpeningTagArray[i] = (uint)this.m_OpeningDefinition[i];
				this.m_OpeningTagUnicodeArray[i] = (uint)this.m_OpeningDefinition[i];
			}
			int s2 = this.m_ClosingDefinition.Length;
			this.m_ClosingTagArray = new uint[s2];
			this.m_ClosingTagUnicodeArray = new uint[s2];
			for (int j = 0; j < s2; j++)
			{
				this.m_ClosingTagArray[j] = (uint)this.m_ClosingDefinition[j];
				this.m_ClosingTagUnicodeArray[j] = (uint)this.m_ClosingDefinition[j];
			}
		}

		// Token: 0x0400017B RID: 379
		internal static TextStyle k_NormalStyle;

		// Token: 0x0400017C RID: 380
		[SerializeField]
		private string m_Name;

		// Token: 0x0400017D RID: 381
		[SerializeField]
		private int m_HashCode;

		// Token: 0x0400017E RID: 382
		[SerializeField]
		private string m_OpeningDefinition;

		// Token: 0x0400017F RID: 383
		[SerializeField]
		private string m_ClosingDefinition;

		// Token: 0x04000180 RID: 384
		[SerializeField]
		private uint[] m_OpeningTagArray;

		// Token: 0x04000181 RID: 385
		[SerializeField]
		private uint[] m_ClosingTagArray;

		// Token: 0x04000182 RID: 386
		[SerializeField]
		internal uint[] m_OpeningTagUnicodeArray;

		// Token: 0x04000183 RID: 387
		[SerializeField]
		internal uint[] m_ClosingTagUnicodeArray;
	}
}
