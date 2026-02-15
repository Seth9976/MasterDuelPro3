using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Willow.InGameField
{
	// Token: 0x0200155A RID: 5466
	[RequireDerived]
	public abstract class BaseFieldEvent : ScriptableObject
	{
		// Token: 0x0400DE29 RID: 56873
		[NonSerialized]
		public BaseFieldEvent parent;

		// Token: 0x0400DE2A RID: 56874
		[NonSerialized]
		public List<BaseFieldEvent> childEventList;

		// Token: 0x0400DE2B RID: 56875
		public EventScope eventScope;
	}
}
