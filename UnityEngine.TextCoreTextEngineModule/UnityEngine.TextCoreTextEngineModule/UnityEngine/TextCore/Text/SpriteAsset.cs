using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000030 RID: 48
	[HelpURL("https://docs.unity3d.com/2023.3/Documentation/Manual/UIE-sprite.html")]
	[ExcludeFromPreset]
	public class SpriteAsset : TextAsset
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000A294 File Offset: 0x00008494
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000A2AC File Offset: 0x000084AC
		public FaceInfo faceInfo
		{
			get
			{
				return this.m_FaceInfo;
			}
			internal set
			{
				this.m_FaceInfo = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000A2B8 File Offset: 0x000084B8
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000A2D0 File Offset: 0x000084D0
		public Texture spriteSheet
		{
			get
			{
				return this.m_SpriteAtlasTexture;
			}
			internal set
			{
				this.m_SpriteAtlasTexture = value;
				this.width = (float)this.m_SpriteAtlasTexture.width;
				this.height = (float)this.m_SpriteAtlasTexture.height;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000A300 File Offset: 0x00008500
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000A308 File Offset: 0x00008508
		internal float width { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000A311 File Offset: 0x00008511
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000A319 File Offset: 0x00008519
		internal float height { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000A324 File Offset: 0x00008524
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000A350 File Offset: 0x00008550
		public List<SpriteCharacter> spriteCharacterTable
		{
			get
			{
				bool flag = this.m_GlyphIndexLookup == null;
				if (flag)
				{
					this.UpdateLookupTables();
				}
				return this.m_SpriteCharacterTable;
			}
			internal set
			{
				this.m_SpriteCharacterTable = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000A35C File Offset: 0x0000855C
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000A388 File Offset: 0x00008588
		public Dictionary<uint, SpriteCharacter> spriteCharacterLookupTable
		{
			get
			{
				bool flag = this.m_SpriteCharacterLookup == null;
				if (flag)
				{
					this.UpdateLookupTables();
				}
				return this.m_SpriteCharacterLookup;
			}
			internal set
			{
				this.m_SpriteCharacterLookup = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000A394 File Offset: 0x00008594
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000A3AC File Offset: 0x000085AC
		public List<SpriteGlyph> spriteGlyphTable
		{
			get
			{
				return this.m_SpriteGlyphTable;
			}
			internal set
			{
				this.m_SpriteGlyphTable = value;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000A3B6 File Offset: 0x000085B6
		private void Awake()
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A3BC File Offset: 0x000085BC
		public void UpdateLookupTables()
		{
			this.width = (float)this.m_SpriteAtlasTexture.width;
			this.height = (float)this.m_SpriteAtlasTexture.height;
			bool flag = this.m_GlyphIndexLookup == null;
			if (flag)
			{
				this.m_GlyphIndexLookup = new Dictionary<uint, int>();
			}
			else
			{
				this.m_GlyphIndexLookup.Clear();
			}
			bool flag2 = this.m_SpriteGlyphLookup == null;
			if (flag2)
			{
				this.m_SpriteGlyphLookup = new Dictionary<uint, SpriteGlyph>();
			}
			else
			{
				this.m_SpriteGlyphLookup.Clear();
			}
			for (int i = 0; i < this.m_SpriteGlyphTable.Count; i++)
			{
				SpriteGlyph spriteGlyph = this.m_SpriteGlyphTable[i];
				uint glyphIndex = spriteGlyph.index;
				bool flag3 = !this.m_GlyphIndexLookup.ContainsKey(glyphIndex);
				if (flag3)
				{
					this.m_GlyphIndexLookup.Add(glyphIndex, i);
				}
				bool flag4 = !this.m_SpriteGlyphLookup.ContainsKey(glyphIndex);
				if (flag4)
				{
					this.m_SpriteGlyphLookup.Add(glyphIndex, spriteGlyph);
				}
			}
			bool flag5 = this.m_NameLookup == null;
			if (flag5)
			{
				this.m_NameLookup = new Dictionary<int, int>();
			}
			else
			{
				this.m_NameLookup.Clear();
			}
			bool flag6 = this.m_SpriteCharacterLookup == null;
			if (flag6)
			{
				this.m_SpriteCharacterLookup = new Dictionary<uint, SpriteCharacter>();
			}
			else
			{
				this.m_SpriteCharacterLookup.Clear();
			}
			for (int j = 0; j < this.m_SpriteCharacterTable.Count; j++)
			{
				SpriteCharacter spriteCharacter = this.m_SpriteCharacterTable[j];
				bool flag7 = spriteCharacter == null;
				if (!flag7)
				{
					uint glyphIndex2 = spriteCharacter.glyphIndex;
					bool flag8 = !this.m_SpriteGlyphLookup.ContainsKey(glyphIndex2);
					if (!flag8)
					{
						spriteCharacter.glyph = this.m_SpriteGlyphLookup[glyphIndex2];
						spriteCharacter.textAsset = this;
						int nameHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_SpriteCharacterTable[j].name);
						bool flag9 = !this.m_NameLookup.ContainsKey(nameHashCode);
						if (flag9)
						{
							this.m_NameLookup.Add(nameHashCode, j);
						}
						uint unicode = this.m_SpriteCharacterTable[j].unicode;
						bool flag10 = unicode != 65534U && !this.m_SpriteCharacterLookup.ContainsKey(unicode);
						if (flag10)
						{
							this.m_SpriteCharacterLookup.Add(unicode, spriteCharacter);
						}
					}
				}
			}
			this.m_IsSpriteAssetLookupTablesDirty = false;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000A620 File Offset: 0x00008820
		public int GetSpriteIndexFromHashcode(int hashCode)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int index;
			bool flag2 = this.m_NameLookup.TryGetValue(hashCode, out index);
			int num;
			if (flag2)
			{
				num = index;
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A660 File Offset: 0x00008860
		public int GetSpriteIndexFromUnicode(uint unicode)
		{
			bool flag = this.m_SpriteCharacterLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			SpriteCharacter spriteCharacter;
			bool flag2 = this.m_SpriteCharacterLookup.TryGetValue(unicode, out spriteCharacter);
			int num;
			if (flag2)
			{
				num = (int)spriteCharacter.glyphIndex;
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A6A4 File Offset: 0x000088A4
		public int GetSpriteIndexFromName(string name)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int hashCode = TextUtilities.GetHashCodeCaseInSensitive(name);
			return this.GetSpriteIndexFromHashcode(hashCode);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A6D8 File Offset: 0x000088D8
		public static SpriteAsset SearchForSpriteByUnicode(SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			bool flag = spriteAsset == null;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteIndex = -1;
				spriteAsset2 = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					spriteAsset2 = spriteAsset;
				}
				else
				{
					bool flag3 = SpriteAsset.k_searchedSpriteAssets == null;
					if (flag3)
					{
						SpriteAsset.k_searchedSpriteAssets = new HashSet<int>();
					}
					else
					{
						SpriteAsset.k_searchedSpriteAssets.Clear();
					}
					int id = spriteAsset.GetInstanceID();
					SpriteAsset.k_searchedSpriteAssets.Add(id);
					bool flag4 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag4)
					{
						spriteAsset2 = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, out spriteIndex);
					}
					else
					{
						spriteIndex = -1;
						spriteAsset2 = null;
					}
				}
			}
			return spriteAsset2;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A788 File Offset: 0x00008988
		private static SpriteAsset SearchForSpriteByUnicodeInternal(List<SpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				SpriteAsset temp = spriteAssets[i];
				bool flag = temp == null;
				if (!flag)
				{
					int id = temp.GetInstanceID();
					bool flag2 = !SpriteAsset.k_searchedSpriteAssets.Add(id);
					if (!flag2)
					{
						temp = SpriteAsset.SearchForSpriteByUnicodeInternal(temp, unicode, includeFallbacks, out spriteIndex);
						bool flag3 = temp != null;
						if (flag3)
						{
							return temp;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A808 File Offset: 0x00008A08
		private static SpriteAsset SearchForSpriteByUnicodeInternal(SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			bool flag = spriteIndex != -1;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteAsset2 = spriteAsset;
			}
			else
			{
				bool flag2 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					spriteAsset2 = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					spriteAsset2 = null;
				}
			}
			return spriteAsset2;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A868 File Offset: 0x00008A68
		public static SpriteAsset SearchForSpriteByHashCode(SpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex, TextSettings textSettings = null)
		{
			bool flag = spriteAsset == null;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteIndex = -1;
				spriteAsset2 = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					spriteAsset2 = spriteAsset;
				}
				else
				{
					bool flag3 = SpriteAsset.k_searchedSpriteAssets == null;
					if (flag3)
					{
						SpriteAsset.k_searchedSpriteAssets = new HashSet<int>();
					}
					else
					{
						SpriteAsset.k_searchedSpriteAssets.Clear();
					}
					int id = spriteAsset.GetHashCode();
					SpriteAsset.k_searchedSpriteAssets.Add(id);
					bool flag4 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag4)
					{
						SpriteAsset tempSpriteAsset = SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, out spriteIndex);
						bool flag5 = spriteIndex != -1;
						if (flag5)
						{
							return tempSpriteAsset;
						}
					}
					bool flag6 = textSettings == null;
					if (flag6)
					{
						spriteIndex = -1;
						spriteAsset2 = null;
					}
					else
					{
						bool flag7 = includeFallbacks && textSettings.defaultSpriteAsset != null;
						if (flag7)
						{
							SpriteAsset tempSpriteAsset = SpriteAsset.SearchForSpriteByHashCodeInternal(textSettings.defaultSpriteAsset, hashCode, true, out spriteIndex);
							bool flag8 = spriteIndex != -1;
							if (flag8)
							{
								return tempSpriteAsset;
							}
						}
						SpriteAsset.k_searchedSpriteAssets.Clear();
						uint missingSpriteCharacterUnicode = textSettings.missingSpriteCharacterUnicode;
						spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(missingSpriteCharacterUnicode);
						bool flag9 = spriteIndex != -1;
						if (flag9)
						{
							spriteAsset2 = spriteAsset;
						}
						else
						{
							SpriteAsset.k_searchedSpriteAssets.Add(id);
							bool flag10 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
							if (flag10)
							{
								SpriteAsset tempSpriteAsset = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, missingSpriteCharacterUnicode, true, out spriteIndex);
								bool flag11 = spriteIndex != -1;
								if (flag11)
								{
									return tempSpriteAsset;
								}
							}
							bool flag12 = includeFallbacks && textSettings.defaultSpriteAsset != null;
							if (flag12)
							{
								SpriteAsset tempSpriteAsset = SpriteAsset.SearchForSpriteByUnicodeInternal(textSettings.defaultSpriteAsset, missingSpriteCharacterUnicode, true, out spriteIndex);
								bool flag13 = spriteIndex != -1;
								if (flag13)
								{
									return tempSpriteAsset;
								}
							}
							spriteIndex = -1;
							spriteAsset2 = null;
						}
					}
				}
			}
			return spriteAsset2;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000AA54 File Offset: 0x00008C54
		private static SpriteAsset SearchForSpriteByHashCodeInternal(List<SpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				SpriteAsset temp = spriteAssets[i];
				bool flag = temp == null;
				if (!flag)
				{
					int id = temp.GetHashCode();
					bool flag2 = !SpriteAsset.k_searchedSpriteAssets.Add(id);
					if (!flag2)
					{
						temp = SpriteAsset.SearchForSpriteByHashCodeInternal(temp, hashCode, searchFallbacks, out spriteIndex);
						bool flag3 = temp != null;
						if (flag3)
						{
							return temp;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		private static SpriteAsset SearchForSpriteByHashCodeInternal(SpriteAsset spriteAsset, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			bool flag = spriteIndex != -1;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteAsset2 = spriteAsset;
			}
			else
			{
				bool flag2 = searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					spriteAsset2 = SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					spriteAsset2 = null;
				}
			}
			return spriteAsset2;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000AB34 File Offset: 0x00008D34
		public void SortGlyphTable()
		{
			bool flag = this.m_SpriteGlyphTable == null || this.m_SpriteGlyphTable.Count == 0;
			if (!flag)
			{
				this.m_SpriteGlyphTable = this.m_SpriteGlyphTable.OrderBy((SpriteGlyph item) => item.index).ToList<SpriteGlyph>();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000AB98 File Offset: 0x00008D98
		internal void SortCharacterTable()
		{
			bool flag = this.m_SpriteCharacterTable != null && this.m_SpriteCharacterTable.Count > 0;
			if (flag)
			{
				this.m_SpriteCharacterTable = this.m_SpriteCharacterTable.OrderBy((SpriteCharacter c) => c.unicode).ToList<SpriteCharacter>();
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		internal void SortGlyphAndCharacterTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
		}

		// Token: 0x04000140 RID: 320
		internal Dictionary<int, int> m_NameLookup;

		// Token: 0x04000141 RID: 321
		internal Dictionary<uint, int> m_GlyphIndexLookup;

		// Token: 0x04000142 RID: 322
		[SerializeField]
		internal FaceInfo m_FaceInfo;

		// Token: 0x04000143 RID: 323
		[FormerlySerializedAs("spriteSheet")]
		[SerializeField]
		internal Texture m_SpriteAtlasTexture;

		// Token: 0x04000146 RID: 326
		[SerializeField]
		private List<SpriteCharacter> m_SpriteCharacterTable = new List<SpriteCharacter>();

		// Token: 0x04000147 RID: 327
		internal Dictionary<uint, SpriteCharacter> m_SpriteCharacterLookup;

		// Token: 0x04000148 RID: 328
		[SerializeField]
		private List<SpriteGlyph> m_SpriteGlyphTable = new List<SpriteGlyph>();

		// Token: 0x04000149 RID: 329
		internal Dictionary<uint, SpriteGlyph> m_SpriteGlyphLookup;

		// Token: 0x0400014A RID: 330
		[SerializeField]
		public List<SpriteAsset> fallbackSpriteAssets;

		// Token: 0x0400014B RID: 331
		internal bool m_IsSpriteAssetLookupTablesDirty = false;

		// Token: 0x0400014C RID: 332
		private static HashSet<int> k_searchedSpriteAssets;
	}
}
