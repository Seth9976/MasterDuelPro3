using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using AOT;
using Unity.Burst.LowLevel;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.Burst
{
	// Token: 0x0200000A RID: 10
	public static class BurstCompiler
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000021D9 File Offset: 0x000003D9
		public static bool IsLoadAdditionalLibrarySupported()
		{
			return BurstCompiler.IsApiAvailable("LoadBurstLibrary");
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000021E5 File Offset: 0x000003E5
		private static BurstCompiler.CommandBuilder BeginCompilerCommand(string cmd)
		{
			if (BurstCompiler._cmdBuilder == null)
			{
				BurstCompiler._cmdBuilder = new BurstCompiler.CommandBuilder();
			}
			return BurstCompiler._cmdBuilder.Begin(cmd);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002203 File Offset: 0x00000403
		public static bool IsEnabled
		{
			get
			{
				return BurstCompiler._IsEnabled && BurstCompiler.BurstCompilerHelper.IsBurstGenerated;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002213 File Offset: 0x00000413
		public static void SetExecutionMode(BurstExecutionEnvironment mode)
		{
			BurstCompilerService.SetCurrentExecutionMode((uint)mode);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000221B File Offset: 0x0000041B
		public static BurstExecutionEnvironment GetExecutionMode()
		{
			return (BurstExecutionEnvironment)BurstCompilerService.GetCurrentExecutionMode();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002222 File Offset: 0x00000422
		internal static T CompileDelegate<T>(T delegateMethod) where T : class
		{
			return (T)((object)Marshal.GetDelegateForFunctionPointer((IntPtr)BurstCompiler.Compile(delegateMethod, false), delegateMethod.GetType()));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000224A File Offset: 0x0000044A
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void VerifyDelegateIsNotMulticast<T>(T delegateMethod) where T : class
		{
			if ((delegateMethod as Delegate).GetInvocationList().Length > 1)
			{
				throw new InvalidOperationException(string.Format("Burst does not support multicast delegates, please use a regular delegate for `{0}'", delegateMethod));
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002278 File Offset: 0x00000478
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void VerifyDelegateHasCorrectUnmanagedFunctionPointerAttribute<T>(T delegateMethod) where T : class
		{
			UnmanagedFunctionPointerAttribute attrib = delegateMethod.GetType().GetCustomAttribute<UnmanagedFunctionPointerAttribute>();
			if (attrib == null || attrib.CallingConvention != CallingConvention.Cdecl)
			{
				global::UnityEngine.Debug.LogWarning("The delegate type " + delegateMethod.GetType().FullName + " should be decorated with [UnmanagedFunctionPointer(CallingConvention.Cdecl)] to ensure runtime interoperabilty between managed code and Burst-compiled code.");
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022C6 File Offset: 0x000004C6
		[Obsolete("This method will be removed in a future version of Burst")]
		public static IntPtr CompileILPPMethod(RuntimeMethodHandle burstMethodHandle, RuntimeMethodHandle managedMethodHandle, RuntimeTypeHandle delegateTypeHandle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022D0 File Offset: 0x000004D0
		public static IntPtr CompileILPPMethod2(RuntimeMethodHandle burstMethodHandle)
		{
			if (burstMethodHandle.Value == IntPtr.Zero)
			{
				throw new ArgumentNullException("burstMethodHandle");
			}
			Action onCompileILPPMethod = BurstCompiler.OnCompileILPPMethod2;
			if (onCompileILPPMethod != null)
			{
				onCompileILPPMethod();
			}
			MethodInfo burstMethod = (MethodInfo)MethodBase.GetMethodFromHandle(burstMethodHandle);
			return (IntPtr)BurstCompiler.Compile(new BurstCompiler.FakeDelegate(burstMethod), burstMethod, true, true);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000022C6 File Offset: 0x000004C6
		[Obsolete("This method will be removed in a future version of Burst")]
		public unsafe static void* GetILPPMethodFunctionPointer(IntPtr ilppMethod)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000232C File Offset: 0x0000052C
		public unsafe static void* GetILPPMethodFunctionPointer2(IntPtr ilppMethod, RuntimeMethodHandle managedMethodHandle, RuntimeTypeHandle delegateTypeHandle)
		{
			if (ilppMethod == IntPtr.Zero)
			{
				throw new ArgumentNullException("ilppMethod");
			}
			if (managedMethodHandle.Value == IntPtr.Zero)
			{
				throw new ArgumentNullException("managedMethodHandle");
			}
			if (delegateTypeHandle.Value == IntPtr.Zero)
			{
				throw new ArgumentNullException("delegateTypeHandle");
			}
			return ilppMethod.ToPointer();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022C6 File Offset: 0x000004C6
		[Obsolete("This method will be removed in a future version of Burst")]
		public unsafe static void* CompileUnsafeStaticMethod(RuntimeMethodHandle handle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002394 File Offset: 0x00000594
		public static FunctionPointer<T> CompileFunctionPointer<T>(T delegateMethod) where T : class
		{
			return new FunctionPointer<T>(new IntPtr(BurstCompiler.Compile(delegateMethod, true)));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023AC File Offset: 0x000005AC
		private unsafe static void* Compile(object delegateObj, bool isFunctionPointer)
		{
			if (!(delegateObj is Delegate))
			{
				throw new ArgumentException("object instance must be a System.Delegate", "delegateObj");
			}
			Delegate delegateMethod = (Delegate)delegateObj;
			return BurstCompiler.Compile(delegateMethod, delegateMethod.Method, isFunctionPointer, false);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023E8 File Offset: 0x000005E8
		private unsafe static void* Compile(object delegateObj, MethodInfo methodInfo, bool isFunctionPointer, bool isILPostProcessing)
		{
			if (delegateObj == null)
			{
				throw new ArgumentNullException("delegateObj");
			}
			if (delegateObj.GetType().IsGenericType)
			{
				throw new InvalidOperationException(string.Format("The delegate type `{0}` must be a non-generic type", delegateObj.GetType()));
			}
			if (!methodInfo.IsStatic)
			{
				throw new InvalidOperationException(string.Format("The method `{0}` must be static. Instance methods are not supported", methodInfo));
			}
			if (methodInfo.IsGenericMethod)
			{
				throw new InvalidOperationException(string.Format("The method `{0}` must be a non-generic method", methodInfo));
			}
			Delegate managedFallbackDelegateMethod = null;
			if (!isILPostProcessing)
			{
				managedFallbackDelegateMethod = delegateObj as Delegate;
			}
			if (!BurstCompilerOptions.HasBurstCompileAttribute(methodInfo))
			{
				throw new InvalidOperationException(string.Format("Burst cannot compile the function pointer `{0}` because the `[BurstCompile]` attribute is missing", methodInfo));
			}
			void* function;
			if (BurstCompiler.Options.EnableBurstCompilation && BurstCompiler.BurstCompilerHelper.IsBurstGenerated)
			{
				function = BurstCompilerService.GetAsyncCompiledAsyncDelegateMethod(BurstCompilerService.CompileAsyncDelegateMethod(delegateObj, string.Empty));
			}
			else
			{
				if (isILPostProcessing)
				{
					return null;
				}
				GCHandle.Alloc(managedFallbackDelegateMethod);
				function = (void*)Marshal.GetFunctionPointerForDelegate(managedFallbackDelegateMethod);
			}
			if (function == null)
			{
				throw new InvalidOperationException(string.Format("Burst failed to compile the function pointer `{0}`", methodInfo));
			}
			return function;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void Shutdown()
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void Cancel()
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024D7 File Offset: 0x000006D7
		internal static bool IsCurrentCompilationDone()
		{
			return true;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void Enable()
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void Disable()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024DA File Offset: 0x000006DA
		internal static bool IsHostEditorArm()
		{
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024E0 File Offset: 0x000006E0
		internal static void TriggerUnsafeStaticMethodRecompilation()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				foreach (Attribute attribute in from x in assemblies[i].GetCustomAttributes()
					where x.GetType().FullName == "Unity.Burst.BurstCompiler+StaticTypeReinitAttribute"
					select x)
				{
					(attribute as BurstCompiler.StaticTypeReinitAttribute).reinitType.GetMethod("Constructor", BindingFlags.Static | BindingFlags.Public).Invoke(null, new object[0]);
				}
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void TriggerRecompilation()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002588 File Offset: 0x00000788
		internal static void UnloadAdditionalLibraries()
		{
			BurstCompiler.SendCommandToCompiler("$unload_burst_natives", null);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002596 File Offset: 0x00000796
		internal static void InitialiseDebuggerHooks()
		{
			if (BurstCompiler.IsApiAvailable("BurstManagedDebuggerPluginV1") && string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BURST_DISABLE_DEBUGGER_HOOKS")))
			{
				BurstCompiler.SendCommandToCompiler(BurstCompiler.SendCommandToCompiler("$request_debug_command", null), null);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000025C7 File Offset: 0x000007C7
		internal static bool IsApiAvailable(string apiName)
		{
			return BurstCompiler.SendCommandToCompiler("$is_native_api_available", apiName) == "True";
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000025E0 File Offset: 0x000007E0
		internal static int RequestSetProtocolVersion(int version)
		{
			string editorVersion = BurstCompiler.SendCommandToCompiler("$request_set_protocol_version_editor", string.Format("{0}", version));
			int result;
			if (string.IsNullOrEmpty(editorVersion) || !int.TryParse(editorVersion, out result))
			{
				result = 0;
			}
			BurstCompiler.SendCommandToCompiler("$set_protocol_version_burst", string.Format("{0}", result));
			return result;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void Initialize(string[] assemblyFolders, string[] ignoreAssemblies)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void NotifyCompilationStarted(string[] assemblyFolders, string[] ignoreAssemblies)
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void NotifyAssemblyCompilationNotRequired(string assemblyName)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void NotifyAssemblyCompilationFinished(string assemblyName, string[] defines)
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void NotifyCompilationFinished()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002638 File Offset: 0x00000838
		internal static string AotCompilation(string[] assemblyFolders, string[] assemblyRoots, string options)
		{
			return "failed";
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000024D5 File Offset: 0x000006D5
		internal static void SetProfilerCallbacks()
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002640 File Offset: 0x00000840
		private static string SendRawCommandToCompiler(string command)
		{
			string results = BurstCompilerService.GetDisassembly(BurstCompiler.DummyMethodInfo, command);
			if (!string.IsNullOrEmpty(results))
			{
				return results.TrimStart('\n');
			}
			return "";
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000266F File Offset: 0x0000086F
		private static string SendCommandToCompiler(string commandName, string commandArgs = null)
		{
			if (commandName == null)
			{
				throw new ArgumentNullException("commandName");
			}
			if (commandArgs == null)
			{
				return BurstCompiler.SendRawCommandToCompiler(commandName);
			}
			return BurstCompiler.BeginCompilerCommand(commandName).With(commandArgs).SendToCompiler();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000024D5 File Offset: 0x000006D5
		private static void DummyMethod()
		{
		}

		// Token: 0x0400001E RID: 30
		[ThreadStatic]
		private static BurstCompiler.CommandBuilder _cmdBuilder;

		// Token: 0x0400001F RID: 31
		internal static bool _IsEnabled;

		// Token: 0x04000020 RID: 32
		public static readonly BurstCompilerOptions Options = new BurstCompilerOptions(true);

		// Token: 0x04000021 RID: 33
		internal static Action OnCompileILPPMethod2;

		// Token: 0x04000022 RID: 34
		private static readonly MethodInfo DummyMethodInfo = typeof(BurstCompiler).GetMethod("DummyMethod", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x0200000B RID: 11
		private class CommandBuilder
		{
			// Token: 0x0600003F RID: 63 RVA: 0x000026C2 File Offset: 0x000008C2
			public CommandBuilder()
			{
				this._builder = new StringBuilder();
				this._hasArgs = false;
			}

			// Token: 0x06000040 RID: 64 RVA: 0x000026DC File Offset: 0x000008DC
			public BurstCompiler.CommandBuilder Begin(string cmd)
			{
				this._builder.Clear();
				this._hasArgs = false;
				this._builder.Append(cmd);
				return this;
			}

			// Token: 0x06000041 RID: 65 RVA: 0x000026FF File Offset: 0x000008FF
			public BurstCompiler.CommandBuilder With(string arg)
			{
				if (!this._hasArgs)
				{
					this._builder.Append(' ');
				}
				this._hasArgs = true;
				this._builder.Append(arg);
				return this;
			}

			// Token: 0x06000042 RID: 66 RVA: 0x0000272C File Offset: 0x0000092C
			public BurstCompiler.CommandBuilder With(IntPtr arg)
			{
				if (!this._hasArgs)
				{
					this._builder.Append(' ');
				}
				this._hasArgs = true;
				this._builder.AppendFormat("0x{0:X16}", arg.ToInt64());
				return this;
			}

			// Token: 0x06000043 RID: 67 RVA: 0x00002769 File Offset: 0x00000969
			public BurstCompiler.CommandBuilder And(char sep = '|')
			{
				this._builder.Append(sep);
				return this;
			}

			// Token: 0x06000044 RID: 68 RVA: 0x00002779 File Offset: 0x00000979
			public string SendToCompiler()
			{
				return BurstCompiler.SendRawCommandToCompiler(this._builder.ToString());
			}

			// Token: 0x04000023 RID: 35
			private StringBuilder _builder;

			// Token: 0x04000024 RID: 36
			private bool _hasArgs;
		}

		// Token: 0x0200000C RID: 12
		[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
		internal class StaticTypeReinitAttribute : Attribute
		{
			// Token: 0x06000045 RID: 69 RVA: 0x0000278B File Offset: 0x0000098B
			public StaticTypeReinitAttribute(Type toReinit)
			{
				this.reinitType = toReinit;
			}

			// Token: 0x04000025 RID: 37
			public readonly Type reinitType;
		}

		// Token: 0x0200000D RID: 13
		[BurstCompile]
		internal static class BurstCompilerHelper
		{
			// Token: 0x06000046 RID: 70 RVA: 0x0000279C File Offset: 0x0000099C
			[BurstCompile]
			[MonoPInvokeCallback(typeof(BurstCompiler.BurstCompilerHelper.IsBurstEnabledDelegate))]
			private static bool IsBurstEnabled()
			{
				bool result = true;
				BurstCompiler.BurstCompilerHelper.DiscardedMethod(ref result);
				return result;
			}

			// Token: 0x06000047 RID: 71 RVA: 0x000027B3 File Offset: 0x000009B3
			[BurstDiscard]
			private static void DiscardedMethod(ref bool value)
			{
				value = false;
			}

			// Token: 0x06000048 RID: 72 RVA: 0x000027B8 File Offset: 0x000009B8
			private static bool IsCompiledByBurst(Delegate del)
			{
				return BurstCompilerService.GetAsyncCompiledAsyncDelegateMethod(BurstCompilerService.CompileAsyncDelegateMethod(del, string.Empty)) != null;
			}

			// Token: 0x04000026 RID: 38
			private static readonly BurstCompiler.BurstCompilerHelper.IsBurstEnabledDelegate IsBurstEnabledImpl = new BurstCompiler.BurstCompilerHelper.IsBurstEnabledDelegate(BurstCompiler.BurstCompilerHelper.IsBurstEnabled);

			// Token: 0x04000027 RID: 39
			public static readonly bool IsBurstGenerated = BurstCompiler.BurstCompilerHelper.IsCompiledByBurst(BurstCompiler.BurstCompilerHelper.IsBurstEnabledImpl);

			// Token: 0x0200000E RID: 14
			// (Invoke) Token: 0x0600004B RID: 75
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			private delegate bool IsBurstEnabledDelegate();
		}

		// Token: 0x0200000F RID: 15
		private class FakeDelegate
		{
			// Token: 0x0600004E RID: 78 RVA: 0x000027F3 File Offset: 0x000009F3
			public FakeDelegate(MethodInfo method)
			{
				this.Method = method;
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600004F RID: 79 RVA: 0x00002802 File Offset: 0x00000A02
			[Preserve]
			public MethodInfo Method { get; }
		}
	}
}
