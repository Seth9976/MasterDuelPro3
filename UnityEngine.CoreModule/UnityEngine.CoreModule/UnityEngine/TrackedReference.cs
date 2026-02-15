using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001BD RID: 445
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class TrackedReference
	{
		// Token: 0x0600113A RID: 4410 RVA: 0x000205EB File Offset: 0x0001E7EB
		protected TrackedReference()
		{
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00024E74 File Offset: 0x00023074
		public static bool operator ==(TrackedReference x, TrackedReference y)
		{
			bool flag = y == null && x == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = y == null;
				if (flag3)
				{
					flag2 = x.m_Ptr == IntPtr.Zero;
				}
				else
				{
					bool flag4 = x == null;
					if (flag4)
					{
						flag2 = y.m_Ptr == IntPtr.Zero;
					}
					else
					{
						flag2 = x.m_Ptr == y.m_Ptr;
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00024EE8 File Offset: 0x000230E8
		public override bool Equals(object o)
		{
			return o as TrackedReference == this;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00024F08 File Offset: 0x00023108
		public override int GetHashCode()
		{
			return (int)this.m_Ptr;
		}

		// Token: 0x0400068A RID: 1674
		[VisibleToOtherModules(new string[] { "UnityEngine.AnimationModule" })]
		internal IntPtr m_Ptr;
	}
}
