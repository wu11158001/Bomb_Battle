using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"Newtonsoft.Json.dll",
		"System.Core.dll",
		"System.dll",
		"Unity.Addressables.dll",
		"Unity.Localization.dll",
		"Unity.ResourceManager.dll",
		"UnityEngine.CoreModule.dll",
		"UnityEngine.JSONSerializeModule.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// DelegateList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>>
	// DelegateList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>>
	// DelegateList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// DelegateList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// DelegateList<float>
	// System.Action<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle,object>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Action<float>
	// System.Action<int>
	// System.Action<object,object>
	// System.Action<object>
	// System.Buffers.ArrayPool<int>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool.LockedStack<int>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool.PerCoreLockedStacks<int>
	// System.Buffers.TlsOverPerCoreLockedStacksArrayPool<int>
	// System.ByReference<int>
	// System.Collections.Generic.ArraySortHelper<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.ArraySortHelper<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.Comparer<UnityEngine.Localization.LocaleIdentifier>
	// System.Collections.Generic.Comparer<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Comparer<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Dictionary.Enumerator<System.Guid,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary.Enumerator<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary.Enumerator<int,byte>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<System.Guid,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,byte>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<System.Guid,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary.KeyCollection<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary.KeyCollection<int,byte>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<long,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<System.Guid,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,byte>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<System.Guid,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary.ValueCollection<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary.ValueCollection<int,byte>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<long,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary<System.Guid,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.Dictionary<int,byte>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<long,object>
	// System.Collections.Generic.Dictionary<object,System.ValueTuple<object,object>>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.EqualityComparer<System.Guid>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.EqualityComparer<UnityEngine.Localization.LocaleIdentifier>
	// System.Collections.Generic.EqualityComparer<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.EqualityComparer<byte>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<long>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.Guid,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,byte>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,System.ValueTuple<object,object>>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.ICollection<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.ICollection<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.IComparer<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.IComparer<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Guid,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.UIntPtr,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,byte>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,System.ValueTuple<object,object>>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.IEnumerable<UnityEngine.Localization.Tables.TableReference>
	// System.Collections.Generic.IEnumerable<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.IEnumerable<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Guid,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.UIntPtr,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,byte>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,System.ValueTuple<object,object>>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.IEnumerator<UnityEngine.Localization.Tables.TableReference>
	// System.Collections.Generic.IEnumerator<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.IEnumerator<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEqualityComparer<System.Guid>
	// System.Collections.Generic.IEqualityComparer<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<long>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IList<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.IList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.IList<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.KeyValuePair<System.Guid,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.KeyValuePair<System.UIntPtr,object>
	// System.Collections.Generic.KeyValuePair<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.KeyValuePair<int,byte>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<long,object>
	// System.Collections.Generic.KeyValuePair<object,System.ValueTuple<object,object>>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.LinkedList.Enumerator<object>
	// System.Collections.Generic.LinkedList<object>
	// System.Collections.Generic.LinkedListNode<object>
	// System.Collections.Generic.List.Enumerator<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.List.Enumerator<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.List.Enumerator<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.List<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.List<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.ObjectComparer<UnityEngine.Localization.LocaleIdentifier>
	// System.Collections.Generic.ObjectComparer<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.ObjectComparer<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<System.Guid>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.ObjectEqualityComparer<UnityEngine.Localization.LocaleIdentifier>
	// System.Collections.Generic.ObjectEqualityComparer<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.Generic.ObjectEqualityComparer<byte>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<long>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.Queue.Enumerator<object>
	// System.Collections.Generic.Queue<object>
	// System.Collections.Generic.Stack.Enumerator<object>
	// System.Collections.Generic.Stack<object>
	// System.Collections.Generic.ValueListBuilder<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Comparison<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Comparison<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Comparison<object>
	// System.EventHandler<object>
	// System.Func<System.Collections.Generic.KeyValuePair<int,byte>,byte>
	// System.Func<System.Collections.Generic.KeyValuePair<long,object>,byte>
	// System.Func<System.Collections.Generic.KeyValuePair<long,object>,object>
	// System.Func<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// System.Func<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Func<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Func<object,UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// System.Func<object,UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Func<object,byte>
	// System.Func<object,long>
	// System.Func<object,object,object>
	// System.Func<object,object>
	// System.Func<object>
	// System.Nullable<byte>
	// System.Nullable<int>
	// System.Predicate<System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// System.Predicate<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>
	// System.Predicate<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// System.Predicate<object>
	// System.ReadOnlySpan<int>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<object>
	// System.Runtime.CompilerServices.TaskAwaiter<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// System.Runtime.CompilerServices.TaskAwaiter<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Runtime.CompilerServices.TaskAwaiter<object>
	// System.Span<int>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<object>
	// System.Threading.Tasks.Task<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// System.Threading.Tasks.Task<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Threading.Tasks.Task<object>
	// System.Threading.Tasks.TaskCompletionSource<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// System.Threading.Tasks.TaskCompletionSource<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Threading.Tasks.TaskCompletionSource<object>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<object>
	// System.Threading.Tasks.TaskFactory<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// System.Threading.Tasks.TaskFactory<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// System.Threading.Tasks.TaskFactory<object>
	// System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>
	// System.ValueTuple<object,object>
	// UnityEngine.AddressableAssets.AddressablesImpl.<>c__DisplayClass79_0<object>
	// UnityEngine.Events.InvokableCall<byte>
	// UnityEngine.Events.InvokableCall<float>
	// UnityEngine.Events.UnityAction<byte>
	// UnityEngine.Events.UnityAction<float>
	// UnityEngine.Events.UnityAction<object>
	// UnityEngine.Events.UnityEvent<byte>
	// UnityEngine.Events.UnityEvent<float>
	// UnityEngine.Localization.Operations.GetTableEntryOperation.<>c<object,object>
	// UnityEngine.Localization.Operations.GetTableEntryOperation<object,object>
	// UnityEngine.Localization.Operations.LoadAllTablesOperation.<>c<object,object>
	// UnityEngine.Localization.Operations.LoadAllTablesOperation<object,object>
	// UnityEngine.Localization.Operations.LoadTableOperation.<>c<object,object>
	// UnityEngine.Localization.Operations.LoadTableOperation<object,object>
	// UnityEngine.Localization.Operations.PreloadDatabaseOperation.<>c<object,object>
	// UnityEngine.Localization.Operations.PreloadDatabaseOperation<object,object>
	// UnityEngine.Localization.Operations.PreloadLocaleOperation.<>c<object,object>
	// UnityEngine.Localization.Operations.PreloadLocaleOperation<object,object>
	// UnityEngine.Localization.Operations.PreloadTablesOperation.<>c<object,object>
	// UnityEngine.Localization.Operations.PreloadTablesOperation<object,object>
	// UnityEngine.Localization.Operations.WaitForCurrentOperationAsyncOperationBase<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// UnityEngine.Localization.Operations.WaitForCurrentOperationAsyncOperationBase<object>
	// UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>
	// UnityEngine.Localization.Settings.LocalizedDatabase<object,object>
	// UnityEngine.Localization.Tables.DetailedLocalizationTable.<>c<object>
	// UnityEngine.Localization.Tables.DetailedLocalizationTable<object>
	// UnityEngine.Pool.CollectionPool.<>c<object,System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// UnityEngine.Pool.CollectionPool.<>c<object,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// UnityEngine.Pool.CollectionPool.<>c<object,object>
	// UnityEngine.Pool.CollectionPool<object,System.ValueTuple<UnityEngine.Localization.LocaleIdentifier,object>>
	// UnityEngine.Pool.CollectionPool<object,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>
	// UnityEngine.Pool.CollectionPool<object,object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass60_0<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass60_0<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass60_0<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass61_0<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass61_0<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase.<>c__DisplayClass61_0<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle.<>c<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle.<>c<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle.<>c<object>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.Localization.Settings.LocalizedDatabase.TableEntryResult<object,object>>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
	// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>
	// UnityEngine.ResourceManagement.ChainOperationTypelessDepedency<object>
	// UnityEngine.ResourceManagement.ResourceManager.CompletedOperation<object>
	// UnityEngine.ResourceManagement.Util.GlobalLinkedListNodeCache<object>
	// UnityEngine.ResourceManagement.Util.LinkedListNodeCache<object>
	// }}

	public void RefMethods()
	{
		// object Newtonsoft.Json.JsonConvert.DeserializeObject<object>(string)
		// object Newtonsoft.Json.JsonConvert.DeserializeObject<object>(string,Newtonsoft.Json.JsonSerializerSettings)
		// object System.Activator.CreateInstance<object>()
		// bool System.Linq.Enumerable.All<System.Collections.Generic.KeyValuePair<int,byte>>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,byte>>,System.Func<System.Collections.Generic.KeyValuePair<int,byte>,bool>)
		// bool System.Linq.Enumerable.Any<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,RelayManager.<CreateRelay>d__1>(System.Runtime.CompilerServices.TaskAwaiter<object>&,RelayManager.<CreateRelay>d__1&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<object>.Start<RelayManager.<CreateRelay>d__1>(RelayManager.<CreateRelay>d__1&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,RoomManager.<DeleteLobby>d__18>(System.Runtime.CompilerServices.TaskAwaiter&,RoomManager.<DeleteLobby>d__18&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,RoomManager.<HandleLobbyHeartbeat>d__5>(System.Runtime.CompilerServices.TaskAwaiter&,RoomManager.<HandleLobbyHeartbeat>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,RoomManager.<KickPlayer>d__14>(System.Runtime.CompilerServices.TaskAwaiter&,RoomManager.<KickPlayer>d__14&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,RoomManager.<LeaveLobby>d__13>(System.Runtime.CompilerServices.TaskAwaiter&,RoomManager.<LeaveLobby>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,RelayManager.<JoinRelay>d__2>(System.Runtime.CompilerServices.TaskAwaiter<object>&,RelayManager.<JoinRelay>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,RoomManager.<CreateRoom>d__10>(System.Runtime.CompilerServices.TaskAwaiter<object>&,RoomManager.<CreateRoom>d__10&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,RoomManager.<GetListRoom>d__8>(System.Runtime.CompilerServices.TaskAwaiter<object>&,RoomManager.<GetListRoom>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,RoomManager.<JoinRoom>d__11>(System.Runtime.CompilerServices.TaskAwaiter<object>&,RoomManager.<JoinRoom>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,RoomManager.<QuickJoinRoom>d__12>(System.Runtime.CompilerServices.TaskAwaiter<object>&,RoomManager.<QuickJoinRoom>d__12&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,RoomManager.<RefreshRoom>d__9>(System.Runtime.CompilerServices.TaskAwaiter<object>&,RoomManager.<RefreshRoom>d__9&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,RoomManager.<TransferRoomHost>d__17>(System.Runtime.CompilerServices.TaskAwaiter<object>&,RoomManager.<TransferRoomHost>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,RoomManager.<UpdateLobbyData>d__16>(System.Runtime.CompilerServices.TaskAwaiter<object>&,RoomManager.<UpdateLobbyData>d__16&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,RoomManager.<UpdatePlayerData>d__15>(System.Runtime.CompilerServices.TaskAwaiter<object>&,RoomManager.<UpdatePlayerData>d__15&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RelayManager.<JoinRelay>d__2>(RelayManager.<JoinRelay>d__2&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<CreateRoom>d__10>(RoomManager.<CreateRoom>d__10&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<DeleteLobby>d__18>(RoomManager.<DeleteLobby>d__18&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<GetListRoom>d__8>(RoomManager.<GetListRoom>d__8&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<HandleLobbyHeartbeat>d__5>(RoomManager.<HandleLobbyHeartbeat>d__5&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<JoinRoom>d__11>(RoomManager.<JoinRoom>d__11&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<KickPlayer>d__14>(RoomManager.<KickPlayer>d__14&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<LeaveLobby>d__13>(RoomManager.<LeaveLobby>d__13&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<QuickJoinRoom>d__12>(RoomManager.<QuickJoinRoom>d__12&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<RefreshRoom>d__9>(RoomManager.<RefreshRoom>d__9&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<TransferRoomHost>d__17>(RoomManager.<TransferRoomHost>d__17&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<UpdateLobbyData>d__16>(RoomManager.<UpdateLobbyData>d__16&)
		// System.Void System.Runtime.CompilerServices.AsyncVoidMethodBuilder.Start<RoomManager.<UpdatePlayerData>d__15>(RoomManager.<UpdatePlayerData>d__15&)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<object>(object)
		// System.Void UnityEngine.AddressableAssets.Addressables.Release<object>(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.AddressableAssets.AddressablesImpl.LoadAssetAsync<object>(object)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.AddressableAssets.AddressablesImpl.LoadAssetWithChain<object>(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle,object)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.AddressableAssets.AddressablesImpl.TrackHandle<object>(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>)
		// object UnityEngine.Component.GetComponent<object>()
		// object[] UnityEngine.Component.GetComponentsInChildren<object>(bool)
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object[] UnityEngine.GameObject.GetComponentsInChildren<object>(bool)
		// object UnityEngine.JsonUtility.FromJson<object>(string)
		// object UnityEngine.Object.FindAnyObjectByType<object>()
		// object UnityEngine.Object.FindFirstObjectByType<object>()
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Transform)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Transform,bool)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle.Convert<object>()
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.ResourceManager.CreateChainOperation<object>(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle,System.Func<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object>>)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.ResourceManager.CreateCompletedOperationInternal<object>(object,bool,System.Exception,bool)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.ResourceManager.CreateCompletedOperationWithException<object>(object,System.Exception)
		// object UnityEngine.ResourceManagement.ResourceManager.CreateOperation<object>(System.Type,int,UnityEngine.ResourceManagement.Util.IOperationCacheKey,System.Action<UnityEngine.ResourceManagement.AsyncOperations.IAsyncOperation>)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.ResourceManager.ProvideResource<object>(UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation)
		// UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<object> UnityEngine.ResourceManagement.ResourceManager.StartOperation<object>(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationBase<object>,UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle)
	}
}