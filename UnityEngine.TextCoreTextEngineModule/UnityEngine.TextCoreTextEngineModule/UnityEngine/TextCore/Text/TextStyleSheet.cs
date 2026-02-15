using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003A RID: 58
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	[Serializable]
	public class TextStyleSheet : ScriptableObject
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000B460 File Offset: 0x00009660
		internal List<TextStyle> styles
		{
			get
			{
				return this.m_StyleList;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000B478 File Offset: 0x00009678
		private void Reset()
		{
			this.LoadStyleDictionaryInternal();
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000B484 File Offset: 0x00009684
		public TextStyle GetStyle(int hashCode)
		{
			bool flag = this.m_StyleLookupDictionary == null;
			if (flag)
			{
				object obj = this.styleLookupLock;
				lock (obj)
				{
					bool flag3 = this.m_StyleLookupDictionary == null;
					if (flag3)
					{
						this.LoadStyleDictionaryInternal();
					}
				}
			}
			TextStyle style;
			bool flag4 = this.m_StyleLookupDictionary.TryGetValue(hashCode, out style);
			TextStyle textStyle;
			if (flag4)
			{
				textStyle = style;
			}
			else
			{
				textStyle = null;
			}
			return textStyle;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000B508 File Offset: 0x00009708
		public TextStyle GetStyle(string name)
		{
			bool flag = this.m_StyleLookupDictionary == null;
			if (flag)
			{
				this.LoadStyleDictionaryInternal();
			}
			int hashCode = TextUtilities.GetHashCodeCaseInSensitive(name);
			TextStyle style;
			bool flag2 = this.m_StyleLookupDictionary.TryGetValue(hashCode, out style);
			TextStyle textStyle;
			if (flag2)
			{
				textStyle = style;
			}
			else
			{
				textStyle = null;
			}
			return textStyle;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000B478 File Offset: 0x00009678
		public void RefreshStyles()
		{
			this.LoadStyleDictionaryInternal();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000B550 File Offset: 0x00009750
		private void LoadStyleDictionaryInternal()
		{
			Dictionary<int, TextStyle> styleLookup = this.m_StyleLookupDictionary;
			bool flag = styleLookup == null;
			if (flag)
			{
				styleLookup = new Dictionary<int, TextStyle>();
			}
			else
			{
				styleLookup.Clear();
			}
			for (int i = 0; i < this.m_StyleList.Count; i++)
			{
				this.m_StyleList[i].RefreshStyle();
				bool flag2 = !styleLookup.ContainsKey(this.m_StyleList[i].hashCode);
				if (flag2)
				{
					styleLookup.Add(this.m_StyleList[i].hashCode, this.m_StyleList[i]);
				}
			}
			int normalStyleHashCode = TextUtilities.GetHashCodeCaseInSensitive("Normal");
			bool flag3 = !styleLookup.ContainsKey(normalStyleHashCode);
			if (flag3)
			{
				TextStyle style = new TextStyle("Normal", string.Empty, string.Empty);
				this.m_StyleList.Add(style);
				styleLookup.Add(normalStyleHashCode, style);
			}
			this.m_StyleLookupDictionary = styleLookup;
		}

		// Token: 0x04000184 RID: 388
		[SerializeField]
		private List<TextStyle> m_StyleList = new List<TextStyle>(1);

		// Token: 0x04000185 RID: 389
		private Dictionary<int, TextStyle> m_StyleLookupDictionary;

		// Token: 0x04000186 RID: 390
		private object styleLookupLock = new object();
	}
}
