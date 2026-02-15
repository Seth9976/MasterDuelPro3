using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200017A RID: 378
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	[RequiredByNativeCode]
	public sealed class ContextMenu : Attribute
	{
		// Token: 0x06000F94 RID: 3988 RVA: 0x00020D46 File Offset: 0x0001EF46
		public ContextMenu(string itemName)
			: this(itemName, false)
		{
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00020D52 File Offset: 0x0001EF52
		public ContextMenu(string itemName, bool isValidateFunction)
			: this(itemName, isValidateFunction, 1000000)
		{
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00020D63 File Offset: 0x0001EF63
		public ContextMenu(string itemName, bool isValidateFunction, int priority)
		{
			this.menuItem = itemName;
			this.validate = isValidateFunction;
			this.priority = priority;
		}

		// Token: 0x0400061E RID: 1566
		public readonly string menuItem;

		// Token: 0x0400061F RID: 1567
		public readonly bool validate;

		// Token: 0x04000620 RID: 1568
		public readonly int priority;
	}
}
