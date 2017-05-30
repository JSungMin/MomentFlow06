using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class XmlManager : MonoBehaviour {

	private static XmlManager instance;

	public static XmlManager Instance {
		get {
			if (null == instance) {
				instance = GameObject.Find ("XmlManager").GetComponent<XmlManager>();
				if (instance == null) {
					GameObject newXmlManager = new GameObject ("XmlManager");
					instance = newXmlManager.AddComponent<XmlManager> ();
				}
			}
			return instance;
		}
	}

	public void Awake()
	{
		instance = this;
	}

	public XmlDocument GetXmlDocument(string fileName)
	{
		TextAsset textAsset = (TextAsset)Resources.Load ("XML/" + fileName);
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (textAsset.text);

		return xmlDoc;
	}
}
