using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using UnityEngine;

namespace MDPro3
{
	// Token: 0x0200121D RID: 4637
	public static class BinaryExtensions
	{
		// Token: 0x06008978 RID: 35192 RVA: 0x0010A6A4 File Offset: 0x001088A4
		public static void WriteUnicode(this BinaryWriter writer, string text, int len)
		{
			try
			{
				byte[] unicode = Encoding.Unicode.GetBytes(text);
				byte[] result = new byte[len * 2];
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = 204;
				}
				int max = len * 2 - 2;
				Array.Copy(unicode, result, (unicode.Length > max) ? max : unicode.Length);
				result[unicode.Length] = 0;
				result[unicode.Length + 1] = 0;
				writer.Write(result);
			}
			catch (Exception ex)
			{
				Debug.Log(ex);
			}
		}

		// Token: 0x06008979 RID: 35193 RVA: 0x0010A724 File Offset: 0x00108924
		public static string ReadUnicode(this BinaryReader reader, int len)
		{
			byte[] unicode = reader.ReadBytes(len * 2);
			string text = Encoding.Unicode.GetString(unicode);
			return text.Substring(0, text.IndexOf('\0'));
		}

		// Token: 0x0600897A RID: 35194 RVA: 0x0010A758 File Offset: 0x00108958
		public static string ReadALLUnicode(this BinaryReader reader)
		{
			byte[] unicode = reader.ReadToEnd();
			string text = Encoding.Unicode.GetString(unicode);
			return text.Substring(0, text.IndexOf('\0'));
		}

		// Token: 0x0600897B RID: 35195 RVA: 0x00027A9D File Offset: 0x00025C9D
		public static byte[] ReadToEnd(this BinaryReader reader)
		{
			return reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position));
		}

		// Token: 0x0600897C RID: 35196 RVA: 0x0010A788 File Offset: 0x00108988
		public static GPS ReadGPS(this BinaryReader reader)
		{
			return new GPS
			{
				controller = (uint)OcgCore.LocalPlayer((int)reader.ReadByte()),
				location = (uint)reader.ReadByte(),
				sequence = (uint)reader.ReadByte(),
				position = (int)reader.ReadByte()
			};
		}

		// Token: 0x0600897D RID: 35197 RVA: 0x0010A7C4 File Offset: 0x001089C4
		public static GPS ReadShortGPS(this BinaryReader reader)
		{
			return new GPS
			{
				controller = (uint)OcgCore.LocalPlayer((int)reader.ReadByte()),
				location = (uint)reader.ReadByte(),
				sequence = (uint)reader.ReadByte(),
				position = 1
			};
		}

		// Token: 0x0600897E RID: 35198 RVA: 0x0010A7FC File Offset: 0x001089FC
		public static void ReadCardData(this BinaryReader r, GameCard cardTemp = null)
		{
			GameCard cardToRefresh = cardTemp;
			int flag = r.ReadInt32();
			int code = 0;
			GPS gps = new GPS();
			if ((flag & 1) != 0)
			{
				code = r.ReadInt32();
			}
			if ((flag & 2) != 0)
			{
				gps = r.ReadGPS();
				cardToRefresh = Program.instance.ocgcore.GCS_Get(gps);
			}
			if (cardToRefresh == null)
			{
				return;
			}
			Card data = cardToRefresh.GetData();
			if ((flag & 1) != 0 && data.Id != code)
			{
				data = CardsManager.Get(code, false);
				data.Id = code;
			}
			if ((flag & 2) != 0)
			{
				cardToRefresh.p = gps;
			}
			if (data.Id > 0 && (cardToRefresh.p.location & 2U) > 0U && cardToRefresh.p.controller == 1U)
			{
				cardToRefresh.p.position = 1;
			}
			if ((flag & 4) != 0)
			{
				data.Alias = r.ReadInt32();
			}
			if ((flag & 8) != 0)
			{
				data.Type = r.ReadInt32();
			}
			int l = 0;
			if ((flag & 16) != 0)
			{
				l = r.ReadInt32();
			}
			int l2 = 0;
			if ((flag & 32) != 0)
			{
				l2 = r.ReadInt32();
			}
			if ((flag & 64) != 0)
			{
				data.Attribute = r.ReadInt32();
			}
			if ((flag & 128) != 0)
			{
				data.Race = r.ReadInt32();
			}
			if ((flag & 256) != 0)
			{
				data.Attack = r.ReadInt32();
			}
			if ((flag & 512) != 0)
			{
				data.Defense = r.ReadInt32();
			}
			if ((flag & 1024) != 0)
			{
				data.rAttack = r.ReadInt32();
			}
			if ((flag & 2048) != 0)
			{
				data.rDefense = r.ReadInt32();
			}
			if ((flag & 4096) != 0)
			{
				data.Reason = r.ReadInt32();
			}
			if ((flag & 8192) != 0)
			{
				data.ReasonCard = r.ReadInt32();
			}
			if ((flag & 16384) != 0)
			{
				cardToRefresh.AddTarget(Program.instance.ocgcore.GCS_Get(r.ReadGPS()));
			}
			if ((flag & 32768) != 0)
			{
				int count = r.ReadInt32();
				for (int i = 0; i < count; i++)
				{
					cardToRefresh.AddTarget(Program.instance.ocgcore.GCS_Get(r.ReadGPS()));
				}
			}
			if ((flag & 65536) != 0)
			{
				List<GameCard> overs = Program.instance.ocgcore.GCS_GetOverlays(cardToRefresh);
				int count2 = r.ReadInt32();
				for (int j = 0; j < count2; j++)
				{
					if (j < overs.Count)
					{
						overs[j].SetCode(r.ReadInt32());
					}
					else
					{
						r.ReadInt32();
					}
				}
			}
			if ((flag & 131072) != 0)
			{
				int count3 = r.ReadInt32();
				for (int k = 0; k < count3; k++)
				{
					r.ReadInt32();
				}
			}
			if ((flag & 262144) != 0)
			{
				r.ReadInt32();
			}
			if ((flag & 524288) != 0)
			{
				int status = r.ReadInt32();
				cardToRefresh.Disabled = (status & 1) == 1;
				cardToRefresh.SemiNomiSummoned = (status & 8) == 8;
			}
			if ((flag & 2097152) != 0)
			{
				data.LScale = r.ReadInt32();
			}
			if ((flag & 4194304) != 0)
			{
				data.RScale = r.ReadInt32();
			}
			int l3 = 0;
			if ((flag & 8388608) != 0)
			{
				l3 = r.ReadInt32();
				data.LinkMarker = r.ReadInt32();
			}
			if ((flag & 16) != 0 || (flag & 32) != 0 || (flag & 8388608) != 0)
			{
				if (l > l2)
				{
					data.Level = l;
				}
				else
				{
					data.Level = l2;
				}
				if (l3 > data.Level)
				{
					data.Level = l3;
				}
			}
			cardToRefresh.SetData(data);
		}
	}
}
