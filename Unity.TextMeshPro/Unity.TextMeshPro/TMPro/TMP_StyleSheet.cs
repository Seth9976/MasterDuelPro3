using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200007B RID: 123
	[ExcludeFromPreset]
	[Serializable]
	public class TMP_StyleSheet : ScriptableObject
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00014C4A File Offset: 0x00012E4A
		internal List<TMP_Style> styles
		{
			get
			{
				return this.m_StyleList;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00014C52 File Offset: 0x00012E52
		private void Reset()
		{
			this.LoadStyleDictionaryInternal();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00014C5C File Offset: 0x00012E5C
		public TMP_Style GetStyle(int hashCode)
		{
			if (this.m_StyleLookupDictionary == null)
			{
				this.LoadStyleDictionaryInternal();
			}
			TMP_Style style;
			if (this.m_StyleLookupDictionary.TryGetValue(hashCode, out style))
			{
				return style;
			}
			return null;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00014C8C File Offset: 0x00012E8C
		public TMP_Style GetStyle(string name)
		{
			if (this.m_StyleLookupDictionary == null)
			{
				this.LoadStyleDictionaryInternal();
			}
			int hashCode = TMP_TextParsingUtilities.GetHashCode(name);
			TMP_Style style;
			if (this.m_StyleLookupDictionary.TryGetValue(hashCode, out style))
			{
				return style;
			}
			return null;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00014C52 File Offset: 0x00012E52
		public void RefreshStyles()
		{
			this.LoadStyleDictionaryInternal();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00014CC4 File Offset: 0x00012EC4
		private void LoadStyleDictionaryInternal()
		{
			if (this.m_StyleLookupDictionary == null)
			{
				this.m_StyleLookupDictionary = new Dictionary<int, TMP_Style>();
			}
			else
			{
				this.m_StyleLookupDictionary.Clear();
			}
			for (int i = 0; i < this.m_StyleList.Count; i++)
			{
				this.m_StyleList[i].RefreshStyle();
				if (!this.m_StyleLookupDictionary.ContainsKey(this.m_StyleList[i].hashCode))
				{
					this.m_StyleLookupDictionary.Add(this.m_StyleList[i].hashCode, this.m_StyleList[i]);
				}
			}
			int normalStyleHashCode = TMP_TextParsingUtilities.GetHashCode("Normal");
			if (!this.m_StyleLookupDictionary.ContainsKey(normalStyleHashCode))
			{
				TMP_Style style = new TMP_Style("Normal", string.Empty, string.Empty);
				this.m_StyleList.Add(style);
				this.m_StyleLookupDictionary.Add(normalStyleHashCode, style);
			}
		}

		// Token: 0x040003AD RID: 941
		[SerializeField]
		private List<TMP_Style> m_StyleList = new List<TMP_Style>(1);

		// Token: 0x040003AE RID: 942
		private Dictionary<int, TMP_Style> m_StyleLookupDictionary;
	}
}
