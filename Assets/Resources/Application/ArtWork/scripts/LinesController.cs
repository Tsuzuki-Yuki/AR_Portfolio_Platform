using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesController : MonoBehaviour {

	private int linesNum ;
	[SerializeField] private Transform[] lines;
	private List<Vector3> positions = new List<Vector3>();
	private List<Vector3> rotations = new List<Vector3>();
	private List<Vector3> scales = new List<Vector3>();
	private Dictionary<string, float> seeds = new Dictionary<string, float>();
	private Vector3 positionVec, rotationVec, scaleVec;
	[SerializeField] private Material mat;
	//[SerializeField] private GameObject linePrefab;

	void Start () {
		linesNum = lines.Length;
		
		InitSeeds ();

		SetValueInVecs ();

		for(int i = 0; i < linesNum; i++){
			positions.Add (positionVec);
			rotations.Add(rotationVec);
			scales.Add(scaleVec);

			IncrementKeys ();
			SetValueInVecs ();
			
			//マテリアルを設定する
			Destroy(lines[i].GetComponent<Renderer>().material);
			lines[i].GetComponent<Renderer>().material = mat;
		}


	}

	void Update () {
		
		UpdateTransformList ();
		SetValueInVecs ();
		UpdateLinesTransform ();

	}

	private void InitSeeds(){
		seeds.Add("positionX", Random.value*100);
		seeds.Add("positionY", Random.value*100);
		seeds.Add("positionZ", Random.value*100);
		seeds.Add("rotationX", Random.value*100);
		seeds.Add("rotationY", Random.value*100);
		seeds.Add("rotationZ", Random.value*100);
		seeds.Add("scaleX", Random.value*100);
		seeds.Add("scaleY", Random.value*100);
		seeds.Add("scaleZ", Random.value*100);

	}

	private void SetValueInVecs(){
		positionVec = new Vector3 (
			Perlin.Noise(seeds["positionX"])*10,
			Perlin.Noise(seeds["positionY"])*10,
			Perlin.Noise(seeds["positionZ"])*10
		);

		rotationVec = new Vector3 (
			Perlin.Noise(seeds["rotationX"])*360 * 2f,
			Perlin.Noise(seeds["rotationY"])*360 * 2f,
			Perlin.Noise(seeds["rotationZ"])*360 * 2f
		);

		scaleVec = new Vector3 (
			(Perlin.Noise(seeds["scaleX"]) + 1.0f) * 1f,
			(Perlin.Noise(seeds["scaleY"]) + 1.0f) * 1f,
			(Perlin.Noise(seeds["scaleZ"]) + 1.0f) * 1f
		);
	}

	private void UpdateLinesTransform(){
		for(int i = 0; i < linesNum; i++){
			lines [i].position   = positions [i];
			lines [i].rotation   = Quaternion.Euler(rotations [i]);
			//lines [i].localScale = scales [i];
		}
	}

	private void UpdateTransformList(){
		for(int i = 0; i < linesNum-1; i++){
			positions [i] = positions [i + 1];
			rotations [i] = rotations [i + 1];
			scales    [i] = scales    [i + 1];
		}

		positions [positions.Count - 1] = positionVec;
		rotations [rotations.Count - 1] = rotationVec;
		scales    [scales.Count - 1]    = scaleVec;

		IncrementKeys ();
	}

	private void IncrementKeys() {
		float Posincrement = 0.015f;
		seeds ["positionX"] += Posincrement;
		seeds ["positionY"] += Posincrement;
		seeds ["positionZ"] += Posincrement;
		seeds ["rotationX"] += 0.002f;
		seeds ["rotationY"] += 0.002f;
		seeds ["rotationZ"] += 0.002f;
		seeds ["scaleX"] += 0.02f;
		seeds ["scaleY"] += 0.02f;
		seeds ["scaleZ"] += 0.02f;

	}
}