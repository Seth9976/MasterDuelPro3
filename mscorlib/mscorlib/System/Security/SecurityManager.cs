using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace System.Security
{
	/// <summary>Provides the main access point for classes interacting with the security system. This class cannot be inherited.</summary>
	// Token: 0x02000329 RID: 809
	[ComVisible(true)]
	public static class SecurityManager
	{
		/// <summary>Gets or sets a value indicating whether code must have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> in order to execute.</summary>
		/// <returns>true if code must have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> in order to execute; otherwise, false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x00033991 File Offset: 0x00031B91
		[Obsolete]
		public static bool CheckExecutionRights
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets a value indicating whether security is enabled.</summary>
		/// <returns>true if security is enabled; otherwise, false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />. </exception>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001CE0 RID: 7392
		[Obsolete("The security manager cannot be turned off on MS runtime")]
		public static extern bool SecurityEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/// <summary>Determines whether a permission is granted to the caller.</summary>
		/// <returns>true if the permissions granted to the caller include the permission <paramref name="perm" />; otherwise, false.</returns>
		/// <param name="perm">The permission to test against the grant of the caller. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001CE1 RID: 7393 RVA: 0x00070EBF File Offset: 0x0006F0BF
		[Obsolete]
		public static bool IsGranted(IPermission perm)
		{
			return perm == null || !SecurityManager.SecurityEnabled || SecurityManager.IsGranted(Assembly.GetCallingAssembly(), perm);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x00070EDC File Offset: 0x0006F0DC
		internal static bool IsGranted(Assembly a, IPermission perm)
		{
			PermissionSet grantedPermissionSet = a.GrantedPermissionSet;
			if (grantedPermissionSet != null && !grantedPermissionSet.IsUnrestricted())
			{
				CodeAccessPermission codeAccessPermission = (CodeAccessPermission)grantedPermissionSet.GetPermission(perm.GetType());
				if (!perm.IsSubsetOf(codeAccessPermission))
				{
					return false;
				}
			}
			PermissionSet deniedPermissionSet = a.DeniedPermissionSet;
			if (deniedPermissionSet != null && !deniedPermissionSet.IsEmpty())
			{
				if (deniedPermissionSet.IsUnrestricted())
				{
					return false;
				}
				CodeAccessPermission codeAccessPermission2 = (CodeAccessPermission)a.DeniedPermissionSet.GetPermission(perm.GetType());
				if (codeAccessPermission2 != null && perm.IsSubsetOf(codeAccessPermission2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence.</summary>
		/// <returns>The set of permissions that can be granted by the security system.</returns>
		/// <param name="evidence">The evidence set used to evaluate policy. </param>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.&lt;NetFx40_LegacySecurityPolicy&gt; Element.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001CE3 RID: 7395 RVA: 0x00070F5C File Offset: 0x0006F15C
		[Obsolete]
		public static PermissionSet ResolvePolicy(Evidence evidence)
		{
			if (evidence == null)
			{
				return new PermissionSet(PermissionState.None);
			}
			PermissionSet permissionSet = null;
			IEnumerator hierarchy = SecurityManager.Hierarchy;
			while (hierarchy.MoveNext())
			{
				object obj = hierarchy.Current;
				PolicyLevel policyLevel = (PolicyLevel)obj;
				if (SecurityManager.ResolvePolicyLevel(ref permissionSet, policyLevel, evidence))
				{
					break;
				}
			}
			SecurityManager.ResolveIdentityPermissions(permissionSet, evidence);
			return permissionSet;
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence and requests.</summary>
		/// <returns>The set of permissions that would be granted by the security system.</returns>
		/// <param name="evidence">The evidence set used to evaluate policy. </param>
		/// <param name="reqdPset">The required permissions the code needs to run. </param>
		/// <param name="optPset">The optional permissions that will be used if granted, but aren't required for the code to run. </param>
		/// <param name="denyPset">The denied permissions that must never be granted to the code even if policy otherwise permits it. </param>
		/// <param name="denied">An output parameter that contains the set of permissions not granted. </param>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.&lt;NetFx40_LegacySecurityPolicy&gt; Element.</exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">Policy fails to grant the minimum required permissions specified by the <paramref name="reqdPset" /> parameter. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001CE4 RID: 7396 RVA: 0x00070FA4 File Offset: 0x0006F1A4
		[Obsolete]
		public static PermissionSet ResolvePolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied)
		{
			PermissionSet permissionSet = SecurityManager.ResolvePolicy(evidence);
			if (reqdPset != null && !reqdPset.IsSubsetOf(permissionSet))
			{
				throw new PolicyException(Locale.GetText("Policy doesn't grant the minimal permissions required to execute the assembly."));
			}
			if (SecurityManager.CheckExecutionRights)
			{
				bool flag = false;
				if (permissionSet != null)
				{
					if (permissionSet.IsUnrestricted())
					{
						flag = true;
					}
					else
					{
						IPermission permission = permissionSet.GetPermission(typeof(SecurityPermission));
						flag = SecurityManager._execution.IsSubsetOf(permission);
					}
				}
				if (!flag)
				{
					throw new PolicyException(Locale.GetText("Policy doesn't grant the right to execute the assembly."));
				}
			}
			denied = denyPset;
			return permissionSet;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x00071024 File Offset: 0x0006F224
		private static IEnumerator Hierarchy
		{
			get
			{
				object lockObject = SecurityManager._lockObject;
				lock (lockObject)
				{
					if (SecurityManager._hierarchy == null)
					{
						SecurityManager.InitializePolicyHierarchy();
					}
				}
				return SecurityManager._hierarchy.GetEnumerator();
			}
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00071074 File Offset: 0x0006F274
		private static void InitializePolicyHierarchy()
		{
			string directoryName = Path.GetDirectoryName(Environment.GetMachineConfigPath());
			string text = Path.Combine(Environment.UnixGetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create), "mono");
			PolicyLevel policyLevel = new PolicyLevel("Enterprise", PolicyLevelType.Enterprise);
			SecurityManager._level = policyLevel;
			policyLevel.LoadFromFile(Path.Combine(directoryName, "enterprisesec.config"));
			PolicyLevel policyLevel2 = new PolicyLevel("Machine", PolicyLevelType.Machine);
			SecurityManager._level = policyLevel2;
			policyLevel2.LoadFromFile(Path.Combine(directoryName, "security.config"));
			PolicyLevel policyLevel3 = new PolicyLevel("User", PolicyLevelType.User);
			SecurityManager._level = policyLevel3;
			policyLevel3.LoadFromFile(Path.Combine(text, "security.config"));
			SecurityManager._hierarchy = ArrayList.Synchronized(new ArrayList { policyLevel, policyLevel2, policyLevel3 });
			SecurityManager._level = null;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00071140 File Offset: 0x0006F340
		internal static bool ResolvePolicyLevel(ref PermissionSet ps, PolicyLevel pl, Evidence evidence)
		{
			PolicyStatement policyStatement = pl.Resolve(evidence);
			if (policyStatement != null)
			{
				if (ps == null)
				{
					ps = policyStatement.PermissionSet;
				}
				else
				{
					ps = ps.Intersect(policyStatement.PermissionSet);
					if (ps == null)
					{
						ps = new PermissionSet(PermissionState.None);
					}
				}
				if ((policyStatement.Attributes & PolicyStatementAttribute.LevelFinal) == PolicyStatementAttribute.LevelFinal)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00071190 File Offset: 0x0006F390
		internal static void ResolveIdentityPermissions(PermissionSet ps, Evidence evidence)
		{
			if (ps.IsUnrestricted())
			{
				return;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				IIdentityPermissionFactory identityPermissionFactory = obj as IIdentityPermissionFactory;
				if (identityPermissionFactory != null)
				{
					IPermission permission = identityPermissionFactory.CreateIdentityPermission(evidence);
					ps.AddPermission(permission);
				}
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x000711D6 File Offset: 0x0006F3D6
		// (set) Token: 0x06001CEA RID: 7402 RVA: 0x000711DD File Offset: 0x0006F3DD
		internal static PolicyLevel ResolvingPolicyLevel
		{
			get
			{
				return SecurityManager._level;
			}
			set
			{
				SecurityManager._level = value;
			}
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000711E8 File Offset: 0x0006F3E8
		internal static PermissionSet Decode(byte[] encodedPermissions)
		{
			if (encodedPermissions == null || encodedPermissions.Length < 1)
			{
				throw new SecurityException("Invalid metadata format.");
			}
			byte b = encodedPermissions[0];
			if (b == 46)
			{
				return PermissionSet.CreateFromBinaryFormat(encodedPermissions);
			}
			if (b == 60)
			{
				return new PermissionSet(Encoding.Unicode.GetString(encodedPermissions));
			}
			throw new SecurityException(Locale.GetText("Unknown metadata format."));
		}

		/// <summary>Determines whether the current thread requires a security context capture if its security state has to be re-created at a later point in time.</summary>
		/// <returns>false if the stack contains no partially trusted application domains, no partially trusted assemblies, and no currently active <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> or <see cref="M:System.Security.CodeAccessPermission.Deny" /> modifiers; true if the common language runtime cannot guarantee that the stack contains none of these. </returns>
		// Token: 0x06001CEC RID: 7404 RVA: 0x00033EF4 File Offset: 0x000320F4
		public static bool CurrentThreadRequiresSecurityContextCapture()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000D42 RID: 3394
		private static object _lockObject = new object();

		// Token: 0x04000D43 RID: 3395
		private static ArrayList _hierarchy;

		// Token: 0x04000D44 RID: 3396
		private static PolicyLevel _level;

		// Token: 0x04000D45 RID: 3397
		private static SecurityPermission _execution = new SecurityPermission(SecurityPermissionFlag.Execution);
	}
}
