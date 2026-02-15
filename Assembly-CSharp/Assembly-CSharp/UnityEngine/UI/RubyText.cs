using System;
using System.Collections.Generic;
using YgomSystem.LocalizedFont;

namespace UnityEngine.UI
{
	// Token: 0x02001176 RID: 4470
	public class RubyText : Text, ILocalizedFontOwner
	{
		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x060084AC RID: 33964 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060084AD RID: 33965 RVA: 0x0000216D File Offset: 0x0000036D
		public LocalizedFontSettingsBase.FontType localizedFontType
		{
			get
			{
				return LocalizedFontSettingsBase.FontType.Other;
			}
			set
			{
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x060084AE RID: 33966 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060084AF RID: 33967 RVA: 0x0000216D File Offset: 0x0000036D
		public int localizedFontMaterialIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x060084B0 RID: 33968 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x060084B1 RID: 33969 RVA: 0x0000216D File Offset: 0x0000036D
		public float rubyScale
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x060084B2 RID: 33970 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060084B3 RID: 33971 RVA: 0x0000216D File Offset: 0x0000036D
		public bool rubyFitWidth
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x060084B4 RID: 33972 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float preferredWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x060084B5 RID: 33973 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float preferredHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x060084B6 RID: 33974 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Start()
		{
		}

		// Token: 0x060084B7 RID: 33975 RVA: 0x0000216A File Offset: 0x0000036A
		private string parseRubyTag(string sourceText, string rubyTag, string rtTag, string endTag, List<RubyText.RubyInfo> infos)
		{
			return null;
		}

		// Token: 0x060084B8 RID: 33976 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
		}

		// Token: 0x0400C033 RID: 49203
		[SerializeField]
		private float rubyScaleFactor;

		// Token: 0x0400C034 RID: 49204
		private bool rubyFit;

		// Token: 0x0400C035 RID: 49205
		[SerializeField]
		private LocalizedFontSettingsBase.FontType m_localizedFontType;

		// Token: 0x0400C036 RID: 49206
		[SerializeField]
		private int m_localizedFontMaterialIndex;

		// Token: 0x0400C037 RID: 49207
		private readonly UIVertex[] m_TempVerts;

		// Token: 0x0400C038 RID: 49208
		private List<RubyText.RubyInfo> rubyInfos;

		// Token: 0x0400C039 RID: 49209
		private const string tagRuby = "$R";

		// Token: 0x0400C03A RID: 49210
		private const string tagRubyBegin = "(";

		// Token: 0x0400C03B RID: 49211
		private const string tagRubyEnd = ")";

		// Token: 0x0400C03C RID: 49212
		private float minX;

		// Token: 0x0400C03D RID: 49213
		private float maxX;

		// Token: 0x02001177 RID: 4471
		private struct RubyInfo
		{
			// Token: 0x0400C03E RID: 49214
			public string rubyText;

			// Token: 0x0400C03F RID: 49215
			public int noRubyIndex;

			// Token: 0x0400C040 RID: 49216
			public int baseTextLen;
		}
	}
}
