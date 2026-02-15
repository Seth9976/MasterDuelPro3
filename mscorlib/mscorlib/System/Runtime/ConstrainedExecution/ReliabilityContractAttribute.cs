using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Defines a contract for reliability between the author of some code, and the developers who have a dependency on that code.</summary>
	// Token: 0x02000574 RID: 1396
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Interface, Inherited = false)]
	public sealed class ReliabilityContractAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.ConstrainedExecution.ReliabilityContractAttribute" /> class with the specified <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> guarantee and <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> value.</summary>
		/// <param name="consistencyGuarantee">One of the <see cref="T:System.Runtime.ConstrainedExecution.Consistency" /> values. </param>
		/// <param name="cer">One of the <see cref="T:System.Runtime.ConstrainedExecution.Cer" /> values. </param>
		// Token: 0x06002AC4 RID: 10948 RVA: 0x000AA67B File Offset: 0x000A887B
		public ReliabilityContractAttribute(Consistency consistencyGuarantee, Cer cer)
		{
			this.<ConsistencyGuarantee>k__BackingField = consistencyGuarantee;
			this.<Cer>k__BackingField = cer;
		}
	}
}
