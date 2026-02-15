using System;
using YgomGame.Home.RoomJumpInternal;
using YgomGame.Room;

namespace YgomGame.Home
{
	// Token: 0x02000BE3 RID: 3043
	public class RoomJump
	{
		// Token: 0x06005697 RID: 22167 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GotoRoomSelect(RoomEntryViewController.Mode mode, Action<bool> resultCallback = null)
		{
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GotoCurrentRoom(Action<bool> resultCallback = null)
		{
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x0000216D File Offset: 0x0000036D
		private static void execute(RoomJumperBase jumper, Action<bool> callback = null)
		{
		}
	}
}
