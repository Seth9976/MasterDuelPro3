using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WindBot.Game.AI
{
	// Token: 0x0200023F RID: 575
	public class Dialogs
	{
		// Token: 0x06000C9B RID: 3227 RVA: 0x0003692C File Offset: 0x00034B2C
		public Dialogs(GameClient game)
		{
			this._game = game;
			JsonSerializer serializer = new JsonSerializer();
			string dialogfilename = game.Dialog;
			using (FileStream fs = Program.ReadFile("Dialogs", dialogfilename, "json"))
			{
				using (StreamReader sr = new StreamReader(fs))
				{
					using (JsonTextReader jsonTextReader = new JsonTextReader(sr))
					{
						DialogsData data = serializer.Deserialize<DialogsData>(jsonTextReader);
						this._welcome = data.welcome;
						this._deckerror = data.deckerror;
						this._duelstart = data.duelstart;
						this._newturn = data.newturn;
						this._endturn = data.endturn;
						this._directattack = data.directattack;
						this._attack = data.attack;
						this._ondirectattack = data.ondirectattack;
						this._facedownmonstername = data.facedownmonstername;
						this._activate = data.activate;
						this._summon = data.summon;
						this._setmonster = data.setmonster;
						this._chaining = data.chaining;
						this._surrender = data.surrender;
						this._custom = data.custom;
					}
				}
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00036A8C File Offset: 0x00034C8C
		public void SendSorry()
		{
			this.InternalSendMessageForced(new string[] { "Sorry, an error occurs." }, Array.Empty<object>());
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00036AA7 File Offset: 0x00034CA7
		public void SendDeckSorry(string card)
		{
			if (card == "DECK")
			{
				this.InternalSendMessageForced(new string[] { "Deck illegal. Please check the database of your YGOPro and WindBot." }, Array.Empty<object>());
				return;
			}
			this.InternalSendMessageForced(this._deckerror, new object[] { card });
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00036AE6 File Offset: 0x00034CE6
		public void SendWelcome()
		{
			this.InternalSendMessage(this._welcome, Array.Empty<object>());
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00036AF9 File Offset: 0x00034CF9
		public void SendDuelStart()
		{
			this.InternalSendMessage(this._duelstart, Array.Empty<object>());
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00036B0C File Offset: 0x00034D0C
		public void SendNewTurn()
		{
			this.InternalSendMessage(this._newturn, Array.Empty<object>());
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00036B1F File Offset: 0x00034D1F
		public void SendEndTurn()
		{
			this.InternalSendMessage(this._endturn, Array.Empty<object>());
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00036B32 File Offset: 0x00034D32
		public void SendDirectAttack(string attacker)
		{
			this.InternalSendMessage(this._directattack, new object[] { attacker });
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00036B4A File Offset: 0x00034D4A
		public void SendAttack(string attacker, string defender)
		{
			if (defender == "monster")
			{
				defender = this._facedownmonstername;
			}
			this.InternalSendMessage(this._attack, new object[] { attacker, defender });
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00036B7B File Offset: 0x00034D7B
		public void SendOnDirectAttack(string attacker)
		{
			if (string.IsNullOrEmpty(attacker))
			{
				attacker = this._facedownmonstername;
			}
			this.InternalSendMessage(this._ondirectattack, new object[] { attacker });
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00036BA3 File Offset: 0x00034DA3
		public void SendOnDirectAttack()
		{
			this.InternalSendMessage(this._ondirectattack, Array.Empty<object>());
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00036BB6 File Offset: 0x00034DB6
		public void SendActivate(string spell)
		{
			this.InternalSendMessage(this._activate, new object[] { spell });
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00036BCE File Offset: 0x00034DCE
		public void SendSummon(string monster)
		{
			this.InternalSendMessage(this._summon, new object[] { monster });
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00036BE6 File Offset: 0x00034DE6
		public void SendSetMonster()
		{
			this.InternalSendMessage(this._setmonster, Array.Empty<object>());
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00036BF9 File Offset: 0x00034DF9
		public void SendChaining(string card)
		{
			this.InternalSendMessage(this._chaining, new object[] { card });
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00036C11 File Offset: 0x00034E11
		public void SendSurrender()
		{
			this.InternalSendMessage(this._surrender, Array.Empty<object>());
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00036C24 File Offset: 0x00034E24
		private void InternalSendMessage(IList<string> array, params object[] opts)
		{
			if (!this._game._chat)
			{
				return;
			}
			if (array == null || array.Count == 0)
			{
				return;
			}
			string message = string.Format(array[Program.Rand.Next(array.Count)], opts);
			if (message != "")
			{
				this._game.Chat(message);
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00036C84 File Offset: 0x00034E84
		private void InternalSendMessageForced(IList<string> array, params object[] opts)
		{
			string message = string.Format(array[Program.Rand.Next(array.Count)], opts);
			if (message != "")
			{
				this._game.Chat(message);
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00036CC8 File Offset: 0x00034EC8
		public void SendCustomChat(int index, params object[] opts)
		{
			if (!this._game._chat || this._custom == null)
			{
				return;
			}
			string message = string.Format(this._custom[index], opts);
			if (message != "")
			{
				this._game.Chat(message);
			}
		}

		// Token: 0x04000F9A RID: 3994
		private GameClient _game;

		// Token: 0x04000F9B RID: 3995
		private string[] _welcome;

		// Token: 0x04000F9C RID: 3996
		private string[] _deckerror;

		// Token: 0x04000F9D RID: 3997
		private string[] _duelstart;

		// Token: 0x04000F9E RID: 3998
		private string[] _newturn;

		// Token: 0x04000F9F RID: 3999
		private string[] _endturn;

		// Token: 0x04000FA0 RID: 4000
		private string[] _directattack;

		// Token: 0x04000FA1 RID: 4001
		private string[] _attack;

		// Token: 0x04000FA2 RID: 4002
		private string[] _ondirectattack;

		// Token: 0x04000FA3 RID: 4003
		private string _facedownmonstername;

		// Token: 0x04000FA4 RID: 4004
		private string[] _activate;

		// Token: 0x04000FA5 RID: 4005
		private string[] _summon;

		// Token: 0x04000FA6 RID: 4006
		private string[] _setmonster;

		// Token: 0x04000FA7 RID: 4007
		private string[] _chaining;

		// Token: 0x04000FA8 RID: 4008
		private string[] _surrender;

		// Token: 0x04000FA9 RID: 4009
		private string[] _custom;
	}
}
