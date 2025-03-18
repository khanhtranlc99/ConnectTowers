using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextureSOs/TextureSO")]
public class TextureSO : ScriptableObject
{
    public List<ModelDisplayTexture> lsModelDisplayTextures;

    public ModelDisplayTexture GetModelDisplayTexture(int idParam)
    {
        foreach (var child in this.lsModelDisplayTextures)
        {
            if(child.id == idParam) return child;
        }
        return null;
    }
}

[System.Serializable]
public class ModelDisplayTexture
{
    public int id;
    public GameObject model;
}
