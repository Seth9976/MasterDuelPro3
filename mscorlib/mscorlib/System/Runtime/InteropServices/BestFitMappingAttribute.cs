using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls whether Unicode characters are converted to the closest matching ANSI characters.</summary>
	// Token: 0x0200053C RID: 1340
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
	public sealed class BestFitMappingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.BestFitMappingAttribute" /> class set to the value of the <see cref="P:System.Runtime.InteropServices.BestFitMappingAttribute.BestFitMapping" /> property.</summary>
		/// <param name="BestFitMapping">true to indicate that best-fit mapping is enabled; otherwise, false. The default is true. </param>
		// Token: 0x06002927 RID: 10535 RVA: 0x000A8139 File Offset: 0x000A6339
		public BestFitMappingAttribute(bool BestFitMapping)
		{
			this._bestFitMapping = BestFitMapping;
		}

		// Token: 0x0400157D RID: 5501
		internal bool _bestFitMapping;
	}
}
