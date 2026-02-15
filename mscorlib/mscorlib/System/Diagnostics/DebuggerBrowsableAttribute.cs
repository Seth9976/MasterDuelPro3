using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Determines if and how a member is displayed in the debugger variable windows. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006D9 RID: 1753
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class DebuggerBrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerBrowsableAttribute" /> class. </summary>
		/// <param name="state">One of the <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> values that specifies how to display the member.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="state" /> is not one of the <see cref="T:System.Diagnostics.DebuggerBrowsableState" /> values.</exception>
		// Token: 0x06003780 RID: 14208 RVA: 0x000DABB5 File Offset: 0x000D8DB5
		public DebuggerBrowsableAttribute(DebuggerBrowsableState state)
		{
			if (state < DebuggerBrowsableState.Never || state > DebuggerBrowsableState.RootHidden)
			{
				throw new ArgumentOutOfRangeException("state");
			}
			this.state = state;
		}

		// Token: 0x04001DEB RID: 7659
		private DebuggerBrowsableState state;
	}
}
