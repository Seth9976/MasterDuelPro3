using System;

namespace YgomDebug
{
	// Token: 0x0200115B RID: 4443
	[Serializable]
	public struct LogDataSideForAnalysis
	{
		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x0600845C RID: 33884 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool show
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x0600845D RID: 33885 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isCardDataShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x0600845E RID: 33886 RVA: 0x000029CC File Offset: 0x00000BCC
		public int cardid
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x0600845F RID: 33887 RVA: 0x000029CC File Offset: 0x00000BCC
		public int effectid
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x06008460 RID: 33888 RVA: 0x000029CC File Offset: 0x00000BCC
		public int position
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06008461 RID: 33889 RVA: 0x000029CC File Offset: 0x00000BCC
		public int owner
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06008462 RID: 33890 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool turned
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06008463 RID: 33891 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool insight
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06008464 RID: 33892 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool face
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06008465 RID: 33893 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isoverlayunit
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06008466 RID: 33894 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isIconDataShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06008467 RID: 33895 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isDataValid
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06008468 RID: 33896 RVA: 0x000029CC File Offset: 0x00000BCC
		public int playerid
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06008469 RID: 33897 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddEffectId(int effectid)
		{
		}

		// Token: 0x0600846A RID: 33898 RVA: 0x000F74E4 File Offset: 0x000F56E4
		public ValueTuple<int, byte, byte, bool, bool> GetCardData()
		{
			return default(ValueTuple<int, byte, byte, bool, bool>);
		}

		// Token: 0x0600846B RID: 33899 RVA: 0x000F74FC File Offset: 0x000F56FC
		public ValueTuple<int, bool> GetIconData()
		{
			return default(ValueTuple<int, bool>);
		}

		// Token: 0x0400BF92 RID: 49042
		private int dataint;

		// Token: 0x0400BF93 RID: 49043
		private byte boolbits;

		// Token: 0x0400BF94 RID: 49044
		private byte databyte0;

		// Token: 0x0400BF95 RID: 49045
		private byte databyte1;

		// Token: 0x0400BF96 RID: 49046
		private byte datatype;
	}
}
