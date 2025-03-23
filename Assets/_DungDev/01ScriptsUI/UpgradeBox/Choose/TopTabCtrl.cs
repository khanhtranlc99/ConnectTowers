using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopTabCtrl : MonoBehaviour
{
    [SerializeField] List<TabBtnDatas> lsBtnImages = new();

    private void Start()
    {
        for (int i = 0; i < lsBtnImages.Count; i++)
        {
            bool isActive = (i == 0); 
            SetBtnState(lsBtnImages[i], isActive);

            int index = i; 
            //lsBtnImages[i].btn.onClick.AddListener(() => OnBtnClick(index));
            lsBtnImages[i].btn.onClick.AddListener(delegate { OnBtnClick(index); });
        }
    }

    void OnBtnClick(int clickParam)
    {
        for (int i = 0; i < lsBtnImages.Count; i++)
        {
            bool isActive = (i == clickParam); 
            SetBtnState(lsBtnImages[i], isActive);
        }
        
    }

    void SetBtnState(TabBtnDatas btnImages, bool isActive)
    {
        btnImages.imgSelect.gameObject.SetActive(isActive);  
        btnImages.imgUnSelect.gameObject.SetActive(!isActive); 

        if (btnImages.transCenter != null)
        {
            btnImages.transCenter.gameObject.SetActive(isActive);
            
        }
    }
}

[System.Serializable]
public class TabBtnDatas
{
    public Button btn;
    public Image imgSelect;
    public Image imgUnSelect;
    public Transform transCenter;
}
