using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000076 RID: 118
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/TextMeshPro/Sprites.html")]
	[ExcludeFromPreset]
	public class TMP_SpriteAsset : TMP_Asset
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0001415D File Offset: 0x0001235D
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00014173 File Offset: 0x00012373
		public List<TMP_SpriteCharacter> spriteCharacterTable
		{
			get
			{
				if (this.m_GlyphIndexLookup == null)
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

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0001417C File Offset: 0x0001237C
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00014192 File Offset: 0x00012392
		public Dictionary<uint, TMP_SpriteCharacter> spriteCharacterLookupTable
		{
			get
			{
				if (this.m_SpriteCharacterLookup == null)
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

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0001419B File Offset: 0x0001239B
		// (set) Token: 0x060003AD RID: 941 RVA: 0x000141A3 File Offset: 0x000123A3
		public List<TMP_SpriteGlyph> spriteGlyphTable
		{
			get
			{
				return this.m_GlyphTable;
			}
			internal set
			{
				this.m_GlyphTable = value;
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000141AC File Offset: 0x000123AC
		private void Awake()
		{
			if (base.material != null && string.IsNullOrEmpty(this.m_Version))
			{
				this.UpgradeSpriteAsset();
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000141CF File Offset: 0x000123CF
		private Material GetDefaultSpriteMaterial()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			Material material = new Material(Shader.Find("TextMeshPro/Sprite"));
			material.SetTexture(ShaderUtilities.ID_MainTex, this.spriteSheet);
			return material;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000141F8 File Offset: 0x000123F8
		public void UpdateLookupTables()
		{
			if (base.material != null && string.IsNullOrEmpty(this.m_Version))
			{
				this.UpgradeSpriteAsset();
			}
			if (this.m_GlyphIndexLookup == null)
			{
				this.m_GlyphIndexLookup = new Dictionary<uint, int>();
			}
			else
			{
				this.m_GlyphIndexLookup.Clear();
			}
			if (this.m_SpriteGlyphLookup == null)
			{
				this.m_SpriteGlyphLookup = new Dictionary<uint, TMP_SpriteGlyph>();
			}
			else
			{
				this.m_SpriteGlyphLookup.Clear();
			}
			for (int i = 0; i < this.m_GlyphTable.Count; i++)
			{
				TMP_SpriteGlyph spriteGlyph = this.m_GlyphTable[i];
				uint glyphIndex = spriteGlyph.index;
				if (!this.m_GlyphIndexLookup.ContainsKey(glyphIndex))
				{
					this.m_GlyphIndexLookup.Add(glyphIndex, i);
				}
				if (!this.m_SpriteGlyphLookup.ContainsKey(glyphIndex))
				{
					this.m_SpriteGlyphLookup.Add(glyphIndex, spriteGlyph);
				}
			}
			if (this.m_NameLookup == null)
			{
				this.m_NameLookup = new Dictionary<int, int>();
			}
			else
			{
				this.m_NameLookup.Clear();
			}
			if (this.m_SpriteCharacterLookup == null)
			{
				this.m_SpriteCharacterLookup = new Dictionary<uint, TMP_SpriteCharacter>();
			}
			else
			{
				this.m_SpriteCharacterLookup.Clear();
			}
			for (int j = 0; j < this.m_SpriteCharacterTable.Count; j++)
			{
				TMP_SpriteCharacter spriteCharacter = this.m_SpriteCharacterTable[j];
				if (spriteCharacter != null)
				{
					uint glyphIndex2 = spriteCharacter.glyphIndex;
					if (this.m_SpriteGlyphLookup.ContainsKey(glyphIndex2))
					{
						spriteCharacter.glyph = this.m_SpriteGlyphLookup[glyphIndex2];
						spriteCharacter.textAsset = this;
						int nameHashCode = TMP_TextUtilities.GetHashCode(this.m_SpriteCharacterTable[j].name);
						if (!this.m_NameLookup.ContainsKey(nameHashCode))
						{
							this.m_NameLookup.Add(nameHashCode, j);
						}
						uint unicode = this.m_SpriteCharacterTable[j].unicode;
						if (unicode != 65534U && !this.m_SpriteCharacterLookup.ContainsKey(unicode))
						{
							this.m_SpriteCharacterLookup.Add(unicode, spriteCharacter);
						}
					}
				}
			}
			this.m_IsSpriteAssetLookupTablesDirty = false;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000143E4 File Offset: 0x000125E4
		public int GetSpriteIndexFromHashcode(int hashCode)
		{
			if (this.m_NameLookup == null)
			{
				this.UpdateLookupTables();
			}
			int index;
			if (this.m_NameLookup.TryGetValue(hashCode, out index))
			{
				return index;
			}
			return -1;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00014414 File Offset: 0x00012614
		public int GetSpriteIndexFromUnicode(uint unicode)
		{
			if (this.m_SpriteCharacterLookup == null)
			{
				this.UpdateLookupTables();
			}
			TMP_SpriteCharacter spriteCharacter;
			if (this.m_SpriteCharacterLookup.TryGetValue(unicode, out spriteCharacter))
			{
				return (int)spriteCharacter.glyphIndex;
			}
			return -1;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00014448 File Offset: 0x00012648
		public int GetSpriteIndexFromName(string name)
		{
			if (this.m_NameLookup == null)
			{
				this.UpdateLookupTables();
			}
			int hashCode = TMP_TextUtilities.GetSimpleHashCode(name);
			return this.GetSpriteIndexFromHashcode(hashCode);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00014474 File Offset: 0x00012674
		public static TMP_SpriteAsset SearchForSpriteByUnicode(TMP_SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			if (spriteAsset == null)
			{
				spriteIndex = -1;
				return null;
			}
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (TMP_SpriteAsset.k_searchedSpriteAssets == null)
			{
				TMP_SpriteAsset.k_searchedSpriteAssets = new HashSet<int>();
			}
			else
			{
				TMP_SpriteAsset.k_searchedSpriteAssets.Clear();
			}
			int id = spriteAsset.GetInstanceID();
			TMP_SpriteAsset.k_searchedSpriteAssets.Add(id);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, out spriteIndex);
			}
			if (includeFallbacks && TMP_Settings.defaultSpriteAsset != null)
			{
				return TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(TMP_Settings.defaultSpriteAsset, unicode, true, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001451C File Offset: 0x0001271C
		private static TMP_SpriteAsset SearchForSpriteByUnicodeInternal(List<TMP_SpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TMP_SpriteAsset temp = spriteAssets[i];
				if (!(temp == null))
				{
					int id = temp.GetInstanceID();
					if (TMP_SpriteAsset.k_searchedSpriteAssets.Add(id))
					{
						temp = TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(temp, unicode, includeFallbacks, out spriteIndex);
						if (temp != null)
						{
							return temp;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00014578 File Offset: 0x00012778
		private static TMP_SpriteAsset SearchForSpriteByUnicodeInternal(TMP_SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000145B8 File Offset: 0x000127B8
		public static TMP_SpriteAsset SearchForSpriteByHashCode(TMP_SpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex)
		{
			if (spriteAsset == null)
			{
				spriteIndex = -1;
				return null;
			}
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (TMP_SpriteAsset.k_searchedSpriteAssets == null)
			{
				TMP_SpriteAsset.k_searchedSpriteAssets = new HashSet<int>();
			}
			else
			{
				TMP_SpriteAsset.k_searchedSpriteAssets.Clear();
			}
			int id = spriteAsset.instanceID;
			TMP_SpriteAsset.k_searchedSpriteAssets.Add(id);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				TMP_SpriteAsset tempSpriteAsset = TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return tempSpriteAsset;
				}
			}
			if (includeFallbacks && TMP_Settings.defaultSpriteAsset != null)
			{
				TMP_SpriteAsset tempSpriteAsset = TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(TMP_Settings.defaultSpriteAsset, hashCode, true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return tempSpriteAsset;
				}
			}
			TMP_SpriteAsset.k_searchedSpriteAssets.Clear();
			uint missingSpriteCharacterUnicode = TMP_Settings.missingCharacterSpriteUnicode;
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(missingSpriteCharacterUnicode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			TMP_SpriteAsset.k_searchedSpriteAssets.Add(id);
			if (includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				TMP_SpriteAsset tempSpriteAsset = TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, missingSpriteCharacterUnicode, true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return tempSpriteAsset;
				}
			}
			if (includeFallbacks && TMP_Settings.defaultSpriteAsset != null)
			{
				TMP_SpriteAsset tempSpriteAsset = TMP_SpriteAsset.SearchForSpriteByUnicodeInternal(TMP_Settings.defaultSpriteAsset, missingSpriteCharacterUnicode, true, out spriteIndex);
				if (spriteIndex != -1)
				{
					return tempSpriteAsset;
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000146EC File Offset: 0x000128EC
		private static TMP_SpriteAsset SearchForSpriteByHashCodeInternal(List<TMP_SpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				TMP_SpriteAsset temp = spriteAssets[i];
				if (!(temp == null))
				{
					int id = temp.instanceID;
					if (TMP_SpriteAsset.k_searchedSpriteAssets.Add(id))
					{
						temp = TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(temp, hashCode, searchFallbacks, out spriteIndex);
						if (temp != null)
						{
							return temp;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00014748 File Offset: 0x00012948
		private static TMP_SpriteAsset SearchForSpriteByHashCodeInternal(TMP_SpriteAsset spriteAsset, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			if (spriteIndex != -1)
			{
				return spriteAsset;
			}
			if (searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0)
			{
				return TMP_SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, out spriteIndex);
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00014788 File Offset: 0x00012988
		public void SortGlyphTable()
		{
			if (this.m_GlyphTable == null || this.m_GlyphTable.Count == 0)
			{
				return;
			}
			this.m_GlyphTable = this.m_GlyphTable.OrderBy((TMP_SpriteGlyph item) => item.index).ToList<TMP_SpriteGlyph>();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000147E0 File Offset: 0x000129E0
		internal void SortCharacterTable()
		{
			if (this.m_SpriteCharacterTable != null && this.m_SpriteCharacterTable.Count > 0)
			{
				this.m_SpriteCharacterTable = this.m_SpriteCharacterTable.OrderBy((TMP_SpriteCharacter c) => c.unicode).ToList<TMP_SpriteCharacter>();
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00014838 File Offset: 0x00012A38
		internal void SortGlyphAndCharacterTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00014848 File Offset: 0x00012A48
		private void UpgradeSpriteAsset()
		{
			this.m_Version = "1.1.0";
			Debug.Log(string.Concat(new string[] { "Upgrading sprite asset [", base.name, "] to version ", this.m_Version, "." }), this);
			this.m_SpriteCharacterTable.Clear();
			this.m_GlyphTable.Clear();
			for (int i = 0; i < this.spriteInfoList.Count; i++)
			{
				TMP_Sprite oldSprite = this.spriteInfoList[i];
				TMP_SpriteGlyph spriteGlyph = new TMP_SpriteGlyph();
				spriteGlyph.index = (uint)i;
				spriteGlyph.sprite = oldSprite.sprite;
				spriteGlyph.metrics = new GlyphMetrics(oldSprite.width, oldSprite.height, oldSprite.xOffset, oldSprite.yOffset, oldSprite.xAdvance);
				spriteGlyph.glyphRect = new GlyphRect((int)oldSprite.x, (int)oldSprite.y, (int)oldSprite.width, (int)oldSprite.height);
				spriteGlyph.scale = 1f;
				spriteGlyph.atlasIndex = 0;
				this.m_GlyphTable.Add(spriteGlyph);
				TMP_SpriteCharacter spriteCharacter = new TMP_SpriteCharacter();
				spriteCharacter.glyph = spriteGlyph;
				spriteCharacter.unicode = (uint)((oldSprite.unicode == 0) ? 65534 : oldSprite.unicode);
				spriteCharacter.name = oldSprite.name;
				spriteCharacter.scale = oldSprite.scale;
				this.m_SpriteCharacterTable.Add(spriteCharacter);
			}
			this.UpdateLookupTables();
		}

		// Token: 0x04000396 RID: 918
		internal Dictionary<int, int> m_NameLookup;

		// Token: 0x04000397 RID: 919
		internal Dictionary<uint, int> m_GlyphIndexLookup;

		// Token: 0x04000398 RID: 920
		public Texture spriteSheet;

		// Token: 0x04000399 RID: 921
		[SerializeField]
		private List<TMP_SpriteCharacter> m_SpriteCharacterTable = new List<TMP_SpriteCharacter>();

		// Token: 0x0400039A RID: 922
		internal Dictionary<uint, TMP_SpriteCharacter> m_SpriteCharacterLookup;

		// Token: 0x0400039B RID: 923
		[FormerlySerializedAs("m_SpriteGlyphTable")]
		[SerializeField]
		private List<TMP_SpriteGlyph> m_GlyphTable = new List<TMP_SpriteGlyph>();

		// Token: 0x0400039C RID: 924
		internal Dictionary<uint, TMP_SpriteGlyph> m_SpriteGlyphLookup;

		// Token: 0x0400039D RID: 925
		public List<TMP_Sprite> spriteInfoList;

		// Token: 0x0400039E RID: 926
		[SerializeField]
		public List<TMP_SpriteAsset> fallbackSpriteAssets;

		// Token: 0x0400039F RID: 927
		internal bool m_IsSpriteAssetLookupTablesDirty;

		// Token: 0x040003A0 RID: 928
		private static HashSet<int> k_searchedSpriteAssets;
	}
}
