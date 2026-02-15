using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Percy
{
	// Token: 0x020011E2 RID: 4578
	internal static class Dll
	{
		// Token: 0x060087D9 RID: 34777
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern void set_card_reader(Dll.CardReader f);

		// Token: 0x060087DA RID: 34778
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern void set_message_handler(Dll.MessageHandler f);

		// Token: 0x060087DB RID: 34779
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		private static extern void set_script_reader(Dll.ScriptReader f);

		// Token: 0x060087DC RID: 34780
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr create_duel(uint seed);

		// Token: 0x060087DD RID: 34781
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern IntPtr create_duel_v2([In] uint[] seeds);

		// Token: 0x060087DE RID: 34782
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void start_duel(IntPtr pduel, uint options);

		// Token: 0x060087DF RID: 34783
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int get_ai_going_first_second(IntPtr pduel, IntPtr deckname);

		// Token: 0x060087E0 RID: 34784
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int set_player_going_first_second(IntPtr pduel, int first, IntPtr deckname);

		// Token: 0x060087E1 RID: 34785
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void set_ai_id(IntPtr pduel, int playerid);

		// Token: 0x060087E2 RID: 34786
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void end_duel(IntPtr pduel);

		// Token: 0x060087E3 RID: 34787
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void set_player_info(IntPtr pduel, int playerid, int lp, int startcount, int drawcount);

		// Token: 0x060087E4 RID: 34788
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void new_card(IntPtr pduel, uint code, byte owner, byte playerid, byte location, byte sequence, byte position);

		// Token: 0x060087E5 RID: 34789
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void new_tag_card(IntPtr pduel, uint code, byte owner, byte location);

		// Token: 0x060087E6 RID: 34790
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int process(IntPtr pduel);

		// Token: 0x060087E7 RID: 34791
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int get_message(IntPtr pduel, IntPtr buf);

		// Token: 0x060087E8 RID: 34792
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void get_log_message(IntPtr pduel, byte[] buf);

		// Token: 0x060087E9 RID: 34793
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void set_responseb(IntPtr pduel, IntPtr buf);

		// Token: 0x060087EA RID: 34794
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern void set_responsei(IntPtr pduel, uint value);

		// Token: 0x060087EB RID: 34795
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int query_card(IntPtr pduel, byte playerid, byte location, byte sequence, int queryFlag, IntPtr buf, int useCache);

		// Token: 0x060087EC RID: 34796
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int query_field_count(IntPtr pduel, byte playerid, byte location);

		// Token: 0x060087ED RID: 34797
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int query_field_card(IntPtr pduel, byte playerid, byte location, int queryFlag, IntPtr buf, int useCache);

		// Token: 0x060087EE RID: 34798
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int query_field_info(IntPtr pduel, IntPtr buf);

		// Token: 0x060087EF RID: 34799
		[DllImport("ocgcore", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public static extern int preload_script(IntPtr pduel, IntPtr script);

		// Token: 0x060087F0 RID: 34800 RVA: 0x000F785E File Offset: 0x000F5A5E
		public static void Set_card_api(Ygopro.CardHandler h)
		{
			Dll.card_handler = h;
			Dll.set_card_reader(new Dll.CardReader(Dll.OnCardReader));
		}

		// Token: 0x060087F1 RID: 34801 RVA: 0x000F7877 File Offset: 0x000F5A77
		public static void Set_script_api(Ygopro.ScriptHandler h)
		{
			Dll.script_handler = h;
			Dll.set_script_reader(new Dll.ScriptReader(Dll.OnScriptHandler));
		}

		// Token: 0x060087F2 RID: 34802 RVA: 0x000F7890 File Offset: 0x000F5A90
		public static void Set_chat_api(Ygopro.ChatHandler h)
		{
			Dll.chat_handler = h;
			Dll.set_message_handler(new Dll.MessageHandler(Dll.OnMessageHandler));
		}

		// Token: 0x060087F3 RID: 34803 RVA: 0x000F78AC File Offset: 0x000F5AAC
		[MonoPInvokeCallback]
		private unsafe static IntPtr OnScriptHandler(string scriptName, int* len)
		{
			ScriptData ret = Dll.script_handler(scriptName);
			*len = ret.len;
			return ret.buffer;
		}

		// Token: 0x060087F4 RID: 34804 RVA: 0x000F78D3 File Offset: 0x000F5AD3
		[MonoPInvokeCallback]
		private unsafe static uint OnCardReader(uint code, CardData* pData)
		{
			*pData = Dll.card_handler((long)((ulong)code));
			return code;
		}

		// Token: 0x060087F5 RID: 34805 RVA: 0x000F78E8 File Offset: 0x000F5AE8
		[MonoPInvokeCallback]
		private static uint OnMessageHandler(IntPtr pDuel, uint messageType)
		{
			Dll.get_log_message(pDuel, Dll._buffer_2);
			string message = Encoding.UTF8.GetString(Dll._buffer_2).TrimEnd('\0');
			Dll.chat_handler(message);
			return 0U;
		}

		// Token: 0x0400C2EE RID: 49902
		private static Ygopro.CardHandler card_handler;

		// Token: 0x0400C2EF RID: 49903
		private static Ygopro.ScriptHandler script_handler;

		// Token: 0x0400C2F0 RID: 49904
		private static Ygopro.ChatHandler chat_handler;

		// Token: 0x0400C2F1 RID: 49905
		private static readonly byte[] _buffer_2 = new byte[256];

		// Token: 0x020011E3 RID: 4579
		// (Invoke) Token: 0x060087F8 RID: 34808
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private unsafe delegate IntPtr ScriptReader(string scriptName, int* len);

		// Token: 0x020011E4 RID: 4580
		// (Invoke) Token: 0x060087FC RID: 34812
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private unsafe delegate uint CardReader(uint code, CardData* pData);

		// Token: 0x020011E5 RID: 4581
		// (Invoke) Token: 0x06008800 RID: 34816
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate uint MessageHandler(IntPtr pDuel, uint messageType);
	}
}
