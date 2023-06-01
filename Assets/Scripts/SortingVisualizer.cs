using System.Collections.Generic;
using UnityEngine;

public class SortingVisualizer : MonoBehaviour
{
	public static SortingVisualizer Instance;// Simple Singleton instance

	public GameObject columnPrefab;
	public int[] array = new int[45];
	private float offset = 0.2f;
	private float startPosition = -4.35f;
	private float cubeSize = 0.05f;
	private float maxHeight = 3.5f;
	private List<GameObject> columns = new List<GameObject>();
	public Algorithm sortingAlgorithm;

	private void Awake() => Instance = this;

	private void Start()
	{
		GenerateGraph();
		StartCoroutine(sortingAlgorithm.Sort());
	}

	public void GenerateGraph()
	{
		ClearGraph();

		//Randomizing values
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (int)Random.Range(1, maxHeight / cubeSize);
		}

		//Rendering values using cubes
		for (int i = 0; i < array.Length; i++)
		{
			GameObject column = Instantiate(columnPrefab, new Vector3(startPosition + (offset * i), 0, 0), Quaternion.identity, transform);
			column.transform.localScale = new Vector3(cubeSize, cubeSize * array[i], cubeSize);
			column.name = array[i].ToString();
			column.transform.localPosition = new Vector3(column.transform.localPosition.x, 0 + column.transform.localScale.y / 2, column.transform.localPosition.z);
			columns.Add(column);
		}
	}

	public void SwapColumns(int firstIndex, int secondIndex)
	{
		//Get columns by index
		GameObject firstColumn = columns[firstIndex];
		GameObject secondColumn = columns[secondIndex];

		//Change order in hierarchy
		int firstSiblingIndex = firstColumn.transform.GetSiblingIndex();
		int secondSiblingIndex = secondColumn.transform.GetSiblingIndex();

		firstColumn.transform.SetSiblingIndex(secondSiblingIndex);
		secondColumn.transform.SetSiblingIndex(firstSiblingIndex);

		//Change order in scene
		Vector3 firstPosition = firstColumn.transform.position;
		Vector3 secondPosition = secondColumn.transform.position;

		firstColumn.transform.position = new Vector3(secondPosition.x, firstPosition.y, firstPosition.z);
		secondColumn.transform.position = new Vector3(firstPosition.x, secondPosition.y, secondPosition.z);

		//Change order in list
		columns[firstIndex] = secondColumn;
		columns[secondIndex] = firstColumn;
	}

	public void HighlightColumn(int index, Color color)
	{
		if (index >= 0 && index < columns.Count)
		{
			GameObject column = columns[index];
			Renderer renderer = column.GetComponent<Renderer>();
			float emissiveIntensity = 4.46f;

			renderer.material.color = color;
			renderer.material.SetColor("_EmissiveColor", color * emissiveIntensity);
		}
	}

	public void ResetColumnColor(int index) => HighlightColumn(index, Color.white);

	private void ClearGraph()
	{
		if (columns != null)
		{
			columns.ForEach(x => DestroyImmediate(x));
		}

		columns.Clear();
	}
}