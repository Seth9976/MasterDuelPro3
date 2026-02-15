using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000018 RID: 24
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule", "UnityEditor.TextCoreTextEngineModule" })]
	[UsedByNativeCode]
	[Serializable]
	internal struct LigatureSubstitutionRecord : IEquatable<LigatureSubstitutionRecord>
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000039F8 File Offset: 0x00001BF8
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003A10 File Offset: 0x00001C10
		public uint[] componentGlyphIDs
		{
			get
			{
				return this.m_ComponentGlyphIDs;
			}
			set
			{
				this.m_ComponentGlyphIDs = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003A1C File Offset: 0x00001C1C
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003A34 File Offset: 0x00001C34
		public uint ligatureGlyphID
		{
			get
			{
				return this.m_LigatureGlyphID;
			}
			set
			{
				this.m_LigatureGlyphID = value;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003A40 File Offset: 0x00001C40
		public bool Equals(LigatureSubstitutionRecord other)
		{
			return this == other;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003A60 File Offset: 0x00001C60
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is LigatureSubstitutionRecord)
			{
				LigatureSubstitutionRecord other = (LigatureSubstitutionRecord)obj;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003A8C File Offset: 0x00001C8C
		public override int GetHashCode()
		{
			return this.m_ComponentGlyphIDs.GetHashCode();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003AAC File Offset: 0x00001CAC
		public static bool operator ==(LigatureSubstitutionRecord lhs, LigatureSubstitutionRecord rhs)
		{
			bool flag = lhs.componentGlyphIDs != null && rhs.componentGlyphIDs != null;
			if (flag)
			{
				int lhsComponentCount = lhs.m_ComponentGlyphIDs.Length;
				bool flag2 = lhsComponentCount != rhs.m_ComponentGlyphIDs.Length;
				if (flag2)
				{
					return false;
				}
				for (int i = 0; i < lhsComponentCount; i++)
				{
					bool flag3 = lhs.m_ComponentGlyphIDs[i] != rhs.m_ComponentGlyphIDs[i];
					if (flag3)
					{
						return false;
					}
				}
			}
			else
			{
				bool flag4 = lhs.componentGlyphIDs != null || rhs.componentGlyphIDs != null;
				if (flag4)
				{
					return false;
				}
			}
			return lhs.ligatureGlyphID == rhs.m_LigatureGlyphID;
		}

		// Token: 0x0400008B RID: 139
		[SerializeField]
		[NativeName("componentGlyphs")]
		private uint[] m_ComponentGlyphIDs;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		[NativeName("ligatureGlyph")]
		private uint m_LigatureGlyphID;
	}
}
