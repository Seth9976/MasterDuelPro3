using System;
using System.Collections.Generic;

namespace YgomSystem.Utility
{
	// Token: 0x02000541 RID: 1345
	public class ScriptManager
	{
		// Token: 0x06002AF5 RID: 10997 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddScript(string lang, Func<object, ScriptManager.IScriptContext> ctxgen)
		{
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetGlobalContext()
		{
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x0000216A File Offset: 0x0000036A
		public static ScriptManager.IScriptContext CreateContext(string lang, object ctx)
		{
			return null;
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x0000216A File Offset: 0x0000036A
		public static string LoadImmediateText(string url)
		{
			return null;
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ExecUrlScript(string url, object ctx)
		{
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x0000216A File Offset: 0x0000036A
		public static ScriptManager.IScriptContext GetGlobalContext(string lang)
		{
			return null;
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ExecLoadScript(string lang, string srcurl, object ctx)
		{
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ExecScript(string lang, string code, object ctx)
		{
		}

		// Token: 0x04002A0B RID: 10763
		private static SortedDictionary<string, Func<object, ScriptManager.IScriptContext>> scriptctxgen;

		// Token: 0x04002A0C RID: 10764
		private static SortedDictionary<string, ScriptManager.IScriptContext> globalctx;

		// Token: 0x02000542 RID: 1346
		public interface IScriptContextOwner
		{
			// Token: 0x06002AFE RID: 11006
			ScriptManager.IScriptContext GetScriptContext(string lang);
		}

		// Token: 0x02000543 RID: 1347
		public interface IScriptContext
		{
			// Token: 0x06002AFF RID: 11007
			void Exec(string code);
		}
	}
}
