using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000190 RID: 400
	public abstract class DragAndDropData
	{
		// Token: 0x06000BCF RID: 3023
		public abstract object GetGenericData(string key);

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000BD0 RID: 3024
		public abstract object source { get; }

		// Token: 0x1700021B RID: 539
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x00038B33 File Offset: 0x00036D33
		public virtual string[] paths
		{
			[CompilerGenerated]
			set
			{
				this.<paths>k__BackingField = value;
			}
		}
	}
}
