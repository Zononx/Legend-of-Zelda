using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;

    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        InitHeart();
    }
    //Don vi mau'
    public void InitHeart()
    {
        for(int i = 0;i< heartContainers.RuntimeValue; i++)
        {
            if (i < hearts.Length)
            {
				hearts[i].gameObject.SetActive(true);
				hearts[i].sprite = fullHeart;
			}
        }
    }
    public void UpdateHeart()
    {
        InitHeart();
		float tempHeart = playerCurrentHealth.RuntimeValue / 2;
        for(int i = 0; i< heartContainers.RuntimeValue; i++)
        {
            if(i <= tempHeart-1)
            {
                //Full heart
                hearts[i].sprite = fullHeart;
            }
            else if(i >= tempHeart)
            {
                //Emty heart
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                //Half heart
                hearts[i].sprite = halfFullHeart;
            }
        }
    }
}
