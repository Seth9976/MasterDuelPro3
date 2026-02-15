using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000CD3 RID: 3283
	public struct CardInfoData
	{
		// Token: 0x04009952 RID: 39250
		public bool hasinstance;

		// Token: 0x04009953 RID: 39251
		public int cardid;

		// Token: 0x04009954 RID: 39252
		public int uniqueid;

		// Token: 0x04009955 RID: 39253
		public int player;

		// Token: 0x04009956 RID: 39254
		public int owner;

		// Token: 0x04009957 RID: 39255
		public int position;

		// Token: 0x04009958 RID: 39256
		public int index;

		// Token: 0x04009959 RID: 39257
		public int cardattribute;

		// Token: 0x0400995A RID: 39258
		public int styleid;

		// Token: 0x0400995B RID: 39259
		public int orglevel;

		// Token: 0x0400995C RID: 39260
		public int orgrank;

		// Token: 0x0400995D RID: 39261
		public int orgtype;

		// Token: 0x0400995E RID: 39262
		public int effectflag;

		// Token: 0x0400995F RID: 39263
		public int overlaynum;

		// Token: 0x04009960 RID: 39264
		public int orgscale;

		// Token: 0x04009961 RID: 39265
		public int scale;

		// Token: 0x04009962 RID: 39266
		public int turncounter;

		// Token: 0x04009963 RID: 39267
		public bool isfightableoneffect;

		// Token: 0x04009964 RID: 39268
		public bool istuner;

		// Token: 0x04009965 RID: 39269
		public Engine.BasicVal basicval;

		// Token: 0x04009966 RID: 39270
		public List<KeyValuePair<Engine.CounterType, int>> countertable;
	}
}
