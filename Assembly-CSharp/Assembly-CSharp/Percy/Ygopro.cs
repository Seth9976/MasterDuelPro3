using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using MDPro3.Servant;
using Meisui.Random;

namespace Percy
{
	// Token: 0x020011E6 RID: 4582
	public class Ygopro
	{
		// Token: 0x06008803 RID: 34819 RVA: 0x000F7934 File Offset: 0x000F5B34
		public Ygopro(Action<byte[]> HowToSendBufferToPlayer, Ygopro.CardHandler HowToReadCard, Ygopro.ScriptHandler HowToReadScript, Ygopro.ChatHandler HowToShowLog)
		{
			this.sendToPlayer = HowToSendBufferToPlayer;
			Dll.Set_card_api(HowToReadCard);
			Dll.Set_script_api(HowToReadScript);
			Dll.Set_chat_api(HowToShowLog);
			this.cast = HowToShowLog;
			Random ran = new Random(Environment.TickCount);
			this.duel = Dll.create_duel((uint)ran.Next(100, 99999));
		}

		// Token: 0x06008804 RID: 34820 RVA: 0x000F799C File Offset: 0x000F5B9C
		public void Dispose()
		{
			Dll.end_duel(this.duel);
			Random ran = new Random(Environment.TickCount);
			this.duel = Dll.create_duel((uint)ran.Next(100, 99999));
		}

		// Token: 0x06008805 RID: 34821 RVA: 0x000F79D7 File Offset: 0x000F5BD7
		private void DebugLog(string obj)
		{
			Action<string> log = this.m_log;
			if (log == null)
			{
				return;
			}
			log(obj);
		}

		// Token: 0x06008806 RID: 34822 RVA: 0x000F79EC File Offset: 0x000F5BEC
		private int Move(int length, bool erase = false)
		{
			int returnValue = 0;
			if (length > 0 && this.currentReader != null && this.currentWriter != null)
			{
				try
				{
					byte[] readed = this.currentReader.ReadBytes(length);
					if (readed.Length != 0)
					{
						returnValue = (int)readed[0];
					}
					if (erase)
					{
						for (int i = 0; i < length; i++)
						{
							this.currentWriter.Write(0);
						}
					}
					else
					{
						this.currentWriter.Write(readed);
					}
				}
				catch
				{
				}
			}
			return returnValue;
		}

		// Token: 0x06008807 RID: 34823 RVA: 0x000F7A64 File Offset: 0x000F5C64
		private void Flush()
		{
			this.sendToPlayer(((MemoryStream)this.currentWriter.BaseStream).ToArray());
		}

		// Token: 0x06008808 RID: 34824 RVA: 0x000F7A86 File Offset: 0x000F5C86
		private int LocalPlayer(int p)
		{
			if (Ygopro.isFirst)
			{
				return p;
			}
			return 1 - p;
		}

		// Token: 0x06008809 RID: 34825 RVA: 0x000F7A94 File Offset: 0x000F5C94
		private void Refresh()
		{
			if (this.godMode)
			{
				this.RefreshMonsters(0, 598015);
				this.RefreshMonsters(1, 598015);
				this.RefreshSpells(0, 6823935);
				this.RefreshSpells(1, 6823935);
				this.RefreshHand(0, 1581055);
				this.RefreshHand(1, 1581055);
				this.RefreshGrave(0, 532479);
				this.RefreshGrave(1, 532479);
				this.RefreshExtra(0, 532479);
				this.RefreshExtra(1, 532479);
				this.RefreshDeck(0, 532479);
				this.RefreshDeck(1, 532479);
				this.RefreshRemoved(0, 532479);
				this.RefreshRemoved(1, 532479);
				return;
			}
			if (Ygopro.isFirst)
			{
				this.RefreshMonsters(0, 598015);
				this.RefreshMonsters(1, 598015);
				this.RefreshSpells(0, 6823935);
				this.RefreshSpells(1, 6823935);
				this.RefreshGrave(0, 532479);
				this.RefreshGrave(1, 532479);
				this.RefreshHand(0, 1581055);
				this.RefreshExtra(0, 532479);
				this.RefreshRemoved(0, 532479);
				return;
			}
			this.RefreshMonsters(0, 598015);
			this.RefreshMonsters(1, 598015);
			this.RefreshSpells(0, 6823935);
			this.RefreshSpells(1, 6823935);
			this.RefreshGrave(0, 532479);
			this.RefreshGrave(1, 532479);
			this.RefreshHand(1, 1581055);
			this.RefreshExtra(1, 532479);
			this.RefreshRemoved(1, 532479);
		}

