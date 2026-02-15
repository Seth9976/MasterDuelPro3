using System;

namespace YgomGame.Duel
{
	// Token: 0x02000EB5 RID: 3765
	public class ListCardData
	{
		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06006DAA RID: 28074 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool hasInstance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06006DAB RID: 28075 RVA: 0x000029CC File Offset: 0x00000BCC
		public int uniqueid
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06006DAC RID: 28076 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool insight
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06006DAD RID: 28077 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isknown
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006DAE RID: 28078 RVA: 0x00002739 File Offset: 0x00000939
		public ListCardData(int cardid, int styleid, Engine.BasicVal basicval, Engine.CardStatus cardstatus, bool forceinsgiht, int listIndexForEngine, int chainnum, ListCardData.DataSource dataSource, int textid = 0, int targetUid = -1, bool extraExcludeFlag = false)
		{
		}

		// Token: 0x06006DAF RID: 28079 RVA: 0x00002739 File Offset: 0x00000939
		public ListCardData(int cardid, int listIndexForEngine)
		{
		}

		// Token: 0x0400A8AC RID: 43180
		public bool selectted;

		// Token: 0x0400A8AD RID: 43181
		public bool forceinsgiht;

		// Token: 0x0400A8AE RID: 43182
		public bool extraExcludeFlag;

		// Token: 0x0400A8AF RID: 43183
		public bool targetted;

		// Token: 0x0400A8B0 RID: 43184
		public byte styleid;

		// Token: 0x0400A8B1 RID: 43185
		public byte chainnum;

		// Token: 0x0400A8B2 RID: 43186
		public int cardid;

		// Token: 0x0400A8B3 RID: 43187
		public int badgeindex;

		// Token: 0x0400A8B4 RID: 43188
		public int listIndexForEngine;

		// Token: 0x0400A8B5 RID: 43189
		public int dataindex;

		// Token: 0x0400A8B6 RID: 43190
		public int textid;

		// Token: 0x0400A8B7 RID: 43191
		public int targetUid;

		// Token: 0x0400A8B8 RID: 43192
		public Engine.CardStatus cardstatus;

		// Token: 0x0400A8B9 RID: 43193
		public Engine.BasicVal basicval;

		// Token: 0x0400A8BA RID: 43194
		public ListCardData.DataSource dataSource;

		// Token: 0x02000EB6 RID: 3766
		public enum DataSource
		{
			// Token: 0x0400A8BC RID: 43196
			FromPosSelectList,
			// Token: 0x0400A8BD RID: 43197
			FromGetCommandMask,
			// Token: 0x0400A8BE RID: 43198
			FromRunList,
			// Token: 0x0400A8BF RID: 43199
			FromLocalCollection
		}
	}
}
