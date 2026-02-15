using System;

namespace System.Security
{
	/// <summary>Indicates the set of security rules the common language runtime should enforce for an assembly.  </summary>
	// Token: 0x0200031B RID: 795
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SecurityRulesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityRulesAttribute" /> class using the specified rule set value. </summary>
		/// <param name="ruleSet">One of the enumeration values that specifies the transparency rules set. </param>
		// Token: 0x06001C5B RID: 7259 RVA: 0x0006EB44 File Offset: 0x0006CD44
		public SecurityRulesAttribute(SecurityRuleSet ruleSet)
		{
			this.m_ruleSet = ruleSet;
		}

		// Token: 0x04000D10 RID: 3344
		private SecurityRuleSet m_ruleSet;
	}
}
