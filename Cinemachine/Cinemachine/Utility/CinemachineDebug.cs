using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000E5 RID: 229
	public class CinemachineDebug
	{
		// Token: 0x06000530 RID: 1328 RVA: 0x00021CA6 File Offset: 0x0001FEA6
		public static void ReleaseScreenPos(global::UnityEngine.Object client)
		{
			if (CinemachineDebug.mClients != null && CinemachineDebug.mClients.Contains(client))
			{
				CinemachineDebug.mClients.Remove(client);
			}
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00021CC8 File Offset: 0x0001FEC8
		public static Rect GetScreenPos(global::UnityEngine.Object client, string text, GUIStyle style)
		{
			if (CinemachineDebug.mClients == null)
			{
				CinemachineDebug.mClients = new HashSet<global::UnityEngine.Object>();
			}
			if (!CinemachineDebug.mClients.Contains(client))
			{
				CinemachineDebug.mClients.Add(client);
			}
			Vector2 pos = Vector2.zero;
			Vector2 size = style.CalcSize(new GUIContent(text));
			if (CinemachineDebug.mClients != null)
			{
				using (HashSet<global::UnityEngine.Object>.Enumerator enumerator = CinemachineDebug.mClients.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == client)
						{
							break;
						}
						pos.y += size.y;
					}
				}
			}
			return new Rect(pos, size);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00021D78 File Offset: 0x0001FF78
		public static StringBuilder SBFromPool()
		{
			if (CinemachineDebug.mAvailableStringBuilders == null || CinemachineDebug.mAvailableStringBuilders.Count == 0)
			{
				return new StringBuilder();
			}
			StringBuilder stringBuilder = CinemachineDebug.mAvailableStringBuilders[CinemachineDebug.mAvailableStringBuilders.Count - 1];
			CinemachineDebug.mAvailableStringBuilders.RemoveAt(CinemachineDebug.mAvailableStringBuilders.Count - 1);
			stringBuilder.Length = 0;
			return stringBuilder;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00021DD1 File Offset: 0x0001FFD1
		public static void ReturnToPool(StringBuilder sb)
		{
			if (CinemachineDebug.mAvailableStringBuilders == null)
			{
				CinemachineDebug.mAvailableStringBuilders = new List<StringBuilder>();
			}
			CinemachineDebug.mAvailableStringBuilders.Add(sb);
		}

		// Token: 0x040004A5 RID: 1189
		private static HashSet<global::UnityEngine.Object> mClients;

		// Token: 0x040004A6 RID: 1190
		public static CinemachineDebug.OnGUIDelegate OnGUIHandlers;

		// Token: 0x040004A7 RID: 1191
		private static List<StringBuilder> mAvailableStringBuilders;

		// Token: 0x020000E6 RID: 230
		// (Invoke) Token: 0x06000536 RID: 1334
		public delegate void OnGUIDelegate();
	}
}
