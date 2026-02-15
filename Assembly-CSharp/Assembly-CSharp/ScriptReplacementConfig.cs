using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000008 RID: 8
[CreateAssetMenu(fileName = "ScriptReplacementConfig", menuName = "Scriptable Objects/Script Replacement Configuration")]
public class ScriptReplacementConfig : ScriptableObject
{
	// Token: 0x0400001A RID: 26
	public List<ScriptReplacementConfig.ScriptReplacementPair> replacementPairs = new List<ScriptReplacementConfig.ScriptReplacementPair>();

	// Token: 0x02000009 RID: 9
	[Serializable]
	public class ScriptReplacementPair
	{
		// Token: 0x0400001B RID: 27
		public string scriptName;

		// Token: 0x0400001C RID: 28
		public string oldGuid;

		// Token: 0x0400001D RID: 29
		public string newGuid;

		// Token: 0x0400001E RID: 30
		public long oldFileId;

		// Token: 0x0400001F RID: 31
		public long newFileId;
	}
}
