using System;

namespace TMPro
{
	// Token: 0x020000AD RID: 173
	public struct TMP_LinkInfo
	{
		// Token: 0x06000643 RID: 1603 RVA: 0x0002EB98 File Offset: 0x0002CD98
		internal void SetLinkID(char[] text, int startIndex, int length)
		{
			if (this.linkID == null || this.linkID.Length < length)
			{
				this.linkID = new char[length];
			}
			for (int i = 0; i < length; i++)
			{
				this.linkID[i] = text[startIndex + i];
			}
			this.linkIdLength = length;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0002EBE4 File Offset: 0x0002CDE4
		public string GetLinkText()
		{
			string text = string.Empty;
			TMP_TextInfo textInfo = this.textComponent.textInfo;
			for (int i = this.linkTextfirstCharacterIndex; i < this.linkTextfirstCharacterIndex + this.linkTextLength; i++)
			{
				text += textInfo.characterInfo[i].character.ToString();
			}
			return text;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0002EC3E File Offset: 0x0002CE3E
		public string GetLink()
		{
			return this.GetLinkID();
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0002EC46 File Offset: 0x0002CE46
		public string GetLinkID()
		{
			if (this.textComponent == null)
			{
				return string.Empty;
			}
			return new string(this.linkID, 0, this.linkIdLength);
		}

		// Token: 0x040005C7 RID: 1479
		public TMP_Text textComponent;

		// Token: 0x040005C8 RID: 1480
		public int hashCode;

		// Token: 0x040005C9 RID: 1481
		public int linkIdFirstCharacterIndex;

		// Token: 0x040005CA RID: 1482
		public int linkIdLength;

		// Token: 0x040005CB RID: 1483
		public int linkTextfirstCharacterIndex;

		// Token: 0x040005CC RID: 1484
		public int linkTextLength;

		// Token: 0x040005CD RID: 1485
		internal char[] linkID;
	}
}
