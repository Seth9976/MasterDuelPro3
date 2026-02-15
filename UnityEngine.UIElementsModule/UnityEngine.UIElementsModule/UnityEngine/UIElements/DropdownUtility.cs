using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AA RID: 426
	internal static class DropdownUtility
	{
		// Token: 0x06000C4A RID: 3146 RVA: 0x0003B7F4 File Offset: 0x000399F4
		internal static IGenericMenu CreateDropdown()
		{
			IGenericMenu genericMenu2;
			if (DropdownUtility.MakeDropdownFunc == null)
			{
				IGenericMenu genericMenu = new GenericDropdownMenu();
				genericMenu2 = genericMenu;
			}
			else
			{
				genericMenu2 = DropdownUtility.MakeDropdownFunc();
			}
			return genericMenu2;
		}

		// Token: 0x040007CC RID: 1996
		internal static Func<IGenericMenu> MakeDropdownFunc;
	}
}
