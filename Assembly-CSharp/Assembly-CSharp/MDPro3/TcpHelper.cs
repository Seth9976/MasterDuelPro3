using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.Servant;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x02001218 RID: 4632
	public static class TcpHelper
	{
		// Token: 0x0600894C RID: 35148 RVA: 0x00109434 File Offset: 0x00107634
		public static bool LinkStart(string ipString, string name, string portString, string pswString, bool local, Action doWhenSuccess)
		{
			if (!TcpHelper.canJoin)
			{
				return false;
			}
			Thread thread = TcpHelper.linkThread;
			if (thread != null)
			{
				thread.Abort();
			}
			TcpHelper.canJoin = false;
			TcpHelper.linkThread = new Thread(delegate
			{
				bool joined = false;
				if (local)
				{
					while (YgoServer.ServerRunning())
					{
						if (joined)
						{
							break;
						}
						try
						{
							joined = TcpHelper.Join(ipString, name, portString, pswString, doWhenSuccess);
						}
						catch (Exception ex)
						{
							Debug.LogException(ex);
							joined = false;
						}
						Thread.Sleep(100);
					}
				}
				else
				{
					TcpHelper.Join(ipString, name, portString, pswString, doWhenSuccess);
				}
				TcpHelper.canJoin = true;
			});
			TcpHelper.linkThread.Start();
			return true;
		}

		// Token: 0x0600894D RID: 35149 RVA: 0x001094B4 File Offset: 0x001076B4
		private static bool Join(string ipString, string name, string portString, string pswString, Action doWhenSuccess)
		{
			Program.instance.room.duelEnded = false;
			if (TcpHelper.tcpClient != null && TcpHelper.tcpClient.Connected)
			{
				TcpHelper.onDisConnected = true;
				return false;
			}
			bool flag;
			try
			{
				Debug.LogFormat("Try Address: {0}, Port: {1}, Password: {2}", new object[] { ipString, portString, pswString });
				TcpHelper.tcpClient = new TcpClientWithTimeout(ipString, int.Parse(portString), 3000).Connect();
				TcpHelper.networkStream = TcpHelper.tcpClient.GetStream();
				new Thread(new ThreadStart(TcpHelper.Receiver)).Start();
				TcpHelper.messageQueue.Clear();
				TcpHelper.InitializeSender();
				TcpHelper.CtosMessage_ExternalAddress(ipString);
				TcpHelper.CtosMessage_PlayerInfo(name);
				TcpHelper.CtosMessage_JoinGame(pswString);
				TcpHelper.joinedAddress = ipString;
				TcpHelper.joinedPort = portString;
				TcpHelper.joinedPassword = pswString;
				OcgCore.mycardDuel = TcpHelper.joinedAddress == "tiramisu.moenext.com";
				if (doWhenSuccess != null)
				{
					doWhenSuccess();
				}
				Debug.LogFormat("Joind Address: {0}, Port: {1}, Password: {2}", new object[]
				{
					TcpHelper.joinedAddress,
					TcpHelper.joinedPort,
					TcpHelper.joinedPassword
				});
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600894E RID: 35150 RVA: 0x001095E0 File Offset: 0x001077E0
		public static void InitializeSender()
		{
			try
			{
				Thread thread = TcpHelper.senderThread;
				if (thread != null)
				{
					thread.Abort();
				}
				TcpHelper.senderThread = new Thread(new ThreadStart(TcpHelper.Sender))
				{
					IsBackground = true
				};
				TcpHelper.senderThread.Start();
			}
			catch
			{
				if (Program.instance.solo.showing)
				{
					MessageManager.messageFromSubString = InterString.Get("端口被占用， 请尝试修改端口后再尝试。端口号应大于0，小于65535。", 0);
				}
			}
		}

		// Token: 0x0600894F RID: 35151 RVA: 0x0010965C File Offset: 0x0010785C
		public static void Receiver()
		{
			try
			{
				while (TcpHelper.tcpClient != null && TcpHelper.networkStream != null && TcpHelper.tcpClient.Connected && Program.Running && !Program.instance.room.duelEnded)
				{
					TcpHelper.AddDateJumoLine(SocketMaster.ReadPacket(TcpHelper.networkStream));
				}
				TcpHelper.onDisConnected = true;
			}
			catch
			{
				TcpHelper.onDisConnected = true;
			}
		}

		// Token: 0x06008950 RID: 35152 RVA: 0x001096D0 File Offset: 0x001078D0
		public static void AddDateJumoLine(byte[] data)
		{
			Monitor.Enter(TcpHelper.datas);
			try
			{
				TcpHelper.datas.Add(data);
			}
			catch (Exception ex)
			{
				Debug.Log(ex);
			}
			Monitor.Exit(TcpHelper.datas);
		}

		// Token: 0x06008951 RID: 35153 RVA: 0x00109718 File Offset: 0x00107918
		public static void PerFrameFunction()
		{
			if (TcpHelper.datas.Count > 0 && Monitor.TryEnter(TcpHelper.datas))
			{
				for (int i = 0; i < TcpHelper.datas.Count; i++)
				{
					try
					{
						BinaryReader r = new BinaryReader(new MemoryStream(TcpHelper.datas[i]));
						switch (r.ReadByte())
						{
						case 1:
							Program.instance.room.StocMessage_GameMsg(r);
							break;
						case 2:
							Program.instance.room.StocMessage_ErrorMsg(r);
							break;
						case 3:
							if (!RoomServant.FromHandTest)
							{
								Program.instance.room.StocMessage_SelectHand(r);
							}
							break;
						case 4:
							if (!RoomServant.FromHandTest)
							{
								Program.instance.room.StocMessage_SelectTp(r);
							}
							break;
						case 5:
							Program.instance.room.StocMessage_HandResult(r);
							break;
						case 6:
							Program.instance.room.StocMessage_TpResult(r);
							break;
						case 7:
							Program.instance.room.StocMessage_ChangeSide(r);
							break;
						case 8:
							Program.instance.room.StocMessage_WaitingSide(r);
							break;
						case 9:
							Program.instance.room.StocMessage_DeckCount(r);
							break;
						case 17:
							Program.instance.room.StocMessage_CreateGame(r);
							break;
						case 18:
							Program.instance.room.StocMessage_JoinGame(r);
							break;
						case 19:
							Program.instance.room.StocMessage_TypeChange(r);
							break;
						case 20:
							Program.instance.room.StocMessage_LeaveGame(r);
							break;
						case 21:
							Program.instance.room.StocMessage_DuelStart(r);
							break;
						case 22:
							Program.instance.room.StocMessage_DuelEnd(r);
							break;
						case 23:
							Program.instance.room.StocMessage_Replay(r);
							break;
						case 24:
							Program.instance.ocgcore.StocMessage_TimeLimit(r);
							break;
						case 25:
							Program.instance.room.StocMessage_Chat(r);
							break;
						case 32:
							Program.instance.room.StocMessage_HsPlayerEnter(r);
							break;
						case 33:
							Program.instance.room.StocMessage_HsPlayerChange(r);
							break;
						case 34:
							Program.instance.room.StocMessage_HsWatchChange(r);
							break;
						case 35:
							Program.instance.ocgcore.StocMessage_TeammateSurrender();
							break;
						}
					}
					catch
					{
					}
				}
				TcpHelper.datas.Clear();
				Monitor.Exit(TcpHelper.datas);
			}
			if (TcpHelper.onDisConnected)
			{
				if (TcpHelper.tcpClient != null && TcpHelper.tcpClient.Connected)
				{
					try
					{
						TcpHelper.tcpClient.Client.Shutdown(SocketShutdown.Receive);
						TcpHelper.tcpClient.Close();
					}
					catch
					{
					}
				}
				TcpHelper.onDisConnected = false;
				TcpHelper.tcpClient = null;
				TcpHelper.canJoin = true;
				if (Program.instance.ocgcore.showing)
				{
					Program.instance.room.duelEnded = true;
					Program.instance.ocgcore.ForceMSquit();
					MessageManager.Cast(InterString.Get("对方已离开游戏，您现在可以离开。", 0));
					return;
				}
				if (Program.instance.deckEditor.showing)
				{
					MessageManager.Cast(InterString.Get("对方已离开游戏，您现在可以离开。", 0));
					Program.instance.ShiftToServant(Program.instance.online);
					return;
				}
				if (!Program.instance.online.showing && !Program.instance.solo.showing)
				{
					MessageManager.Cast(InterString.Get("连接被断开。", 0));
					if (Program.instance.room.showing)
					{
						Program.instance.room.OnExit();
					}
				}
			}
		}

		// Token: 0x06008952 RID: 35154 RVA: 0x00109B44 File Offset: 0x00107D44
		public static void Send(Package message)
		{
			object obj = TcpHelper.locker;
			lock (obj)
			{
				TcpHelper.messageQueue.Enqueue(message);
			}
		}

		// Token: 0x06008953 RID: 35155 RVA: 0x00109B88 File Offset: 0x00107D88
		private static void Sender()
		{
			while (TcpHelper.tcpClient != null && TcpHelper.tcpClient.Connected)
			{
				TcpHelper.senderThread.Join(100);
				object obj = TcpHelper.locker;
				Package currentMessage;
				lock (obj)
				{
					if (TcpHelper.messageQueue.Count == 0)
					{
						continue;
					}
					currentMessage = TcpHelper.messageQueue.Dequeue();
				}
				try
				{
					byte[] data = currentMessage.Data.Get();
					using (MemoryStream memstream = new MemoryStream())
					{
						using (BinaryWriter b = new BinaryWriter(memstream))
						{
							b.Write(BitConverter.GetBytes((short)(data.Length + 1)), 0, 2);
							b.Write(BitConverter.GetBytes((short)((byte)currentMessage.Function)), 0, 1);
							b.Write(data, 0, data.Length);
						}
						byte[] s = memstream.ToArray();
						try
						{
							TcpHelper.tcpClient.Client.Send(s);
						}
						catch (SocketException ex)
						{
							Debug.LogError("Failed to send data: " + ex.Message);
							TcpHelper.onDisConnected = true;
							break;
						}
					}
				}
				catch
				{
					TcpHelper.onDisConnected = true;
					break;
				}
			}
		}

		// Token: 0x06008954 RID: 35156 RVA: 0x00109CEC File Offset: 0x00107EEC
		public static void CtosMessage_Response(byte[] response)
		{
			Package package = new Package();
			package.Function = 1;
			package.Data.writer.Write(response);
			TcpHelper.Send(package);
		}

		// Token: 0x06008955 RID: 35157 RVA: 0x00109D10 File Offset: 0x00107F10
		public static void CtosMessage_UpdateDeck(Deck deckFor)
		{
			if (deckFor.Main.Count == 0)
			{
				return;
			}
			TcpHelper.deckStrings.Clear();
			TcpHelper.deck = deckFor;
			Package message = new Package();
			message.Function = 2;
			message.Data.writer.Write(deckFor.Main.Count + deckFor.Extra.Count);
			message.Data.writer.Write(deckFor.Side.Count);
			for (int i = 0; i < deckFor.Main.Count; i++)
			{
				message.Data.writer.Write(deckFor.Main[i]);
				Card c = CardsManager.Get(deckFor.Main[i], false);
				TcpHelper.deckStrings.Add(c.Name);
			}
			for (int j = 0; j < deckFor.Extra.Count; j++)
			{
				message.Data.writer.Write(deckFor.Extra[j]);
			}
			for (int k = 0; k < deckFor.Side.Count; k++)
			{
				message.Data.writer.Write(deckFor.Side[k]);
			}
			TcpHelper.Send(message);
		}

		// Token: 0x06008956 RID: 35158 RVA: 0x00109E4E File Offset: 0x0010804E
		public static void CtosMessage_HandResult(int res)
		{
			Package package = new Package();
			package.Function = 3;
			package.Data.writer.Write((byte)res);
			TcpHelper.Send(package);
		}

		// Token: 0x06008957 RID: 35159 RVA: 0x00109E74 File Offset: 0x00108074
		public static void CtosMessage_TpResult(bool tp)
		{
			Package message = new Package();
			message.Function = 4;
			if (tp)
			{
				message.Data.writer.Write(1);
			}
			else
			{
				message.Data.writer.Write(0);
			}
			TcpHelper.Send(message);
		}

		// Token: 0x06008958 RID: 35160 RVA: 0x00109EBB File Offset: 0x001080BB
		public static void CtosMessage_ExternalAddress(string hostname)
		{
			Package package = new Package();
			package.Function = 23;
			package.Data.writer.Write(0U);
			package.Data.writer.WriteUnicode(hostname, hostname.Length + 1);
			TcpHelper.Send(package);
		}

		// Token: 0x06008959 RID: 35161 RVA: 0x00109EF9 File Offset: 0x001080F9
		public static void CtosMessage_PlayerInfo(string name)
		{
			Package package = new Package();
			package.Function = 16;
			package.Data.writer.WriteUnicode(name, 20);
			TcpHelper.Send(package);
		}

		// Token: 0x0600895A RID: 35162 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CtosMessage_CreateGame()
		{
		}

		// Token: 0x0600895B RID: 35163 RVA: 0x00109F20 File Offset: 0x00108120
		public static void CtosMessage_JoinGame(string psw)
		{
			TcpHelper.deckStrings.Clear();
			Package package = new Package();
			package.Function = 18;
			package.Data.writer.Write((short)Config.ClientVersion);
			package.Data.writer.Write(204);
			package.Data.writer.Write(204);
			package.Data.writer.Write(0);
			package.Data.writer.WriteUnicode(psw, 20);
			TcpHelper.Send(package);
		}

		// Token: 0x0600895C RID: 35164 RVA: 0x00109FAD File Offset: 0x001081AD
		public static void CtosMessage_LeaveGame()
		{
			TcpHelper.Send(new Package
			{
				Function = 19
			});
		}

		// Token: 0x0600895D RID: 35165 RVA: 0x00109FC1 File Offset: 0x001081C1
		public static void CtosMessage_Surrender()
		{
			TcpHelper.Send(new Package
			{
				Function = 20
			});
		}

		// Token: 0x0600895E RID: 35166 RVA: 0x00109FD5 File Offset: 0x001081D5
		public static void CtosMessage_TimeConfirm()
		{
			TcpHelper.Send(new Package
			{
				Function = 21
			});
		}

		// Token: 0x0600895F RID: 35167 RVA: 0x00109FE9 File Offset: 0x001081E9
		public static void CtosMessage_Chat(string str)
		{
			Package package = new Package();
			package.Function = 22;
			package.Data.writer.WriteUnicode(str, str.Length + 1);
			TcpHelper.Send(package);
		}

		// Token: 0x06008960 RID: 35168 RVA: 0x0010A016 File Offset: 0x00108216
		public static void CtosMessage_HsToDuelist()
		{
			TcpHelper.Send(new Package
			{
				Function = 32
			});
		}

		// Token: 0x06008961 RID: 35169 RVA: 0x0010A02A File Offset: 0x0010822A
		public static void CtosMessage_HsToObserver()
		{
			TcpHelper.Send(new Package
			{
				Function = 33
			});
		}

		// Token: 0x06008962 RID: 35170 RVA: 0x0010A03E File Offset: 0x0010823E
		public static void CtosMessage_HsReady()
		{
			TcpHelper.Send(new Package
			{
				Function = 34
			});
		}

		// Token: 0x06008963 RID: 35171 RVA: 0x0010A052 File Offset: 0x00108252
		public static void CtosMessage_HsNotReady()
		{
			TcpHelper.Send(new Package
			{
				Function = 35
			});
		}

		// Token: 0x06008964 RID: 35172 RVA: 0x0010A066 File Offset: 0x00108266
		public static void CtosMessage_HsKick(int pos)
		{
			Package package = new Package();
			package.Function = 36;
			package.Data.writer.Write((byte)pos);
			TcpHelper.Send(package);
		}

		// Token: 0x06008965 RID: 35173 RVA: 0x0010A08C File Offset: 0x0010828C
		public static void CtosMessage_HsStart()
		{
			TcpHelper.Send(new Package
			{
				Function = 37
			});
		}

		// Token: 0x06008966 RID: 35174 RVA: 0x0010A0A0 File Offset: 0x001082A0
		public static List<Package> ReadPackagesInRecord(string path)
		{
			List<Package> re = null;
			try
			{
				re = TcpHelper.GetPackages(File.ReadAllBytes(path));
			}
			catch (Exception ex)
			{
				re = new List<Package>();
				Debug.Log(ex);
			}
			return re;
		}

		// Token: 0x06008967 RID: 35175 RVA: 0x0010A0DC File Offset: 0x001082DC
		public static List<Package> GetPackages(byte[] buffer)
		{
			List<Package> re = new List<Package>();
			try
			{
				BinaryReader binaryReader;
				BinaryReader reader = (binaryReader = new BinaryReader(new MemoryStream(buffer)));
				try
				{
					while (reader.BaseStream.Position < reader.BaseStream.Length)
					{
						re.Add(new Package
						{
							Function = (int)reader.ReadByte(),
							Data = new BinaryMaster(reader.ReadBytes((int)reader.ReadUInt32()))
						});
					}
				}
				finally
				{
					if (binaryReader != null)
					{
						((IDisposable)binaryReader).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Log(ex);
			}
			return re;
		}

		// Token: 0x06008968 RID: 35176 RVA: 0x0010A17C File Offset: 0x0010837C
		public static void SaveRecord(string replayName)
		{
			try
			{
				if (TcpHelper.packagesInRecord.Count > 10)
				{
					bool write = false;
					int i = 0;
					int startI = 0;
					foreach (Package item in TcpHelper.packagesInRecord)
					{
						i++;
						try
						{
							if (item.Function == 4)
							{
								write = true;
								startI = i;
							}
							if (item.Function == 162)
							{
								write = true;
								startI = i;
							}
						}
						catch (Exception ex)
						{
							Debug.Log(ex);
						}
					}
					if (write)
					{
						if (startI > TcpHelper.packagesInRecord.Count)
						{
							startI = TcpHelper.packagesInRecord.Count;
						}
						TcpHelper.packagesInRecord.Insert(startI, Program.instance.ocgcore.GetNamePacket());
						if (File.Exists("Replay/" + replayName + ".yrp3d"))
						{
							File.Delete("Replay/" + replayName + ".yrp3d");
						}
						FileStream stream = File.Create("Replay/" + replayName + ".yrp3d");
						BinaryWriter writer = new BinaryWriter(stream);
						int j = 0;
						for (int k = startI - 1; k < TcpHelper.packagesInRecord.Count; k++)
						{
							j++;
							writer.Write((byte)TcpHelper.packagesInRecord[k].Function);
							writer.Write((uint)TcpHelper.packagesInRecord[k].Data.GetLength());
							writer.Write(TcpHelper.packagesInRecord[k].Data.Get());
						}
						stream.Flush();
						writer.Close();
						stream.Close();
						if (OcgCore.duelEnded)
						{
							TcpHelper.packagesInRecord.Clear();
						}
					}
				}
			}
			catch (Exception ex2)
			{
				Debug.Log(ex2);
			}
		}

		// Token: 0x06008969 RID: 35177 RVA: 0x0010A374 File Offset: 0x00108574
		public static void AddRecordLine(Package p)
		{
			if (OcgCore.condition != OcgCore.Condition.Replay)
			{
				TcpHelper.packagesInRecord.Add(p);
			}
		}

		// Token: 0x0600896A RID: 35178 RVA: 0x0010A38C File Offset: 0x0010858C
		public static bool IsPortAvailable(int port)
		{
			if (port < 0 || port > 65535)
			{
				throw new ArgumentException(string.Format("指定的端口号 {0} 超出有效范围。", port));
			}
			bool? flag = TcpHelper.IsPortOccupiedBySystem(port);
			bool flag2 = true;
			return !((flag.GetValueOrDefault() == flag2) & (flag != null)) && TcpHelper.TryBindSocket(port);
		}

		// Token: 0x0600896B RID: 35179 RVA: 0x0010A3E0 File Offset: 0x001085E0
		private static bool? IsPortOccupiedBySystem(int port)
		{
			bool? flag;
			try
			{
				flag = new bool?(IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Any((IPEndPoint endpoint) => endpoint.Port == port));
			}
			catch (Exception)
			{
				flag = null;
			}
			return flag;
		}

		// Token: 0x0600896C RID: 35180 RVA: 0x0010A43C File Offset: 0x0010863C
		private static bool TryBindSocket(int port)
		{
			bool flag;
			try
			{
				using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
				{
					socket.Bind(new IPEndPoint(IPAddress.Loopback, port));
					flag = true;
				}
			}
			catch (SocketException)
			{
				flag = false;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0400C468 RID: 50280
		public static TcpClient tcpClient;

		// Token: 0x0400C469 RID: 50281
		private static NetworkStream networkStream;

		// Token: 0x0400C46A RID: 50282
		public static bool canJoin = true;

		// Token: 0x0400C46B RID: 50283
		public static bool onDisConnected;

		// Token: 0x0400C46C RID: 50284
		private static readonly List<byte[]> datas = new List<byte[]>();

		// Token: 0x0400C46D RID: 50285
		private static readonly object locker = new object();

		// Token: 0x0400C46E RID: 50286
		public static Deck deck;

		// Token: 0x0400C46F RID: 50287
		public static List<string> deckStrings = new List<string>();

		// Token: 0x0400C470 RID: 50288
		public static string lastRecordName = "";

		// Token: 0x0400C471 RID: 50289
		public static List<Package> packagesInRecord = new List<Package>();

		// Token: 0x0400C472 RID: 50290
		private static readonly Queue<Package> messageQueue = new Queue<Package>();

		// Token: 0x0400C473 RID: 50291
		private static Thread senderThread;

		// Token: 0x0400C474 RID: 50292
		private static Thread linkThread;

		// Token: 0x0400C475 RID: 50293
		public static string joinedAddress;

		// Token: 0x0400C476 RID: 50294
		public static string joinedPort;

		// Token: 0x0400C477 RID: 50295
		public static string joinedPassword;
	}
}
