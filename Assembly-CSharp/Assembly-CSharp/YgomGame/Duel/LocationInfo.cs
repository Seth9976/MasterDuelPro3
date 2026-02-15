using System;
using System.Runtime.CompilerServices;

namespace YgomGame.Duel
{
	// Token: 0x02000EB7 RID: 3767
	public class LocationInfo
	{
		// Token: 0x140000BB RID: 187
		// (add) Token: 0x06006DB0 RID: 28080 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06006DB1 RID: 28081 RVA: 0x0000216D File Offset: 0x0000036D
		public event LocationInfo.UpdateCallback updateCallback
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

		// Token: 0x06006DB2 RID: 28082 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x06006DB3 RID: 28083 RVA: 0x0000216D File Offset: 0x0000036D
		public void Set(int player, int position, int index)
		{
		}

		// Token: 0x06006DB4 RID: 28084 RVA: 0x0000216D File Offset: 0x0000036D
		public void Reset()
		{
		}

		// Token: 0x06006DB5 RID: 28085 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEquals(int player, int position, int index)
		{
			return false;
		}

		// Token: 0x06006DB6 RID: 28086 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetIndex(int player, int position)
		{
			return 0;
		}

		// Token: 0x0400A8C0 RID: 43200
		public int player;

		// Token: 0x0400A8C1 RID: 43201
		public int position;

		// Token: 0x0400A8C2 RID: 43202
		public int index;

		// Token: 0x02000EB8 RID: 3768
		// (Invoke) Token: 0x06006DB9 RID: 28089
		public delegate void UpdateCallback();
	}
}
