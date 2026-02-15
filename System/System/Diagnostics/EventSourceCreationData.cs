using System;

namespace System.Diagnostics
{
	/// <summary>Represents the configuration settings used to create an event log source on the local computer or a remote computer.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200018C RID: 396
	public class EventSourceCreationData
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x00031884 File Offset: 0x0002FA84
		internal EventSourceCreationData(string source, string logName, string machineName)
		{
			this._source = source;
			if (logName == null || logName.Length == 0)
			{
				this._logName = "Application";
			}
			else
			{
				this._logName = logName;
			}
			this._machineName = machineName;
		}

		/// <summary>Gets or sets the number of categories in the category resource file.</summary>
		/// <returns>The number of categories in the category resource file. The default value is zero.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x000318B9 File Offset: 0x0002FAB9
		public int CategoryCount
		{
			get
			{
				return this._categoryCount;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains category strings for the source.</summary>
		/// <returns>The path of the category resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x000318C1 File Offset: 0x0002FAC1
		public string CategoryResourceFile
		{
			get
			{
				return this._categoryResourceFile;
			}
		}

		/// <summary>Gets or sets the name of the event log to which the source writes entries.</summary>
		/// <returns>The name of the event log. This can be Application, System, or a custom log name. The default value is "Application."</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000969 RID: 2409 RVA: 0x000318C9 File Offset: 0x0002FAC9
		public string LogName
		{
			get
			{
				return this._logName;
			}
		}

		/// <summary>Gets or sets the name of the computer on which to register the event source.</summary>
		/// <returns>The name of the system on which to register the event source. The default is the local computer (".").</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x000318D1 File Offset: 0x0002FAD1
		public string MachineName
		{
			get
			{
				return this._machineName;
			}
		}

		/// <summary>Gets or sets the path of the message resource file that contains message formatting strings for the source.</summary>
		/// <returns>The path of the message resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x000318D9 File Offset: 0x0002FAD9
		public string MessageResourceFile
		{
			get
			{
				return this._messageResourceFile;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains message parameter strings for the source.</summary>
		/// <returns>The path of the parameter resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x000318E1 File Offset: 0x0002FAE1
		public string ParameterResourceFile
		{
			get
			{
				return this._parameterResourceFile;
			}
		}

		/// <summary>Gets or sets the name to register with the event log as an event source.</summary>
		/// <returns>The name to register with the event log as a source of entries. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x000318E9 File Offset: 0x0002FAE9
		public string Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x0400071C RID: 1820
		private string _source;

		// Token: 0x0400071D RID: 1821
		private string _logName;

		// Token: 0x0400071E RID: 1822
		private string _machineName;

		// Token: 0x0400071F RID: 1823
		private string _messageResourceFile;

		// Token: 0x04000720 RID: 1824
		private string _parameterResourceFile;

		// Token: 0x04000721 RID: 1825
		private string _categoryResourceFile;

		// Token: 0x04000722 RID: 1826
		private int _categoryCount;
	}
}
