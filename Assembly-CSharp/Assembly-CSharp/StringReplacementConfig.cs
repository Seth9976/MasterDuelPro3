using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000A RID: 10
[CreateAssetMenu(fileName = "StringReplacementConfig", menuName = "Scriptable Objects/String Replacement Configuration")]
public class StringReplacementConfig : ScriptableObject
{
	// Token: 0x06000025 RID: 37 RVA: 0x000028FD File Offset: 0x00000AFD
	public bool HasRules()
	{
		return this.replacementRules != null && this.replacementRules.Count > 0;
	}

	// Token: 0x04000020 RID: 32
	public List<StringReplacementConfig.ReplacementRule> replacementRules = new List<StringReplacementConfig.ReplacementRule>();

	// Token: 0x0200000B RID: 11
	[Serializable]
	public class ReplacementRule
	{
		// Token: 0x04000021 RID: 33
		public string findString;

		// Token: 0x04000022 RID: 34
		public string replaceString;

		// Token: 0x04000023 RID: 35
		[Tooltip("是否区分大小写")]
		public bool caseSensitive = true;
	}
}
