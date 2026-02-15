using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.Notification
{
	// Token: 0x020006F6 RID: 1782
	public class FirebaseManager : MonoBehaviour
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06003785 RID: 14213 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsEnable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06003786 RID: 14214 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003787 RID: 14215 RVA: 0x0000216D File Offset: 0x0000036D
		public static string Token
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}
	}
}
