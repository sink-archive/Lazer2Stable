using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace Lazer2Stable
{
	public class Spinner
	{
		private int _spinnerCharIndex;

		private readonly ImmutableDictionary<int, char> _spinnerChars = new Dictionary<int, char>
		{
			{0, '⠁'},
			{1, '⠈'},
			{2, '⠐'},
			{3, '⠠'},
			{4, '⢀'},
			{5, '⡀'},
			{6, '⠄'},
			{7, '⠂'}
		}.ToImmutableDictionary();

		private readonly int _repeatIndex;

		public Spinner() => _repeatIndex = _spinnerChars.Count - 1;

		public void Advance()
		{
			var col = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(_spinnerChars[_spinnerCharIndex]);
			Console.ForegroundColor = col;
			Console.CursorLeft--;
			_spinnerCharIndex++;
			if (_spinnerCharIndex > _repeatIndex)
				_spinnerCharIndex = 0;
		}

		private static readonly List<SpinnerTask> RunningTasks = new();
		
		public static void RunSpinner()
		{
			var st = new SpinnerTask();
			RunningTasks.Add(st);
			st.Start();
		}

		public static void StopAllSpinners()
		{
			foreach (var task in RunningTasks) task.Stop();
		}
	}

	internal class SpinnerTask
	{
		private readonly Spinner _spinner = new();
		private          bool    _run     = true;

		public void Stop() => _run = false;

		public void Start()
		{
			void TaskFunc()
			{
				while (_run)
				{
					_spinner.Advance();
					Thread.Sleep(75);
				}
			}
			
			Task.Factory.StartNew(TaskFunc);
		}
	}
}