using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using YgomGame.Utility;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BD6 RID: 3030
	[Serializable]
	public class TableCellValue
	{
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06005656 RID: 22102 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005657 RID: 22103 RVA: 0x0000216D File Offset: 0x0000036D
		public Sprite imageSprite
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06005658 RID: 22104 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005659 RID: 22105 RVA: 0x0000216D File Offset: 0x0000036D
		public MDMarkupDef.TableCellValueType valueType
		{
			get
			{
				return MDMarkupDef.TableCellValueType.Text;
			}
			set
			{
			}
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x0000216A File Offset: 0x0000036A
		public object ExportJsonObj()
		{
			return null;
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportJsonObj(object jsonObj)
		{
		}

		// Token: 0x04009335 RID: 37685
		[SerializeField]
		public string type;

		// Token: 0x04009336 RID: 37686
		[SerializeField]
		public TextAlignmentOptions alignment;

		// Token: 0x04009337 RID: 37687
		[SerializeField]
		public GlobalTextData text;

		// Token: 0x04009338 RID: 37688
		[SerializeField]
		public string imagePath;

		// Token: 0x04009339 RID: 37689
		[SerializeField]
		public float overrideHeight;

		// Token: 0x0400933A RID: 37690
		[SerializeField]
		public float overrideWidth;

		// Token: 0x0400933B RID: 37691
		[SerializeField]
		public bool usePrefferedSize;

		// Token: 0x0400933C RID: 37692
		[SerializeField]
		public bool detailEnabled;

		// Token: 0x0400933D RID: 37693
		[SerializeField]
		public int mrk;

		// Token: 0x0400933E RID: 37694
		[SerializeField]
		public int premiere;

		// Token: 0x0400933F RID: 37695
		[SerializeField]
		public MDMarkupDef.CardSize cardSize;

		// Token: 0x04009340 RID: 37696
		[SerializeField]
		public bool isPeriod;

		// Token: 0x04009341 RID: 37697
		[SerializeField]
		public int itemCategory;

		// Token: 0x04009342 RID: 37698
		[SerializeField]
		public int itemId;

		// Token: 0x04009343 RID: 37699
		[SerializeField]
		public MDMarkupDef.ItemSize itemSize;

		// Token: 0x04009344 RID: 37700
		[SerializeField]
		public MDMarkupBannerContext banner;

		// Token: 0x04009345 RID: 37701
		[SerializeField]
		public string link;

		// Token: 0x04009346 RID: 37702
		[SerializeField]
		public MDMarkupDef.ButtonStyle buttonStyle;
	}
}
