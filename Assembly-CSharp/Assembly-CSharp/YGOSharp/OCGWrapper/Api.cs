using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace YGOSharp.OCGWrapper
{
	// Token: 0x020001C3 RID: 451
	public static class Api
	{
		// Token: 0x060007BF RID: 1983
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void set_card_reader(Api.CardReader f);

		// Token: 0x060007C0 RID: 1984
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void set_message_handler(Api.MessageHandler f);

		// Token: 0x060007C1 RID: 1985
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void set_script_reader(Api.ScriptReader f);

		// Token: 0x060007C2 RID: 1986
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr create_duel(uint seed);

		// Token: 0x060007C3 RID: 1987
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void start_duel(IntPtr pduel, int options);

		// Token: 0x060007C4 RID: 1988
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void end_duel(IntPtr pduel);

		// Token: 0x060007C5 RID: 1989
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void set_player_info(IntPtr pduel, int playerid, int lp, int startcount, int drawcount);

		// Token: 0x060007C6 RID: 1990
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void new_card(IntPtr pduel, uint code, byte owner, byte playerid, byte location, byte sequence, byte position);

		// Token: 0x060007C7 RID: 1991
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void new_tag_card(IntPtr pduel, uint code, byte owner, byte location);

		// Token: 0x060007C8 RID: 1992
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int process(IntPtr pduel);

		// Token: 0x060007C9 RID: 1993
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int get_message(IntPtr pduel, IntPtr buf);

		// Token: 0x060007CA RID: 1994
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void get_log_message(IntPtr pduel, IntPtr buf);

		// Token: 0x060007CB RID: 1995
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void set_responseb(IntPtr pduel, IntPtr buf);

		// Token: 0x060007CC RID: 1996
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void set_responsei(IntPtr pduel, uint value);

		// Token: 0x060007CD RID: 1997
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int query_card(IntPtr pduel, byte playerid, byte location, byte sequence, int queryFlag, IntPtr buf, int useCache);

		// Token: 0x060007CE RID: 1998
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int query_field_count(IntPtr pduel, byte playerid, byte location);

		// Token: 0x060007CF RID: 1999
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int query_field_card(IntPtr pduel, byte playerid, byte location, int queryFlag, IntPtr buf, int useCache);

		// Token: 0x060007D0 RID: 2000
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int query_field_info(IntPtr pduel, IntPtr buf);

		// Token: 0x060007D1 RID: 2001 RVA: 0x000257DC File Offset: 0x000239DC
		public static void Init(string rootPath = ".", string scriptDirectory = "script", string databaseFile = "cards.cdb")
		{
			Api._rootPath = rootPath;
			Api._scriptDirectory = scriptDirectory;
			CardsManager.Init(Path.Combine(Path.GetFullPath(rootPath), databaseFile));
			Duel.Duels = new Dictionary<IntPtr, Duel>();
			Api._buffer = Marshal.AllocHGlobal(131072);
			Api._cardCallback = new Api.CardReader(Api.OnCardReader);
			Api._scriptCallback = new Api.ScriptReader(Api.OnScriptReader);
			Api._messageCallback = new Api.MessageHandler(Api.OnMessageHandler);
			Api.set_card_reader(Api._cardCallback);
			Api.set_script_reader(Api._scriptCallback);
			Api.set_message_handler(Api._messageCallback);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00025870 File Offset: 0x00023A70
		public static void Dispose()
		{
			foreach (Duel duel in Duel.Duels.Values)
			{
				duel.Dispose();
			}
			Marshal.FreeHGlobal(Api._buffer);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000258C8 File Offset: 0x00023AC8
		private unsafe static uint OnCardReader(uint code, Card.CardData* pData)
		{
			Card card = CardsManager.GetCard((int)code);
			if (card != null)
			{
				*pData = card.Data;
			}
			return code;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x000258EC File Offset: 0x00023AEC
		private unsafe static IntPtr OnScriptReader(string scriptName, int* len)
		{
			string filename = Api.GetScriptFilename(scriptName);
			if (!File.Exists(filename))
			{
				return IntPtr.Zero;
			}
			byte[] content = File.ReadAllBytes(filename);
			*len = content.Length;
			Marshal.Copy(content, 0, Api._buffer, content.Length);
			return Api._buffer;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002592E File Offset: 0x00023B2E
		private static uint OnMessageHandler(IntPtr pDuel, uint messageType)
		{
			if (Duel.Duels.ContainsKey(pDuel))
			{
				Duel.Duels[pDuel].OnMessage(messageType);
			}
			return 0U;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002594F File Offset: 0x00023B4F
		private static string GetScriptFilename(string scriptName)
		{
			return Path.Combine(Api._rootPath, scriptName.Replace("./script", Api._scriptDirectory));
		}

		// Token: 0x04000BA1 RID: 2977
		private static string _rootPath;

		// Token: 0x04000BA2 RID: 2978
		private static string _scriptDirectory;

		// Token: 0x04000BA3 RID: 2979
		private static IntPtr _buffer;

		// Token: 0x04000BA4 RID: 2980
		private static Api.ScriptReader _scriptCallback;

		// Token: 0x04000BA5 RID: 2981
		private static Api.CardReader _cardCallback;

		// Token: 0x04000BA6 RID: 2982
		private static Api.MessageHandler _messageCallback;

		// Token: 0x020001C4 RID: 452
		// (Invoke) Token: 0x060007D8 RID: 2008
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate IntPtr ScriptReader(string scriptName, int* len);

		// Token: 0x020001C5 RID: 453
		// (Invoke) Token: 0x060007DC RID: 2012
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate uint CardReader(uint code, Card.CardData* pData);

		// Token: 0x020001C6 RID: 454
		// (Invoke) Token: 0x060007E0 RID: 2016
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint MessageHandler(IntPtr pDuel, uint messageType);
	}
}
