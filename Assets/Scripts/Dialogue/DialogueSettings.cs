using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
public class DialogueSettings : ScriptableObject
{
    // Apenas como exemplo, não há necessidade disso tudo, o Inspector possui um botão para adicionar itens na lista
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence;
    
    public List<Sentences> dialogues = new List<Sentences>();

}

[System.Serializable]
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Languages sentence;
}

[System.Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSettings ds = (DialogueSettings)target;
        
        Languages lang = new Languages();
        lang.portuguese = ds.sentence;

        Sentences sentences = new Sentences();
        sentences.profile = ds.speakerSprite;
        sentences.sentence = lang;

        if (GUILayout.Button("Create Dialogue"))
        {
            if (ds.sentence != null)
            {
                ds.dialogues.Add(sentences);

                ds.speakerSprite = null;
                ds.sentence = "";
            }
        }
    }
}
#endif