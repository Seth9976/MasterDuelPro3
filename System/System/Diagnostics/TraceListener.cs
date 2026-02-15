using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides the abstract base class for the listeners who monitor trace and debug output.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000166 RID: 358
	public abstract class TraceListener : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceListener" /> class using the specified name as the listener.</summary>
		/// <param name="name">The name of the <see cref="T:System.Diagnostics.TraceListener" />. </param>
		// Token: 0x0600084C RID: 2124 RVA: 0x0002CDFC File Offset: 0x0002AFFC
		protected TraceListener(string name)
		{
			this.listenerName = name;
		}

		/// <summary>Gets the custom trace listener attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the custom attributes for the trace listener.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0002CE19 File Offset: 0x0002B019
		public StringDictionary Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets or sets a name for this <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		/// <returns>A name for this <see cref="T:System.Diagnostics.TraceListener" />. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0002CE34 File Offset: 0x0002B034
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x0002CE4A File Offset: 0x0002B04A
		public virtual string Name
		{
			get
			{
				if (this.listenerName != null)
				{
					return this.listenerName;
				}
				return "";
			}
			set
			{
				this.listenerName = value;
			}
		}

		/// <summary>Gets a value indicating whether the trace listener is thread safe. </summary>
		/// <returns>true if the trace listener is thread safe; otherwise, false. The default is false.</returns>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x000028AE File Offset: 0x00000AAE
		public virtual bool IsThreadSafe
		{
			get
			{
				return false;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Diagnostics.TraceListener" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000851 RID: 2129 RVA: 0x0002CE53 File Offset: 0x0002B053
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.TraceListener" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000852 RID: 2130 RVA: 0x00002FA0 File Offset: 0x000011A0
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>When overridden in a derived class, flushes the output buffer.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000853 RID: 2131 RVA: 0x00002FA0 File Offset: 0x000011A0
		public virtual void Flush()
		{
		}

		/// <summary>Gets or sets the indent level.</summary>
		/// <returns>The indent level. The default is zero.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000162 RID: 354
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x0002CE62 File Offset: 0x0002B062
		public int IndentLevel
		{
			set
			{
				this.indentLevel = ((value < 0) ? 0 : value);
			}
		}

		/// <summary>Gets or sets the number of spaces in an indent.</summary>
		/// <returns>The number of spaces in an indent. The default is four spaces.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Set operation failed because the value is less than zero.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000163 RID: 355
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0002CE72 File Offset: 0x0002B072
		public int IndentSize
		{
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("IndentSize", value, SR.GetString("The IndentSize property must be non-negative."));
				}
				this.indentSize = value;
			}
		}

		/// <summary>Gets and sets the trace filter for the trace listener.</summary>
		/// <returns>An object derived from the <see cref="T:System.Diagnostics.TraceFilter" /> base class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0002CE9A File Offset: 0x0002B09A
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0002CEA2 File Offset: 0x0002B0A2
		[ComVisible(false)]
		public TraceFilter Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to indent the output.</summary>
		/// <returns>true if the output should be indented; otherwise, false.</returns>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0002CEAB File Offset: 0x0002B0AB
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0002CEB3 File Offset: 0x0002B0B3
		protected bool NeedIndent
		{
			get
			{
				return this.needIndent;
			}
			set
			{
				this.needIndent = value;
			}
		}

		/// <summary>Gets or sets the trace output options.</summary>
		/// <returns>A bitwise combination of the enumeration values. The default is <see cref="F:System.Diagnostics.TraceOptions.None" />. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Set operation failed because the value is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0002CEBC File Offset: 0x0002B0BC
		// (set) Token: 0x0600085B RID: 2139 RVA: 0x0002CEC4 File Offset: 0x0002B0C4
		[ComVisible(false)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return this.traceOptions;
			}
			set
			{
				if (value >> 6 != TraceOptions.None)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.traceOptions = value;
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002CEDD File Offset: 0x0002B0DD
		internal void SetAttributes(Hashtable attribs)
		{
			TraceUtils.VerifyAttributes(attribs, this.GetSupportedAttributes(), this);
			this.attributes = new StringDictionary();
			this.attributes.ReplaceHashtable(attribs);
		}

		/// <summary>Emits an error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to emit. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600085D RID: 2141 RVA: 0x0002CF03 File Offset: 0x0002B103
		public virtual void Fail(string message)
		{
			this.Fail(message, null);
		}

		/// <summary>Emits an error message and a detailed error message to the listener you create when you implement the <see cref="T:System.Diagnostics.TraceListener" /> class.</summary>
		/// <param name="message">A message to emit. </param>
		/// <param name="detailMessage">A detailed message to emit. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600085E RID: 2142 RVA: 0x0002CF10 File Offset: 0x0002B110
		public virtual void Fail(string message, string detailMessage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(SR.GetString("Fail:"));
			stringBuilder.Append(" ");
			stringBuilder.Append(message);
			if (detailMessage != null)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(detailMessage);
			}
			this.WriteLine(stringBuilder.ToString());
		}

		/// <summary>Gets the custom attributes supported by the trace listener.</summary>
		/// <returns>A string array naming the custom attributes supported by the trace listener, or null if there are no custom attributes.</returns>
		// Token: 0x0600085F RID: 2143 RVA: 0x000027B6 File Offset: 0x000009B6
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		/// <summary>When overridden in a derived class, writes the specified message to the listener you create in the derived class.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000860 RID: 2144
		public abstract void Write(string message);

		/// <summary>Writes the indent to the listener you create when you implement this class, and resets the <see cref="P:System.Diagnostics.TraceListener.NeedIndent" /> property to false.</summary>
		// Token: 0x06000861 RID: 2145 RVA: 0x0002CF6C File Offset: 0x0002B16C
		protected virtual void WriteIndent()
		{
			this.NeedIndent = false;
			for (int i = 0; i < this.indentLevel; i++)
			{
				if (this.indentSize == 4)
				{
					this.Write("    ");
				}
				else
				{
					for (int j = 0; j < this.indentSize; j++)
					{
						this.Write(" ");
					}
				}
			}
		}

		/// <summary>When overridden in a derived class, writes a message to the listener you create in the derived class, followed by a line terminator.</summary>
		/// <param name="message">A message to write. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000862 RID: 2146
		public abstract void WriteLine(string message);

		/// <summary>Writes trace information, a data object and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">The trace data to emit.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000863 RID: 2147 RVA: 0x0002CFC4 File Offset: 0x0002B1C4
		[ComVisible(false)]
		public virtual void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			string text = string.Empty;
			if (data != null)
			{
				text = data.ToString();
			}
			this.WriteLine(text);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a message, and event information to the listener specific output.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000864 RID: 2148 RVA: 0x0002D01A File Offset: 0x0002B21A
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, message))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			this.WriteLine(message);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002D052 File Offset: 0x0002B252
		private void WriteHeader(string source, TraceEventType eventType, int id)
		{
			this.Write(string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2} : ", source, eventType.ToString(), id.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002D084 File Offset: 0x0002B284
		private void WriteFooter(TraceEventCache eventCache)
		{
			if (eventCache == null)
			{
				return;
			}
			this.indentLevel++;
			if (this.IsEnabled(TraceOptions.ProcessId))
			{
				this.WriteLine("ProcessId=" + eventCache.ProcessId.ToString());
			}
			if (this.IsEnabled(TraceOptions.LogicalOperationStack))
			{
				this.Write("LogicalOperationStack=");
				Stack logicalOperationStack = eventCache.LogicalOperationStack;
				bool flag = true;
				foreach (object obj in logicalOperationStack)
				{
					if (!flag)
					{
						this.Write(", ");
					}
					else
					{
						flag = false;
					}
					this.Write(obj.ToString());
				}
				this.WriteLine(string.Empty);
			}
			if (this.IsEnabled(TraceOptions.ThreadId))
			{
				this.WriteLine("ThreadId=" + eventCache.ThreadId);
			}
			if (this.IsEnabled(TraceOptions.DateTime))
			{
				this.WriteLine("DateTime=" + eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
			}
			if (this.IsEnabled(TraceOptions.Timestamp))
			{
				this.WriteLine("Timestamp=" + eventCache.Timestamp.ToString());
			}
			if (this.IsEnabled(TraceOptions.Callstack))
			{
				this.WriteLine("Callstack=" + eventCache.Callstack);
			}
			this.indentLevel--;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002D1F4 File Offset: 0x0002B3F4
		internal bool IsEnabled(TraceOptions opts)
		{
			return (opts & this.TraceOutputOptions) > TraceOptions.None;
		}

		// Token: 0x04000664 RID: 1636
		private int indentLevel;

		// Token: 0x04000665 RID: 1637
		private int indentSize = 4;

		// Token: 0x04000666 RID: 1638
		private TraceOptions traceOptions;

		// Token: 0x04000667 RID: 1639
		private bool needIndent = true;

		// Token: 0x04000668 RID: 1640
		private string listenerName;

		// Token: 0x04000669 RID: 1641
		private TraceFilter filter;

		// Token: 0x0400066A RID: 1642
		private StringDictionary attributes;

		// Token: 0x0400066B RID: 1643
		internal string initializeData;
	}
}
