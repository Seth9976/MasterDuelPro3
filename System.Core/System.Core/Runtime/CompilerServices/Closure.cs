using System;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Runtime.CompilerServices
{
	/// <summary>Represents the runtime state of a dynamically generated method.</summary>
	// Token: 0x02000116 RID: 278
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DebuggerStepThrough]
	public sealed class Closure
	{
		/// <summary>Creates an object to hold state of a dynamically generated method.</summary>
		/// <param name="constants">The constant values that are used by the method.</param>
		/// <param name="locals">The hoisted local variables from the parent context.</param>
		// Token: 0x06000977 RID: 2423 RVA: 0x00025729 File Offset: 0x00023929
		public Closure(object[] constants, object[] locals)
		{
			this.Constants = constants;
			this.Locals = locals;
		}

		/// <summary>Represents the non-trivial constants and locally executable expressions that are referenced by a dynamically generated method.</summary>
		// Token: 0x040002EF RID: 751
		public readonly object[] Constants;

		/// <summary>Represents the hoisted local variables from the parent context.</summary>
		// Token: 0x040002F0 RID: 752
		public readonly object[] Locals;
	}
}
