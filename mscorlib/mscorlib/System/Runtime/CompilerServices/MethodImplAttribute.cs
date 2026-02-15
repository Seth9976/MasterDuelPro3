using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the details of how a method is implemented. This class cannot be inherited. </summary>
	// Token: 0x020005B9 RID: 1465
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class MethodImplAttribute : Attribute
	{
		// Token: 0x06002B7E RID: 11134 RVA: 0x000ABEB0 File Offset: 0x000AA0B0
		internal MethodImplAttribute(MethodImplAttributes methodImplAttributes)
		{
			MethodImplOptions methodImplOptions = MethodImplOptions.Unmanaged | MethodImplOptions.ForwardRef | MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall | MethodImplOptions.Synchronized | MethodImplOptions.NoInlining | MethodImplOptions.AggressiveInlining | MethodImplOptions.NoOptimization;
			this._val = (MethodImplOptions)(methodImplAttributes & (MethodImplAttributes)methodImplOptions);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.MethodImplAttribute" /> class.</summary>
		// Token: 0x06002B7F RID: 11135 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public MethodImplAttribute()
		{
		}

		// Token: 0x04001616 RID: 5654
		internal MethodImplOptions _val;
	}
}
