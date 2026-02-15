using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Duel;

namespace YgomDebug
{
	// Token: 0x0200115A RID: 4442
	[Serializable]
	public struct LogDataCenterForAnalysis
	{
		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06008443 RID: 33859 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool show
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06008444 RID: 33860 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isActDataShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x06008445 RID: 33861 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isIndent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06008446 RID: 33862 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool team
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06008447 RID: 33863 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool extendinfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06008448 RID: 33864 RVA: 0x000029CC File Offset: 0x00000BCC
		public LOGACTIONTYPE acttype
		{
			get
			{
				return LOGACTIONTYPE.ACTION_NONE;
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06008449 RID: 33865 RVA: 0x000029CC File Offset: 0x00000BCC
		public int efxbegin
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x0600844A RID: 33866 RVA: 0x000029CC File Offset: 0x00000BCC
		public int efxend
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x0600844B RID: 33867 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLPCDataShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x0600844C RID: 33868 RVA: 0x000029CC File Offset: 0x00000BCC
		public int changevalue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x0600844D RID: 33869 RVA: 0x000029CC File Offset: 0x00000BCC
		public int restLP
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x0600844E RID: 33870 RVA: 0x000029CC File Offset: 0x00000BCC
		public Engine.DamageType lpctype
		{
			get
			{
				return Engine.DamageType.ByEffect;
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x0600844F RID: 33871 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isCCDataShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06008450 RID: 33872 RVA: 0x000029CC File Offset: 0x00000BCC
		public int numpre
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06008451 RID: 33873 RVA: 0x000029CC File Offset: 0x00000BCC
		public int numaft
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06008452 RID: 33874 RVA: 0x000029CC File Offset: 0x00000BCC
		public Engine.CounterType countertype
		{
			get
			{
				return Engine.CounterType.Magic;
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06008453 RID: 33875 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isDiceDataShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06008454 RID: 33876 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isCoinDataShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008455 RID: 33877 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddEfxNoInfo(int efxbegin, int efxend)
		{
		}

		// Token: 0x06008456 RID: 33878 RVA: 0x000F7484 File Offset: 0x000F5684
		public ValueTuple<LOGACTIONTYPE, bool> GetActionData()
		{
			return default(ValueTuple<LOGACTIONTYPE, bool>);
		}

		// Token: 0x06008457 RID: 33879 RVA: 0x000F749C File Offset: 0x000F569C
		public ValueTuple<int, int, Engine.DamageType, bool> GetLPChangeData()
		{
			return default(ValueTuple<int, int, Engine.DamageType, bool>);
		}

		// Token: 0x06008458 RID: 33880 RVA: 0x000F74B4 File Offset: 0x000F56B4
		public ValueTuple<int, int, Engine.CounterType, bool> GetCounterChangeData()
		{
			return default(ValueTuple<int, int, Engine.CounterType, bool>);
		}

		// Token: 0x06008459 RID: 33881 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBmgVisible()
		{
		}

		// Token: 0x0600845A RID: 33882 RVA: 0x000F74CC File Offset: 0x000F56CC
		public ValueTuple<bool, int> GetDiceData()
		{
			return default(ValueTuple<bool, int>);
		}

		// Token: 0x0600845B RID: 33883 RVA: 0x0000216A File Offset: 0x0000036A
		public List<bool> GetCoinResults()
		{
			return null;
		}

		// Token: 0x0400BF8C RID: 49036
		[SerializeField]
		private int dataint0;

		// Token: 0x0400BF8D RID: 49037
		[SerializeField]
		private int dataint1;

		// Token: 0x0400BF8E RID: 49038
		[SerializeField]
		private byte boolbits;

		// Token: 0x0400BF8F RID: 49039
		[SerializeField]
		private byte databyte0;

		// Token: 0x0400BF90 RID: 49040
		[SerializeField]
		private byte databyte1;

		// Token: 0x0400BF91 RID: 49041
		[SerializeField]
		private byte datatype;
	}
}
