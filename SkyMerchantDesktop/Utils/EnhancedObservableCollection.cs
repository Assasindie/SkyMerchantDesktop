using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyMerchantDesktop.Utils
{
	[Serializable]
	public class EnhancedObservableCollection<T> : ObservableCollection<T>
	{
		/// <summary>
		/// Initialises a new empty Enhanced Observable Collection. This collection has the ability to report when items are added, removed or modified or when the list is refreshed/cleared. 
		/// </summary>
		public EnhancedObservableCollection() : base()
		{
		}

		/// <summary>
		/// Initialises a new Enhanced Observable Collection using the items from another enumerable. This collection has the ability to report when items are added, removed or modified or when the list is refreshed/cleared. 
		/// </summary>
		public EnhancedObservableCollection(IEnumerable<T> items) : base(items)
		{
		}

		public void ReportChange(T item)
		{
			NotifyCollectionChangedEventArgs args =
				new NotifyCollectionChangedEventArgs(
					NotifyCollectionChangedAction.Replace,
					item,
					item,
					IndexOf(item));
			OnCollectionChanged(args);
		}

		public void ClearList()
		{
			ClearItems();
		}

		public EnhancedObservableCollection<T> Clone<T>()
		{
			return JsonConvert.DeserializeObject<EnhancedObservableCollection<T>>(JsonConvert.SerializeObject(this));
		}
	}

	[Serializable]
	public class ObservableRangeCollection<T> : ObservableCollection<T>
	{
		/// <summary>
		/// Initialises a new empty Observable Range Collection. This collection has the ability to report when items are added, removed or modified or when the list is refreshed/cleared. 
		/// </summary>
		public ObservableRangeCollection() : base()
		{
		}

		/// <summary>
		/// Initialises a new empty Observable Range Collection. This collection has the ability to report when items are added, removed or modified or when the list is refreshed/cleared. 
		/// </summary>
		public ObservableRangeCollection(IEnumerable<T> items) : base(items)
		{
		}

		public void AddRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add)
		{
			if (notificationMode != NotifyCollectionChangedAction.Add && notificationMode != NotifyCollectionChangedAction.Reset)
				throw new ArgumentException("Mode must be either Add or Reset for AddRange.", nameof(notificationMode));
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));

			CheckReentrancy();

			var startIndex = Count;

			var itemsAdded = AddArrangeCore(collection);

			if (!itemsAdded)
				return;

			if (notificationMode == NotifyCollectionChangedAction.Reset)
			{
				RaiseChangeNotificationEvents(action: NotifyCollectionChangedAction.Reset);

				return;
			}

			var changedItems = collection is List<T> ? (List<T>)collection : new List<T>(collection);

			RaiseChangeNotificationEvents(
				action: NotifyCollectionChangedAction.Add,
				changedItems: changedItems,
				startingIndex: startIndex);
		}

		public void RemoveRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Reset)
		{
			if (notificationMode != NotifyCollectionChangedAction.Remove && notificationMode != NotifyCollectionChangedAction.Reset)
				throw new ArgumentException("Mode must be either Remove or Reset for RemoveRange.", nameof(notificationMode));
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));

			CheckReentrancy();

			if (notificationMode == NotifyCollectionChangedAction.Reset)
			{
				var raiseEvents = false;
				foreach (var item in collection)
				{
					Items.Remove(item);
					raiseEvents = true;
				}

				if (raiseEvents)
					RaiseChangeNotificationEvents(action: NotifyCollectionChangedAction.Reset);

				return;
			}

			var changedItems = new List<T>(collection);
			for (var i = 0; i < changedItems.Count; i++)
			{
				if (!Items.Remove(changedItems[i]))
				{
					changedItems.RemoveAt(i); // can't use foreach 
					i--;
				}
			}

			if (changedItems.Count == 0)
				return;

			RaiseChangeNotificationEvents(
				action: NotifyCollectionChangedAction.Remove,
				changedItems: changedItems);
		}

		public void Replace(T item) => ReplaceRange(new T[] { item });

		public void ReplaceRange(IEnumerable<T> collection)
		{
			if (collection == null)
				throw new ArgumentNullException(nameof(collection));

			CheckReentrancy();

			var previouslyEmpty = Items.Count == 0;

			Items.Clear();

			AddArrangeCore(collection);

			var currentlyEmpty = Items.Count == 0;

			if (previouslyEmpty && currentlyEmpty)
				return;

			RaiseChangeNotificationEvents(action: NotifyCollectionChangedAction.Reset);
		}

		private bool AddArrangeCore(IEnumerable<T> collection)
		{
			var itemAdded = false;
			foreach (var item in collection)
			{
				Items.Add(item);
				itemAdded = true;
			}
			return itemAdded;
		}

		private void RaiseChangeNotificationEvents(NotifyCollectionChangedAction action, List<T>? changedItems = null, int startingIndex = -1)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
			OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));

			if (changedItems is null)
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(action));
			else
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, changedItems: changedItems, startingIndex: startingIndex));
		}

		public ObservableRangeCollection<T> Clone<T>()
		{
			return JsonConvert.DeserializeObject<ObservableRangeCollection<T>>(JsonConvert.SerializeObject(this));
		}
	}
}
