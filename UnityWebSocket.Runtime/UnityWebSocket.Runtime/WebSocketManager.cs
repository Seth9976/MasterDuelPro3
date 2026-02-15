using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityWebSocket
{
	// Token: 0x02000012 RID: 18
	[DefaultExecutionOrder(-10000)]
	internal class WebSocketManager : MonoBehaviour
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000306A File Offset: 0x0000126A
		public static WebSocketManager Instance
		{
			get
			{
				if (!WebSocketManager._instance)
				{
					WebSocketManager.CreateInstance();
				}
				return WebSocketManager._instance;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003082 File Offset: 0x00001282
		private void Awake()
		{
			global::UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003090 File Offset: 0x00001290
		public static void CreateInstance()
		{
			GameObject go = GameObject.Find("/[UnityWebSocket]");
			if (!go)
			{
				go = new GameObject("[UnityWebSocket]");
			}
			WebSocketManager._instance = go.GetComponent<WebSocketManager>();
			if (!WebSocketManager._instance)
			{
				WebSocketManager._instance = go.AddComponent<WebSocketManager>();
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000030DD File Offset: 0x000012DD
		public void Add(WebSocket socket)
		{
			if (!this.sockets.Contains(socket))
			{
				this.sockets.Add(socket);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000030F9 File Offset: 0x000012F9
		public void Remove(WebSocket socket)
		{
			if (this.sockets.Contains(socket))
			{
				this.sockets.Remove(socket);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003118 File Offset: 0x00001318
		private void Update()
		{
			if (this.sockets.Count <= 0)
			{
				return;
			}
			for (int i = this.sockets.Count - 1; i >= 0; i--)
			{
				this.sockets[i].Update();
			}
		}

		// Token: 0x0400004C RID: 76
		private const string rootName = "[UnityWebSocket]";

		// Token: 0x0400004D RID: 77
		private static WebSocketManager _instance;

		// Token: 0x0400004E RID: 78
		private readonly List<WebSocket> sockets = new List<WebSocket>();
	}
}
