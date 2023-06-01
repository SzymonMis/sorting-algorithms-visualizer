using System.Collections;
using UnityEngine;

public class BubbleSort : Algorithm
{
	public override IEnumerator Sort()
	{
		int length = SortingVisualizer.Instance.array.Length;
		bool isSorted = false;

		while (!isSorted)
		{
			for (int i = 0; i < length - 1; i++)
			{
				for (int j = 0; j < length - i - 1; j++)
				{
					SortingVisualizer.Instance.HighlightColumn(j, Color.green);
					SortingVisualizer.Instance.HighlightColumn(j + 1, Color.green);

					yield return new WaitForSeconds(0.001f);

					if (SortingVisualizer.Instance.array[j] > SortingVisualizer.Instance.array[j + 1])
					{
						int temp = SortingVisualizer.Instance.array[j];

						SortingVisualizer.Instance.array[j] = SortingVisualizer.Instance.array[j + 1];
						SortingVisualizer.Instance.array[j + 1] = temp;

						SortingVisualizer.Instance.SwapColumns(j, j + 1);
					}
					else
					{
						isSorted = true;
					}

					SortingVisualizer.Instance.ResetColumnColor(j);
					SortingVisualizer.Instance.ResetColumnColor(j + 1);
				}
			}

			length--;

			for (int i = 0; i < SortingVisualizer.Instance.array.Length; i++)
			{
				SortingVisualizer.Instance.HighlightColumn(i, Color.green);

				yield return new WaitForSeconds(0.001f);

				SortingVisualizer.Instance.ResetColumnColor(i);
			}
		}
	}
}