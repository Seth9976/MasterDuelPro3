using System;
using UnityEngine;

namespace YgomGame.Utility
{
	// Token: 0x02000826 RID: 2086
	[Serializable]
	public class GlobalTextData
	{
		// Token: 0x0600404A RID: 16458 RVA: 0x0000216D File Offset: 0x0000036D
		public void Clear()
		{
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x0000216D File Offset: 0x0000036D
		private void ParseTextId()
		{
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetTextGroup()
		{
			return null;
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetText()
		{
			return null;
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetConvertedText()
		{
			return null;
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTextId<T>(T textId)
		{
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetRawText()
		{
			return null;
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRawText(string rawText)
		{
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x0000216A File Offset: 0x0000036A
		public GlobalTextData Copy()
		{
			return null;
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x0000216D File Offset: 0x0000036D
		public void CopyTo(GlobalTextData target)
		{
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetLangText(string langKey)
		{
			return null;
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x0000216A File Offset: 0x0000036A
		public object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04003958 RID: 14680
		public bool useId;

		// Token: 0x04003959 RID: 14681
		public string tid;

		// Token: 0x0400395A RID: 14682
		[TextArea]
		public string ja_JP;

		// Token: 0x0400395B RID: 14683
		[TextArea]
		public string en_US;

		// Token: 0x0400395C RID: 14684
		[TextArea]
		public string fr_FR;

		// Token: 0x0400395D RID: 14685
		[TextArea]
		public string it_IT;

		// Token: 0x0400395E RID: 14686
		[TextArea]
		public string de_DE;

		// Token: 0x0400395F RID: 14687
		[TextArea]
		public string es_ES;

		// Token: 0x04003960 RID: 14688
		[TextArea]
		public string pt_BR;

		// Token: 0x04003961 RID: 14689
		[TextArea]
		public string ko_KR;

		// Token: 0x04003962 RID: 14690
		[TextArea]
		public string zh_TW;

		// Token: 0x04003963 RID: 14691
		[TextArea]
		public string zh_CN;

		// Token: 0x04003964 RID: 14692
		private string m_TextGroup;

		// Token: 0x04003965 RID: 14693
		private string m_TextId;
	}
}