		// Token: 0x0600880A RID: 34826 RVA: 0x000F7C38 File Offset: 0x000F5E38
		private byte[] QueryFieldCard(int player, CardLocation location, int flag, bool useCache)
		{
			int len = Dll.query_field_card(this.duel, (byte)player, (byte)location, flag, this._buffer, useCache ? 1 : 0);
			byte[] result = new byte[len];
			Marshal.Copy(this._buffer, result, 0, len);
			return result;
		}

		// Token: 0x0600880B RID: 34827 RVA: 0x000F7C7C File Offset: 0x000F5E7C
		private void RefreshMonsters(int player, int flag = 598015)
		{
			byte[] array = this.QueryFieldCard(player, CardLocation.MonsterZone, flag, false);
			BinaryMaster binary = new BinaryMaster(null);
			binary.writer.Write(6);
			binary.writer.Write((byte)player);
			binary.writer.Write(4);
			BinaryReader reader = new BinaryReader(new MemoryStream(array));
			for (int i = 0; i < 7; i++)
			{
				int len = reader.ReadInt32();
				if (len == 4)
				{
					binary.writer.Write(4);
				}
				else
				{
					byte[] raw = reader.ReadBytes(len - 4);
					if ((raw[11] & 10) != 0 && !this.godMode && this.LocalPlayer(player) != 0)
					{
						binary.writer.Write(8);
						binary.writer.Write(0);
					}
					else
					{
						binary.writer.Write(len);
						binary.writer.Write(raw);
					}
				}
			}
			this.sendToPlayer(binary.Get());
		}

		// Token: 0x0600880C RID: 34828 RVA: 0x000F7D5C File Offset: 0x000F5F5C
		private void RefreshSpells(int player, int flag = 6823935)
		{
			byte[] array = this.QueryFieldCard(player, CardLocation.SpellZone, flag, false);
			BinaryMaster binary = new BinaryMaster(null);
			binary.writer.Write(6);
			binary.writer.Write((byte)player);
			binary.writer.Write(8);
			BinaryReader reader = new BinaryReader(new MemoryStream(array));
			for (int i = 0; i < 8; i++)
			{
				int len = reader.ReadInt32();
				if (len == 4)
				{
					binary.writer.Write(4);
				}
				else
				{
					byte[] raw = reader.ReadBytes(len - 4);
					if ((raw[11] & 10) != 0 && !this.godMode && this.LocalPlayer(player) != 0)
					{
						binary.writer.Write(8);
						binary.writer.Write(0);
					}
					else
					{
						binary.writer.Write(len);
						binary.writer.Write(raw);
					}
				}
			}
			this.sendToPlayer(binary.Get());
		}

		// Token: 0x0600880D RID: 34829 RVA: 0x000F7E3C File Offset: 0x000F603C
		private void RefreshHand(int player, int flag = 1581055)
		{
			byte[] result = this.QueryFieldCard(player, CardLocation.Hand, flag, false);
			BinaryMaster binary = new BinaryMaster(null);
			binary.writer.Write(6);
			binary.writer.Write((byte)player);
			binary.writer.Write(2);
			binary.writer.Write(result);
			this.sendToPlayer(binary.Get());
		}

		// Token: 0x0600880E RID: 34830 RVA: 0x000F7EA0 File Offset: 0x000F60A0
		private void RefreshGrave(int player, int flag = 532479)
		{
			byte[] result = this.QueryFieldCard(player, CardLocation.Grave, flag, false);
			BinaryMaster binary = new BinaryMaster(null);
			binary.writer.Write(6);
			binary.writer.Write((byte)player);
			binary.writer.Write(16);
			binary.writer.Write(result);
			this.sendToPlayer(binary.Get());
		}

		// Token: 0x0600880F RID: 34831 RVA: 0x000F7F04 File Offset: 0x000F6104
		private void RefreshDeck(int player, int flag = 532479)
		{
			byte[] result = this.QueryFieldCard(player, CardLocation.Deck, flag, false);
			BinaryMaster binary = new BinaryMaster(null);
			binary.writer.Write(6);
			binary.writer.Write((byte)player);
			binary.writer.Write(1);
			binary.writer.Write(result);
			this.sendToPlayer(binary.Get());
		}

		// Token: 0x06008810 RID: 34832 RVA: 0x000F7F68 File Offset: 0x000F6168
		private void RefreshExtra(int player, int flag = 532479)
		{
			byte[] result = this.QueryFieldCard(player, CardLocation.Extra, flag, false);
			BinaryMaster binary = new BinaryMaster(null);
			binary.writer.Write(6);
			binary.writer.Write((byte)player);
			binary.writer.Write(64);
			binary.writer.Write(result);
			this.sendToPlayer(binary.Get());
		}

