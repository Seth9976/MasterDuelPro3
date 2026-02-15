using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	/// <summary>Provides access to local and remote processes and enables you to start and stop local system processes.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000171 RID: 369
	[DefaultEvent("Exited")]
	[Designer("System.Diagnostics.Design.ProcessDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MonitoringDescription("Provides access to local and remote processes, enabling starting and stopping of local processes.")]
	[DefaultProperty("StartInfo")]
	public class Process : Component
	{
		/// <summary>Occurs when an application writes to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060008AC RID: 2220 RVA: 0x0002E658 File Offset: 0x0002C858
		// (remove) Token: 0x060008AD RID: 2221 RVA: 0x0002E690 File Offset: 0x0002C890
		[Browsable(true)]
		[MonitoringDescription("Indicates if the process component is associated with a real process.")]
		public event DataReceivedEventHandler OutputDataReceived;

		/// <summary>Occurs when an application writes to its redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060008AE RID: 2222 RVA: 0x0002E6C8 File Offset: 0x0002C8C8
		// (remove) Token: 0x060008AF RID: 2223 RVA: 0x0002E700 File Offset: 0x0002C900
		[MonitoringDescription("Indicates if the process component is associated with a real process.")]
		[Browsable(true)]
		public event DataReceivedEventHandler ErrorDataReceived;

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Process" /> class.</summary>
		// Token: 0x060008B0 RID: 2224 RVA: 0x0002E735 File Offset: 0x0002C935
		public Process()
		{
			this.machineName = ".";
			this.outputStreamReadMode = Process.StreamReadMode.undefined;
			this.errorStreamReadMode = Process.StreamReadMode.undefined;
			this.m_processAccess = 2035711;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0002E761 File Offset: 0x0002C961
		private Process(string machineName, bool isRemoteMachine, int processId, ProcessInfo processInfo)
		{
			this.machineName = machineName;
			this.isRemoteMachine = isRemoteMachine;
			this.processId = processId;
			this.haveProcessId = true;
			this.outputStreamReadMode = Process.StreamReadMode.undefined;
			this.errorStreamReadMode = Process.StreamReadMode.undefined;
			this.m_processAccess = 2035711;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0002E79E File Offset: 0x0002C99E
		[Browsable(false)]
		[MonitoringDescription("Indicates if the process component is associated with a real process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		private bool Associated
		{
			get
			{
				return this.haveProcessId || this.haveProcessHandle;
			}
		}

		/// <summary>Gets the value that the associated process specified when it terminated.</summary>
		/// <returns>The code that the associated process specified when it terminated.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process has not exited.-or- The process <see cref="P:System.Diagnostics.Process.Handle" /> is not valid. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.ExitCode" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0002E7B0 File Offset: 0x0002C9B0
		[MonitoringDescription("The value returned from the associated process when it terminated.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int ExitCode
		{
			get
			{
				this.EnsureState(Process.State.Exited);
				if (this.exitCode == -1 && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					throw new InvalidOperationException("Cannot get the exit code from a non-child process on Unix");
				}
				return this.exitCode;
			}
		}

		/// <summary>Gets a value indicating whether the associated process has been terminated.</summary>
		/// <returns>true if the operating system process referenced by the <see cref="T:System.Diagnostics.Process" /> component has terminated; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">There is no process associated with the object. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The exit code for the process could not be retrieved. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.HasExited" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0002E7E0 File Offset: 0x0002C9E0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Indicates if the associated process has been terminated.")]
		public bool HasExited
		{
			get
			{
				if (!this.exited)
				{
					this.EnsureState(Process.State.Associated);
					SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1049600, false);
						int num;
						if (safeProcessHandle.IsInvalid)
						{
							this.exited = true;
						}
						else if (NativeMethods.GetExitCodeProcess(safeProcessHandle, out num) && num != 259)
						{
							this.exited = true;
							this.exitCode = num;
						}
						else
						{
							if (!this.signaled)
							{
								ProcessWaitHandle processWaitHandle = null;
								try
								{
									processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
									this.signaled = processWaitHandle.WaitOne(0, false);
								}
								finally
								{
									if (processWaitHandle != null)
									{
										processWaitHandle.Close();
									}
								}
							}
							if (this.signaled)
							{
								if (!NativeMethods.GetExitCodeProcess(safeProcessHandle, out num))
								{
									throw new Win32Exception();
								}
								this.exited = true;
								this.exitCode = num;
							}
						}
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
					if (this.exited)
					{
						this.RaiseOnExited();
					}
				}
				return this.exited;
			}
		}

		/// <summary>Gets the native handle of the associated process.</summary>
		/// <returns>The handle that the operating system assigned to the associated process when the process was started. The system uses this handle to keep track of process attributes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process has not been started or has exited. The <see cref="P:System.Diagnostics.Process.Handle" /> property cannot be read because there is no process associated with this <see cref="T:System.Diagnostics.Process" /> instance.-or- The <see cref="T:System.Diagnostics.Process" /> instance has been attached to a running process but you do not have the necessary permissions to get a handle with full access rights. </exception>
		/// <exception cref="T:System.NotSupportedException">You are trying to access the <see cref="P:System.Diagnostics.Process.Handle" /> property for a process that is running on a remote computer. This property is available only for processes that are running on the local computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0002E8D0 File Offset: 0x0002CAD0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("Returns the native handle for this process.   The handle is only available if the process was started using this component.")]
		public IntPtr Handle
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.OpenProcessHandle(this.m_processAccess).DangerousGetHandle();
			}
		}

		/// <summary>Gets the unique identifier for the associated process.</summary>
		/// <returns>The system-generated unique identifier of the process that is referenced by this <see cref="T:System.Diagnostics.Process" /> instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process's <see cref="P:System.Diagnostics.Process.Id" /> property has not been set.-or- There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set the <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property to false to access this property on Windows 98 and Windows Me.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0002E8EB File Offset: 0x0002CAEB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The unique identifier for the process.")]
		public int Id
		{
			get
			{
				this.EnsureState(Process.State.HaveId);
				return this.processId;
			}
		}

		/// <summary>Gets or sets the properties to pass to the <see cref="M:System.Diagnostics.Process.Start" /> method of the <see cref="T:System.Diagnostics.Process" />.</summary>
		/// <returns>The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that represents the data with which to start the process. These arguments include the name of the executable file or document used to start the process.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value that specifies the <see cref="P:System.Diagnostics.Process.StartInfo" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0002E8FA File Offset: 0x0002CAFA
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0002E916 File Offset: 0x0002CB16
		[MonitoringDescription("Specifies information used to start a process.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ProcessStartInfo StartInfo
		{
			get
			{
				if (this.startInfo == null)
				{
					this.startInfo = new ProcessStartInfo(this);
				}
				return this.startInfo;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.startInfo = value;
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls that are issued as a result of a process exit event.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> used to marshal event handler calls that are issued as a result of an <see cref="E:System.Diagnostics.Process.Exited" /> event on the process.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0002E930 File Offset: 0x0002CB30
		[Browsable(false)]
		[MonitoringDescription("The object used to marshal the event handler calls issued as a result of a Process exit.")]
		[DefaultValue(null)]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (this.synchronizingObject == null && base.DesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002E98A File Offset: 0x0002CB8A
		private void ReleaseProcessHandle(SafeProcessHandle handle)
		{
			if (handle == null)
			{
				return;
			}
			if (this.haveProcessHandle && handle == this.m_processHandle)
			{
				return;
			}
			handle.Close();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002E9A8 File Offset: 0x0002CBA8
		private void CompletionCallback(object context, bool wasSignaled)
		{
			this.StopWatchingForExit();
			this.RaiseOnExited();
		}

		/// <summary>Release all resources used by this process.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060008BC RID: 2236 RVA: 0x0002E9B6 File Offset: 0x0002CBB6
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.Close();
				}
				this.disposed = true;
				base.Dispose(disposing);
			}
		}

		/// <summary>Frees all the resources that are associated with this component.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008BD RID: 2237 RVA: 0x0002E9D8 File Offset: 0x0002CBD8
		public void Close()
		{
			if (this.Associated)
			{
				if (this.haveProcessHandle)
				{
					this.StopWatchingForExit();
					this.m_processHandle.Close();
					this.m_processHandle = null;
					this.haveProcessHandle = false;
				}
				this.haveProcessId = false;
				this.isRemoteMachine = false;
				this.machineName = ".";
				this.raisedOnExited = false;
				StreamWriter streamWriter = this.standardInput;
				this.standardInput = null;
				if (this.inputStreamReadMode == Process.StreamReadMode.undefined && streamWriter != null)
				{
					streamWriter.Close();
				}
				StreamReader streamReader = this.standardOutput;
				this.standardOutput = null;
				if (this.outputStreamReadMode == Process.StreamReadMode.undefined && streamReader != null)
				{
					streamReader.Close();
				}
				streamReader = this.standardError;
				this.standardError = null;
				if (this.errorStreamReadMode == Process.StreamReadMode.undefined && streamReader != null)
				{
					streamReader.Close();
				}
				AsyncStreamReader asyncStreamReader = this.output;
				this.output = null;
				if (this.outputStreamReadMode == Process.StreamReadMode.asyncMode && asyncStreamReader != null)
				{
					asyncStreamReader.CancelOperation();
					asyncStreamReader.Close();
				}
				asyncStreamReader = this.error;
				this.error = null;
				if (this.errorStreamReadMode == Process.StreamReadMode.asyncMode && asyncStreamReader != null)
				{
					asyncStreamReader.CancelOperation();
					asyncStreamReader.Close();
				}
				this.Refresh();
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002EAE8 File Offset: 0x0002CCE8
		private void EnsureState(Process.State state)
		{
			if ((state & Process.State.Associated) != (Process.State)0 && !this.Associated)
			{
				throw new InvalidOperationException(SR.GetString("No process is associated with this object."));
			}
			if ((state & Process.State.HaveId) != (Process.State)0 && !this.haveProcessId)
			{
				this.EnsureState(Process.State.Associated);
				throw new InvalidOperationException(SR.GetString("Feature requires a process identifier."));
			}
			if ((state & Process.State.IsLocal) != (Process.State)0 && this.isRemoteMachine)
			{
				throw new NotSupportedException(SR.GetString("Feature is not supported for remote machines."));
			}
			if ((state & Process.State.HaveProcessInfo) != (Process.State)0)
			{
				throw new InvalidOperationException(SR.GetString("Process has exited, so the requested information is not available."));
			}
			if ((state & Process.State.Exited) != (Process.State)0)
			{
				if (!this.HasExited)
				{
					throw new InvalidOperationException(SR.GetString("Process must exit before requested information can be determined."));
				}
				if (!this.haveProcessHandle)
				{
					throw new InvalidOperationException(SR.GetString("Process was not started by this object, so requested information cannot be determined."));
				}
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0002EBA0 File Offset: 0x0002CDA0
		private void EnsureWatchingForExit()
		{
			if (!this.watchingForExit)
			{
				lock (this)
				{
					if (!this.watchingForExit)
					{
						this.watchingForExit = true;
						try
						{
							this.waitHandle = new ProcessWaitHandle(this.m_processHandle);
							this.registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.waitHandle, new WaitOrTimerCallback(this.CompletionCallback), null, -1, true);
						}
						catch
						{
							this.watchingForExit = false;
							throw;
						}
					}
				}
			}
		}

		/// <summary>Gets a new <see cref="T:System.Diagnostics.Process" /> component and associates it with the currently active process.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component associated with the process resource that is running the calling application.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008C0 RID: 2240 RVA: 0x0002EC38 File Offset: 0x0002CE38
		public static Process GetCurrentProcess()
		{
			return new Process(".", false, NativeMethods.GetCurrentProcessId(), null);
		}

		/// <summary>Raises the <see cref="E:System.Diagnostics.Process.Exited" /> event.</summary>
		// Token: 0x060008C1 RID: 2241 RVA: 0x0002EC4C File Offset: 0x0002CE4C
		protected void OnExited()
		{
			EventHandler eventHandler = this.onExited;
			if (eventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(eventHandler, new object[]
					{
						this,
						EventArgs.Empty
					});
					return;
				}
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002ECA4 File Offset: 0x0002CEA4
		private SafeProcessHandle GetProcessHandle(int access, bool throwIfExited)
		{
			if (this.haveProcessHandle)
			{
				if (throwIfExited)
				{
					ProcessWaitHandle processWaitHandle = null;
					try
					{
						processWaitHandle = new ProcessWaitHandle(this.m_processHandle);
						if (processWaitHandle.WaitOne(0, false))
						{
							if (this.haveProcessId)
							{
								throw new InvalidOperationException(SR.GetString("Cannot process request because the process ({0}) has exited.", new object[] { this.processId.ToString(CultureInfo.CurrentCulture) }));
							}
							throw new InvalidOperationException(SR.GetString("Cannot process request because the process has exited."));
						}
					}
					finally
					{
						if (processWaitHandle != null)
						{
							processWaitHandle.Close();
						}
					}
				}
				return this.m_processHandle;
			}
			this.EnsureState((Process.State)3);
			SafeProcessHandle invalidHandle = SafeProcessHandle.InvalidHandle;
			IntPtr currentProcess = NativeMethods.GetCurrentProcess();
			if (!NativeMethods.DuplicateHandle(new HandleRef(this, currentProcess), new HandleRef(this, currentProcess), new HandleRef(this, currentProcess), out invalidHandle, 0, false, 3))
			{
				throw new Win32Exception();
			}
			if (throwIfExited && (access & 1024) != 0 && NativeMethods.GetExitCodeProcess(invalidHandle, out this.exitCode) && this.exitCode != 259)
			{
				throw new InvalidOperationException(SR.GetString("Cannot process request because the process ({0}) has exited.", new object[] { this.processId.ToString(CultureInfo.CurrentCulture) }));
			}
			return invalidHandle;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0002EDC4 File Offset: 0x0002CFC4
		private SafeProcessHandle GetProcessHandle(int access)
		{
			return this.GetProcessHandle(access, true);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0002EDCE File Offset: 0x0002CFCE
		private SafeProcessHandle OpenProcessHandle(int access)
		{
			if (!this.haveProcessHandle)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.SetProcessHandle(this.GetProcessHandle(access));
			}
			return this.m_processHandle;
		}

		/// <summary>Discards any information about the associated process that has been cached inside the process component.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008C5 RID: 2245 RVA: 0x0002EE04 File Offset: 0x0002D004
		public void Refresh()
		{
			this.threads = null;
			this.modules = null;
			this.exited = false;
			this.signaled = false;
			this.haveWorkingSetLimits = false;
			this.havePriorityClass = false;
			this.haveExitTime = false;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002EE37 File Offset: 0x0002D037
		private void SetProcessHandle(SafeProcessHandle processHandle)
		{
			this.m_processHandle = processHandle;
			this.haveProcessHandle = true;
			if (this.watchForExit)
			{
				this.EnsureWatchingForExit();
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002EE55 File Offset: 0x0002D055
		private void SetProcessId(int processId)
		{
			this.processId = processId;
			this.haveProcessId = true;
		}

		/// <summary>Starts (or reuses) the process resource that is specified by the <see cref="P:System.Diagnostics.Process.StartInfo" /> property of this <see cref="T:System.Diagnostics.Process" /> component and associates it with the component.</summary>
		/// <returns>true if a process resource is started; false if no new process resource is started (for example, if an existing process is reused).</returns>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified in the <see cref="T:System.Diagnostics.Process" /> component's <see cref="P:System.Diagnostics.Process.StartInfo" />.-or- The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> member of the <see cref="P:System.Diagnostics.Process.StartInfo" /> property is true while <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" />, <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" />, or <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> is true. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">There was an error in opening the associated file. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008C8 RID: 2248 RVA: 0x0002EE68 File Offset: 0x0002D068
		public bool Start()
		{
			this.Close();
			ProcessStartInfo processStartInfo = this.StartInfo;
			if (processStartInfo.FileName.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("Cannot start process because a file name has not been provided."));
			}
			if (processStartInfo.UseShellExecute)
			{
				return this.StartWithShellExecuteEx(processStartInfo);
			}
			return this.StartWithCreateProcess(processStartInfo);
		}

		/// <summary>Starts the process resource that is specified by the parameter containing process start information (for example, the file name of the process to start) and associates the resource with a new <see cref="T:System.Diagnostics.Process" /> component.</summary>
		/// <returns>A new <see cref="T:System.Diagnostics.Process" /> component that is associated with the process resource, or null if no process resource is started (for example, if an existing process is reused).</returns>
		/// <param name="startInfo">The <see cref="T:System.Diagnostics.ProcessStartInfo" /> that contains the information that is used to start the process, including the file name and any command-line arguments. </param>
		/// <exception cref="T:System.InvalidOperationException">No file name was specified in the <paramref name="startInfo" /> parameter's <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property.-or- The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property of the <paramref name="startInfo" /> parameter is true and the <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" />, <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" />, or <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> property is also true.-or-The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> property of the <paramref name="startInfo" /> parameter is true and the <see cref="P:System.Diagnostics.ProcessStartInfo.UserName" /> property is not null or empty or the <see cref="P:System.Diagnostics.ProcessStartInfo.Password" /> property is not null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="startInfo" /> parameter is null. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The process object has already been disposed. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in the <paramref name="startInfo" /> parameter's <see cref="P:System.Diagnostics.ProcessStartInfo.FileName" /> property could not be found.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">An error occurred when opening the associated file. -or-The sum of the length of the arguments and the length of the full path to the process exceeds 2080. The error message associated with this exception can be one of the following: "The data area passed to a system call is too small." or "Access is denied."</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008C9 RID: 2249 RVA: 0x0002EEB8 File Offset: 0x0002D0B8
		public static Process Start(ProcessStartInfo startInfo)
		{
			Process process = new Process();
			if (startInfo == null)
			{
				throw new ArgumentNullException("startInfo");
			}
			process.StartInfo = startInfo;
			if (process.Start())
			{
				return process;
			}
			return null;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002EEEC File Offset: 0x0002D0EC
		private void StopWatchingForExit()
		{
			if (this.watchingForExit)
			{
				lock (this)
				{
					if (this.watchingForExit)
					{
						this.watchingForExit = false;
						this.registeredWaitHandle.Unregister(null);
						this.waitHandle.Close();
						this.waitHandle = null;
						this.registeredWaitHandle = null;
					}
				}
			}
		}

		/// <summary>Formats the process's name as a string, combined with the parent component type, if applicable.</summary>
		/// <returns>The <see cref="P:System.Diagnostics.Process.ProcessName" />, combined with the base component's <see cref="M:System.Object.ToString" /> return value.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="M:System.Diagnostics.Process.ToString" /> is not supported on Windows 98.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008CB RID: 2251 RVA: 0x0002EF60 File Offset: 0x0002D160
		public override string ToString()
		{
			if (!this.Associated)
			{
				return base.ToString();
			}
			string text = string.Empty;
			try
			{
				text = this.ProcessName;
			}
			catch (PlatformNotSupportedException)
			{
			}
			if (text.Length != 0)
			{
				return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", base.ToString(), text);
			}
			return base.ToString();
		}

		/// <summary>Instructs the <see cref="T:System.Diagnostics.Process" /> component to wait the specified number of milliseconds for the associated process to exit.</summary>
		/// <returns>true if the associated process has exited; otherwise, false.</returns>
		/// <param name="milliseconds">The amount of time, in milliseconds, to wait for the associated process to exit. The maximum is the largest possible value of a 32-bit integer, which represents infinity to the operating system. </param>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The wait setting could not be accessed. </exception>
		/// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.-or- There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.-or- You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit(System.Int32)" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008CC RID: 2252 RVA: 0x0002EFC4 File Offset: 0x0002D1C4
		public bool WaitForExit(int milliseconds)
		{
			SafeProcessHandle safeProcessHandle = null;
			ProcessWaitHandle processWaitHandle = null;
			bool flag;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1048576, false);
				if (safeProcessHandle.IsInvalid)
				{
					flag = true;
				}
				else
				{
					processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
					if (processWaitHandle.WaitOne(milliseconds, false))
					{
						flag = true;
						this.signaled = true;
					}
					else
					{
						flag = false;
						this.signaled = false;
					}
				}
				if (this.output != null && milliseconds == -1)
				{
					this.output.WaitUtilEOF();
				}
				if (this.error != null && milliseconds == -1)
				{
					this.error.WaitUtilEOF();
				}
			}
			finally
			{
				if (processWaitHandle != null)
				{
					processWaitHandle.Close();
				}
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			if (flag && this.watchForExit)
			{
				this.RaiseOnExited();
			}
			return flag;
		}

		/// <summary>Instructs the <see cref="T:System.Diagnostics.Process" /> component to wait indefinitely for the associated process to exit.</summary>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The wait setting could not be accessed. </exception>
		/// <exception cref="T:System.SystemException">No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" /> property can be determined does not exist.-or- There is no process associated with this <see cref="T:System.Diagnostics.Process" /> object.-or- You are attempting to call <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This method is available only for processes that are running on the local computer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060008CD RID: 2253 RVA: 0x0002F078 File Offset: 0x0002D278
		public void WaitForExit()
		{
			this.WaitForExit(-1);
		}

		/// <summary>Begins asynchronous read operations on the redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream of the application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" /> property is false.- or - An asynchronous read operation is already in progress on the <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream.- or - The <see cref="P:System.Diagnostics.Process.StandardOutput" /> stream has been used by a synchronous read operation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008CE RID: 2254 RVA: 0x0002F084 File Offset: 0x0002D284
		[ComVisible(false)]
		public void BeginOutputReadLine()
		{
			if (this.outputStreamReadMode == Process.StreamReadMode.undefined)
			{
				this.outputStreamReadMode = Process.StreamReadMode.asyncMode;
			}
			else if (this.outputStreamReadMode != Process.StreamReadMode.asyncMode)
			{
				throw new InvalidOperationException(SR.GetString("Cannot mix synchronous and asynchronous operation on process stream."));
			}
			if (this.pendingOutputRead)
			{
				throw new InvalidOperationException(SR.GetString("An async read operation has already been started on the stream."));
			}
			this.pendingOutputRead = true;
			if (this.output == null)
			{
				if (this.standardOutput == null)
				{
					throw new InvalidOperationException(SR.GetString("StandardOut has not been redirected or the process hasn't started yet."));
				}
				Stream baseStream = this.standardOutput.BaseStream;
				this.output = new AsyncStreamReader(this, baseStream, new UserCallBack(this.OutputReadNotifyUser), this.standardOutput.CurrentEncoding);
			}
			this.output.BeginReadLine();
		}

		/// <summary>Begins asynchronous read operations on the redirected <see cref="P:System.Diagnostics.Process.StandardError" /> stream of the application.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> property is false.- or - An asynchronous read operation is already in progress on the <see cref="P:System.Diagnostics.Process.StandardError" /> stream.- or - The <see cref="P:System.Diagnostics.Process.StandardError" /> stream has been used by a synchronous read operation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060008CF RID: 2255 RVA: 0x0002F138 File Offset: 0x0002D338
		[ComVisible(false)]
		public void BeginErrorReadLine()
		{
			if (this.errorStreamReadMode == Process.StreamReadMode.undefined)
			{
				this.errorStreamReadMode = Process.StreamReadMode.asyncMode;
			}
			else if (this.errorStreamReadMode != Process.StreamReadMode.asyncMode)
			{
				throw new InvalidOperationException(SR.GetString("Cannot mix synchronous and asynchronous operation on process stream."));
			}
			if (this.pendingErrorRead)
			{
				throw new InvalidOperationException(SR.GetString("An async read operation has already been started on the stream."));
			}
			this.pendingErrorRead = true;
			if (this.error == null)
			{
				if (this.standardError == null)
				{
					throw new InvalidOperationException(SR.GetString("StandardError has not been redirected."));
				}
				Stream baseStream = this.standardError.BaseStream;
				this.error = new AsyncStreamReader(this, baseStream, new UserCallBack(this.ErrorReadNotifyUser), this.standardError.CurrentEncoding);
			}
			this.error.BeginReadLine();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002F1EC File Offset: 0x0002D3EC
		internal void OutputReadNotifyUser(string data)
		{
			DataReceivedEventHandler outputDataReceived = this.OutputDataReceived;
			if (outputDataReceived != null)
			{
				DataReceivedEventArgs dataReceivedEventArgs = new DataReceivedEventArgs(data);
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.Invoke(outputDataReceived, new object[] { this, dataReceivedEventArgs });
					return;
				}
				outputDataReceived(this, dataReceivedEventArgs);
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002F244 File Offset: 0x0002D444
		internal void ErrorReadNotifyUser(string data)
		{
			DataReceivedEventHandler errorDataReceived = this.ErrorDataReceived;
			if (errorDataReceived != null)
			{
				DataReceivedEventArgs dataReceivedEventArgs = new DataReceivedEventArgs(data);
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.Invoke(errorDataReceived, new object[] { this, dataReceivedEventArgs });
					return;
				}
				errorDataReceived(this, dataReceivedEventArgs);
			}
		}

		// Token: 0x060008D2 RID: 2258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ProcessName_icall(IntPtr handle);

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002F29C File Offset: 0x0002D49C
		private static string ProcessName_internal(SafeProcessHandle handle)
		{
			bool flag = false;
			string text;
			try
			{
				handle.DangerousAddRef(ref flag);
				text = Process.ProcessName_icall(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return text;
		}

		/// <summary>Gets the name of the process.</summary>
		/// <returns>The name that the system uses to identify the process to the user.</returns>
		/// <exception cref="T:System.InvalidOperationException">The process does not have an identifier, or no process is associated with the <see cref="T:System.Diagnostics.Process" />.-or- The associated process has exited. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
		/// <exception cref="T:System.NotSupportedException">The process is not on this computer.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("The name of this process.")]
		public string ProcessName
		{
			get
			{
				if (this.process_name == null)
				{
					SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1024);
						this.process_name = Process.ProcessName_internal(safeProcessHandle);
						if (this.process_name == null)
						{
							throw new InvalidOperationException("Process has exited or is inaccessible, so the requested information is not available.");
						}
						if (this.process_name.EndsWith(".exe") || this.process_name.EndsWith(".bat") || this.process_name.EndsWith(".com"))
						{
							this.process_name = this.process_name.Substring(0, this.process_name.Length - 4);
						}
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
				}
				return this.process_name;
			}
		}

		// Token: 0x060008D5 RID: 2261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ShellExecuteEx_internal(ProcessStartInfo startInfo, ref Process.ProcInfo procInfo);

		// Token: 0x060008D6 RID: 2262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateProcess_internal(ProcessStartInfo startInfo, IntPtr stdin, IntPtr stdout, IntPtr stderr, ref Process.ProcInfo procInfo);

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002F398 File Offset: 0x0002D598
		private bool StartWithShellExecuteEx(ProcessStartInfo startInfo)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!string.IsNullOrEmpty(startInfo.UserName) || startInfo.Password != null)
			{
				throw new InvalidOperationException(SR.GetString("The Process object must have the UseShellExecute property set to false in order to start a process as a user."));
			}
			if (startInfo.RedirectStandardInput || startInfo.RedirectStandardOutput || startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException(SR.GetString("The Process object must have the UseShellExecute property set to false in order to redirect IO streams."));
			}
			if (startInfo.StandardErrorEncoding != null)
			{
				throw new InvalidOperationException(SR.GetString("StandardErrorEncoding is only supported when standard error is redirected."));
			}
			if (startInfo.StandardOutputEncoding != null)
			{
				throw new InvalidOperationException(SR.GetString("StandardOutputEncoding is only supported when standard output is redirected."));
			}
			if (startInfo.environmentVariables != null)
			{
				throw new InvalidOperationException(SR.GetString("The Process object must have the UseShellExecute property set to false in order to use environment variables."));
			}
			Process.ProcInfo procInfo = default(Process.ProcInfo);
			Process.FillUserInfo(startInfo, ref procInfo);
			bool flag;
			try
			{
				flag = Process.ShellExecuteEx_internal(startInfo, ref procInfo);
			}
			finally
			{
				if (procInfo.Password != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(procInfo.Password);
				}
				procInfo.Password = IntPtr.Zero;
			}
			if (!flag)
			{
				throw new Win32Exception(-procInfo.pid);
			}
			this.SetProcessHandle(new SafeProcessHandle(procInfo.process_handle, true));
			this.SetProcessId(procInfo.pid);
			return flag;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002F4D8 File Offset: 0x0002D6D8
		private static void CreatePipe(out IntPtr read, out IntPtr write, bool writeDirection)
		{
			MonoIOError monoIOError;
			if (!MonoIO.CreatePipe(out read, out write, out monoIOError))
			{
				throw MonoIO.GetException(monoIOError);
			}
			if (Process.IsWindows)
			{
				IntPtr intPtr = (writeDirection ? write : read);
				if (!MonoIO.DuplicateHandle(Process.GetCurrentProcess().Handle, intPtr, Process.GetCurrentProcess().Handle, out intPtr, 0, 0, 2, out monoIOError))
				{
					throw MonoIO.GetException(monoIOError);
				}
				if (writeDirection)
				{
					if (!MonoIO.Close(write, out monoIOError))
					{
						throw MonoIO.GetException(monoIOError);
					}
					write = intPtr;
					return;
				}
				else
				{
					if (!MonoIO.Close(read, out monoIOError))
					{
						throw MonoIO.GetException(monoIOError);
					}
					read = intPtr;
				}
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0002F560 File Offset: 0x0002D760
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT || platform == PlatformID.WinCE;
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002F58C File Offset: 0x0002D78C
		private bool StartWithCreateProcess(ProcessStartInfo startInfo)
		{
			if (startInfo.StandardOutputEncoding != null && !startInfo.RedirectStandardOutput)
			{
				throw new InvalidOperationException(SR.GetString("StandardOutputEncoding is only supported when standard output is redirected."));
			}
			if (startInfo.StandardErrorEncoding != null && !startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException(SR.GetString("StandardErrorEncoding is only supported when standard error is redirected."));
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			Process.ProcInfo procInfo = default(Process.ProcInfo);
			if (startInfo.HaveEnvVars)
			{
				List<string> list = new List<string>();
				foreach (object obj in startInfo.EnvironmentVariables)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value != null)
					{
						list.Add((string)dictionaryEntry.Key + "=" + (string)dictionaryEntry.Value);
					}
				}
				procInfo.envVariables = list.ToArray();
			}
			if (startInfo.ArgumentList.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in startInfo.ArgumentList)
				{
					PasteArguments.AppendArgument(stringBuilder, text);
				}
				startInfo.Arguments = stringBuilder.ToString();
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			IntPtr intPtr4 = IntPtr.Zero;
			IntPtr intPtr5 = IntPtr.Zero;
			IntPtr intPtr6 = IntPtr.Zero;
			try
			{
				if (startInfo.RedirectStandardInput)
				{
					Process.CreatePipe(out intPtr, out intPtr2, true);
				}
				else
				{
					intPtr = MonoIO.ConsoleInput;
					intPtr2 = IntPtr.Zero;
				}
				if (startInfo.RedirectStandardOutput)
				{
					Process.CreatePipe(out intPtr3, out intPtr4, false);
				}
				else
				{
					intPtr3 = IntPtr.Zero;
					intPtr4 = MonoIO.ConsoleOutput;
				}
				if (startInfo.RedirectStandardError)
				{
					Process.CreatePipe(out intPtr5, out intPtr6, false);
				}
				else
				{
					intPtr5 = IntPtr.Zero;
					intPtr6 = MonoIO.ConsoleError;
				}
				Process.FillUserInfo(startInfo, ref procInfo);
				if (!Process.CreateProcess_internal(startInfo, intPtr, intPtr4, intPtr6, ref procInfo))
				{
					throw new Win32Exception(-procInfo.pid, string.Concat(new string[]
					{
						"ApplicationName='",
						startInfo.FileName,
						"', CommandLine='",
						startInfo.Arguments,
						"', CurrentDirectory='",
						startInfo.WorkingDirectory,
						"', Native error= ",
						Win32Exception.GetErrorMessage(-procInfo.pid)
					}));
				}
			}
			catch
			{
				if (startInfo.RedirectStandardInput)
				{
					if (intPtr != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr, out monoIOError);
					}
					if (intPtr2 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr2, out monoIOError);
					}
				}
				if (startInfo.RedirectStandardOutput)
				{
					if (intPtr3 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr3, out monoIOError);
					}
					if (intPtr4 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr4, out monoIOError);
					}
				}
				if (startInfo.RedirectStandardError)
				{
					if (intPtr5 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr5, out monoIOError);
					}
					if (intPtr6 != IntPtr.Zero)
					{
						MonoIOError monoIOError;
						MonoIO.Close(intPtr6, out monoIOError);
					}
				}
				throw;
			}
			finally
			{
				if (procInfo.Password != IntPtr.Zero)
				{
					Marshal.ZeroFreeBSTR(procInfo.Password);
					procInfo.Password = IntPtr.Zero;
				}
			}
			this.SetProcessHandle(new SafeProcessHandle(procInfo.process_handle, true));
			this.SetProcessId(procInfo.pid);
			if (startInfo.RedirectStandardInput)
			{
				MonoIOError monoIOError;
				MonoIO.Close(intPtr, out monoIOError);
				Encoding encoding = startInfo.StandardInputEncoding ?? Console.InputEncoding;
				this.standardInput = new StreamWriter(new FileStream(intPtr2, FileAccess.Write, true, 8192), encoding)
				{
					AutoFlush = true
				};
			}
			if (startInfo.RedirectStandardOutput)
			{
				MonoIOError monoIOError;
				MonoIO.Close(intPtr4, out monoIOError);
				Encoding encoding2 = startInfo.StandardOutputEncoding ?? Console.OutputEncoding;
				this.standardOutput = new StreamReader(new FileStream(intPtr3, FileAccess.Read, true, 8192), encoding2, true);
			}
			if (startInfo.RedirectStandardError)
			{
				MonoIOError monoIOError;
				MonoIO.Close(intPtr6, out monoIOError);
				Encoding encoding3 = startInfo.StandardErrorEncoding ?? Console.OutputEncoding;
				this.standardError = new StreamReader(new FileStream(intPtr5, FileAccess.Read, true, 8192), encoding3, true);
			}
			return true;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002FA04 File Offset: 0x0002DC04
		private static void FillUserInfo(ProcessStartInfo startInfo, ref Process.ProcInfo procInfo)
		{
			if (startInfo.UserName.Length != 0)
			{
				procInfo.UserName = startInfo.UserName;
				procInfo.Domain = startInfo.Domain;
				if (startInfo.Password != null)
				{
					procInfo.Password = Marshal.SecureStringToBSTR(startInfo.Password);
				}
				else
				{
					procInfo.Password = IntPtr.Zero;
				}
				procInfo.LoadUserProfile = startInfo.LoadUserProfile;
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002FA68 File Offset: 0x0002DC68
		private void RaiseOnExited()
		{
			if (!this.watchForExit)
			{
				return;
			}
			if (!this.raisedOnExited)
			{
				lock (this)
				{
					if (!this.raisedOnExited)
					{
						this.raisedOnExited = true;
						this.OnExited();
					}
				}
			}
		}

		// Token: 0x04000698 RID: 1688
		private bool haveProcessId;

		// Token: 0x04000699 RID: 1689
		private int processId;

		// Token: 0x0400069A RID: 1690
		private bool haveProcessHandle;

		// Token: 0x0400069B RID: 1691
		private SafeProcessHandle m_processHandle;

		// Token: 0x0400069C RID: 1692
		private bool isRemoteMachine;

		// Token: 0x0400069D RID: 1693
		private string machineName;

		// Token: 0x0400069E RID: 1694
		private int m_processAccess;

		// Token: 0x0400069F RID: 1695
		private ProcessThreadCollection threads;

		// Token: 0x040006A0 RID: 1696
		private ProcessModuleCollection modules;

		// Token: 0x040006A1 RID: 1697
		private bool haveWorkingSetLimits;

		// Token: 0x040006A2 RID: 1698
		private bool havePriorityClass;

		// Token: 0x040006A3 RID: 1699
		private ProcessStartInfo startInfo;

		// Token: 0x040006A4 RID: 1700
		private bool watchForExit;

		// Token: 0x040006A5 RID: 1701
		private bool watchingForExit;

		// Token: 0x040006A6 RID: 1702
		private EventHandler onExited;

		// Token: 0x040006A7 RID: 1703
		private bool exited;

		// Token: 0x040006A8 RID: 1704
		private int exitCode;

		// Token: 0x040006A9 RID: 1705
		private bool signaled;

		// Token: 0x040006AA RID: 1706
		private bool haveExitTime;

		// Token: 0x040006AB RID: 1707
		private bool raisedOnExited;

		// Token: 0x040006AC RID: 1708
		private RegisteredWaitHandle registeredWaitHandle;

		// Token: 0x040006AD RID: 1709
		private WaitHandle waitHandle;

		// Token: 0x040006AE RID: 1710
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x040006AF RID: 1711
		private StreamReader standardOutput;

		// Token: 0x040006B0 RID: 1712
		private StreamWriter standardInput;

		// Token: 0x040006B1 RID: 1713
		private StreamReader standardError;

		// Token: 0x040006B2 RID: 1714
		private bool disposed;

		// Token: 0x040006B3 RID: 1715
		private Process.StreamReadMode outputStreamReadMode;

		// Token: 0x040006B4 RID: 1716
		private Process.StreamReadMode errorStreamReadMode;

		// Token: 0x040006B5 RID: 1717
		private Process.StreamReadMode inputStreamReadMode;

		// Token: 0x040006B8 RID: 1720
		internal AsyncStreamReader output;

		// Token: 0x040006B9 RID: 1721
		internal AsyncStreamReader error;

		// Token: 0x040006BA RID: 1722
		internal bool pendingOutputRead;

		// Token: 0x040006BB RID: 1723
		internal bool pendingErrorRead;

		// Token: 0x040006BC RID: 1724
		private string process_name;

		// Token: 0x02000172 RID: 370
		private enum StreamReadMode
		{
			// Token: 0x040006BE RID: 1726
			undefined,
			// Token: 0x040006BF RID: 1727
			syncMode,
			// Token: 0x040006C0 RID: 1728
			asyncMode
		}

		// Token: 0x02000173 RID: 371
		private enum State
		{
			// Token: 0x040006C2 RID: 1730
			HaveId = 1,
			// Token: 0x040006C3 RID: 1731
			IsLocal,
			// Token: 0x040006C4 RID: 1732
			IsNt = 4,
			// Token: 0x040006C5 RID: 1733
			HaveProcessInfo = 8,
			// Token: 0x040006C6 RID: 1734
			Exited = 16,
			// Token: 0x040006C7 RID: 1735
			Associated = 32,
			// Token: 0x040006C8 RID: 1736
			IsWin2k = 64,
			// Token: 0x040006C9 RID: 1737
			HaveNtProcessInfo = 12
		}

		// Token: 0x02000174 RID: 372
		private struct ProcInfo
		{
			// Token: 0x040006CA RID: 1738
			public IntPtr process_handle;

			// Token: 0x040006CB RID: 1739
			public int pid;

			// Token: 0x040006CC RID: 1740
			public string[] envVariables;

			// Token: 0x040006CD RID: 1741
			public string UserName;

			// Token: 0x040006CE RID: 1742
			public string Domain;

			// Token: 0x040006CF RID: 1743
			public IntPtr Password;

			// Token: 0x040006D0 RID: 1744
			public bool LoadUserProfile;
		}
	}
}
