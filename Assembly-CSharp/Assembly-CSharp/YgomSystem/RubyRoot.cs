using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x020004BD RID: 1213
	public class RubyRoot : MonoBehaviour
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform rectTransform
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x000029CC File Offset: 0x00000BCC
		public static RubyRoot.Lang GetLang(string locale)
		{
			return RubyRoot.Lang.JP;
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetText(string str, RubyRoot.Lang lang, int fontsize, Color col)
		{
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupText(string str, RubyRoot.Lang lang)
		{
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x0000216D File Offset: 0x0000036D
		public void RebuildRuby()
		{
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFont(RubyRoot.Lang lang)
		{
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetFontSize(int size)
		{
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetColor(Color col)
		{
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetBaseTextWidth()
		{
			return 0f;
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000F16EE File Offset: 0x000EF8EE
		private void parseRubyTag(string sourceText, out string baseStr, out string rubyStr, string rubyTag = "$R", string rtTag = "(", string endTag = ")")
		{
			baseStr = null;
			rubyStr = null;
		}

		// Token: 0x040027F2 RID: 10226
		[SerializeField]
		private int fontSize;

		// Token: 0x040027F3 RID: 10227
		[SerializeField]
		public List<RubyRoot.RubyInfo> rubyInfoList;

		// Token: 0x040027F4 RID: 10228
		[SerializeField]
		private RubyTextEx rubyText;

		// Token: 0x040027F5 RID: 10229
		[SerializeField]
		private RubyTextEx baseText;

		// Token: 0x040027F6 RID: 10230
		[SerializeField]
		private float rubySizeRatio;

		// Token: 0x040027F7 RID: 10231
		private const string tagRuby = "$R";

		// Token: 0x040027F8 RID: 10232
		private const string tagRubyBegin = "(";

		// Token: 0x040027F9 RID: 10233
		private const string tagRubyEnd = ")";

		// Token: 0x040027FA RID: 10234
		private RectTransform _rt;

		// Token: 0x020004BE RID: 1214
		public enum Lang
		{
			// Token: 0x040027FC RID: 10236
			JP,
			// Token: 0x040027FD RID: 10237
			EN,
			// Token: 0x040027FE RID: 10238
			KR,
			// Token: 0x040027FF RID: 10239
			CN,
			// Token: 0x04002800 RID: 10240
			TW
		}

		// Token: 0x020004BF RID: 1215
		public class RubyInfo
		{
			// Token: 0x04002801 RID: 10241
			public int rubyTextIdx;

			// Token: 0x04002802 RID: 10242
			public int rubyCount;

			// Token: 0x04002803 RID: 10243
			public int baseTextIdx;

			// Token: 0x04002804 RID: 10244
			public int wordCount;

			// Token: 0x04002805 RID: 10245
			public float wordPos;

			// Token: 0x04002806 RID: 10246
			public float wordWidth;

			// Token: 0x04002807 RID: 10247
			public string word;
		}
	}
}
