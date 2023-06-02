using System.Collections;
using UnityEngine;

public class SelectionSort : Algorithm
{
	public override IEnumerator Sort(float delay)
	{
		int length = SortingVisualizer.Instance.array.Length;

		for (int i = 0; i < length - 1; i++)
		{
			int minIndex = i;
			SortingVisualizer.Instance.HighlightColumn(minIndex, Color.blue);  // Visually marking the initial minimum index

			for (int j = i + 1; j < length; j++)
			{
				SortingVisualizer.Instance.HighlightColumn(j, Color.yellow);
				yield return new WaitForSeconds(delay);

				if (SortingVisualizer.Instance.array[j] < SortingVisualizer.Instance.array[minIndex])
				{
					SortingVisualizer.Instance.ResetColumnColor(minIndex);
					minIndex = j;
				}

				SortingVisualizer.Instance.ResetColumnColor(j);
			}

			if (minIndex != i)
			{
				// Swap elements in array
				int temp = SortingVisualizer.Instance.array[minIndex];
				SortingVisualizer.Instance.array[minIndex] = SortingVisualizer.Instance.array[i];
				SortingVisualizer.Instance.array[i] = temp;

				SortingVisualizer.Instance.HighlightColumn(minIndex, Color.blue);

				yield return new WaitForSeconds(delay * 100);

				// Swap columns
				SortingVisualizer.Instance.SwapColumns(i, minIndex);
			}

			SortingVisualizer.Instance.HighlightColumn(i, Color.green);
		}

		//Last iteration to visually signal completion of sorting
		for (int i = 0; i < length; i++)
		{
			SortingVisualizer.Instance.HighlightColumn(i, Color.green);
			yield return new WaitForSeconds(0.001f);
			SortingVisualizer.Instance.ResetColumnColor(i);
		}
	}
}