using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Defines events for a class.</summary>
	// Token: 0x02000663 RID: 1635
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_EventBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class EventBuilder
	{
		// Token: 0x04001950 RID: 6480
		internal string name;

		// Token: 0x04001951 RID: 6481
		private Type type;

		// Token: 0x04001952 RID: 6482
		private TypeBuilder typeb;

		// Token: 0x04001953 RID: 6483
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04001954 RID: 6484
		internal MethodBuilder add_method;

		// Token: 0x04001955 RID: 6485
		internal MethodBuilder remove_method;

		// Token: 0x04001956 RID: 6486
		internal MethodBuilder raise_method;

		// Token: 0x04001957 RID: 6487
		internal MethodBuilder[] other_methods;

		// Token: 0x04001958 RID: 6488
		internal EventAttributes attrs;

		// Token: 0x04001959 RID: 6489
		private int table_idx;
	}
}
