using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Represents errors that occur during application execution.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001A3 RID: 419
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class Exception : ISerializable
	{
		// Token: 0x06000F62 RID: 3938 RVA: 0x00040F67 File Offset: 0x0003F167
		private void Init()
		{
			this._message = null;
			this._stackTrace = null;
			this._dynamicMethods = null;
			this.HResult = -2146233088;
			this._safeSerializationManager = new SafeSerializationManager();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class.</summary>
		// Token: 0x06000F63 RID: 3939 RVA: 0x00040F94 File Offset: 0x0003F194
		public Exception()
		{
			this.Init();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error. </param>
		// Token: 0x06000F64 RID: 3940 RVA: 0x00040FA2 File Offset: 0x0003F1A2
		public Exception(string message)
		{
			this.Init();
			this._message = message;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
		// Token: 0x06000F65 RID: 3941 RVA: 0x00040FB7 File Offset: 0x0003F1B7
		public Exception(string message, Exception innerException)
		{
			this.Init();
			this._message = message;
			this._innerException = innerException;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Exception" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0). </exception>
		// Token: 0x06000F66 RID: 3942 RVA: 0x00040FD4 File Offset: 0x0003F1D4
		protected Exception(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._className = info.GetString("ClassName");
			this._message = info.GetString("Message");
			this._data = (IDictionary)info.GetValueNoThrow("Data", typeof(IDictionary));
			this._innerException = (Exception)info.GetValue("InnerException", typeof(Exception));
			this._helpURL = info.GetString("HelpURL");
			this._stackTraceString = info.GetString("StackTraceString");
			this._remoteStackTraceString = info.GetString("RemoteStackTraceString");
			this._remoteStackIndex = info.GetInt32("RemoteStackIndex");
			this.HResult = info.GetInt32("HResult");
			this._source = info.GetString("Source");
			this._safeSerializationManager = info.GetValueNoThrow("SafeSerializationManager", typeof(SafeSerializationManager)) as SafeSerializationManager;
			if (this._className == null || this.HResult == 0)
			{
				throw new SerializationException(Environment.GetResourceString("Insufficient state to return the real object."));
			}
			if (context.State == StreamingContextStates.CrossAppDomain)
			{
				this._remoteStackTraceString += this._stackTraceString;
				this._stackTraceString = null;
			}
		}

		/// <summary>Gets a message that describes the current exception.</summary>
		/// <returns>The error message that explains the reason for the exception, or an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00041129 File Offset: 0x0003F329
		public virtual string Message
		{
			get
			{
				if (this._message == null)
				{
					if (this._className == null)
					{
						this._className = this.GetClassName();
					}
					return Environment.GetResourceString("Exception of type '{0}' was thrown.", new object[] { this._className });
				}
				return this._message;
			}
		}

		/// <summary>Gets a collection of key/value pairs that provide additional user-defined information about the exception.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IDictionary" /> interface and contains a collection of user-defined key/value pairs. The default is an empty collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x00041167 File Offset: 0x0003F367
		public virtual IDictionary Data
		{
			get
			{
				if (this._data == null)
				{
					this._data = new ListDictionaryInternal();
				}
				return this._data;
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00041182 File Offset: 0x0003F382
		private string GetClassName()
		{
			if (this._className == null)
			{
				this._className = this.GetType().ToString();
			}
			return this._className;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> instance that caused the current exception.</summary>
		/// <returns>An instance of Exception that describes the error that caused the current exception. The InnerException property returns the same value as was passed into the constructor, or a null reference (Nothing in Visual Basic) if the inner exception value was not supplied to the constructor. This property is read-only.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x000411A3 File Offset: 0x0003F3A3
		public Exception InnerException
		{
			get
			{
				return this._innerException;
			}
		}

		/// <summary>Gets the method that throws the current exception.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> that threw the current exception.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x000411AC File Offset: 0x0003F3AC
		public MethodBase TargetSite
		{
			get
			{
				StackTrace stackTrace = new StackTrace(this, true);
				if (stackTrace.FrameCount > 0)
				{
					return stackTrace.GetFrame(0).GetMethod();
				}
				return null;
			}
		}

		/// <summary>Gets a string representation of the immediate frames on the call stack.</summary>
		/// <returns>A string that describes the immediate frames of the call stack.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x000411D8 File Offset: 0x0003F3D8
		public virtual string StackTrace
		{
			get
			{
				return this.GetStackTrace(true);
			}
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x000411E4 File Offset: 0x0003F3E4
		private string GetStackTrace(bool needFileInfo)
		{
			string text = this._stackTraceString;
			string text2 = this._remoteStackTraceString;
			if (!needFileInfo)
			{
				text = this.StripFileInfo(text, false);
				text2 = this.StripFileInfo(text2, true);
			}
			if (text != null)
			{
				return text2 + text;
			}
			if (this._stackTrace == null)
			{
				return text2;
			}
			string stackTrace = Environment.GetStackTrace(this, needFileInfo);
			return text2 + stackTrace;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00041238 File Offset: 0x0003F438
		internal void SetErrorCode(int hr)
		{
			this.HResult = hr;
		}

		/// <summary>Gets or sets a link to the help file associated with this exception.</summary>
		/// <returns>The Uniform Resource Name (URN) or Uniform Resource Locator (URL).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00041241 File Offset: 0x0003F441
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x00041249 File Offset: 0x0003F449
		public virtual string HelpLink
		{
			get
			{
				return this._helpURL;
			}
			set
			{
				this._helpURL = value;
			}
		}

		/// <summary>Gets or sets the name of the application or the object that causes the error.</summary>
		/// <returns>The name of the application or the object that causes the error.</returns>
		/// <exception cref="T:System.ArgumentException">The object must be a runtime <see cref="N:System.Reflection" /> object</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x00041254 File Offset: 0x0003F454
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x000412B1 File Offset: 0x0003F4B1
		public virtual string Source
		{
			get
			{
				if (this._source == null)
				{
					StackTrace stackTrace = new StackTrace(this, true);
					if (stackTrace.FrameCount > 0)
					{
						MethodBase method = stackTrace.GetFrame(0).GetMethod();
						if (method != null)
						{
							this._source = method.DeclaringType.Assembly.GetName().Name;
						}
					}
				}
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		/// <summary>Creates and returns a string representation of the current exception.</summary>
		/// <returns>A string representation of the current exception.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F73 RID: 3955 RVA: 0x000412BA File Offset: 0x0003F4BA
		public override string ToString()
		{
			return this.ToString(true, true);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x000412C4 File Offset: 0x0003F4C4
		private string ToString(bool needFileLineInfo, bool needMessage)
		{
			string text = (needMessage ? this.Message : null);
			string text2;
			if (text == null || text.Length <= 0)
			{
				text2 = this.GetClassName();
			}
			else
			{
				text2 = this.GetClassName() + ": " + text;
			}
			if (this._innerException != null)
			{
				text2 = string.Concat(new string[]
				{
					text2,
					" ---> ",
					this._innerException.ToString(needFileLineInfo, needMessage),
					Environment.NewLine,
					"   ",
					Environment.GetResourceString("--- End of inner exception stack trace ---")
				});
			}
			string stackTrace = this.GetStackTrace(needFileLineInfo);
			if (stackTrace != null)
			{
				text2 = text2 + Environment.NewLine + stackTrace;
			}
			return text2;
		}

		/// <summary>When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic). </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06000F75 RID: 3957 RVA: 0x0004136C File Offset: 0x0003F56C
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			string text = this._stackTraceString;
			if (this._stackTrace != null && text == null)
			{
				text = Environment.GetStackTrace(this, true);
			}
			if (this._source == null)
			{
				this._source = this.Source;
			}
			info.AddValue("ClassName", this.GetClassName(), typeof(string));
			info.AddValue("Message", this._message, typeof(string));
			info.AddValue("Data", this._data, typeof(IDictionary));
			info.AddValue("InnerException", this._innerException, typeof(Exception));
			info.AddValue("HelpURL", this._helpURL, typeof(string));
			info.AddValue("StackTraceString", text, typeof(string));
			info.AddValue("RemoteStackTraceString", this._remoteStackTraceString, typeof(string));
			info.AddValue("RemoteStackIndex", this._remoteStackIndex, typeof(int));
			info.AddValue("ExceptionMethod", null);
			info.AddValue("HResult", this.HResult);
			info.AddValue("Source", this._source, typeof(string));
			if (this._safeSerializationManager != null && this._safeSerializationManager.IsActive)
			{
				info.AddValue("SafeSerializationManager", this._safeSerializationManager, typeof(SafeSerializationManager));
				this._safeSerializationManager.CompleteSerialization(this, info, context);
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00041503 File Offset: 0x0003F703
		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			this._stackTrace = null;
			if (this._safeSerializationManager == null)
			{
				this._safeSerializationManager = new SafeSerializationManager();
				return;
			}
			this._safeSerializationManager.CompleteDeserialization(this);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0004152C File Offset: 0x0003F72C
		private string StripFileInfo(string stackTrace, bool isRemoteStackTrace)
		{
			return stackTrace;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0004152F File Offset: 0x0003F72F
		internal string RemoteStackTrace
		{
			get
			{
				return this._remoteStackTraceString;
			}
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00041537 File Offset: 0x0003F737
		internal void RestoreExceptionDispatchInfo(ExceptionDispatchInfo exceptionDispatchInfo)
		{
			this.captured_traces = (StackTrace[])exceptionDispatchInfo.BinaryStackTraceArray;
			this._stackTrace = null;
			this._stackTraceString = null;
		}

		/// <summary>Gets or sets HRESULT, a coded numerical value that is assigned to a specific exception.</summary>
		/// <returns>The HRESULT value.</returns>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00041558 File Offset: 0x0003F758
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00041560 File Offset: 0x0003F760
		public int HResult
		{
			get
			{
				return this._HResult;
			}
			protected set
			{
				this._HResult = value;
			}
		}

		/// <summary>Gets the runtime type of the current instance.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the exact runtime type of the current instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F7C RID: 3964 RVA: 0x0003395C File Offset: 0x00031B5C
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00041569 File Offset: 0x0003F769
		internal bool IsTransient
		{
			get
			{
				return Exception.nIsTransient(this._HResult);
			}
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x00033EF4 File Offset: 0x000320F4
		private static bool nIsTransient(int hr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00041576 File Offset: 0x0003F776
		internal static string GetMessageFromNativeResources(Exception.ExceptionMessageKind kind)
		{
			switch (kind)
			{
			case Exception.ExceptionMessageKind.ThreadAbort:
				return "Thread was being aborted.";
			case Exception.ExceptionMessageKind.ThreadInterrupted:
				return "Thread was interrupted from a waiting state.";
			case Exception.ExceptionMessageKind.OutOfMemory:
				return "Insufficient memory to continue the execution of the program.";
			default:
				return "";
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x000415A5 File Offset: 0x0003F7A5
		internal void SetMessage(string s)
		{
			this._message = s;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x000415B0 File Offset: 0x0003F7B0
		internal Exception FixRemotingException()
		{
			string text = string.Format((this._remoteStackIndex == 0) ? "{0}{0}Server stack trace: {0}{1}{0}{0}Exception rethrown at [{2}]: {0}" : "{1}{0}{0}Exception rethrown at [{2}]: {0}", Environment.NewLine, this.StackTrace, this._remoteStackIndex);
			this._remoteStackTraceString = text;
			this._remoteStackIndex++;
			this._stackTraceString = null;
			return this;
		}

		// Token: 0x040005FB RID: 1531
		[OptionalField]
		private static object s_EDILock = new object();

		// Token: 0x040005FC RID: 1532
		private string _className;

		// Token: 0x040005FD RID: 1533
		internal string _message;

		// Token: 0x040005FE RID: 1534
		private IDictionary _data;

		// Token: 0x040005FF RID: 1535
		private Exception _innerException;

		// Token: 0x04000600 RID: 1536
		private string _helpURL;

		// Token: 0x04000601 RID: 1537
		private object _stackTrace;

		// Token: 0x04000602 RID: 1538
		private string _stackTraceString;

		// Token: 0x04000603 RID: 1539
		private string _remoteStackTraceString;

		// Token: 0x04000604 RID: 1540
		private int _remoteStackIndex;

		// Token: 0x04000605 RID: 1541
		private object _dynamicMethods;

		// Token: 0x04000606 RID: 1542
		internal int _HResult;

		// Token: 0x04000607 RID: 1543
		private string _source;

		// Token: 0x04000608 RID: 1544
		[OptionalField(VersionAdded = 4)]
		private SafeSerializationManager _safeSerializationManager;

		// Token: 0x04000609 RID: 1545
		internal StackTrace[] captured_traces;

		// Token: 0x0400060A RID: 1546
		private IntPtr[] native_trace_ips;

		// Token: 0x0400060B RID: 1547
		private int caught_in_unmanaged;

		// Token: 0x0400060C RID: 1548
		private const int _COMPlusExceptionCode = -532462766;

		// Token: 0x020001A4 RID: 420
		internal enum ExceptionMessageKind
		{
			// Token: 0x0400060E RID: 1550
			ThreadAbort = 1,
			// Token: 0x0400060F RID: 1551
			ThreadInterrupted,
			// Token: 0x04000610 RID: 1552
			OutOfMemory
		}
	}
}
