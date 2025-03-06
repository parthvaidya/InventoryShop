using UnityEngine;
using UnityEngine.UI;

public class ShopViewPortButton : MonoBehaviour
{
    [System.Serializable]
    public struct ViewportBinding
    {
        public Button button;      // UI Button
        public GameObject viewport; // Corresponding viewport
    }

    public ViewportBinding[] bindings; // Array for button-viewport pairs

    void Start()
    {
        // Assign button click events
        for (int i = 0; i < bindings.Length; i++)
        {
            int index = i; // Store index to avoid closure issue
            
            bindings[i].button.onClick.AddListener(() => SetActiveViewport(index));
            
        }

        //SetActiveViewport(0); 
    }

    public void SetActiveViewport(int index)
    {
        for (int i = 0; i < bindings.Length; i++)
        {
            bindings[i].viewport.SetActive(i == index);
            //SoundManager.Instance.Play(Sounds.ShopItems);
        }
    }
}
