using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private Stack<UIScreen> screenStack = new Stack<UIScreen>();
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensure only one exists
            return;
        }

        Instance = this;
    }
    public void PushScreen(UIScreen screen)
    {
        screenStack.Push(screen);
        screen.Show();
    }
    public void PopScreen()
    {
        if (screenStack.Count == 0) return;

        UIScreen top = screenStack.Pop();
        top.Hide();

        if (screenStack.Count > 0)
        {
            screenStack.Peek().Show();
        }
    }
    public void HandleBackPress()
    {
        if (screenStack.Count == 0) return;

        UIScreen top = screenStack.Peek();
        bool handled = top.OnBackPressed();

        if (!handled)
        {
            PopScreen();
        }
    }
}