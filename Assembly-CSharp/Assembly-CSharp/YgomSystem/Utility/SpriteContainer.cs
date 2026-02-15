using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000547 RID: 1351
	public class SpriteContainer : ScriptableObject
	{
		// Token: 0x06002B08 RID: 11016 RVA: 0x0000216A File Offset: 0x0000036A
		public SpriteContainer.Container GetContainer(string label)
		{
			return null;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x0000216A File Offset: 0x0000036A
		public Sprite GetSprite(string label)
		{
			return null;
		}

		// Token: 0x04002A15 RID: 10773
		public List<SpriteContainer.Container> containers;

		// Token: 0x04002A16 RID: 10774
		public global::UnityEngine.Object extraAsset;

		// Token: 0x02000548 RID: 1352
		[Serializable]
		public class Container
		{
			// Token: 0x04002A17 RID: 10775
			public string label;

			// Token: 0x04002A18 RID: 10776
			public Sprite sprite;
		}
	}
}
