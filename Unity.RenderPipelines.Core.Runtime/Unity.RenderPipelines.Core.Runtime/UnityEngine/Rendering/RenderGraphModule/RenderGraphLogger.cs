using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200024B RID: 587
	internal class RenderGraphLogger
	{
		// Token: 0x06000FEC RID: 4076 RVA: 0x0003A1EC File Offset: 0x000383EC
		public void Initialize(string logName)
		{
			StringBuilder stringBuilder;
			if (!this.m_LogMap.TryGetValue(logName, out stringBuilder))
			{
				stringBuilder = new StringBuilder();
				this.m_LogMap.Add(logName, stringBuilder);
			}
			this.m_CurrentBuilder = stringBuilder;
			this.m_CurrentBuilder.Clear();
			this.m_CurrentIndentation = 0;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003A236 File Offset: 0x00038436
		public void IncrementIndentation(int value)
		{
			this.m_CurrentIndentation += Math.Abs(value);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003A24B File Offset: 0x0003844B
		public void DecrementIndentation(int value)
		{
			this.m_CurrentIndentation = Math.Max(0, this.m_CurrentIndentation - Math.Abs(value));
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003A268 File Offset: 0x00038468
		public void LogLine(string format, params object[] args)
		{
			for (int i = 0; i < this.m_CurrentIndentation; i++)
			{
				this.m_CurrentBuilder.Append('\t');
			}
			this.m_CurrentBuilder.AppendFormat(format, args);
			this.m_CurrentBuilder.AppendLine();
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003A2B0 File Offset: 0x000384B0
		public string GetLog(string logName)
		{
			StringBuilder builder;
			if (this.m_LogMap.TryGetValue(logName, out builder))
			{
				return builder.ToString();
			}
			return "";
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003A2DC File Offset: 0x000384DC
		public string GetAllLogs()
		{
			string result = "";
			foreach (KeyValuePair<string, StringBuilder> kvp in this.m_LogMap)
			{
				StringBuilder builder = kvp.Value;
				builder.AppendLine();
				result += builder.ToString();
			}
			this.m_LogMap.Clear();
			return result;
		}

		// Token: 0x04000A43 RID: 2627
		private Dictionary<string, StringBuilder> m_LogMap = new Dictionary<string, StringBuilder>();

		// Token: 0x04000A44 RID: 2628
		private StringBuilder m_CurrentBuilder;

		// Token: 0x04000A45 RID: 2629
		private int m_CurrentIndentation;
	}
}
