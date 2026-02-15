using System;
using System.Runtime.ConstrainedExecution;

namespace System.Threading
{
	/// <summary>Provides the functionality that allows a common language runtime host to participate in the flow, or migration, of the execution context.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200027C RID: 636
	public class HostExecutionContextManager
	{
		/// <summary>Captures the host execution context from the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.HostExecutionContext" /> object representing the host execution context of the current thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001795 RID: 6037 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO]
		public virtual HostExecutionContext Capture()
		{
			throw new NotImplementedException();
		}

		/// <summary>Restores the host execution context to its prior state.</summary>
		/// <param name="previousState">The previous context state to revert to.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="previousState" /> is null.-or-<paramref name="previousState" /> was not created on the current thread.-or-<paramref name="previousState" /> is not the last state for the <see cref="T:System.Threading.HostExecutionContext" />.</exception>
		// Token: 0x06001796 RID: 6038 RVA: 0x00033EF4 File Offset: 0x000320F4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MonoTODO]
		public virtual void Revert(object previousState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the current host execution context to the specified host execution context.</summary>
		/// <returns>An object for restoring the <see cref="T:System.Threading.HostExecutionContext" /> to its previous state.</returns>
		/// <param name="hostExecutionContext">The <see cref="T:System.Threading.HostExecutionContext" /> to be set.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="hostExecutionContext" /> was not acquired through a capture operation. -or- <paramref name="hostExecutionContext" /> has been the argument to a previous <see cref="M:System.Threading.HostExecutionContextManager.SetHostExecutionContext(System.Threading.HostExecutionContext)" />  method call.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06001797 RID: 6039 RVA: 0x00033EF4 File Offset: 0x000320F4
		[MonoTODO]
		public virtual object SetHostExecutionContext(HostExecutionContext hostExecutionContext)
		{
			throw new NotImplementedException();
		}
	}
}
