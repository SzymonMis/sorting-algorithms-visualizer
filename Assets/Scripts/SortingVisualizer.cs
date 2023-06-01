using System.Collections.Generic;
using UnityEngine;

public class SortingVisualizer : MonoBehaviour
{
	public GameObject columnPrefab;
	public int[] array = new int[45];
	private float offset = 0.2f;
	private float startPosition = -4.35f;
	private float cubeSize = 0.05f;
	private float maxHeight = 3.5f;
	public List<GameObject> columns = new List<GameObject>();

	private void Start()
	{
		GenerateGraph();
	}

	public void GenerateGraph()
	{
		ClearGraph();

		//Randomize values
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = (int)Random.Range(1, maxHeight / cubeSize);
		}

		//Rendering values with cubes
		for (int i = 0; i < array.Length; i++)
		{
			GameObject column = Instantiate(columnPrefab, new Vector3(startPosition + (offset * i), 0, 0), Quaternion.identity, transform);
			column.transform.localScale = new Vector3(cubeSize, cubeSize * array[i], cubeSize);
			column.name = array[i].ToString();
			column.transform.localPosition = new Vector3(column.transform.localPosition.x, 0 + column.transform.localScale.y / 2, column.transform.localPosition.z);
			columns.Add(column);
		}
	}

	private void ClearGraph()
	{
		if (columns != null)
		{
			columns.ForEach(x => DestroyImmediate(x));
		}

		columns.Clear();
	}
}