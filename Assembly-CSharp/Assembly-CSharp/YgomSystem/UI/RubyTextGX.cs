using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.YGomTMPro;

namespace YgomSystem.UI
{
	// Token: 0x020005B9 RID: 1465
	public class RubyTextGX : ExtendedTextMeshProUGUI
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x06002E30 RID: 11824 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002E32 RID: 11826 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x0000216A File Offset: 0x0000036A
		private List<RubyTextGX.RubyInfo> m_RubyInfoList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x0000216A File Offset: 0x0000036A
		private string m_ParsedText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x0000216A File Offset: 0x0000036A
		private string m_ParsedTextWithRuby
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float preferredWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06002E37 RID: 11831 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float preferredHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002E39 RID: 11833 RVA: 0x0000216D File Offset: 0x0000036D
		public override string text
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Awake()
		{
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x0000216D File Offset: 0x0000036D
		protected void OnPopulateRubyText(TMP_TextInfo tmpinfo)
		{
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x0000216D File Offset: 0x0000036D
		private void GenerateMeshModifyInfoData(TMP_TextInfo tmpinfo)
		{
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTmpInfo(TMP_TextInfo tmpinfo, float baseY, float ofs, float word_ofs, float px, float pw, float ph, float cy, float rubyScale, int index, int idx, float d, TMP_TextInfoExtended.VERTEXINDEX pos)
		{
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateTmpInfo2(TMP_TextInfo tmpinfo, float baseY, float ofs, float px, float pw, float ph, float cy, float rubyScale, int index, float d, TMP_TextInfoExtended.VERTEXINDEX pos)
		{
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddUpdateData(int index, TMP_TextInfoExtended.VERTEXINDEX pos, Vector3 uiv)
		{
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateRubyTextInfo(string rawstr)
		{
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x0000216A File Offset: 0x0000036A
		private string parseRubyTag(string sourceText, string rubyTag, string rtTag, string endTag, List<RubyTextGX.RubyInfo> infos)
		{
			return null;
		}

		// Token: 0x04002BDB RID: 11227
		private const string tagRuby = "$R";

		// Token: 0x04002BDC RID: 11228
		private const string tagRubyBegin = "(";

		// Token: 0x04002BDD RID: 11229
		private const string tagRubyEnd = ")";

		// Token: 0x04002BDE RID: 11230
		[SerializeField]
		private float rubyScaleFactor;

		// Token: 0x04002BDF RID: 11231
		private RubyTextGX.RubyTextInfo m_RubyTextInfo;

		// Token: 0x04002BE0 RID: 11232
		private List<ValueTuple<int, TMP_TextInfoExtended.VERTEXINDEX, Vector3>> m_MeshModifyInfoList;

		// Token: 0x04002BE1 RID: 11233
		private bool m_ReCalculated;

		// Token: 0x04002BE2 RID: 11234
		private bool m_RubyFit;

		// Token: 0x04002BE3 RID: 11235
		private float m_MeshRectMinX;

		// Token: 0x04002BE4 RID: 11236
		private float m_MeshRectMaxX;

		// Token: 0x04002BE5 RID: 11237
		private float m_MeshRectMinY;

		// Token: 0x04002BE6 RID: 11238
		private float m_MeshRectMaxY;

		// Token: 0x04002BE7 RID: 11239
		private ContentSizeFitter m_ContentSizeFilter;

		// Token: 0x04002BE8 RID: 11240
		private float m_PreferredWidthWithRuby;

		// Token: 0x04002BE9 RID: 11241
		private float m_PreferredHeightWithRuby;

		// Token: 0x020005BA RID: 1466
		private struct RubyInfo
		{
			// Token: 0x04002BEA RID: 11242
			public string rubyText;

			// Token: 0x04002BEB RID: 11243
			public int noRubyIndex;

			// Token: 0x04002BEC RID: 11244
			public int baseTextLen;
		}

		// Token: 0x020005BB RID: 1467
		private struct RubyTextInfo
		{
			// Token: 0x04002BED RID: 11245
			public List<RubyTextGX.RubyInfo> rubyInfoList;

			// Token: 0x04002BEE RID: 11246
			public string parsedText;

			// Token: 0x04002BEF RID: 11247
			public string parsedTextWithRuby;
		}
	}
}
