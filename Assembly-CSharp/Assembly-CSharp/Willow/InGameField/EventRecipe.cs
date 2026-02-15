using System;
using System.Collections.Generic;
using UnityEngine;

namespace Willow.InGameField
{
	// Token: 0x02001560 RID: 5472
	[CreateAssetMenu]
	[Serializable]
	public class EventRecipe : ScriptableObject
	{
		// Token: 0x0400DE33 RID: 56883
		public List<IntFieldEvent> intFieldEventList;

		// Token: 0x0400DE34 RID: 56884
		public List<BoolFieldEvent> boolFieldEventList;
	}
}
