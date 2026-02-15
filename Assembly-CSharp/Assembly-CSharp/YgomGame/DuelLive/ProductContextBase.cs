using System;
using System.Collections.Generic;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C65 RID: 3173
	public abstract class ProductContextBase<T> : IComparable<T>, IProductContext
	{
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06005A93 RID: 23187 RVA: 0x000F1669 File Offset: 0x000EF869
		public long duelLiveId
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06005A94 RID: 23188 RVA: 0x000029CC File Offset: 0x00000BCC
		public int menuId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06005A95 RID: 23189 RVA: 0x000029CC File Offset: 0x00000BCC
		public int replayIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06005A96 RID: 23190 RVA: 0x000029CC File Offset: 0x00000BCC
		public int categoryId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06005A97 RID: 23191 RVA: 0x000029CC File Offset: 0x00000BCC
		public int categoryIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06005A98 RID: 23192 RVA: 0x000029CC File Offset: 0x00000BCC
		public int subCategoryId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06005A99 RID: 23193 RVA: 0x000029CC File Offset: 0x00000BCC
		public int subCategoryIdx
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06005A9A RID: 23194 RVA: 0x000029CC File Offset: 0x00000BCC
		public int sectionId
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06005A9B RID: 23195 RVA: 0x000029CC File Offset: 0x00000BCC
		public int widgetType
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06005A9C RID: 23196 RVA: 0x0000216A File Offset: 0x0000036A
		public List<object> mrk
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06005A9D RID: 23197 RVA: 0x0000216A File Offset: 0x0000036A
		public string imagePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06005A9E RID: 23198 RVA: 0x0000216A File Offset: 0x0000036A
		public string name1
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06005A9F RID: 23199 RVA: 0x0000216A File Offset: 0x0000036A
		public string name2
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06005AA0 RID: 23200 RVA: 0x000029CC File Offset: 0x00000BCC
		public int sort
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x0000216D File Offset: 0x0000036D
		public void Import(Dictionary<string, object> productData)
		{
		}

		// Token: 0x06005AA2 RID: 23202
		public abstract int Compare(T a, T b);

		// Token: 0x06005AA3 RID: 23203
		public abstract int CompareTo(T other);

		// Token: 0x040095E5 RID: 38373
		protected Dictionary<string, object> m_ProductData;
	}
}
