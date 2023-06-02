using System.Collections;
using UnityEngine;

public class BubbleSort : Algorithm
{
	public override IEnumerator Sort(float delay)
	{
		int length = SortingVisualizer.Instance.array.Length;
		int temp;
		bool swapped;

		for (int i = 0; i < length - 1; i++)
		{
			swapped = false;

			for (int j = 0; j < length - i - 1; j++)
			{
				SortingVisualizer.Instance.HighlightColumn(j, Color.green);
				SortingVisualizer.Instance.HighlightColumn(j + 1, Color.green);

				yield return new WaitForSeconds(delay);

				if (SortingVisualizer.Instance.array[j] > SortingVisualizer.Instance.array[j + 1])
				{
					temp = SortingVisualizer.Instance.array[j];

					SortingVisualizer.Instance.array[j] = SortingVisualizer.Instance.array[j + 1];
					SortingVisualizer.Instance.array[j + 1] = temp;

					SortingVisualizer.Instance.SwapColumns(j, j + 1);
					swapped = true;
				}

				SortingVisualizer.Instance.ResetColumnColor(j);
				SortingVisualizer.Instance.ResetColumnColor(j + 1);
			}
			if (!swapped)
			{
				break;
			}
		}

		//Last iteration to visually signal completion of sorting
		for (int i = 0; i < SortingVisualizer.Instance.array.Length; i++)
		{
			SortingVisualizer.Instance.HighlightColumn(i, Color.green);
			yield return new WaitForSeconds(0.001f);
			SortingVisualizer.Instance.ResetColumnColor(i);
		}
	}
}