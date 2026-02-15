using System;

namespace MDPro3.Net
{
	// Token: 0x02001330 RID: 4912
	[Serializable]
	public class MyCardRoom
	{
		// Token: 0x0400CD24 RID: 52516
		public string id;

		// Token: 0x0400CD25 RID: 52517
		public string title;

		// Token: 0x0400CD26 RID: 52518
		public MyCardRoomUser user;

		// Token: 0x0400CD27 RID: 52519
		public MyCardRoomUser[] users;

		// Token: 0x0400CD28 RID: 52520
		public MyCardRoomOptions options;

		// Token: 0x0400CD29 RID: 52521
		public string arena;
	}
}
