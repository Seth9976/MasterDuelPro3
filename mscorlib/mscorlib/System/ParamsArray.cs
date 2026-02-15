using System;

namespace System
{
	// Token: 0x02000133 RID: 307
	internal readonly struct ParamsArray
	{
		// Token: 0x06000A47 RID: 2631 RVA: 0x0002E6B2 File Offset: 0x0002C8B2
		public ParamsArray(object arg0)
		{
			this._arg0 = arg0;
			this._arg1 = null;
			this._arg2 = null;
			this._args = ParamsArray.s_oneArgArray;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002E6D4 File Offset: 0x0002C8D4
		public ParamsArray(object arg0, object arg1)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = null;
			this._args = ParamsArray.s_twoArgArray;
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002E6F6 File Offset: 0x0002C8F6
		public ParamsArray(object arg0, object arg1, object arg2)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._args = ParamsArray.s_threeArgArray;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002E718 File Offset: 0x0002C918
		public ParamsArray(object[] args)
		{
			int num = args.Length;
			this._arg0 = ((num > 0) ? args[0] : null);
			this._arg1 = ((num > 1) ? args[1] : null);
			this._arg2 = ((num > 2) ? args[2] : null);
			this._args = args;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0002E760 File Offset: 0x0002C960
		public int Length
		{
			get
			{
				return this._args.Length;
			}
		}

		// Token: 0x170000B3 RID: 179
		public object this[int index]
		{
			get
			{
				if (index != 0)
				{
					return this.GetAtSlow(index);
				}
				return this._arg0;
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002E77D File Offset: 0x0002C97D
		private object GetAtSlow(int index)
		{
			if (index == 1)
			{
				return this._arg1;
			}
			if (index == 2)
			{
				return this._arg2;
			}
			return this._args[index];
		}

		// Token: 0x04000466 RID: 1126
		private static readonly object[] s_oneArgArray = new object[1];

		// Token: 0x04000467 RID: 1127
		private static readonly object[] s_twoArgArray = new object[2];

		// Token: 0x04000468 RID: 1128
		private static readonly object[] s_threeArgArray = new object[3];

		// Token: 0x04000469 RID: 1129
		private readonly object _arg0;

		// Token: 0x0400046A RID: 1130
		private readonly object _arg1;

		// Token: 0x0400046B RID: 1131
		private readonly object _arg2;

		// Token: 0x0400046C RID: 1132
		private readonly object[] _args;
	}
}
