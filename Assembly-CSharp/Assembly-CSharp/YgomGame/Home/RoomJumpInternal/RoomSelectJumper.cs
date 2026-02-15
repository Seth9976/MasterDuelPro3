using System;
using System.Runtime.CompilerServices;
using YgomGame.Room;

namespace YgomGame.Home.RoomJumpInternal
{
	// Token: 0x02000BE6 RID: 3046
	public class RoomSelectJumper : RoomJumperBase
	{
		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060056A1 RID: 22177 RVA: 0x000029CC File Offset: 0x00000BCC
		protected RoomEntryViewController.Mode mode
		{
			[CompilerGenerated]
			get
			{
				return RoomEntryViewController.Mode.NORMAL;
			}
		}

		// Token: 0x060056A2 RID: 22178 RVA: 0x000F4CDA File Offset: 0x000F2EDA
		public RoomSelectJumper(RoomEntryViewController.Mode mode)
		{
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Check(Action<bool> resultCallback)
		{
		}

		// Token: 0x060056A4 RID: 22180 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Jump()
		{
		}
	}
}
