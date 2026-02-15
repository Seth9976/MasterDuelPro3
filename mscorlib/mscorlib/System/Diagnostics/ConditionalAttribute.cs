using System;

namespace System.Diagnostics
{
	/// <summary>Indicates to compilers that a method call or attribute should be ignored unless a specified conditional compilation symbol is defined.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006D1 RID: 1745
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	[Serializable]
	public sealed class ConditionalAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConditionalAttribute" /> class.</summary>
		/// <param name="conditionString">A string that specifies the case-sensitive conditional compilation symbol that is associated with the attribute. </param>
		// Token: 0x0600377A RID: 14202 RVA: 0x000DAB97 File Offset: 0x000D8D97
		public ConditionalAttribute(string conditionString)
		{
			this.<ConditionString>k__BackingField = conditionString;
		}
	}
}
