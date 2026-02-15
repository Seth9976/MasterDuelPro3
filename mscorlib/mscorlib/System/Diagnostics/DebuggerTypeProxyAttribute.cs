using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies the display proxy for a type.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006DA RID: 1754
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerTypeProxyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> class using the type of the proxy. </summary>
		/// <param name="type">The proxy type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06003781 RID: 14209 RVA: 0x000DABD7 File Offset: 0x000D8DD7
		public DebuggerTypeProxyAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.typeName = type.AssemblyQualifiedName;
		}

		// Token: 0x04001DEC RID: 7660
		private string typeName;
	}
}
