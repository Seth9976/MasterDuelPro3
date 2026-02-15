using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	public struct ControlPoint
	{
		// Token: 0x06000388 RID: 904 RVA: 0x0000F662 File Offset: 0x0000D862
		public ControlPoint(Vector3 a, Vector3 b)
		{
			this.a = a;
			this.b = b;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000F672 File Offset: 0x0000D872
		public static ControlPoint operator +(ControlPoint cp, Vector3 v)
		{
			return new ControlPoint(cp.a + v, cp.b + v);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000F694 File Offset: 0x0000D894
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.a.ToString(),
				" | ",
				this.b.ToString(),
				"]"
			});
		}

		// Token: 0x040001A6 RID: 422
		public Vector3 a;

		// Token: 0x040001A7 RID: 423
		public Vector3 b;
	}
}
