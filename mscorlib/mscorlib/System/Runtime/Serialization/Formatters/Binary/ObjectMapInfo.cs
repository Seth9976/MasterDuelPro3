using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020004F8 RID: 1272
	internal sealed class ObjectMapInfo
	{
		// Token: 0x060027F4 RID: 10228 RVA: 0x000A1361 File Offset: 0x0009F561
		internal ObjectMapInfo(int objectId, int numMembers, string[] memberNames, Type[] memberTypes)
		{
			this.objectId = objectId;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.memberTypes = memberTypes;
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000A1388 File Offset: 0x0009F588
		internal bool isCompatible(int numMembers, string[] memberNames, Type[] memberTypes)
		{
			bool flag = true;
			if (this.numMembers == numMembers)
			{
				for (int i = 0; i < numMembers; i++)
				{
					if (!this.memberNames[i].Equals(memberNames[i]))
					{
						flag = false;
						break;
					}
					if (memberTypes != null && this.memberTypes[i] != memberTypes[i])
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x040013DC RID: 5084
		internal int objectId;

		// Token: 0x040013DD RID: 5085
		private int numMembers;

		// Token: 0x040013DE RID: 5086
		private string[] memberNames;

		// Token: 0x040013DF RID: 5087
		private Type[] memberTypes;
	}
}
