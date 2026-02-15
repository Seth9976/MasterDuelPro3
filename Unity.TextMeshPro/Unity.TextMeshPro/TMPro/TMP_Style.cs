using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200007A RID: 122
	[Serializable]
	public class TMP_Style
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00014B0C File Offset: 0x00012D0C
		public static TMP_Style NormalStyle
		{
			get
			{
				if (TMP_Style.k_NormalStyle == null)
				{
					TMP_Style.k_NormalStyle = new TMP_Style("Normal", string.Empty, string.Empty);
				}
				return TMP_Style.k_NormalStyle;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00014B33 File Offset: 0x00012D33
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00014B3B File Offset: 0x00012D3B
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				if (value != this.m_Name)
				{
					this.m_Name = value;
				}
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00014B52 File Offset: 0x00012D52
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x00014B5A File Offset: 0x00012D5A
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
			set
			{
				if (value != this.m_HashCode)
				{
					this.m_HashCode = value;
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00014B6C File Offset: 0x00012D6C
		public string styleOpeningDefinition
		{
			get
			{
				return this.m_OpeningDefinition;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00014B74 File Offset: 0x00012D74
		public string styleClosingDefinition
		{
			get
			{
				return this.m_ClosingDefinition;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00014B7C File Offset: 0x00012D7C
		public uint[] styleOpeningTagArray
		{
			get
			{
				return this.m_OpeningTagArray;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00014B84 File Offset: 0x00012D84
		public uint[] styleClosingTagArray
		{
			get
			{
				return this.m_ClosingTagArray;
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00014B8C File Offset: 0x00012D8C
		internal TMP_Style(string styleName, string styleOpeningDefinition, string styleClosingDefinition)
		{
			this.m_Name = styleName;
			this.m_HashCode = TMP_TextParsingUtilities.GetHashCode(styleName);
			this.m_OpeningDefinition = styleOpeningDefinition;
			this.m_ClosingDefinition = styleClosingDefinition;
			this.RefreshStyle();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00014BBC File Offset: 0x00012DBC
		public void RefreshStyle()
		{
			this.m_HashCode = TMP_TextParsingUtilities.GetHashCode(this.m_Name);
			int s = this.m_OpeningDefinition.Length;
			this.m_OpeningTagArray = new uint[s];
			for (int i = 0; i < s; i++)
			{
				this.m_OpeningTagArray[i] = (uint)this.m_OpeningDefinition[i];
			}
			int s2 = this.m_ClosingDefinition.Length;
			this.m_ClosingTagArray = new uint[s2];
			for (int j = 0; j < s2; j++)
			{
				this.m_ClosingTagArray[j] = (uint)this.m_ClosingDefinition[j];
			}
		}

		// Token: 0x040003A6 RID: 934
		internal static TMP_Style k_NormalStyle;

		// Token: 0x040003A7 RID: 935
		[SerializeField]
		private string m_Name;

		// Token: 0x040003A8 RID: 936
		[SerializeField]
		private int m_HashCode;

		// Token: 0x040003A9 RID: 937
		[SerializeField]
		private string m_OpeningDefinition;

		// Token: 0x040003AA RID: 938
		[SerializeField]
		private string m_ClosingDefinition;

		// Token: 0x040003AB RID: 939
		[SerializeField]
		private uint[] m_OpeningTagArray;

		// Token: 0x040003AC RID: 940
		[SerializeField]
		private uint[] m_ClosingTagArray;
	}
}
