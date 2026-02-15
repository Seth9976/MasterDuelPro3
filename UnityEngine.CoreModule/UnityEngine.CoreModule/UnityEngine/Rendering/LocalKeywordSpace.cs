using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003D9 RID: 985
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	public readonly struct LocalKeywordSpace : IEquatable<LocalKeywordSpace>
	{
		// Token: 0x06001B01 RID: 6913 RVA: 0x0003B348 File Offset: 0x00039548
		public override bool Equals(object o)
		{
			bool flag;
			if (o is LocalKeywordSpace)
			{
				LocalKeywordSpace other = (LocalKeywordSpace)o;
				flag = this.Equals(other);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0003B374 File Offset: 0x00039574
		public bool Equals(LocalKeywordSpace rhs)
		{
			return this.m_KeywordSpace == rhs.m_KeywordSpace;
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0003B398 File Offset: 0x00039598
		public static bool operator ==(LocalKeywordSpace lhs, LocalKeywordSpace rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0003B3B4 File Offset: 0x000395B4
		public override int GetHashCode()
		{
			return this.m_KeywordSpace.GetHashCode();
		}

		// Token: 0x04000CEE RID: 3310
		private readonly IntPtr m_KeywordSpace;
	}
}
