using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000069 RID: 105
	[Serializable]
	public class UnicodeLineBreakingRules
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0003002C File Offset: 0x0002E22C
		internal HashSet<uint> leadingCharactersLookup
		{
			get
			{
				bool flag = this.m_LeadingCharactersLookup == null;
				if (flag)
				{
					this.LoadLineBreakingRules();
				}
				return this.m_LeadingCharactersLookup;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00030058 File Offset: 0x0002E258
		internal HashSet<uint> followingCharactersLookup
		{
			get
			{
				bool flag = this.m_LeadingCharactersLookup == null;
				if (flag)
				{
					this.LoadLineBreakingRules();
				}
				return this.m_FollowingCharactersLookup;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00030084 File Offset: 0x0002E284
		public bool useModernHangulLineBreakingRules
		{
			get
			{
				return this.m_UseModernHangulLineBreakingRules;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0003008C File Offset: 0x0002E28C
		internal void LoadLineBreakingRules()
		{
			bool flag = this.m_LeadingCharactersLookup == null;
			if (flag)
			{
				bool flag2 = this.m_LeadingCharacters == null;
				if (flag2)
				{
					this.m_LeadingCharacters = Resources.Load<TextAsset>("LineBreaking Leading Characters");
				}
				this.m_LeadingCharactersLookup = ((this.m_LeadingCharacters != null) ? UnicodeLineBreakingRules.GetCharacters(this.m_LeadingCharacters) : new HashSet<uint>());
				bool flag3 = this.m_FollowingCharacters == null;
				if (flag3)
				{
					this.m_FollowingCharacters = Resources.Load<TextAsset>("LineBreaking Following Characters");
				}
				this.m_FollowingCharactersLookup = ((this.m_FollowingCharacters != null) ? UnicodeLineBreakingRules.GetCharacters(this.m_FollowingCharacters) : new HashSet<uint>());
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00030138 File Offset: 0x0002E338
		private static HashSet<uint> GetCharacters(TextAsset file)
		{
			HashSet<uint> ruleSet = new HashSet<uint>();
			string text = file.text;
			for (int i = 0; i < text.Length; i++)
			{
				ruleSet.Add((uint)text[i]);
			}
			return ruleSet;
		}

		// Token: 0x040004A0 RID: 1184
		[SerializeField]
		private TextAsset m_UnicodeLineBreakingRules;

		// Token: 0x040004A1 RID: 1185
		[SerializeField]
		private TextAsset m_LeadingCharacters;

		// Token: 0x040004A2 RID: 1186
		[SerializeField]
		private TextAsset m_FollowingCharacters;

		// Token: 0x040004A3 RID: 1187
		[SerializeField]
		private bool m_UseModernHangulLineBreakingRules;

		// Token: 0x040004A4 RID: 1188
		private HashSet<uint> m_LeadingCharactersLookup;

		// Token: 0x040004A5 RID: 1189
		private HashSet<uint> m_FollowingCharactersLookup;
	}
}
