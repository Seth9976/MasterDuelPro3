using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Willow.UI
{
	// Token: 0x02001557 RID: 5463
	public class TextMeshProEx : TextMeshProUGUI, ITextAccessor
	{
		// Token: 0x170014D2 RID: 5330
		// (get) Token: 0x06009E91 RID: 40593 RVA: 0x0019B5E8 File Offset: 0x001997E8
		// (set) Token: 0x06009E92 RID: 40594 RVA: 0x0000216D File Offset: 0x0000036D
		public DateTime dateTime
		{
			get
			{
				return default(DateTime);
			}
			set
			{
			}
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x06009E93 RID: 40595 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06009E94 RID: 40596 RVA: 0x0000216D File Offset: 0x0000036D
		public string labelName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170014D4 RID: 5332
		// (get) Token: 0x06009E95 RID: 40597 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E96 RID: 40598 RVA: 0x0000216D File Offset: 0x0000036D
		public TextMeshProEx.TextMeshProSDF SDFName
		{
			get
			{
				return TextMeshProEx.TextMeshProSDF.Main_SDF;
			}
			set
			{
			}
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x06009E97 RID: 40599 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06009E98 RID: 40600 RVA: 0x0000216D File Offset: 0x0000036D
		public string materialName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x06009E99 RID: 40601 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06009E9A RID: 40602 RVA: 0x0000216D File Offset: 0x0000036D
		public string textColorSettings
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x06009E9B RID: 40603 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E9C RID: 40604 RVA: 0x0000216D File Offset: 0x0000036D
		public int textColorIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x06009E9D RID: 40605 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009E9E RID: 40606 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isNoDefaultLine
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x06009E9F RID: 40607 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06009EA0 RID: 40608 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isNoIStyle
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170014DA RID: 5338
		// (set) Token: 0x06009EA1 RID: 40609 RVA: 0x0000216D File Offset: 0x0000036D
		public string setString
		{
			set
			{
			}
		}

		// Token: 0x06009EA2 RID: 40610 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void Start()
		{
		}

		// Token: 0x06009EA3 RID: 40611 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLabel()
		{
		}

		// Token: 0x06009EA4 RID: 40612 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float GetPreferredWidthEx()
		{
			return 0f;
		}

		// Token: 0x06009EA5 RID: 40613 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetButtonText()
		{
		}

		// Token: 0x06009EA6 RID: 40614 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLabel(string tagName, bool isAutoSet = false)
		{
		}

		// Token: 0x06009EA7 RID: 40615 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetString(string value, bool shouldInOutGameTagFilter = false)
		{
		}

		// Token: 0x06009EA8 RID: 40616 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTextMaterial(string matName, bool isRefresh = false)
		{
		}

		// Token: 0x06009EA9 RID: 40617 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<string> GetMatList(string sdfName)
		{
			return null;
		}

		// Token: 0x06009EAA RID: 40618 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<string> GetMatList(TMP_FontAsset fontAsset)
		{
			return null;
		}

		// Token: 0x06009EAB RID: 40619 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void LoadFontAsset()
		{
		}

		// Token: 0x06009EAC RID: 40620 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetSDFColor()
		{
		}

		// Token: 0x06009EAD RID: 40621 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSDF(SystemLanguage lang, bool cache = true)
		{
		}

		// Token: 0x06009EAE RID: 40622 RVA: 0x0000216A File Offset: 0x0000036A
		private string FilterInOutGameTag(string str)
		{
			return null;
		}

		// Token: 0x06009EAF RID: 40623 RVA: 0x0000216A File Offset: 0x0000036A
		private string RemoveOnlyTag(string str, string tagName)
		{
			return null;
		}

		// Token: 0x06009EB0 RID: 40624 RVA: 0x0000216A File Offset: 0x0000036A
		private string RemoveTag(string str, string tagName)
		{
			return null;
		}

		// Token: 0x06009EB1 RID: 40625 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Material[] GetSharedMaterials()
		{
			return null;
		}

		// Token: 0x06009EB2 RID: 40626 RVA: 0x0000216D File Offset: 0x0000036D
		private static void InitLang(SystemLanguage lang)
		{
		}

		// Token: 0x06009EB3 RID: 40627 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetFontAssetPath(TextMeshProEx.TextMeshProSDF sdfName)
		{
			return null;
		}

		// Token: 0x06009EB4 RID: 40628 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetFontMatPath(TextMeshProEx.TextMeshProSDF sdfName, string matName)
		{
			return null;
		}

		// Token: 0x06009EB5 RID: 40629 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadFontAsset(global::UnityEngine.Object owner, SystemLanguage lang, TextMeshProEx.TextMeshProSDF sdfName, Action<TMP_FontAsset> callback, bool cache = true)
		{
		}

		// Token: 0x06009EB6 RID: 40630 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadFontMaterial(global::UnityEngine.Object owner, SystemLanguage lang, TextMeshProEx.TextMeshProSDF sdfName, string matName, Action<Material> callback, bool cache = true)
		{
		}

		// Token: 0x06009EB7 RID: 40631 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearFontAsset()
		{
		}

		// Token: 0x06009EB8 RID: 40632 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LoadFontAssetFallback(global::UnityEngine.Object owner, SystemLanguage lang, TextMeshProEx.TextMeshProSDF sdfName, TMP_FontAsset baseAsset, Action<TMP_FontAsset> callback)
		{
		}

		// Token: 0x06009EB9 RID: 40633 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LoadFontAssetFallback(global::UnityEngine.Object owner, SystemLanguage lang, TextMeshProEx.TextMeshProSDF sdfName, string sdfNameBase, TMP_FontAsset baseAsset, Action<TMP_FontAsset> callback, bool isFontAdd = false, bool isFontCurrency = false)
		{
		}

		// Token: 0x06009EBA RID: 40634 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LoadAsync<T>(string path, global::UnityEngine.Object owner, Action<T> callback = null, bool cache = true) where T : global::UnityEngine.Object
		{
		}

		// Token: 0x06009EBB RID: 40635 RVA: 0x0000216A File Offset: 0x0000036A
		private static global::UnityEngine.Object[] LoadAllMaterial(string path)
		{
			return null;
		}

		// Token: 0x0400DE0A RID: 56842
		public static string kNoMaterial;

		// Token: 0x0400DE0B RID: 56843
		public static string kDefaultMaterial;

		// Token: 0x0400DE0C RID: 56844
		public static string kNoTextColorSettings;

		// Token: 0x0400DE0D RID: 56845
		public static string kNoReflectTextColorSettings;

		// Token: 0x0400DE0E RID: 56846
		public static string kDefaultTextColorSettings;

		// Token: 0x0400DE0F RID: 56847
		private float m_defaultLine;

		// Token: 0x0400DE10 RID: 56848
		[SerializeField]
		private string m_labelName;

		// Token: 0x0400DE11 RID: 56849
		[SerializeField]
		private TextMeshProEx.TextMeshProSDF m_SDFName;

		// Token: 0x0400DE12 RID: 56850
		[SerializeField]
		private string m_materialName;

		// Token: 0x0400DE13 RID: 56851
		[SerializeField]
		private string m_textColorSettings;

		// Token: 0x0400DE14 RID: 56852
		[SerializeField]
		private int m_textColorIndex;

		// Token: 0x0400DE15 RID: 56853
		[SerializeField]
		private bool m_isNoDefaultLine;

		// Token: 0x0400DE16 RID: 56854
		[SerializeField]
		private bool m_isNoIStyle;

		// Token: 0x0400DE17 RID: 56855
		public Action<TextMeshProEx> onStartCallback;

		// Token: 0x0400DE18 RID: 56856
		private string m_setSpriteText;

		// Token: 0x0400DE19 RID: 56857
		private bool m_isLoadSDF;

		// Token: 0x0400DE1A RID: 56858
		private float m_textWidth;

		// Token: 0x0400DE1B RID: 56859
		private static SystemLanguage s_language;

		// Token: 0x0400DE1C RID: 56860
		private static Dictionary<string, TMP_FontAsset> s_dicAsset;

		// Token: 0x0400DE1D RID: 56861
		private const string kTextPath = "Font/";

		// Token: 0x02001558 RID: 5464
		public enum TextMeshProLang
		{
			// Token: 0x0400DE1F RID: 56863
			JP_,
			// Token: 0x0400DE20 RID: 56864
			US_,
			// Token: 0x0400DE21 RID: 56865
			Max
		}

		// Token: 0x02001559 RID: 5465
		public enum TextMeshProSDF
		{
			// Token: 0x0400DE23 RID: 56867
			Main_SDF,
			// Token: 0x0400DE24 RID: 56868
			Title_SDF,
			// Token: 0x0400DE25 RID: 56869
			未使用,
			// Token: 0x0400DE26 RID: 56870
			Number_SDF,
			// Token: 0x0400DE27 RID: 56871
			Chat_SDF,
			// Token: 0x0400DE28 RID: 56872
			Summon_SDF
		}
	}
}
