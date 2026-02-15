using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Determines how a class or field is displayed in the debugger variable windows.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006DB RID: 1755
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Delegate, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerDisplayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerDisplayAttribute" /> class. </summary>
		/// <param name="value">The string to be displayed in the value column for instances of the type; an empty string ("") causes the value column to be hidden.</param>
		// Token: 0x06003782 RID: 14210 RVA: 0x000DABFF File Offset: 0x000D8DFF
		public DebuggerDisplayAttribute(string value)
		{
			if (value == null)
			{
				this.value = "";
			}
			else
			{
				this.value = value;
			}
			this.name = "";
			this.type = "";
		}

		// Token: 0x04001DED RID: 7661
		private string name;

		// Token: 0x04001DEE RID: 7662
		private string value;

		// Token: 0x04001DEF RID: 7663
		private string type;
	}
}