		// Token: 0x06008811 RID: 34833 RVA: 0x000F7FCC File Offset: 0x000F61CC
		private void RefreshRemoved(int player, int flag = 532479)
		{
			byte[] result = this.QueryFieldCard(player, CardLocation.Removed, flag, false);
			BinaryMaster binary = new BinaryMaster(null);
			binary.writer.Write(6);
			binary.writer.Write((byte)player);
			binary.writer.Write(32);
			binary.writer.Write(result);
			this.sendToPlayer(binary.Get());
		}

		// Token: 0x06008812 RID: 34834 RVA: 0x000F8030 File Offset: 0x000F6230
		private bool Analyse(BinaryReader reader)
		{
			bool returnValue = false;
			this.currentReader = reader;
			MemoryStream me = new MemoryStream();
			this.currentWriter = new BinaryWriter(me);
			GameMessage mes = (GameMessage)this.Move(1, false);
			switch (mes)
			{
			case GameMessage.Retry:
				returnValue = true;
				this.err = true;
				goto IL_0BDF;
			case GameMessage.Hint:
				this.Move(6, false);
				goto IL_0BDF;
			case GameMessage.Waiting:
			case GameMessage.Start:
			case GameMessage.UpdateData:
			case GameMessage.UpdateCard:
			case GameMessage.RequestDeck:
			case GameMessage.ReverseDeck:
			case GameMessage.Summoned:
			case GameMessage.SpSummoned:
			case GameMessage.FlipSummoned:
			case GameMessage.ChainEnd:
			case GameMessage.AttackDiabled:
			case GameMessage.DamageStepStart:
			case GameMessage.DamageStepEnd:
			case GameMessage.BeChainTarget:
			case GameMessage.CreateRelation:
			case GameMessage.ReleaseRelation:
				goto IL_0BDF;
			case GameMessage.Win:
				this.Move(2, false);
				returnValue = true;
				this.end = true;
				goto IL_0BDF;
			case (GameMessage)9:
			case (GameMessage)17:
			case (GameMessage)27:
			case (GameMessage)28:
			case (GameMessage)29:
			case GameMessage.ShuffleExtra:
			case GameMessage.ConfirmExtratop:
			case (GameMessage)43:
			case (GameMessage)44:
			case (GameMessage)45:
			case (GameMessage)46:
			case (GameMessage)47:
			case (GameMessage)48:
			case (GameMessage)49:
			case (GameMessage)51:
			case (GameMessage)52:
			case (GameMessage)57:
			case (GameMessage)58:
			case (GameMessage)59:
			case (GameMessage)66:
			case (GameMessage)67:
			case (GameMessage)68:
			case (GameMessage)69:
			case (GameMessage)77:
			case (GameMessage)78:
			case (GameMessage)79:
			case (GameMessage)82:
			case (GameMessage)84:
			case (GameMessage)85:
			case (GameMessage)86:
			case (GameMessage)87:
			case (GameMessage)88:
			case (GameMessage)89:
			case (GameMessage)98:
			case (GameMessage)99:
			case (GameMessage)103:
			case (GameMessage)104:
			case (GameMessage)105:
			case (GameMessage)106:
			case (GameMessage)107:
			case (GameMessage)108:
			case (GameMessage)109:
			case (GameMessage)115:
			case (GameMessage)116:
			case (GameMessage)117:
			case (GameMessage)118:
			case (GameMessage)119:
			case (GameMessage)124:
			case (GameMessage)125:
			case (GameMessage)126:
			case (GameMessage)127:
			case (GameMessage)128:
			case (GameMessage)129:
			case GameMessage.RockPaperScissors:
			case GameMessage.HandResult:
			case (GameMessage)134:
			case (GameMessage)135:
			case (GameMessage)136:
			case (GameMessage)137:
			case (GameMessage)138:
			case (GameMessage)139:
			case (GameMessage)144:
			case (GameMessage)145:
			case (GameMessage)146:
			case (GameMessage)147:
			case (GameMessage)148:
			case (GameMessage)149:
			case (GameMessage)150:
			case (GameMessage)151:
			case (GameMessage)152:
			case (GameMessage)153:
			case (GameMessage)154:
			case (GameMessage)155:
			case (GameMessage)156:
			case (GameMessage)157:
			case (GameMessage)158:
			case (GameMessage)159:
			case (GameMessage)166:
			case (GameMessage)167:
			case (GameMessage)168:
			case (GameMessage)169:
				break;
			case GameMessage.SelectBattleCmd:
				this.Move(1, false);
				this.Move(this.Move(1, false) * 11, false);
				this.Move(this.Move(1, false) * 8 + 2, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.SelectIdleCmd:
				this.Move(1, false);
				this.Move(this.Move(1, false) * 7, false);
				this.Move(this.Move(1, false) * 7, false);
				this.Move(this.Move(1, false) * 7, false);
				this.Move(this.Move(1, false) * 7, false);
				this.Move(this.Move(1, false) * 7, false);
				this.Move(this.Move(1, false) * 11 + 3, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.SelectEffectYn:
				this.Move(13, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.SelectYesNo:
				this.Move(5, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.SelectOption:
				this.Move(1, false);
				this.Move(this.Move(1, false) * 4, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.SelectCard:
			case GameMessage.SelectTribute:
			{
				int player = this.Move(1, false);
				this.Move(3, false);
				int count = this.Move(1, false);
				for (int i = 0; i < count; i++)
				{
					int code = this.currentReader.ReadInt32();
					int p = (int)this.currentReader.ReadByte();
					this.currentWriter.Write((p == player) ? code : 0);
					this.currentWriter.Write((byte)p);
					this.Move(3, false);
				}
				returnValue = true;
				goto IL_0BDF;
			}
			case GameMessage.SelectChain:
			{
				this.Move(1, false);
				int count = this.Move(1, false);
				this.Move(1, false);
				this.Move(4, false);
				this.Move(4, false);
				for (int j = 0; j < count; j++)
				{
					this.Move(1, false);
					this.Move(1, false);
					this.Move(4, false);
					this.Move(4, false);
					this.Move(4, false);
				}
				returnValue = true;
				goto IL_0BDF;
			}
			case GameMessage.SelectPlace:
			case GameMessage.SelectPosition:
			case GameMessage.SelectDisfield:
				this.Move(6, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.SortChain:
			case GameMessage.SortCard:
				this.Move(1, false);
				this.Move(this.Move(1, false) * 7, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.SelectCounter:
				this.Move(5, false);
				this.Move(this.Move(1, false) * 9, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.SelectSum:
				this.Move(8, false);
				this.Move(this.Move(1, false) * 11, false);
				this.Move(this.Move(1, false) * 11, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.SelectUnselectCard:
			{
				int player = this.Move(1, false);
				this.Move(1, false);
				this.Move(3, false);
				int count2 = this.Move(1, false);
				for (int k = 0; k < count2; k++)
				{
					int code2 = this.currentReader.ReadInt32();
					int p2 = (int)this.currentReader.ReadByte();
					this.currentWriter.Write((p2 == player) ? code2 : 0);
					this.currentWriter.Write((byte)p2);
					this.Move(3, false);
				}
				int count3 = this.Move(1, false);
				for (int l = 0; l < count3; l++)
				{
					int code3 = this.currentReader.ReadInt32();
					int p3 = (int)this.currentReader.ReadByte();
					this.currentWriter.Write((p3 == player) ? code3 : 0);
					this.currentWriter.Write((byte)p3);
					this.Move(3, false);
				}
				returnValue = true;
				goto IL_0BDF;
			}
			case GameMessage.ConfirmDecktop:
				this.Move(1, false);
				this.Move(this.Move(1, false) * 7, false);
				goto IL_0BDF;
			case GameMessage.ConfirmCards:
				this.Move(1, false);
				this.Move(1, false);
				this.Move(this.Move(1, false) * 7, false);
				goto IL_0BDF;
			case GameMessage.ShuffleDeck:
			case GameMessage.RefreshDeck:
				this.Move(1, false);
				goto IL_0BDF;
			case GameMessage.ShuffleHand:
				this.Move(1, false);
				this.Move(this.Move(1, false) * 4, false);
				goto IL_0BDF;
			case GameMessage.SwapGraveDeck:
				this.Move(1, false);
				goto IL_0BDF;
			case GameMessage.ShuffleSetCard:
				this.Move(1, false);
				this.Move(this.Move(1, false) * 8, false);
				goto IL_0BDF;
			case GameMessage.DeckTop:
				this.Move(6, false);
				goto IL_0BDF;
			case GameMessage.NewTurn:
				this.Move(1, false);
				goto IL_0BDF;
			case GameMessage.NewPhase:
				this.Move(2, false);
				goto IL_0BDF;
			case GameMessage.Move:
			{
				byte[] raw = this.currentReader.ReadBytes(16);
				byte b = raw[4];
				byte b2 = raw[5];
				byte b3 = raw[8];
				int cl = (int)raw[9];
				byte b4 = raw[10];
				int cp = (int)raw[11];
				if (!OcgCore.nextMoveNeedCode && ((!Convert.ToBoolean(cl & 144) && Convert.ToBoolean(cl & 3)) || Convert.ToBoolean(cp & 10)))
				{
					raw[0] = 0;
					raw[1] = 0;
					raw[2] = 0;
					raw[3] = 0;
				}
				this.currentWriter.Write(raw);
				goto IL_0BDF;
			}
			case GameMessage.PosChange:
				this.Move(9, false);
				goto IL_0BDF;
			case GameMessage.Set:
				this.Move(4, true);
				this.Move(4, false);
				goto IL_0BDF;
			case GameMessage.Swap:
				this.Move(16, false);
				goto IL_0BDF;
			case GameMessage.FieldDisabled:
				this.Move(4, false);
				goto IL_0BDF;
			case GameMessage.Summoning:
				this.Move(8, false);
				goto IL_0BDF;
			case GameMessage.SpSummoning:
				this.Move(8, false);
				goto IL_0BDF;
			case GameMessage.FlipSummoning:
				this.Move(8, false);
				goto IL_0BDF;
			case GameMessage.Chaining:
				this.Move(16, false);
				goto IL_0BDF;
			case GameMessage.Chained:
				this.Move(1, false);
				goto IL_0BDF;
			case GameMessage.ChainSolving:
				this.Move(1, false);
				goto IL_0BDF;
			case GameMessage.ChainSolved:
				this.Move(1, false);
				goto IL_0BDF;
			case GameMessage.ChainNegated:
			case GameMessage.ChainDisabled:
				this.Move(1, false);
				goto IL_0BDF;
			case GameMessage.CardSelected:
				this.Move(1, false);
				this.Move(this.Move(1, false) * 4, false);
				goto IL_0BDF;
			case GameMessage.RandomSelected:
				this.Move(1, false);
				this.Move(this.Move(1, false) * 4, false);
				goto IL_0BDF;
			case GameMessage.BecomeTarget:
				this.Move(this.Move(1, false) * 4, false);
				goto IL_0BDF;
			case GameMessage.Draw:
			{
				int player = this.Move(1, false);
				int count = this.Move(1, false);
				for (int m = 0; m < count; m++)
				{
					int code4 = this.currentReader.ReadInt32() & int.MaxValue;
					if (Ygopro.isFirst)
					{
						if (player == 0)
						{
							this.currentWriter.Write(code4);
						}
						else
						{
							this.currentWriter.Write(0);
						}
					}
					else if (player == 0)
					{
						this.currentWriter.Write(0);
					}
					else
					{
						this.currentWriter.Write(code4);
					}
				}
				goto IL_0BDF;
			}
			case GameMessage.Damage:
			case GameMessage.Recover:
			case GameMessage.LpUpdate:
			case GameMessage.PayLpCost:
				this.Move(5, false);
				goto IL_0BDF;
			case GameMessage.Equip:
				this.Move(8, false);
				goto IL_0BDF;
			case GameMessage.Unequip:
				this.Move(4, false);
				goto IL_0BDF;
			case GameMessage.CardTarget:
			case GameMessage.CancelTarget:
				this.Move(8, false);
				goto IL_0BDF;
			case GameMessage.AddCounter:
			case GameMessage.RemoveCounter:
				this.Move(7, false);
				goto IL_0BDF;
			case GameMessage.Attack:
				this.Move(8, false);
				goto IL_0BDF;
			case GameMessage.Battle:
				this.Move(26, false);
				goto IL_0BDF;
			case GameMessage.MissedEffect:
				this.Move(8, false);
				goto IL_0BDF;
			case GameMessage.TossCoin:
			case GameMessage.TossDice:
				this.Move(1, false);
				this.Move(this.Move(1, false), false);
				goto IL_0BDF;
			case GameMessage.AnnounceRace:
				this.Move(6, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.AnnounceAttrib:
				this.Move(6, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.AnnounceCard:
			case GameMessage.AnnounceNumber:
				this.Move(1, false);
				this.Move(this.Move(1, false) * 4, false);
				returnValue = true;
				goto IL_0BDF;
			case GameMessage.CardHint:
				this.Move(9, false);
				goto IL_0BDF;
			case GameMessage.TagSwap:
			{
				int player = this.Move(1, false);
				this.Move(1, false);
				int ecount = this.Move(1, false);
				this.Move(1, false);
				int hcount = this.Move(1, false);
				this.Move(4, false);
				for (int n = 0; n < hcount + ecount; n++)
				{
					uint code5 = this.currentReader.ReadUInt32();
					if ((code5 & 2147483648U) != 0U)
					{
						this.currentWriter.Write(code5);
					}
					else
					{
						this.currentWriter.Write(0);
					}
				}
				goto IL_0BDF;
			}
			case GameMessage.ReloadField:
			{
				this.Move(1, false);
				for (int i_ = 0; i_ < 2; i_++)
				{
					this.Move(4, false);
					for (int i2 = 0; i2 < 7; i2++)
					{
						if (this.Move(1, false) > 0)
						{
							this.Move(2, false);
						}
					}
					for (int i3 = 0; i3 < 8; i3++)
					{
						if (this.Move(1, false) > 0)
						{
							this.Move(1, false);
						}
					}
					this.Move(1, false);
					this.Move(1, false);
					this.Move(1, false);
					this.Move(1, false);
					this.Move(1, false);
					this.Move(1, false);
					this.Move(this.Move(1, false) * 15, false);
				}
				goto IL_0BDF;
			}
			case GameMessage.AiName:
			{
				ushort length = this.currentReader.ReadUInt16();
				this.currentWriter.Write(length);
				this.Move((int)(length + 1), false);
				goto IL_0BDF;
			}
			case GameMessage.ShowHint:
			{
				ushort length2 = this.currentReader.ReadUInt16();
				this.currentWriter.Write(length2);
				this.Move((int)(length2 + 1), false);
				goto IL_0BDF;
			}
			case GameMessage.PlayerHint:
				this.Move(6, false);
				goto IL_0BDF;
			case GameMessage.MatchKill:
				this.Move(4, false);
				goto IL_0BDF;
			default:
				if (mes == GameMessage.CustomMsg || mes == GameMessage.DuelWinner)
				{
					goto IL_0BDF;
				}
				break;
			}
			returnValue = true;
			IL_0BDF:
			this.Flush();
			if (mes <= GameMessage.ChainDisabled)
			{
				if (mes <= GameMessage.Set)
				{
					if (mes - GameMessage.ShuffleDeck > 6 && mes != GameMessage.Set)
					{
						goto IL_0C54;
					}
				}
				else
				{
					switch (mes)
					{
					case GameMessage.Summoned:
					case GameMessage.SpSummoned:
					case GameMessage.FlipSummoned:
						break;
					case GameMessage.SpSummoning:
					case GameMessage.FlipSummoning:
						goto IL_0C54;
					default:
						if (mes - GameMessage.ChainSolved > 3)
						{
							goto IL_0C54;
						}
						break;
					}
				}
			}
			else if (mes <= GameMessage.Battle)
			{
				if (mes != GameMessage.Draw && mes != GameMessage.Battle)
				{
					goto IL_0C54;
				}
			}
			else if (mes != GameMessage.DamageStepEnd && mes - GameMessage.TagSwap > 1)
			{
				goto IL_0C54;
			}
			this.Refresh();
			IL_0C54:
			this.DebugLog(mes.ToString() + (returnValue ? (" Wating Buffer:\n" + BitConverter.ToString(((MemoryStream)this.currentWriter.BaseStream).ToArray())) : ""));
			return returnValue;
		}

		// Token: 0x06008813 RID: 34835 RVA: 0x000F8CD8 File Offset: 0x000F6ED8
		private void Process()
		{
			for (;;)
			{
				int len = Dll.process(this.duel) & 65535;
				if (len > 0)
				{
					byte[] arr = new byte[8192];
					int num = Dll.get_message(this.duel, this._buffer);
					Marshal.Copy(this._buffer, arr, 0, 8192);
					bool breakOut = false;
					MemoryStream stream = new MemoryStream(arr);
					BinaryReader reader = new BinaryReader(stream);
					while (stream.Position < (long)len)
					{
						breakOut = this.Analyse(reader);
					}
					if (breakOut)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06008814 RID: 34836 RVA: 0x000F8D54 File Offset: 0x000F6F54
		private IntPtr GetPtrString(string path)
		{
			byte[] s = Encoding.UTF8.GetBytes(path);
			List<byte> list = s.ToList<byte>();
			list.Add(0);
			s = list.ToArray();
			IntPtr ptrFileName = Marshal.AllocHGlobal(s.Length);
			Marshal.Copy(s, 0, ptrFileName, s.Length);
			Marshal.WriteByte(ptrFileName, s.Length, 0);
			return ptrFileName;
		}

		// Token: 0x06008815 RID: 34837 RVA: 0x000F8DA0 File Offset: 0x000F6FA0
		private Deck FromYDKtoDeck(string path)
		{
			Deck deck = new Deck();
			try
			{
				string[] array = File.ReadAllText(path).Replace("\r", "").Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
				int flag = -1;
				foreach (string line in array)
				{
					if (line == "#main")
					{
						flag = 1;
					}
					else if (line == "#extra")
					{
						flag = 2;
					}
					else if (line == "!side")
					{
						flag = 3;
					}
					else
					{
						int code = 0;
						try
						{
							code = int.Parse(line);
						}
						catch (Exception)
						{
						}
						if (code > 100)
						{
							switch (flag)
							{
							case 1:
								deck.Main.Add(code);
								break;
							case 2:
								deck.Extra.Add(code);
								break;
							case 3:
								deck.Side.Add(code);
								break;
							}
						}
					}
				}
			}
			catch
			{
			}
			return deck;
		}

		// Token: 0x06008816 RID: 34838 RVA: 0x000F8EA8 File Offset: 0x000F70A8
		private void AddDeck(Deck deck, int playerId, bool random)
		{
			if (random)
			{
				Random seed = new Random();
				for (int i = 0; i < deck.Main.Count; i++)
				{
					int random_index = seed.Next() % deck.Main.Count;
					int t = deck.Main[i];
					deck.Main[i] = deck.Main[random_index];
					deck.Main[random_index] = t;
				}
			}
			for (int j = deck.Main.Count - 1; j >= 0; j--)
			{
				Dll.new_card(this.duel, (uint)deck.Main[j], (byte)playerId, (byte)playerId, 1, 0, 8);
			}
			for (int k = 0; k < deck.Extra.Count; k++)
			{
				Dll.new_card(this.duel, (uint)deck.Extra[k], (byte)playerId, (byte)playerId, 64, 0, 8);
			}
		}

		// Token: 0x06008817 RID: 34839 RVA: 0x000F8F8E File Offset: 0x000F718E
		private void AddDeckFromFile(string playerDek, int playerId, bool random)
		{
			this.AddDeck(this.FromYDKtoDeck(playerDek), playerId, random);
		}

		// Token: 0x06008818 RID: 34840 RVA: 0x000F8FA0 File Offset: 0x000F71A0
		public bool StartPuzzle(string path)
		{
			this.godMode = true;
			Ygopro.isFirst = true;
			Dll.set_player_info(this.duel, 0, 8000, 5, 1);
			Dll.set_player_info(this.duel, 1, 8000, 5, 1);
			Dll.preload_script(this.duel, this.GetPtrString(path));
			Dll.start_duel(this.duel, 0U);
			this.Refresh();
			new Thread(new ThreadStart(this.Process)).Start();
			return true;
		}

		// Token: 0x06008819 RID: 34841 RVA: 0x000F901C File Offset: 0x000F721C
		public void Response(byte[] resp)
		{
			if (resp.Length > 64)
			{
				return;
			}
			IntPtr buf = Marshal.AllocHGlobal(64);
			Marshal.Copy(resp, 0, buf, resp.Length);
			Dll.set_responseb(this.duel, buf);
			Marshal.FreeHGlobal(buf);
			new Thread(new ThreadStart(this.Process)).Start();
		}

		// Token: 0x0600881A RID: 34842 RVA: 0x000F906C File Offset: 0x000F726C
		private void SendToYrp(byte[] buffer)
		{
			this.yrp3dbuilder.Write(buffer[0]);
			this.yrp3dbuilder.Write(buffer.Length - 1);
			for (int i = 1; i < buffer.Length; i++)
			{
				this.yrp3dbuilder.Write(buffer[i]);
			}
		}

		// Token: 0x0600881B RID: 34843 RVA: 0x000F90B4 File Offset: 0x000F72B4
		public byte[] GetYRP3dBuffer(YRP yrp)
		{
			Action<byte[]> tempS = this.sendToPlayer;
			this.sendToPlayer = new Action<byte[]>(this.SendToYrp);
			MemoryStream stream = new MemoryStream();
			this.yrp3dbuilder = new BinaryWriter(stream);
			this.sendToPlayer(yrp.GetNamePacket());
			Dll.end_duel(this.duel);
			if (yrp.ID == 846230137)
			{
				this.duel = Dll.create_duel_v2(yrp.SeedsV2);
				OcgCore.CurrentReplayUseYRP2 = true;
			}
			else
			{
				MersenneTwister mtrnd = new MersenneTwister(yrp.Seed);
				this.duel = Dll.create_duel(mtrnd.genrand_Int32());
				OcgCore.CurrentReplayUseYRP2 = false;
			}
			this.godMode = true;
			Ygopro.isFirst = true;
			Dll.set_player_info(this.duel, 0, yrp.StartLp, yrp.StartHand, yrp.DrawCount);
			Dll.set_player_info(this.duel, 1, yrp.StartLp, yrp.StartHand, yrp.DrawCount);
			if (yrp.playerData.Count == 4)
			{
				foreach (int item in yrp.playerData[0].main)
				{
					Dll.new_card(this.duel, (uint)item, 0, 0, 1, 0, 8);
				}
				foreach (int item2 in yrp.playerData[0].extra)
				{
					Dll.new_card(this.duel, (uint)item2, 0, 0, 64, 0, 8);
				}
				foreach (int item3 in yrp.playerData[1].main)
				{
					Dll.new_tag_card(this.duel, (uint)item3, 0, 1);
				}
				foreach (int item4 in yrp.playerData[1].extra)
				{
					Dll.new_tag_card(this.duel, (uint)item4, 0, 64);
				}
				foreach (int item5 in yrp.playerData[2].main)
				{
					Dll.new_card(this.duel, (uint)item5, 1, 1, 1, 0, 8);
				}
				foreach (int item6 in yrp.playerData[2].extra)
				{
					Dll.new_card(this.duel, (uint)item6, 1, 1, 64, 0, 8);
				}
				foreach (int item7 in yrp.playerData[3].main)
				{
					Dll.new_tag_card(this.duel, (uint)item7, 1, 1);
				}
				using (List<int>.Enumerator enumerator = yrp.playerData[3].extra.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int item8 = enumerator.Current;
						Dll.new_tag_card(this.duel, (uint)item8, 1, 64);
					}
					goto IL_048F;
				}
			}
			foreach (int item9 in yrp.playerData[0].main)
			{
				Dll.new_card(this.duel, (uint)item9, 0, 0, 1, 0, 8);
			}
			foreach (int item10 in yrp.playerData[0].extra)
			{
				Dll.new_card(this.duel, (uint)item10, 0, 0, 64, 0, 8);
			}
			foreach (int item11 in yrp.playerData[1].main)
			{
				Dll.new_card(this.duel, (uint)item11, 1, 1, 1, 0, 8);
			}
			foreach (int item12 in yrp.playerData[1].extra)
			{
				Dll.new_card(this.duel, (uint)item12, 1, 1, 64, 0, 8);
			}
			IL_048F:
			BinaryMaster master = new BinaryMaster(null);
			master.writer.Write('\u0004');
			master.writer.Write(0);
			master.writer.Write((byte)(yrp.opt >> 16));
			master.writer.Write(yrp.StartLp);
			master.writer.Write(yrp.StartLp);
			master.writer.Write((ushort)Dll.query_field_count(this.duel, 0, 1));
			master.writer.Write((ushort)Dll.query_field_count(this.duel, 0, 64));
			master.writer.Write((ushort)Dll.query_field_count(this.duel, 1, 1));
			master.writer.Write((ushort)Dll.query_field_count(this.duel, 1, 64));
			this.sendToPlayer(master.Get());
			Dll.start_duel(this.duel, yrp.opt);
			this.Refresh();
			this.end = false;
			this.err = false;
			try
			{
				do
				{
					this.Process();
					if (yrp.gameData.Count == 0 || yrp.gameData[0].Length > 64)
					{
						break;
					}
					IntPtr buf = Marshal.AllocHGlobal(64);
					Marshal.Copy(yrp.gameData[0], 0, buf, yrp.gameData[0].Length);
					Dll.set_responseb(this.duel, buf);
					Marshal.FreeHGlobal(buf);
					this.DebugLog("Push:  " + BitConverter.ToString(yrp.gameData[0]));
					yrp.gameData.RemoveAt(0);
				}
				while (!this.end);
			}
			catch (Exception)
			{
			}
			if (this.err && this.cast != null)
			{
				this.cast("Error Occurred.");
			}
			this.Dispose();
			this.sendToPlayer = tempS;
			this.yrp3dbuilder.Close();
			stream.Close();
			return stream.ToArray();
		}

		// Token: 0x0400C2F2 RID: 49906
		public static string HintInGame = "PercyAI Pro2Team 1033.D";

		// Token: 0x0400C2F3 RID: 49907
		private BinaryReader currentReader;

		// Token: 0x0400C2F4 RID: 49908
		private BinaryWriter currentWriter;

		// Token: 0x0400C2F5 RID: 49909
		private bool end;

		// Token: 0x0400C2F6 RID: 49910
		private bool err;

		// Token: 0x0400C2F7 RID: 49911
		public static bool isFirst = true;

		// Token: 0x0400C2F8 RID: 49912
		private readonly Ygopro.ChatHandler cast;

		// Token: 0x0400C2F9 RID: 49913
		public Action<string> m_log;

		// Token: 0x0400C2FA RID: 49914
		private readonly IntPtr _buffer = Marshal.AllocHGlobal(8192);

		// Token: 0x0400C2FB RID: 49915
		private IntPtr duel;

		// Token: 0x0400C2FC RID: 49916
		private Action<byte[]> sendToPlayer;

		// Token: 0x0400C2FD RID: 49917
		private bool godMode;

		// Token: 0x0400C2FE RID: 49918
		private BinaryWriter yrp3dbuilder;

		// Token: 0x020011E7 RID: 4583
		// (Invoke) Token: 0x0600881E RID: 34846
		public delegate CardData CardHandler(long code);

		// Token: 0x020011E8 RID: 4584
		// (Invoke) Token: 0x06008822 RID: 34850
		public delegate ScriptData ScriptHandler(string name);

		// Token: 0x020011E9 RID: 4585
		// (Invoke) Token: 0x06008826 RID: 34854
		public delegate void ChatHandler(string str);
	}
}
