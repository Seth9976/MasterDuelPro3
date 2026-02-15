using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.ServiceModel.Internals;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000034 RID: 52
	internal sealed class EtwDiagnosticTrace : DiagnosticTraceBase
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00004FB4 File Offset: 0x000031B4
		static EtwDiagnosticTrace()
		{
			if (!PartialTrustHelpers.HasEtwPermissions())
			{
				EtwDiagnosticTrace.defaultEtwProviderId = Guid.Empty;
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005030 File Offset: 0x00003230
		public EtwDiagnosticTrace(string traceSourceName, Guid etwProviderId)
			: base(traceSourceName)
		{
			try
			{
				this.TraceSourceName = traceSourceName;
				base.EventSourceName = this.TraceSourceName + " " + "4.0.0.0";
				this.CreateTraceSource();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				new EventLogger(base.EventSourceName, null).LogEvent(TraceEventType.Error, 4, 3221291108U, false, new string[] { ex.ToString() });
			}
			try
			{
				this.CreateEtwProvider(etwProviderId);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				this.etwProvider = null;
				new EventLogger(base.EventSourceName, null).LogEvent(TraceEventType.Error, 4, 3221291108U, false, new string[] { ex2.ToString() });
			}
			if (base.TracingEnabled || this.EtwTracingEnabled)
			{
				base.AddDomainEventHandlersForCleanup();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000511C File Offset: 0x0000331C
		public static Guid DefaultEtwProviderId
		{
			get
			{
				return EtwDiagnosticTrace.defaultEtwProviderId;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005123 File Offset: 0x00003323
		public EtwProvider EtwProvider
		{
			get
			{
				return this.etwProvider;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000512B File Offset: 0x0000332B
		public bool IsEtwProviderEnabled
		{
			get
			{
				return this.EtwTracingEnabled && this.etwProvider.IsEnabled();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005142 File Offset: 0x00003342
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000514F File Offset: 0x0000334F
		public Action RefreshState
		{
			get
			{
				return this.EtwProvider.ControllerCallBack;
			}
			set
			{
				this.EtwProvider.ControllerCallBack = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000515D File Offset: 0x0000335D
		public bool IsEnd2EndActivityTracingEnabled
		{
			get
			{
				return this.IsEtwProviderEnabled && this.EtwProvider.IsEnd2EndActivityTracingEnabled;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005174 File Offset: 0x00003374
		private bool EtwTracingEnabled
		{
			get
			{
				return this.etwProvider != null;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000517F File Offset: 0x0000337F
		public void SetEnd2EndActivityTracingEnabled(bool isEnd2EndTracingEnabled)
		{
			this.EtwProvider.SetEnd2EndActivityTracingEnabled(isEnd2EndTracingEnabled);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000518D File Offset: 0x0000338D
		public override bool ShouldTrace(TraceEventLevel level)
		{
			return base.ShouldTrace(level) || this.ShouldTraceToEtw(level);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000051A1 File Offset: 0x000033A1
		public bool ShouldTraceToEtw(TraceEventLevel level)
		{
			return this.EtwProvider != null && this.EtwProvider.IsEnabled((byte)level, 0L);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000051BC File Offset: 0x000033BC
		public void WriteTraceSource(ref EventDescriptor eventDescriptor, string description, TracePayload payload)
		{
			if (base.TracingEnabled)
			{
				XPathNavigator xpathNavigator = null;
				try
				{
					string text;
					int num;
					EtwDiagnosticTrace.GenerateLegacyTraceCode(ref eventDescriptor, out text, out num);
					string text2 = EtwDiagnosticTrace.BuildTrace(ref eventDescriptor, description, payload, text);
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(text2);
					xpathNavigator = xmlDocument.CreateNavigator();
					if (base.CalledShutdown)
					{
						base.TraceSource.Flush();
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					base.LogTraceFailure((xpathNavigator == null) ? string.Empty : xpathNavigator.ToString(), ex);
				}
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005248 File Offset: 0x00003448
		private static string BuildTrace(ref EventDescriptor eventDescriptor, string description, TracePayload payload, string msdnTraceCode)
		{
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string text;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						xmlTextWriter.WriteStartElement("TraceRecord");
						xmlTextWriter.WriteAttributeString("xmlns", "http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord");
						xmlTextWriter.WriteAttributeString("Severity", TraceLevelHelper.LookupSeverity((TraceEventLevel)eventDescriptor.Level, (TraceEventOpcode)eventDescriptor.Opcode));
						xmlTextWriter.WriteAttributeString("Channel", EtwDiagnosticTrace.LookupChannel((TraceChannel)eventDescriptor.Channel));
						xmlTextWriter.WriteElementString("TraceIdentifier", msdnTraceCode);
						xmlTextWriter.WriteElementString("Description", description);
						xmlTextWriter.WriteElementString("AppDomain", payload.AppDomainFriendlyName);
						if (!string.IsNullOrEmpty(payload.EventSource))
						{
							xmlTextWriter.WriteElementString("Source", payload.EventSource);
						}
						if (!string.IsNullOrEmpty(payload.ExtendedData))
						{
							xmlTextWriter.WriteRaw(payload.ExtendedData);
						}
						if (!string.IsNullOrEmpty(payload.SerializedException))
						{
							xmlTextWriter.WriteRaw(payload.SerializedException);
						}
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.Flush();
						stringWriter.Flush();
						text = stringBuilder.ToString();
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return text;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000053C0 File Offset: 0x000035C0
		private static void GenerateLegacyTraceCode(ref EventDescriptor eventDescriptor, out string msdnTraceCode, out int legacyEventId)
		{
			switch (eventDescriptor.EventId)
			{
			case 57393:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "AppDomainUnload");
				legacyEventId = 131073;
				return;
			case 57394:
			case 57404:
			case 57405:
			case 57406:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "TraceHandledException");
				legacyEventId = 131076;
				return;
			case 57396:
			case 57407:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "ThrowingException");
				legacyEventId = 131075;
				return;
			case 57397:
				msdnTraceCode = EtwDiagnosticTrace.GenerateMsdnTraceCode("System.ServiceModel.Diagnostics", "UnhandledException");
				legacyEventId = 131077;
				return;
			}
			msdnTraceCode = eventDescriptor.EventId.ToString(CultureInfo.InvariantCulture);
			legacyEventId = eventDescriptor.EventId;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000549F File Offset: 0x0000369F
		private static string GenerateMsdnTraceCode(string traceSource, string traceCodeString)
		{
			return string.Format(CultureInfo.InvariantCulture, "http://msdn.microsoft.com/{0}/library/{1}.{2}.aspx", CultureInfo.CurrentCulture.Name, traceSource, traceCodeString);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000054BC File Offset: 0x000036BC
		private static string LookupChannel(TraceChannel traceChannel)
		{
			string text;
			if (traceChannel != TraceChannel.Application)
			{
				switch (traceChannel)
				{
				case TraceChannel.Admin:
					text = "Admin";
					break;
				case TraceChannel.Operational:
					text = "Operational";
					break;
				case TraceChannel.Analytic:
					text = "Analytic";
					break;
				case TraceChannel.Debug:
					text = "Debug";
					break;
				case TraceChannel.Perf:
					text = "Perf";
					break;
				default:
					text = traceChannel.ToString();
					break;
				}
			}
			else
			{
				text = "Application";
			}
			return text;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000552C File Offset: 0x0000372C
		public TracePayload GetSerializedPayload(object source, TraceRecord traceRecord, Exception exception)
		{
			return this.GetSerializedPayload(source, traceRecord, exception, false);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005538 File Offset: 0x00003738
		public TracePayload GetSerializedPayload(object source, TraceRecord traceRecord, Exception exception, bool getServiceReference)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			if (source != null)
			{
				text = DiagnosticTraceBase.CreateSourceString(source);
			}
			if (traceRecord != null)
			{
				StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
				try
				{
					using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
					{
						using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
						{
							xmlTextWriter.WriteStartElement("ExtendedData");
							traceRecord.WriteTo(xmlTextWriter);
							xmlTextWriter.WriteEndElement();
							xmlTextWriter.Flush();
							stringWriter.Flush();
							text2 = stringBuilder.ToString();
						}
					}
				}
				finally
				{
					EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
				}
			}
			if (exception != null)
			{
				text3 = EtwDiagnosticTrace.ExceptionToTraceString(exception, 28672);
			}
			if (getServiceReference && EtwDiagnosticTrace.traceAnnotation != null)
			{
				return new TracePayload(text3, text, DiagnosticTraceBase.AppDomainFriendlyName, text2, EtwDiagnosticTrace.traceAnnotation());
			}
			return new TracePayload(text3, text, DiagnosticTraceBase.AppDomainFriendlyName, text2, string.Empty);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005634 File Offset: 0x00003834
		public bool IsEtwEventEnabled(ref EventDescriptor eventDescriptor, bool fullCheck)
		{
			if (fullCheck)
			{
				return this.EtwTracingEnabled && this.etwProvider.IsEventEnabled(ref eventDescriptor);
			}
			return this.EtwTracingEnabled && this.etwProvider.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005671 File Offset: 0x00003871
		private void CreateTraceSource()
		{
			if (!string.IsNullOrEmpty(this.TraceSourceName))
			{
				base.SetTraceSource(new DiagnosticTraceSource(this.TraceSourceName));
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005694 File Offset: 0x00003894
		private void CreateEtwProvider(Guid etwProviderId)
		{
			if (etwProviderId != Guid.Empty && EtwDiagnosticTrace.isVistaOrGreater)
			{
				this.etwProvider = (EtwProvider)EtwDiagnosticTrace.etwProviderCache[etwProviderId];
				if (this.etwProvider == null)
				{
					Hashtable hashtable = EtwDiagnosticTrace.etwProviderCache;
					lock (hashtable)
					{
						this.etwProvider = (EtwProvider)EtwDiagnosticTrace.etwProviderCache[etwProviderId];
						if (this.etwProvider == null)
						{
							this.etwProvider = new EtwProvider(etwProviderId);
							EtwDiagnosticTrace.etwProviderCache.Add(etwProviderId, this.etwProvider);
						}
					}
				}
				this.etwProviderId = etwProviderId;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005758 File Offset: 0x00003958
		protected override void OnShutdownTracing()
		{
			this.ShutdownTraceSource();
			this.ShutdownEtwProvider();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005768 File Offset: 0x00003968
		private void ShutdownTraceSource()
		{
			try
			{
				if (TraceCore.AppDomainUnloadIsEnabled(this))
				{
					TraceCore.AppDomainUnload(this, AppDomain.CurrentDomain.FriendlyName, DiagnosticTraceBase.ProcessName, DiagnosticTraceBase.ProcessId.ToString(CultureInfo.CurrentCulture));
				}
				base.TraceSource.Flush();
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				base.LogTraceFailure(null, ex);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000057D8 File Offset: 0x000039D8
		private void ShutdownEtwProvider()
		{
			try
			{
				if (this.etwProvider != null)
				{
					this.etwProvider.Dispose();
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				base.LogTraceFailure(null, ex);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005820 File Offset: 0x00003A20
		public override bool IsEnabled()
		{
			return TraceCore.TraceCodeEventLogCriticalIsEnabled(this) || TraceCore.TraceCodeEventLogVerboseIsEnabled(this) || TraceCore.TraceCodeEventLogInfoIsEnabled(this) || TraceCore.TraceCodeEventLogWarningIsEnabled(this) || TraceCore.TraceCodeEventLogErrorIsEnabled(this);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000584C File Offset: 0x00003A4C
		public override void TraceEventLogEvent(TraceEventType type, TraceRecord traceRecord)
		{
			switch (type)
			{
			case TraceEventType.Critical:
				if (TraceCore.TraceCodeEventLogCriticalIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogCritical(this, traceRecord);
					return;
				}
				break;
			case TraceEventType.Error:
				if (TraceCore.TraceCodeEventLogErrorIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogError(this, traceRecord);
				}
				break;
			case (TraceEventType)3:
				break;
			case TraceEventType.Warning:
				if (TraceCore.TraceCodeEventLogWarningIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogWarning(this, traceRecord);
					return;
				}
				break;
			default:
				if (type != TraceEventType.Information)
				{
					if (type != TraceEventType.Verbose)
					{
						return;
					}
					if (TraceCore.TraceCodeEventLogVerboseIsEnabled(this))
					{
						TraceCore.TraceCodeEventLogVerbose(this, traceRecord);
						return;
					}
				}
				else if (TraceCore.TraceCodeEventLogInfoIsEnabled(this))
				{
					TraceCore.TraceCodeEventLogInfo(this, traceRecord);
					return;
				}
				break;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000058CA File Offset: 0x00003ACA
		protected override void OnUnhandledException(Exception exception)
		{
			if (TraceCore.UnhandledExceptionIsEnabled(this))
			{
				TraceCore.UnhandledException(this, (exception != null) ? exception.ToString() : string.Empty, exception);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000058EC File Offset: 0x00003AEC
		internal static string ExceptionToTraceString(Exception exception, int maxTraceStringLength)
		{
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string text;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						EtwDiagnosticTrace.WriteExceptionToTraceString(xmlTextWriter, exception, maxTraceStringLength, 64);
						xmlTextWriter.Flush();
						stringWriter.Flush();
						text = stringBuilder.ToString();
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return text;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005978 File Offset: 0x00003B78
		private static void WriteExceptionToTraceString(XmlTextWriter xml, Exception exception, int remainingLength, int remainingAllowedRecursionDepth)
		{
			if (remainingAllowedRecursionDepth < 1)
			{
				return;
			}
			if (!EtwDiagnosticTrace.WriteStartElement(xml, "Exception", ref remainingLength))
			{
				return;
			}
			try
			{
				IList<Tuple<string, string>> list = new List<Tuple<string, string>>
				{
					new Tuple<string, string>("ExceptionType", DiagnosticTraceBase.XmlEncode(exception.GetType().AssemblyQualifiedName)),
					new Tuple<string, string>("Message", DiagnosticTraceBase.XmlEncode(exception.Message)),
					new Tuple<string, string>("StackTrace", DiagnosticTraceBase.XmlEncode(DiagnosticTraceBase.StackTraceString(exception))),
					new Tuple<string, string>("ExceptionString", DiagnosticTraceBase.XmlEncode(exception.ToString()))
				};
				Win32Exception ex = exception as Win32Exception;
				if (ex != null)
				{
					list.Add(new Tuple<string, string>("NativeErrorCode", ex.NativeErrorCode.ToString("X", CultureInfo.InvariantCulture)));
				}
				foreach (Tuple<string, string> tuple in list)
				{
					if (!EtwDiagnosticTrace.WriteXmlElementString(xml, tuple.Item1, tuple.Item2, ref remainingLength))
					{
						return;
					}
				}
				if (exception.Data != null && exception.Data.Count > 0)
				{
					string exceptionData = EtwDiagnosticTrace.GetExceptionData(exception);
					if (exceptionData.Length < remainingLength)
					{
						xml.WriteRaw(exceptionData);
						remainingLength -= exceptionData.Length;
					}
				}
				if (exception.InnerException != null)
				{
					string innerException = EtwDiagnosticTrace.GetInnerException(exception, remainingLength, remainingAllowedRecursionDepth - 1);
					if (!string.IsNullOrEmpty(innerException) && innerException.Length < remainingLength)
					{
						xml.WriteRaw(innerException);
					}
				}
			}
			finally
			{
				xml.WriteEndElement();
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005B30 File Offset: 0x00003D30
		private static string GetInnerException(Exception exception, int remainingLength, int remainingAllowedRecursionDepth)
		{
			if (remainingAllowedRecursionDepth < 1)
			{
				return null;
			}
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string text;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						if (!EtwDiagnosticTrace.WriteStartElement(xmlTextWriter, "InnerException", ref remainingLength))
						{
							text = null;
						}
						else
						{
							EtwDiagnosticTrace.WriteExceptionToTraceString(xmlTextWriter, exception.InnerException, remainingLength, remainingAllowedRecursionDepth);
							xmlTextWriter.WriteEndElement();
							xmlTextWriter.Flush();
							stringWriter.Flush();
							text = stringBuilder.ToString();
						}
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return text;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005BDC File Offset: 0x00003DDC
		private static string GetExceptionData(Exception exception)
		{
			StringBuilder stringBuilder = EtwDiagnosticTrace.StringBuilderPool.Take();
			string text;
			try
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.CurrentCulture))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
					{
						xmlTextWriter.WriteStartElement("DataItems");
						foreach (object obj in exception.Data.Keys)
						{
							xmlTextWriter.WriteStartElement("Data");
							xmlTextWriter.WriteElementString("Key", DiagnosticTraceBase.XmlEncode(obj.ToString()));
							if (exception.Data[obj] == null)
							{
								xmlTextWriter.WriteElementString("Value", string.Empty);
							}
							else
							{
								xmlTextWriter.WriteElementString("Value", DiagnosticTraceBase.XmlEncode(exception.Data[obj].ToString()));
							}
							xmlTextWriter.WriteEndElement();
						}
						xmlTextWriter.WriteEndElement();
						xmlTextWriter.Flush();
						stringWriter.Flush();
						text = stringBuilder.ToString();
					}
				}
			}
			finally
			{
				EtwDiagnosticTrace.StringBuilderPool.Return(stringBuilder);
			}
			return text;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005D20 File Offset: 0x00003F20
		private static bool WriteStartElement(XmlTextWriter xml, string localName, ref int remainingLength)
		{
			int num = localName.Length * 2 + 5;
			if (num <= remainingLength)
			{
				xml.WriteStartElement(localName);
				remainingLength -= num;
				return true;
			}
			return false;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005D50 File Offset: 0x00003F50
		private static bool WriteXmlElementString(XmlTextWriter xml, string localName, string value, ref int remainingLength)
		{
			int num;
			if (string.IsNullOrEmpty(value) && !LocalAppContextSwitches.IncludeNullExceptionMessageInETWTrace)
			{
				num = localName.Length + 4;
			}
			else
			{
				num = localName.Length * 2 + 5 + value.Length;
			}
			if (num <= remainingLength)
			{
				xml.WriteElementString(localName, value);
				remainingLength -= num;
				return true;
			}
			return false;
		}

		// Token: 0x0400007B RID: 123
		public static readonly Guid ImmutableDefaultEtwProviderId = new Guid("{c651f5f6-1c0d-492e-8ae1-b4efd7c9d503}");

		// Token: 0x0400007C RID: 124
		private static Guid defaultEtwProviderId = EtwDiagnosticTrace.ImmutableDefaultEtwProviderId;

		// Token: 0x0400007D RID: 125
		private static Hashtable etwProviderCache = new Hashtable();

		// Token: 0x0400007E RID: 126
		private static bool isVistaOrGreater = Environment.OSVersion.Version.Major >= 6;

		// Token: 0x0400007F RID: 127
		private static Func<string> traceAnnotation;

		// Token: 0x04000080 RID: 128
		private EtwProvider etwProvider;

		// Token: 0x04000081 RID: 129
		private Guid etwProviderId;

		// Token: 0x04000082 RID: 130
		private static EventDescriptor transferEventDescriptor = new EventDescriptor(499, 0, 18, 0, 0, 0, 2305843009215397989L);

		// Token: 0x02000035 RID: 53
		private static class StringBuilderPool
		{
			// Token: 0x06000124 RID: 292 RVA: 0x00005DA0 File Offset: 0x00003FA0
			public static StringBuilder Take()
			{
				StringBuilder stringBuilder = null;
				if (EtwDiagnosticTrace.StringBuilderPool.freeStringBuilders.TryDequeue(out stringBuilder))
				{
					return stringBuilder;
				}
				return new StringBuilder();
			}

			// Token: 0x06000125 RID: 293 RVA: 0x00005DC4 File Offset: 0x00003FC4
			public static void Return(StringBuilder sb)
			{
				if (EtwDiagnosticTrace.StringBuilderPool.freeStringBuilders.Count <= 64)
				{
					sb.Clear();
					EtwDiagnosticTrace.StringBuilderPool.freeStringBuilders.Enqueue(sb);
				}
			}

			// Token: 0x04000083 RID: 131
			private static readonly ConcurrentQueue<StringBuilder> freeStringBuilders = new ConcurrentQueue<StringBuilder>();
		}
	}
}
