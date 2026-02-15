using System;
using System.IO;
using System.Runtime.InteropServices;
using Ionic.Zip;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using Percy;

namespace MDPro3
{
	// Token: 0x02001217 RID: 4631
	public class PercyOCG
	{
		// Token: 0x06008941 RID: 35137 RVA: 0x00109090 File Offset: 0x00107290
		public PercyOCG()
		{
			PercyOCG._buffer = Marshal.AllocHGlobal(262144);
			PercyOCG.error = InterString.Get("YGOPro旧版的回放崩溃了！您可以选择使用永不崩溃的新版回放。", 0);
			this.ygopro = new Ygopro(new Action<byte[]>(this.ReceiveHandler), new Ygopro.CardHandler(this.CardHandler), new Ygopro.ScriptHandler(this.ScriptHandler), new Ygopro.ChatHandler(this.ChatHandler));
		}

		// Token: 0x06008942 RID: 35138 RVA: 0x00109100 File Offset: 0x00107300
		private CardData CardHandler(long code)
		{
			Card card = CardsManager.Get((int)code, false);
			CardData returnValue = new CardData
			{
				Code = card.Id,
				Alias = card.Alias,
				Attack = card.Attack,
				Attribute = card.Attribute,
				Defense = card.Defense,
				Level = card.Level,
				LScale = card.LScale,
				Race = card.Race,
				RScale = card.RScale,
				Type = card.Type,
				LinkMarker = card.LinkMarker
			};
			returnValue.ConvertLongToSetCode(card.Setcode);
			return returnValue;
		}

		// Token: 0x06008943 RID: 35139 RVA: 0x001091C0 File Offset: 0x001073C0
		private ScriptData ScriptHandler(string fileName)
		{
			ScriptData ret;
			ret.buffer = IntPtr.Zero;
			ret.len = 0;
			string fileName2 = fileName.TrimStart(new char[] { '.', '/' });
			if (fileName.StartsWith("Puzzle/") || fileName.StartsWith("TempFolder/"))
			{
				if (File.Exists(fileName))
				{
					byte[] content = File.ReadAllBytes(fileName);
					Marshal.Copy(content, 0, PercyOCG._buffer, content.Length);
					ret.buffer = PercyOCG._buffer;
					ret.len = content.Length;
				}
			}
			else
			{
				foreach (ZipFile zip in ZipHelper.zips)
				{
					if (zip.ContainsEntry(fileName2))
					{
						MemoryStream ms = new MemoryStream();
						zip[fileName2].Extract(ms);
						byte[] content = ms.ToArray();
						byte[] subcontent = new byte[30];
						for (int i = 0; i < 30; i++)
						{
							subcontent[i] = content[i];
						}
						Marshal.Copy(content, 0, PercyOCG._buffer, content.Length);
						ret.buffer = PercyOCG._buffer;
						ret.len = content.Length;
						break;
					}
				}
			}
			return ret;
		}

		// Token: 0x06008944 RID: 35140 RVA: 0x00109304 File Offset: 0x00107504
		private void ChatHandler(string result)
		{
			Program.instance.ocgcore.StocMessage_Error(result);
		}

		// Token: 0x06008945 RID: 35141 RVA: 0x00109318 File Offset: 0x00107518
		private void ReceiveHandler(byte[] buffer)
		{
			byte[] bufferR = new byte[buffer.Length + 1];
			bufferR[0] = 1;
			buffer.CopyTo(bufferR, 1);
			TcpHelper.AddDateJumoLine(bufferR);
		}

		// Token: 0x06008946 RID: 35142 RVA: 0x00109342 File Offset: 0x00107542
		public void Dispose()
		{
			this.ygopro.Dispose();
		}

		// Token: 0x06008947 RID: 35143 RVA: 0x0010934F File Offset: 0x0010754F
		public void Response(byte[] resp)
		{
			this.ygopro.Response(resp);
		}

		// Token: 0x06008948 RID: 35144 RVA: 0x00109360 File Offset: 0x00107560
		public void StartPuzzle(string path)
		{
			if (!this.ygopro.StartPuzzle(path))
			{
				MessageManager.Cast(InterString.Get("启动残局<#FF0000>[?]</color>失败。", path, 0));
				return;
			}
			Config.SetBool(path.Substring(0, path.Length - 4) + "_Enter", true);
			Config.Save();
			OcgCore.condition = OcgCore.Condition.Duel;
			OcgCore.isFirst = true;
			Program.instance.ocgcore.returnServant = (DeckEditor.ToHandTest ? Program.instance.deckEditor : Program.instance.puzzle);
			OcgCore.timeLimit = 0;
			OcgCore.inPuzzle = true;
			Program.instance.ShiftToServant(Program.instance.ocgcore);
			OcgCore.handler = new OcgCore.ResponseHandler(this.Response);
		}

		// Token: 0x06008949 RID: 35145 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartAI()
		{
		}

		// Token: 0x0600894A RID: 35146 RVA: 0x0000216D File Offset: 0x0000036D
		public void StartDuel()
		{
		}

		// Token: 0x0400C463 RID: 50275
		public static string HintInGame = Ygopro.HintInGame;

		// Token: 0x0400C464 RID: 50276
		public static bool godMode;

		// Token: 0x0400C465 RID: 50277
		private static string error = "Error occurred.";

		// Token: 0x0400C466 RID: 50278
		private static IntPtr _buffer;

		// Token: 0x0400C467 RID: 50279
		public Ygopro ygopro;
	}
}
