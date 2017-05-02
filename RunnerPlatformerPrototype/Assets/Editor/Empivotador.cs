using UnityEngine;
using UnityEditor;
using System.Collections;

public class Empivotador{

	// Use this for initialization
	[MenuItem("Tools/Empivotador")]
	public static void Accion () {
		MeshFilter mf = Selection.activeGameObject.GetComponent<MeshFilter>();
		if(mf==null) {
			return;
		}
		
		Mesh copia = GameObject.Instantiate( mf.sharedMesh ) as Mesh;
		
		Vector3[] vertices = copia.vertices;
		for( int i=0;i<vertices.Length;++i ){
			vertices[i].y+=0.5f;
		}
		
		copia.vertices = vertices;		
		mf.sharedMesh = copia;
		AssetDatabase.CreateAsset( copia, "Assets/malla.asset" );
	}
}
