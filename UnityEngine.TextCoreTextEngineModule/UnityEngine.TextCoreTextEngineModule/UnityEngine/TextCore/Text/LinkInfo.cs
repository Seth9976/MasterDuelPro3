using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001E RID: 30
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal struct LinkInfo
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00004058 File Offset: 0x00002258
		internal void SetLinkId(char[] text, int startIndex, int length)
		{
			bool flag = this.linkId == null || this.linkId.Length < length;
			if (flag)
			{
				this.linkId = new char[length];
			}
			for (int i = 0; i < length; i++)
			{
				this.linkId[i] = text[startIndex + i];
			}
			this.linkIdLength = length;
			this.m_LinkIdString = null;
			this.m_LinkTextString = null;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000040C0 File Offset: 0x000022C0
		public string GetLinkText(TextInfo textInfo)
		{
			bool flag = string.IsNullOrEmpty(this.m_LinkTextString);
			if (flag)
			{
				for (int i = this.linkTextfirstCharacterIndex; i < this.linkTextfirstCharacterIndex + this.linkTextLength; i++)
				{
					this.m_LinkTextString += ((char)textInfo.textElementInfo[i].character).ToString();
				}
			}
			return this.m_LinkTextString;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004138 File Offset: 0x00002338
		public string GetLinkId()
		{
			bool flag = string.IsNullOrEmpty(this.m_LinkIdString);
			if (flag)
			{
				this.m_LinkIdString = new string(this.linkId, 0, this.linkIdLength);
			}
			return this.m_LinkIdString;
		}

		// Token: 0x0400009C RID: 156
		public int hashCode;

		// Token: 0x0400009D RID: 157
		public int linkIdFirstCharacterIndex;

		// Token: 0x0400009E RID: 158
		public int linkIdLength;

		// Token: 0x0400009F RID: 159
		public int linkTextfirstCharacterIndex;

		// Token: 0x040000A0 RID: 160
		public int linkTextLength;

		// Token: 0x040000A1 RID: 161
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal char[] linkId;

		// Token: 0x040000A2 RID: 162
		private string m_LinkIdString;

		// Token: 0x040000A3 RID: 163
		private string m_LinkTextString;
	}
}
