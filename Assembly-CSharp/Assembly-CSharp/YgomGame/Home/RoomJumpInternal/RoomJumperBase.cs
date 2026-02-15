using System;

namespace YgomGame.Home.RoomJumpInternal
{
	// Token: 0x02000BE5 RID: 3045
	public abstract class RoomJumperBase
	{
		// Token: 0x0600569E RID: 22174
		public abstract void Check(Action<bool> resultCallback);

		// Token: 0x0600569F RID: 22175
		public abstract void Jump();
	}
}
