using System.Text;
using System.Text.Json;

namespace Counters
{
	public class AppState
	{
		public List<CounterModel> Counters { get; set; } = new List<CounterModel> { new CounterModel() };
		public event Action OnChange;
		public void AddCounter()
		{
			Counters.Add(new CounterModel { Id = Counters.Count });
			NotifyStateChanged();
		}
		public byte[] Export() 
		{
			var sb = new StringBuilder();
			sb.AppendLine($"*;Name;Count");
			foreach (var counter in Counters.OrderByDescending(x => x.IsFiveStar).ThenByDescending(x => x.Value).ThenBy(x => x.Name))
      {
				sb.AppendLine($"{(counter.IsFiveStar ? "5" : "4")};{counter.Name};{counter.Value}");
      }
			return Encoding.UTF8.GetBytes(sb.ToString());
		}
		private void NotifyStateChanged() => OnChange?.Invoke();
	}
}
