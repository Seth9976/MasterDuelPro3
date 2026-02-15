using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Provides static methods and properties to manage an application, such as methods to start and stop an application, to process Windows messages, and properties to get information about an application. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200000C RID: 12
	public sealed class Application
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002101 File Offset: 0x00000301
		static Application()
		{
			Application.InitializeUIAutomation();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000213C File Offset: 0x0000033C
		private static void InitializeUIAutomation()
		{
			Assembly assembly = null;
			try
			{
				assembly = Assembly.Load("UIAutomationWinforms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f4ceacb585d99812");
			}
			catch
			{
			}
			if (assembly == null)
			{
				return;
			}
			try
			{
				Type type = assembly.GetType("Mono.UIAutomation.Winforms.Global", false);
				if (!(type != null))
				{
					throw new Exception(string.Format("Type {0} not found in assembly {1}.", "Mono.UIAutomation.Winforms.Global", "UIAutomationWinforms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f4ceacb585d99812"));
				}
				MethodInfo method = type.GetMethod("Initialize", BindingFlags.Static | BindingFlags.Public);
				if (!(method != null))
				{
					throw new Exception(string.Format("Method {0} not found in type {1}.", "Initialize", "Mono.UIAutomation.Winforms.Global"));
				}
				method.Invoke(null, new object[0]);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Error setting up UIA: " + ex);
			}
		}

		/// <summary>Gets a value indicating whether the caller can quit this application.</summary>
		/// <returns>true if the caller can quit this application; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002210 File Offset: 0x00000410
		public static bool AllowQuit
		{
			get
			{
				return !Application.browser_embedded;
			}
		}

		/// <summary>Gets or sets the culture information for the current thread.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> representing the culture information for the current thread.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000221A File Offset: 0x0000041A
		public static CultureInfo CurrentCulture
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
		}

		/// <summary>Gets a value indicating whether a message loop exists on this thread.</summary>
		/// <returns>true if a message loop exists; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002226 File Offset: 0x00000426
		public static bool MessageLoop
		{
			get
			{
				return Application.MWFThread.Current.MessageLoop;
			}
		}

		/// <summary>Gets a value specifying whether the current application is drawing controls with visual styles.</summary>
		/// <returns>true if visual styles are enabled for controls in the client area of application windows; otherwise, false.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002232 File Offset: 0x00000432
		public static bool RenderWithVisualStyles
		{
			get
			{
				if (VisualStyleInformation.IsSupportedByOS)
				{
					if (!VisualStyleInformation.IsEnabledByUser)
					{
						return false;
					}
					if (!XplatUI.ThemesEnabled)
					{
						return false;
					}
					if (Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled)
					{
						return true;
					}
					if (Application.VisualStyleState == VisualStyleState.ClientAreaEnabled)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets a value that specifies how visual styles are applied to application windows.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002262 File Offset: 0x00000462
		public static VisualStyleState VisualStyleState
		{
			get
			{
				return Application.visual_style_state;
			}
		}

		/// <summary>Processes all Windows messages currently in the message queue.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000018 RID: 24 RVA: 0x00002269 File Offset: 0x00000469
		public static void DoEvents()
		{
			XplatUI.DoEvents();
		}

		/// <summary>Runs any filters against a window message, and returns a copy of the modified message.</summary>
		/// <returns>True if the filters were processed; otherwise, false.</returns>
		/// <param name="message">The Windows event message to filter. </param>
		// Token: 0x06000019 RID: 25 RVA: 0x00002270 File Offset: 0x00000470
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static bool FilterMessage(ref Message message)
		{
			ArrayList arrayList = Application.message_filters;
			lock (arrayList)
			{
				for (int i = 0; i < Application.message_filters.Count; i++)
				{
					if (((IMessageFilter)Application.message_filters[i]).PreFilterMessage(ref message))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Gets a collection of open forms owned by the application.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.FormCollection" /> containing all the currently open forms owned by this application.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022E0 File Offset: 0x000004E0
		public static FormCollection OpenForms
		{
			get
			{
				return Application.forms;
			}
		}

		/// <summary>Informs all message pumps that they must terminate, and then closes all application windows after the messages have been processed.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600001B RID: 27 RVA: 0x000022E7 File Offset: 0x000004E7
		public static void Exit()
		{
			Application.Exit(new CancelEventArgs());
		}

		/// <summary>Informs all message pumps that they must terminate, and then closes all application windows after the messages have been processed.</summary>
		/// <param name="e">Returns whether any <see cref="T:System.Windows.Forms.Form" /> within the application cancelled the exit.</param>
		// Token: 0x0600001C RID: 28 RVA: 0x000022F4 File Offset: 0x000004F4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void Exit(CancelEventArgs e)
		{
			FormCollection formCollection = Application.forms;
			lock (formCollection)
			{
				foreach (object obj in new ArrayList(Application.forms))
				{
					Form form = (Form)obj;
					e.Cancel = form.FireClosingEvents(CloseReason.ApplicationExitCall, false);
					if (e.Cancel)
					{
						return;
					}
					form.suppress_closing_events = true;
					form.Close();
					form.Dispose();
				}
			}
			XplatUI.PostQuitMessage(0);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event. </summary>
		/// <param name="t">An <see cref="T:System.Exception" /> that represents the exception that was thrown. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600001D RID: 29 RVA: 0x000023A4 File Offset: 0x000005A4
		public static void OnThreadException(Exception t)
		{
			if (Application.MWFThread.Current.HandlingException)
			{
				Console.WriteLine(t);
				Environment.Exit(1);
			}
			try
			{
				Application.MWFThread.Current.HandlingException = true;
				if (Application.ThreadException != null)
				{
					Application.ThreadException(null, new ThreadExceptionEventArgs(t));
				}
				else if (SystemInformation.UserInteractive)
				{
					new ThreadExceptionDialog(t).ShowDialog();
				}
				else
				{
					Console.WriteLine(t.ToString());
					Application.Exit();
				}
			}
			finally
			{
				Application.MWFThread.Current.HandlingException = false;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002434 File Offset: 0x00000634
		internal static void FirePreRun()
		{
			EventHandler preRun = Application.PreRun;
			if (preRun != null)
			{
				preRun(null, EventArgs.Empty);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002458 File Offset: 0x00000658
		private static void DisableFormsForModalLoop(Queue toplevels, ApplicationContext context)
		{
			FormCollection formCollection = Application.forms;
			lock (formCollection)
			{
				IEnumerator enumerator = Application.forms.GetEnumerator();
				IL_0085:
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Form form = (Form)obj;
					if (form != context.MainForm)
					{
						Control control = form;
						bool flag2 = false;
						while (control.Parent != context.MainForm)
						{
							control = control.Parent;
							if (control == null)
							{
								IL_0059:
								if (!flag2 && form.IsHandleCreated && XplatUI.IsEnabled(form.Handle))
								{
									XplatUI.EnableWindow(form.Handle, false);
									toplevels.Enqueue(form);
									goto IL_0085;
								}
								goto IL_0085;
							}
						}
						flag2 = true;
						goto IL_0059;
					}
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002510 File Offset: 0x00000710
		private static void EnableFormsForModalLoop(Queue toplevels, ApplicationContext context)
		{
			while (toplevels.Count > 0)
			{
				Form form = (Form)toplevels.Dequeue();
				if (form.IsHandleCreated)
				{
					XplatUI.EnableWindow(form.window.Handle, true);
					context.MainForm = form;
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002554 File Offset: 0x00000754
		internal static void RunLoop(bool Modal, ApplicationContext context)
		{
			Application.MWFThread mwfthread = Application.MWFThread.Current;
			MSG msg = default(MSG);
			if (context == null)
			{
				context = new ApplicationContext();
			}
			ApplicationContext context2 = mwfthread.Context;
			mwfthread.Context = context;
			if (context.MainForm != null)
			{
				context.MainForm.is_modal = Modal;
				context.MainForm.context = context;
				context.MainForm.closing = false;
				context.MainForm.Visible = true;
				if (context.MainForm != null)
				{
					context.MainForm.Activate();
				}
			}
			Queue queue;
			if (Modal)
			{
				queue = new Queue();
				Application.DisableFormsForModalLoop(queue, context);
				if (context.MainForm != null)
				{
					XplatUI.EnableWindow(context.MainForm.Handle, true);
					XplatUI.SetModal(context.MainForm.Handle, true);
				}
			}
			else
			{
				queue = null;
			}
			object obj = XplatUI.StartLoop(Thread.CurrentThread);
			mwfthread.MessageLoop = true;
			bool flag = false;
			while (!flag && XplatUI.GetMessage(obj, ref msg, IntPtr.Zero, 0, 0))
			{
				Message message = Message.Create(msg.hwnd, (int)msg.message, msg.wParam, msg.lParam);
				if (!Application.FilterMessage(ref message))
				{
					Msg message2 = msg.message;
					if (message2 <= Msg.WM_SYSCHAR)
					{
						if (message2 != Msg.WM_QUIT)
						{
							if (message2 - Msg.WM_KEYDOWN > 2 && message2 - Msg.WM_SYSKEYDOWN > 2)
							{
								goto IL_02DA;
							}
							Control control = Control.FromHandle(msg.hwnd);
							if (Application.keyboard_capture != null)
							{
								if (message.Msg == 260 && message.WParam.ToInt32() == 18)
								{
									Application.keyboard_capture.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.Keyboard);
									continue;
								}
								message.HWnd = Application.keyboard_capture.Handle;
								PreProcessControlState preProcessControlState = Application.keyboard_capture.PreProcessControlMessageInternal(ref message);
								if (preProcessControlState == PreProcessControlState.MessageProcessed)
								{
									continue;
								}
								if (preProcessControlState - PreProcessControlState.MessageNeeded <= 1)
								{
									if ((message.Msg != 256 && message.Msg != 258) || Application.keyboard_capture.ProcessControlMnemonic((char)(int)message.WParam) || control == null || !Application.ControlOnToolStrip(control))
									{
										continue;
									}
									message.HWnd = msg.hwnd;
								}
							}
							if (control != null && control.PreProcessControlMessageInternal(ref message) != PreProcessControlState.MessageProcessed)
							{
								goto IL_02DA;
							}
							if (control == null)
							{
								goto IL_02DA;
							}
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						if (message2 != Msg.WM_LBUTTONDOWN && message2 != Msg.WM_RBUTTONDOWN && message2 != Msg.WM_MBUTTONDOWN)
						{
							goto IL_02DA;
						}
						if (Application.keyboard_capture == null)
						{
							goto IL_02DA;
						}
						Control control2 = Control.FromHandle(msg.hwnd);
						if (control2 == null)
						{
							ToolStripManager.FireAppClicked();
							goto IL_02DA;
						}
						if (control2 is ToolStrip)
						{
							if ((control2 as ToolStrip).GetTopLevelToolStrip() != Application.keyboard_capture.GetTopLevelToolStrip())
							{
								ToolStripManager.FireAppClicked();
								goto IL_02DA;
							}
							goto IL_02DA;
						}
						else
						{
							if ((control2.Parent == null || !(control2.Parent is ToolStripDropDownMenu) || (control2.Parent as ToolStripDropDownMenu).GetTopLevelToolStrip() != Application.keyboard_capture.GetTopLevelToolStrip()) && control2.TopLevelControl != null)
							{
								ToolStripManager.FireAppClicked();
								goto IL_02DA;
							}
							goto IL_02DA;
						}
					}
					IL_02EA:
					if ((context.MainForm != null && !context.MainForm.IsHandleCreated) || context.MainForm == null || (!context.MainForm.closing && (!Modal || context.MainForm.Visible)))
					{
						continue;
					}
					if (!Modal)
					{
						XplatUI.PostQuitMessage(0);
						continue;
					}
					break;
					IL_02DA:
					XplatUI.TranslateMessage(ref msg);
					XplatUI.DispatchMessage(ref msg);
					goto IL_02EA;
				}
			}
			mwfthread.MessageLoop = false;
			XplatUI.EndLoop(Thread.CurrentThread);
			if (Modal)
			{
				Form mainForm = context.MainForm;
				context.MainForm = null;
				Application.EnableFormsForModalLoop(queue, context);
				if (mainForm != null && mainForm.IsHandleCreated)
				{
					XplatUI.SetModal(mainForm.Handle, false);
				}
				mainForm.RaiseCloseEvents(true, false);
				mainForm.is_modal = false;
			}
			if (context.MainForm != null)
			{
				context.MainForm.context = null;
				context.MainForm = null;
			}
			mwfthread.Context = context2;
			if (!Modal)
			{
				mwfthread.Exit();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002928 File Offset: 0x00000B28
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000292F File Offset: 0x00000B2F
		internal static ToolStrip KeyboardCapture
		{
			get
			{
				return Application.keyboard_capture;
			}
			set
			{
				Application.keyboard_capture = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002937 File Offset: 0x00000B37
		internal static bool VisualStylesEnabled
		{
			get
			{
				return Application.visual_styles_enabled;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002940 File Offset: 0x00000B40
		internal static void AddForm(Form f)
		{
			FormCollection formCollection = Application.forms;
			lock (formCollection)
			{
				Application.forms.Add(f);
			}
			if (Application.FormAdded != null)
			{
				Application.FormAdded(f, null);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002998 File Offset: 0x00000B98
		internal static void RemoveForm(Form f)
		{
			FormCollection formCollection = Application.forms;
			lock (formCollection)
			{
				Application.forms.Remove(f);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000029DC File Offset: 0x00000BDC
		private static bool ControlOnToolStrip(Control c)
		{
			for (Control control = c.Parent; control != null; control = control.Parent)
			{
				if (control is ToolStrip)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000053 RID: 83
		private static bool browser_embedded;

		// Token: 0x04000054 RID: 84
		private static InputLanguage input_language = InputLanguage.CurrentInputLanguage;

		// Token: 0x04000055 RID: 85
		private static string safe_caption_format = "{1} - {0} - {2}";

		// Token: 0x04000056 RID: 86
		private static readonly ArrayList message_filters = new ArrayList();

		// Token: 0x04000057 RID: 87
		private static readonly FormCollection forms = new FormCollection();

		// Token: 0x04000058 RID: 88
		private static ToolStrip keyboard_capture;

		// Token: 0x04000059 RID: 89
		private static VisualStyleState visual_style_state = VisualStyleState.ClientAndNonClientAreasEnabled;

		// Token: 0x0400005A RID: 90
		private static bool visual_styles_enabled;

		// Token: 0x0400005B RID: 91
		internal static bool use_compatible_text_rendering = true;

		// Token: 0x0400005C RID: 92
		[CompilerGenerated]
		private static EventHandler ApplicationExit;

		// Token: 0x0400005D RID: 93
		[CompilerGenerated]
		private static EventHandler ThreadExit;

		// Token: 0x0400005E RID: 94
		[CompilerGenerated]
		private static ThreadExceptionEventHandler ThreadException;

		// Token: 0x0400005F RID: 95
		[CompilerGenerated]
		private static EventHandler FormAdded;

		// Token: 0x04000060 RID: 96
		[CompilerGenerated]
		private static EventHandler PreRun;

		// Token: 0x0200000D RID: 13
		internal class MWFThread
		{
			// Token: 0x06000028 RID: 40 RVA: 0x00002A07 File Offset: 0x00000C07
			private MWFThread()
			{
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00002A0F File Offset: 0x00000C0F
			// (set) Token: 0x0600002A RID: 42 RVA: 0x00002A17 File Offset: 0x00000C17
			public ApplicationContext Context
			{
				get
				{
					return this.context;
				}
				set
				{
					this.context = value;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002A20 File Offset: 0x00000C20
			// (set) Token: 0x0600002C RID: 44 RVA: 0x00002A28 File Offset: 0x00000C28
			public bool MessageLoop
			{
				get
				{
					return this.messageloop_started;
				}
				set
				{
					this.messageloop_started = value;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600002D RID: 45 RVA: 0x00002A31 File Offset: 0x00000C31
			// (set) Token: 0x0600002E RID: 46 RVA: 0x00002A39 File Offset: 0x00000C39
			public bool HandlingException
			{
				get
				{
					return this.handling_exception;
				}
				set
				{
					this.handling_exception = value;
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x0600002F RID: 47 RVA: 0x00002A44 File Offset: 0x00000C44
			public static int LoopCount
			{
				get
				{
					Hashtable hashtable = Application.MWFThread.threads;
					int num2;
					lock (hashtable)
					{
						int num = 0;
						using (IEnumerator enumerator = Application.MWFThread.threads.Values.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (((Application.MWFThread)enumerator.Current).messageloop_started)
								{
									num++;
								}
							}
						}
						num2 = num;
					}
					return num2;
				}
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000030 RID: 48 RVA: 0x00002AD8 File Offset: 0x00000CD8
			public static Application.MWFThread Current
			{
				get
				{
					Application.MWFThread mwfthread = null;
					Hashtable hashtable = Application.MWFThread.threads;
					lock (hashtable)
					{
						mwfthread = (Application.MWFThread)Application.MWFThread.threads[Thread.CurrentThread.GetHashCode()];
						if (mwfthread == null)
						{
							mwfthread = new Application.MWFThread();
							mwfthread.thread_id = Thread.CurrentThread.GetHashCode();
							Application.MWFThread.threads[mwfthread.thread_id] = mwfthread;
						}
					}
					return mwfthread;
				}
			}

			// Token: 0x06000031 RID: 49 RVA: 0x00002B64 File Offset: 0x00000D64
			public void Exit()
			{
				if (this.context != null)
				{
					this.context.ExitThread();
				}
				this.context = null;
				if (Application.ThreadExit != null)
				{
					Application.ThreadExit(null, EventArgs.Empty);
				}
				if (Application.MWFThread.LoopCount == 0 && Application.ApplicationExit != null)
				{
					Application.ApplicationExit(null, EventArgs.Empty);
				}
				((Application.MWFThread)Application.MWFThread.threads[this.thread_id]).MessageLoop = false;
			}

			// Token: 0x04000061 RID: 97
			private ApplicationContext context;

			// Token: 0x04000062 RID: 98
			private bool messageloop_started;

			// Token: 0x04000063 RID: 99
			private bool handling_exception;

			// Token: 0x04000064 RID: 100
			private int thread_id;

			// Token: 0x04000065 RID: 101
			private static readonly Hashtable threads = new Hashtable();
		}
	}
}
