using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000FE RID: 254
	internal sealed class EmptyEnumerator : IEnumerator
	{
		// Token: 0x06000D75 RID: 3445 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		bool IEnumerator.MoveNext()
		{
			return false;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0000A558 File Offset: 0x00008758
		void IEnumerator.Reset()
		{
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0000AB38 File Offset: 0x00008D38
		object IEnumerator.Current
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}
	}
}
