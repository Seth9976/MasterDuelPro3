using System;

namespace TMPro
{
	// Token: 0x020000AE RID: 174
	public struct TMP_WordInfo
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x0002EC70 File Offset: 0x0002CE70
		public string GetWord()
		{
			string word = string.Empty;
			TMP_CharacterInfo[] charInfo = this.textComponent.textInfo.characterInfo;
			for (int i = this.firstCharacterIndex; i < this.lastCharacterIndex + 1; i++)
			{
				word += charInfo[i].character.ToString();
			}
			return word;
		}

		// Token: 0x040005CE RID: 1486
		public TMP_Text textComponent;

		// Token: 0x040005CF RID: 1487
		public int firstCharacterIndex;

		// Token: 0x040005D0 RID: 1488
		public int lastCharacterIndex;

		// Token: 0x040005D1 RID: 1489
		public int characterCount;
	}
}
