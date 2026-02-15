using System;

namespace UnityEngineInternal
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Method)]
	[Serializable]
	public class TypeInferenceRuleAttribute : Attribute
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002090 File Offset: 0x00000290
		public TypeInferenceRuleAttribute(TypeInferenceRules rule)
			: this(rule.ToString())
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020A7 File Offset: 0x000002A7
		public TypeInferenceRuleAttribute(string rule)
		{
			this._rule = rule;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020B8 File Offset: 0x000002B8
		public override string ToString()
		{
			return this._rule;
		}

		// Token: 0x04000009 RID: 9
		private readonly string _rule;
	}
}
