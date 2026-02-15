using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MDPro3.Duel
{
	// Token: 0x0200150B RID: 5387
	public class MessageDispatcher
	{
		// Token: 0x06009C70 RID: 40048 RVA: 0x0018F357 File Offset: 0x0018D557
		public MessageDispatcher()
		{
			this.log = new LogMessage(this);
			this.voice = new VoiceMessage(this);
			this.duel = new DuelMessage(this);
		}

		// Token: 0x06009C71 RID: 40049 RVA: 0x0018F384 File Offset: 0x0018D584
		public async UniTask Process(Package p)
		{
			if (p.Function != 1)
			{
				this.lastPackage = p;
			}
			this.playerResponed = false;
			try
			{
				await this.log.Process(p);
			}
			catch (Exception ex)
			{
				Debug.Log(ex.Message);
			}
			try
			{
				await this.voice.Process(p);
			}
			catch (Exception ex2)
			{
				Debug.Log(ex2.Message);
			}
			try
			{
				await this.duel.Process(p);
			}
			catch (Exception ex3)
			{
				Debug.Log(ex3.Message);
			}
		}

		// Token: 0x06009C72 RID: 40050 RVA: 0x0018F3D0 File Offset: 0x0018D5D0
		public async UniTask RetryMessage()
		{
			if (this.lastPackage != null)
			{
				await this.Process(this.lastPackage);
			}
		}

		// Token: 0x06009C73 RID: 40051 RVA: 0x0018F413 File Offset: 0x0018D613
		public void Dispose()
		{
			this.log.Dispose();
			this.voice.Dispose();
			this.duel.Dispose();
		}

		// Token: 0x0400DAA7 RID: 55975
		public readonly LogMessage log;

		// Token: 0x0400DAA8 RID: 55976
		public readonly VoiceMessage voice;

		// Token: 0x0400DAA9 RID: 55977
		public readonly DuelMessage duel;

		// Token: 0x0400DAAA RID: 55978
		public bool playerResponed;

		// Token: 0x0400DAAB RID: 55979
		private Package lastPackage;
	}
}
