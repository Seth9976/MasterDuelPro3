using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using YGOSharp.OCGWrapper.Enums;

namespace YGOSharp.OCGWrapper
{
	// Token: 0x020001CA RID: 458
	public class Duel
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x00025D0D File Offset: 0x00023F0D
		public void SetAnalyzer(Func<GameMessage, BinaryReader, byte[], int> analyzer)
		{
			this._analyzer = analyzer;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00025D16 File Offset: 0x00023F16
		public void SetErrorHandler(Action<string> errorHandler)
		{
			this._errorHandler = errorHandler;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00025D1F File Offset: 0x00023F1F
		public void InitPlayers(int startLp, int startHand, int drawCount)
		{
			Api.set_player_info(this._duelPtr, 0, startLp, startHand, drawCount);
			Api.set_player_info(this._duelPtr, 1, startLp, startHand, drawCount);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00025D3F File Offset: 0x00023F3F
		public void AddCard(int cardId, int owner, CardLocation location)
		{
			Api.new_card(this._duelPtr, (uint)cardId, (byte)owner, (byte)owner, (byte)location, 0, 8);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00025D55 File Offset: 0x00023F55
		public void AddTagCard(int cardId, int owner, CardLocation location)
		{
			Api.new_tag_card(this._duelPtr, (uint)cardId, (byte)owner, (byte)location);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00025D67 File Offset: 0x00023F67
		public void Start(int options)
		{
			Api.start_duel(this._duelPtr, options);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00025D78 File Offset: 0x00023F78
		public int Process()
		{
			int fail = 0;
			int result;
			for (;;)
			{
				result = Api.process(this._duelPtr);
				int len = result & 65535;
				if (len > 0)
				{
					fail = 0;
					byte[] arr = new byte[4096];
					int num = Api.get_message(this._duelPtr, this._buffer);
					Marshal.Copy(this._buffer, arr, 0, 4096);
					result = this.HandleMessage(new BinaryReader(new MemoryStream(arr)), arr, len);
					if (result != 0)
					{
						break;
					}
				}
				else if (++fail == 10)
				{
					return -1;
				}
			}
			return result;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00025DF3 File Offset: 0x00023FF3
		public void SetResponse(int resp)
		{
			Api.set_responsei(this._duelPtr, (uint)resp);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00025E04 File Offset: 0x00024004
		public void SetResponse(byte[] resp)
		{
			if (resp.Length > 64)
			{
				return;
			}
			IntPtr buf = Marshal.AllocHGlobal(64);
			Marshal.Copy(resp, 0, buf, resp.Length);
			Api.set_responseb(this._duelPtr, buf);
			Marshal.FreeHGlobal(buf);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00025E3E File Offset: 0x0002403E
		public int QueryFieldCount(int player, CardLocation location)
		{
			return Api.query_field_count(this._duelPtr, (byte)player, (byte)location);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00025E50 File Offset: 0x00024050
		public byte[] QueryFieldCard(int player, CardLocation location, int flag = 16769023, bool useCache = false)
		{
			int len = Api.query_field_card(this._duelPtr, (byte)player, (byte)location, flag, this._buffer, useCache ? 1 : 0);
			byte[] result = new byte[len];
			Marshal.Copy(this._buffer, result, 0, len);
			return result;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00025E94 File Offset: 0x00024094
		public byte[] QueryCard(int player, int location, int sequence, int flag = 16769023, bool useCache = false)
		{
			int len = Api.query_card(this._duelPtr, (byte)player, (byte)location, (byte)sequence, flag, this._buffer, useCache ? 1 : 0);
			byte[] result = new byte[len];
			Marshal.Copy(this._buffer, result, 0, len);
			return result;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00025EDC File Offset: 0x000240DC
		public byte[] QueryFieldInfo()
		{
			Api.query_field_info(this._duelPtr, this._buffer);
			byte[] result = new byte[256];
			Marshal.Copy(this._buffer, result, 0, 256);
			return result;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00025F19 File Offset: 0x00024119
		public void End()
		{
			Api.end_duel(this._duelPtr);
			this.Dispose();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00025F2C File Offset: 0x0002412C
		public IntPtr GetNativePtr()
		{
			return this._duelPtr;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00025F34 File Offset: 0x00024134
		internal Duel(IntPtr duelPtr)
		{
			this._buffer = Marshal.AllocHGlobal(4096);
			this._duelPtr = duelPtr;
			Duel.Duels.Add(this._duelPtr, this);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00025F64 File Offset: 0x00024164
		internal void Dispose()
		{
			Marshal.FreeHGlobal(this._buffer);
			Duel.Duels.Remove(this._duelPtr);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00025F84 File Offset: 0x00024184
		internal void OnMessage(uint messageType)
		{
			byte[] arr = new byte[256];
			Api.get_log_message(this._duelPtr, this._buffer);
			Marshal.Copy(this._buffer, arr, 0, 256);
			string message = Encoding.UTF8.GetString(arr);
			if (message.Contains("\0"))
			{
				message = message.Substring(0, message.IndexOf('\0'));
			}
			if (this._errorHandler != null)
			{
				this._errorHandler(message);
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00025FFC File Offset: 0x000241FC
		private int HandleMessage(BinaryReader reader, byte[] raw, int len)
		{
			while (reader.BaseStream.Position < (long)len)
			{
				GameMessage msg = (GameMessage)reader.ReadByte();
				int result = -1;
				if (this._analyzer != null)
				{
					result = this._analyzer(msg, reader, raw);
				}
				if (result != 0)
				{
					return result;
				}
			}
			return 0;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00026040 File Offset: 0x00024240
		public static Duel Create(uint seed)
		{
			MtRandom mtRandom = new MtRandom();
			mtRandom.Reset(seed);
			return Duel.Create(Api.create_duel(mtRandom.Rand()));
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002605D File Offset: 0x0002425D
		internal static Duel Create(IntPtr pDuel)
		{
			if (pDuel == IntPtr.Zero)
			{
				return null;
			}
			return new Duel(pDuel);
		}

		// Token: 0x04000BC2 RID: 3010
		private readonly IntPtr _duelPtr;

		// Token: 0x04000BC3 RID: 3011
		private readonly IntPtr _buffer;

		// Token: 0x04000BC4 RID: 3012
		private Func<GameMessage, BinaryReader, byte[], int> _analyzer;

		// Token: 0x04000BC5 RID: 3013
		private Action<string> _errorHandler;

		// Token: 0x04000BC6 RID: 3014
		internal static IDictionary<IntPtr, Duel> Duels;
	}
}
