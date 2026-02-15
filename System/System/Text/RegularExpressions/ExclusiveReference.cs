using System;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200012F RID: 303
	internal sealed class ExclusiveReference
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x0001D360 File Offset: 0x0001B560
		public RegexRunner Get()
		{
			if (Interlocked.Exchange(ref this._locked, 1) != 0)
			{
				return null;
			}
			RegexRunner @ref = this._ref;
			if (@ref == null)
			{
				this._locked = 0;
				return null;
			}
			this._obj = @ref;
			return @ref;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001D39C File Offset: 0x0001B59C
		public void Release(RegexRunner obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._obj == obj)
			{
				this._obj = null;
				this._locked = 0;
				return;
			}
			if (this._obj == null && Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				if (this._ref == null)
				{
					this._ref = obj;
				}
				this._locked = 0;
				return;
			}
		}

		// Token: 0x040004E3 RID: 1251
		private RegexRunner _ref;

		// Token: 0x040004E4 RID: 1252
		private RegexRunner _obj;

		// Token: 0x040004E5 RID: 1253
		private volatile int _locked;
	}
}
