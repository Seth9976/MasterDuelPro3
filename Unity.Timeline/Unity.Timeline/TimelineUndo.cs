using System;
using System.Diagnostics;

namespace UnityEngine.Timeline
{
	// Token: 0x0200007B RID: 123
	internal static class TimelineUndo
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000021D7 File Offset: 0x000003D7
		internal static bool undoEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000BBBD File Offset: 0x00009DBD
		public static void PushDestroyUndo(TimelineAsset timeline, Object thingToDirty, Object objectToDestroy)
		{
			if (objectToDestroy != null)
			{
				Object.Destroy(objectToDestroy);
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00002811 File Offset: 0x00000A11
		[Conditional("UNITY_EDITOR")]
		public static void PushUndo(Object[] thingsToDirty, string operation)
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00002811 File Offset: 0x00000A11
		[Conditional("UNITY_EDITOR")]
		public static void PushUndo(Object thingToDirty, string operation)
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00002811 File Offset: 0x00000A11
		[Conditional("UNITY_EDITOR")]
		public static void RegisterCreatedObjectUndo(Object thingCreated, string operation)
		{
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000BBCE File Offset: 0x00009DCE
		internal static string UndoName(string name)
		{
			return "Timeline " + name;
		}
	}
}
