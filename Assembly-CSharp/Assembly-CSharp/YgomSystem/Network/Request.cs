using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YgomSystem.Network
{
	// Token: 0x02000727 RID: 1831
	public class Request
	{
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x0600394E RID: 14670 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600394F RID: 14671 RVA: 0x0000216D File Offset: 0x0000036D
		private static event EventHandler s_commonStartHandle
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06003950 RID: 14672 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06003951 RID: 14673 RVA: 0x0000216D File Offset: 0x0000036D
		private static event EventHandler s_commonCompleteHandle
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06003952 RID: 14674 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06003953 RID: 14675 RVA: 0x0000216D File Offset: 0x0000036D
		private static event EventHandler s_commonErrorHandle
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle SetCommonHandler(Handle handle)
		{
			return null;
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InvokeCommonStartHandle(Handle handle)
		{
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddCommonStartEvent(EventHandler e)
		{
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddCommonCompleteEvent(EventHandler e)
		{
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddCommonErrorEvent(EventHandler e)
		{
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveCommonStartEvent(EventHandler e)
		{
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveCommonCompleteEvent(EventHandler e)
		{
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveCommonErrorEvent(EventHandler e)
		{
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddCommandEvent(string command, EventHandler e)
		{
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x0000216D File Offset: 0x0000036D
		public static void DelCommandEvent(string command, EventHandler e)
		{
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Entry(string command, Dictionary<string, object> param = null, float timeOut = 30f)
		{
			return null;
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CommandEvent(string command, Handle handle)
		{
		}

		// Token: 0x04003335 RID: 13109
		private static Dictionary<string, EventHandler> s_commandHandle;
	}
}
