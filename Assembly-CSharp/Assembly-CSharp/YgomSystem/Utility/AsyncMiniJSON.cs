using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.Utility
{
	// Token: 0x02000503 RID: 1283
	public class AsyncMiniJSON : MonoBehaviour
	{
		// Token: 0x0600283D RID: 10301 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Deserialize(string json, GameObject owner, Action<object> onfinish)
		{
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsJsonAsyncParse()
		{
			return false;
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0000216D File Offset: 0x0000036D
		public void Parse(string json, Action<object> onfinish)
		{
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool Task_StartParse(TaskManager.ID tThis, int nExecNum, float fExecSec, object tParam)
		{
			return false;
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x040028FE RID: 10494
		private const float EXECUTE_TASK_LIMIT_TIME = 0.03f;

		// Token: 0x040028FF RID: 10495
		private const string DEFAULT_GAMEOBJECT_NAME = "AsyncMiniJSON";

		// Token: 0x04002900 RID: 10496
		private TaskManager jsonTaskManager;

		// Token: 0x02000504 RID: 1284
		private struct ParseParam
		{
			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06002843 RID: 10307 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06002844 RID: 10308 RVA: 0x0000216D File Offset: 0x0000036D
			public string json
			{
				[CompilerGenerated]
				readonly get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06002845 RID: 10309 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06002846 RID: 10310 RVA: 0x0000216D File Offset: 0x0000036D
			public Action<object> onfinish
			{
				[CompilerGenerated]
				readonly get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x06002847 RID: 10311 RVA: 0x0000216D File Offset: 0x0000036D
			public ParseParam(string arg_json, Action<object> arg_onfinish)
			{
			}
		}

		// Token: 0x02000505 RID: 1285
		private sealed class Parser : IDisposable
		{
			// Token: 0x170001ED RID: 493
			// (get) Token: 0x06002848 RID: 10312 RVA: 0x000029CC File Offset: 0x00000BCC
			private char PeekChar
			{
				get
				{
					return '\0';
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x06002849 RID: 10313 RVA: 0x000029CC File Offset: 0x00000BCC
			private char NextChar
			{
				get
				{
					return '\0';
				}
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x0600284A RID: 10314 RVA: 0x0000216A File Offset: 0x0000036A
			private string NextWord
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x0600284B RID: 10315 RVA: 0x000029CC File Offset: 0x00000BCC
			private AsyncMiniJSON.Parser.TOKEN NextToken
			{
				get
				{
					return AsyncMiniJSON.Parser.TOKEN.NONE;
				}
			}

			// Token: 0x0600284C RID: 10316 RVA: 0x000029CC File Offset: 0x00000BCC
			public static bool IsWordBreak(char c)
			{
				return false;
			}

			// Token: 0x0600284D RID: 10317 RVA: 0x00002739 File Offset: 0x00000939
			private Parser(string jsonString)
			{
			}

			// Token: 0x0600284E RID: 10318 RVA: 0x0000216A File Offset: 0x0000036A
			public static AsyncMiniJSON.Parser Parse(string jsonString, Action<object> onfinish)
			{
				return null;
			}

			// Token: 0x0600284F RID: 10319 RVA: 0x0000216D File Offset: 0x0000036D
			public void Update()
			{
			}

			// Token: 0x06002850 RID: 10320 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsDone()
			{
				return false;
			}

			// Token: 0x06002851 RID: 10321 RVA: 0x0000216D File Offset: 0x0000036D
			public void Dispose()
			{
			}

			// Token: 0x06002852 RID: 10322 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool Task_ParseObject(TaskManager.ID tThis, int nExecNum, float fExecSec, object tParam)
			{
				return false;
			}

			// Token: 0x06002853 RID: 10323 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool Task_ParseArray(TaskManager.ID tThis, int nExecNum, float fExecSec, object tParam)
			{
				return false;
			}

			// Token: 0x06002854 RID: 10324 RVA: 0x0000216D File Offset: 0x0000036D
			private void ParseValue(Action<object> onfinish)
			{
			}

			// Token: 0x06002855 RID: 10325 RVA: 0x0000216D File Offset: 0x0000036D
			private void ParseByToken(AsyncMiniJSON.Parser.TOKEN token, Action<object> onfinish)
			{
			}

			// Token: 0x06002856 RID: 10326 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool Task_ParseString(TaskManager.ID tThis, int nExecNum, float fExecSec, object tParam)
			{
				return false;
			}

			// Token: 0x06002857 RID: 10327 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool Task_ParseNumber(TaskManager.ID tThis, int nExecNum, float fExecSec, object tParam)
			{
				return false;
			}

			// Token: 0x06002858 RID: 10328 RVA: 0x0000216A File Offset: 0x0000036A
			private string ParseString()
			{
				return null;
			}

			// Token: 0x06002859 RID: 10329 RVA: 0x0000216A File Offset: 0x0000036A
			private object ParseNumber()
			{
				return null;
			}

			// Token: 0x0600285A RID: 10330 RVA: 0x0000216D File Offset: 0x0000036D
			private void EatWhitespace()
			{
			}

			// Token: 0x04002901 RID: 10497
			private const string WORD_BREAK = "{}[],:\"";

			// Token: 0x04002902 RID: 10498
			private StringReader json;

			// Token: 0x04002903 RID: 10499
			private TaskManager parserTaskManager;

			// Token: 0x02000506 RID: 1286
			private enum TOKEN
			{
				// Token: 0x04002905 RID: 10501
				NONE,
				// Token: 0x04002906 RID: 10502
				CURLY_OPEN,
				// Token: 0x04002907 RID: 10503
				CURLY_CLOSE,
				// Token: 0x04002908 RID: 10504
				SQUARED_OPEN,
				// Token: 0x04002909 RID: 10505
				SQUARED_CLOSE,
				// Token: 0x0400290A RID: 10506
				COLON,
				// Token: 0x0400290B RID: 10507
				COMMA,
				// Token: 0x0400290C RID: 10508
				STRING,
				// Token: 0x0400290D RID: 10509
				NUMBER,
				// Token: 0x0400290E RID: 10510
				TRUE,
				// Token: 0x0400290F RID: 10511
				FALSE,
				// Token: 0x04002910 RID: 10512
				NULL
			}
		}
	}
}
