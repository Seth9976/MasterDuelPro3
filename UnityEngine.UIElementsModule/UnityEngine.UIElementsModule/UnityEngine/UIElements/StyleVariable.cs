using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200043A RID: 1082
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct StyleVariable
	{
		// Token: 0x06001F3C RID: 7996 RVA: 0x00071A19 File Offset: 0x0006FC19
		public StyleVariable(string name, StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.name = name;
			this.sheet = sheet;
			this.handles = handles;
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00071A34 File Offset: 0x0006FC34
		public override int GetHashCode()
		{
			int hashCode = this.name.GetHashCode();
			hashCode = (hashCode * 397) ^ this.sheet.GetHashCode();
			return (hashCode * 397) ^ this.handles.GetHashCode();
		}

		// Token: 0x04000DD1 RID: 3537
		public readonly string name;

		// Token: 0x04000DD2 RID: 3538
		public readonly StyleSheet sheet;

		// Token: 0x04000DD3 RID: 3539
		public readonly StyleValueHandle[] handles;
	}
}
