using System;
using UnityEngine.Events;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x020002EF RID: 751
	public interface IEditorPlayerConnection
	{
		// Token: 0x060014FB RID: 5371
		void Register(Guid messageId, UnityAction<MessageEventArgs> callback);

		// Token: 0x060014FC RID: 5372
		void RegisterConnection(UnityAction<int> callback);

		// Token: 0x060014FD RID: 5373
		void RegisterDisconnection(UnityAction<int> callback);

		// Token: 0x060014FE RID: 5374
		void Send(Guid messageId, byte[] data);
	}
}
