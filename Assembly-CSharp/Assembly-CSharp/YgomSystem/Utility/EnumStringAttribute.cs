using System;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x0200051D RID: 1309
	public class EnumStringAttribute : PropertyAttribute
	{
		// Token: 0x06002A0B RID: 10763 RVA: 0x000F1A32 File Offset: 0x000EFC32
		public EnumStringAttribute(Type enumType)
		{
		}

		// Token: 0x04002973 RID: 10611
		public readonly Type enumType;
	}
}
